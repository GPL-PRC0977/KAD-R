<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Menu.aspx.vb" Inherits="KADR.Menu" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="user_menu_main_div">
        <table border="1" class="tbl_menu">
            <tr>
                <td><asp:ImageButton runat="server" ID="btn_user_manage" CssClass="btn_user_img" ImageUrl="~\images\customer.jpg" /></td>
            </tr>
            <tr>
                <td>Customer Management</td>
            </tr>
        </table>
        <table border="1" class="tbl_menu2">
             <tr>
                <td><asp:ImageButton CssClass="btn_email" ID="btn_email_resend" runat="server" ImageUrl="~\images\email.jpg" /></td>
            </tr>
            <tr>
                <td>Email Resending</td>
            </tr>
        </table>
                <table border="1" class="tbl_menu3">
             <tr>
                <td><asp:ImageButton CssClass="btn_email" ID="ImageButton1" runat="server" ImageUrl="~\images\user.png" /></td>
            </tr>
            <tr>
                <td>User Maintenance</td>
            </tr>
        </table>
        <table border="1" class="tbl_menu4">
             <tr>
                <td><asp:ImageButton CssClass="btn_email" ID="btn_logout" runat="server" ImageUrl="~\images\logout.jpg" /></td>
            </tr>
            <tr>
                <td>Logout</td>
            </tr>
        </table>
    </div>
</asp:Content>