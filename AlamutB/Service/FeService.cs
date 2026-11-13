using AlamutS.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel;
using System.Globalization;
using IContainer = QuestPDF.Infrastructure.IContainer;
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
        public async Task<List<Family>> Read_Families (int orderId)
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
        public async Task<List<Species>> Read_Subspecies (string genusName, string epithet)
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
        public async Task<List<Species>> Read_SpeciesByDatasetId (int datasetId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_SpeciesByDatasetId", datasetId);
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
        public async Task<List<Species>> Read_SpeciesBySearch (string genusKeyword, string speciesKeyword, string searchMode, int id)
            {
            //searchModes: global(id:na)|countryId|setId
            var response = await _http.PostAsJsonAsync ($"api/Read_SpeciesBySearch?speciesKeyword={speciesKeyword}&searchMode={searchMode}&id={id}", genusKeyword);
            if (response.IsSuccessStatusCode)
                {
                List<Species> lstSpecies = await response.Content.ReadFromJsonAsync<List<Species>> ();
                return lstSpecies ?? new List<Species> ();
                }
            else
                {
                return new List<Species> ();
                }
            }
        public async Task<List<Species>> Read_SpeciesByUPS (int ups, string searchMode, int id)
            {
            //searchModes: global(id:na)|countryId|setId
            var response = await _http.PostAsJsonAsync ($"api/Read_SpeciesByUPS?ups={ups}&searchMode={searchMode}", id);
            if (response.IsSuccessStatusCode)
                {
                List<Species> lstSpecies = await response.Content.ReadFromJsonAsync<List<Species>> ();
                return lstSpecies ?? new List<Species> ();
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
        public async Task<List<Dataset>> Read_Datasets (int userId, int datasetType)
            {
            //notice: to get datasets of all users from DB, set userId = 0 (see stored procedure) 
            var response = await _http.PostAsJsonAsync ($"api/Read_Datasets?datasetType={datasetType}", userId);
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
        public async Task<List<Dataset>> Read_DatasetsBySetId (int setId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_DatasetsBySetId", setId);
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
        public async Task<List<Dataset>> Read_DatasetsBySubsetIndex (int setId, int subsetIndex)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_DatasetsBySubsetIndex?subsetIndex={subsetIndex}", setId);
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
        public async Task<List<Dataset>> Read_DatasetsBySearch (int userId, int datasetType, string datasetKeyword)
            {
            //notice: to get datasets of all users from DB, set userId = 0 (see stored procedure) 
            var response = await _http.PostAsJsonAsync ($"api/Read_DatasetsBySearch?userId={userId}&datasetType={datasetType}", datasetKeyword);
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
        public async Task<Dataset> Read_Dataset (int datasetId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_Dataset", datasetId);
            if (response.IsSuccessStatusCode)
                {
                Dataset dataset = await response.Content.ReadFromJsonAsync<Dataset> ();
                return dataset;
                }
            else
                {
                return new Dataset ();
                }
            }
        public async Task<List<Dataset>> Read_DatasetsByCountryId (int countryId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_DatasetsByCountryId", countryId);
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
        public async Task<List<Dataset>> Read_DatasetsByProvinceId (int provinceId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_DatasetsByProvinceId", provinceId);
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
        public async Task<List<Dataset>> Read_DatasetsBySpeciesId (string speciesId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_DatasetsBySpeciesId", speciesId);
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
        #region DATASETDATA
        public async Task<bool> Create_DatasetData (DatasetData datasetData)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_DatasetData", datasetData);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<DatasetData>> Read_DatasetData (int datasetId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_DatasetData", datasetId);
            if (response.IsSuccessStatusCode)
                {
                List<DatasetData> lstDatasetData = await response.Content.ReadFromJsonAsync<List<DatasetData>> ();
                return lstDatasetData;
                }
            else
                {
                return new List<DatasetData> ();
                }
            }
        public async Task<bool> Update_DatasetData (DatasetData datasetData)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_DatasetData", datasetData);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_DatasetData (int datasetDataId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_DatasetData", datasetDataId);
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
        #region STATIONDATA
        public async Task<bool> Create_StationData (StationData stationData)
            {
            var response = await _http.PostAsJsonAsync ("api/Create_StationData", stationData);
            if (response.IsSuccessStatusCode)
                {
                return true;
                }
            else
                {
                return false;
                }
            }
        public async Task<List<StationData>> Read_StationData (int stationId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_StationData", stationId);
            if (response.IsSuccessStatusCode)
                {
                List<StationData> lstStationData = await response.Content.ReadFromJsonAsync<List<StationData>> ();
                return lstStationData;
                }
            else
                {
                return new List<StationData> ();
                }
            }
        public async Task<bool> Update_StationData (StationData stationData)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_StationData", stationData);
            return (response.IsSuccessStatusCode) ? true : false;
            }
        public async Task<bool> Delete_StationData (int stationDataId)
            {
            var response = await _http.PostAsJsonAsync ("api/Delete_StationData", stationDataId);
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
        public async Task<List<SetData>> Read_SetData (int setId)
            {
            var response = await _http.PostAsJsonAsync ("api/Read_SetData", setId);
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
        #region NOTEBOOK
        public async Task<int> Create_Notebook (Notebook notebook)
            {
            var response = await _http.PostAsJsonAsync ($"api/Create_Notebook", notebook);
            return response.IsSuccessStatusCode ? 1 : 0;
            }
        public async Task<List<Notebook>> Read_Notebooks (int userId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_Notebooks", userId);
            if (response.IsSuccessStatusCode)
                {
                List<Notebook>? lstNotebooks = await response.Content.ReadFromJsonAsync<List<Notebook>> ();
                return lstNotebooks ?? new List<Notebook> ();
                }
            else
                {
                return new List<Notebook> ();
                }
            }
        public async Task<Notebook> Read_Notebook (int notebookId, bool readNotes)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_Notebook?readNotes={readNotes}", notebookId);
            if (response.IsSuccessStatusCode)
                {
                Notebook? notebook = await response.Content.ReadFromJsonAsync<Notebook> ();
                return notebook ?? new Notebook ();
                }
            else
                {
                return new Notebook ();
                }
            }
        public async Task<bool> Update_Notebook (Notebook notebook)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_Notebook", notebook);
            return response.IsSuccessStatusCode ? true : false;
            }
        public async Task<bool> Delete_Notebooks (int userId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Delete_Notebooks", userId);
            return response.IsSuccessStatusCode ? true : false;
            }
        public async Task<bool> Delete_Notebook (int notebookId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Delete_Notebook", notebookId);
            return response.IsSuccessStatusCode ? true : false;
            }
        #endregion
        #region SECTION
        public async Task<int> Create_Section (Section section)
            {
            var response = await _http.PostAsJsonAsync ($"api/Create_section", section);
            return response.IsSuccessStatusCode ? 1 : 0;
            }
        public async Task<List<Section>> Read_Sections (int notebookId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_Sections", notebookId);
            if (response.IsSuccessStatusCode)
                {
                List<Section>? lstSections = await response.Content.ReadFromJsonAsync<List<Section>> ();
                return lstSections ?? new List<Section> ();
                }
            else
                {
                return new List<Section> ();
                }
            }
        public async Task<Section> Read_Section (int SectionId, bool readNotes)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_Section?readNotes={readNotes}", SectionId);
            if (response.IsSuccessStatusCode)
                {
                Section? section = await response.Content.ReadFromJsonAsync<Section> ();
                return section ?? new Section ();
                }
            else
                {
                return new Section ();
                }
            }
        public async Task<bool> Update_Section (Section Section)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_Section", Section);
            return response.IsSuccessStatusCode ? true : false;
            }
        public async Task<bool> Update_SectionParent (Section Section)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_SectionParent", Section);
            return response.IsSuccessStatusCode ? true : false;
            }
        //public async Task<bool> Delete_Sections (int notebookId)
        //    {
        //    var response = await _http.PostAsJsonAsync ($"api/Delete_Sections", notebookId);
        //    return response.IsSuccessStatusCode ? true : false;
        //    }
        public async Task<bool> Delete_Section (int SectionId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Delete_Section", SectionId);
            return response.IsSuccessStatusCode ? true : false;
            }
        #endregion
        #region NOTE
        public async Task<int> Create_Note (Note note)
            {
            var response = await _http.PostAsJsonAsync ($"api/Create_Note", note);
            return response.IsSuccessStatusCode ? 1 : 0;
            }
        public async Task<List<Note>> Read_Notes (int refType, int refId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_Notes?refType={refType}", refId);
            if (response.IsSuccessStatusCode)
                {
                List<Note>? lstNotes = await response.Content.ReadFromJsonAsync<List<Note>> ();
                return lstNotes ?? new List<Note> ();
                }
            else
                {
                return new List<Note> ();
                }
            }
        public async Task<List<Note>> Read_NotesDue (int userId, int nDays)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_NotesDue?nDays={nDays}", userId);
            if (response.IsSuccessStatusCode)
                {
                List<Note>? lstDueNotes = await response.Content.ReadFromJsonAsync<List<Note>> ();
                return lstDueNotes ?? new List<Note> ();
                }
            else
                {
                return new List<Note> ();
                }
            }
        public async Task<List<Note>> Read_NotesBySearchKey (string searchKey, int parentId, int mode)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_NotesBySearchKey?mode={mode}&searchKey={searchKey}", parentId);
            if (response.IsSuccessStatusCode)
                {
                List<Note>? lstNotes = await response.Content.ReadFromJsonAsync<List<Note>> ();
                return lstNotes ?? new List<Note> ();
                }
            else
                {
                return new List<Note> ();
                }
            }
        public async Task<Note> Read_Note (int noteId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Read_Note", noteId);
            if (response.IsSuccessStatusCode)
                {
                Note? note = await response.Content.ReadFromJsonAsync<Note> ();
                return note ?? new Note ();
                }
            else
                {
                return new Note ();
                }
            }
        public async Task<bool> Update_Note (Note note)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_Note", note);
            return response.IsSuccessStatusCode ? true : false;
            }
        public async Task<bool> Update_NoteRef (Note note)
            {
            var response = await _http.PostAsJsonAsync ("api/Update_NoteRef", note);
            return response.IsSuccessStatusCode ? true : false;
            }
        public async Task<bool> Delete_Notes (int refId, int refType)
            {
            var response = await _http.PostAsJsonAsync ($"api/Delete_Notes?refType={refType}", refId);
            return response.IsSuccessStatusCode ? true : false;
            }
        public async Task<bool> Delete_Note (int noteId)
            {
            var response = await _http.PostAsJsonAsync ($"api/Delete_Note", noteId);
            return response.IsSuccessStatusCode ? true : false;
            }
        #endregion
        #region ANALYSIS
        public async Task<List<Diversity>> Calc_Diversity (int setId, int subsetIndex)
            {
            var response = await _http.PostAsJsonAsync ($"api/Calc_Diversity?subsetIndex={subsetIndex}", setId);
            if (response.IsSuccessStatusCode)
                {
                List<Diversity> lstDiversities = await response.Content.ReadFromJsonAsync<List<Diversity>> ();
                return lstDiversities;
                }
            else
                {
                return new List<Diversity> ();
                }
            }
        public async Task<FV> Get_FloristicValueStats (int setId, int subsetIndex)
            {
            var response = await _http.PostAsJsonAsync ($"api/Get_FloristicValueStats?subsetIndex={subsetIndex}", setId);
            if (response.IsSuccessStatusCode)
                {
                FV FVstats = await response.Content.ReadFromJsonAsync<FV> ();
                return FVstats;
                }
            else
                {
                return new FV ();
                }
            }
        public async Task<List<Species>> Get_FlorAnData (int setId, int subsetIndex)
            {
            var response = await _http.PostAsJsonAsync ($"api/Get_FlorAnData?subsetIndex={subsetIndex}", setId);
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
        public async Task<List<Occurrence>> Get_MultivarFlorAnData (int setId, int subsetIndex)
            {
            var response = await _http.PostAsJsonAsync ($"api/Get_MultivarFlorAnData?subsetIndex={subsetIndex}", setId);
            if (response.IsSuccessStatusCode)
                {
                List<Occurrence> lstOccurrenceData = await response.Content.ReadFromJsonAsync<List<Occurrence>> ();
                return lstOccurrenceData;
                }
            else
                {
                return new List<Occurrence> ();
                }
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
        #region Export PDF
        public byte [] GenerateReportOfDatasetDataPdf (List<Dataset> _lstDatasets, List<DatasetData> _lstDatasetData, string reportType)
            {
            /* NOTES for developers (1405.07.21):
            New QuestPDF upgrade doesn't automatically use Farsi-fonts. Use standard web-font 'Vazirmatn-Regular.ttf'.
            1- copy font file into a project folder eg. D:\VS\Examiner\ExaminerB\Resources\Fonts
            2- register the font in program.cs (see there)
            3- use the font as below:
            page.DefaultTextStyle (x => x 
                        .FontFamily ("Lato", "Vazirmatn")
                        .FontSize (10)); */
            QuestPDF.Settings.License = LicenseType.Community;
            var report = Document.Create (container =>
            {
                container.Page (page =>
                {
                    page.Size (PageSizes.A4);
                    page.Margin (1, Unit.Centimetre);
                    page.PageColor (Colors.White);
                    page.DefaultTextStyle (x => x
                        .FontFamily ("Lato", "Vazirmatn")
                        .FontSize (10));
                    switch (reportType)
                        {
                        case "ReportSpecies":
                                {
                                page.Header ().Element (compose => ComposeHeader (compose, "Report Species"));
                                page.Content ().Element (compose => ComposeContentSpecies (compose, _lstDatasets, _lstDatasetData));
                                page.Footer ().Element (ComposeFooter);
                                break;
                                }
                        case "ReportDatasets":
                                {
                                page.Header ().Element (compose => ComposeHeader (compose, "Report Datasets"));
                                page.Content ().Element (compose => ComposeContentDatasets (compose, _lstDatasets, _lstDatasetData));
                                page.Footer ().Element (ComposeFooter);
                                break;
                                }
                        case "ReportDatasetTitles":
                                {
                                page.Header ().Element (compose => ComposeHeader (compose, "Report Titles of selected Datasets"));
                                page.Content ().Element (compose => ComposeContentDatasetTitles (compose, _lstDatasets));
                                page.Footer ().Element (ComposeFooter);
                                break;
                                }
                        default:
                                {
                                return;
                                }
                        }
                });
            });
            return report.GeneratePdf ();
            }
        private void ComposeHeader (IContainer container, string title)
            {
            var imagePath = Path.Combine (Directory.GetCurrentDirectory (), "wwwroot", "img", "_logoAlamut.png");
            container.Row (row =>
            {
                row.RelativeItem ().Column (column =>
                {
                    column.Item ()
                        .AlignCenter ()
                        .Width (80)
                        .Image (imagePath);
                    column.Item ()
                        .AlignCenter ()
                        .Text (title)
                        .FontSize (10)
                        .Bold ()
                        .FontColor (Colors.Grey.Darken2)
                        .DirectionFromRightToLeft ();
                    column.Item ()
                        .AlignCenter ()
                        .Text ($"{ToPersianDate (DateTime.Now.ToString ())}") //if DateTime.UtcNow is used, 2:30 or 3:30 must be added for Iran (depending on SummerTime)
                        .FontSize (9)
                        .FontColor (Colors.Red.Darken1);
                });
            });
            }
        private void ComposeFooter (IContainer container)
            {
            container.AlignCenter ().Text (x =>
            {
                x.Span ("صفحه ");
                x.CurrentPageNumber ();
                x.Span (" از ");
                x.TotalPages ();
            });
            }
        private void ComposeContentSpecies (IContainer container, List<Dataset> parent, List<DatasetData> data)
            {
            container.PaddingVertical (10).Column (column =>
            {
                column.Spacing (10);
                //Header card
                column.Item ().Element (cardContainer =>
                {
                    cardContainer
                        .Border (1)
                        .BorderColor (Colors.Grey.Lighten4)
                        .Background (Colors.Yellow.Lighten5)
                        .Padding (12)
                        .ShowEntire ()
                        .Column (cardColumn =>
                        {
                            cardColumn.Spacing (8);
                            cardColumn.Item ().AlignLeft ().Text (text =>
                            {
                                text.Span ($"dataset: {parent [0].DatasetId.ToString ()} \r\n ")
                                    .FontSize (9)
                                    .FontColor (Colors.Blue.Darken3)
                                    .DirectionFromRightToLeft ();
                                text.Span ($"{parent [0].DatasetName}   (lang: {parent [0].RefLang}) \r\n")
                                    .FontSize (9)
                                    .FontColor (Colors.Red.Darken3)
                                    .DirectionFromRightToLeft ();
                                text.Span ($"abbr: {parent [0].DatasetAbbr} \r\n ")
                                    .FontSize (9)
                                    .FontColor (Colors.Blue.Darken3)
                                    .DirectionFromRightToLeft ();
                                text.Span ($"prov: {parent [0].ProvinceName} \r\n ")
                                    .FontSize (9)
                                    .FontColor (Colors.Blue.Darken3)
                                    .DirectionFromRightToLeft ();
                                text.Span ($"coords: {parent [0].Coords} \r\n ")
                                    .FontSize (9)
                                    .FontColor (Colors.Blue.Darken3)
                                    .DirectionFromRightToLeft ();
                                text.Span ($"ref: {parent [0].DatasetRef} \r\n")
                                    .FontSize (9)
                                    .FontColor (Colors.Black)
                                    .DirectionFromRightToLeft ();
                            });
                        });
                });
                //Table of Species
                column.Item ().Table (table =>
                {
                    //1 Define columns
                    table.ColumnsDefinition (columns =>
                    {
                        columns.ConstantColumn (35);  //row # fix width in points (1 point = 1/72 inch)
                        columns.RelativeColumn (2);   //Sciname --relative width (using sum of all relatives) 
                    });
                    //2 Header
                    table.Header (header =>
                    {
                        header.Cell ().Background (Colors.Grey.Lighten3).Padding (4)
                            .Text ("#").FontSize (9).Bold ();
                        header.Cell ().Background (Colors.Grey.Lighten3).Padding (4)
                            .Text ("Sciname").FontSize (9).Bold ();
                    });
                    //3 Body rows
                    int index = 0;
                    foreach (var item in data)
                        {
                        index++;
                        var bg = index % 2 == 0 ? Colors.Grey.Lighten5 : Colors.White;
                        table.Cell ().Background (bg).BorderBottom (0.5f).BorderColor (Colors.Grey.Lighten3)
                            .Padding (4).Text (index.ToString ()).FontSize (9);
                        table.Cell ().Background (bg).BorderBottom (0.5f).BorderColor (Colors.Grey.Lighten3)
                            .Padding (4).Text (item.SciName).FontSize (12);
                        }
                });
            });
            }
        private void ComposeContentDatasets (IContainer container, List<Dataset> data, List<DatasetData> parent)
            {
            container.PaddingVertical (10).Column (column =>
            {
                column.Spacing (10);
                //Header card
                column.Item ().Element (cardContainer =>
                {
                    cardContainer
                        .Border (1)
                        .BorderColor (Colors.Grey.Lighten4)
                        .Background (Colors.Yellow.Lighten5)
                        .Padding (12)
                        .ShowEntire ()
                        .Column (cardColumn =>
                        {
                            cardColumn.Spacing (8);
                            cardColumn.Item ().AlignLeft ().Text (text =>
                            {
                                text.Span ($"{parent [0].SciName}")
                                    .FontSize (9)
                                    .FontColor (Colors.Red.Darken3)
                                    .DirectionFromRightToLeft ();
                            });
                        });
                });
                //Table of Species
                column.Item ().Table (table =>
                {
                    //1 Define columns
                    table.ColumnsDefinition (columns =>
                    {
                        columns.ConstantColumn (35);  //row # fix width in points (1 point = 1/72 inch)
                        columns.RelativeColumn (2);   //province --relative width (using sum of all relatives) 
                        columns.RelativeColumn (5);   //Dataset  --relative width (using sum of all relatives) 
                    });
                    //2 Header
                    table.Header (header =>
                    {
                        header.Cell ().Background (Colors.Grey.Lighten3).Padding (4)
                            .Text ("#").FontSize (9).Bold ();
                        header.Cell ().Background (Colors.Grey.Lighten3).Padding (4)
                            .Text ("Prvince").FontSize (9).Bold ();
                        header.Cell ().Background (Colors.Grey.Lighten3).Padding (4)
                            .Text ("Dataset").FontSize (9).Bold ();
                    });
                    //3 Body rows
                    int index = 0;
                    foreach (var item in data)
                        {
                        index++;
                        var bg = index % 2 == 0 ? Colors.Grey.Lighten5 : Colors.White;
                        table.Cell ().Background (bg).BorderBottom (0.5f).BorderColor (Colors.Grey.Lighten3)
                            .Padding (4).Text (index.ToString ()).FontSize (8);
                        table.Cell ().Background (bg).BorderBottom (0.5f).BorderColor (Colors.Grey.Lighten3)
                            .Padding (4).Text (item.ProvinceName).FontSize (8);
                        table.Cell ().Background (bg).BorderBottom (0.5f).BorderColor (Colors.Grey.Lighten3)
                            .Padding (4).Text (item.DatasetName).FontSize (9);
                        }
                });
            });
            }
        private void ComposeContentDatasetTitles (IContainer container, List<Dataset> data)
            {
            container.PaddingVertical (10).Column (column =>
            {
                column.Spacing (10);
                //Header card
                column.Item ().Element (cardContainer =>
                {
                    cardContainer
                        .Border (1)
                        .BorderColor (Colors.Grey.Lighten4)
                        .Background (Colors.Yellow.Lighten5)
                        .Padding (12)
                        .ShowEntire ()
                        .Column (cardColumn =>
                        {
                            cardColumn.Spacing (8);
                            cardColumn.Item ().AlignLeft ().Text (text =>
                            {
                                text.Span ($"List of selectes datasets")
                                    .FontSize (9)
                                    .FontColor (Colors.Red.Darken3)
                                    .DirectionFromRightToLeft ();
                            });
                        });
                });
                //Table of Species
                column.Item ().Table (table =>
                {
                    //1 Define columns
                    table.ColumnsDefinition (columns =>
                    {
                        columns.ConstantColumn (35);  //row # fix width in points (1 point = 1/72 inch)
                        columns.RelativeColumn (2);   //province --relative width (using sum of all relatives) 
                        columns.RelativeColumn (5);   //Dataset  --relative width (using sum of all relatives) 
                    });
                    //2 Header
                    table.Header (header =>
                    {
                        header.Cell ().Background (Colors.Grey.Lighten3).Padding (4)
                            .Text ("#").FontSize (9).Bold ();
                        header.Cell ().Background (Colors.Grey.Lighten3).Padding (4)
                            .Text ("Prvince").FontSize (9).Bold ();
                        header.Cell ().Background (Colors.Grey.Lighten3).Padding (4)
                            .Text ("Dataset").FontSize (9).Bold ();
                    });
                    //3 Body rows
                    int index = 0;
                    foreach (var item in data)
                        {
                        index++;
                        var bg = index % 2 == 0 ? Colors.Grey.Lighten5 : Colors.White;
                        table.Cell ().Background (bg).BorderBottom (0.5f).BorderColor (Colors.Grey.Lighten3)
                            .Padding (4).Text (index.ToString ()).FontSize (8);
                        table.Cell ().Background (bg).BorderBottom (0.5f).BorderColor (Colors.Grey.Lighten3)
                            .Padding (4).Text (item.ProvinceName).FontSize (8);
                        table.Cell ().Background (bg).BorderBottom (0.5f).BorderColor (Colors.Grey.Lighten3)
                            .Padding (4).Text (item.DatasetName).FontSize (9);
                        }
                });
            });
            }
        #endregion
        }
    }
