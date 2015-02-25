using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Test_Website;

public partial class Main : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Load Cases for the doctor
        if (HttpContext.Current.User.IsInRole("Doctor"))
        {
            sqlDSAttentionCases.SelectCommand = " SELECT [CaseId],C.[HashUserId] , P.[Name], [AlertPatient], [AlertDr] "
                + " FROM [Case] C"
                + " INNER JOIN [PersonalInformation] P ON P.[HashUserId] = C.[HashUserId] "
                + "	WHERE [AlertDr] = 1";
            sqlDSAllCases.SelectCommand = " SELECT [CaseId],C.[HashUserId] , P.[Name], [AlertPatient], [AlertDr] "
                + " FROM [Case] C"
                + " INNER JOIN [PersonalInformation] P ON P.[HashUserId] = C.[HashUserId] "
                + "	WHERE [AlertDr] = 0";
        }
        //Load and display cases for Patients
        if (HttpContext.Current.User.IsInRole("Patient"))
        {
            Security security = new Security();
            string hName = security.GetMd5Hash(HttpContext.Current.User.Identity.Name);
            sqlDSAttentionCases.SelectCommand = " SELECT [CaseId], [HashUserId] ,'' as [Name], [AlertPatient], [AlertDr] "
                + " FROM [Case] WHERE [HashUserId] = '" + hName + "' and [AlertPatient] =  1";
            sqlDSAllCases.SelectCommand = " SELECT [CaseId], [HashUserId] ,'' as [Name], [AlertPatient], [AlertDr] "
                + " FROM [Case] WHERE [HashUserId] = '" + hName + "' and [AlertPatient] =  0";

            gvAttentionCases.Columns[1].Visible = false;
            gvAllCases.Columns[1].Visible = false;
        }
        //Display correct messages when there are no case
        if (gvAttentionCases.Rows.Count == 0 && HttpContext.Current.User.IsInRole("Doctor") )
        {
            h4.InnerText = " There are no cases currently requiring your attention.";
            h42.InnerText = "";
        }
        else if (gvAttentionCases.Rows.Count == 0 &&  gvAllCases.Rows.Count == 0)
        {
            h4.InnerText = " You can submit your first consultation request in the New Consult page.";
            h42.InnerText = "";
        }
        else
            h4.InnerText = (gvAttentionCases.Rows.Count == 0) ? "No cases currently require your attention." : " ALERT!!! The following case(s) need your attention";
    }

    // Decrypt Patient full name to show in Gridview
    protected void gvAttentionCases_RowDataBound(object sender, GridViewRowEventArgs e)
    {               
        if ( HttpContext.Current.User.IsInRole("Doctor") && e.Row.RowType.Equals(DataControlRowType.DataRow))
        {
            Security security = new Security();
            e.Row.Cells[1].Text = security.Decrypt(e.Row.Cells[1].Text);
        }
    }

    // Decrypt Patient full name to show in Gridview
    protected void gvAllCases_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (HttpContext.Current.User.IsInRole("Doctor") && e.Row.RowType.Equals(DataControlRowType.DataRow))
        {
            Security security = new Security();
            e.Row.Cells[1].Text = security.Decrypt(e.Row.Cells[1].Text);
        }
    }
}