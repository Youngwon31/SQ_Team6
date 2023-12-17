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
        private string connectionString = ConfigurationManager.ConnectionStrings["storesDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStoresByCategory("Restaurant");
            }
        }

        private void LoadStoresByCategory(string category)
        {
            LoadRecommendedStores(category);//start loading stores based off restaurant category
            LoadNewStores(category);//start loading stores based off restaurant category
        }

        protected void StoreSelected(object sender, CommandEventArgs e)
        {
            string storeId = e.CommandArgument.ToString();
            Response.Redirect($"StoreDescription.aspx?StoreID={storeId}");//redirect to storeDescription with the store that was selected
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

        private void BindDataToRepeater(Repeater repeater, string query, string param, object value)
        {
            //binding data to a repeater so we can display repetitive data in a structured way
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
                // Passes a search query to the database and retrieves the results.
                // For this example, we will assume that only one result is retrieved.
                // Write database connection and query execution code.

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
                            // If there are results, redirect to StoreDescription.aspx.
                            Response.Redirect($"StoreDescription.aspx?StoreID={result}");
                        }
                        else
                        {
                            // If no search results are found, the user is notified. (e.g. display message on label)
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
            //our categories for store types
            lnkGrocery.CssClass = selectedCategory == "Grocery" ? "category-icon selected-category" : "category-icon";
            lnkConvenience.CssClass = selectedCategory == "Convenience" ? "category-icon selected-category" : "category-icon";
            lnkRestaurant.CssClass = selectedCategory == "Restaurant" ? "category-icon selected-category" : "category-icon";
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                string query = @"SELECT StoreID, StoreName FROM Stores 
                         WHERE StoreName LIKE @searchQuery
                         LIMIT 10;";

                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");
                        using (var da = new MySqlDataAdapter(cmd))
                        {
                            var dt = new DataTable();
                            da.Fill(dt);
                            rptSearchResults.DataSource = dt;
                            rptSearchResults.DataBind();
                        }
                    }
                }
            }
        }

        protected void rptSearchResults_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelectStore")
            {
                string storeId = e.CommandArgument.ToString();
                Response.Redirect($"StoreDescription.aspx?StoreID={storeId}");
            }
        }
    }

}
