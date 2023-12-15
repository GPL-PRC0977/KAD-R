<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.vb" Inherits="KADR.Login1" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
                <script type = "text/javascript">

        function ShowMsg() {
            alert('Invalid username/password!');
        }
        function inputs() {
            alert('Please provide valid username and password!');
        }

    </script>
    <div class="login_main_div">
        <asp:Image runat="server" CssClass="login_image_bak" ImageUrl="~\images\lock.png" />
        <div class="vert_line"></div>
        <table class="tbl" border="0">
            <tr>
                <td>
                    <asp:Label CssClass="login_lbl" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Username</td>
            </tr>
            <tr>
                <td> <asp:TextBox ID="user_txt" runat="server" CssClass="usertxt_css" placeholder="Ex: juan.delacruz"></asp:TextBox></td>
            </tr>
             <tr>
                <td>Password</td>
            </tr>
            <tr>
                <td><asp:TextBox ID="pass_txt" TextMode="Password" runat="server" CssClass="passtxt_css"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2"> <asp:Button ID="btn_login" runat="server" CssClass="btnlogin_css" Text="Login" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
