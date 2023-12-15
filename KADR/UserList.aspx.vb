Imports System.Data.SqlClient
Public Class UserList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("status") = "active" Then
            Response.Redirect("Login.aspx")
        End If

        Dim cmd As String
        Dim ds As New DataSet

        Dim html As New StringBuilder()
        Placeholder1.Controls.Clear()

        Try
            cmd = "select [ID],[CUSTCODE] as [CUSTOMER CODE],[CUSTNAME] as [CUSTOMER NAME],[CUSTEMAIL] as [CUSTOMER EMAIL] from [KADr].[dbo].[CustomerList]"
            ds = executeQueryRS(cmd)
            If ds.Tables(0).Rows.Count > 0 Then
                ' gv_customer.DataSource = ds.Tables(0)
                ' gv_customer.DataBind
                html.Append("<table border='1' class='gv_customer'>")
                html.Append("<th>ID</th>")
                html.Append("<th>CUSTOMER CODE</th>")
                html.Append("<th>CUSTOMER NAME</th>")
                html.Append("<th>CUSTOMER EMAIL</th>")
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    html.Append("<tr>")
                    html.Append("<td>" & ds.Tables(0).Rows(i)("ID").ToString & "</td>")
                    html.Append("<td>" & ds.Tables(0).Rows(i)("CUSTOMER CODE").ToString & "</td>")
                    html.Append("<td>" & ds.Tables(0).Rows(i)("CUSTOMER NAME").ToString & "</td>")
                    html.Append("<td>" & Replace(ds.Tables(0).Rows(i)("CUSTOMER EMAIL").ToString, ",", "<br>") & "</td>")
                    html.Append("</tr>")
                Next
                html.Append("</table>")
                Placeholder1.Controls.Add(New Literal() With {.Text = html.ToString()})
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class