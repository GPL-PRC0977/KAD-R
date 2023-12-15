<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="email_resend.aspx.vb" Inherits="KADR.email_resend" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <script type = "text/javascript">

        function CreateFile() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to create NAV text file?")) {
                return true;
            } else {
                return false;
            }    
        }


        function ShowMsg() {
            alert('There are lines with insufficient quantity.');
        }       

    </script>

<div class="email_resend_main_div add_style">
    <table border="1" class="email_resend_tbl">
        <tr>
            <td colspan="6"> <asp:Image runat="server" CssClass="email_css" ImageUrl="~\images\email.jpg" /><b class="b_title">Email Resending</b></td>
        </tr>
        <tr>
            <td colspan="6">
                Date Range &nbsp<asp:TextBox TextMode="Date" ID="txt_date1" CssClass="" runat="server"></asp:TextBox>&nbsp To &nbsp<asp:TextBox TextMode="Date" ID="txt_date2" CssClass="" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Select Company</td>
            <td><asp:DropDownList ID="dd_company" runat="server" CssClass="dd_width dd_style" AutoPostBack="true"></asp:DropDownList></td>
            <td>Select SO Number</td>
            <td><%--<asp:TextBox ID="txt_so" CssClass="txt_so" runat="server"></asp:TextBox>--%><asp:DropDownList ID="dd_so" runat="server" CssClass="dd_width dd_style"></asp:DropDownList></td>
            <td><asp:Button ID="btn_view" Text="View" runat="server" CssClass="btn_view" /></td>
            <td><asp:Button ID="btn_resend" Text="Resend" CssClass="btn_view btn_green" runat="server"  OnClientClick="if ( ! CreateFile()) return false;"  /></td>
        </tr>
    </table>
    <div class="div_holder">
            <asp:GridView ID="gv_data" runat="server" CssClass="gv_data"></asp:GridView>
    </div>

</div>
</asp:Content>
