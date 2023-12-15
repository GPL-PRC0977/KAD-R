<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="emailresend.aspx.vb" Inherits="KADR.emailresend" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <script type = "text/javascript">

        function CreateFile() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("This SO transaction will be send via email. Click OK to proceed.")) {
                return true;
            } else {
                return false;
            }    
        }


        function ShowMsg() {
            alert('There are lines with insufficient quantity.');
        }

        function waiting() {
            document.getElementById("<%=status_txt.ClientID%>").innerHTML = "   Retrieving data please wait...";
            document.getElementById("<%=btn_resend.ClientID%>").disabled = true;
        }

       function email_waiting() {
           document.getElementById("<%=status_txt.ClientID%>").innerHTML = "   Please wait...email is sending...";       
       }

            function enable_sending() {
                document.getElementById("<%=btn_resend.ClientID%>").enabled = true;
            }

            function show_div() {
                document.getElementById("<%=div_full_holder.ClientID%>").hidden = "";
            }

    </script>
    <div class="div_full" id="div_full_holder" runat="server" hidden="hidden">
        <div class="progress_holder">
            <asp:Label runat="server" ID="pw_lbl" CssClass="pw_lbl">Please wait...email is sending...</asp:Label>
            <asp:Image runat="server" ImageUrl="~\images\progress.gif" CssClass="progress_css"/>
        </div>
    </div>

<div class="email_resend_main_div">
    <table border="1" class="email_resend_tbl">
        <asp:HiddenField runat="server" ID="txt_hidden" />
        <tr>
            <td colspan="6"> <asp:Image runat="server" CssClass="email_css" ImageUrl="~\images\email.jpg" /><b class="b_title">SO Transaction Email Sending</b>
                
                <a href="Login.aspx" class="logout">→ Logout</a>
                <a href="Menu.aspx" class="addcustomer">Menu</a>
            </td>
        </tr>
        <tr>
            <td>Input SO Number</td>
            <td style="width: 600px;"><asp:TextBox ID="txt_so" CssClass="txt_so dd_style2" runat="server"></asp:TextBox>&nbsp<asp:Label ID="status_txt" runat="server"></asp:Label></td>
            <td><asp:Button ID="btn_view" Text="View" runat="server" CssClass="btn_view"  OnClientClick="waiting();" /></td>
            <td><asp:Button ID="btn_resend" Text="Send Email" CssClass="btn_view btn_green" runat="server"  OnClientClick="if ( ! CreateFile()) return false; show_div();"  /></td>
        </tr>
    </table>
    <div class="div_holder">
            <asp:GridView ID="gv_data" runat="server" CssClass="gv_data"></asp:GridView>
    </div>

</div>
</asp:Content>
