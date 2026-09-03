using AlamutB.Services2Backend;
using AlamutS.Models;
using Microsoft.AspNetCore.Mvc;
namespace AlamutB.Backend
    {
    [ApiController]
    [Route ("api")]
    public class BeController : Controller
        {
        #region Injection and Constructor
        private readonly IConfiguration _config;
        private readonly BeIService _BeService;
        public BeController (IConfiguration config, BeIService BeService)
            {
            _config = config;
            _BeService = BeService;
            }
        #endregion

        #region USER
        [HttpPost ("Create_User")]
        public async Task<ActionResult<bool>> Create_User ([FromBody] User user)
            {
            var result = await _BeService.Create_UserAsync (user);
            return Ok (result);
            }
        [HttpPost ("Read_Users")]
        public async Task<ActionResult<List<User>>> Read_Users ()
            {
            var result = await _BeService.Read_UsersAsync ();
            return Ok (result);
            }
        [HttpPost ("Read_User")]
        public async Task<ActionResult<User?>> Read_User ([FromQuery] string username, [FromBody] string password)
            {
            var result = await _BeService.Read_UserAsync (username, password);
            return Ok (result);
            }
        [HttpPost ("Read_NewUsers")]
        public async Task<ActionResult<List<User>>> Read_NewUsers ()
            {
            var result = await _BeService.Read_NewUsersAsync ();
            return Ok (result);
            }
        [HttpPost ("Update_User")]
        public async Task<ActionResult<bool>> Update_Useer ([FromBody] User user)
            {
            var result = await _BeService.Update_UserAsync (user);
            return Ok (result);
            }
        [HttpPost ("Delete_User")]
        public async Task<ActionResult<bool>> Delete_User ([FromBody] int userId)
            {
            var result = await _BeService.Delete_UserAsync (userId);
            return Ok (result);
            }
        #endregion
        #region COUNTRY
        [HttpPost ("Create_Country")]
        public async Task<ActionResult<bool>> Create_Country ([FromBody] Country country)
            {
            var result = await _BeService.Create_CountryAsync (country);
            return Ok (result);
            }
        [HttpPost ("Read_Countries")]
        public async Task<ActionResult<List<Country>>> Read_Country ()
            {
            var result = await _BeService.Read_CountriesAsync ();
            return Ok (result);
            }
        [HttpPost ("Update_Country")]
        public async Task<ActionResult<bool>> Update_Country ([FromBody] Country country)
            {
            var result = await _BeService.Update_CountryAsync (country);
            return Ok (result);
            }
        [HttpPost ("Delete_Country")]
        public async Task<ActionResult<bool>> Delete_Country ([FromBody] int countryId)
            {
            var result = await _BeService.Delete_CountryAsync (countryId);
            return Ok (result);
            }
        #endregion
        #region PROVINCE
        [HttpPost ("Create_Province")]
        public async Task<ActionResult<bool>> Create_Province ([FromBody] Province province)
            {
            var result = await _BeService.Create_ProvinceAsync (province);
            return Ok (result);
            }
        [HttpPost ("Read_Provinces")]
        public async Task<ActionResult<List<Province>>> Read_Provinces ([FromBody] int countryId)
            {
            var result = await _BeService.Read_ProvincesAsync (countryId);
            return Ok (result);
            }
        [HttpPost ("Update_Province")]
        public async Task<ActionResult<bool>> Update_Province ([FromBody] Province province)
            {
            var result = await _BeService.Update_ProvinceAsync (province);
            return Ok (result);
            }
        [HttpPost ("Delete_Province")]
        public async Task<ActionResult<bool>> Delete_Province ([FromBody] int provinceId)
            {
            var result = await _BeService.Delete_ProvinceAsync (provinceId);
            return Ok (result);
            }
        #endregion
        #region TAXONOMY-ApgGroup
        [HttpPost ("Read_ApgGroups")]
        public async Task<ActionResult<List<ApgGroup>>> Read_ApgGroups ()
            {
            var result = await _BeService.Read_ApgGroupsAsync ();
            return Ok (result);
            }
        #endregion
        #region TAXONOMY-ORDER
        [HttpPost ("Create_Order")]
        public async Task<ActionResult<bool>> Create_Order ([FromBody] Order order)
            {
            var result = await _BeService.Create_OrderAsync (order);
            return Ok (result);
            }
        [HttpPost ("Read_Orders")]
        public async Task<ActionResult<List<Order>>> Read_Orders ([FromBody] int apgGroupId)
            {
            var result = await _BeService.Read_OrdersAsync (apgGroupId);
            return Ok (result);
            }
        [HttpPost ("Read_OrdersBySearch")]
        public async Task<ActionResult<List<Order>>> Read_OrdersBySearch ([FromBody] string orderKeyword)
            {
            var result = await _BeService.Read_OrdersBySearchAsync (orderKeyword);
            return Ok (result);
            }
        [HttpPost ("Update_Order")]
        public async Task<ActionResult<bool>> Update_Order ([FromBody] Order order)
            {
            var result = await _BeService.Update_OrderAsync (order);
            return Ok (result);
            }
        [HttpPost ("Delete_Order")]
        public async Task<ActionResult<bool>> Delete_Order ([FromBody] int orderId)
            {
            var result = await _BeService.Delete_OrderAsync (orderId);
            return Ok (result);
            }
        #endregion
        #region TAXONOMY-FAMILY
        [HttpPost ("Create_Family")]
        public async Task<ActionResult<bool>> Create_Family ([FromBody] Family family)
            {
            var result = await _BeService.Create_FamilyAsync (family);
            return Ok (result);
            }
        [HttpPost ("Read_Families")]
        public async Task<ActionResult<List<Family>>> Read_Families ([FromBody] int orderId)
            {
            var result = await _BeService.Read_FamiliesAsync (orderId);
            return Ok (result);
            }
        [HttpPost ("Read_FamiliesBySearch")]
        public async Task<ActionResult<List<Family>>> Read_FamiliesBySearch ([FromBody] string familyKeyword)
            {
            var result = await _BeService.Read_FamiliesBySearchAsync (familyKeyword);
            return Ok (result);
            }
        [HttpPost ("Update_Family")]
        public async Task<ActionResult<bool>> Update_Family ([FromBody] Family family)
            {
            var result = await _BeService.Update_FamilyAsync (family);
            return Ok (result);
            }
        [HttpPost ("Delete_Family")]
        public async Task<ActionResult<bool>> Delete_Family ([FromBody] int familyId)
            {
            var result = await _BeService.Delete_FamilyAsync (familyId);
            return Ok (result);
            }
        #endregion
        #region TAXONOMY-GENUS
        [HttpPost ("Create_Genus")]
        public async Task<ActionResult<bool>> Create_Genus ([FromBody] Genus genus)
            {
            var result = await _BeService.Create_GenusAsync (genus);
            return Ok (result);
            }
        [HttpPost ("Read_Genera")]
        public async Task<ActionResult<List<Genus>>> Read_Genera ([FromBody] int familyId)
            {
            var result = await _BeService.Read_GeneraAsync (familyId);
            return Ok (result);
            }
        [HttpPost ("Read_GeneraBySearch")]
        public async Task<ActionResult<List<Genus>>> Read_GeneraBySearch ([FromBody] string genusKeyword)
            {
            var result = await _BeService.Read_GeneraBySearchAsync (genusKeyword);
            return Ok (result);
            }
        [HttpPost ("Update_Genus")]
        public async Task<ActionResult<bool>> Update_Genus ([FromBody] Genus genus)
            {
            var result = await _BeService.Update_GenusAsync (genus);
            return Ok (result);
            }
        [HttpPost ("Delete_Genus")]
        public async Task<ActionResult<bool>> Delete_Genus ([FromBody] int genusId)
            {
            var result = await _BeService.Delete_GenusAsync (genusId);
            return Ok (result);
            }
        #endregion
        #region TAXONOMY-SPECIES
        [HttpPost ("Create_Species")]
        public async Task<ActionResult<bool>> Create_Species ([FromBody] Species species)
            {
            var result = await _BeService.Create_SpeciesAsync (species);
            return Ok (result);
            }
        [HttpPost ("Read_Species")]
        public async Task<ActionResult<List<Species>>> Read_Species ([FromBody] int genusId)
            {
            var result = await _BeService.Read_SpeciesAsync (genusId);
            return Ok (result);
            }
        [HttpPost ("Read_Subspecies")]
        public async Task<ActionResult<List<Species>>> Read_Subspecies ([FromBody] string genusName, [FromQuery] string epithet)
            {
            var result = await _BeService.Read_SubspeciesAsync (genusName, epithet);
            return Ok (result);
            }
        [HttpPost ("Read_SpeciesByDatasetId")]
        public async Task<ActionResult<List<Species>>> Read_SpeciesByDatasetId ([FromBody] int datasetId)
            {
            var result = await _BeService.Read_SpeciesByDatasetIdAsync (datasetId);
            return Ok (result);
            }
        [HttpPost ("Read_SpeciesById")]
        public async Task<ActionResult<Species>> Read_SpeciesById ([FromBody] int speciesId)
            {
            var result = await _BeService.Read_SpeciesByIdAsync (speciesId);
            return Ok (result);
            }
        [HttpPost ("Read_SpeciesBySearch")]
        public async Task<ActionResult<List<Species>>> Read_SpeciesBySearch ([FromBody] string genusKeyword, [FromQuery] string speciesKeyword)
            {
            var result = await _BeService.Read_SpeciesBySearchAsync (genusKeyword, speciesKeyword);
            return Ok (result);
            }
        [HttpPost ("Update_Species")]
        public async Task<ActionResult<bool>> Update_Species ([FromBody] Species species)
            {
            var result = await _BeService.Update_SpeciesAsync (species);
            return Ok (result);
            }
        [HttpPost ("Delete_Species")]
        public async Task<ActionResult<bool>> Delete_Species ([FromBody] int speciesId)
            {
            var result = await _BeService.Delete_SpeciesAsync (speciesId);
            return Ok (result);
            }

        #endregion
        #region DATASET
        [HttpPost ("Create_Dataset")]
        public async Task<ActionResult<bool>> Create_Dataset ([FromBody] Dataset dataset)
            {
            var result = await _BeService.Create_DatasetAsync (dataset);
            return Ok (result);
            }
        [HttpPost ("Read_Datasets")]
        public async Task<ActionResult<List<Dataset>>> Read_Datasets ([FromBody] int userId, [FromQuery] int datasetType)
            {
            var result = await _BeService.Read_DatasetsAsync (userId, datasetType);
            return Ok (result);
            }
        [HttpPost ("Read_DatasetsBySetId")]
        public async Task<ActionResult<List<Dataset>>> Read_DatasetsBySetId ([FromBody] int setId)
            {
            var result = await _BeService.Read_DatasetsBySetIdAsync (setId);
            return Ok (result);
            }
        [HttpPost ("Read_DatasetsBySearch")]
        public async Task<ActionResult<List<Dataset>>> Read_DatasetsBySearch ([FromQuery] int userId, [FromQuery] int datasetType, [FromBody] string datasetKeyword)
            {
            var result = await _BeService.Read_DatasetsBySearchAsync (userId, datasetType, datasetKeyword);
            return Ok (result);
            }
        [HttpPost ("Read_Dataset")]
        public async Task<ActionResult<Dataset>> Read_Dataset ([FromBody] int datasetId)
            {
            var result = await _BeService.Read_DatasetAsync (datasetId);
            return Ok (result);
            }
        [HttpPost ("Update_Dataset")]
        public async Task<ActionResult<bool>> Update_Dataset ([FromBody] Dataset dataset)
            {
            var result = await _BeService.Update_DatasetAsync (dataset);
            return Ok (result);
            }
        [HttpPost ("Delete_Dataset")]
        public async Task<ActionResult<bool>> Delete_Dataset ([FromBody] int datasetId)
            {
            var result = await _BeService.Delete_DatasetAsync (datasetId);
            return Ok (result);
            }
        #endregion
        #region DATASETDATA
        [HttpPost ("Create_DatasetData")]
        public async Task<ActionResult<bool>> Create_DatasetData ([FromBody] DatasetData datasetData)
            {
            var result = await _BeService.Create_DatasetDataAsync (datasetData);
            return Ok (result);
            }
        [HttpPost ("Read_DatasetData")]
        public async Task<ActionResult<List<DatasetData>>> Read_DatasetData ([FromBody] int datasetId)
            {
            var result = await _BeService.Read_DatasetDataAsync (datasetId);
            return Ok (result);
            }
        [HttpPost ("Update_DatasetData")]
        public async Task<ActionResult<bool>> Update_DatasetData ([FromBody] DatasetData datasetData)
            {
            var result = await _BeService.Update_DatasetDataAsync (datasetData);
            return Ok (result);
            }
        [HttpPost ("Delete_DatasetData")]
        public async Task<ActionResult<bool>> Delete_DatasetData ([FromBody] int datasetDataId)
            {
            var result = await _BeService.Delete_DatasetDataAsync (datasetDataId);
            return Ok (result);
            }
        #endregion
        #region STATION
        [HttpPost ("Create_Station")]
        public async Task<ActionResult<bool>> Create_Station ([FromBody] Station station)
            {
            var result = await _BeService.Create_StationAsync (station);
            return Ok (result);
            }
        [HttpPost ("Read_Stations")]
        public async Task<ActionResult<List<Station>>> Read_Station ()
            {
            var result = await _BeService.Read_StationsAsync ();
            return Ok (result);
            }
        [HttpPost ("Update_Station")]
        public async Task<ActionResult<bool>> Update_Station ([FromBody] Station Station)
            {
            var result = await _BeService.Update_StationAsync (Station);
            return Ok (result);
            }
        [HttpPost ("Delete_Station")]
        public async Task<ActionResult<bool>> Delete_Station ([FromBody] int stationId)
            {
            var result = await _BeService.Delete_StationAsync (stationId);
            return Ok (result);
            }
        #endregion
        #region STATIONDATA
        [HttpPost ("Create_StationData")]
        public async Task<ActionResult<bool>> Create_StationData ([FromBody] StationData stationData)
            {
            var result = await _BeService.Create_StationDataAsync (stationData);
            return Ok (result);
            }
        [HttpPost ("Read_StationData")]
        public async Task<ActionResult<List<StationData>>> Read_StationData ([FromBody] int datasetId)
            {
            var result = await _BeService.Read_StationDataAsync (datasetId);
            return Ok (result);
            }
        [HttpPost ("Update_StationData")]
        public async Task<ActionResult<bool>> Update_StationData ([FromBody] StationData stationData)
            {
            var result = await _BeService.Update_StationDataAsync (stationData);
            return Ok (result);
            }
        [HttpPost ("Delete_StationData")]
        public async Task<ActionResult<bool>> Delete_StationData ([FromBody] int stationDataId)
            {
            var result = await _BeService.Delete_StationDataAsync (stationDataId);
            return Ok (result);
            }
        #endregion
        #region SET
        [HttpPost ("Create_Set")]
        public async Task<ActionResult<bool>> Create_Set ([FromBody] Set set)
            {
            var result = await _BeService.Create_SetAsync (set);
            return Ok (result);
            }
        [HttpPost ("Read_Sets")]
        public async Task<ActionResult<List<Set>>> Read_Sets ([FromBody] int userId)
            {
            var result = await _BeService.Read_SetsAsync (userId);
            return Ok (result);
            }
        [HttpPost ("Read_Set")]
        public async Task<ActionResult<Set>> Read_Set ([FromBody] int setId)
            {
            var result = await _BeService.Read_SetAsync (setId);
            return Ok (result);
            }
        [HttpPost ("Update_Set")]
        public async Task<ActionResult<bool>> Update_Set ([FromBody] Set set)
            {
            var result = await _BeService.Update_SetAsync (set);
            return Ok (result);
            }
        [HttpPost ("Delete_Set")]
        public async Task<ActionResult<bool>> Delete_Set ([FromBody] int setId)
            {
            var result = await _BeService.Delete_SetAsync (setId);
            return Ok (result);
            }
        #endregion
        #region SETDATA
        [HttpPost ("Create_SetData")]
        public async Task<ActionResult<bool>> Create_SetData ([FromBody] SetData setData)
            {
            var result = await _BeService.Create_SetDataAsync (setData);
            return Ok (result);
            }
        [HttpPost ("Read_SetData")]
        public async Task<ActionResult<SetData>> Read_SetData ([FromBody] int setId)
            {
            var result = await _BeService.Read_SetDataAsync (setId);
            return Ok (result);
            }
        [HttpPost ("Update_SetData")]
        public async Task<ActionResult<bool>> Update_SetData ([FromBody] SetData setData)
            {
            var result = await _BeService.Update_SetDataAsync (setData);
            return Ok (result);
            }
        [HttpPost ("Delete_SetData")]
        public async Task<ActionResult<bool>> Delete_SetData ([FromBody] int setDataId)
            {
            var result = await _BeService.Delete_SetDataAsync (setDataId);
            return Ok (result);
            }
        #endregion
        #region NOTEBOOK
        [HttpPost ("Create_Notebook")]
        public async Task<ActionResult<bool>> Create_Notebook ([FromBody] Notebook notebook)
            {
            var result = await _BeService.Create_NotebookAsync (notebook);
            return Ok (result);
            }
        [HttpPost ("Read_Notebooks")]
        public async Task<ActionResult<List<Notebook>>> Read_Notebooks ([FromBody] int userId)
            {
            var result = await _BeService.Read_NotebooksAsync (userId);
            return Ok (result);
            }
        [HttpPost ("Read_Notebook")]
        public async Task<ActionResult<Notebook>> Read_Notebook ([FromBody] int notebookId)
            {
            var result = await _BeService.Read_NotebookAsync (notebookId);
            return Ok (result);
            }
        [HttpPost ("Update_Notebook")]
        public async Task<ActionResult<bool>> Update_Notebook ([FromBody] Notebook notebook)
            {
            var result = await _BeService.Update_NotebookAsync (notebook);
            return Ok (result);
            }
        [HttpPost ("Delete_Notebook")]
        public async Task<ActionResult<bool>> Delete_Notebook ([FromBody] int notebookId)
            {
            var result = await _BeService.Delete_NotebookAsync (notebookId);
            return Ok (result);
            }
        #endregion
        #region SECTION
        [HttpPost ("Create_Section")]
        public async Task<ActionResult<bool>> Create_Section ([FromBody] Section section)
            {
            var result = await _BeService.Create_SectionAsync (section);
            return Ok (result);
            }
        [HttpPost ("Read_Sections")]
        public async Task<ActionResult<List<Section>>> Read_Sections ([FromBody] int notebookId, [FromQuery] bool readNotes)
            {
            var result = await _BeService.Read_SectionsAsync (notebookId, readNotes);
            return Ok (result);
            }
        [HttpPost ("Read_Section")]
        public async Task<ActionResult<Section>> Read_Section ([FromBody] int SectionId, [FromQuery] bool readNotes)
            {
            var result = await _BeService.Read_SectionAsync (SectionId, readNotes);
            return Ok (result);
            }
        [HttpPost ("Update_Section")]
        public async Task<ActionResult<bool>> Update_Section ([FromBody] Section section)
            {
            var result = await _BeService.Update_SectionAsync (section);
            return Ok (result);
            }
        [HttpPost ("Update_SectionParent")]
        public async Task<ActionResult<bool>> Update_SectionPaernt ([FromBody] Section section)
            {
            var result = await _BeService.Update_SectionParentAsync (section);
            return Ok (result);
            }
        [HttpPost ("Delete_Section")]
        public async Task<ActionResult<bool>> Delete_Section ([FromBody] int sectionId)
            {
            var result = await _BeService.Delete_SectionAsync (sectionId);
            return Ok (result);
            }
        #endregion
        #region NOTE
        [HttpPost ("Create_Note")]
        public async Task<ActionResult<bool>> Create_Note ([FromBody] Note note)
            {
            var result = await _BeService.Create_NoteAsync (note);
            return Ok (result);
            }
        [HttpPost ("Read_Notes")]
        public async Task<ActionResult<List<Note>>> Read_Notes ([FromBody] int refId, [FromQuery] int refType)
            {
            var result = await _BeService.Read_NotesAsync (refType, refId);
            return Ok (result);
            }
        [HttpPost ("Read_NotesDue")]
        public async Task<ActionResult<List<Note>>> Read_NotesDue ([FromBody] int userId, [FromQuery] int nDays)
            {
            var result = await _BeService.Read_NotesDueAsync (userId, nDays);
            return Ok (result);
            }
        [HttpPost ("Read_Note")]
        public async Task<ActionResult<Note>> Read_Note ([FromBody] int noteId)
            {
            var result = await _BeService.Read_NoteAsync (noteId);
            return Ok (result);
            }
        [HttpPost ("Update_Note")]
        public async Task<ActionResult<bool>> Update_Note ([FromBody] Note note)
            {
            var result = await _BeService.Update_NoteAsync (note);
            return Ok (result);
            }
        [HttpPost ("Delete_Notes")]
        public async Task<ActionResult<bool>> Delete_Notes ([FromQuery] int refType, [FromBody] int refId)
            {
            var result = await _BeService.Delete_NotesAsync (refType, refId);
            return Ok (result);
            }
        [HttpPost ("Delete_Note")]
        public async Task<ActionResult<bool>> Delete_Note ([FromBody] int noteId)
            {
            var result = await _BeService.Delete_NoteAsync (noteId);
            return Ok (result);
            }
        #endregion
        #region ANALYSIS
        [HttpPost ("Calc_Diversity")]
        public async Task<ActionResult<List<Diversity>>> Calc_Diversity ([FromBody] int setId, [FromQuery] int subsetIndex)
            {
            var result = await _BeService.Calc_DiversityAsync (setId, subsetIndex);
            return Ok (result);
            }
        #endregion
        #region REPORT
        #endregion
        #region MAP
        #endregion
        #region METEO
        #endregion
        #region CLIMAGRAPH
        #endregion





        }
    }
