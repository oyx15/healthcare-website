<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new account.</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">User name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                    CssClass="text-danger" ErrorMessage="The user name field is required." />
            </div>
        </div>
         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-2 control-label">Full Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Name" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                    CssClass="text-danger" ErrorMessage="The user name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
        </div>
        <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
           
     <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="dob" CssClass="col-md-2 control-label">Date of Birth</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="dob" TextMode="Date" value="yyyy-mm-dd" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="dob"
                    CssClass="text-danger" ErrorMessage="We require your Date of Birth!" />
           </div>
            </div>
             <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="email" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="email" TextMode="SingleLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="email"
                    CssClass="text-danger" ErrorMessage="Please provide your Email ID" />
            </div>
                 </div>

       <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="phone" CssClass="col-md-2 control-label">Phone Number</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="phone" TextMode="Phone" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="phone"
                    CssClass="text-danger" ErrorMessage="What's your phone number?" />
            </div>              
       </div>

           <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="gender" CssClass="col-md-2 control-label">Gender</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="gender" CssClass="form-control">
                    <asp:ListItem Text="M" Value="False"></asp:ListItem>
                     <asp:ListItem Text="F" Value="True"></asp:ListItem>
                </asp:DropDownList> 
                <asp:RequiredFieldValidator runat="server" ControlToValidate="phone"
                    CssClass="text-danger" ErrorMessage="Kindly provide your gender" />
            </div>              
       </div>
         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="age" CssClass="col-md-2 control-label">Age</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="age" TextMode="SingleLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="age"
                    CssClass="text-danger" ErrorMessage="How old are you?" />
            </div>              
       </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btnRegister" runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>

