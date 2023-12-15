Imports System.Data.Sql
Public Class delete
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cmd As String
        Dim ds As New DataSet

        Try
            cmd = "delete from [KADr].[dbo].[Users] where ID = '" & Request.QueryString("id") & "'"
            executeQueryRS(cmd)
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "alert;", "alert('User is successfully deleted!');", True)
            Response.Redirect("UserMaintenance.aspx")
        Catch ex As Exception

        End Try
    End Sub

End Class