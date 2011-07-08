<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="DnDHelper.Web.Admin.UserManagement" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Admin - User Management</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>User Management</h2>
<div id="usersPanel">
    <h4>Użytkownicy:</h4>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/Scripts/jquery-1.4.1.js" />
        <asp:ScriptReference Path="~/Scripts/jquery-ui-1.8.14.custom.min.js" />
        <asp:ScriptReference Path="~/Scripts/userManagement.js" />
    </Scripts>
    <Services>
        <asp:ServiceReference Path="~/Admin/UserManagementService.asmx" />
    </Services>
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <p>Wyszukiwanie po nazwie: 
            <asp:TextBox ID="tbSearchName" runat="server"></asp:TextBox>
            <asp:Button ID="Button1"
                runat="server" Text="Szukaj" onclick="Button1_Click1" /></p>
            <asp:GridView BorderWidth="1" HeaderStyle-CssClass="tableHeader1" 
                RowStyle-CssClass="tableRow1" SelectedRowStyle-CssClass="tableRow1Selected" AlternatingRowStyle-CssClass="tableRow1Alternate"
                CellPadding="2" ID="usersGrid" runat="server" GridLines="Both" 
                DataSourceID="usersSource" PageSize="6" AllowPaging="true" 
                AutoGenerateColumns="false" width="100%" 
                onrowdatabound="usersGrid_RowDataBound" onselectedindexchanged="usersGrid_SelectedIndexChanged" 
                >
                <Columns>
                    <asp:BoundField DataField="ProviderUserKey" HeaderText="Id" />
                    <asp:BoundField DataField="UserName" HeaderText="Nazwa" />
                    <asp:BoundField DataField="LastLoginDate" HeaderText="Ostatnie logowanie" />
                    <asp:CommandField ButtonType="Link" SelectText="" ShowSelectButton="true" Visible="false" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="usersSource" runat="server" TypeName="DnDHelper.Web.Admin.MembershipDataHelper" SelectMethod="GetAllUsers" MaximumRowsParameterName="pageSize" SelectCountMethod="GetUsersCount" EnablePaging="true" StartRowIndexParameterName="startIndex" >
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="newUserDialog" title="Nowy użytkownik">
        <div id="errorMessage" class="errorMessage" style="display:none" ></div>
        <ul>
            <li>Nazwa użytkownika: 
                <asp:TextBox ID="tbUserName" runat="server" MaxLength="30"></asp:TextBox></li>
            <li>Hasło: 
                <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" MaxLength="30"></asp:TextBox>
                </li>
        </ul>
    </div>
    <p style="text-align:right"><a href="#" onclick="$('#newUserDialog').dialog('open'); return false">Nowy użytkownik</a></p>
</div>
<div id="rolesPanel">
    <h2>Roles</h2>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table style="width: 100%;">
            <tr>
                <th style="width:40%">Wszystkie</th><th style="width:20%"></th></th><th style="width:40%">Te do których należy użytkownik</th>
            </tr>
            <tr>
                <td>
                    <asp:ListBox ID="allRoles" runat="server" Width="100%" DataSourceID="rolesDataSource"></asp:ListBox>
                    <asp:ObjectDataSource ID="rolesDataSource" runat="server" TypeName="DnDHelper.Web.Admin.MembershipDataHelper" SelectMethod="GetAllRoles"></asp:ObjectDataSource>
                </td>
                <td style="text-align:center">
                    <asp:Button ID="addRole" runat="server" Text="Dodaj rolę" 
                        onclick="addRole_Click" /><br />
                    <asp:Button ID="removeRole" runat="server" Text="Usuń rolę" 
                        onclick="removeRole_Click" /><br />
                    <asp:Button ID="removeUser" runat="server" Text="Usuń użytkownika" 
                        onclick="removeUser_Click" />
                </td>
                <td>
                    <asp:ListBox ID="userRoles" runat="server" Width="100%"></asp:ListBox>
                </td>
            </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
    
</asp:Content>
