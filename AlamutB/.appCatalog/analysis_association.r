###############################################################################
# Co-occurrence analysis of floristic presence/absence data
#
# This script:
#   1. Reads a species x dataset matrix (0/1) from CSV or Excel
#   2. Validates and cleans the matrix
#   3. Computes the observed C-score
#   4. Generates null matrices via the fixed-fixed swap algorithm
#   5. Computes SES and two-sided p-value
#   6. Computes Jaccard and Sorensen indices for every species pair
#   7. Writes a results table to CSV (and optionally Excel)
#
# Requirements:
#   install.packages("vegan")    # for oecosimu, nestedchecker
#   install.packages("readxl")   # only if input is .xlsx
#   install.packages("writexl")  # only if output should be .xlsx
#
# Author: msht
# Last updated: 1405.09.06
###############################################################################


## ===========================================================================
## 1. USER CONFIGURATION -- edit only this block
## ===========================================================================

# Path to the input file. Can be .csv, .xlsx, or .xls
input_file <- "D:/alamut_matrix.csv"

# Path to the output file. Use .csv for a simple table, or .xlsx for Excel.
output_file <- "D:/alamut_matrix_out.csv"

# Which sheet to read if the input is Excel? Ignored for CSV.
# Use 1 for the first sheet, or the sheet name in quotes, e.g. "Sheet1".
excel_sheet <- 1

# Does the input file have a header row with dataset names? Almost always TRUE.
has_header <- TRUE

# Does the first column contain species names (to be used as row names)?
# TRUE means: column 1 = species names, columns 2..n = datasets.
species_in_first_column <- TRUE

# Number of null matrices to generate. 5000 is a good default.
# Increase to 10000 if p-values come out at the minimum possible value.
n_permutations <- 5000

# Number of swap attempts per matrix cell. Higher = better mixing but slower.
# 10 is a reasonable default for matrices of a few hundred species.
swaps_per_cell <- 10

# Random seed for reproducibility. Change it if you want a different run,
# but keep it fixed if you want to reproduce a previous result.
random_seed <- 12345


## ===========================================================================
## 2. LOAD REQUIRED PACKAGES
## ===========================================================================

# vegan provides oecosimu() and nestedchecker()
if (!requireNamespace("vegan", quietly = TRUE)) {
  stop("Package 'vegan' is not installed. Run: install.packages('vegan')")
}
library(vegan)

# readxl is only needed for Excel input
read_excel_available <- requireNamespace("readxl", quietly = TRUE)

# writexl is only needed for Excel output
write_excel_available <- requireNamespace("writexl", quietly = TRUE)


## ===========================================================================
## 3. READ THE INPUT FILE (CSV or Excel)
## ===========================================================================

# Determine file type from the extension (case-insensitive)
ext <- tolower(tools::file_ext(input_file))

if (ext %in% c("csv", "txt")) {
  # ---- CSV / plain text ----
  message("Reading CSV file: ", input_file)
  raw <- read.csv(
    input_file,
    header = has_header,
    check.names = FALSE,      # keep dataset names exactly as written
    stringsAsFactors = FALSE,
    na.strings = c("", "NA", "NaN")
  )

} else if (ext %in% c("xlsx", "xls")) {
  # ---- Excel ----
  if (!read_excel_available) {
    stop("Package 'readxl' is required for Excel input. Run: install.packages('readxl')")
  }
  message("Reading Excel file: ", input_file, " (sheet: ", excel_sheet, ")")
  raw <- as.data.frame(
    readxl::read_excel(
      input_file,
      sheet = excel_sheet,
      col_names = has_header,
      .name_repair = "minimal"
    )
  )
  raw <- as.data.frame(raw, stringsAsFactors = FALSE)

} else {
  stop("Unsupported file type: .", ext, ". Use .csv, .xlsx, or .xls.")
}


## ===========================================================================
## 4. SEPARATE SPECIES NAMES FROM THE DATA MATRIX
## ===========================================================================

if (species_in_first_column) {
  species_names <- as.character(raw[[1]])
  mat_df <- raw[, -1, drop = FALSE]
} else {
  # If species are in row names already (rare for CSV/Excel)
  species_names <- rownames(raw)
  mat_df <- raw
}

# Sanity check: species names must be present and unique
if (any(is.na(species_names)) || any(species_names == "")) {
  stop("Some species names are missing or empty in the first column.")
}
if (anyDuplicated(species_names)) {
  warning("Duplicate species names found. They will be kept, but check your data.")
}

# Dataset names are the column names of mat_df
dataset_names <- colnames(mat_df)
if (is.null(dataset_names) || any(dataset_names == "")) {
  stop("Dataset names (column headers) are missing. Check the header row.")
}

message("Matrix read: ", nrow(mat_df), " species x ", ncol(mat_df), " datasets.")


