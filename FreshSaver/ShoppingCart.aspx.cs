using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace FreshSaver
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCartItems();//displays all the items in our cart along with total price
            }
        }

        private void LoadCartItems()
        {
            var cart = Session["Cart"] as Dictionary<int, int>;

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
                }

                CartItemsRepeater.DataSource = cartItems;
                CartItemsRepeater.DataBind();

                lblTotalCost.Text = cartItems.Sum(item => item.ItemTotal).ToString("F2");//gets the sum total of each item we put in the cart
                
            }
            else
            {
                lblEmptyShop.Text = "Your shopping cart is empty.";
                btnCheckout.Enabled = false;
            }
        }

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

    

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Checkout.aspx");//continue to checkout
        }
    }
}