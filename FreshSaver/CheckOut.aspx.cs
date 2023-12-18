/*
* Filename: CheckOut.aspx.cs
* Author: Ben Heyden, Tugrap Turker Aydiner, Jiu Kim, Youngwon Seo
* Date: 16/12/2023
* Description: This file contains the server-side code for the Checkout page of the FreshSaver website.
*              It handles the display of selected store details and manages the checkout process, including
*              payment option selection and credit card validation. It also confirms the order and clears the session.
*/

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FreshSaver
{
    public partial class CheckOut : System.Web.UI.Page
    {
        // Executes when the page is loaded.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetStoreDetails();//getting stores pickup available times and store name
                rblPaymentOptions.SelectedIndex = 0;//setting pay in person as default option
            }

            // Update visibility of credit card info based on selected payment option.
            pnlCreditCardInfo.Visible = rblPaymentOptions.SelectedValue == "PayOnline";
        
        }

        // Handles changes in payment option selection.
        protected void rblPaymentOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show/hide the credit card information panel based on the selected option
            pnlCreditCardInfo.Visible = rblPaymentOptions.SelectedValue == "PayOnline";

        }

        // Retrieves and displays store details such as pickup times and store name.
        private void GetStoreDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["storesDB"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                //getting the selected stores pick up time and store name
                string query = "SELECT PickUpTimes, StoreName FROM Stores";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string pickUpTimes = reader["PickUpTimes"].ToString();
                            string location = reader["StoreName"].ToString();
                            lblLocation.Text = location;
                            lblPickupTime.Text = pickUpTimes;
                        }
                    }
                }
            }
        }

        // Validates the expiry date of the credit card.
        protected void ValidateExpiryDate(object source, ServerValidateEventArgs args)
        {
            //checking the input wasn't left empty
            if (!string.IsNullOrEmpty(args.Value)) 
            {
                string[] dateParts = args.Value.Split('/');//splitting the user input where the / is
                if (dateParts.Length == 2)
                {
                    int month, year;
                    if (int.TryParse(dateParts[0], out month) && int.TryParse(dateParts[1], out year))//placing each part into ints
                    {
                        if (month >= 1 && month <= 12 && year >= 0 && year <= 99)//checking the user didn't put invalid numbers (e.g. a month larger than 12)
                        {
                            DateTime expiry = new DateTime(2000 + year, month, DateTime.DaysInMonth(2000 + year, month));
                            if (expiry > DateTime.Now)//comparing if the dateTime of the expiry is greater than the current time
                            {
                                args.IsValid = true;
                            }
                            else
                            {
                                args.IsValid = false;
                            }
                        }
                            

                    }
                }
            }
            
        }

        // Finalizes the checkout process.
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                lblFinish.Text = "Thank you for your order! Please pick it up at the designated location during the allowed times!";
                lblFinish.Visible = true;//showing thank you label

                Session.Clear();//clearing all remaining session tokens
            }
            

        }


    }
}