## ===========================================================================
## 5. VALIDATE AND CLEAN THE MATRIX
## ===========================================================================

# Convert everything to numeric (Excel may read 0/1 as text in some cases)
mat_numeric <- suppressWarnings(
  matrix(
    as.numeric(as.matrix(mat_df)),
    nrow = nrow(mat_df),
    ncol = ncol(mat_df),
    dimnames = list(species_names, dataset_names)
  )
)

# Check for non-numeric or NA values introduced by conversion
if (any(is.na(mat_numeric))) {
  bad_rows <- which(rowSums(is.na(mat_numeric)) > 0)
  stop(
    "Non-numeric or missing values found in these species rows: ",
    paste(head(bad_rows, 10), collapse = ", "),
    if (length(bad_rows) > 10) " ..." else ""
  )
}

# Check that all values are 0 or 1 (or coerce with a warning)
unique_vals <- sort(unique(as.vector(mat_numeric)))
if (!all(unique_vals %in% c(0, 1))) {
  warning(
    "Matrix contains values other than 0 and 1: ",
    paste(unique_vals, collapse = ", "),
    ". Values > 0 will be treated as presence (1)."
  )
  mat_numeric[mat_numeric > 0] <- 1
}

# Remove species that occur in zero datasets (all-zero rows)
all_zero <- rowSums(mat_numeric) == 0
if (any(all_zero)) {
  message("Removing ", sum(all_zero), " species with zero occurrences.")
  mat_numeric <- mat_numeric[!all_zero, , drop = FALSE]
}

# Remove datasets that contain zero species (all-zero columns)
empty_datasets <- colSums(mat_numeric) == 0
if (any(empty_datasets)) {
  message("Removing ", sum(empty_datasets), " datasets with zero species.")
  mat_numeric <- mat_numeric[, !empty_datasets, drop = FALSE]
}

# Remove species occurring in fewer than 3 datasets (rare-species filter)
# This is the threshold recommended in the paper's Methods section.
min_occurrences <- 3
rare <- rowSums(mat_numeric) < min_occurrences
if (any(rare)) {
  message("Removing ", sum(rare), " species occurring in fewer than ",
          min_occurrences, " datasets.")
  mat_numeric <- mat_numeric[!rare, , drop = FALSE]
}

if (nrow(mat_numeric) < 2 || ncol(mat_numeric) < 2) {
  stop("After filtering, the matrix has fewer than 2 species or 2 datasets.")
}

message("Final matrix: ", nrow(mat_numeric), " species x ",
        ncol(mat_numeric), " datasets.")


## ===========================================================================
## 6. DEFINE THE C-SCORE STATISTIC FOR oecosimu
## ===========================================================================

# oecosimu expects a function that takes a community matrix and returns
# a single numeric value (or a named numeric vector).
# nestedchecker() returns several quantities; we keep only the C-score,
# which is the mean number of checkerboard units per species pair.

cscore_statistic <- function(x) {
  res <- nestedchecker(x)
  # res$C.score is the mean checkerboard score across all pairs
  return(c(C.score = res$C.score))
}


## ===========================================================================
## 7. RUN THE NULL MODEL ANALYSIS
## ===========================================================================

message("Running null model analysis with ", n_permutations,
        " permutations and ", swaps_per_cell, " swaps per cell...")

set.seed(random_seed)

# method = "swap" is the fixed-fixed algorithm: both row and column sums
# are preserved. This is the model recommended in the paper.
start_time <- Sys.time()

oecosim_result <- oecosimu(
  comm        = mat_numeric,
  nestfun     = cscore_statistic,
  method      = "swap",
  nsimul      = n_permutations,
  alternative = "two.sided",
  burnin      = 0,          # swap() does not use burnin; kept for clarity
  thin        = 1
)

end_time <- Sys.time()
message("Null model completed in ",
        round(as.numeric(difftime(end_time, start_time, units = "mins")), 2),
        " minutes.")


## ===========================================================================
## 8. EXTRACT OVERALL SES AND P-VALUE
## ===========================================================================

observed_cscore <- as.numeric(oecosim_result$statistic["C.score"])
null_scores     <- as.numeric(oecosim_result$oecosimu$simulated)
null_mean       <- mean(null_scores)
null_sd         <- sd(null_scores)

# Standardized effect size: how many SDs the observed value lies from
# the null mean. Positive SES means more segregation than expected;
# negative SES means more aggregation than expected.
ses <- if (is.na(null_sd) || null_sd == 0) NA_real_ else
  (observed_cscore - null_mean) / null_sd

# Two-sided empirical p-value: proportion of null values at least as
# extreme as the observed value, relative to the null mean.
extreme <- sum(abs(null_scores - null_mean) >= abs(observed_cscore - null_mean))
p_value <- (extreme + 1) / (length(null_scores) + 1)  # +1 avoids p = 0

