Imports System.Data.SqlClient
Module Module1
    Public Function executeQuery(strSQL As String) As DataSet
        Dim ds As New DataSet

        Dim cn As New SqlConnection(My.Settings.NAVConn.ToString)
        Dim cmd As New SqlCommand(strSQL)
        Dim da As New SqlDataAdapter(cmd)

        Try
            cmd.Connection = cn
            cmd.CommandTimeout = 0
            cn.Open()
            da.Fill(ds)

        Catch ex As Exception
            ds = Nothing
        End Try
        cn.Close()
        cn = Nothing
        Return ds
    End Function
    Public Function executeQueryRS(strSQL As String) As DataSet
        Dim ds As New DataSet

        Dim cn As New SqlConnection(My.Settings.RSConn.ToString)
        Dim cmd As New SqlCommand(strSQL)
        Dim da As New SqlDataAdapter(cmd)

        Try
            cmd.Connection = cn
            cmd.CommandTimeout = 0
            cn.Open()
            da.Fill(ds)

        Catch ex As Exception
            ds = Nothing
        End Try
        cn.Close()
        cn = Nothing
        Return ds
    End Function
    Public Function executeQueryRS_Company(strSQL As String) As DataSet
        Dim ds As New DataSet

        Dim cn As New SqlConnection(My.Settings.RSConn_company.ToString)
        Dim cmd As New SqlCommand(strSQL)
        Dim da As New SqlDataAdapter(cmd)

        Try
            cmd.Connection = cn
            cmd.CommandTimeout = 0
            cn.Open()
            da.Fill(ds)

        Catch ex As Exception
            ds = Nothing
        End Try
        cn.Close()
        cn = Nothing
        Return ds
    End Function
End Module
