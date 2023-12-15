Imports System.Data.SqlClient
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Net.Mail
Public Class emailresend
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("status") = "active" Then
            Response.Redirect("Login.aspx")
        End If
        ' div_full_holder.Visible = False
    End Sub

    Private Sub btn_resend_Click(sender As Object, e As EventArgs) Handles btn_resend.Click
        If txt_so.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "alert;", "alert('Please provide SO Number!');", True)
        Else

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
                Dim getcompany As String() = Replace(txt_so.Text, "'", "").Split("-")

                cmd = "exec [getdata] '" & getcompany(1) & "','" & Replace(txt_so.Text, "'", "") & "'"

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

                contents = contents.Replace("[@SONO@]", Replace(txt_so.Text, "'", ""))
                SendMail(email_recipient, contents, email_subject, xfile)

                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "alert;", "alert('Email Successfully Sent!');", True)
                'div_full_holder.Visible = False
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Page.GetType(), "alert;", "alert('" & ex.Message & "');", True)
            End Try
        End If
    End Sub
    Public Function SendMail(emailRecipient As String, emailbody As String, emailSubject As String, filepath As String) As Integer
        Try
            Dim smtpServer As String = My.Settings.EmailServerNew
            Dim emailSender As String = My.Settings.EmailSender
            Dim emailSenderDisplayName As String = My.Settings.EmailSenderName
            Dim client As New SmtpClient(smtpServer, My.Settings.port_new)
            Dim e_mail As New MailMessage
            Dim mailaddress As String
            client.EnableSsl = False
            e_mail.IsBodyHtml = True
            client.Credentials = New System.Net.NetworkCredential("", "")

            e_mail.From = New MailAddress(emailSender, emailSenderDisplayName)

            Dim sTo As String() = emailRecipient.Split(",")
            For Each mailaddress In sTo
                e_mail.To.Add("" & mailaddress & "")
            Next

            e_mail.Bcc.Add(My.Settings.bcc_email)

            e_mail.Body = emailbody
            e_mail.Subject = emailSubject
            Dim myFile As Net.Mail.Attachment = New Net.Mail.Attachment(filepath)
            e_mail.Attachments.Add(myFile)
            client.Send(e_mail)

            Return 0
        Catch ex As Exception
            Return -1
        End Try

    End Function

    Private Sub btn_view_Click(sender As Object, e As EventArgs) Handles btn_view.Click
        If txt_so.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "alert;", "alert('Please input SO number!');", True)
        Else
            Dim getcompany As String() = txt_so.Text.Split("-")
            gv_data.DataSource = getdata(getcompany(1), Replace(txt_so.Text, "'", ""))
            gv_data.DataBind()
            txt_hidden.Value = gv_data.Rows(0).Cells(3).Text
            status_txt.Text = ""
            btn_resend.Enabled = True
        End If
    End Sub


    Public Function getdata(company As String, so_num As String)
        Dim cmd As String
        Dim ds As New DataSet

        cmd = "exec [getdata] '" & company & "','" & Replace(so_num, "'", "") & "'"
        ds = executeQueryRS(cmd)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If

    End Function

End Class