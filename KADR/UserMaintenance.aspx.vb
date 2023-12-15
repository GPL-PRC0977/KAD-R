Imports System.Data.SqlClient

Public Class UserMaintenance
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
            cmd = "select * from [KADr].[dbo].[Users]"
            ds = executeQueryRS(cmd)
            If ds.Tables(0).Rows.Count > 0 Then
                html.Append("<table border='1' class='gv_users_list'>")
                html.Append("<th>ID</th>")
                html.Append("<th>USER NAME</th>")
                html.Append("<th>LEVEL</th>")
                html.Append("<th></th>")
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    html.Append("<tr>")
                    html.Append("<td>" & ds.Tables(0).Rows(i)("ID").ToString & "</td>")
                    html.Append("<td>" & ds.Tables(0).Rows(i)("USER_NAME").ToString & "</td>")
                    html.Append("<td>" & ds.Tables(0).Rows(i)("LEVEL").ToString & "</td>")
                    html.Append("<td><a OnClick='if ( ! CreateFile()) return false; show_div();' href='delete.aspx?id=" & ds.Tables(0).Rows(i)("ID").ToString & "' class='delete_css'>Delete</a></td>")
                    html.Append("</tr>")
                Next
                html.Append("</table>")
                Placeholder1.Controls.Add(New Literal() With {.Text = html.ToString()})
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Dim cmd As String
        Dim ds As New DataSet

        If user_txt.Text = "" Or dd_level.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "alert;", "alert('All information are required!');", True)
        Else
            Try
                cmd = "Select * from  [KADr].[dbo].[Users] where [USER_NAME] = '" & Replace(user_txt.Text, "'", "") & "'"
                ds = executeQueryRS(cmd)
                If ds.Tables(0).Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "alert;", "alert('Username is already exist!');", True)
                Else
                    cmd = "insert into [KADr].[dbo].[Users] ([USER_NAME],[LEVEL]) values ('" & Replace(user_txt.Text, "'", "") & "','" & dd_level.Text & "')"
                    executeQueryRS(cmd)
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "alert;", "alert('New user successfully added!');", True)
                    Response.Redirect("UserMaintenance.aspx")
                End If
            Catch ex As Exception

            End Try
        End If

    End Sub
End Class