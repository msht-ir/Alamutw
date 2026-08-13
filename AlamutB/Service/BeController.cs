using AlamutB.Services2Backend;
using Microsoft.AspNetCore.Mvc;
using AlamutS.Models;
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
        [HttpPost ("Read_Country")]
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
            var result = await _BeService.Read_FamiliesAsync (familyId);
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
        [HttpPost ("Read_SpeciesById")]
        public async Task<ActionResult<Species>> Read_SpeciesById ([FromBody] int speciesId)
            {
            var result = await _BeService.Read_SpeciesByIdAsync (speciesId);
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
        public async Task<ActionResult<List<Dataset>>> Read_Datasets ([FromBody] int userId)
            {
            var result = await _BeService.Read_DatasetsAsync (userId);
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
        #region DATASETDATA-FL
        [HttpPost ("Create_DatasetDataFL")]
        public async Task<ActionResult<bool>> Create_DatasetDataFL ([FromBody] DatasetDataFL datasetDataFL)
            {
            var result = await _BeService.Create_DatasetDataFLAsync (datasetDataFL);
            return Ok (result);
            }
        [HttpPost ("Read_DatasetDataFL")]
        public async Task<ActionResult<List<DatasetDataFL>>> Read_DatasetDataFL ([FromBody] int datasetId)
            {
            var result = await _BeService.Read_DatasetDataFLAsync (datasetId);
            return Ok (result);
            }
        [HttpPost ("Update_DatasetDataFL")]
        public async Task<ActionResult<bool>> Update_DatasetDataFL ([FromBody] DatasetDataFL datasetDataFL)
            {
            var result = await _BeService.Update_DatasetDataFLAsync (datasetDataFL);
            return Ok (result);
            }
        [HttpPost ("Delete_DatasetDataFL")]
        public async Task<ActionResult<bool>> Delete_DatasetDataFL ([FromBody] int datasetDataFLId)
            {
            var result = await _BeService.Delete_DatasetDataFLAsync (datasetDataFLId);
            return Ok (result);
            }
        #endregion
        #region DATASETDATA-CN
        [HttpPost ("Create_DatasetDataCN")]
        public async Task<ActionResult<bool>> Create_DatasetDataCN ([FromBody] DatasetDataCN datasetDataCN)
            {
            var result = await _BeService.Create_DatasetDataCNAsync (datasetDataCN);
            return Ok (result);
            }
        [HttpPost ("Read_DatasetDataCNs")]
        public async Task<ActionResult<List<DatasetDataCN>>> Read_DatasetDataCN ([FromBody] int datasetId)
            {
            var result = await _BeService.Read_DatasetDataCNAsync (datasetId);
            return Ok (result);
            }
        [HttpPost ("Update_DatasetDataCN")]
        public async Task<ActionResult<bool>> Update_DatasetDataCN ([FromBody] DatasetDataCN datasetDataCN)
            {
            var result = await _BeService.Update_DatasetDataCNAsync (datasetDataCN);
            return Ok (result);
            }
        [HttpPost ("Delete_DatasetDataCN")]
        public async Task<ActionResult<bool>> Delete_DatasetDataCN ([FromBody] int datasetDataCNId)
            {
            var result = await _BeService.Delete_DatasetDataCNAsync (datasetDataCNId);
            return Ok (result);
            }
        #endregion
        #region DATASETDATA-LM
        [HttpPost ("Create_DatasetDataLM")]
        public async Task<ActionResult<bool>> Create_DatasetDataLM ([FromBody] DatasetDataLM datasetDataLM)
            {
            var result = await _BeService.Create_DatasetDataLMAsync (datasetDataLM);
            return Ok (result);
            }
        [HttpPost ("Read_DatasetDataLMs")]
        public async Task<ActionResult<List<DatasetDataLM>>> Read_DatasetDataLM ([FromBody] int datasetId)
            {
            var result = await _BeService.Read_DatasetDataLMAsync (datasetId);
            return Ok (result);
            }
        [HttpPost ("Update_DatasetDataLM")]
        public async Task<ActionResult<bool>> Update_DatasetDataLM ([FromBody] DatasetDataLM datasetDataLM)
            {
            var result = await _BeService.Update_DatasetDataLMAsync (datasetDataLM);
            return Ok (result);
            }
        [HttpPost ("Delete_DatasetDataLM")]
        public async Task<ActionResult<bool>> Delete_DatasetDataLM ([FromBody] int datasetDataLMId)
            {
            var result = await _BeService.Delete_DatasetDataLMAsync (datasetDataLMId);
            return Ok (result);
            }
        #endregion
        #region STATION
        [HttpPost ("Create_DatasetDataLM")]
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
        #region DATASETDATA-ST
        [HttpPost ("Create_DatasetDataLM")]
        public async Task<ActionResult<bool>> Create_DatasetDataST ([FromBody] DatasetDataST datasetDataST)
            {
            var result = await _BeService.Create_DatasetDataSTAsync (datasetDataST);
            return Ok (result);
            }
        [HttpPost ("Read_DatasetDataSTs")]
        public async Task<ActionResult<List<DatasetDataST>>> Read_DatasetDataST ([FromBody] int datasetId)
            {
            var result = await _BeService.Read_DatasetDataSTAsync (datasetId);
            return Ok (result);
            }
        [HttpPost ("Update_DatasetDataST")]
        public async Task<ActionResult<bool>> Update_DatasetDataST ([FromBody] DatasetDataST datasetDataST)
            {
            var result = await _BeService.Update_DatasetDataSTAsync (datasetDataST);
            return Ok (result);
            }
        [HttpPost ("Delete_DatasetDataST")]
        public async Task<ActionResult<bool>> Delete_DatasetDataST ([FromBody] int datasetDataSTId)
            {
            var result = await _BeService.Delete_DatasetDataSTAsync (datasetDataSTId);
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
        #region NOTE
        [HttpPost ("Create_Note")]
        public async Task<ActionResult<bool>> Create_Note ([FromBody] Note note)
            {
            var result = await _BeService.Create_NoteAsync (note);
            return Ok (result);
            }
        [HttpPost ("Read_Notes")]
        public async Task<ActionResult<List<Note>>> Read_Notes ([FromBody] int userId)
            {
            var result = await _BeService.Read_NotesAsync (userId);
            return Ok (result);
            }
        [HttpPost ("Read_DueNotes")]
        public async Task<ActionResult<List<Note>>> Read_DueNotes ([FromBody] int userId, [FromQuery] int days)
            {
            var result = await _BeService.Read_DueNotesAsync (userId, days);
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
        [HttpPost ("Delete_Note")]
        public async Task<ActionResult<bool>> Delete_Note ([FromBody] int noteId)
            {
            var result = await _BeService.Delete_NoteAsync (noteId);
            return Ok (result);
            }
        #endregion
        #region ANALYSIS
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
