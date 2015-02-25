using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using Test_Website;

public partial class Account_Login : Page
{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Register JS encryption for Login button
                btnLogin.Attributes.Add("onclick", "Encrypt2Textbox('" + UserName.ClientID + "','" + Password.ClientID + "');");
            }
            
        }        



        protected void LogIn(object sender, EventArgs e)
        {

            Security security = new Security();

            if (IsValid)
            {
                // Validate the user password
                var manager = new UserManager();
                ApplicationUser user = manager.Find(security.Decrypt(UserName.Text), security.Decrypt(Password.Text));

                if (user != null)
                {
                    IdentityHelper.SignIn(manager, user, RememberMe.Checked);
                    string name = HttpContext.Current.User.Identity.Name;
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    FailureText.Text = "Invalid username or password.";
                    ErrorMessage.Visible = true;
                    // Call JS decryption to display name of username in browser
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DecryptTextbox('" + UserName.ClientID + "');", true);

                    Password.Text = "";
                }                
                
            }
        }
}