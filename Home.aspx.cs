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

            string searchStoresQuery = $@"SELECT s.StoreName, s.Address, s.ImageURL,
        (SELECT mi.MenuName FROM MenuItems mi WHERE mi.StoreID = s.StoreID LIMIT 1) AS MenuName,
        (SELECT mi.OriginalPrice FROM MenuItems mi WHERE mi.StoreID = s.StoreID LIMIT 1) AS OriginalPrice,
        (SELECT mi.DiscountedPrice FROM MenuItems mi WHERE mi.StoreID = s.StoreID LIMIT 1) AS DiscountedPrice
        FROM Stores s
        WHERE s.StoreName LIKE @searchQuery OR s.Address LIKE @searchQuery;";

            BindDataToRepeater(StoresRepeater, searchStoresQuery, "@searchQuery", "%" + searchQuery + "%");
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
