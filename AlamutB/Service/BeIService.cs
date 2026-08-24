using AlamutS.Models;
using Microsoft.Data.SqlClient;
namespace AlamutB.Services2Backend
    {
    public interface BeIService
        {
        #region USER
        Task LogAsync (int userId, SqlConnection cnn);
        Task<bool> Create_UserAsync (User user);
        Task<User?> Read_UserAsync (string username, string password);
        Task<List<User>> Read_UsersAsync ();
        Task<List<User>> Read_NewUsersAsync ();
        Task<bool> Update_UserAsync (User user);
        Task<bool> Delete_UserAsync (int userId);
        #endregion
        #region COUNTRY
        Task<bool> Create_CountryAsync (Country country);
        Task<List<Country>> Read_CountriesAsync ();
        Task<bool> Update_CountryAsync (Country country);
        Task<bool> Delete_CountryAsync (int countryId);
        #endregion
        #region PROVINCE
        Task<bool> Create_ProvinceAsync (Province province);
        Task<List<Province>> Read_ProvincesAsync (int countryId);
        Task<bool> Update_ProvinceAsync (Province province);
        Task<bool> Delete_ProvinceAsync (int provinceId);
        #endregion
        #region TAXONOMY-ApgGroup
        Task<List<ApgGroup>> Read_ApgGroupsAsync ();
        #endregion
        #region TAXONOMY-ORDER
        Task<bool> Create_OrderAsync (Order order);
        Task<List<Order>> Read_OrdersAsync (int apgGroupId);
        Task<List<Order>> Read_OrdersBySearchAsync (string orderKeyword);
        Task<Order> Read_OrderAsync (int orderId);
        Task<bool> Update_OrderAsync (Order order);
        Task<bool> Delete_OrderAsync (int orderId);
        #endregion
        #region TAXONOMY-FAMILY
        Task<bool> Create_FamilyAsync (Family family);
        Task<List<Family>> Read_FamiliesAsync (int orderId);
        Task<List<Family>> Read_FamiliesBySearchAsync (string familyKeyword);
        Task<Family> Read_FamilyAsync (int familyId);
        Task<bool> Update_FamilyAsync (Family family);
        Task<bool> Delete_FamilyAsync (int familyId);
        #endregion
        #region TAXONOMY-GENUS
        Task<bool> Create_GenusAsync (Genus genus);
        Task<List<Genus>> Read_GeneraAsync (int familyId);
        Task<List<Genus>> Read_GeneraBySearchAsync (string genusKeyword);
        Task<Genus> Read_GenusAsync (int genusId);
        Task<bool> Update_GenusAsync (Genus genus);
        Task<bool> Delete_GenusAsync (int genusId);
        #endregion
        #region TAXONOMY-SPECIES
        Task<bool> Create_SpeciesAsync (Species species);
        Task<List<Species>> Read_SpeciesAsync (int genusId);
        Task<List<Species>> Read_SubspeciesAsync (string genusName, string epithet);
        Task<List<Species>> Read_SpeciesByDatasetIdAsync (int datasetType, int datasetLMId);
        Task<Species> Read_SpeciesByIdAsync (int speciesId);
        Task<bool> Update_SpeciesAsync (Species species);
        Task<bool> Delete_SpeciesAsync (int speciesId);
        #endregion
        #region DATASET
        Task<bool> Create_DatasetAsync (Dataset dataset);
        Task<List<Dataset>> Read_DatasetsAsync (int userId, int datasetType);
        Task<List<Dataset>> Read_DatasetsBySearchAsync (int userId, int datasetType, string datasetKeyword);
        Task<Dataset> Read_DatasetAsync (int datasetId);
        Task<bool> Update_DatasetAsync (Dataset dataset);
        Task<bool> Delete_DatasetAsync (int DatasetId);
        #endregion
        #region DATASETDATA-FL
        Task<bool> Create_DatasetDataFLAsync (DatasetDataFL datasetDataFL);
        Task<List<DatasetDataFL>> Read_DatasetDataFLAsync (int datasetId);
        Task<bool> Update_DatasetDataFLAsync (DatasetDataFL datasetDataFL);
        Task<bool> Delete_DatasetDataFLAsync (int datasetDataFLId);
        #endregion
        #region DATASETDATA-CN
        Task<bool> Create_DatasetDataCNAsync (DatasetDataCN datasetDataCN);
        Task<List<DatasetDataCN>> Read_DatasetDataCNAsync (int datasetId);
        Task<bool> Update_DatasetDataCNAsync (DatasetDataCN datasetDataCN);
        Task<bool> Delete_DatasetDataCNAsync (int datasetDataCNId);
        #endregion
        #region DATASETDATA-LM
        Task<bool> Create_DatasetDataLMAsync (DatasetDataLM datasetDataLM);
        Task<List<DatasetDataLM>> Read_DatasetDataLMAsync (int datasetId);
        Task<bool> Update_DatasetDataLMAsync (DatasetDataLM datasetDataLM);
        Task<bool> Delete_DatasetDataLMAsync (int datasetLMId, string speciesId);
        #endregion
        #region STATION
        Task<bool> Create_StationAsync (Station station);
        Task<List<Station>> Read_StationsAsync ();
        Task<Station> Read_StationAsync (int stationId);
        Task<bool> Update_StationAsync (Station station);
        Task<bool> Delete_StationAsync (int stationId);
        #endregion
        #region DATASETDATA-ST
        Task<bool> Create_DatasetDataSTAsync (DatasetDataST datasetDataST);
        Task<List<DatasetDataST>> Read_DatasetDataSTAsync (int datasetId);
        Task<bool> Update_DatasetDataSTAsync (DatasetDataST datasetDataST);
        Task<bool> Delete_DatasetDataSTAsync (int datasetDataSTId);
        #endregion
        #region SET
        Task<bool> Create_SetAsync (Set set);
        Task<List<Set>> Read_SetsAsync (int userId);
        Task<Set> Read_SetAsync (int setId);
        Task<bool> Update_SetAsync (Set Set);
        Task<bool> Delete_SetAsync (int setId);
        #endregion
        #region SETDATA
        Task<bool> Create_SetDataAsync (SetData setData);
        Task<List<SetData>> Read_SetDataAsync (int setId);
        Task<bool> Update_SetDataAsync (SetData setData);
        Task<bool> Delete_SetDataAsync (int setDataId);
        #endregion
        #region NOTEBOOK
        Task<int> Create_NotebookAsync (Notebook notebook);
        Task<List<Notebook>> Read_NotebooksAsync (int userId);
        Task<Notebook> Read_NotebookAsync (int notebookId);
        Task<bool> Update_NotebookAsync (Notebook notebook);
        Task<bool> Delete_NotebookAsync (int notebookId);
        #endregion
        #region SECTION
        Task<int> Create_SectionAsync (Section section);
        Task<List<Section>> Read_SectionsAsync (int notebookId, bool readNotes);
        Task<Section> Read_SectionAsync (int sectionId, bool readNotes);
        Task<bool> Update_SectionAsync (Section section);
        Task<bool> Update_SectionParentAsync (Section section);
        Task<bool> Delete_SectionAsync (int subProjectId);
        #endregion
        #region NOTE
        Task<bool> Create_NoteAsync (Note note);
        Task<List<Note>> Read_NotesAsync (int refType, int refId);
        Task<List<Note>> Read_NotesDueAsync (int userId, int nDays);
        Task<Note> Read_NoteAsync (int noteId);
        Task<bool> Update_NoteAsync (Note note);
        Task<bool> Delete_NotesAsync (int refType, int refId);
        Task<bool> Delete_NoteAsync (int noteId);
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
