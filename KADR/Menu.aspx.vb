Public Class Menu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("status") = "active" Then
            Response.Redirect("Login.aspx")
        End If
    End Sub

    Private Sub btn_email_resend_Click(sender As Object, e As ImageClickEventArgs) Handles btn_email_resend.Click
        If My.Settings.email_resend_page = 1 Then
            Response.Redirect("email_resend.aspx")
        Else
            Response.Redirect("emailresend.aspx")
        End If

    End Sub

    Private Sub btn_logout_Click(sender As Object, e As ImageClickEventArgs) Handles btn_logout.Click
        Response.Redirect("Login.aspx")
    End Sub

    Private Sub btn_user_manage_Click(sender As Object, e As ImageClickEventArgs) Handles btn_user_manage.Click
        Response.Redirect("UserManagement.aspx")
    End Sub

    Private Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If Session("level") <> "Administrator" Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "alert;", "alert('You are not allowed to access this module!');", True)
        Else
            Response.Redirect("UserMaintenance.aspx")
        End If

    End Sub
End Class