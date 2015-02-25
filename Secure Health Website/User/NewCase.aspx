<%@ Page Title="New Case" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="NewCase.aspx.cs" Inherits="NewCase" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <div class="row">
        <div class="col-md-8">
            <div class="form-horizontal">
                <h4>How can we help?</h4>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtDescription" CssClass="col-md-2 control-label">Description</asp:Label>
                    <asp:Textbox runat="server" id="txtDescription" CssClass="form-control" rows="5" cols="50" TextMode="MultiLine" ></asp:Textbox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescription" CssClass="text-danger" ErrorMessage="The description field is required." />
                    <p>
                        Please describe your symptoms, how long you have been i'll, and any other useful information to help the doctor make the right prescription.
                    </p>
                </div>
                <div class="form-group">
                    <div>
                        <asp:Button ID="btnSubmit" runat="server" OnClick="Submit" Text="Submit" CssClass="btn btn-default" />
                    </div> 
                </div>
                <div class="form-group">
                    <div>
                        <asp:Label id="lblSuccess" Visible="False" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                    </div> 
                </div>
            </div>
        </div>
    </div>
    
    
</asp:Content>
