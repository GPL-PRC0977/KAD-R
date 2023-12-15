Public Class UserManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("status") = "active" Then
            Response.Redirect("Login.aspx")
        End If

    End Sub

    Private Sub btn_add_Click(sender As Object, e As EventArgs) Handles btn_add.Click
        If txt_email.Text = "" Then
            lbl_note.Text = "<i style='color:red'>Please input a valid email address.</i>"
        Else
            lbl_note.Text = "<label style='color:gray'>Ex: jaun.delacruz@primergrp.com</label>"
            email_list.Items.Add(txt_email.Text)
            txt_email.Text = ""
        End If

    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        Response.Redirect("Menu.aspx")
    End Sub

    Private Sub btn_del_Click(sender As Object, e As ImageClickEventArgs) Handles btn_del.Click
        Dim deletedItems As New List(Of ListItem)()
        For Each item As ListItem In email_list.Items
            If item.Selected Then
                deletedItems.Add(item)
            End If
        Next

        For Each item As ListItem In deletedItems
            email_list.Items.Remove(item)
        Next
    End Sub

    Private Sub btn_delete_customer_Click(sender As Object, e As EventArgs) Handles btn_delete_customer.Click
        Dim cmd As String
        Dim ds As New DataSet

        Try
            cmd = "select * from [KADr].[dbo].[CustomerList] where [CUSTCODE] = '" & Replace(txt_customercode.Text, "'", "") & "'"
            ds = executeQueryRS(cmd)
            If ds.Tables(0).Rows.Count > 0 Then
                cmd = "delete from [KADr].[dbo].[CustomerList] where [CUSTCODE] = '" & Replace(txt_customercode.Text, "'", "") & "'"
                executeQueryRS(cmd)
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), " Thenalert;", "alert('Data successfully deleted!');location.href = 'UserManagement.aspx'", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_find_Click(sender As Object, e As EventArgs) Handles btn_find.Click
        Dim cmd As String
        Dim ds As New DataSet

        Try
            cmd = "select * from [KADr].[dbo].[CustomerList] where [CUSTCODE] = '" & Replace(txt_customercode.Text, "'", "") & "'"
            ds = executeQueryRS(cmd)

            If ds.Tables(0).Rows.Count = 0 Then
                cmd = "select * from [KADr].[dbo].[vw_CustomerCode] where [No_] = '" & Replace(txt_customercode.Text, "'", "") & "'"
                ds = executeQueryRS(cmd)

                If ds.Tables(0).Rows.Count > 0 Then
                    txt_cust_name.Text = ds.Tables(0).Rows(0)("name").ToString
                    btn_add.Enabled = True
                    btn_save.Enabled = True
                Else
                    txt_cust_name.Text = ""
                    email_list.Items.Clear()
                    btn_save.Enabled = False
                    btn_add.Enabled = False
                    btn_delete_customer.Enabled = False
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), " Thenalert;", "alert('Cant find this Customer Code in NAV');", True)
                End If

            Else
                If ds.Tables(0).Rows.Count > 0 Then
                    txt_cust_name.Text = ds.Tables(0).Rows(0)("CUSTNAME").ToString
                    btn_add.Enabled = True
                    btn_save.Enabled = True

                    If Session("level") <> "Administrator" Then
                        btn_delete_customer.Enabled = False
                    Else
                        btn_delete_customer.Enabled = True
                    End If
                Else
                    txt_cust_name.Text = ""
                    email_list.Items.Clear()
                    btn_save.Enabled = False
                End If

                If ds.Tables(0).Rows.Count > 0 Then
                    email_list.Items.Clear()
                    Dim custemails As String = ds.Tables(0).Rows(0)("CUSTEMAIL").ToString
                    Dim split_emails As String() = custemails.Split(",")
                    Dim smail As String

                    For Each smail In split_emails
                        email_list.Items.Add(smail)
                    Next

                Else
                    email_list.Items.Clear()

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Dim cmd As String
        Dim ds As New DataSet
        Dim emails As String
        Try
            If txt_customercode.Text = "" Or txt_cust_name.Text = "" Or email_list.Items.Count = 0 Then
                If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                    Page.ClientScript.RegisterStartupScript _
                (Me.GetType(), "alert", "ShowMsg();", True)
                End If
                txt_customercode.BorderColor = Drawing.Color.Red
                txt_cust_name.BorderColor = Drawing.Color.Red
                txt_email.BorderColor = Drawing.Color.Red
            Else
                For i = 0 To email_list.Items.Count - 1
                    emails = emails & "," & email_list.Items(i).Text
                Next

                cmd = "SELECT * FROM [KADr].[dbo].[CustomerList] WHERE CUSTCODE = '" & Replace(txt_customercode.Text, "'", "") & "'"
                ds = executeQueryRS(cmd)
                If ds.Tables(0).Rows.Count > 0 Then
                    cmd = "UPDATE  [KADr].[dbo].[CustomerList] SET CUSTNAME='" & Replace(txt_cust_name.Text, "'", "") & "',CUSTEMAIL = '" & emails.Remove(0, 1) & "' WHERE CUSTCODE = '" & Replace(txt_customercode.Text, "'", "") & "'"
                    executeQueryRS(cmd)
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), " Thenalert;", "alert('Data successfully updated.');location.href = 'UserManagement.aspx'", True)
                Else
                    cmd = "INSERT INTO [KADr].[dbo].[CustomerList] (CUSTCODE,CUSTNAME,CUSTEMAIL,CREATEDDATE) values ('" & Replace(txt_customercode.Text, "'", "") & "','" & Replace(txt_cust_name.Text, "'", "") & "','" & emails.Remove(0, 1) & "','" & DateTime.Now & "')"
                    executeQueryRS(cmd)
                    txt_customercode.BorderColor = Drawing.Color.Gray
                    txt_customercode.BorderWidth = 1
                    txt_cust_name.BorderColor = Drawing.Color.Gray
                    txt_cust_name.BorderWidth = 1
                    txt_email.BorderColor = Drawing.Color.Gray
                    txt_email.BorderWidth = 1
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), " Thenalert;", "alert('Data successfully saved.');location.href = 'UserManagement.aspx'", True)
                End If

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), " Thenalert;", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

End Class