using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
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
                if (Request.QueryString["StoreID"] != null)
                {
                    int storeId = int.Parse(Request.QueryString["StoreID"]);
                    LoadStoreDetails(storeId);
                    UpdateCartTotals();
                }
            }
        }

        private void LoadStoreDetails(int storeId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StoreDB"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT s.StoreName, s.Address, s.ImageURL, m.MenuName, m.Description, m.OriginalPrice, m.DiscountedPrice, m.MenuItemID, m.Stock, m.ImageURL
                                 FROM Stores s
                                 JOIN MenuItems m ON s.StoreID = m.StoreID
                                 WHERE s.StoreID = @StoreID;"; 

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StoreID", storeId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) // Changed to while loop to read all menu items
                        {
                            lblStoreName.Text = reader["StoreName"].ToString();
                            imgStoreImage.ImageUrl = reader["ImageURL"].ToString(); // Set the store image URL
                            lblAddress.Text = reader["Address"].ToString();

                            // For each menu item, create a new MenuItem object and add it to a list
                            // Note: MenuItem is a hypothetical class representing a menu item
                            List<MenuItem> menuItems = new List<MenuItem>();
                            var menuItem = new MenuItem
                            {
                                MenuName = reader["MenuName"].ToString(),
                                Description = reader["Description"].ToString(),
                                OriginalPrice = reader.GetDecimal("OriginalPrice"),
                                DiscountedPrice = reader.GetDecimal("DiscountedPrice"),
                                MenuItemID = reader.GetInt32("MenuItemID"),
                                Stock = reader.GetInt32("Stock"),
                                ImageURL = reader["ImageURL"].ToString() // Assuming you have an image URL for each menu item
                            };
                            menuItems.Add(menuItem);

                            // Bind the list of menu items to a Repeater or other data-bound control to display them
                            // Your ASPX page will need to be adjusted to use a Repeater control
                            MenuItemsRepeater.DataSource = menuItems;
                            MenuItemsRepeater.DataBind();
                        }
                    }
                }
            }
        }


        protected void AddToCart(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int menuItemId = Convert.ToInt32(((Button)sender).CommandArgument);

            // Fetch the current stock for the menu item
            int currentStock = GetCurrentStock(menuItemId);
            if (currentStock <= 0)
            {
                // Show an out-of-stock alert
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Out of stock!');", true);
                return;
            }

            // If the item is in stock, add to the session-based cart
            var cart = Session["Cart"] as Dictionary<int, int>;
            if (cart == null)
            {
                cart = new Dictionary<int, int>();
                Session["Cart"] = cart;
            }

            if (cart.ContainsKey(menuItemId))
            {
                cart[menuItemId]++;
            }
            else
            {
                cart.Add(menuItemId, 1);
            }

            // 재고 바로빼면 안되니까 일단 코멘트아웃
            //UpdateStock(menuItemId, currentStock - 1);

            // Update the cart display
            UpdateCartTotals();

        }

        private int GetCurrentStock(int menuItemId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StoreDB"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Stock FROM MenuItems WHERE MenuItemID = @MenuItemID;";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MenuItemID", menuItemId);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private void UpdateStock(int menuItemId, int newStock)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StoreDB"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE MenuItems SET Stock = @NewStock WHERE MenuItemID = @MenuItemID;";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewStock", newStock);
                    cmd.Parameters.AddWithValue("@MenuItemID", menuItemId);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        protected void UpdateCartTotals()
        {
            var cart = Session["Cart"] as Dictionary<int, int>;
            if (cart != null)
            {
                decimal totalCost = 0;
                int totalQuantity = 0;

                foreach (var item in cart)
                {
                    int menuItemId = item.Key;
                    int quantity = item.Value;

                    // Get the price of the menu item
                    decimal price = GetPrice(menuItemId);
                    totalCost += price * quantity; // Ensure correct multiplication here
                    totalQuantity += quantity;
                }

                // Display totals on the page
                lblTotalCost.Text = totalCost.ToString("F2");
                lblTotalQuantity.Text = totalQuantity.ToString();
            }
        }

        private decimal GetPrice(int menuItemId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StoreDB"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT DiscountedPrice FROM MenuItems WHERE MenuItemID = @MenuItemID;";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MenuItemID", menuItemId);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        public class MenuItem
        {
            public int MenuItemID { get; set; }
            public string MenuName { get; set; }
            public string Description { get; set; }
            public decimal OriginalPrice { get; set; }
            public decimal DiscountedPrice { get; set; }
            public int Stock { get; set; }
            public string ImageURL { get; set; }
        }
    }
}