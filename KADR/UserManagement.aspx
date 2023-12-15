<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="UserManagement.aspx.vb" Inherits="KADR.UserManagement" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
            <script type = "text/javascript">

        function CreateFile() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to save the data?")) {
                return true;
            } else {
                return false;
            }    
        }

        function deleteRecord() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to delete this record?")) {
                return true;
            } else {
                return false;
            }
        }

        function ShowMsg() {
            alert('Saving failed. \n All information are required!');
        }       

    </script>
    <div class="user_manage_main_div">
        <table border="1" class="tbl_add_user">
            <tr>
                <td colspan="2">
                    <asp:Image runat="server" CssClass="user_add_image" ImageUrl="~\images\add_user.png" />
                   <b style="margin-left: 40px;"> Customer Management </b> <a href="UserList.aspx" class="addcustomer margin_right" >View List →</a> <a href="Menu.aspx" class="addcustomer margin_right">Menu</a></td>
            </tr>
            <tr>
                <td>Customer Code</td>
                <td><asp:TextBox ID="txt_customercode" runat="server" CssClass="txt_ccode_css" Width="180"></asp:TextBox>
                    <asp:Button ID="btn_find" runat="server" CssClass="btn_find_css"  Text="Find"/>
                    <asp:Button ID="btn_delete_customer" runat="server" CssClass="btn_find_css" Enabled="false"  Text="Delete" OnClientClick="if ( ! deleteRecord()) return false;"/>
                </td>
            </tr>
            <tr>
                <td>Customer Name</td>
                <td><asp:TextBox ID="txt_cust_name" runat="server" CssClass="txt_custname_css" Width="400" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Email</td>
                <td>
                    <asp:TextBox ID="txt_email" runat="server" CssClass="txt_email_css" Width="400" TextMode="Email"></asp:TextBox>
                    <asp:Button ID="btn_add" runat="server" CssClass="btn_add_css"  Text="Add" Enabled="false"/><br />
                    <asp:Label CssClass="lbl_example" ID="lbl_note" runat="server" Text="Ex: jaun.delacruz@primergrp.com"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td colspan="2"> Email List 
<%--                    <asp:Button Text="Remove" runat="server" ID="btn_remove" CssClass="btn_remove_css" />--%>
                    <asp:ImageButton ID="btn_del" runat="server" CssClass="btn_del_css" ImageUrl="~\images\delete.png" ToolTip="Delete Selected Email" />
                    <br /> <asp:ListBox ID="email_list" runat="server" CssClass="email_list_css" SelectionMode="Multiple"></asp:ListBox> <p>
                    <asp:Button ID="btn_cancel" runat="server" CssClass="btn_save_css"  Text="Cancel"/>
                    <asp:Button ID="btn_save" runat="server" CssClass="btn_save_css"  Text="Save" Enabled="false" OnClientClick="if ( ! CreateFile()) return false;"/>
                     
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
