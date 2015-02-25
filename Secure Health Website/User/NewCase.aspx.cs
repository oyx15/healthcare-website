using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Test_Website;

public partial class NewCase : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Submit(object sender, EventArgs e)
    {
        // Hash the username and register the case to get the case ID
        Security security =  new Security();
        string hName = security.GetMd5Hash(HttpContext.Current.User.Identity.Name);
        string caseId = SQLDataAccess.RegisterCaseAndGetID(hName, txtDescription.Text);

        if (!string.IsNullOrEmpty(caseId))
        {
            txtDescription.Enabled = false;
            btnSubmit.Enabled = false;
            lblSuccess.Text = "Your information has been sent to the doctor. Your case Id is: " + caseId;
            lblSuccess.Visible = true;
        }
    }
}