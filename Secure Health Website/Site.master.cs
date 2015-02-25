using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;

    protected void Page_Init(object sender, EventArgs e)
    {
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Redirect to use HTTPS
            if (!string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name) || !(HttpContext.Current.Request.RawUrl == "/" || HttpContext.Current.Request.RawUrl == "/About" || HttpContext.Current.Request.RawUrl == "/Contact"))
            {
                if (HttpContext.Current.Request.IsSecureConnection.Equals(false) && HttpContext.Current.Request.IsLocal.Equals(true))
                {
                    string uri = Request.ServerVariables["HTTP_HOST"];
                    string httpsLink = "https://" + uri.Substring(0, (uri.IndexOf(':') != -1) ? uri.IndexOf(':') : uri.Length) + ":44300"
                          + HttpContext.Current.Request.RawUrl;
                    Response.Redirect(httpsLink);
                }
                if (HttpContext.Current.Request.IsSecureConnection.Equals(false) && HttpContext.Current.Request.IsLocal.Equals(false))
                {
                    Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"]
                    + HttpContext.Current.Request.RawUrl);
                }
            }
            
            if (!string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                lnkMain.Visible = true;
            }
            if (HttpContext.Current.User.IsInRole("Patient"))
            {
                lnkNewCase.Visible = true;
                lnkMain.InnerText = "Patient Main";

            }
            if (HttpContext.Current.User.IsInRole("Doctor"))
            {             
                lnkMain.InnerText = "Doctor Main";
            }
        }
    }

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        // Redirect the application upon logout
        Context.GetOwinContext().Authentication.SignOut();
        if (HttpContext.Current.Request.IsSecureConnection.Equals(true) && HttpContext.Current.Request.IsLocal.Equals(true))
        {
            string uri = Request.ServerVariables["HTTP_HOST"];
            string httpLink = "http://" + uri.Substring(0, (uri.IndexOf(':') != -1) ? uri.IndexOf(':') : uri.Length) + ":60013/";
            Response.Redirect(httpLink);
        }
        if (HttpContext.Current.Request.IsSecureConnection.Equals(true) && HttpContext.Current.Request.IsLocal.Equals(false))
        {
            Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"] +  HttpContext.Current.Request.RawUrl);
        }
    }
}