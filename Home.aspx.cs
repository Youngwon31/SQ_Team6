using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FreshSaver
{
    public partial class Home : Page
    {
        // Database connection string
        private string connectionString = ConfigurationManager.ConnectionStrings["StoreDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStoresByCategory("Restaurant");
            }
        }

        private void LoadStoresByCategory(string category)
        {
            LoadRecommendedStores(category);
            LoadNewStores(category);
        }

        protected void StoreSelected(object sender, CommandEventArgs e)
        {
            string storeId = e.CommandArgument.ToString();
            Response.Redirect($"StoreDescription.aspx?StoreID={storeId}");
        }


        private void LoadRecommendedStores(string category)
        {
            // Define the query to get the top priced menu item for each store in the category
            string query = @"SELECT
                    s.StoreID,
                    s.StoreName,
                    s.Address,
                    s.Category,
                    s.ImageURL,
                    m.MenuName,
                    m.OriginalPrice,
                    m.DiscountedPrice
                FROM Stores s
                JOIN MenuItems m ON s.StoreID = m.StoreID
                WHERE s.Category = @category
                ORDER BY m.OriginalPrice DESC
                LIMIT 1;
            ";
            BindDataToRepeater(StoresRepeater, query, "@category", category);
        }

        private void LoadNewStores(string category)
        {
            // Define the query to get the most recent store in the category
            string query = @"SELECT
                    s.StoreID,
                    s.StoreName,
                    s.Address,
                    s.Category,
                    s.ImageURL,
                    m.MenuName,
                    m.OriginalPrice,
                    m.DiscountedPrice
                FROM Stores s
                JOIN MenuItems m ON s.StoreID = m.StoreID
                WHERE s.Category = @category AND s.RegistrationDate >= CURDATE() - INTERVAL 7 DAY
                ORDER BY s.RegistrationDate DESC
                LIMIT 1;
            ";
            BindDataToRepeater(NewStoresRepeater, query, "@category", category);
        }

        private void FilterStores(string category)
        {
            string mainStoresQuery = $@"SELECT StoreName, Address, ImageURL FROM Stores 
                                WHERE Category = @category;";
            BindDataToRepeater(StoresRepeater, mainStoresQuery, "@category", category);

            string newStoresQuery = $@"SELECT StoreName, Address, ImageURL FROM Stores 
                               WHERE Category = @category AND RegistrationDate >= CURDATE() - INTERVAL 7 DAY;";
            BindDataToRepeater(NewStoresRepeater, newStoresQuery, "@category", category);
        }

        private void BindDataToRepeater(Repeater repeater, string query, string param, object value)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(param, value);
                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        repeater.DataSource = dt;
                        repeater.DataBind();
                    }
                }
            }
        }

        protected void SearchStores(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                // 검색 쿼리를 데이터베이스에 전달하고 결과를 가져옵니다.
                // 여기서는 예시로 하나의 결과만 가져오는 것으로 가정합니다.
                // 데이터베이스 연결 및 쿼리 실행 코드를 작성하세요.

                // 예시 쿼리:
                string query = @"SELECT StoreID FROM Stores 
                         WHERE StoreName LIKE @searchQuery 
                         OR Address LIKE @searchQuery 
                         LIMIT 1;";

                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            // 결과가 있으면 StoreDescription.aspx로 리디렉션합니다.
                            Response.Redirect($"StoreDescription.aspx?StoreID={result}");
                        }
                        else
                        {
                            // 검색 결과가 없으면 사용자에게 알림을 줍니다. (예: 레이블에 메시지 표시)
                        }
                    }
                }
            }
        }

        protected void CategorySelected(object sender, CommandEventArgs e)
        {
            string category = e.CommandArgument.ToString();
            SetCategoryButtonCss(category);

            // Filter both repeaters by the selected category
            LoadStoresByCategory(category);
        }

        private void SetCategoryButtonCss(string selectedCategory)
        {
            lnkGrocery.CssClass = selectedCategory == "Grocery" ? "category-icon selected-category" : "category-icon";
            lnkConvenience.CssClass = selectedCategory == "Convenience" ? "category-icon selected-category" : "category-icon";
            lnkRestaurant.CssClass = selectedCategory == "Restaurant" ? "category-icon selected-category" : "category-icon";
        }
    }
}
