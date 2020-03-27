Imports System.Data.OleDb
Imports System.Data

Public Class frmRegistration
    Private Sub frmRegistration_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Hide()
        FrmMain.Show()

    End Sub

    Dim rdr As OleDbDataReader = Nothing
    Dim dtable As DataTable
    Dim con As OleDbConnection = Nothing
    Dim adp As OleDbDataAdapter
    Dim ds As DataSet
    Dim cmd As OleDbCommand = Nothing
    Dim dt As New DataTable
    Dim cs As String =  "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + "C:\Users\noime dc manalaysay\Desktop\InventorySystem\Sale.accdb"



    Sub fillcombo()
        Try
            Dim Cn As New OleDbConnection(cs)
            Cn.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct (Username) FROM Registration", Cn)
            ds = New DataSet("ds")
            adp.Fill(ds)
            dtable = ds.Tables(0)
            Username.Items.Clear()

            For Each drow As DataRow In dtable.Rows
                Username.Items.Add(drow(0).ToString())
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub frmRegistration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillcombo()
        Try
            con = New OleDbConnection(cs)
            con.Open()
            Dim ct As String = "select Username from registration where username=@find"

            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "Username"))
            cmd.Parameters("@find").Value = Username.Text
            rdr = cmd.ExecuteReader()

            If rdr.Read Then
                MessageBox.Show("UserName Already Exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Username.Text = ""

                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
            Else
                con = New OleDbConnection(cs)
                con.Open()

                Dim cb As String = "insert into registration(username,user_password,name,contactno) Values (@d1,@d2,@d3,@d4)"

                cmd = New OleDbCommand(ct)

                cmd.Connection = con

                cmd.Parameters.Add(New OleDbParameter("@d1", System.Data.OleDb.OleDbType.VarChar, 30, "username"))

                cmd.Parameters.Add(New OleDbParameter("@d2", System.Data.OleDb.OleDbType.VarChar, 30, "user_password"))

                cmd.Parameters.Add(New OleDbParameter("@d3", System.Data.OleDb.OleDbType.VarChar, 30, "name"))

                cmd.Parameters.Add(New OleDbParameter("@d4", System.Data.OleDb.OleDbType.VarChar, 30, "contactno"))

                cmd.Parameters("@d1").Value = Trim(Username.Text)

                cmd.Parameters("@d2").Value = Trim(Password.Text)

                cmd.Parameters("@d3").Value = Trim(TxtName.Text)

                cmd.Parameters("@d4").Value = Trim(ContactNo.Text)

                cmd.ExecuteReader()

                fillcombo()
                con.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub


    Private Sub DSE_ID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Username.SelectedIndexChanged

        Try

            Update_Record.Enabled = True

            con = New OleDbConnection(cs)
            con.Open()

            Dim ct As String = "SELECT * FROM Registration WHERE Username=@find"

            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "Username"))
            cmd.Parameters("@find").Value = Trim(Username.Text)
            rdr = cmd.ExecuteReader()

            If rdr.Read() Then
                Password.Text = Trim(rdr.GetString(1))
                TxtName.Text = Trim(rdr.GetString(2))
                ContactNo.Text = Trim(rdr.GetString(3))
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If


        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub Update_Record_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_Record.Click
        Try
            If Username.Text = "" Then
                MessageBox.Show("Please select user name", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            con = New OleDbConnection(cs)
            con.Open()
            Dim cb As String = "update registration set user_password='" & Password.Text & "',name='" & TxtName.Text & "',contactno='" & ContactNo.Text & "' where username='" & Username.Text & "'"
            cmd = New OleDbCommand(cb)
            cmd.Connection = con
            cmd.ExecuteReader()

            MessageBox.Show("Sucessfully Updated", "User details", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Update_Record.Enabled = False
            fillcombo()
            con.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Username_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Username.SelectedIndexChanged, MyBase.Load
        Try
            Update_Record.Enabled = True

            con = New OleDbConnection(cs)
            con.Open()

            Dim ct As String = "SELECT * FROM Registration WHERE Username=@find"

            cmd = New OleDbCommand(ct)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@find", System.Data.OleDb.OleDbType.VarChar, 30, "Username"))
            cmd.Parameters("@find").Value = Trim(Username.Text)
            rdr = cmd.ExecuteReader()

            If rdr.Read() Then
                Password.Text = Trim(rdr.GetString(1))
                TxtName.Text = Trim(rdr.GetString(2))
                ContactNo.Text = Trim(rdr.GetString(3))
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If


        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub Get_Data_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Get_Details.Click
        frmRegistrationDetails.Show()
        Me.Hide()
    End Sub

    Private Sub ContactNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ContactNo.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then

            e.Handled = True
        End If
    End Sub

    Private Sub Username_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Username.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) _
       Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) _
       And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) _
       Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            'space accepted
            If (Microsoft.VisualBasic.Asc(e.KeyChar) <> 32) Then
                e.Handled = True
            End If
        End If
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then

            e.Handled = False
        End If
    End Sub

    Private Sub delete_records()
        Try
            Dim RowsAffected As Integer = 0
            con = New OleDbConnection(cs)
            con.Open()

            Dim cq As String = "delete from [registration] where Username=@DELETE1:"

            cmd = New OleDbCommand(cq)
            cmd.Connection = con
            cmd.Parameters.Add(New OleDbParameter("@DELETE1", System.Data.OleDb.OleDbType.VarChar, 30, "Username"))

            cmd.Parameters("@DELETE1").Value = Trim(Username.Text)
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected > 0 Then

                MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information)

                MsgBox("sorry no record deleted")
                Username.Text = ""
                Password.Text = ""
                ContactNo.Text = ""
                TxtName.Text = ""

                cmd.ExecuteReader()
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End If

            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub NewRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewRecord.Click
        Username.Text = ""
        Password.Text = ""
        ContactNo.Text = ""
        TxtName.Text = ""
        Update_Record.Enabled = False
    End Sub

End Class
