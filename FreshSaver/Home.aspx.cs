/*
* Filename: Home.aspx.cs
* Author: Ben Heyden, Tugrap Turker Aydiner, Jiu Kim, Youngwon Seo
* Date: 16/12/2023
* Description: This file contains the server-side code for the Home page of the FreshSaver website.
*              It includes functionality for loading stores by category, handling search queries,
*              and managing user interactions such as selecting a store or a category.
*              The code dynamically binds store data to repeaters based on categories or search results.
*/

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

        // Executes when the page is loaded.
        protected void Page_Load(object sender, EventArgs e)
        {
            // Load stores by default category when the page is first loaded
            if (!IsPostBack)
            {
                LoadStoresByCategory("Restaurant");
            }
        }

        // Loads stores based on the specified category by calling methods to load recommended and new stores.
        private void LoadStoresByCategory(string category)
        {
            // Loads stores into repeaters based on the selected category
            LoadRecommendedStores(category);
            LoadNewStores(category);
        }

        // Loads stores based on the specified category by calling methods to load recommended and new stores.
        protected void StoreSelected(object sender, CommandEventArgs e)
        {
            // Redirects to the store description page when a store is selected
            string storeId = e.CommandArgument.ToString();
            Response.Redirect($"StoreDescription.aspx?StoreID={storeId}");
        }


        // Loads recommended stores based on the highest-priced menu item in the specified category.
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

        // Loads new stores that have registered in the past week in the specified category.
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

        // Binds data to a repeater control using the provided SQL query.
        // It connects to the database, executes the query, and binds the results to the specified repeater.
        private void BindDataToRepeater(Repeater repeater, string query, string param, object value)
        {
            // Connects to the database and binds data to a repeater control
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

        // Handles the search functionality. 
        // Searches the database for stores matching the query and redirects to the store description if a match is found.
        protected void SearchStores(object sender, EventArgs e)
        {
            // Handles search functionality for stores
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

        // Handles category selection, updates the UI, and loads stores related to the selected category.
        protected void CategorySelected(object sender, CommandEventArgs e)
        {
            // Handles category selection and updates UI accordingly
            string category = e.CommandArgument.ToString();
            SetCategoryButtonCss(category);
            LoadStoresByCategory(category);
        }

        // Updates CSS classes for category buttons based on selection
        private void SetCategoryButtonCss(string selectedCategory)
        {
            // Updates CSS classes for category buttons based on selection
            lnkGrocery.CssClass = selectedCategory == "Grocery" ? "category-icon selected-category" : "category-icon";
            lnkConvenience.CssClass = selectedCategory == "Convenience" ? "category-icon selected-category" : "category-icon";
            lnkRestaurant.CssClass = selectedCategory == "Restaurant" ? "category-icon selected-category" : "category-icon";
        }

        // Dynamically handles changes in the search text box.
        // Triggers a search query based on the input and updates the search results repeater.
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Handles dynamic search functionality
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

        // Redirects to the store description page when a search result item is selected.
        protected void rptSearchResults_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Redirects to the store description page when a search result item is selected
            if (e.CommandName == "SelectStore")
            {
                string storeId = e.CommandArgument.ToString();
                Response.Redirect($"StoreDescription.aspx?StoreID={storeId}");
            }
        }
    }

}
