/*
* Filename: ShoppingCart.aspx.cs
* Author: Ben Heyden, Tugrap Turker Aydiner, Jiu Kim, Youngwon Seo
* Date: 16/12/2023
* Description: This file contains the server-side code for the Shopping Cart page of the FreshSaver website.
*              It manages the display of items in the user's shopping cart, including item details and total cost.
*              The page also provides functionality to proceed to checkout and logs shopping cart activity.
*/

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;

namespace FreshSaver
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        // Executes when the page is loaded.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCartItems();//displays all the items in our cart along with total price
            }
        }

        // Loads items from the session-based shopping cart and binds them to a repeater control.
        private void LoadCartItems()
        {
            var cart = Session["Cart"] as Dictionary<int, int>;

            string menuNameLog = "";
            string quantityLog = "";
            string itemTotalLog = "";

            if (cart != null && cart.Count > 0) //checks if cart is empty
            {
                List<MenuItem> cartItems = new List<MenuItem>();

                foreach (var item in cart)//goes through all items in cart
                {
                    int menuItemId = item.Key;
                    int quantity = item.Value;

                    MenuItem menuItem = GetMenuItemDetails(menuItemId);

                    decimal itemTotal = menuItem.DiscountedPrice * quantity;//gets each items total price by multiplying by quantity

                    cartItems.Add(new MenuItem
                    {
                        MenuName = menuItem.Description,
                        Quantity = quantity,
                        ItemTotal = itemTotal

                    });

                    // Update log information
                    menuNameLog += menuItem.Description + "; ";
                    quantityLog += quantity.ToString() + "; ";
                    itemTotalLog += itemTotal.ToString("F2") + "; ";

                }

                CartItemsRepeater.DataSource = cartItems;
                CartItemsRepeater.DataBind();

                lblTotalCost.Text = cartItems.Sum(item => item.ItemTotal).ToString("F2");//gets the sum total of each item we put in the cart
                LogCheckoutAttempt(menuNameLog, quantityLog, itemTotalLog);
            }
            else
            {
                lblEmptyShop.Text = "Your shopping cart is empty.";
                btnCheckout.Enabled = false;
            }
        }

        // Retrieves details of a specific menu item from the database.
        private MenuItem GetMenuItemDetails(int menuItemId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["storesDB"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                //gets all relevant information to the items we placed in our cart
                string query = "SELECT MenuName, Description, OriginalPrice, DiscountedPrice, ItemURL, stock " +
                                "FROM MenuItems " +
                                "WHERE MenuItemID = @MenuItemID";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MenuItemID", menuItemId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MenuItem//properly storing the info we got from the database
                            {
                                MenuName = reader["MenuName"].ToString(),
                                Description = reader["Description"].ToString(),
                                OriginalPrice = Convert.ToDecimal(reader["OriginalPrice"]),
                                DiscountedPrice = Convert.ToDecimal(reader["DiscountedPrice"]),
                                ItemURL = reader["ItemURL"].ToString(),
                                Stock = Convert.ToInt32(reader["Stock"])
                            };
                        }
                    }
                }
            }
            return new MenuItem();
        }


        // Redirects to the checkout page when the checkout button is clicked.
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Checkout.aspx");//continue to checkout
        }

        // Logs checkout attempts with details about menu items and quantities.
        private void LogCheckoutAttempt(string menuNameLog, string quantityLog, string itemTotalLog)
        {
            string filePath = Server.MapPath("~/logs/CheckoutLogs.txt");
            string logText = $"Timestamp: {DateTime.Now}, Menu Items: {menuNameLog}, Quantities: {quantityLog}, Item Totals: {itemTotalLog}\n";

            try
            {
                File.AppendAllText(filePath, logText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to log file: " + ex.Message);
            }
        }
    }
}