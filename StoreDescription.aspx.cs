using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace FreshSaver
{
    public partial class StoreDescription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve StoreID from the query string
                if (Request.QueryString["StoreID"] != null)
                {
                    int storeId = int.Parse(Request.QueryString["StoreID"]);
                    LoadStoreDetails(storeId);
                }
            }
        }

        private void LoadStoreDetails(int storeId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StoreDB"].ConnectionString;

            // 'SqlConnection'을 'MySqlConnection'으로 변경합니다.
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // 'SqlCommand'를 'MySqlCommand'로 변경합니다.
                string query = @"SELECT s.StoreName, s.Address, s.ImageURL, m.MenuName, m.Description, m.OriginalPrice, m.DiscountedPrice
                         FROM Stores s
                         JOIN MenuItems m ON s.StoreID = m.StoreID
                         WHERE s.StoreID = @StoreID
                         ORDER BY m.OriginalPrice DESC
                         LIMIT 1;";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StoreID", storeId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblStoreName.InnerText = reader["StoreName"].ToString(); // InnerText 사용
                                                                                     // imgStoreImage 및 lblAddress 설정 코드는 동일하게 유지
                            lblMenuName.InnerText = reader["MenuName"].ToString(); // InnerText 사용
                            lblDescription.InnerText = reader["Description"].ToString(); // InnerText 사용
                            lblOriginalPrice.InnerText = reader["OriginalPrice"].ToString(); // InnerText 사용
                            lblDiscountedPrice.InnerText = reader["DiscountedPrice"].ToString(); // InnerText 사용
                        }
                    }
                }
            }
        }

    }
}