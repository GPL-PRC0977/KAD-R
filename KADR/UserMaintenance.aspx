<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserMaintenance.aspx.vb" Inherits="KADR.UserMaintenance" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <script type = "text/javascript">

        function CreateFile() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to delete this user?")) {
                return true;
            } else {
                return false;
            }    
        }

        function AddNew() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to add this user?")) {
                return true;
            } else {
                return false;
            }
        }

    </script>
    <div class="user_maintenance">
        <a href="Menu.aspx" class="back_css">←  Back to Menu</a>
        <label class="UM">User Maintenance</label>
        <table border="1" class="tbl_maintenance">
            <tr>
                <td colspan="2">Username: </td>
            </tr>
            <tr>
                <td><asp:TextBox ID="user_txt" runat="server" CssClass="username_css" placeholder="Ex: juan.delacruz"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">User Level</td>
            </tr>
            <tr>
                <td colspan="2"><asp:DropDownList ID="dd_level" CssClass="dd_level" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="User">User</asp:ListItem>
                    <asp:ListItem Value="Administrator">Administrator</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="2" class="tbl_td"><asp:Button ID="btn_save" runat="server" CssClass="btn_css btn_save_user" Text="Save" OnClientClick="if ( ! AddNew()) return false; show_div();" /></td>
            </tr>
        </table>
        <div class="user_holder">
            <asp:PlaceHolder ID="Placeholder1" runat="server"></asp:PlaceHolder>
        </div>
    </div>
</asp:Content>