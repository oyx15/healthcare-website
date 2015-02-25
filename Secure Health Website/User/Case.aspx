<%@ Page Title="Case" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Case.aspx.cs" Inherits="Case" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %><%:" " + Request["case"] %>.</h2>
    <div class="col-md-8" runat="server" id="LastUpdate">
    </div>
    <div class="row">
        <div class="col-md-8">
            <div class="form-horizontal">
                <h4 runat="server" id="h4">How can we help?</h4>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtDescription" CssClass="col-md-2 control-label">Description</asp:Label>
                    <asp:Textbox runat="server" id="txtDescription" CssClass="form-control" rows="5" cols="50" TextMode="MultiLine" ReadOnly="true" ></asp:Textbox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescription" CssClass="text-danger" ErrorMessage="The description field is required." />
                    <p runat="server" id="PatientComment">
                        Please describe your symptoms, how long you have been i'll, and any other useful information to help the doctor make the right prescription.
                    </p>
                </div>
                <div runat="server" id="divEditDescription" class="form-group">
                    <div>
                        <asp:Button ID="btnEditDescription" runat="server" OnClick="EditDescription" Text="Edit" CssClass="btn btn-default" />
                        <asp:Button ID="btnSubmitDescription" runat="server" OnClick="SubmitDescription" Text="Submit" CssClass="btn btn-default" />
                    </div> 
                </div>
                 <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtPresctription" CssClass="col-md-2 control-label">Prescription</asp:Label>
                    <asp:Textbox runat="server" id="txtPresctription" CssClass="form-control" rows="5" cols="50" TextMode="MultiLine" ReadOnly="true" ></asp:Textbox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPresctription" CssClass="text-danger" ErrorMessage="The description field is required." />
                </div>
                <div runat="server" id="divEditPrescription" class="form-group">
                    <div>
                        <asp:Button ID="btnEditPrescription" runat="server" OnClick="EditPrescription" Text="Edit" CssClass="btn btn-default" />
                        <asp:Button ID="btnSubmitPrescription" runat="server" OnClick="SubmitPrescription" Text="Submit" CssClass="btn btn-default" />
                    </div> 
                </div>
                <div class="form-group">
                    <div>
                        <asp:Label id="lblSuccess" Visible="false" runat="server"></asp:Label>
                    </div> 
                </div>
            </div>
        </div>
    </div>
</asp:Content>
