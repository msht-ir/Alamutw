using AlamutS.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Data;
using System.Drawing;
using static MudBlazor.CategoryTypes;
using Order = AlamutS.Models.Order;
using Station = AlamutS.Models.Station;
namespace AlamutB.Services2Backend
    {
    public class BeService : BeIService
        {
        private readonly IConfiguration _config;
        public BeService (IConfiguration config) => _config = config;
        #region U:Usrs
        public async Task LogAsync (int userId, SqlConnection cnn)
            {
            //
            }
        public async Task<bool> Create_UserAsync (User user)
            {
            string sql = "BS.Create_User";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connString);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@UserNickname", user.UserNickname);
                cmd.Parameters.AddWithValue ("@Username", user.Username);
                cmd.Parameters.AddWithValue ("@Password", user.Password);
                cmd.Parameters.AddWithValue ("@UserEmail", user.UserEmail);
                cmd.Parameters.AddWithValue ("@ExpDateTime", user.ExpDateTime);
                cmd.Parameters.AddWithValue ("@UserTags", user.UserTags);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception)
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<User?> Read_UserAsync (string username, string password)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Read_User";
            using SqlConnection cnn = new (connString);
            User userOut = new ();
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@Username", username);
                cmd.Parameters.AddWithValue ("@Password", password);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                while (await reader.ReadAsync ())
                    {
                    if ((username.ToLower () == reader.GetString (2).ToLower ()) && (password == reader.GetString (3)))
                        {
                        userOut.UserId = reader.GetInt32 (0);
                        userOut.UserNickname = reader.GetString (1);
                        userOut.Username = reader.GetString (2);
                        userOut.Password = reader.GetString (3);
                        userOut.UserEmail = reader.GetString (4);
                        userOut.ExpDateTime = reader.GetDateTimeOffset (5);
                        userOut.UserTags = reader.GetInt32 (6);
                        }
                    }
                await cnn.CloseAsync ();
                if (userOut.UserId > 0)
                    {
                    //await LogAsync (userOut.UserId, cnn);
                    return userOut;
                    }
                return new User ();
                }
            catch (Exception ex)
                {
                Console.WriteLine ("LoginAsTeacher error: " + ex.Message);
                cnn.Close ();
                return new User ();
                }
            }
        public async Task<List<User>> Read_UsersAsync ()
            {
            var users = new List<User> ();
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connString);
            using SqlCommand cmd = new ("BS.Read_Users", cnn); // -- ORDER BY UserNickname
            cmd.CommandType = CommandType.StoredProcedure;
            await cnn.OpenAsync ();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync ();
            while (await reader.ReadAsync ())
                {
                users.Add (new User
                    {
                    UserId = reader.GetInt32 (0),
                    UserNickname = reader.GetString (1),
                    Username = reader.GetString (2),
                    Password = reader.GetString (3),
                    UserEmail = reader.GetString (4),
                    ExpDateTime = reader.GetDateTime (5),
                    UserTags = reader.GetInt32 (6)
                    });
                }
            return users;
            }
        public async Task<List<User>> Read_NewUsersAsync ()
            {
            var users = new List<User> ();
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connString);
            using SqlCommand cmd = new ("BS.Read_NewUsers", cnn); // -- ORDER BY usrNickName
            cmd.CommandType = CommandType.StoredProcedure;
            await cnn.OpenAsync ();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync ();
            while (await reader.ReadAsync ())
                {
                users.Add (new User
                    {
                    UserId = reader.GetInt32 (0),
                    UserNickname = reader.GetString (1),
                    Username = reader.GetString (2),
                    Password = reader.GetString (3),
                    UserEmail = reader.GetString (4),
                    ExpDateTime = reader.GetDateTime (5),
                    UserTags = reader.GetInt32 (6)
                    });
                }
            return users;
            }
        public async Task<bool> Update_UserAsync (User user)
            {
            string sql = "BS.Update_User";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@UserNickname", user.UserNickname);
                cmd.Parameters.AddWithValue ("@Username", user.Username);
                cmd.Parameters.AddWithValue ("@Password", user.Password);
                cmd.Parameters.AddWithValue ("@UserEmail", user.UserEmail);
                cmd.Parameters.AddWithValue ("@ExpDateTime", user.ExpDateTime);
                cmd.Parameters.AddWithValue ("@UserTags", user.UserTags);
                cmd.Parameters.AddWithValue ("@UserId", user.UserId);
                await cmd.ExecuteNonQueryAsync ();
                return true;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public async Task<bool> Delete_UserAsync (int userId)
            {
            string sql = "BS.Delete_User";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new (connString);
            await cnn.OpenAsync ();
            using SqlCommand cmd = new SqlCommand (sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue ("@UserId", userId);
            await cmd.ExecuteNonQueryAsync ();
            return true;
            }
        #endregion
        #region COUNTRY
        public async Task<bool> Create_CountryAsync (Country country)
            {
            string sql = "BS.Create_Country";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@CountryName", country.CountryName);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<Country>> Read_CountriesAsync ()
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Read_Countries";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Country> lstCountries = new List<Country> ();
                while (await reader.ReadAsync ())
                    {
                    lstCountries.Add (new Country
                        {
                        CountryId = reader.GetInt32 (0),
                        CountryName = reader.GetString (1)
                        });
                    }
                await cnn.CloseAsync ();
                return lstCountries;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Country> ();
                }
            }
        public async Task<bool> Update_CountryAsync (Country country)
            {
            string sql = "BS.Update_Country";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@CountryId", country.CountryId);
                cmd.Parameters.AddWithValue ("@CountryName", country.CountryName);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<bool> Delete_CountryAsync (int countryId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Delete_Country";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@CountryId", countryId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        #endregion
        #region PROVINCE
        public async Task<bool> Create_ProvinceAsync (Province province)
            {
            string sql = "BS.Create_Province";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@ProvinceId", province.ProvinceId);
                cmd.Parameters.AddWithValue ("@CountryId", province.CountryId);
                cmd.Parameters.AddWithValue ("@ProvinceName", province.ProvinceName);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<Province>> Read_ProvincesAsync (int countryId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Read_Provinces";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@CountryId", countryId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Province> lstProvinces = new List<Province> ();
                while (await reader.ReadAsync ())
                    {
                    lstProvinces.Add (new Province
                        {
                        ProvinceId = reader.GetInt32 (0),
                        CountryId = reader.GetByte (1),
                        ProvinceName = reader.GetString (2)
                        });
                    }
                await cnn.CloseAsync ();
                return lstProvinces;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Province> ();
                }
            }
        public async Task<bool> Update_ProvinceAsync (Province province)
            {
            string sql = "BS.Update_Province";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@ProvinceId", province.ProvinceId);
                cmd.Parameters.AddWithValue ("@CountryId", province.CountryId);
                cmd.Parameters.AddWithValue ("@ProvinceName", province.ProvinceName);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<bool> Delete_ProvinceAsync (int provinceId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Delete_Country";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@ProvinceId", provinceId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        #endregion
        #region TAXONOMY-ApgGroup
        public async Task<List<ApgGroup>> Read_ApgGroupsAsync ()
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "TX.Read_ApgGroups";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<ApgGroup> lstApgGroups = new List<ApgGroup> ();
                while (await reader.ReadAsync ())
                    {
                    lstApgGroups.Add (new ApgGroup
                        {
                        ApgGroupId = reader.GetInt32 (0),
                        ApgGroupSeq = reader.GetInt16 (1),
                        ApgGroupName = reader.GetString (2)
                        });
                    }
                await cnn.CloseAsync ();
                foreach (ApgGroup apg in lstApgGroups)
                    {
                    apg.Orders = await Read_OrdersAsync (apg.ApgGroupId);
                    }
                return lstApgGroups;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<ApgGroup> ();
                }
            }
        #endregion
        #region TAXONOMY-ORDER
        public async Task<bool> Create_OrderAsync (Order order)
            {
            string sql = "BS.Create_Order";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@ApgGroupId", order.ApgGroupId);
                cmd.Parameters.AddWithValue ("@OrderName", order.OrderName);
                cmd.Parameters.AddWithValue ("@OrderSequence", order.OrderSequence);
                cmd.Parameters.AddWithValue ("@OrderNote", order.OrderNote);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<Order>> Read_OrdersAsync (int apgGroupId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "TX.Read_Orders";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@ApgGroupId", apgGroupId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Order> lstOrders = new List<Order> ();
                while (await reader.ReadAsync ())
                    {
                    lstOrders.Add (new Order
                        {
                        OrderId = reader.GetInt32 (0),
                        ApgGroupId = reader.GetInt32 (1),
                        OrderName = reader.GetString (2),
                        OrderSequence = reader.GetInt16 (3),
                        OrderNote = reader.GetString (4)
                        });
                    }
                await cnn.CloseAsync ();
                return lstOrders;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Order> ();
                }
            }
        public async Task<List<Order>> Read_OrdersBySearchAsync (string orderKeyword)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "TX.Read_OrdersBySearch";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@OrderKeyword", orderKeyword);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Order> lstOrders = new List<Order> ();
                while (await reader.ReadAsync ())
                    {
                    lstOrders.Add (new Order
                        {
                        OrderId = reader.GetInt32 (0),
                        ApgGroupId = reader.GetInt32 (1),
                        OrderName = reader.GetString (2),
                        OrderSequence = reader.GetInt16 (3),
                        OrderNote = reader.GetString (4)
                        });
                    }
                await cnn.CloseAsync ();
                return lstOrders;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Order> ();
                }
            }
        public async Task<Order> Read_OrderAsync (int orderId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Read_Order";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                Order order = new Order ();
                while (await reader.ReadAsync ())
                    {
                    order.OrderId = reader.GetInt32 (0);
                    order.ApgGroupId = reader.GetInt32 (1);
                    order.OrderName = reader.GetString (2);
                    order.OrderSequence = reader.GetInt16 (3);
                    order.OrderNote = reader.GetString (4);
                    }
                await cnn.CloseAsync ();
                return order;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new Order ();
                }
            }
        public async Task<bool> Update_OrderAsync (Order order)
            {
            string sql = "BS.Update_Order";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@OrderId", order.OrderId);
                cmd.Parameters.AddWithValue ("@ApgGroupId", order.ApgGroupId);
                cmd.Parameters.AddWithValue ("@OrderName", order.OrderName);
                cmd.Parameters.AddWithValue ("@OrderSequence", order.OrderSequence);
                cmd.Parameters.AddWithValue ("@OrderNote", order.OrderNote);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<bool> Delete_OrderAsync (int orderId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Delete_Order";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@OrderId", orderId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        #endregion
        #region TAXONOMY-FAMILY
        public async Task<bool> Create_FamilyAsync (Family family)
            {
            string sql = "BS.Create_Family";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@OrderId", family.OrderId);
                cmd.Parameters.AddWithValue ("@FamilyName", family.FamilyName);
                cmd.Parameters.AddWithValue ("@FamilyAut", family.FamilyAut);
                cmd.Parameters.AddWithValue ("@Protolog", family.Protolog);
                cmd.Parameters.AddWithValue ("@DeltaNote", family.DeltaNote);
                cmd.Parameters.AddWithValue ("@FlowerFormula", family.FlowerFormula);
                cmd.Parameters.AddWithValue ("@FamilyTags", family.FamilyTags);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<Family>> Read_FamiliesAsync (int orderId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "TX.Read_Families";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@OrderId", orderId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Family> lstFamilies = new List<Family> ();
                while (await reader.ReadAsync ())
                    {
                    lstFamilies.Add (new Family
                        {
                        FamilyId = reader.GetInt32 (0),
                        OrderId = reader.GetInt32 (1),
                        FamilyName = reader.GetString (2),
                        FamilyAut = reader.GetString (3),
                        Protolog = reader.GetString (4),
                        DeltaNote = reader.GetString (5),
                        FlowerFormula = reader.GetString (6),
                        FamilyTags = reader.GetInt32 (7)
                        });
                    }
                await cnn.CloseAsync ();
                //foreach (Family fam in lstFamilies)
                //    {
                //    fam.Genera = await Read_GeneraAsync (fam.FamilyId);
                //    }
                return lstFamilies;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Family> ();
                }
            }
        public async Task<List<Family>> Read_FamiliesBySearchAsync (string familyKeyword)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "TX.Read_FamiliesBySearch";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@FamilyKeyword", familyKeyword);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Family> lstFamilies = new List<Family> ();
                while (await reader.ReadAsync ())
                    {
                    lstFamilies.Add (new Family
                        {
                        FamilyId = reader.GetInt32 (0),
                        OrderId = reader.GetInt32 (1),
                        FamilyName = reader.GetString (2),
                        FamilyAut = reader.GetString (3),
                        Protolog = reader.GetString (4),
                        DeltaNote = reader.GetString (5),
                        FlowerFormula = reader.GetString (6),
                        FamilyTags = reader.GetInt32 (7)
                        });
                    }
                await cnn.CloseAsync ();
                return lstFamilies;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Family> ();
                }
            }
        public async Task<Family> Read_FamilyAsync (int familyId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Read_Family";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@FamilyId", familyId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                Family family = new Family ();
                while (await reader.ReadAsync ())
                    {
                    family.FamilyId = reader.GetInt32 (0);
                    family.OrderId = reader.GetInt32 (1);
                    family.FamilyName = reader.GetString (2);
                    family.FamilyAut = reader.GetString (3);
                    family.Protolog = reader.GetString (4);
                    family.DeltaNote = reader.GetString (5);
                    family.FlowerFormula = reader.GetString (6);
                    family.FamilyTags = reader.GetInt32 (7);
                    }
                await cnn.CloseAsync ();
                return family;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new Family ();
                }
            }
        public async Task<bool> Update_FamilyAsync (Family family)
            {
            string sql = "BS.Update_Family";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@FamilyId", family.FamilyId);
                cmd.Parameters.AddWithValue ("@OrderId", family.OrderId);
                cmd.Parameters.AddWithValue ("@FamilyName", family.FamilyName);
                cmd.Parameters.AddWithValue ("@FamilyAut", family.FamilyAut);
                cmd.Parameters.AddWithValue ("@Protolog", family.Protolog);
                cmd.Parameters.AddWithValue ("@DeltaNote", family.DeltaNote);
                cmd.Parameters.AddWithValue ("@FlowerFormula", family.FlowerFormula);
                cmd.Parameters.AddWithValue ("@FamilyTags", family.FamilyTags);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<bool> Delete_FamilyAsync (int familyId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Delete_Fmaily";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@FamilyId", familyId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        #endregion
        #region TAXONOMY-GENUS
        public async Task<bool> Create_GenusAsync (Genus genus)
            {
            string sql = "BS.Create_Genus";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@FamilyId", genus.FamilyId);
                cmd.Parameters.AddWithValue ("@GenusName", genus.GenusName);
                cmd.Parameters.AddWithValue ("@GenusAut", genus.GenusAut);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<Genus>> Read_GeneraAsync (int familyId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "TX.Read_Genera";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@FamilyId", familyId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Genus> lstGenera = new List<Genus> ();
                while (await reader.ReadAsync ())
                    {
                    lstGenera.Add (new Genus
                        {
                        GenusId = reader.GetInt32 (0),
                        FamilyId = reader.GetInt32 (1),
                        GenusName = reader.GetString (2),
                        GenusAut = reader.GetString (3)
                        });
                    }
                await cnn.CloseAsync ();
                return lstGenera;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Genus> ();
                }
            }
        public async Task<List<Genus>> Read_GeneraBySearchAsync (string genusKeyword)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "TX.Read_GeneraBySearch";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@GenusKeyword", genusKeyword);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Genus> lstGenera = new List<Genus> ();
                while (await reader.ReadAsync ())
                    {
                    lstGenera.Add (new Genus
                        {
                        GenusId = reader.GetInt32 (0),
                        FamilyId = reader.GetInt32 (1),
                        GenusName = reader.GetString (2),
                        GenusAut = reader.GetString (3)
                        });
                    }
                await cnn.CloseAsync ();
                return lstGenera;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Genus> ();
                }
            }
        public async Task<Genus> Read_GenusAsync (int genusId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Read_Genus";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@GenusId", genusId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                Genus genus = new Genus ();
                while (await reader.ReadAsync ())
                    {
                    genus.GenusId = reader.GetInt32 (0);
                    genus.FamilyId = reader.GetInt32 (1);
                    genus.GenusName = reader.GetString (2);
                    genus.GenusAut = reader.GetString (3);
                    }
                await cnn.CloseAsync ();
                return genus;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new Genus ();
                }
            }
        public async Task<bool> Update_GenusAsync (Genus genus)
            {
            string sql = "BS.Update_Genus";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@GenusId", genus.GenusId);
                cmd.Parameters.AddWithValue ("@FamilyId", genus.FamilyId);
                cmd.Parameters.AddWithValue ("@GenusName", genus.GenusName);
                cmd.Parameters.AddWithValue ("@GenusAut", genus.GenusAut);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<bool> Delete_GenusAsync (int genusId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Delete_Genus";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@GenusId", genusId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        #endregion
        #region TAXONOMY-SPECIES
        public async Task<bool> Create_SpeciesAsync (Species species)
            {
            string sql = "BS.Create_Species";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@SpeciesId", species.SpeciesId);
                cmd.Parameters.AddWithValue ("@AcceptedId", species.AcceptedId);
                cmd.Parameters.AddWithValue ("@GenusId", species.GenusId);
                cmd.Parameters.AddWithValue ("@Epithet", species.Epithet);
                cmd.Parameters.AddWithValue ("@Rank", species.Rank);
                cmd.Parameters.AddWithValue ("@InfraEpithet", species.InfraEpithet);
                cmd.Parameters.AddWithValue ("@Author", species.Author);
                cmd.Parameters.AddWithValue ("@NomStatus", species.NomStatus);
                cmd.Parameters.AddWithValue ("@ConsStatus", species.ConsStatus);
                cmd.Parameters.AddWithValue ("@Characters", species.Characters);
                cmd.Parameters.AddWithValue ("@Chorotype", species.Chorotype);
                cmd.Parameters.AddWithValue ("@Lifeform", species.Lifeform);
                cmd.Parameters.AddWithValue ("@IsHybrid", species.IsHybrid);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<Species>> Read_SpeciesAsync (int genusId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "TX.Read_Species";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@GenusId", genusId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Species> lstSpecies = new List<Species> ();
                while (await reader.ReadAsync ())
                    {
                    lstSpecies.Add (new Species
                        {
                        SpeciesId = reader.GetString (0),
                        AcceptedId = reader.GetString (1),
                        GenusId = reader.GetInt32 (2),
                        Epithet = reader.GetString (3),
                        Rank = reader.GetInt32 (4),
                        InfraEpithet = reader.GetString (5),
                        Author = reader.GetString (6),
                        NomStatus = reader.GetInt32 (7),
                        TaxStatus = reader.GetInt32 (8),
                        ConsStatus = reader.GetInt32 (9),
                        Characters = reader.GetInt32 (10),
                        Chorotype = reader.GetString (11),
                        Lifeform = reader.GetString (12),
                        IsHybrid = reader.GetBoolean (13),
                        SciName = reader.GetString (14)
                        });
                    }
                await cnn.CloseAsync ();
                return lstSpecies;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Species> ();
                }
            }
        public async Task<List<Species>> Read_SubspeciesAsync (string genusName, string epithet)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "TX.Read_Subspecies";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@GenusName", genusName);
                cmd.Parameters.AddWithValue ("@Epithet", epithet);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Species> lstSpecies = new List<Species> ();
                while (await reader.ReadAsync ())
                    {
                    lstSpecies.Add (new Species
                        {
                        SpeciesId = reader.GetString (0),
                        AcceptedId = reader.GetString (1),
                        GenusId = reader.GetInt32 (2),
                        Epithet = reader.GetString (3),
                        Rank = reader.GetInt32 (4),
                        InfraEpithet = reader.GetString (5),
                        Author = reader.GetString (6),
                        NomStatus = reader.GetInt32 (7),
                        TaxStatus = reader.GetInt32 (8),
                        ConsStatus = reader.GetInt32 (9),
                        Characters = reader.GetInt32 (10),
                        Chorotype = reader.GetString (11),
                        Lifeform = reader.GetString (12),
                        IsHybrid = reader.GetBoolean (13),
                        SciName = reader.GetString (14)
                        });
                    }
                await cnn.CloseAsync ();
                return lstSpecies;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Species> ();
                }
            }
        public async Task<List<Species>> Read_SpeciesByDatasetIdAsync (int datasetId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "TX.Read_SpeciesByDatasetId";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", datasetId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Species> lstSpecies = new List<Species> ();
                while (await reader.ReadAsync ())
                    {
                    lstSpecies.Add (new Species
                        {
                        SpeciesId = reader.GetString (0),
                        AcceptedId = reader.GetString (1),
                        GenusId = reader.GetInt32 (2),
                        Epithet = reader.GetString (3),
                        Rank = reader.GetInt32 (4),
                        InfraEpithet = reader.GetString (5),
                        Author = reader.GetString (6),
                        NomStatus = reader.GetInt32 (7),
                        TaxStatus = reader.GetInt32 (8),
                        ConsStatus = reader.GetInt32 (9),
                        Characters = reader.GetInt32 (10),
                        Chorotype = reader.GetString (11),
                        Lifeform = reader.GetString (12),
                        IsHybrid = reader.GetBoolean (13),
                        SciName = reader.GetString (14)
                        });
                    }
                await cnn.CloseAsync ();
                return lstSpecies;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Species> ();
                }
            }
        public async Task<List<Species>> Read_SpeciesBySearchAsync (string genusKeyword, string speciesKeyword)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "TX.Read_SpeciesBySearch";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@GenusKeyword", genusKeyword);
                cmd.Parameters.AddWithValue ("@SpeciesKeyword", speciesKeyword);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Species> lstSpecies = new List<Species> ();
                while (await reader.ReadAsync ())
                    {
                    lstSpecies.Add (new Species
                        {
                        SpeciesId = reader.GetString (0),
                        AcceptedId = reader.GetString (1),
                        GenusId = reader.GetInt32 (2),
                        Epithet = reader.GetString (3),
                        Rank = reader.GetInt32 (4),
                        InfraEpithet = reader.GetString (5),
                        Author = reader.GetString (6),
                        NomStatus = reader.GetInt32 (7),
                        TaxStatus = reader.GetInt32 (8),
                        ConsStatus = reader.GetInt32 (9),
                        Characters = reader.GetInt32 (10),
                        Chorotype = reader.GetString (11),
                        Lifeform = reader.GetString (12),
                        IsHybrid = reader.GetBoolean (13),
                        SciName = reader.GetString (14)
                        });
                    }
                await cnn.CloseAsync ();
                return lstSpecies;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Species> ();
                }
            }
        public async Task<Species> Read_SpeciesByIdAsync (int speciesId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "TX.Read_SpeciesById";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@SpeciesId", speciesId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                Species species = new Species ();
                while (await reader.ReadAsync ())
                    {
                    species.SpeciesId = reader.GetString (0);
                    species.AcceptedId = reader.GetString (1);
                    species.GenusId = reader.GetInt32 (2);
                    species.Epithet = reader.GetString (3);
                    species.Rank = reader.GetInt32 (4);
                    species.InfraEpithet = reader.GetString (5);
                    species.Author = reader.GetString (6);
                    species.NomStatus = reader.GetByte (7);
                    species.TaxStatus = reader.GetByte (8);
                    species.ConsStatus = reader.GetByte (9);
                    species.Characters = reader.GetInt32 (10);
                    species.Chorotype = reader.GetString (11);
                    species.Lifeform = reader.GetString (12);
                    species.IsHybrid = reader.GetBoolean (13);
                    species.SciName = reader.GetString (14);
                    }
                await cnn.CloseAsync ();
                return species;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new Species ();
                }
            }
        public async Task<bool> Update_SpeciesAsync (Species species)
            {
            string sql = "BS.Update_Species";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@SpeciesId", species.SpeciesId);
                cmd.Parameters.AddWithValue ("@AcceptedId", species.AcceptedId);
                cmd.Parameters.AddWithValue ("@GenusId", species.GenusId);
                cmd.Parameters.AddWithValue ("@Epithet", species.Epithet);
                cmd.Parameters.AddWithValue ("@Rank", species.Rank);
                cmd.Parameters.AddWithValue ("@InfraEpithet", species.InfraEpithet);
                cmd.Parameters.AddWithValue ("@Author", species.Author);
                cmd.Parameters.AddWithValue ("@NomStatus", species.NomStatus);
                cmd.Parameters.AddWithValue ("@ConsStatus", species.ConsStatus);
                cmd.Parameters.AddWithValue ("@Characters", species.Characters);
                cmd.Parameters.AddWithValue ("@Chorotype", species.Chorotype);
                cmd.Parameters.AddWithValue ("@Lifeform", species.Lifeform);
                cmd.Parameters.AddWithValue ("@IsHybrid", species.IsHybrid);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<bool> Delete_SpeciesAsync (int speciesId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Delete_Species";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@SpeciesId", speciesId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        #endregion
        #region DATASET
        public async Task<bool> Create_DatasetAsync (Dataset dataset)
            {
            string sql = "DT.Create_Dataset";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@UserId", dataset.UserId);
                cmd.Parameters.AddWithValue ("@DatasetType", dataset.DatasetType);
                cmd.Parameters.AddWithValue ("@DatasetAbbr", dataset.DatasetAbbr);
                cmd.Parameters.AddWithValue ("@DatasetName", dataset.DatasetName);
                cmd.Parameters.AddWithValue ("@DatasetRef", dataset.DatasetRef);
                cmd.Parameters.AddWithValue ("@RefYear", dataset.RefYear);
                cmd.Parameters.AddWithValue ("@RefLang", dataset.RefLang);
                cmd.Parameters.AddWithValue ("@DatasetManager", dataset.DatasetManager);
                cmd.Parameters.AddWithValue ("@AreaHa", dataset.AreaHa);
                cmd.Parameters.AddWithValue ("@ProvinceId", dataset.ProvinceId);
                cmd.Parameters.AddWithValue ("@Coords", dataset.Coords);
                cmd.Parameters.AddWithValue ("@AltMax", dataset.AltMax);
                cmd.Parameters.AddWithValue ("@AltMin", dataset.AltMin);
                cmd.Parameters.AddWithValue ("@AltAver", dataset.AltAver);
                cmd.Parameters.AddWithValue ("@TempMax", dataset.TempMax);
                cmd.Parameters.AddWithValue ("@TempMin", dataset.TempMin);
                cmd.Parameters.AddWithValue ("@TempAver", dataset.TempAver);
                cmd.Parameters.AddWithValue ("@Precipitation", dataset.Precipitation);
                cmd.Parameters.AddWithValue ("@DatasetTags", dataset.DatasetTags);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<Dataset>> Read_DatasetsAsync (int userId, int datasetType)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Read_Datasets";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@UserId", userId);
                cmd.Parameters.AddWithValue ("@DatasetType", datasetType);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Dataset> lstDatasets = new List<Dataset> ();
                while (await reader.ReadAsync ())
                    {
                    lstDatasets.Add (new Dataset
                        {
                        DatasetId = reader.GetInt32 (0),
                        UserId = reader.GetInt32 (1),
                        DatasetType = reader.GetInt32 (2),
                        DatasetAbbr = reader.GetString (3),
                        DatasetName = reader.GetString (4),
                        DatasetRef = reader.GetString (5),
                        RefYear = reader.GetInt32 (6),
                        RefLang = reader.GetString (7),
                        DatasetManager = reader.GetString (8),
                        AreaHa = reader.GetInt32 (9),
                        ProvinceId = reader.GetInt32 (10),
                        Coords = reader.GetString (11),
                        AltMax = reader.GetInt32 (12),
                        AltMin = reader.GetInt32 (13),
                        AltAver = reader.GetInt32 (14),
                        TempMax = reader.GetDecimal (15),
                        TempMin = reader.GetDecimal (16),
                        TempAver = reader.GetDecimal (17),
                        Precipitation = reader.GetDecimal (18),
                        DatasetTags = reader.GetInt32 (19)
                        });
                    }
                await cnn.CloseAsync ();
                return lstDatasets;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Dataset> ();
                }
            }
        public async Task<List<Dataset>> Read_DatasetsBySetIdAsync (int setId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Read_DatasetsBySetId";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@SetId", setId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Dataset> lstDatasets = new List<Dataset> ();
                while (await reader.ReadAsync ())
                    {
                    lstDatasets.Add (new Dataset
                        {
                        DatasetId = reader.GetInt32 (0),
                        UserId = reader.GetInt32 (1),
                        DatasetType = reader.GetInt32 (2),
                        DatasetAbbr = reader.GetString (3),
                        DatasetName = reader.GetString (4),
                        DatasetRef = reader.GetString (5),
                        RefYear = reader.GetInt32 (6),
                        RefLang = reader.GetString (7),
                        DatasetManager = reader.GetString (8),
                        AreaHa = reader.GetInt32 (9),
                        ProvinceId = reader.GetInt32 (10),
                        Coords = reader.GetString (11),
                        AltMax = reader.GetInt32 (12),
                        AltMin = reader.GetInt32 (13),
                        AltAver = reader.GetInt32 (14),
                        TempMax = reader.GetDecimal (15),
                        TempMin = reader.GetDecimal (16),
                        TempAver = reader.GetDecimal (17),
                        Precipitation = reader.GetDecimal (18),
                        DatasetTags = reader.GetInt32 (19)
                        });
                    }
                await cnn.CloseAsync ();
                return lstDatasets;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Dataset> ();
                }
            }
        public async Task<List<Dataset>> Read_DatasetsBySearchAsync (int userId, int datasetType, string datasetKeyword)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Read_DatasetsBySearch";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@UserId", userId);
                cmd.Parameters.AddWithValue ("@DatasetType", datasetType);
                cmd.Parameters.AddWithValue ("@DatasetKeyword", datasetKeyword);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Dataset> lstDatasets = new List<Dataset> ();
                while (await reader.ReadAsync ())
                    {
                    lstDatasets.Add (new Dataset
                        {
                        DatasetId = reader.GetInt32 (0),
                        UserId = reader.GetInt32 (1),
                        DatasetType = reader.GetInt32 (2),
                        DatasetAbbr = reader.GetString (3),
                        DatasetName = reader.GetString (4),
                        DatasetRef = reader.GetString (5),
                        RefYear = reader.GetInt32 (6),
                        RefLang = reader.GetString (7),
                        DatasetManager = reader.GetString (8),
                        AreaHa = reader.GetInt32 (9),
                        ProvinceId = reader.GetInt32 (10),
                        Coords = reader.GetString (11),
                        AltMax = reader.GetInt32 (12),
                        AltMin = reader.GetInt32 (13),
                        AltAver = reader.GetInt32 (14),
                        TempMax = reader.GetDecimal (15),
                        TempMin = reader.GetDecimal (16),
                        TempAver = reader.GetDecimal (17),
                        Precipitation = reader.GetDecimal (18),
                        DatasetTags = reader.GetInt32 (19)
                        });
                    }
                await cnn.CloseAsync ();
                return lstDatasets;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Dataset> ();
                }
            }
        public async Task<Dataset> Read_DatasetAsync (int datasetId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Read_Datasets";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", datasetId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                Dataset dataset = new Dataset ();
                while (await reader.ReadAsync ())
                    {
                    dataset.DatasetId = reader.GetInt32 (0);
                    dataset.UserId = reader.GetInt32 (1);
                    dataset.DatasetType = reader.GetInt32 (2);
                    dataset.DatasetAbbr = reader.GetString (3);
                    dataset.DatasetName = reader.GetString (4);
                    dataset.DatasetRef = reader.GetString (5);
                    dataset.RefYear = reader.GetInt32 (6);
                    dataset.RefLang = reader.GetString (7);
                    dataset.DatasetManager = reader.GetString (8);
                    dataset.AreaHa = reader.GetInt32 (9);
                    dataset.ProvinceId = reader.GetInt32 (10);
                    dataset.Coords = reader.GetString (11);
                    dataset.AltMax = reader.GetInt32 (12);
                    dataset.AltMin = reader.GetInt32 (13);
                    dataset.AltAver = reader.GetInt32 (14);
                    dataset.TempMax = reader.GetInt32 (15);
                    dataset.TempMin = reader.GetInt32 (16);
                    dataset.TempAver = reader.GetInt32 (17);
                    dataset.Precipitation = reader.GetInt32 (18);
                    dataset.DatasetTags = reader.GetInt32 (19);
                    }
                await cnn.CloseAsync ();
                return dataset;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new Dataset ();
                }
            }
        public async Task<bool> Update_DatasetAsync (Dataset dataset)
            {
            string sql = "DT.Update_Dataset";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", dataset.DatasetId);
                cmd.Parameters.AddWithValue ("@UserId", dataset.UserId);
                cmd.Parameters.AddWithValue ("@DatasetType", dataset.DatasetType);
                cmd.Parameters.AddWithValue ("@DatasetAbbr", dataset.DatasetAbbr);
                cmd.Parameters.AddWithValue ("@DatasetName", dataset.DatasetName);
                cmd.Parameters.AddWithValue ("@DatasetRef", dataset.DatasetRef);
                cmd.Parameters.AddWithValue ("@RefYear", dataset.RefYear);
                cmd.Parameters.AddWithValue ("@RefLang", dataset.RefLang);
                cmd.Parameters.AddWithValue ("@DatasetManager", dataset.DatasetManager);
                cmd.Parameters.AddWithValue ("@AreaHa", dataset.AreaHa);
                cmd.Parameters.AddWithValue ("@ProvinceId", dataset.ProvinceId);
                cmd.Parameters.AddWithValue ("@Coords", dataset.Coords);
                cmd.Parameters.AddWithValue ("@AltMax", dataset.AltMax);
                cmd.Parameters.AddWithValue ("@AltMin", dataset.AltMin);
                cmd.Parameters.AddWithValue ("@AltAver", dataset.AltAver);
                cmd.Parameters.AddWithValue ("@TempMax", dataset.TempMax);
                cmd.Parameters.AddWithValue ("@TempMin", dataset.TempMin);
                cmd.Parameters.AddWithValue ("@TempAver", dataset.TempAver);
                cmd.Parameters.AddWithValue ("@Precipitation", dataset.Precipitation);
                cmd.Parameters.AddWithValue ("@DatasetTags", dataset.DatasetTags);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<bool> Delete_DatasetAsync (int DatasetId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Delete_Dataset";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", DatasetId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        #endregion
        #region DATASETDATA
        public async Task<bool> Create_DatasetDataAsync (DatasetData datasetData)
            {
            string sql = "DT.Create_DatasetData";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", datasetData.DatasetId);
                cmd.Parameters.AddWithValue ("@SpeciesId", datasetData.SpeciesId);
                cmd.Parameters.AddWithValue ("@Infraspecific", datasetData.Infraspecific);
                cmd.Parameters.AddWithValue ("@LineString", datasetData.LineString);
                cmd.Parameters.AddWithValue ("@ParsedGenus", datasetData.ParsedGenus);
                cmd.Parameters.AddWithValue ("@ParsedEpithet", datasetData.ParsedEpithet);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<DatasetData>> Read_DatasetDataAsync (int datasetId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Read_DatasetData";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", datasetId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<DatasetData> lstDatasetData = new List<DatasetData> ();
                while (await reader.ReadAsync ())
                    {
                    lstDatasetData.Add (new DatasetData
                        {
                        DatasetDataId = reader.GetInt32 (0),
                        DatasetId = reader.GetInt32 (1),
                        SpeciesId = reader.GetString (2),
                        Infraspecific = reader.GetString (3),
                        LineString = reader.GetString (4),
                        ParsedGenus = reader.GetString (5),
                        ParsedEpithet = reader.GetString (6),
                        SciName = reader.GetString (7)
                        });
                    }
                await cnn.CloseAsync ();
                return lstDatasetData;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<DatasetData> ();
                }
            }
        public async Task<bool> Update_DatasetDataAsync (DatasetData datasetData)
            {
            string sql = "DT.Update_DatasetData";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetDataId", datasetData.DatasetDataId);
                cmd.Parameters.AddWithValue ("@DatasetId", datasetData.DatasetId);
                cmd.Parameters.AddWithValue ("@SpeciesId", datasetData.SpeciesId);
                cmd.Parameters.AddWithValue ("@Infraspecific", datasetData.Infraspecific);
                cmd.Parameters.AddWithValue ("@LineString", datasetData.LineString);
                cmd.Parameters.AddWithValue ("@ParsedGenus", datasetData.ParsedGenus);
                cmd.Parameters.AddWithValue ("@ParsedEpithet", datasetData.ParsedEpithet);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<bool> Delete_DatasetDataAsync (int datasetDataId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Delete_DatasetData";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetDataId", datasetDataId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        #endregion
        #region STATION
        public async Task<bool> Create_StationAsync (Station station)
            {
            string sql = "BS.Create_Station";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@StationCode", station.StationCode);
                cmd.Parameters.AddWithValue ("@StationName", station.StationName);
                cmd.Parameters.AddWithValue ("@StationAlt", station.StationAlt);
                cmd.Parameters.AddWithValue ("@StationCoords", station.StationCoords);

                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<Station>> Read_StationsAsync ()
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Read_Stations";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Station> lstStations = new List<Station> ();
                while (await reader.ReadAsync ())
                    {
                    lstStations.Add (new Station
                        {
                        StationId = reader.GetInt32 (0),
                        StationCode = reader.GetInt32 (1),
                        StationName = reader.GetString (2),
                        StationAlt = reader.GetInt32 (3),
                        StationCoords = reader.GetString (4)
                        });
                    }
                await cnn.CloseAsync ();
                return lstStations;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Station> ();
                }
            }
        public async Task<Station> Read_StationAsync (int stationId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Read_Datasets";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@StationId", stationId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                Station station = new Station ();
                while (await reader.ReadAsync ())
                    {
                    station.StationId = reader.GetInt32 (0);
                    station.StationCode = reader.GetInt32 (1);
                    station.StationName = reader.GetString (2);
                    station.StationAlt = reader.GetInt32 (3);
                    station.StationCoords = reader.GetString (4);
                    }
                await cnn.CloseAsync ();
                return station;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new Station ();
                }
            }
        public async Task<bool> Update_StationAsync (Station station)
            {
            string sql = "BS.Update_Station";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@StationId", station.StationId);
                cmd.Parameters.AddWithValue ("@StationCode", station.StationCode);
                cmd.Parameters.AddWithValue ("@StationName", station.StationName);
                cmd.Parameters.AddWithValue ("@StationAlt", station.StationAlt);
                cmd.Parameters.AddWithValue ("@StationCoords", station.StationCoords);

                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<bool> Delete_StationAsync (int stationId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Delete_Dataset";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@StationId", stationId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        #endregion
        #region STATIONDATA
        public async Task<bool> Create_StationDataAsync (StationData stationData)
            {
            string sql = "DT.Create_StationData";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@StationId", stationData.StationId);
                cmd.Parameters.AddWithValue ("@Yr", stationData.Yr);
                cmd.Parameters.AddWithValue ("@DataType", stationData.DataType);
                cmd.Parameters.AddWithValue ("@M01", stationData.M01);
                cmd.Parameters.AddWithValue ("@M02", stationData.M02);
                cmd.Parameters.AddWithValue ("@M03", stationData.M03);
                cmd.Parameters.AddWithValue ("@M04", stationData.M04);
                cmd.Parameters.AddWithValue ("@M05", stationData.M05);
                cmd.Parameters.AddWithValue ("@M06", stationData.M06);
                cmd.Parameters.AddWithValue ("@M07", stationData.M07);
                cmd.Parameters.AddWithValue ("@M08", stationData.M08);
                cmd.Parameters.AddWithValue ("@M09", stationData.M09);
                cmd.Parameters.AddWithValue ("@M10", stationData.M10);
                cmd.Parameters.AddWithValue ("@M11", stationData.M11);
                cmd.Parameters.AddWithValue ("@M12", stationData.M12);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<StationData>> Read_StationDataAsync (int stationId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Read_StationData";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", stationId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<StationData> lstStationData = new List<StationData> ();
                while (await reader.ReadAsync ())
                    {
                    lstStationData.Add (new StationData
                        {
                        StationDataId = reader.GetInt32 (0),
                        StationId = reader.GetInt32 (1),
                        Yr = reader.GetInt32 (2),
                        DataType = reader.GetString (3),
                        M01 = reader.GetInt32 (4),
                        M02 = reader.GetInt32 (5),
                        M03 = reader.GetInt32 (6),
                        M04 = reader.GetInt32 (7),
                        M05 = reader.GetInt32 (8),
                        M06 = reader.GetInt32 (9),
                        M07 = reader.GetInt32 (10),
                        M08 = reader.GetInt32 (11),
                        M09 = reader.GetInt32 (12),
                        M10 = reader.GetInt32 (13),
                        M11 = reader.GetInt32 (14),
                        M12 = reader.GetInt32 (15),
                        });
                    }
                await cnn.CloseAsync ();
                return lstStationData;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<StationData> ();
                }
            }
        public async Task<bool> Update_StationDataAsync (StationData stationData)
            {
            string sql = "BS.Update_StationData";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@StationDataId", stationData.StationDataId);
                cmd.Parameters.AddWithValue ("@StationId", stationData.StationId);
                cmd.Parameters.AddWithValue ("@Yr", stationData.Yr);
                cmd.Parameters.AddWithValue ("@DataType", stationData.DataType);
                cmd.Parameters.AddWithValue ("@M01", stationData.M01);
                cmd.Parameters.AddWithValue ("@M02", stationData.M02);
                cmd.Parameters.AddWithValue ("@M03", stationData.M03);
                cmd.Parameters.AddWithValue ("@M04", stationData.M04);
                cmd.Parameters.AddWithValue ("@M05", stationData.M05);
                cmd.Parameters.AddWithValue ("@M06", stationData.M06);
                cmd.Parameters.AddWithValue ("@M07", stationData.M07);
                cmd.Parameters.AddWithValue ("@M08", stationData.M08);
                cmd.Parameters.AddWithValue ("@M09", stationData.M09);
                cmd.Parameters.AddWithValue ("@M10", stationData.M10);
                cmd.Parameters.AddWithValue ("@M11", stationData.M11);
                cmd.Parameters.AddWithValue ("@M12", stationData.M12);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<bool> Delete_StationDataAsync (int stationDataId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Delete_StationData";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@StationDataId", stationDataId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        #endregion
        #region SET
        public async Task<bool> Create_SetAsync (Set set)
            {
            string sql = "DT.Create_Set";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@UserId", set.UserId);
                cmd.Parameters.AddWithValue ("@SetType", set.SetType);
                cmd.Parameters.AddWithValue ("@SetName", set.SetName);
                cmd.Parameters.AddWithValue ("@SetTags", set.SetTags);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<Set>> Read_SetsAsync (int userId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Read_Sets";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@UserId", userId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Set> lstSets = new List<Set> ();
                while (await reader.ReadAsync ())
                    {
                    lstSets.Add (new Set
                        {
                        SetId = reader.GetInt32 (0),
                        UserId = reader.GetInt32 (1),
                        SetType = reader.GetInt32 (2),
                        SetName = reader.GetString (3),
                        SetTags = reader.GetInt32 (4)
                        });
                    }
                await cnn.CloseAsync ();
                return lstSets;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Set> ();
                }
            }
        public async Task<Set> Read_SetAsync (int setId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Read_Set";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@SetId", setId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                Set set = new Set ();
                while (await reader.ReadAsync ())
                    {
                    set.SetId = reader.GetInt32 (0);
                    set.UserId = reader.GetInt32 (1);
                    set.SetType = reader.GetInt32 (2);
                    set.SetName = reader.GetString (3);
                    set.SetTags = reader.GetInt32 (4);
                    }
                await cnn.CloseAsync ();
                return set;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new Set ();
                }
            }
        public async Task<bool> Update_SetAsync (Set Set)
            {
            string sql = "DT.Update_Set";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@SetId", Set.SetId);
                cmd.Parameters.AddWithValue ("@UserId", Set.UserId);
                cmd.Parameters.AddWithValue ("@SetType", Set.SetType);
                cmd.Parameters.AddWithValue ("@SetName", Set.SetName);
                cmd.Parameters.AddWithValue ("@SetTags", Set.SetTags);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<bool> Delete_SetAsync (int setId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Delete_Set";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@SetId", setId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        #endregion
        #region SETDATA
        public async Task<bool> Create_SetDataAsync (SetData setData)
            {
            string sql = "DT.Create_SetData";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@SetId", setData.SetId);
                cmd.Parameters.AddWithValue ("@DatasetId", setData.DatasetId);
                cmd.Parameters.AddWithValue ("@Subsets", setData.Subsets);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<SetData>> Read_SetDataAsync (int setId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Read_SetData";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@SetId", setId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<SetData> lstSetData = new List<SetData> ();
                while (await reader.ReadAsync ())
                    {
                    lstSetData.Add (new SetData
                        {
                        SetDataId = reader.GetInt32 (0),
                        SetId = reader.GetInt32 (1),
                        DatasetId = reader.GetInt32 (2),
                        Subsets = reader.GetInt32 (3),
                        DatasetName = reader.GetString (4)
                        });
                    }
                await cnn.CloseAsync ();
                return lstSetData;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<SetData> ();
                }
            }
        public async Task<bool> Update_SetDataAsync (SetData setData)
            {
            string sql = "DT.Update_SetData";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@SetDataId", setData.SetDataId);
                cmd.Parameters.AddWithValue ("@SetId", setData.SetId);
                cmd.Parameters.AddWithValue ("@DatasetId", setData.DatasetId);
                cmd.Parameters.AddWithValue ("@Subsets", setData.Subsets);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch
                {
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<bool> Delete_SetDataAsync (int setDataId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Delete_SetData";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@SetDataId", setDataId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        #endregion
        #region NOTEBOOK
        public async Task<int> Create_NotebookAsync (Notebook notebook)
            {
            string sql = "DT.Create_Notebook";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new (connString);
            //Create
            await cnn.OpenAsync ();
            SqlCommand cmd2 = new SqlCommand (sql, cnn);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue ("@UserId", notebook.UserId);
            cmd2.Parameters.AddWithValue ("@NotebookName", notebook.NotebookName);
            cmd2.Parameters.AddWithValue ("@NotebookTags", notebook.NotebookTags);
            await cmd2.ExecuteNonQueryAsync ();
            await cnn.CloseAsync ();
            return 1;
            }
        public async Task<List<Notebook>> Read_NotebooksAsync (int userId)
            {
            List<Notebook> lstNotebooks = new List<Notebook> ();
            string sql = "DT.Read_Notebooks";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connString);
            await cnn.OpenAsync ();
            SqlCommand cmd = new SqlCommand (sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue ("@UserId", userId);
            SqlDataReader reader = await cmd.ExecuteReaderAsync ();
            lstNotebooks.Clear ();
            while (await reader.ReadAsync ())
                {
                lstNotebooks.Add (new Notebook
                    {
                    NotebookId = reader.GetInt32 (0),
                    UserId = reader.GetInt32 (1),
                    NotebookName = reader.GetString (2),
                    NotebookTags = reader.GetInt32 (3),
                    Sections = new List<Section> ()
                    });
                }
            await cnn.CloseAsync ();
            foreach (Notebook nbook in lstNotebooks)
                {
                nbook.Sections = await Read_SectionsAsync (nbook.NotebookId, false);
                }
            return lstNotebooks;
            }
        public async Task<Notebook> Read_NotebookAsync (int notebookId)
            {
            string sql = "DT.Read_Notebook";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connString);
            await cnn.OpenAsync ();
            SqlCommand cmd = new SqlCommand (sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue ("@NotebookId", notebookId);
            SqlDataReader reader = await cmd.ExecuteReaderAsync ();
            Notebook notebook = new Notebook ();
            while (await reader.ReadAsync ())
                {
                notebook.NotebookId = reader.GetInt32 (0);
                notebook.UserId = reader.GetInt32 (1);
                notebook.UserId = reader.GetInt32 (2);
                notebook.NotebookName = reader.GetString (3);
                notebook.NotebookTags = reader.GetInt32 (4);
                notebook.Sections = new List<Section> ();
                }
            await cnn.CloseAsync ();
            notebook.Sections = await Read_SectionsAsync (notebook.NotebookId, false);
            return notebook;
            }
        public async Task<bool> Update_NotebookAsync (Notebook notebook)
            {
            string sql = "DT.Update_Notebook";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new (connString);
            await cnn.OpenAsync ();
            SqlCommand cmd = new SqlCommand (sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue ("@NotebookId", notebook.NotebookId);
            cmd.Parameters.AddWithValue ("@UserId", notebook.UserId);
            cmd.Parameters.AddWithValue ("@NotebookName", notebook.NotebookName);
            cmd.Parameters.AddWithValue ("@NotebookTags", notebook.NotebookTags);
            await cmd.ExecuteNonQueryAsync ();
            await cnn.CloseAsync ();
            return true;
            }
        public async Task<bool> Delete_NotebookAsync (int notebookId)
            {
            string sql = @"DT.Delete_Notebook";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new (connString);
            await cnn.OpenAsync ();
            SqlCommand cmd = new SqlCommand (sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue ("@NotebookId", notebookId);
            await cmd.ExecuteNonQueryAsync ();
            await cnn.CloseAsync ();
            return true;
            }
        #endregion
        #region SECTION
        public async Task<int> Create_SectionAsync (Section section)
            {
            string sql = "DT.Create_Section";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new (connString);
            await cnn.OpenAsync ();
            SqlCommand cmd2 = new SqlCommand (sql, cnn);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue ("@NotebookId", section.NotebookId);
            cmd2.Parameters.AddWithValue ("@SectionName", section.SectionName);
            cmd2.Parameters.AddWithValue ("@SectionTags", section.SectionTags);
            await cmd2.ExecuteNonQueryAsync ();
            await cnn.CloseAsync ();
            return 1;
            }
        public async Task<List<Section>> Read_SectionsAsync (int notebookId, bool readNotes)
            {
            List<Section> lstSections = new List<Section> ();
            string sql = "DT.Read_Sections";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connString);
            await cnn.OpenAsync ();
            SqlCommand cmd = new SqlCommand (sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue ("@NotebookId", notebookId);
            SqlDataReader reader = await cmd.ExecuteReaderAsync ();
            lstSections.Clear ();
            while (await reader.ReadAsync ())
                {
                lstSections.Add (new Section
                    {
                    SectionId = reader.GetInt32 (0),
                    NotebookId = reader.GetInt32 (1),
                    SectionName = reader.GetString (2),
                    Notes = new List<Note> ()
                    });
                }
            await cnn.CloseAsync ();
            if (readNotes)
                {
                foreach (Section sect in lstSections)
                    {
                    sect.Notes = await Read_NotesAsync (12, sect.SectionId);
                    }
                }
            return lstSections;
            }
        public async Task<Section> Read_SectionAsync (int sectionId, bool readNotes)
            {
            string sql = "DT.Read_Section";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connString);
            await cnn.OpenAsync ();
            SqlCommand cmd = new SqlCommand (sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue ("@SectionId", sectionId);
            SqlDataReader reader = await cmd.ExecuteReaderAsync ();
            Section section = new Section ();
            while (await reader.ReadAsync ())
                {
                section.SectionId = reader.GetInt32 (0);
                section.NotebookId = reader.GetInt32 (1);
                section.SectionName = reader.GetString (2);
                section.Notes = new List<Note> ();
                }
            await cnn.CloseAsync ();
            if (readNotes)
                {
                section.Notes = await Read_NotesAsync (13, section.SectionId);
                }
            return section;
            }
        public async Task<bool> Update_SectionAsync (Section section)
            {
            string sql = "DT.Update_Section";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new (connString);
            await cnn.OpenAsync ();
            SqlCommand cmd = new SqlCommand (sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue ("@SectionId", section.SectionId);
            cmd.Parameters.AddWithValue ("@NotebookId", section.NotebookId);
            cmd.Parameters.AddWithValue ("@SectionName", section.SectionName);
            cmd.Parameters.AddWithValue ("@SectionTags", section.SectionTags);
            await cmd.ExecuteNonQueryAsync ();
            await cnn.CloseAsync ();
            return true;
            }
        public async Task<bool> Update_SectionParentAsync (Section section)
            {
            string sql = "DT.Update_SectionParent";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new (connString);
            await cnn.OpenAsync ();
            SqlCommand cmd = new SqlCommand (sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue ("@SectionId", section.SectionId);
            cmd.Parameters.AddWithValue ("@NotebookId", section.NotebookId);
            await cmd.ExecuteNonQueryAsync ();
            await cnn.CloseAsync ();
            return true;
            }
        public async Task<bool> Delete_SectionAsync (int subProjectId)
            {
            string sql = "dbo.sp_DeleteSubproject";
            string? connString = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connString);
            await cnn.OpenAsync ();
            SqlCommand cmd = new SqlCommand (sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue ("@subprojectid", subProjectId);
            int i = await cmd.ExecuteNonQueryAsync ();
            await cnn.CloseAsync ();
            return true;
            }
        #endregion
        #region NOTE
        public async Task<bool> Create_NoteAsync (Note note)
            {
            string sql = "DT.Create_Note";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@UserId", note.UserId);
                cmd.Parameters.AddWithValue ("@RefType", note.RefType);
                cmd.Parameters.AddWithValue ("@RefId", note.RefId);
                cmd.Parameters.AddWithValue ("@NoteDateTime", note.NoteDateTime);
                cmd.Parameters.AddWithValue ("@NoteDateTimeDue", (object) note.NoteDateTimeDue ?? DBNull.Value); //using DBNull instead of null
                cmd.Parameters.AddWithValue ("@NoteText", note.NoteText);
                cmd.Parameters.AddWithValue ("@NoteTags", note.NoteTags);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ($"BE Error Create Note : ============== \n {ex}");
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<Note>> Read_NotesAsync (int refType, int refId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Read_Notes";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@RefType", refType);
                cmd.Parameters.AddWithValue ("@RefId", refId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Note> lstNotes = new List<Note> ();
                while (await reader.ReadAsync ())
                    {
                    lstNotes.Add (new Note
                        {
                        NoteId = reader.GetInt32 (0),
                        UserId = reader.GetInt32 (1),
                        RefType = reader.GetInt32 (2),
                        RefId = reader.GetInt32 (3),
                        NoteDateTime = reader.GetDateTimeOffset (4),
                        NoteDateTimeDue = reader.GetDateTimeOffset (5),
                        NoteText = reader.GetString (6),
                        NoteTags = reader.GetInt32 (7),
                        });
                    }
                await cnn.CloseAsync ();
                return lstNotes;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Note> ();
                }
            }
        public async Task<List<Note>> Read_NotesDueAsync (int userId, int nDays)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Read_NotesDue";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@UserId", userId);
                cmd.Parameters.AddWithValue ("@Days", nDays);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<Note> lstNotes = new List<Note> ();
                while (await reader.ReadAsync ())
                    {
                    lstNotes.Add (new Note
                        {
                        NoteId = reader.GetInt32 (0),
                        UserId = reader.GetInt32 (1),
                        RefType = reader.GetInt32 (2),
                        RefId = reader.GetInt32 (3),
                        NoteDateTime = reader.GetDateTimeOffset (4),
                        NoteDateTimeDue = reader.GetDateTimeOffset (5),
                        NoteText = reader.GetString (6),
                        NoteTags = reader.GetInt32 (7),
                        });
                    }
                await cnn.CloseAsync ();
                return lstNotes;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<Note> ();
                }
            }
        public async Task<Note> Read_NoteAsync (int noteId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Read_Note";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@NoteId", noteId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                Note note = new Note ();
                while (await reader.ReadAsync ())
                    {
                    note.NoteId = reader.GetInt32 (0);
                    note.UserId = reader.GetInt32 (1);
                    note.RefType = reader.GetInt32 (2);
                    note.RefId = reader.GetInt32 (3);
                    note.NoteDateTime = reader.GetDateTimeOffset (4);
                    note.NoteDateTimeDue = reader.GetDateTimeOffset (5);
                    note.NoteText = reader.GetString (6);
                    note.NoteTags = reader.GetInt32 (7);
                    }
                await cnn.CloseAsync ();
                return note;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new Note ();
                }
            }
        public async Task<bool> Update_NoteAsync (Note note)
            {
            string sql = "DT.Update_Note";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@NoteId", note.NoteId);
                cmd.Parameters.AddWithValue ("@UserId", note.UserId);
                cmd.Parameters.AddWithValue ("@RefType", note.RefType);
                cmd.Parameters.AddWithValue ("@RefId", note.RefId);
                cmd.Parameters.AddWithValue ("@NoteDateTime", note.NoteDateTime);
                cmd.Parameters.AddWithValue ("@NoteDateTimeDue", (object) note.NoteDateTimeDue ?? DBNull.Value); //using DBNull instead of null
                cmd.Parameters.AddWithValue ("@NoteText", note.NoteText);
                cmd.Parameters.AddWithValue ("@NoteTags", note.NoteTags);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ($"BE error ========== \n {ex}");
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<bool> Delete_NotesAsync (int refType, int refId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Delete_Note";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@RefType", refType);
                cmd.Parameters.AddWithValue ("@RefId", refId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        public async Task<bool> Delete_NoteAsync (int noteId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Delete_Note";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@NoteId", noteId);
                var i = await cmd.ExecuteNonQueryAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return false;
                }
            }
        #endregion
        #region ANALYSIS
        public async Task<List<Diversity>> Calc_DiversityAsync (int setId, int subsetIndex)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "Analyses.Calc_Diversity";
            using SqlConnection cnn = new (connString);
            await cnn.OpenAsync ();
            SqlCommand cmd = new (sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue ("@SetId", setId);
            cmd.Parameters.AddWithValue ("@SubsetIndex", subsetIndex);
            SqlDataReader reader = await cmd.ExecuteReaderAsync ();
            List<Diversity> lstDiversities = new List<Diversity> ();
            while (await reader.ReadAsync ())
                {
                lstDiversities.Add (new Diversity
                    {
                    DatasetIdA = reader.GetInt32 (0),
                    DatasetIdB = reader.GetInt32 (1),
                    DatasetNameA = reader.GetString (2),
                    DatasetNameB = reader.GetString (3),
                    CountA = reader.GetInt32 (4),
                    CountB = reader.GetInt32 (5),
                    SharedSpecies = reader.GetInt32 (6),
                    BetaDiv = reader.GetInt32 (7),
                    DissimS = reader.GetDecimal (8),
                    DissimJ = reader.GetDecimal (9)
                    });
                }
            await cnn.CloseAsync ();
            return lstDiversities;
            }
        /*
        public static void SetsFValue (int intSetID, string strSetTitle, int intSubset)
            {
            FileSystem.FileClose ();
            string fltr = "";
            switch (intSubset)
                {
                case 0:
                        {
                        fltr = "";
                        break;
                        }
                default:
                        {
                        fltr = " AND (Membership & " + Math.Pow (2, intSubset - 1).ToString () + " = " + Math.Pow (2, intSubset - 1).ToString () + ")";
                        break;
                        }
                }
            //get data form 5 tables
            DB.DS.Tables ["tblTMP0"].Clear ();
            string strSQL = "SELECT DISTINCT Species.ID AS SPID, Genera.familyID, Species.genusID, Species.rstat, Species.rcons, Species.rchar, Setdata_DS.setID, Setdata_DS.membership";
            strSQL = strSQL + " FROM Species INNER JOIN Genera ON Species.genusID = Genera.ID INNER JOIN DsData ON Species.ID = DsData.speciesID INNER JOIN Datasets ON Datasets.ID = DsData.datasetID INNER JOIN Setdata_DS ON Datasets.ID = Setdata_DS.datasetID";
            strSQL = strSQL + " WHERE SetID = " + intSetID.ToString () + " AND rank = 'species' " + fltr;
            using (var CnnSS = new System.Data.SqlClient.SqlConnection (DB.CnnString))
                {
                CnnSS.Open ();
                DB.DASS = new System.Data.SqlClient.SqlDataAdapter (strSQL, CnnSS);
                DB.DASS.Fill (DB.DS, "tblTMP0");
                CnnSS.Close ();
                }
            DataTable dt = DB.DS.Tables ["tblTMP0"];
            //LINQ
            int nFam = (from r in dt.AsEnumerable () select r.Field<int> ("familyID")).Distinct ().Count ();
            int nGen = (from r in dt.AsEnumerable () select r.Field<int> ("genusID")).Distinct ().Count ();
            int nSp = (from r in dt.AsEnumerable () select r.Field<int> ("SPID")).Distinct ().Count ();
            int nEndem = (from r in dt.AsEnumerable () where ((r.Field<int> ("rchar") & 1) == 1) select r.Field<int> ("rchar")).Count ();
            int nRare = (from r in dt.AsEnumerable () where ((r.Field<int> ("rchar") & 2) == 2) select r.Field<int> ("rchar")).Count ();
            int nMed = (from r in dt.AsEnumerable () where ((r.Field<int> ("rchar") & 4) == 4) select r.Field<int> ("rchar")).Count ();
            int nRange = (from r in dt.AsEnumerable () where ((r.Field<int> ("rchar") & 8) == 8) select r.Field<int> ("rchar")).Count ();
            int nCR = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 64) == 64) select (int) r.Field<byte> ("rcons")).Count ();
            int nEN = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 32) == 32) select (int) r.Field<byte> ("rcons")).Count ();
            int nVU = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 16) == 16) select (int) r.Field<byte> ("rcons")).Count ();
            int nNT = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 8) == 8) select (int) r.Field<byte> ("rcons")).Count ();
            int nLC = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 4) == 4) select (int) r.Field<byte> ("rcons")).Count ();
            int nLR = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 2) == 2) select (int) r.Field<byte> ("rcons")).Count ();
            int nDD = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 1) == 1) select (int) r.Field<byte> ("rcons")).Count ();
            int nNE = (from r in dt.AsEnumerable () where (r.Field<byte> ("rcons") == 0) select (int) r.Field<byte> ("rcons")).Count ();
            int fv1 = nCR * 1024 + nEN * 512 + nVU * 256 + nNT * 128 + nLC * 64 + nLR * 32 + nEndem * 16 + nRare * 8 + nMed * 4 + nRange * 2 + nSp;
            if (nSp == 0)
                return;
            float fv2 = (float) ((100 * fv1 / nSp) / 100.0);
            //REPORT
            //1: Header 
            FileSystem.FileOpen (1, Application.StartupPath + "alamut_ReportDS.html", OpenMode.Output);
            FileSystem.PrintLine (1, "<html dir= \"ltr\">");
            FileSystem.PrintLine (1, "<head>");
            FileSystem.PrintLine (1, "<title>alamut Set ", intSetID, "</title>");
            FileSystem.PrintLine (1, Reports.Style);
            FileSystem.PrintLine (1, "</head>");
            FileSystem.PrintLine (1, Reports.Header);
            FileSystem.PrintLine (1, "<body>");
            FileSystem.PrintLine (1, "<h3 style='color:green; font-family:tahoma; text-align: center'>alamut floristic database- version: " + DB.BeVersion + "</h3>");
            FileSystem.PrintLine (1, "<p style='color:darkgray; font-family:tahoma; font-size:16px; text-align: center'>User: ", User.Name, "</p>");
            //2: header for set
            FileSystem.PrintLine (1, "<p style='color:blue; font-family:tahoma'>", DateTime.Now, "<br>set: ", intSetID, "<br>", strSetTitle, "<br>subset: ", intSubset, "</p>");
            FileSystem.PrintLine (1, "<hr>");
            //3: body
            FileSystem.PrintLine (1, "<H2 style='color:royalblue; font-family:tahoma; font-size:18px'>Floristic Value (FV): </H2>");
            //tables
            FileSystem.PrintLine (1, "<style>th, td {border: 1px solid black; border-collapse: collapse;}</style>");
            FileSystem.PrintLine (1, "<table style='width:50%; font-family:tahoma; font-size:12px; border-collapse:collapse'>");
            //table-1
            string thisLine = "";
            thisLine += "<tr><td><b>nfam</b></td><td>" + nFam + "</td><td><b>s/f</b></td><td>" + (float) ((100 * nSp / nFam) / 100.0) + "</td><td><b>fv </b></td><td>" + fv1 + "</td></tr>"; //tbl1-row1
            thisLine += "<tr><td><b>ngen</b></td><td>" + nGen + "</td><td><b>g/f</b></td><td>" + (float) ((100 * nGen / nFam) / 100.0) + "</td><td><b>fv'</b></td><td>" + fv2 + "</td></tr>"; //tbl1-row2
            thisLine += "<tr><td><b>nspc</b></td><td>" + nSp + "</td><td><b>s/g</b></td><td>" + (float) ((100 * nSp / nGen) / 100.0) + "</td><td>   </td><td>           </td></tr>";        //tbl1-row2
            FileSystem.PrintLine (1, thisLine);
            FileSystem.PrintLine (1, "</table>");
            FileSystem.PrintLine (1, "<br><br>");
            //table1-footer
            FileSystem.PrintLine (1, "<p>");
            FileSystem.PrintLine (1, "<b>nfam</b> : number of families<br>");
            FileSystem.PrintLine (1, "<b>ngen</b> : nmber of genera   <br>");
            FileSystem.PrintLine (1, "<b>nspc</b> : nmber of species  <br>");
            FileSystem.PrintLine (1, "<b>s/f</b> : species to family ratio<br>");
            FileSystem.PrintLine (1, "<b>g/f</b> : genus to family ratio  <br>");
            FileSystem.PrintLine (1, "<b>s/g</b> : species to genus ratio <br></p>");
            FileSystem.PrintLine (1, "<br><hr><br>");
            //table-2
            FileSystem.PrintLine (1, "<table style='width:50%; font-family:tahoma; font-size:12px; border-collapse:collapse'>");
            thisLine = "";
            thisLine += "<tr><th> x</th><th>criterion </th><th>v<sub>x</sub> = 2<sup>(x-1)</sup></th><th>nsp<sub>(x)</sub></th><th>nsp<sub>(x)</sub> . 2<sup>(x-1)</sup>   </th></tr>";  //tbl2-row1
            thisLine += "<tr><td>11</td><td>CR        </td><td>1024                  </td><td>" + nCR + "</td><td>" + nCR * 1024 + "</td></tr>";  //tbl2-row1
            thisLine += "<tr><td>10</td><td>EN        </td><td>0512                  </td><td>" + nEN + "</td><td>" + nEN * 512 + "</td></tr>";  //tbl2-row2
            thisLine += "<tr><td>09</td><td>VU        </td><td>0256                  </td><td>" + nVU + "</td><td>" + nVU * 256 + "</td></tr>";  //tbl2-row3
            thisLine += "<tr><td>08</td><td>NT        </td><td>0128                  </td><td>" + nNT + "</td><td>" + nNT * 128 + "</td></tr>";  //tbl2-row4
            thisLine += "<tr><td>07</td><td>LC        </td><td>0064                  </td><td>" + nLC + "</td><td>" + nLC * 64 + "</td></tr>";  //tbl2-row5
            thisLine += "<tr><td>06</td><td>LR        </td><td>0032                  </td><td>" + nLR + "</td><td>" + nLR * 32 + "</td></tr>";  //tbl2-row6
            thisLine += "<tr><td>05</td><td>Endem     </td><td>0016                  </td><td>" + nEndem + "</td><td>" + nEndem * 16 + "</td></tr>";  //tbl2-row7
            thisLine += "<tr><td>04</td><td>Rare      </td><td>0008                  </td><td>" + nRare + "</td><td>" + nRare * 8 + "</td></tr>";  //tbl2-row8
            thisLine += "<tr><td>03</td><td>Med       </td><td>0004                  </td><td>" + nMed + "</td><td>" + nMed * 4 + "</td></tr>";  //tbl2-row9
            thisLine += "<tr><td>02</td><td>Range     </td><td>0002                  </td><td>" + nRange + "</td><td>" + nRange * 2 + "</td></tr>";  //tbl2-row10
            thisLine += "<tr><td>01</td><td>Occurrence</td><td>0001                  </td><td>" + nSp + "</td><td>" + nSp * 1 + "</td></tr>";  //tbl2-row11
            thisLine += "<tr><td>  </td><td>          </td><td>                      </td><td><b>fv        </b></td><td><b>" + fv1 + "</b></td></tr>";  //tbl2-row13
            thisLine += "<tr><td>  </td><td>          </td><td>                      </td><td><b>fv'       </b></td><td><b>" + fv2 + "</b></td></tr>";  //tbl2-row14
            FileSystem.PrintLine (1, thisLine);
            FileSystem.PrintLine (1, "</table>");
            FileSystem.PrintLine (1, "<br><hr><br>");
            //table2-footer
            FileSystem.PrintLine (1, "<p>");
            FileSystem.PrintLine (1, "<b>v<sub>x</sub></b> = 2<sup>(x-1)</sup> : value of character (x) for species (i)<br>");
            FileSystem.PrintLine (1, "<b>fv<sub>i</sub></b> = SIGMA (v<sub>(x, i)</sub>), x = 1; x <= nx<br>");
            FileSystem.PrintLine (1, "<b>fv</b> = SIGMA (fv<sub>i</sub>), i = 1; i <= nsp<br>");
            FileSystem.PrintLine (1, "<b>fv</b> = SIGMA (SIGMA (v<sub>(x, i)</sub>), x = 1; x <= nx), i = 1; i <= nsp<br>");
            FileSystem.PrintLine (1, "<b>fv</b> = SIGMA (nsp (x) . 2 <sup>(x – 1)</sup>), x = 1; x <= nx<br>");
            FileSystem.PrintLine (1, "<b>fv'</b> = fv / nsp<br><br>");
            FileSystem.PrintLine (1, "<b><i>where</i></b></b><br>");
            FileSystem.PrintLine (1, "<b>nsp</b> = total number of species (alpha diversity)<br>");
            FileSystem.PrintLine (1, "<b>i</b> = species (i) in the floristic list<br>");
            FileSystem.PrintLine (1, "<b>fv<sub>i</sub></b> = species floristic-value<br>");
            FileSystem.PrintLine (1, "<b>n<sub>x</sub></b> = 11 (characteristics table rows count)<br>");
            FileSystem.PrintLine (1, "<b>x</b> = position of the characteristics in the cTable<br>");
            FileSystem.PrintLine (1, "<b>v<sub>(x,i)</sub></b> = value of characteristics (x) for species (i)<br>");
            FileSystem.PrintLine (1, "<b>nsp<sub>(x)</sub></b> = number of species with characteristic (x)<br>");
            FileSystem.PrintLine (1, "<br>");
            FileSystem.PrintLine (1, "Characteristics 2 to 5 : independent<br>");
            FileSystem.PrintLine (1, "characteristics 6 to 11 : an option group<br>");
            FileSystem.PrintLine (1, "species value (fv<sub>i</sub>) varies from <b>1</b> to <b>1055</b><br></p>");
            //4: Footer
            FileSystem.PrintLine (1, "<p></p>");
            FileSystem.PrintLine (1, "<hr>");
            FileSystem.PrintLine (1, "<H2 style='color:royalblue; font-family:tahoma; font-size:18px'>Reference </H2>");
            FileSystem.PrintLine (1, "<p>alamut floristic database by Dr. Majid Sharifi-Tehrani, 2024 <br>");
            FileSystem.PrintLine (1, "<b>www.msht.ir </b><br>");
            FileSystem.PrintLine (1, "msht.ir@gmail.com <br>");
            FileSystem.PrintLine (1, "sharifi-m@sku.ac.ir <br>");
            FileSystem.PrintLine (1, "+989133112733 </p>");
            //
            FileSystem.PrintLine (1, "<p style='font-family:tahoma; font-size:12px'><br></p><br>");
            FileSystem.PrintLine (1, Reports.Footer);
            FileSystem.PrintLine (1, "</body></html>");
            FileSystem.FileClose (1);
            //5: Show the Report
            Interaction.Shell ("explorer.exe " + Application.StartupPath + "alamut_ReportDS.html");


            }
        public static void SetsFlorAn (int intSetID, string strSetTitle, int intSubset, int intParaams)
            {
            FileSystem.FileClose ();
            string fltr = "";
            switch (intSubset)
                {
                case 0:
                        {
                        fltr = "";
                        break;
                        }
                default:
                        {
                        fltr = " AND (Membership & " + Math.Pow (2, intSubset - 1).ToString () + " = " + Math.Pow (2, intSubset - 1).ToString () + ")";
                        break;
                        }
                }
            DB.DS.Tables ["tblTMP0"].Clear ();
            -- get data form 5 tables
             * ----------------------
             * SELECT DISTINCT
             * 0 .accID      from  Specie     
             * 1 .familyID   from  Genera     
             * 2 .familyName from  Families    
             * 3 .genusID    from  Species    
             * 4 .genusName  from  Genera     
             * 5 .rstat      from  Species    
             * 6 .rcons      from  Species    
             * 7 .rchar      from  Species    
             * 8 .setID      from  Setdata_DS 
             * 9 .membership from  Setdata_DS 
            --
            string strSQL = "SELECT DISTINCT Species.ID AS SPID, Genera.familyID, Families.familyName, Species.genusID, Genera.genusName, Species.rstat, Species.rcons, Species.rchar, Species.CT, Species.LF, Setdata_DS.setID, Setdata_DS.membership";
            strSQL = strSQL + " FROM Families INNER JOIN Genera ON Families.ID = Genera.familyID INNER JOIN Species ON Genera.ID = Species.genusID INNER JOIN DsData ON Species.ID = DsData.speciesID INNER JOIN Datasets ON DsData.datasetID = Datasets.ID INNER JOIN Setdata_DS ON Datasets.ID = Setdata_DS.datasetID";
            strSQL = strSQL + " WHERE SetID = " + intSetID.ToString () + " AND rank = 'species' " + fltr;
            using (var CnnSS = new System.Data.SqlClient.SqlConnection (DB.CnnString))
                {
                CnnSS.Open ();
                DB.DASS = new System.Data.SqlClient.SqlDataAdapter (strSQL, CnnSS);
                DB.DASS.Fill (DB.DS, "tblTMP0");
                CnnSS.Close ();
                }
            DataTable dt = DB.DS.Tables ["tblTMP0"];
            //LINQ
            int nFam = (from r in dt.AsEnumerable () select r.Field<int> ("familyID")).Distinct ().Count ();
            int nGen = (from r in dt.AsEnumerable () select r.Field<int> ("genusID")).Distinct ().Count ();
            int nSp = (from r in dt.AsEnumerable () select r.Field<int> ("SPID")).Distinct ().Count ();
            int nEndem = (from r in dt.AsEnumerable () where ((r.Field<int> ("rchar") & 1) == 1) select r.Field<int> ("rchar")).Count ();
            int nRare = (from r in dt.AsEnumerable () where ((r.Field<int> ("rchar") & 2) == 2) select r.Field<int> ("rchar")).Count ();
            int nMed = (from r in dt.AsEnumerable () where ((r.Field<int> ("rchar") & 4) == 4) select r.Field<int> ("rchar")).Count ();
            int nRange = (from r in dt.AsEnumerable () where ((r.Field<int> ("rchar") & 8) == 8) select r.Field<int> ("rchar")).Count ();
            int nCR = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 64) == 64) select (int) r.Field<byte> ("rcons")).Count ();
            int nEN = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 32) == 32) select (int) r.Field<byte> ("rcons")).Count ();
            int nVU = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 16) == 16) select (int) r.Field<byte> ("rcons")).Count ();
            int nNT = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 8) == 8) select (int) r.Field<byte> ("rcons")).Count ();
            int nLC = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 4) == 4) select (int) r.Field<byte> ("rcons")).Count ();
            int nLR = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 2) == 2) select (int) r.Field<byte> ("rcons")).Count ();
            int nDD = (from r in dt.AsEnumerable () where ((r.Field<byte> ("rcons") & 1) == 1) select (int) r.Field<byte> ("rcons")).Count ();
            int nNE = (from r in dt.AsEnumerable () where (r.Field<byte> ("rcons") == 0) select (int) r.Field<byte> ("rcons")).Count ();
            int fv1 = nCR * 1024 + nEN * 512 + nVU * 256 + nNT * 128 + nLC * 64 + nLR * 32 + nEndem * 16 + nRare * 8 + nMed * 4 + nRange * 2 + nSp;
            if (nSp == 0)
                return;
            float fv2 = (float) ((100 * fv1 / nSp) / 100.0);
            //create excel
            var XL = new Excel.Application ();
            var WB = XL.Workbooks.Add ("");
            for (int x = 1; x < 4; x++)
                WB.Application.Worksheets.Add ();
            for (int x = 1; x <= 4; x++)
                WB.Worksheets [x].Name = x.ToString ();
            var WS1 = WB.Worksheets [1]; //Fam --> nSp, nGen?, Gen --> nSp?, Top 10s
            var WS2 = WB.Worksheets [2]; //CT-LF chart 
            var WS3 = WB.Worksheets [3]; //FValue
            var WS4 = WB.Worksheets [4]; //Ref
                                         // ************************************************************************************************************* SHEET1
                                         //stats
            WS1.Cells [1, 1] = "alamut database v2";
            WS1.Cells [2, 1] = strSetTitle;
            WS1.Cells [3, 1] = "set id";
            WS1.Cells [3, 2] = intSetID;
            WS1.Cells [4, 1] = "subset (all, 1-8)";
            if (intSubset == 0)
                WS1.Cells [4, 2] = "all";
            else
                WS1.Cells [4, 2] = intSubset;
            WS1.Cells [5, 1] = "nfam";
            WS1.Cells [5, 2] = nFam.ToString ();
            WS1.Cells [6, 1] = "ngen";
            WS1.Cells [6, 2] = nGen;
            WS1.Cells [7, 1] = "nspec";
            WS1.Cells [7, 2] = nSp;
            WS1.Cells [5, 4] = "s/f";
            WS1.Cells [5, 5] = Convert.ToInt32 (100.0 * nSp / nFam) / 100.0;
            WS1.Cells [6, 4] = "g/f";
            WS1.Cells [6, 5] = Convert.ToInt32 (100.0 * nGen / nFam) / 100.0;
            WS1.Cells [7, 4] = "s/g";
            WS1.Cells [7, 5] = Convert.ToInt32 (100.0 * nSp / nGen) / 100.0;
            //header
            WS1.Cells [10, 1] = "family";
            WS1.Cells [10, 2] = "fam";
            WS1.Cells [10, 3] = "spp[i]";
            WS1.Cells [10, 4] = "spp[i]%";
            WS1.Cells [10, 6] = "fam";
            WS1.Cells [10, 7] = "gen[i]";
            WS1.Cells [10, 8] = "gen[i]%";
            WS1.Cells [10, 10] = "fam";
            WS1.Cells [10, 11] = "genus";
            WS1.Cells [10, 12] = "gen";
            WS1.Cells [10, 13] = "spp[i]";
            WS1.Cells [10, 14] = "spp[i]%";
            WS1.Cells [5, 2].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS1.Cells [6, 2].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS1.Cells [7, 2].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS1.Cells [5, 5].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS1.Cells [6, 5].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS1.Cells [7, 5].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS1.Cells [10, 1].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS1.Cells [10, 2].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS1.Cells [10, 3].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS1.Cells [10, 4].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS1.Cells [10, 6].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS1.Cells [10, 7].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS1.Cells [10, 8].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS1.Cells [10, 10].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS1.Cells [10, 11].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS1.Cells [10, 12].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS1.Cells [10, 13].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS1.Cells [10, 14].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);


            //LINQs
            //linq1
            var Sheet1Data1 = from r in dt.AsEnumerable ()
                              group r by r.Field<string> ("familyName") into g
                              orderby g.Count () descending
                              select new
                                  {
                                  f = g.Key,
                                  c = g.Count ()
                                  };
            //linq2
            var Sheet1Data2 = from r in dt.AsEnumerable ()
                              group r by r.Field<string> ("familyName") into g
                              let cnt = g.Select (x => x.Field<string> ("genusName")).Distinct ().Count ()
                              orderby cnt descending
                              select new
                                  {
                                  fam = g.Key,
                                  cnt //OR cnt = g.Select(x=> x.Field<string>("genusName")).Distinct().Count()
                                  };
            //linq3
            var Sheet1Data3 = from r in dt.AsEnumerable ()
                              group r by r.Field<string> ("genusName") into g
                              orderby g.Count () descending
                              select new
                                  {
                                  gen = g.Key,
                                  c = g.Count (),
                                  fam = from z in g select z.Field<string> ("familyName")
                                  };
            //OUTPUTs
            //output1.1
            int i1 = 0;
            foreach (var a in Sheet1Data1)
                {
                WS1.Cells [i1 + 11, 1] = a.f;
                WS1.Cells [i1 + 11, 2] = Strings.Left (a.f, 4);
                WS1.Cells [i1 + 11, 3] = a.c.ToString ();
                WS1.Cells [i1 + 11, 4] = (Convert.ToInt32 (1000.0 * a.c / nSp) / 10.0).ToString (".0") + "%";
                i1++;
                }
            //output1.2
            i1 = 0;
            foreach (var a in Sheet1Data2)
                {
                //WS2.Cells [i2 + 10, 6] = a.fam.ToList () [0];
                WS1.Cells [i1 + 11, 6] = Strings.Left (a.fam, 4);
                WS1.Cells [i1 + 11, 7] = a.cnt;
                WS1.Cells [i1 + 11, 8] = (Convert.ToInt32 (1000.0 * a.cnt / nSp) / 10.0).ToString (".0") + "%";
                i1++;
                }
            //output1.3
            i1 = 0;
            foreach (var a in Sheet1Data3)
                {
                WS1.Cells [i1 + 11, 10] = Strings.Left (a.fam.ToList () [0], 4);
                WS1.Cells [i1 + 11, 11] = a.gen.ToString ();
                WS1.Cells [i1 + 11, 12] = Strings.Left (a.gen.ToString (), 4);
                WS1.Cells [i1 + 11, 13] = a.c;
                WS1.Cells [i1 + 11, 14] = (Convert.ToInt32 (1000.0 * a.c / nSp) / 10.0).ToString (".0") + "%";
                i1++;
                }
            WS1.Columns.AutoFit ();
            //CHARTs
            //A) STAT graphs
            //chart1.stat1
            var charts1stat1 = WS1.ChartObjects () as ChartObjects;
            var chartObject1stat1 = charts1stat1.Add (650, 0, 200, 150);
            var chart1stat1 = chartObject1stat1.Chart;
            var range1stat1 = (Excel.Range) WS1.Range [WS1.Cells [5, 1], WS1.Cells [7, 2]];
            chart1stat1.ChartType = XlChartType.xlColumnClustered;
            chart1stat1.ChartWizard (Source: range1stat1, Title: "taxonomic diversity");
            chart1stat1.ApplyLayout (3);
            chart1stat1.HasTitle = false;
            chart1stat1.HasLegend = false;
            //chart1.stat2
            var charts1stat2 = WS1.ChartObjects () as ChartObjects;
            var chartObject1stat2 = charts1stat2.Add (850, 0, 200, 150);
            var chart1stat2 = chartObject1stat2.Chart;
            var range1stat2 = (Excel.Range) WS1.Range [WS1.Cells [5, 4], WS1.Cells [7, 5]];
            chart1stat2.ChartType = XlChartType.xlColumnClustered;
            chart1stat2.ChartWizard (Source: range1stat2, Title: "s/f/g");
            chart1stat2.ApplyLayout (3);
            chart1stat2.HasTitle = false;
            chart1stat2.HasLegend = false;
            //B) FlorAn Charts
            //TOP 10s
            //chart1.1x
            var charts11x = WS1.ChartObjects () as ChartObjects;
            var chartObject11x = charts11x.Add (650, 150, 400, 200);
            var chart11x = chartObject11x.Chart;
            var range11x = (Excel.Range) WS1.Range [WS1.Cells [11, 2], WS1.Cells [20, 3]];
            chart11x.ChartType = XlChartType.xlColumnClustered;
            chart11x.HasTitle = true;
            chart11x.ChartWizard (Source: range11x, Title: "species rich fams");
            chart11x.ApplyLayout (3);
            chart11x.HasLegend = false;
            //chart1.2x
            var charts12x = WS1.ChartObjects () as ChartObjects;
            var chartObject12x = charts12x.Add (650, 350, 400, 200);
            var chart12x = chartObject12x.Chart;
            var range12x = (Excel.Range) WS1.Range [WS1.Cells [11, 6], WS1.Cells [20, 7]];
            chart12x.ChartType = XlChartType.xlColumnClustered;
            chart12x.HasTitle = true;
            chart12x.ChartWizard (Source: range12x, Title: "generic rich fams");
            chart12x.ApplyLayout (3);
            chart12x.HasLegend = false;
            //chart1.3x
            var charts13x = WS1.ChartObjects () as ChartObjects;
            var chartObject13x = charts13x.Add (650, 550, 400, 200);
            var chart13x = chartObject13x.Chart;
            var range13x = (Excel.Range) WS1.Range [WS1.Cells [11, 12], WS1.Cells [20, 13]];
            chart13x.ChartType = XlChartType.xlColumnClustered;
            chart13x.HasTitle = true;
            chart13x.ChartWizard (Source: range13x, Title: "species rich genea");
            chart13x.ApplyLayout (3);
            chart13x.HasLegend = false;

            // ************************************************************************************************************* SHEET2
            // ************************************************************************************************************* SHEET2
            //CT-LF
            WS2.Cells [1, 1] = "ChoroYype (CT)";
            WS2.Cells [1, 2] = "CT count";
            WS2.Cells [1, 4] = "LifeForm (LF)";
            WS2.Cells [1, 5] = "LF count";
            WS2.Cells [1, 1].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS2.Cells [1, 2].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS2.Cells [1, 4].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS2.Cells [1, 5].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            //linq2.1 para CT
            var Sheet2Data = from r in dt.AsEnumerable ()
                             group r by r.Field<string> ("CT") into grpct
                             select new
                                 {
                                 ct = grpct.Key,
                                 ctcount = grpct.Key.Count ()
                                 };
            int i2 = 0;
            foreach (var a in Sheet2Data)
                {
                if (a.ct.ToString () != "")
                    {
                    WS2.Cells [i2 + 2, 1] = a.ct.ToString ();
                    WS2.Cells [i2 + 2, 2] = a.ctcount.ToString (); //OR: a.ct.ToString ().Count ();
                    i2++;
                    }
                }
            //linq2.2 para LF
            int nPh = (from r in dt.AsEnumerable () where (r.Field<string> ("LF") == "Ph") select r.Field<string> ("LF")).Count ();
            int nCh = (from r in dt.AsEnumerable () where (r.Field<string> ("LF") == "Ch") select r.Field<string> ("LF")).Count ();
            int nHe = (from r in dt.AsEnumerable () where (r.Field<string> ("LF") == "He") select r.Field<string> ("LF")).Count ();
            int nCr = (from r in dt.AsEnumerable () where (r.Field<string> ("LF") == "Cr") select r.Field<string> ("LF")).Count ();
            int nTh = (from r in dt.AsEnumerable () where (r.Field<string> ("LF") == "Th") select r.Field<string> ("LF")).Count ();
            int nNa = (from r in dt.AsEnumerable () where (r.Field<string> ("LF") == "Na") select r.Field<string> ("LF")).Count ();
            WS2.Cells [2, 4] = "Ph";
            WS2.Cells [2, 5] = nPh;
            WS2.Cells [3, 4] = "Ch";
            WS2.Cells [3, 5] = nCh;
            WS2.Cells [4, 4] = "He";
            WS2.Cells [4, 5] = nHe;
            WS2.Cells [5, 4] = "Cr";
            WS2.Cells [5, 5] = nCr;
            WS2.Cells [6, 4] = "Th";
            WS2.Cells [6, 5] = nTh;
            WS2.Cells [7, 4] = "Na";
            WS2.Cells [7, 5] = nNa;
            WS2.Columns.AutoFit ();
            //CHARTs
            //A) chart2.1 : CT
            var charts2ct1 = WS2.ChartObjects () as ChartObjects;
            var chartObject2ct1 = charts2ct1.Add (150, 100, 750, 200);
            var chart2ct1 = chartObject2ct1.Chart;
            var range2ct1 = (Excel.Range) WS2.Range [WS2.Cells [2, 1], WS2.Cells [i2 + 1, 2]]; //i2: number of ct cats
            chart2ct1.ChartType = XlChartType.xlColumnClustered;
            chart2ct1.ChartWizard (Source: range2ct1, Title: "Chorotypes");
            chart2ct1.ApplyLayout (3);
            chart2ct1.HasTitle = true;
            chart2ct1.HasLegend = false;
            //B) chart2.2 : LF
            var charts2ct2 = WS2.ChartObjects () as ChartObjects;
            var chartObject2ct2 = charts2ct2.Add (150, 300, 360, 200);
            var chart2ct2 = chartObject2ct2.Chart;
            var range2ct2 = (Excel.Range) WS2.Range [WS2.Cells [2, 4], WS2.Cells [7, 5]];
            chart2ct2.ChartType = XlChartType.xlColumnClustered;
            chart2ct2.ChartWizard (Source: range2ct2, Title: "Life forms");
            chart2ct2.ApplyLayout (3);
            chart2ct2.HasTitle = true;
            chart2ct2.HasLegend = false;
            // ************************************************************************************************************* SHEET3
            // ************************************************************************************************************* SHEET3
            // ************************************************************************************************************* SHEET3
            WS3.Cells [1, 1] = "FV";
            WS3.Cells [1, 2] = "Floristic Value";
            WS3.Cells [1, 6] = "dataset";
            WS3.Cells [2, 6] = strSetTitle;
            WS3.Cells [1, 8] = "sbset";
            if (intSubset == 0)
                WS3.Cells [2, 8] = "all";
            else
                WS3.Cells [2, 8] = intSubset;
            //CALCs
            //row
            WS3.Cells [3, 1] = "x";
            WS3.Cells [3, 2] = "Cat";
            WS3.Cells [3, 3] = "Nsp(x)";
            WS3.Cells [3, 4] = "C=10^(x-1)";
            WS3.Cells [3, 5] = "C.Nsp(x)";
            WS3.Cells [3, 1].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS3.Cells [3, 2].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS3.Cells [3, 3].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS3.Cells [3, 4].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            WS3.Cells [3, 5].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);
            //col1
            WS3.Cells [4, 1] = "11";
            WS3.Cells [5, 1] = "10";
            WS3.Cells [6, 1] = "9";
            WS3.Cells [7, 1] = "8";
            WS3.Cells [8, 1] = "7";
            WS3.Cells [9, 1] = "6";
            WS3.Cells [10, 1] = "5";
            WS3.Cells [11, 1] = "4";
            WS3.Cells [12, 1] = "3";
            WS3.Cells [13, 1] = "2";
            WS3.Cells [14, 1] = "1";
            //col2
            WS3.Cells [4, 2] = "CR";
            WS3.Cells [5, 2] = "EN";
            WS3.Cells [6, 2] = "VU";
            WS3.Cells [7, 2] = "NT";
            WS3.Cells [8, 2] = "LC";
            WS3.Cells [9, 2] = "LR";
            WS3.Cells [10, 2] = "Endem";
            WS3.Cells [11, 2] = "Rare";
            WS3.Cells [12, 2] = "Med";
            WS3.Cells [13, 2] = "Range";
            WS3.Cells [14, 2] = "Occurrence";
            //col3
            WS3.Cells [4, 3] = nCR;
            WS3.Cells [5, 3] = nEN;
            WS3.Cells [6, 3] = nVU;
            WS3.Cells [7, 3] = nNT;
            WS3.Cells [8, 3] = nLC;
            WS3.Cells [9, 3] = nLR;
            WS3.Cells [10, 3] = nEndem;
            WS3.Cells [11, 3] = nRare;
            WS3.Cells [12, 3] = nMed;
            WS3.Cells [13, 3] = nRange;
            WS3.Cells [14, 3] = nSp;
            //col4
            WS3.Cells [4, 4] = "1024";
            WS3.Cells [5, 4] = "512";
            WS3.Cells [6, 4] = "256";
            WS3.Cells [7, 4] = "128";
            WS3.Cells [8, 4] = "64";
            WS3.Cells [9, 4] = "32";
            WS3.Cells [10, 4] = "16";
            WS3.Cells [11, 4] = "8";
            WS3.Cells [12, 4] = "4";
            WS3.Cells [13, 4] = "2";
            WS3.Cells [14, 4] = "1";
            //col5
            WS3.Cells [4, 5] = 1024 * nCR;
            WS3.Cells [5, 5] = 512 * nEN;
            WS3.Cells [6, 5] = 256 * nVU;
            WS3.Cells [7, 5] = 128 * nNT;
            WS3.Cells [8, 5] = 64 * nLC;
            WS3.Cells [9, 5] = 32 * nLR;
            WS3.Cells [10, 5] = 16 * nEndem;
            WS3.Cells [11, 5] = 8 * nRare;
            WS3.Cells [12, 5] = 4 * nMed;
            WS3.Cells [13, 5] = 2 * nRange;
            WS3.Cells [14, 5] = 1 * nSp;
            //fv
            WS3.Cells [15, 4] = "fv";
            WS3.Cells [15, 5] = 1024 * nCR + 512 * nEN + 256 * nVU + 128 * nNT + 64 * nLC + 32 * nLR + 16 * nEndem + 8 * nRare + 4 * nMed + 2 * nRange + nSp;
            WS3.Cells [16, 4] = "fv'";
            WS3.Cells [16, 5] = Convert.ToInt32 (100.0 * (1024.0 * nCR + 512 * nEN + 256 * nVU + 128 * nNT + 64 * nLC + 32 * nLR + 16 * nEndem + 8 * nRare + 4 * nMed + 2 * nRange + nSp) / nSp) / 100.0;
            WS3.Cells [15, 5].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS3.Cells [16, 5].Font.Color = ColorTranslator.ToOle (Color.Crimson);

            //data for charts
            WS3.Cells [20, 2] = "cons";
            WS3.Cells [20, 3] = nCR + nEN + nVU + nNT + nLC + nLR;
            WS3.Cells [20, 4] = (Convert.ToInt32 ((nCR + nEN + nVU + nNT + nLC + nLR) * 10000 / (nSp * 1.0)) / 100.0) + "%";
            WS3.Cells [20, 4].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS3.Cells [21, 2] = "DD+NE";
            WS3.Cells [21, 3] = nSp - (nCR + nEN + nVU + nNT + nLC + nLR);
            WS3.Cells [22, 2] = "endem";
            WS3.Cells [22, 3] = nEndem;
            WS3.Cells [22, 4] = (Convert.ToInt32 (nEndem * 10000 / (nSp * 1.0)) / 100.0) + "%";
            WS3.Cells [22, 4].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS3.Cells [23, 2] = "non endem";
            WS3.Cells [23, 3] = nSp - nEndem;
            WS3.Cells [24, 2] = "rare";
            WS3.Cells [24, 3] = nRare;
            WS3.Cells [24, 4] = (Convert.ToInt32 (nRare * 10000 / (nSp * 1.0)) / 100.0) + "%";
            WS3.Cells [24, 4].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS3.Cells [25, 2] = "non rare";
            WS3.Cells [25, 3] = nSp - nRare;
            WS3.Cells [26, 2] = "med";
            WS3.Cells [26, 3] = nMed;
            WS3.Cells [26, 4] = (Convert.ToInt32 (nMed * 10000 / (nSp * 1.0)) / 100.0) + "%";
            WS3.Cells [26, 4].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS3.Cells [27, 2] = "non med";
            WS3.Cells [27, 3] = nSp - nMed;
            WS3.Columns.AutoFit ();
            //chart311:consPie:conservarion
            var charts311 = WS3.ChartObjects () as ChartObjects;
            var chartObject311 = charts311.Add (280, 50, 150, 150);
            var chart311 = chartObject311.Chart;
            var range311 = (Excel.Range) WS3.Range [WS3.Cells [20, 2], WS3.Cells [21, 3]];
            chart311.ChartType = XlChartType.xlDoughnut;
            chart311.HasTitle = false;
            chart311.ChartWizard (Source: range311, Title: "Cons");
            chart311.ApplyLayout (3);
            chart311.HasLegend = true;
            //chart312:consBar:conservarion
            var charts312 = WS3.ChartObjects () as ChartObjects;
            var chartObject312 = charts312.Add (430, 50, 300, 150);
            var chart312 = chartObject312.Chart;
            var range312 = (Excel.Range) WS3.Range [WS3.Cells [4, 2], WS3.Cells [9, 3]];
            chart312.ChartType = XlChartType.xlColumnClustered;
            chart312.HasTitle = false;
            chart312.ChartWizard (Source: range312, Title: "Cons");
            chart312.ApplyLayout (3);
            chart312.HasLegend = false;
            //chart32:consPie:endem
            var charts32 = WS3.ChartObjects () as ChartObjects;
            var chartObject32 = charts32.Add (280, 200, 150, 150);
            var chart32 = chartObject32.Chart;
            var range32 = (Excel.Range) WS3.Range [WS3.Cells [22, 2], WS3.Cells [23, 3]];
            chart32.ChartType = XlChartType.xlDoughnut;
            chart32.HasTitle = false;
            chart32.ChartWizard (Source: range32, Title: "Endem");
            chart32.ApplyLayout (3);
            chart32.HasLegend = true;
            //chart33:consPie:rare
            var charts33 = WS3.ChartObjects () as ChartObjects;
            var chartObject33 = charts33.Add (430, 200, 150, 150);
            var chart33 = chartObject33.Chart;
            var range33 = (Excel.Range) WS3.Range [WS3.Cells [24, 2], WS3.Cells [25, 3]];
            chart33.ChartType = XlChartType.xlDoughnut;
            chart33.HasTitle = false;
            chart33.ChartWizard (Source: range33, Title: "Rare");
            chart33.ApplyLayout (3);
            chart33.HasLegend = true;
            //chart34:consPie:med
            var charts34 = WS3.ChartObjects () as ChartObjects;
            var chartObject34 = charts34.Add (580, 200, 150, 150);
            var chart34 = chartObject34.Chart;
            var range34 = (Excel.Range) WS3.Range [WS3.Cells [26, 2], WS3.Cells [27, 3]];
            chart34.ChartType = XlChartType.xlDoughnut;
            chart34.HasTitle = false;
            chart34.ChartWizard (Source: range34, Title: "Med");
            chart34.ApplyLayout (3);
            chart34.HasLegend = true;
            // ************************************************************************************************************* SHEET4
            // ************************************************************************************************************* SHEET4
            // ************************************************************************************************************* SHEET4
            // ************************************************************************************************************* SHEET4
            WS4.Cells [1, 1] = "about";
            WS4.Cells [1, 2] = "alamut floristic database ver 2 - by dr. majid sharifitehrani";
            WS4.Cells [2, 1] = "backend";
            WS4.Cells [2, 2] = DB.BeVersion;
            WS4.Cells [3, 1] = "owner";
            WS4.Cells [3, 2] = Client.Owner;

            WS4.Cells [5, 1] = "author";
            WS4.Cells [5, 2] = "department of biology, faculty of science, university of shahrekord";
            WS4.Cells [6, 2] = "majid sharifitehrani";
            WS4.Cells [7, 2] = "systematic botany, phd";

            WS4.Cells [9, 1] = "citation:";
            WS4.Cells [9, 2] = "2013 Compilation of floristic and herbarium specimens data in Iran: proposal to data structure.";
            WS4.Cells [9, 2].Font.Color = ColorTranslator.ToOle (Color.DarkBlue);

            WS4.Cells [11, 1] = "resume";
            WS4.Cells [11, 2] = "https://msht.ir/msht/msht_00.html";
            WS4.Cells [12, 2] = "https://sku.ac.ir/en/DepartmentGroup/ProfessorForm.aspx?ID=143";
            WS4.Cells [13, 2] = "https://orcid.org/0000-0002-1590-7655";


            WS4.Cells [15, 1] = "contact";
            WS4.Cells [15, 2] = "msht.ir@gmail.com";
            WS4.Cells [16, 2] = "sharifi-m@sku.ac.ir";
            WS4.Cells [17, 2] = "+98 9133112733";
            WS4.Cells [18, 2] = "www.msht.ir";
            WS4.Cells [15, 2].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS4.Cells [16, 2].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS4.Cells [17, 2].Font.Color = ColorTranslator.ToOle (Color.Crimson);
            WS4.Cells [18, 2].Font.Color = ColorTranslator.ToOle (Color.Crimson);

            WS4.Cells [20, 1] = "papers";
            WS4.Cells [20, 2] = "2012 Development of a database for floristic studies in Iran. Proceedings of The 17th National & 5th International Conference of Biology of Iran. Kerman, Iran.";
            WS4.Cells [21, 2] = "2013 Compilation of floristic and herbarium specimens data in Iran: proposal to data structure.";
            WS4.Cells [22, 2] = "2014 Introduction of the new Program ‘CheckName’ with applications in integration, and increased precision and certitude of floristic inventories.";
            WS4.Cells [23, 2] = "2015 Step from alpha to beta in plant biological diversity  of Iran, Academy of Science, I.R.Iran";
            WS4.Cells [24, 2] = "2019 APL compiler and ListMaker program for Alamut floristic database";

            WS4.Cells [26, 1] = "case studies";
            WS4.Cells [26, 2] = "2015 Fritillaria (Liliaceae) in Iran- distribution and nomenclature";
            WS4.Cells [27, 2] = "2016 Floristic study of Sheet Mountain in Chaharmahal and Bakhtiari Province, Iran";
            WS4.Cells [28, 2] = "2016 Flora of Jahanbin mountain area- a contribution to Flora of the Central Zagros region of Iran";
            WS4.Cells [29, 2] = "2017 Plant species diversity in the Central Zagros Region of Iran";
            WS4.Cells [30, 2] = "2017 Plant species diversity in Jokhaneh plain and southern slope of the Nil Mt. in Kohgilouyeh va Boyerahmad Province (central Zagros region of Iran)";
            WS4.Cells [31, 2] = "2019 Study of vascular plant species diversity in Bidouiyeh protected area in Bardsir, Kerman";

            //WS4.Cells [33, 1] = "";
            //WS4.Cells [33, 2] = "";
            //2018 A Review on the Analysis of Population Genetic Structure using Dominant Molecular Markers and Introducing the New Program STRUCTUREasy

            WS4.Columns.AutoFit ();

            // ************************************************************************************************************* SAVE AND EXIT
            string flnm = "";
            if (intSubset == 0)
                {
                flnm = Application.StartupPath + "FlorAn-" + DateTime.Now.ToString ("yyyyMMddHHmm") + "-" + strSetTitle + "-all" + ".xlsx";
                }
            else
                {
                flnm = Application.StartupPath + "FlorAn-" + DateTime.Now.ToString ("yyyyMMddHHmm") + "-" + strSetTitle + "-subset" + intSubset.ToString () + ".xlsx";
                }
            WB.SaveAs (flnm, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //save charts as picture
            flnm = Strings.Left (flnm, flnm.Length - 5);
            //chart1stat1.Export (flnm + "11_.bmp", "BMP", null);
            //chart1stat2.Export (flnm + "12_.bmp", "BMP", null);
            //chart11x.Export (flnm + "11x.bmp", "BMP", null);
            //chart12x.Export (flnm + "12x.bmp", "BMP", null);
            //chart13x.Export (flnm + "13x.bmp", "BMP", null);
            //chart2ct1.Export (flnm + "21_.bmp", "BMP", null);
            //chart2ct2.Export (flnm + "22_.bmp", "BMP", null);
            //chart311.Export (flnm + "311.bmp", "BMP", null);
            //chart312.Export (flnm + "312.bmp", "BMP", null);
            //chart32.Export (flnm + "32_.bmp", "BMP", null);
            //chart33.Export (flnm + "33_.bmp", "BMP", null);
            //chart34.Export (flnm + "34_.bmp", "BMP", null);
            XL.Visible = true;
            //WB.Close ();
            //XL.Quit ();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject (WS1-WS5  WB XL);
            //GC.Collect ();
            //GC.WaitForPendingFinalizers ();
            //WS1-5 WB XL = null;
            return;

            }
        public static void SetsCompare (int nSets, int intSetAID, int intSetBID, int intSetCID, int intSetDID)
            {
            --Compare 4 sets: A-B-C-D
            MessageBox.Show ("Under constrction!", "alamut");
            }
        public static void CreateMatrices4MultiVarFlorAn (int intSetID, string strSetTitle, int intSubset)
            {
            --
             * r,c?
             * fill tblMatrixR (r)
             * fill tblMatrixB (c)
             * fill MatrixD (r,c)
             * Let calling method use these data
             --
            FileSystem.FileClose ();
            string fltr = "";
            switch (intSubset)
                {
                case 0:
                        {
                        fltr = "";
                        break;
                        }
                default:
                        {
                        fltr = " AND (Membership & " + Math.Pow (2, intSubset - 1).ToString () + " = " + Math.Pow (2, intSubset - 1).ToString () + ")";
                        break;
                        }
                }
            --
               RunSQL "SELECT ID, Abbr INTO [tbl_C] FROM Collections WHERE Sx=True ORDER BY Abbr;"
               RunSQL "SELECT DISTINCT Binomials.ID, Binomials.SciName INTO [tbl_R] FROM (Genera INNER JOIN ((Specimens INNER JOIN Collections ON Specimens.CollectionID = Collections.ID) INNER JOIN Binomials ON Specimens.Binomial = Binomials.ID) ON Genera.ID = Binomials.Genus_ID) INNER JOIN Families ON Genera.Family = Families.ID WHERE Collections.Sx=True AND Families.Sel4NTSYSpc=True AND Right(Binomials.SciName, 3) <> 'sp.' ORDER BY Binomials.SciName;"
               RunSQL "SELECT Specimens.CollectionID, Specimens.Binomial INTO [tbl_D] FROM (Specimens INNER JOIN Collections ON Specimens.CollectionID = Collections.ID) INNER JOIN (Genera INNER JOIN Families ON Genera.Family = Families.ID) ON Specimens.Genus = Genera.ID WHERE Collections.Sx=True AND Families.Sel4NTSYSpc=True;"
             --
            //get cols : Datasets
            DB.DS.Tables ["tblMatrixC"].Clear ();
            string strSQLR = "SELECT DISTINCT Datasets.ID, Datasets.dsAbbr FROM Datasets INNER JOIN SetData_DS ON Datasets.ID = SetData_DS.datasetID WHERE SetData_DS.SetID = " + intSetID.ToString () + fltr;
            using (var CnnSS = new System.Data.SqlClient.SqlConnection (DB.CnnString))
                {
                CnnSS.Open ();
                DB.DASS = new System.Data.SqlClient.SqlDataAdapter (strSQLR, CnnSS);
                DB.DASS.Fill (DB.DS, "tblMatrixC");
                CnnSS.Close ();
                }
            //get rows : Species
            DB.DS.Tables ["tblMatrixR"].Clear ();
            string strSQLC = "SELECT DISTINCT Species.ID, Species.genusName + ' ' + Species.epithet";
            strSQLC = strSQLC + " FROM Species INNER JOIN DsData ON Species.ID = DsData.SpeciesID INNER JOIN SetData_DS ON DsData.DatasetID = SetData_DS.DatasetID";
            strSQLC = strSQLC + " WHERE SetID = " + intSetID.ToString () + " AND rank = 'species' " + fltr;
            using (var CnnSS = new System.Data.SqlClient.SqlConnection (DB.CnnString))
                {
                CnnSS.Open ();
                DB.DASS = new System.Data.SqlClient.SqlDataAdapter (strSQLC, CnnSS);
                DB.DASS.Fill (DB.DS, "tblMatrixR");
                CnnSS.Close ();
                }
            int c = (from x in DB.DS.Tables ["tblMatrixC"].AsEnumerable () select x.Field<int> ("ID")).Distinct ().Count ();
            int r = (from z in DB.DS.Tables ["tblMatrixR"].AsEnumerable () select z.Field<int> ("ID")).Distinct ().Count ();
            if (c == 0 || r == 0)
                return;
            else
                {
                MessageBox.Show ("R=" + r.ToString () + " C=" + c.ToString ());
                int [,] MatrixD = new int [r, c];
                }
            }
        public static void SetsR (int intSetID, string strSetTitle, int intSubset)
            {
            //
            MessageBox.Show ("Under constrction!", "alamut");
            }
        public static void SetsNTSYSPC (int intSetID, string strSetTitle, int intSubset, int intParams)
            {
            //
            MessageBox.Show ("Under constrction!", "alamut");
            }
        public static void SetsCLUTO (int intSetID, string strSetTitle, int intSubset)
            {
            //
            MessageBox.Show ("Under constrction!", "alamut");
            }
        public static void SetsSplitsTree (int intSetID, string strSetTitle, int intSubset)
            {
            //
            MessageBox.Show ("Under constrction!", "alamut");
            }
        public static void SetsMEGA (int intSetID, string strSetTitle, int intSubset)
            {
            //
            MessageBox.Show ("Under constrction!", "alamut");
            }
        public static void SetsAssociation (int SPID1, int SPID2, int intSetID)
            {
            //
            MessageBox.Show ("Under constrction!", "alamut");
            }
         */
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
