<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserList.aspx.vb" Inherits="KADR.UserList" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <!DOCTYPE html>

    <div class="user_list_css">
        <label class="cust_list_lbl"><b>Customer List</b></label>
        <a href="UserManagement.aspx" class="usermanagement">← Back</a>
        <div class="wrapper">
         <%--   <asp:GridView ID="gv_customer" runat="server" CssClass="gv_customer"></asp:GridView>--%>
            <asp:PlaceHolder ID="Placeholder1" runat="server"></asp:PlaceHolder>
        </div>
        
    </div>
</asp:Content>