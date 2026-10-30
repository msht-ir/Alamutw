using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AlamutS.Models
    {
    //BaseInfo
    public class User
        {
        public int UserId { get; set; } = 0;
        public string UserNickname { get; set; } = "";
        public string Username { get; set; } = "";
        [Required (ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";
        public string UserEmail { get; set; } = "";
        public DateTimeOffset? ExpDateTime { get; set; } = new DateTimeOffset ();
        public int UserTags { get; set; } = 0;
        public bool IsActive
            {
            get => (UserTags & 1) == 1;
            set
                {
                if (value)
                    UserTags |= 1;
                else
                    UserTags &= ~1;
                }
            }
        public bool UseFaCalendar
            {
            get => (UserTags & 2) == 2;
            set
                {
                if (value)
                    UserTags |= 2;
                else
                    UserTags &= ~2;
                }
            }
        public bool RemindDueNotes
            {
            get => (UserTags & 4) == 4;
            set
                {
                if (value)
                    UserTags |= 4;
                else
                    UserTags &= ~4;
                }
            }
        public bool DueNotesDays
            {
            get => (UserTags & 8) == 8;
            set
                {
                if (value)
                    UserTags |= 8;
                else
                    UserTags &= ~8;
                }
            }
        public bool CanChangeNickname
            {
            get => (UserTags & 16) == 16;
            set
                {
                if (value)
                    UserTags |= 16;
                else
                    UserTags &= ~16;
                }
            }
        public bool OnExpireInactive
            {
            get => (UserTags & 32) == 32;
            set
                {
                if (value)
                    UserTags |= 32;
                else
                    UserTags &= ~32;
                }
            }
        public bool SilverAcc
            {
            get => (UserTags & 64) == 64;
            set
                {
                if (value)
                    UserTags |= 64;
                else
                    UserTags &= ~64;
                }
            }
        public bool GoldAcc
            {
            get => (UserTags & 128) == 128;
            set
                {
                if (value)
                    UserTags |= 128;
                else
                    UserTags &= ~128;
                }
            }
        public bool AdminAcc
            {
            get => (UserTags & 256) == 256;
            set
                {
                if (value)
                    UserTags |= 256;
                else
                    UserTags &= ~256;
                }
            }
        public List<Note> UserNotes { get; set; } = new List<Note> ();
        }
    public class Setting
        {
        public string SettingKey { get; set; } = "";
        public string SettingValue { get; set; } = "";
        }
    public class Country
        {
        public int CountryId { get; set; } = 0;
        public string CountryName { get; set; } = "";
        }
    public class Province
        {
        public int ProvinceId { get; set; } = 0;
        public int CountryId { get; set; } = 0;
        public string ProvinceName { get; set; } = "";
        public List<Dataset> Datasets { get; set; } = new List<Dataset> ();
        }
    //Taxonomy
    public class ApgGroup
        {
        public int ApgGroupId { get; set; } = 0;
        public int ApgGroupSeq { get; set; } = 0;
        public string ApgGroupName { get; set; } = "";
        public List<Order> Orders { get; set; } = new List<Order> ();
        }
    public class Order
        {
        public int OrderId { get; set; } = 0;
        public int ApgGroupId { get; set; } = 0;
        public string OrderName { get; set; } = "";
        public int OrderSequence { get; set; } = 0;
        public string OrderNote { get; set; } = "";
        }
    public class Family
        {
        public int FamilyId { get; set; } = 0;
        public int OrderId { get; set; } = 0;
        public string FamilyName { get; set; } = "";
        public string FamilyAut { get; set; } = "";
        public string Protolog { get; set; } = "";
        public string DeltaNote { get; set; } = "";
        public string FlowerFormula { get; set; } = "";
        public int FamilyTags { get; set; } = 0;
        public List<Genus> Genera { get; set; } = new List<Genus> ();
        }
    public class Genus
        {
        public int GenusId { get; set; } = 0;
        public int FamilyId { get; set; } = 0;
        public string GenusName { get; set; } = "";
        public string GenusAut { get; set; } = "";
        }
    public class Species
        {
        public string SpeciesId { get; set; } = "";
        public string AcceptedId { get; set; } = "";
        public int?   ApgGroupId { get; set; } = 0;
        public string? ApgGroupName { get; set; } = "";
        public int? OrderId { get; set; } = 0;
        public string? OrderName { get; set; } = "";
        public int? FamilyId { get; set; } = 0;
        public string? FamilyName { get; set; } = "";
        public int GenusId { get; set; } = 0;
        public string? GenusName { get; set; } = "";
        public string Epithet { get; set; } = "";
        public int Rank { get; set; } = 0;
        public string InfraEpithet { get; set; } = "";
        public string Author { get; set; } = "";
        public int NomStatus { get; set; } = 0;
        public int TaxStatus { get; set; } = 0;
        public int ConsStatus { get; set; } = 0;
        public int Characters { get; set; } = 0;
        public string Chorotype { get; set; } = "";
        public string Lifeform { get; set; } = "";
        public bool IsHybrid { get; set; } = false;
        public string SciName { get; set; } = "";
        public bool IsSelected { get; set; } = false;
        }
    //DATA
    public class DatasetData
        {
        public int DatasetDataId { get; set; } = 0;
        public int DatasetId { get; set; } = 0;
        public string SpeciesId { get; set; } = "";
        public string Infraspecific { get; set; } = "";
        public string LineString { get; set; } = "";
        public string ParsedGenus { get; set; } = "";
        public string ParsedEpithet { get; set; } = "";
        public string SciName { get; set; } = ""; //not a table column
        public bool IsSelected { get; set; } = false; //for page only
        }
    public class StationData
        {
        public int StationDataId { get; set; } = 0;
        public int StationId { get; set; } = 0;
        public int Yr { get; set; } = 0;
        public string DataType { get; set; } = "";
        public decimal M01 { get; set; } = 0;
        public decimal M02 { get; set; } = 0;
        public decimal M03 { get; set; } = 0;
        public decimal M04 { get; set; } = 0;
        public decimal M05 { get; set; } = 0;
        public decimal M06 { get; set; } = 0;
        public decimal M07 { get; set; } = 0;
        public decimal M08 { get; set; } = 0;
        public decimal M09 { get; set; } = 0;
        public decimal M10 { get; set; } = 0;
        public decimal M11 { get; set; } = 0;
        public decimal M12 { get; set; } = 0;
        }
    public class Dataset
        {
        public int DatasetId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public int DatasetType { get; set; } = 0; //1:FL 2:CN 3:LM 4:ST
        public string DatasetAbbr { get; set; } = "";
        public string DatasetName { get; set; } = "";
        public string DatasetRef { get; set; } = "";
        public int RefYear { get; set; } = 0;
        public string RefLang { get; set; } = "";
        public string DatasetManager { get; set; } = "";
        public int AreaHa { get; set; } = 0;
        public int ProvinceId { get; set; } = 0;
        public int CountryId { get; set; } = 0;
        public string ProvinceName { get; set; } = "";
        public string Coords { get; set; } = "";
        public int AltMax { get; set; } = 0;
        public int AltMin { get; set; } = 0;
        public int AltAver { get; set; } = 0;
        public Decimal TempMax { get; set; } = 0;
        public Decimal TempMin { get; set; } = 0;
        public Decimal TempAver { get; set; } = 0;
        public Decimal Precipitation { get; set; } = 0;
        public int DatasetTags { get; set; } = 0;
        public bool IsSelected { get; set; } = false;
        }
    public class Station
        {
        public int StationId { get; set; } = 0;
        public int StationCode { get; set; } = 0;
        public string StationName { get; set; } = "";
        public int StationAlt { get; set; } = 0;
        public string StationCoords { get; set; } = "";
        }
    public class Set
        {
        public int SetId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public int SetType { get; set; } = 0;
        public string SetName { get; set; } = "";
        public int SetTags { get; set; } = 0;
        public bool IsSelected { get; set; } = false;
        public int SelectedSubset { get; set; } = 0;
        }
    public class SetData
        {
        public int SetDataId { get; set; } = 0;
        public int SetId { get; set; } = 0;
        public int DatasetId { get; set; } = 0;
        public int Subsets { get; set; } = 0;
        public string? DatasetName { get; set; } = "";
        public bool Subset1
            {
            get => (Subsets & 1) == 1;
            set
                {
                if (value)
                    Subsets |= 1;
                else
                    Subsets &= ~1;
                }
            }
        public bool Subset2
            {
            get => (Subsets & 2) == 2;
            set
                {
                if (value)
                    Subsets |= 2;
                else
                    Subsets &= ~2;
                }
            }
        public bool Subset3
            {
            get => (Subsets & 4) == 4;
            set
                {
                if (value)
                    Subsets |= 4;
                else
                    Subsets &= ~4;
                }
            }
        public bool Subset4
            {
            get => (Subsets & 8) == 8;
            set
                {
                if (value)
                    Subsets |= 8;
                else
                    Subsets &= ~8;
                }
            }
        public bool Subset5
            {
            get => (Subsets & 16) == 16;
            set
                {
                if (value)
                    Subsets |= 16;
                else
                    Subsets &= ~16;
                }
            }

        public bool IsSelected { get; set; } = false;
        }
    public class Note
        {
        public int NoteId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public int RefType { get; set; } = 0;
        public int RefId { get; set; } = 0;
        public string RefName { get; set; } = "";
        public DateTimeOffset NoteDateTime { get; set; } = new DateTimeOffset ();
        public DateTimeOffset? NoteDateTimeDue { get; set; } = new DateTimeOffset ();
        public string NoteText { get; set; } = "";
        public int NoteTags { get; set; } = 0;
        public bool IsRtl
            {
            get => (NoteTags & 1) == 1;
            set
                {
                if (value)
                    NoteTags |= 1;
                else
                    NoteTags &= ~1;
                }
            }
        public bool IsShared
            {
            get => (NoteTags & 2) == 2;
            set
                {
                if (value)
                    NoteTags |= 2;
                else
                    NoteTags &= ~2;
                }
            }
        public bool IsBookmarked
            {
            get => (NoteTags & 4) == 4;
            set
                {
                if (value)
                    NoteTags |= 4;
                else
                    NoteTags &= ~4;
                }
            }
        public bool IsTodo
            {
            get => (NoteTags & 8) == 8;
            set
                {
                if (value)
                    NoteTags |= 8;
                else
                    NoteTags &= ~8;
                }
            }
        public bool IsRead
            {
            get => (NoteTags & 16) == 16;
            set
                {
                if (value)
                    NoteTags |= 16;
                else
                    NoteTags &= ~16;
                }
            }
        public bool IsDone
            {
            get => (NoteTags & 32) == 32;
            set
                {
                if (value)
                    NoteTags |= 32;
                else
                    NoteTags &= ~32;
                }
            }
        }
    public class Notebook
        {
        public int NotebookId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public string NotebookName { get; set; } = "";
        public int NotebookTags { get; set; }
        //wrapper properties for each flag
        public bool IsActive
            {
            get => (NotebookTags & 1) == 1;
            set
                {
                if (value)
                    NotebookTags |= 1;
                else
                    NotebookTags &= ~1;
                }
            }
        public List<Section> Sections { get; set; } = new List<Section> ();
        public List<Note> Notes { get; set; } = new List<Note> ();
        }
    public class Section
        {
        public int SectionId { get; set; } = 0;
        public int NotebookId { get; set; } = 0;
        public string SectionName { get; set; } = "";
        public int SectionTags { get; set; } = 0;
        public List<Note> Notes { get; set; } = new List<Note> ();
        }
    //Analyses
    public class Diversity
        {
        public int DatasetIdA { get; set; } = 0;
        public int DatasetIdB { get; set; } = 0;
        public string DatasetNameA { get; set; } = "";
        public string DatasetNameB { get; set; } = "";
        public int CountA { get; set; } = 0;
        public int CountB { get; set; } = 0;
        public int SharedSpecies { get; set; } = 0;
        public int BetaDiv { get; set; } = 0;
        public decimal DissimS { get; set; } = 0;
        public decimal DissimJ { get; set; } = 0;
        }
    public class FV
        {
        public int NFam { get; set; }
        public int NGen { get; set; }
        public int NSp { get; set; }
        public int NEndem { get; set; }
        public int NRare { get; set; }
        public int NMed { get; set; }
        public int NRange { get; set; }
        public int NCR { get; set; }
        public int NEN { get; set; }
        public int NVU { get; set; }
        public int NNT { get; set; }
        public int NLC { get; set; }
        public int NLR { get; set; }
        public int NDD { get; set; }
        public int NNE { get; set; }
        public int FV1 { get; set; }
        public decimal FV2 { get; set; }
        }
    public class Occurrence
        {
        public int DatasetId { get; set; }
        public string DatasetName { get; set; }
        public string SpeciesId { get; set; }
        public string AcceptedId { get; set; }
        public string SciName { get; set; }
        }
    //MapPoint
    public class MapPoint
        {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Name { get; set; }
        }

    }
