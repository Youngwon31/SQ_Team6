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
using Org.BouncyCastle.Tls;

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
                    int storeId = int.Parse(Request.QueryString["StoreID"]);//get the store that we selected
                    LoadStoreDetails(storeId);//load the store info
                    UpdateCartTotals();//update the cart 
                }
            }
        }

        private void LoadStoreDetails(int storeId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["storesDB"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                //query to get all the information necessary about the store
                string query = @"SELECT s.StoreName, s.Address, s.ImageURL, s.PickUpTimes, m.MenuName, m.Description, m.OriginalPrice, m.DiscountedPrice, m.MenuItemID, m.Stock, m.ItemURL
                                 FROM Stores s
                                 JOIN MenuItems m ON s.StoreID = m.StoreID
                                 WHERE s.StoreID = @StoreID;"; 

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StoreID", storeId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<MenuItem> menuItems = new List<MenuItem>();

                        while (reader.Read()) // Changed to while loop to read all menu items
                        {
                            lblStoreName.Text = reader["StoreName"].ToString();
                            imgStoreImage.ImageUrl = reader["ImageURL"].ToString(); // Set the store image URL
                            lblAddress.Text = reader["Address"].ToString();
                            lblPickUpTimes.Text = "Pickup Times: " + reader["PickUpTimes"].ToString();

                            // For each menu item, create a new MenuItem object and add it to a list
                            // Note: MenuItem is a hypothetical class representing a menu item

                            var menuItem = new MenuItem
                            {
                                MenuName = reader["MenuName"].ToString(),
                                Description = reader["Description"].ToString(),
                                OriginalPrice = reader.GetDecimal("OriginalPrice"),
                                DiscountedPrice = reader.GetDecimal("DiscountedPrice"),
                                MenuItemID = reader.GetInt32("MenuItemID"),
                                Stock = reader.GetInt32("Stock"),
                                ImageURL = reader["ItemURL"].ToString() // Assuming you have an image URL for each menu item
                            };
                            menuItems.Add(menuItem);

                            // Bind the list of menu items to a Repeater or other data-bound control to display them
                            // Your ASPX page will need to be adjusted to use a Repeater control
                            
                        }
                        MenuItemsRepeater.DataSource = menuItems;
                        MenuItemsRepeater.DataBind();
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

            // decrease stock as we add to our cart
            UpdateStock(menuItemId, currentStock - 1);

            
            // Update the cart display
            UpdateCartTotals();

        }

        protected void RemoveFromCart(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int menuItemId = Convert.ToInt32(((Button)sender).CommandArgument);

            // Fetch the current stock for the menu item
            int currentStock = GetCurrentStock(menuItemId);
            

            // If the item is in our cart, decrease from the session-based cart
            var cart = Session["Cart"] as Dictionary<int, int>;
            if (cart == null)
            {
                cart = new Dictionary<int, int>();
                Session["Cart"] = cart;
            }

            if (cart.ContainsKey(menuItemId))
            {
                if (cart[menuItemId] >0)
                {
                    cart[menuItemId]--;
                }
                if (cart[menuItemId] == 0)
                {
                    cart.Remove(menuItemId);

                }
                
            }
            else
            {
                cart.Add(menuItemId, 0);
            }

            // increase stock as we add to our cart
            UpdateStock(menuItemId, currentStock + 1);

            // Update the cart display
            UpdateCartTotals();



        }

        private int GetCurrentStock(int menuItemId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["storesDB"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                //query to check how much stock is currently available of specific items in store
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
            string connectionString = ConfigurationManager.ConnectionStrings["storesDB"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                //updating our database to follow how we have affected the quantity of items
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
            string connectionString = ConfigurationManager.ConnectionStrings["storesDB"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                //getting the price from the database of each item in specific store
                string query = "SELECT DiscountedPrice FROM MenuItems WHERE MenuItemID = @MenuItemID;";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MenuItemID", menuItemId);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }
        
    }
}