overall_results <- data.frame(
  metric         = c("observed_C_score", "null_mean", "null_sd",
                     "SES", "p_value_two_sided", "n_permutations", "seed"),
  value          = c(observed_cscore, null_mean, null_sd,
                     ses, p_value, n_permutations, random_seed)
)

message("Overall C-score: ", round(observed_cscore, 4),
        " | SES: ", round(ses, 3),
        " | p: ", signif(p_value, 3))


## ===========================================================================
## 9. COMPUTE PAIRWISE JACCARD AND SORENSEN INDICES
## ===========================================================================

# For every species pair, build the 2x2 table (a, b, c, d) and compute:
#   Jaccard  = a / (a + b + c)
#   Sorensen = 2a / (2a + b + c)
# Neither uses d, which is the cell most affected by unequal dataset areas.

n_species <- nrow(mat_numeric)
species_ids <- rownames(mat_numeric)

# Pre-allocate result vectors. Number of pairs = n*(n-1)/2.
n_pairs <- n_species * (n_species - 1) / 2
pair_species_a <- character(n_pairs)
pair_species_b <- character(n_pairs)
pair_a <- integer(n_pairs)
pair_b <- integer(n_pairs)
pair_c <- integer(n_pairs)
pair_d <- integer(n_pairs)

idx <- 1L
for (i in seq_len(n_species - 1)) {
  for (j in seq(i + 1, n_species)) {
    x <- mat_numeric[i, ]
    y <- mat_numeric[j, ]
    a <- sum(x == 1 & y == 1)
    b <- sum(x == 1 & y == 0)
    c <- sum(x == 0 & y == 1)
    d <- sum(x == 0 & y == 0)
    pair_species_a[idx] <- species_ids[i]
    pair_species_b[idx] <- species_ids[j]
    pair_a[idx] <- a
    pair_b[idx] <- b
    pair_c[idx] <- c
    pair_d[idx] <- d
    idx <- idx + 1L
  }
}

pair_results <- data.frame(
  species_A = pair_species_a,
  species_B = pair_species_b,
  a = pair_a,
  b = pair_b,
  c = pair_c,
  d = pair_d,
  C_score   = pair_b * pair_c,
  Jaccard   = ifelse((pair_a + pair_b + pair_c) == 0, NA,
                     pair_a / (pair_a + pair_b + pair_c)),
  Sorensen  = ifelse((2 * pair_a + pair_b + pair_c) == 0, NA,
                     (2 * pair_a) / (2 * pair_a + pair_b + pair_c)),
  stringsAsFactors = FALSE
)

# Rank pairs by Jaccard (descending) and add a rank column
pair_results$Jaccard_rank <- rank(-pair_results$Jaccard, ties.method = "min")


## ===========================================================================
## 10. WRITE OUTPUT FILES
## ===========================================================================

# Always write the overall results as CSV
overall_out <- sub("\\.[^.]+$", "_overall.csv", output_file)
write.csv(overall_results, overall_out, row.names = FALSE)
message("Overall results written to: ", overall_out)

# Always write the pairwise results as CSV
pairs_out <- sub("\\.[^.]+$", "_pairs.csv", output_file)
write.csv(pair_results, pairs_out, row.names = FALSE)
message("Pairwise results written to: ", pairs_out)

# If the user asked for Excel output, also write an .xlsx workbook
if (tolower(tools::file_ext(output_file)) %in% c("xlsx", "xls")) {
  if (!write_excel_available) {
    warning("Package 'writexl' not installed; skipping Excel output. ",
            "Run: install.packages('writexl')")
  } else {
    writexl::write_xlsx(
      list(
        Overall = overall_results,
        Pairs   = pair_results
      ),
      path = output_file
    )
    message("Excel workbook written to: ", output_file)
  }
}


## ===========================================================================
## 11. PRINT A SHORT SUMMARY TO THE CONSOLE
## ===========================================================================

cat("\n================ ANALYSIS SUMMARY ================\n")
cat("Species analysed      :", n_species, "\n")
cat("Datasets analysed     :", ncol(mat_numeric), "\n")
cat("Species pairs         :", n_pairs, "\n")
cat("Permutations          :", n_permutations, "\n")
cat("Observed C-score      :", round(observed_cscore, 4), "\n")
cat("Null mean (SD)        :", round(null_mean, 4),
    "(", round(null_sd, 4), ")\n")
cat("SES                   :", round(ses, 3), "\n")
cat("Two-sided p-value     :", signif(p_value, 3), "\n")
cat("Interpretation        :",
    ifelse(is.na(ses), "insufficient data",
           ifelse(ses > 2,  "significant segregation (SES > 2)",
                  ifelse(ses < -2, "significant aggregation (SES < -2)",
                         "no strong deviation from randomness"))),
    "\n")
cat("==================================================\n")