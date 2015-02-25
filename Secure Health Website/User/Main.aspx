<%@ Page Title="Welcome" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %> to SHW.</h2>
    <div class="row">
        <div class="col-md-8">
            <div class="form-horizontal">
                <h4 runat="server" id="h4"> ALERT!!! The following case(s) need your attention</h4>
                <div class="form-group">
                    
                    <asp:GridView ID="gvAttentionCases" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CaseId" DataSourceID="sqlDSAttentionCases" OnRowDataBound="gvAttentionCases_RowDataBound">
                        <Columns>
                            <%--<asp:CommandField ShowSelectButton="True" />--%>
                            <asp:HyperLinkField HeaderText="Case Id" DataTextField="CaseId" SortExpression="CaseId"
                                DataNavigateUrlFields="CaseId" DataNavigateUrlFormatString="~/User/Case.aspx?case={0}"></asp:HyperLinkField>
                            <asp:BoundField DataField="Name" HeaderText="Patient Name" SortExpression="Name" />
                            <asp:BoundField DataField="HashUserId" HeaderText="User Id" SortExpression="HashUserId" Visible="false" />
                            <%--<asp:CheckBoxField DataField="AlertPatient" HeaderText="AlertPatient" SortExpression="AlertPatient" />
                            <asp:CheckBoxField DataField="AlertDr" HeaderText="AlertDr" SortExpression="AlertDr" />--%>
                            
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqlDSAttentionCases" runat="server" ConnectionString="<%$ ConnectionStrings:SHWConnection %>" SelectCommand="SELECT [CaseId], [HashUserId], [AlertPatient], [AlertDr] FROM [Case]"></asp:SqlDataSource>
                    
                </div>
                <div runat="server" id="divPrevious">
                     <h4 runat="server" id="h42"> All previousely submitted cases: </h4>
                     <div class="form-group">
                         <asp:GridView ID="gvAllCases" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CaseId" DataSourceID="sqlDSAllCases" OnRowDataBound="gvAllCases_RowDataBound">
                        <Columns>
                            <%--<asp:CommandField ShowSelectButton="True" />--%>
                            <asp:HyperLinkField HeaderText="Case Id" DataTextField="CaseId" SortExpression="CaseId"
                                DataNavigateUrlFields="CaseId" DataNavigateUrlFormatString="~/User/Case.aspx?case={0}"></asp:HyperLinkField>
                            <asp:BoundField DataField="Name" HeaderText="Patient Name" SortExpression="Name" />
                            <asp:BoundField DataField="HashUserId" HeaderText="User Id" SortExpression="HashUserId" Visible="false" />
                            <%--<asp:CheckBoxField DataField="AlertPatient" HeaderText="AlertPatient" SortExpression="AlertPatient" />
                            <asp:CheckBoxField DataField="AlertDr" HeaderText="AlertDr" SortExpression="AlertDr" />--%>
                            
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqlDSAllCases" runat="server" ConnectionString="<%$ ConnectionStrings:SHWConnection %>" SelectCommand="SELECT [CaseId], [HashUserId], [AlertPatient], [AlertDr] FROM [Case]"></asp:SqlDataSource>
                    </div>
                </div>
               
            </div>
        </div>
    </div>
</asp:Content>
