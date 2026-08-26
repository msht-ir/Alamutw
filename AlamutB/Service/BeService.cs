using AlamutS.Models;
using Microsoft.Data.SqlClient;
using System.Data;
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
        public async Task<List<Species>> Read_SpeciesByDatasetIdAsync (int datasetType, int datasetId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "TX.Read_SpeciesByDatasetId";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetType", datasetType);
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
        #region DATASETDATA-FL
        public async Task<bool> Create_DatasetDataFLAsync (DatasetDataFL datasetDataFL)
            {
            string sql = "BS.Create_DatasetDataFL";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", datasetDataFL.DatasetId);
                cmd.Parameters.AddWithValue ("@SpeciesId", datasetDataFL.SpeciesId);
                cmd.Parameters.AddWithValue ("@Infraspecific", datasetDataFL.Infraspecific);
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
        public async Task<List<DatasetDataFL>> Read_DatasetDataFLAsync (int datasetId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Read_DatasetDataFL";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", datasetId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<DatasetDataFL> lstDatasetDataFL = new List<DatasetDataFL> ();
                while (await reader.ReadAsync ())
                    {
                    lstDatasetDataFL.Add (new DatasetDataFL
                        {
                        DatasetDataFLId = reader.GetInt32 (0),
                        DatasetId = reader.GetInt32 (1),
                        SpeciesId = reader.GetInt32 (2),
                        Infraspecific = reader.GetString (3)
                        });
                    }
                await cnn.CloseAsync ();
                return lstDatasetDataFL;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<DatasetDataFL> ();
                }
            }
        public async Task<bool> Update_DatasetDataFLAsync (DatasetDataFL datasetDataFL)
            {
            string sql = "BS.Update_DatasetDataFL";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetDataFLId", datasetDataFL.DatasetDataFLId);
                cmd.Parameters.AddWithValue ("@DatasetId", datasetDataFL.DatasetId);
                cmd.Parameters.AddWithValue ("@SpeciesId", datasetDataFL.SpeciesId);
                cmd.Parameters.AddWithValue ("@Infraspecific", datasetDataFL.Infraspecific);
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
        public async Task<bool> Delete_DatasetDataFLAsync (int datasetDataFLId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Delete_DatasetDataFL";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetDataFLId", datasetDataFLId);
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
        #region DATASETDATA-CN
        public async Task<bool> Create_DatasetDataCNAsync (DatasetDataCN datasetDataCN)
            {
            string sql = "BS.Create_DatasetDataCN";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", datasetDataCN.DatasetId);
                cmd.Parameters.AddWithValue ("@LineString", datasetDataCN.LineString);
                cmd.Parameters.AddWithValue ("@ParsedGenus", datasetDataCN.ParsedGenus);
                cmd.Parameters.AddWithValue ("@ParsedEpithet", datasetDataCN.ParsedEpithet);
                cmd.Parameters.AddWithValue ("@SpeciesId", datasetDataCN.SpeciesId);
                cmd.Parameters.AddWithValue ("@Infraspecific", datasetDataCN.Infraspecific);
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
        public async Task<List<DatasetDataCN>> Read_DatasetDataCNAsync (int datasetId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Read_DatasetDataCN";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", datasetId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<DatasetDataCN> lstDatasetDataCN = new List<DatasetDataCN> ();
                while (await reader.ReadAsync ())
                    {
                    lstDatasetDataCN.Add (new DatasetDataCN
                        {
                        DatasetDataCNId = reader.GetInt32 (0),
                        DatasetId = reader.GetInt32 (1),
                        LineString = reader.GetString (2),
                        ParsedGenus = reader.GetString (3),
                        ParsedEpithet = reader.GetString (4),
                        SpeciesId = reader.GetInt32 (5),
                        Infraspecific = reader.GetString (6),
                        });
                    }
                await cnn.CloseAsync ();
                return lstDatasetDataCN;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<DatasetDataCN> ();
                }
            }
        public async Task<bool> Update_DatasetDataCNAsync (DatasetDataCN datasetDataCN)
            {
            string sql = "BS.Update_DatasetDataCN";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetDataCNId", datasetDataCN.DatasetDataCNId);
                cmd.Parameters.AddWithValue ("@DatasetId", datasetDataCN.DatasetId);
                cmd.Parameters.AddWithValue ("@LineString", datasetDataCN.LineString);
                cmd.Parameters.AddWithValue ("@ParsedGenus", datasetDataCN.ParsedGenus);
                cmd.Parameters.AddWithValue ("@ParsedEpithet", datasetDataCN.ParsedEpithet);
                cmd.Parameters.AddWithValue ("@SpeciesId", datasetDataCN.SpeciesId);
                cmd.Parameters.AddWithValue ("@Infraspecific", datasetDataCN.Infraspecific);
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
        public async Task<bool> Delete_DatasetDataCNAsync (int datasetDataCNId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Delete_DatasetDataCN";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetDataCNId", datasetDataCNId);
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
        #region DATASETDATA-LM
        public async Task<bool> Create_DatasetDataLMAsync (DatasetDataLM datasetDataLM)
            {
            Console.WriteLine ($"datasetDataLM.DatasetId= {datasetDataLM.DatasetId} , datasetDataLM.SpeciesId= {datasetDataLM.SpeciesId}");
            string sql = "DT.Create_DatasetDataLM";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", datasetDataLM.DatasetId);
                cmd.Parameters.AddWithValue ("@SpeciesId", datasetDataLM.SpeciesId);
                await cnn.OpenAsync ();
                await cmd.ExecuteScalarAsync ();
                await cnn.CloseAsync ();
                return true;
                }
            catch (Exception ex)
                {
                Console.WriteLine ($"BE Error (in Create_DatasetDataLMAsync): \n {ex}");
                await cnn.CloseAsync ();
                return false;
                }
            }
        public async Task<List<DatasetDataLM>> Read_DatasetDataLMAsync (int datasetId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Read_DatasetDataLM";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", datasetId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<DatasetDataLM> lstDatasetDataLM = new List<DatasetDataLM> ();
                while (await reader.ReadAsync ())
                    {
                    lstDatasetDataLM.Add (new DatasetDataLM
                        {
                        DatasetDataLMId = reader.GetInt32 (0),
                        DatasetId = reader.GetInt32 (1),
                        SpeciesId = reader.GetString (2)
                        });
                    }
                await cnn.CloseAsync ();
                return lstDatasetDataLM;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<DatasetDataLM> ();
                }
            }
        public async Task<bool> Update_DatasetDataLMAsync (DatasetDataLM datasetDataLM)
            {
            string sql = "BS.Update_DatasetDataLM";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetDataLMId", datasetDataLM.DatasetDataLMId);
                cmd.Parameters.AddWithValue ("@DatasetId", datasetDataLM.DatasetId);
                cmd.Parameters.AddWithValue ("@SpeciesId", datasetDataLM.SpeciesId);
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
        public async Task<bool> Delete_DatasetDataLMAsync (int datasetLMId, string speciesId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "DT.Delete_DatasetDataLM";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetLMId", datasetLMId);
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
        #region DATASETDATA-ST
        public async Task<bool> Create_DatasetDataSTAsync (DatasetDataST datasetDataST)
            {
            string sql = "BS.Create_DatasetDataST";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@StationId", datasetDataST.StationId);
                cmd.Parameters.AddWithValue ("@Yr", datasetDataST.Yr);
                cmd.Parameters.AddWithValue ("@DataType", datasetDataST.DataType);
                cmd.Parameters.AddWithValue ("@M01", datasetDataST.M01);
                cmd.Parameters.AddWithValue ("@M02", datasetDataST.M02);
                cmd.Parameters.AddWithValue ("@M03", datasetDataST.M03);
                cmd.Parameters.AddWithValue ("@M04", datasetDataST.M04);
                cmd.Parameters.AddWithValue ("@M05", datasetDataST.M05);
                cmd.Parameters.AddWithValue ("@M06", datasetDataST.M06);
                cmd.Parameters.AddWithValue ("@M07", datasetDataST.M07);
                cmd.Parameters.AddWithValue ("@M08", datasetDataST.M08);
                cmd.Parameters.AddWithValue ("@M09", datasetDataST.M09);
                cmd.Parameters.AddWithValue ("@M10", datasetDataST.M10);
                cmd.Parameters.AddWithValue ("@M11", datasetDataST.M11);
                cmd.Parameters.AddWithValue ("@M12", datasetDataST.M12);
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
        public async Task<List<DatasetDataST>> Read_DatasetDataSTAsync (int datasetId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Read_DatasetDataST";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetId", datasetId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync ();
                List<DatasetDataST> lstDatasetDataST = new List<DatasetDataST> ();
                while (await reader.ReadAsync ())
                    {
                    lstDatasetDataST.Add (new DatasetDataST
                        {
                        DatasetDataSTId = reader.GetInt32 (0),
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
                return lstDatasetDataST;
                }
            catch (Exception ex)
                {
                Console.WriteLine ("error: " + ex.Message);
                cnn.Close ();
                return new List<DatasetDataST> ();
                }
            }
        public async Task<bool> Update_DatasetDataSTAsync (DatasetDataST datasetDataST)
            {
            string sql = "BS.Update_DatasetDataST";
            string? connstring = _config.GetConnectionString ("cnni");
            using SqlConnection cnn = new SqlConnection (connstring);
            try
                {
                using SqlCommand cmd = new SqlCommand (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetDataSTId", datasetDataST.DatasetDataSTId);
                cmd.Parameters.AddWithValue ("@StationId", datasetDataST.StationId);
                cmd.Parameters.AddWithValue ("@Yr", datasetDataST.Yr);
                cmd.Parameters.AddWithValue ("@DataType", datasetDataST.DataType);
                cmd.Parameters.AddWithValue ("@M01", datasetDataST.M01);
                cmd.Parameters.AddWithValue ("@M02", datasetDataST.M02);
                cmd.Parameters.AddWithValue ("@M03", datasetDataST.M03);
                cmd.Parameters.AddWithValue ("@M04", datasetDataST.M04);
                cmd.Parameters.AddWithValue ("@M05", datasetDataST.M05);
                cmd.Parameters.AddWithValue ("@M06", datasetDataST.M06);
                cmd.Parameters.AddWithValue ("@M07", datasetDataST.M07);
                cmd.Parameters.AddWithValue ("@M08", datasetDataST.M08);
                cmd.Parameters.AddWithValue ("@M09", datasetDataST.M09);
                cmd.Parameters.AddWithValue ("@M10", datasetDataST.M10);
                cmd.Parameters.AddWithValue ("@M11", datasetDataST.M11);
                cmd.Parameters.AddWithValue ("@M12", datasetDataST.M12);
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
        public async Task<bool> Delete_DatasetDataSTAsync (int datasetDataSTId)
            {
            string? connString = _config.GetConnectionString ("cnni");
            string sql = "BS.Delete_DatasetDataST";
            using SqlConnection cnn = new (connString);
            try
                {
                await cnn.OpenAsync ();
                SqlCommand cmd = new (sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@DatasetDataSTId", datasetDataSTId);
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
            string sql = "BS.Create_Set";
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
            string sql = "BS.Read_Sets";
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
            string sql = "BS.Read_Set";
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
            string sql = "BS.Update_Set";
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
            string sql = "BS.Delete_Set";
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
            string sql = "BS.Create_SetData";
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
            string sql = "BS.Read_SetData";
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
            string sql = "BS.Update_SetData";
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
            string sql = "BS.Delete_SetData";
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
                Console.WriteLine ($"BE Error Create Note : ============================== \n {ex}");
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
