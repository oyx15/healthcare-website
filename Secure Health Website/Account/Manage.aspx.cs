using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Linq;
using System.Web;
using Test_Website;


public partial class Account_Manage : System.Web.UI.Page
{
    Security security = new Security();

    protected string SuccessMessage
    {
        get;
        private set;
    }

    protected bool CanRemoveExternalLogins
    {
        get;
        private set;
    }

    private bool HasPassword(UserManager manager)
    {
        var user = manager.FindById(User.Identity.GetUserId());
        return (user != null && user.PasswordHash != null);
    }

    protected void Page_Load()
    {
        if (!IsPostBack)
        {
            // Determine the sections to render
            UserManager manager = new UserManager();
            if (HasPassword(manager))
            {
                changePasswordHolder.Visible = true;
            }
            else
            {
                //setPassword.Visible = true;
                changePasswordHolder.Visible = false;
            }
            CanRemoveExternalLogins = manager.GetLogins(User.Identity.GetUserId()).Count() > 1;

            // Render success message
            var message = Request.QueryString["m"];
            if (message != null)
            {
                // Strip the query string from action
                Form.Action = ResolveUrl("~/Account/Manage");

                SuccessMessage =
                    message == "ChangePwdSuccess" ? "Your password has been changed."
                    : message == "SetPwdSuccess" ? "Your password has been set."
                    : message == "RemoveLoginSuccess" ? "The account was removed."
                    : String.Empty;
                successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
            }
	    getProfile();

            // Register JS encryption for buttons
            btnChangePassword.Attributes.Add("onclick", "Encrypt3Textbox('" + CurrentPassword.ClientID + "','" + NewPassword.ClientID +
                "','" + ConfirmNewPassword.ClientID + "');");
            btnUpdate.Attributes.Add("onclick", "Encrypt3Textbox('" + UserName.ClientID +
               "','" + email.ClientID + "','" + phone.ClientID + "');");
        }
    }

    protected void getProfile()
    {
        String UserId;
        System.Data.DataTable PersonalInfo = new System.Data.DataTable();
        UserId = HttpContext.Current.User.Identity.Name;
        PersonalInfo = SQLDataAccess.GetPersonalInformation(security.GetMd5Hash(UserId));
        UserName.Text = PersonalInfo.Rows[0]["Name"].ToString();
        dob.Text = Convert.ToDateTime(PersonalInfo.Rows[0]["DateofBirth"].ToString()).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US"));
        email.Text = PersonalInfo.Rows[0]["Email"].ToString();
        phone.Text = PersonalInfo.Rows[0]["PhoneNumber"].ToString();
        gender.Text = PersonalInfo.Rows[0]["Gender"].ToString();
        age.Text = PersonalInfo.Rows[0]["Age"].ToString();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Decrypt3Textbox('" + UserName.ClientID +
               "','" + email.ClientID + "','"  + phone.ClientID + "');", true);
    }

    protected void Update_Click(object sender, EventArgs e)
    {
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
        bool Gender = Convert.ToBoolean(gender.Text);
        bool insert = SQLDataAccess.UpdatePersonalInfo(security.GetMd5Hash(HttpContext.Current.User.Identity.Name),UserName.Text,dob.Text, email.Text, phone.Text, Gender, Age);
        getProfile();
    }

    protected void ChangePassword_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            UserManager manager = new UserManager();
            IdentityResult result = manager.ChangePassword(User.Identity.GetUserId(), security.Decrypt(CurrentPassword.Text), security.Decrypt(NewPassword.Text));
            if (result.Succeeded)
            {
                var user = manager.FindById(User.Identity.GetUserId());
                IdentityHelper.SignIn(manager, user, isPersistent: false);
                Response.Redirect("~/Account/Manage?m=ChangePwdSuccess");
            }
            else
            {
                AddErrors(result);
            }
        }
    }

    public IEnumerable<UserLoginInfo> GetLogins()
    {
        UserManager manager = new UserManager();
        var accounts = manager.GetLogins(User.Identity.GetUserId());
        CanRemoveExternalLogins = accounts.Count() > 1 || HasPassword(manager);
        return accounts;
    }

    public void RemoveLogin(string loginProvider, string providerKey)
    {
        UserManager manager = new UserManager();
        var result = manager.RemoveLogin(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
        string msg = String.Empty;
        if (result.Succeeded)
        {
            var user = manager.FindById(User.Identity.GetUserId());
            IdentityHelper.SignIn(manager, user, isPersistent: false);
            msg = "?m=RemoveLoginSuccess";
        }
        Response.Redirect("~/Account/Manage" + msg);
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error);
        }
    }
}