Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.DirectoryServices
Public Class Login1
    Inherits System.Web.UI.Page

    Public Shared username As String
    Public Shared userlevel As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("status") = ""
    End Sub
    Function AuthenticateUser(path As String, user As String, pass As String) As Boolean
        Dim de As New DirectoryEntry(path, user, pass, AuthenticationTypes.Secure)
        Try
            'run a search using those credentials.  
            'If it returns anything, then you're authenticated
            Dim ds As DirectorySearcher = New DirectorySearcher(de)
            ds.FindOne()
            Return True

        Catch
            'otherwise, it will crash out so return false
            Return False

        End Try
    End Function

    Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        Dim user_name As String = Replace(user_txt.Text, "'", "")
        Dim pass_word As String = Replace(pass_txt.Text, "'", "")
        Try
            If user_txt.Text = "" And pass_txt.Text = "" Then
                If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                    Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "inputs();", True)
                End If
            Else
                If AuthenticateUser("LDAP://ldap.primergrp.com", user_name, pass_word) = True Then
                    Session("tmpuser") = "PRIMERGRP\" & Replace(user_name.ToLower, "primergrp\", "")

                    Dim cmd As String
                    Dim ds As New DataSet

                    cmd = "select * from [KADr].[dbo].[Users] where [USER_NAME] = '" & user_name & "'"
                    ds = executeQueryRS(cmd)
                    If ds.Tables(0).Rows.Count > 0 Then
                        Session("status") = "active"
                        Session("level") = ds.Tables(0).Rows(0)("LEVEL").ToString
                        Response.Redirect("Menu.aspx")
                    Else
                        ScriptManager.RegisterStartupScript(Me, Page.GetType(), "alert;", "alert('You dont have access to the system!');", True)
                    End If

                Else
                    If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                        Page.ClientScript.RegisterStartupScript _
                    (Me.GetType(), "alert", "ShowMsg();", True)
                    End If
                End If
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "alert;", "alert('" & ex.Message & "');", True)
        End Try
    End Sub
End Class