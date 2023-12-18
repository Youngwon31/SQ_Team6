<!--
* Filename: CheckOut.aspx
* Author:   Ben Heyden, Tugrap Turker Aydiner, Jiu Kim, Youngwon Seo
* Date:     16/12/2023
* Description: This HTML markup file creates the user interface for the Checkout page of the FreshSaver website.
*              It provides options for users to select their payment method, input credit card information, 
*              and displays the selected pick-up location and times. 
*              The page includes validations for credit card details and allows users to finalize their purchase.
-->


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="FreshSaver.CheckOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Check Out</title>
    <link href="checkout.css" rel="stylesheet" type="text/css" />
</head>
    
<body>
    <form id="form1" runat="server">
         <!-- Display store pick-up time and location -->
        <div>
            <h2>Pick-Up Time and Location:</h2>
            <asp:Label ID="lblLocation" runat="server" />
            <br />
            <asp:Label ID="lblPickupTime" runat="server" />
            <br />

            <!-- Payment option selection -->
            <h3>Payment Options</h3>
            <asp:RadioButtonList ID="rblPaymentOptions" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblPaymentOptions_SelectedIndexChanged">
                <asp:ListItem Text="Pay at Store" Value="PayAtStore" />
                <asp:ListItem Text="Pay Online" Value="PayOnline" />
            </asp:RadioButtonList>

            <!-- Credit card information fields, shown only if Pay Online is selected -->
            <asp:Panel ID="pnlCreditCardInfo" runat="server" Visible="false">
                <h3>Credit Card Information</h3>
                <!-- Input fields for credit card details with validators -->
                Card Number: <asp:TextBox ID="txtCardNumber" runat="server" /><br />
                <asp:RequiredFieldValidator ID="rfvCardNumber" runat="server"
                    ControlToValidate="txtCardNumber"
                    ErrorMessage="Card Number can't be blank." 
                    Display="Dynamic" 
                    ForeColor="Red" />
                <asp:RegularExpressionValidator ID="regexCardNumber" runat="server"
                    ControlToValidate="txtCardNumber"
                    ErrorMessage="Card Number must be 16 digits."
                    ValidationExpression="^\d{16}$"
                    Display="Dynamic"
                    ForeColor="Red"/>
                <br />
                
                <!-- Expiry Date field with validation -->
                Expiry Date: <asp:TextBox ID="txtExpiryDate" runat="server" /><br />
                <asp:RequiredFieldValidator ID="rfvExpiryDate" runat="server"
                    ControlToValidate="txtExpiryDate"
                    ErrorMessage="Expiry Date can't be blank." 
                    Display="Dynamic" 
                    ForeColor="Red" />
                <asp:CustomValidator ID="cvExpiryDate" runat="server" 
                    ControlToValidate="txtExpiryDate"
                    ErrorMessage="Expiry Date must not be expired." 
                    OnServerValidate="ValidateExpiryDate" 
                    Display="Dynamic"
                    ForeColor="Red">

                </asp:CustomValidator>
                <asp:RegularExpressionValidator ID="regexExpiryDate" runat="server"
                    ControlToValidate="txtExpiryDate"
                    ErrorMessage="Expiry Date must be in MM/YY format."
                    ValidationExpression="^(0[1-9]|1[0-2])\/\d{2}$"
                    Display="Dynamic"
                    ForeColor="Red"/>
                <br />

                <!-- CVV field with validation -->
                CVV: <asp:TextBox ID="txtCVV" runat="server" /><br />
                <asp:RequiredFieldValidator ID="rfvCVV" runat="server"
                    ControlToValidate="txtCVV"
                    ErrorMessage="CVV can't be blank." 
                    Display="Dynamic" 
                    ForeColor="Red" />
                <asp:RegularExpressionValidator ID="regexCVV" runat="server"
                    ControlToValidate="txtCVV"
                    ErrorMessage="CVV must be 3 digits."
                    ValidationExpression="^\d{3}$"
                    Display="Dynamic" 
                    ForeColor="Red"/>
                <br />
            </asp:Panel>
                 
            <!-- Checkout Button -->
            <asp:Button ID="btnCheckout" runat="server" Text="Checkout" OnClick="btnCheckout_Click" />
            <br />
            <br />
            
            <!-- Confirmation message and return to home button -->
            <asp:Label ID="lblFinish" runat="server" Text="" Visible="false" />
            <br />
            <asp:Button ID="btnHome" runat="server" Text="Return To Home" PostBackUrl="~/Home.aspx" CssClass="return-to-home-button"/>

        </div>
    </form>
</body>

</html>
