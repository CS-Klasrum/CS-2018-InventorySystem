Imports System.Data.OleDb
Imports System.Security.Cryptography
Imports System.Text

Public Class frmStock
    Dim rdr As OleDbDataReader = Nothing
    Dim dtable As DataTable
    Dim con As OleDbConnection = Nothing
    Dim adp As OleDbDataAdapter
    Dim ds As DataSet
    Dim cmd As OleDbCommand = Nothing
    Dim dt As New DataTable
    Dim cs As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source" = "C:\Users\noime dc manalaysay\Desktop\InventorySystem\Sale.accdb"

    Private Sub frmStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DataGridView1.DataSource = GetData()
    End Sub
    Sub Clear()
        txtStockID.Text = ""
        txtCartons.Text = ""
        txtCategory.Text = ""
        txtPackets.Text = ""
        txtWeight.Text = ""
        txtProductCode.Text = ""
        txtProductName.Text = ""
        txtTotalPackets.Text = ""
        dtpStockDate.Text = Today
        Button2.Focus()

    End Sub

    Private Const ConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\SI_DB.accdb;Persist Security Info=False;"
    Private ReadOnly Property Connection() As OleDbConnection
        Get
            Dim ConnectionToFetch As New OleDbConnection(ConnectionString)
            ConnectionToFetch.Open()
            Return ConnectionToFetch
        End Get
    End Property

    Public Function GetData() As DataView
        Dim SelectQry = "SELECT (ProductCode) as [Product Code],(ProductName) as [Product Name],(Weight) as [Weight],sum(Cartons) as [Cartons],Packets,Sum(TotalPackets) as [Total Packets] FROM stock where Cartons > 0 and TotalPackets > 0   group by ProductCode,ProductName,Weight,Packets order by ProductName "
        Dim SampleSource As New DataSet
        Dim TableView As DataView
        Try
            Dim SampleCommand As New OleDbCommand()
            Dim SampleDataAdapter = New OleDbDataAdapter()
            SampleCommand.CommandText = SelectQry
            SampleCommand.Connection = Connection
            SampleDataAdapter.SelectCommand = SampleCommand
            SampleDataAdapter.Fill(SampleSource)
            TableView = SampleSource.Tables(0).DefaultView
        Catch ex As Exception
            Throw ex
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return TableView
    End Function


    Private Sub txtPackets_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPackets.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then

            e.Handled = True

        End If
    End Sub

    Private Sub txtPackets_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackets.TextChanged
        txtTotalPackets.Text = CInt(Val(txtCartons.Text) * Val(txtPackets.Text))
    End Sub

    Private Sub txtCartons_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCartons.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then

            e.Handled = True

        End If
    End Sub

    Private Sub txtCartons_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCartons.TextChanged
        txtTotalPackets.Text = CInt(Val(txtCartons.Text) * Val(txtPackets.Text))
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Clear()
        frmProductsRecord.DataGridView4.DataSource = Nothing
        frmProductsRecord.cmbCategory.Text = ""
        frmProductsRecord.cmbWeight.Text = ""
        frmProductsRecord.DataGridView3.DataSource = Nothing
        frmProductsRecord.cmbProductName.Text = ""
        frmProductsRecord.txtProduct.Text = ""
        frmProductsRecord.DataGridView2.DataSource = Nothing
        frmProductsRecord.DataGridView1.DataSource = Nothing
        frmProductsRecord.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Clear()
        frmStockDetails1.fillCategory()
        frmStockDetails1.fillProduct()
        frmStockDetails1.fillWeight()
        frmStockDetails1.cmbProductName.Text = ""
        frmStockDetails1.txtProduct.Text = ""
        frmStockDetails1.DataGridView2.DataSource = Nothing
        frmStockDetails1.cmbCategory.Text = ""
        frmStockDetails1.DataGridView3.DataSource = Nothing
        frmStockDetails1.cmbWeight.Text = ""
        frmStockDetails1.DataGridView4.DataSource = Nothing
        frmStockDetails1.DataGridView1.DataSource = Nothing
        frmStockDetails1.Show()
    End Sub

    Private Sub frmStock_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Hide()
        FrmMain.Show()
    End Sub
End Class