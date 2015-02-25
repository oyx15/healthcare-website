using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.UI;
using Test_Website;

using Microsoft.AspNet.Identity.EntityFramework;

public partial class Account_Register : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Register JS encryption for Login button
            btnRegister.Attributes.Add("onclick", "Encrypt6Textbox('" + UserName.ClientID + "','" + Password.ClientID + "','" + ConfirmPassword.ClientID +
                "','" + email.ClientID + "','" + Name.ClientID + "','" + phone.ClientID + "');");
        }        
    }        
    protected void CreateUser_Click(object sender, EventArgs e)
    {
        Security security = new Security();

        int Age = new int();
        try
        {
            Age = Convert.ToInt16(age.Text);
        }
        catch (Exception)
        {
            ErrorMessage.Text = "Please enter a valid age (number)";
            return;
        }

        var manager = new UserManager();
        var user = new ApplicationUser() { UserName = security.Decrypt(UserName.Text) };
        IdentityResult result = manager.Create(user, security.Decrypt(Password.Text));
        if (result.Succeeded)
        {
            // Access the application context and create result variables.
            ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Create the "patient" role if it doesn't already exist.
            if (!roleMgr.RoleExists("Patient"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "Patient" });
            }
           
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // If the new "canEdit" user was successfully created, 
            // add the "canEdit" user to the "canEdit" role. 
            if (!userMgr.IsInRole(user.Id, "Patient"))
            {
                IdUserResult = userMgr.AddToRole(user.Id, "Patient");
            }

            

            bool Gender = Convert.ToBoolean(gender.Text);
            bool insert = SQLDataAccess.InsertPersonalInfo(security.GetMd5Hash(security.Decrypt(UserName.Text)),Name.Text, dob.Text, email.Text, phone.Text, Gender, Age);
            IdentityHelper.SignIn(manager, user, isPersistent: false);
            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        }
        else
        {
            ErrorMessage.Text = result.Errors.FirstOrDefault();

            // Call JS decryption to display plaintext in browser
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Decrypt4Textbox('" + UserName.ClientID +
                "','" + email.ClientID + "','" + Name.ClientID + "','" + phone.ClientID + "');", true);
            Password.Text = "";
            ConfirmPassword.Text = "";
        }
    }
}