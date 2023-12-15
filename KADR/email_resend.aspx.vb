Imports System.Data.SqlClient
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Net.Mail
Public Class email_resend
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cmd As String
        Dim ds As New DataSet

        Try
            cmd = "select [company code] from [Reports].[dbo].[t_company]"
            ds = executeQueryRS_Company(cmd)
            If ds.Tables(0).Rows.Count > 0 Then
                If dd_company.Text = "" Then
                    dd_company.Items.Clear()
                    dd_company.Items.Add("")
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        dd_company.Items.Add(ds.Tables(0).Rows(i)("Company Code").ToString)
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_resend_Click(sender As Object, e As EventArgs) Handles btn_resend.Click
        Dim sPath As String = Server.MapPath("\EmailBody\email.txt")

        Dim objStreamReader As StreamReader
        objStreamReader = File.OpenText(sPath)
        Dim contents As String = objStreamReader.ReadToEnd()
        objStreamReader.Close()

        Dim xls As Microsoft.Office.Interop.Excel.Application
        Dim xlsWorkBook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlsWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value

        Try
            xls = New Microsoft.Office.Interop.Excel.Application
            xlsWorkBook = xls.Workbooks.Add
            xlsWorkSheet = xlsWorkBook.Sheets(1)

            xlsWorkSheet.Range("a1").ColumnWidth = 25
            xlsWorkSheet.Range("b1").ColumnWidth = 15
            xlsWorkSheet.Range("c1").ColumnWidth = 15
            xlsWorkSheet.Range("d1").ColumnWidth = 30
            xlsWorkSheet.Range("e1").ColumnWidth = 10
            xlsWorkSheet.Range("f1").ColumnWidth = 10
            xlsWorkSheet.Range("g1").ColumnWidth = 10

            xlsWorkSheet.Cells(1, 1) = "Destination Name"
            xlsWorkSheet.Cells(1, 2) = "Source No."
            xlsWorkSheet.Cells(1, 3) = "Item No_"
            xlsWorkSheet.Cells(1, 4) = "Item Description"
            xlsWorkSheet.Cells(1, 5) = "Quantity"
            xlsWorkSheet.Cells(1, 6) = "Unit Price"
            xlsWorkSheet.Cells(1, 7) = "Total Gross Amount"

            Dim cmd As String
            Dim ds As New DataSet

            cmd = "exec [getdata] '" & dd_company.Text & "','" & dd_so.Text & "'"

            ds = executeQueryRS(cmd)

            Dim x As Integer = 2
            Dim email_subject = ds.Tables(0).Rows(0)("Name").ToString
            Dim custcode As String = ds.Tables(0).Rows(0)("Customer No").ToString
            For i = 0 To ds.Tables(0).Rows.Count - 1
                xlsWorkSheet.Cells(x, 1) = ds.Tables(0).Rows(i)("Name")
                xlsWorkSheet.Cells(x, 2) = ds.Tables(0).Rows(i)("Source No")
                xlsWorkSheet.Cells(x, 3) = ds.Tables(0).Rows(i)("Item No")
                xlsWorkSheet.Cells(x, 4) = ds.Tables(0).Rows(i)("Description")
                xlsWorkSheet.Cells(x, 5) = ds.Tables(0).Rows(i)("Qty")
                xlsWorkSheet.Cells(x, 6) = ds.Tables(0).Rows(i)("Unit Price")
                xlsWorkSheet.Cells(x, 7) = ds.Tables(0).Rows(i)("Gross Amount")
                x = x + 1
            Next
            Dim filename As String = DateTime.Now.ToString("MMddyyyy") & "-" & DateTime.Now.ToString("HHmmss")
            Dim xfile As String
            xfile = Server.MapPath("~\Attachment") & "\" & filename & ".xlsx"
            xls.DisplayAlerts = False
            xlsWorkBook.SaveAs(xfile)
            xlsWorkBook.Close()
            xls.Quit()

            cmd = "select custemail from [KADr].[dbo].[CustomerList] where custcode = '" & custcode & "'"
            ds = executeQueryRS(cmd)
            Dim email_recipient As String
            If ds.Tables(0).Rows.Count > 0 Then
                email_recipient = ds.Tables(0).Rows(0)("CUSTEMAIL").ToString
            End If

            contents = contents.Replace("[@SONO@]", dd_so.Text)
            SendMail(email_recipient, contents, email_subject, xfile)
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "alert;", "alert('Email Successfully Send!');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "alert;", "alert('" & ex.Message & "');", True)
        End Try


    End Sub
    Public Function SendMail(emailRecipient As String, emailbody As String, tmpsub As String, filepath As String) As Integer
        Try
            Dim smtpServer As String = My.Settings.EmailServerNew
            Dim emailSender As String = My.Settings.EmailSender
            Dim emailSenderDisplayName As String = My.Settings.EmailSenderName
            Dim emailSubject As String = tmpsub
            Dim client As New SmtpClient(smtpServer, My.Settings.port_new)
            client.EnableSsl = False
            client.Credentials = New System.Net.NetworkCredential("", "")
            Dim [from] As New MailAddress(emailSender, emailSenderDisplayName)
            Dim sMail As String
            Dim sTo As String() = emailRecipient.Split(","c)
            Dim [to] As New MailAddress("gilbert.laman@primergrp.com")

            Dim message As New MailMessage([from], [to])

            For Each sMail In sTo
                message.Bcc.Add("" & sMail & "")
            Next

            message.IsBodyHtml = True
            message.CC.Add(My.Settings.bcc_email)

            message.Body = emailbody
            message.Subject = emailSubject
            Dim myFile As Net.Mail.Attachment = New Net.Mail.Attachment(filepath)
            message.Attachments.Add(myFile)

            client.Send(message)


            Return 0
        Catch ex As Exception

            Return -1

        End Try

    End Function

    Private Sub btn_view_Click(sender As Object, e As EventArgs) Handles btn_view.Click
        gv_data.DataSource = getdata(dd_company.Text, dd_so.Text)
        gv_data.DataBind()
    End Sub

    Private Sub dd_company_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dd_company.SelectedIndexChanged
        Dim cmd As String
        Dim ds As New DataSet

        Try
            cmd = "exec  [get_distinct_SO] '" & dd_company.Text & "'"
            ds = executeQueryRS(cmd)
            If ds.Tables(0).Rows.Count > 0 Then
                If dd_so.Text = "" Then
                    dd_so.Items.Clear()
                    dd_so.Items.Add("")
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        dd_so.Items.Add(ds.Tables(0).Rows(i)("SourceNo").ToString)
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function getdata(company As String, so_num As String)
        Dim cmd As String
        Dim ds As New DataSet

        cmd = "exec [getdata] '" & dd_company.Text & "','" & dd_so.Text & "'"
        ds = executeQueryRS(cmd)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If

    End Function

End Class