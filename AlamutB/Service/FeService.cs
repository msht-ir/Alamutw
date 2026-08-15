using AlamutS.Models;
using System.Data;
using System.Globalization;
namespace AlamutB.Service
    {
    public class FeService
        {
        private readonly HttpClient _http;
        public FeService (HttpClient http)
            {
            _http = http;
            }
        #region USER
        public async Task<bool> Create_User (User user)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_User", user);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<User>> Read_Users ()
            {
            var response = await _http.PostAsync ("api/Read_Users", null);
            if (response.IsSuccessStatusCode)
                {
                List<User> lstUsers = await response.Content.ReadFromJsonAsync<List<User>> ();
                return lstUsers;
                }
            else
                {
                return new List<User> ();
                }
            }
        public async Task<User?> Read_User (string username, string password)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_User?username={username}", password);
            if (response.IsSuccessStatusCode)
                {
                return await response.Content.ReadFromJsonAsync<User> ();
                }
            else
                {
                return new User ();
                }
            }
        public async Task<List<User>> Read_NewUsers ()
            {
            var response = await _http.PostAsync ("api/Read_NewUsers", null);
            if (response.IsSuccessStatusCode)
                {
                List<User> lstNewUsers = await response.Content.ReadFromJsonAsync<List<User>> ();
                return lstNewUsers;
                }
            else
                {
                return new List<User> ();
                }
            }
        public async Task<bool> Update_User (User user)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_User", user);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_User (int userId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_User", userId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region COUNTRY
        public async Task<bool> Create_Country (Country country)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_Country", country);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<Country>> Read_Countries ()
            {
            var response = await _http.PostAsync ("api/Read_Countries", null);
            if (response.IsSuccessStatusCode)
                {
                List<Country> lstCountries = await response.Content.ReadFromJsonAsync<List<Country>> ();
                return lstCountries;
                }
            else
                {
                return new List<Country> ();
                }
            }
        public async Task<bool> Update_Country (Country country)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_Country", country);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_Country (int countryId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_Country", countryId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region PROVINCE
        public async Task<bool> Create_Province (Province province)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_Province", province);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<Province>> Read_Provinces (int countryId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_Provinces", countryId);
            if (response.IsSuccessStatusCode)
                {
                List<Province> lstProvinces = await response.Content.ReadFromJsonAsync<List<Province>> ();
                return lstProvinces;
                }
            else
                {
                return new List<Province> ();
                }
            }
        public async Task<bool> Update_Province (Province province)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_Province", province);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_Province (int provinceId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_Province", provinceId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region TAXONOMY-ApgGroup
        public async Task<List<ApgGroup>> Read_ApgGroups ()
            {
            var response = await _http.PostAsync ("api/Read_ApgGroups", null);
            if (response.IsSuccessStatusCode)
                {
                List<ApgGroup> lstApgGroups = await response.Content.ReadFromJsonAsync<List<ApgGroup>> ();
                return lstApgGroups;
                }
            else
                {
                return new List<ApgGroup> ();
                }
            }
        #endregion
        #region TAXONOMY-ORDER
        public async Task<bool> Create_Order (Order order)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_Order", order);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<Order>> Read_Orders (int apgGroupId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_Orders", apgGroupId);
            if (response.IsSuccessStatusCode)
                {
                List<Order> lstOrders = await response.Content.ReadFromJsonAsync<List<Order>> ();
                return lstOrders;
                }
            else
                {
                return new List<Order> ();
                }
            }
        public async Task<List<Order>> Read_OrdersBySearch (string orderKeyword)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_OrdersBySearch", orderKeyword);
            if (response.IsSuccessStatusCode)
                {
                List<Order> lstOrders = await response.Content.ReadFromJsonAsync<List<Order>> ();
                return lstOrders;
                }
            else
                {
                return new List<Order> ();
                }
            }
        public async Task<bool> Update_Order (Order order)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_Order", order);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_Order (int orderId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_Order", orderId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region TAXONOMY-FAMILY
        public async Task<bool> Create_Family (Family family)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_Family", family);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<Family>> Read_Families ( int orderId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_Families", orderId);
            if (response.IsSuccessStatusCode)
                {
                List<Family> lstFamilies = await response.Content.ReadFromJsonAsync<List<Family>> ();
                return lstFamilies;
                }
            else
                {
                return new List<Family> ();
                }
            }
        public async Task<List<Family>> Read_FamiliesBySearch (string familyKeyword)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_FamiliesBySearch", familyKeyword);
            if (response.IsSuccessStatusCode)
                {
                List<Family> lstFamilies = await response.Content.ReadFromJsonAsync<List<Family>> ();
                return lstFamilies;
                }
            else
                {
                return new List<Family> ();
                }
            }
        public async Task<bool> Update_Family (Family Family)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_Family", Family);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_Family (int FamilyId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_Family", FamilyId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region TAXONOMY-GENUS
        public async Task<bool> Create_Genus (Genus genus)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_Genus", genus);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<Genus>> Read_Genera (int familyId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_Genera", familyId);
            if (response.IsSuccessStatusCode)
                {
                List<Genus> lstGenera = await response.Content.ReadFromJsonAsync<List<Genus>> ();
                return lstGenera;
                }
            else
                {
                return new List<Genus> ();
                }
            }
        public async Task<List<Genus>> Read_GeneraBySearch (string genusKeyword)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_GeneraBySearch", genusKeyword);
            if (response.IsSuccessStatusCode)
                {
                List<Genus> lstGenera = await response.Content.ReadFromJsonAsync<List<Genus>> ();
                return lstGenera;
                }
            else
                {
                return new List<Genus> ();
                }
            }
        public async Task<bool> Update_Genus (Genus genus)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_Genus", genus);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_Genus (int genusId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_Genus", genusId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region TAXONOMY-SPECIES
        public async Task<bool> Create_Species (Species species)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_Species", species);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<Species>> Read_Species (int genusId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_Species", genusId);
            if (response.IsSuccessStatusCode)
                {
                List<Species> lstSpecies = await response.Content.ReadFromJsonAsync<List<Species>> ();
                return lstSpecies;
                }
            else
                {
                return new List<Species> ();
                }
            }
        public async Task<List<Species>> Read_Subspecies (string genusName , string epithet)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_Subspecies?epithet={epithet}", genusName);
            if (response.IsSuccessStatusCode)
                {
                List<Species> lstSpecies = await response.Content.ReadFromJsonAsync<List<Species>> ();
                return lstSpecies;
                }
            else
                {
                return new List<Species> ();
                }
            }
        public async Task<List<Species>> Read_SpeciesByDatasetLMId (int datasetLMId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_SpeciesByDatasetLMId", datasetLMId);
            if (response.IsSuccessStatusCode)
                {
                List<Species> lstSpecies = await response.Content.ReadFromJsonAsync<List<Species>> ();
                return lstSpecies;
                }
            else
                {
                return new List<Species> ();
                }
            }
        public async Task<List<Species>> Read_SpeciesById (int speciesId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_SpeciesById", speciesId);
            if (response.IsSuccessStatusCode)
                {
                List<Species> lstSpecies = await response.Content.ReadFromJsonAsync<List<Species>> ();
                return lstSpecies;
                }
            else
                {
                return new List<Species> ();
                }
            }
        public async Task<bool> Update_Species (Species species)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_Species", species);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_Species (int speciesId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_Species", speciesId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region DATASET
        public async Task<bool> Create_Dataset (Dataset dataset)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_Dataset", dataset);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<Dataset>> Read_Datasets (int userId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_Datasets", userId);
            if (response.IsSuccessStatusCode)
                {
                List<Dataset> lstDatasets = await response.Content.ReadFromJsonAsync<List<Dataset>> ();
                return lstDatasets;
                }
            else
                {
                return new List<Dataset> ();
                }
            }
        public async Task<bool> Update_Dataset (Dataset dataset)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_Dataset", dataset);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_Dataset (int datasetId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_Dataset", datasetId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region DATASETDATA-FL
        public async Task<bool> Create_DatasetDataFL (DatasetDataFL datasetDataFL)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_DatasetDataFL", datasetDataFL);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<DatasetDataFL>> Read_DatasetDataFL (int datasetId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_DatasetDataFL", datasetId);
            if (response.IsSuccessStatusCode)
                {
                List<DatasetDataFL> lstDatasetDataFL = await response.Content.ReadFromJsonAsync<List<DatasetDataFL>> ();
                return lstDatasetDataFL;
                }
            else
                {
                return new List<DatasetDataFL> ();
                }
            }
        public async Task<bool> Update_DatasetDataFL (DatasetDataFL datasetDataFL)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_DatasetDataFL", datasetDataFL);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_DatasetDataFL (int datasetDataFLId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_DatasetDataFL", datasetDataFLId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region DATASETDATA-CN
        public async Task<bool> Create_DatasetDataCN (DatasetDataCN datasetDataCN)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_DatasetDataCN", datasetDataCN);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<DatasetDataCN>> Read_DatasetDataCN (int datasetId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_DatasetDataCN", datasetId);
            if (response.IsSuccessStatusCode)
                {
                List<DatasetDataCN> lstDatasetDataCN = await response.Content.ReadFromJsonAsync<List<DatasetDataCN>> ();
                return lstDatasetDataCN;
                }
            else
                {
                return new List<DatasetDataCN> ();
                }
            }
        public async Task<bool> Update_DatasetDataCN (DatasetDataCN datasetDataCN)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_DatasetDataCN", datasetDataCN);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_DatasetDataCN (int datasetDataCNId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_DatasetDataCN", datasetDataCNId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region DATASETDATA-LM
        public async Task<bool> Create_DatasetDataLM (DatasetDataLM datasetDataLM)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_DatasetDataLM", datasetDataLM);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<DatasetDataLM>> Read_DatasetDataLM (int datasetId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_DatasetDataLM", datasetId);
            if (response.IsSuccessStatusCode)
                {
                List<DatasetDataLM> lstDatasetDataLM = await response.Content.ReadFromJsonAsync<List<DatasetDataLM>> ();
                return lstDatasetDataLM;
                }
            else
                {
                return new List<DatasetDataLM> ();
                }
            }
        public async Task<bool> Update_DatasetDataLM (DatasetDataLM datasetDataLM)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_DatasetDataLM", datasetDataLM);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_DatasetDataLM (int datasetDataLMId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_DatasetDataLM", datasetDataLMId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region STATION
        public async Task<bool> Create_Station (Station station)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_Station", station);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<Station>> Read_Stations ()
            {
            var response = await _http.PostAsync ("api/Read_Stations", null);
            if (response.IsSuccessStatusCode)
                {
                List<Station> lstStations = await response.Content.ReadFromJsonAsync<List<Station>> ();
                return lstStations;
                }
            else
                {
                return new List<Station> ();
                }
            }
        public async Task<bool> Update_Station (Station station)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_Station", station);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_Station (int stationId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_Station", stationId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region DATASETDATA-ST
        public async Task<bool> Create_DatasetDataST (DatasetDataST datasetDataST)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_DatasetDataST", datasetDataST);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<DatasetDataST>> Read_DatasetDataST (int stationId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_DatasetDataST", stationId);
            if (response.IsSuccessStatusCode)
                {
                List<DatasetDataST> lstDatasetDataST = await response.Content.ReadFromJsonAsync<List<DatasetDataST>> ();
                return lstDatasetDataST;
                }
            else
                {
                return new List<DatasetDataST> ();
                }
            }
        public async Task<bool> Update_DatasetDataST (DatasetDataST datasetDataST)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_DatasetDataST", datasetDataST);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_DatasetDataST (int datasetDataSTId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_DatasetDataST", datasetDataSTId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region SET
        public async Task<bool> Create_Set (Set set)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_Set", set);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<Set>> Read_Sets (int userId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_Sets", userId);
            if (response.IsSuccessStatusCode)
                {
                List<Set> lstSets = await response.Content.ReadFromJsonAsync<List<Set>> ();
                return lstSets;
                }
            else
                {
                return new List<Set> ();
                }
            }
        public async Task<bool> Update_Set (Set set)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_Set", set);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_Set (int setId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_Set", setId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region SETDATA
        public async Task<bool> Create_SetData (SetData setData)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_SetData", setData);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<SetData>> Read_SetDatas (int setId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_SetDatas", setId);
            if (response.IsSuccessStatusCode)
                {
                List<SetData> lstSetDatas = await response.Content.ReadFromJsonAsync<List<SetData>> ();
                return lstSetDatas;
                }
            else
                {
                return new List<SetData> ();
                }
            }
        public async Task<bool> Update_SetData (SetData setData)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_SetData", setData);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_SetData (int setDataId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_SetData", setDataId);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        #endregion
        #region NOTE
        public async Task<bool> Create_Note (Note note)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_Note", note);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<Note>> Read_Notes (int userId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_Notes", userId);
            if (response.IsSuccessStatusCode)
                {
                List<Note> lstNotes = await response.Content.ReadFromJsonAsync<List<Note>> ();
                return lstNotes;
                }
            else
                {
                return new List<Note> ();
                }
            }
        public async Task<List<Note>> Read_DueNotes (int userId, int _days)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_DueNotes?days={_days}", userId);
            if (response.IsSuccessStatusCode)
                {
                List<Note> lstNotes = await response.Content.ReadFromJsonAsync<List<Note>> ();
                return lstNotes;
                }
            else
                {
                return new List<Note> ();
                }
            }
        public async Task<Note> Read_Note (int noteId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_Note", noteId);
            if (response.IsSuccessStatusCode)
                {
                Note note = await response.Content.ReadFromJsonAsync<Note> ();
                return note;
                }
            else
                {
                return new Note ();
                }
            }
        public async Task<bool> Update_Note (Note note)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_Note", note);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_Note (int noteId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_Note", noteId);
            return (response.IsSuccessStatusCode) ? true : false;
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
        #region FE Service
        public string ToPersianDate (string? dt)
            {
            //this method converts miladi to shamsi just for display; actual data on db is miladi-date
            //handle null/empty
            if (string.IsNullOrWhiteSpace (dt))
                return "-";
            //TryParse: prevents error on invalid date formats
            if (!DateTime.TryParse (dt, out DateTime datetime))
                {
                return "-";
                }
            //convert
            PersianCalendar pc = new PersianCalendar ();
            return $"{pc.GetYear (datetime):0000}-" +
                       $"{pc.GetMonth (datetime):00}-" +
                       $"{pc.GetDayOfMonth (datetime):00} " +
                       $"{pc.GetHour (datetime):00}:{pc.GetMinute (datetime):00}";
            }
        #endregion

        }
    }
