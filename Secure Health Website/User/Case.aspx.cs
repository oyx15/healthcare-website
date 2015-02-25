using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Test_Website;

public partial class Case : Page
{
    string _caseId = string.Empty;
    System.Data.DataTable caseInfo = new System.Data.DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        var caseId = Request["case"];

        if (string.IsNullOrEmpty(caseId))
        {
            Response.Redirect("~/User/Main");
        }
        _caseId = caseId;


        if (!IsPostBack)
        {
            // Get the case Details
            caseInfo = SQLDataAccess.GetCaseDetails(caseId);
            Security security = new Security();
            if (!HttpContext.Current.User.IsInRole("Doctor") && caseInfo.Rows[0]["HashUserId"].ToString() != security.GetMd5Hash(HttpContext.Current.User.Identity.Name))
                Response.Redirect("~/User/Main");
            if(HttpContext.Current.User.IsInRole("Doctor"))
                h4.InnerText = "Patient: " + security.Decrypt(caseInfo.Rows[0]["Name"].ToString());
            string lastModification = (Convert.ToBoolean(caseInfo.Rows[0]["LastModifiedBy"].ToString())) ? "Doctor" : "Patient";
            LastUpdate.InnerText = "Creation Date: " + caseInfo.Rows[0]["CreationDate"].ToString() + "\n Last update: " + caseInfo.Rows[0]["LastModification"].ToString()
                + "\n Last updated by: " + lastModification;
            txtDescription.Text = caseInfo.Rows[0]["PatientDescription"].ToString();
            txtPresctription.Text = caseInfo.Rows[0]["DrPrescription"].ToString();
           
            //Manage the information displayed according to the roles
            if (HttpContext.Current.User.IsInRole("Patient"))
            {
                divEditPrescription.Visible = false;
                btnSubmitDescription.Visible = false;
                SQLDataAccess.ClearPatientFlag(caseId);
            }
            else if (HttpContext.Current.User.IsInRole("Doctor"))
            {
                divEditDescription.Visible = false;
                PatientComment.Visible = false;
                if (Convert.ToBoolean(caseInfo.Rows[0]["AlertDr"])) 
                {
                        txtPresctription.ReadOnly = false;
                        txtPresctription.Focus();
                        btnEditPrescription.Visible = false;
                }
                else
                    btnSubmitPrescription.Visible = false;  
            }
        }

    }
    protected void EditDescription(object sender, EventArgs e)
    {
        txtDescription.ReadOnly = false;
        btnSubmitDescription.Visible = true;
        btnEditDescription.Visible = false;
    }
    protected void SubmitDescription(object sender, EventArgs e)
    {
        txtDescription.ReadOnly = true;
        btnSubmitDescription.Visible = false;
        btnEditDescription.Visible = true;

        bool result = SQLDataAccess.UpdateCaseDetails(txtDescription.Text, "", _caseId, true);

        caseInfo = SQLDataAccess.GetCaseDetails(_caseId);

        string lastModification = (Convert.ToBoolean(caseInfo.Rows[0]["LastModifiedBy"].ToString())) ? "Doctor" : "Patient";
        LastUpdate.InnerText = "Creation Date: " + caseInfo.Rows[0]["CreationDate"].ToString() + "\n Last update: " + caseInfo.Rows[0]["LastModification"].ToString()
            + "\n Last updated by: " + lastModification;
    }
    protected void EditPrescription(object sender, EventArgs e)
    {
        txtPresctription.ReadOnly = false;
        btnSubmitPrescription.Visible = true;
        btnEditPrescription.Visible = false;
    }
    protected void SubmitPrescription(object sender, EventArgs e)
    {
        txtPresctription.ReadOnly = true;
        btnSubmitPrescription.Visible = false;
        btnEditPrescription.Visible = true;

        bool result = SQLDataAccess.UpdateCaseDetails("", txtPresctription.Text, _caseId, false);

        caseInfo = SQLDataAccess.GetCaseDetails(_caseId);

        string lastModification = (Convert.ToBoolean(caseInfo.Rows[0]["LastModifiedBy"].ToString())) ? "Doctor" : "Patient";
        LastUpdate.InnerText = "Creation Date: " + caseInfo.Rows[0]["CreationDate"].ToString() + "\n Last update: " + caseInfo.Rows[0]["LastModification"].ToString()
            + "\n Last updated by: " + lastModification;
    }
}