
Imports System.Data.OleDb

Public Class frmSearchProduct1
    Dim rdr As OleDbDataReader = Nothing
    Dim dtable As DataTable
    Dim con As OleDbConnection = Nothing
    Dim adp As OleDbDataAdapter
    Dim ds As DataSet
    Dim cmd As OleDbCommand = Nothing
    Dim dt As New DataTable
    Dim cs As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + "C:\Users\noime dc manalaysay\Desktop\InventorySystem\Sale.accdb"
    Private Sub cmbProductName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProductName.SelectedIndexChanged
          Try


            con = New OleDbConnection(cs)

            con.Open()
            cmd = New OleDbCommand("SELECT (StockID)as [Stock ID],(StockDate)as [Entry Date],(ProductCode) as [Product Code],(ProductName) as [Product Name],(Category) as [Category],(Weight) as [Weight/Qty],(Cartons) as [Cartons],(Packets) as [Packets],(TotalPackets) as [Total Packets] from Stock where Productname = '" & cmbProductName.Text & "'order by ProductName", con)

            Dim myDA As OleDbDataAdapter = New OleDbDataAdapter(cmd)

            Dim myDataSet As DataSet = New DataSet()
            myDA.Fill(myDataSet, "Product")
            myDA.Fill(myDataSet, "Stock")
            DataGridView1.DataSource = myDataSet.Tables("Product").DefaultView
            DataGridView1.DataSource = myDataSet.Tables("Stock").DefaultView
            Dim sum As Int64 = 0
            Dim sum1 As Int64 = 0
            For Each r As DataGridViewRow In Me.DataGridView2.Rows
                sum = sum + r.Cells(6).Value
                sum1 = sum1 + r.Cells(8).Value

            Next
            txtC2.Text = sum
            txtP2.Text = sum1

            con.Close()
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub frmSearchProduct_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillCategory()
        fillProductName()
        fillWeight()
    End Sub
    Sub fillWeight()

        Try

            Dim CN As New OleDbConnection(cs)

            CN.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct  (weight) FROM Stock", CN)
            ds = New DataSet("ds")

            adp.Fill(ds)
            dtable = ds.Tables(0)
            cmbWeight.Items.Clear()

            For Each drow As DataRow In dtable.Rows
                cmbWeight.Items.Add(drow(0).ToString())

                'DocName.SelectedIndex = -1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Sub fillProductName()

        Try

            Dim CN As New OleDbConnection(cs)

            CN.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct  (ProductName) FROM Stock", CN)
            ds = New DataSet("ds")

            adp.Fill(ds)
            dtable = ds.Tables(0)
            cmbProductName.Items.Clear()

            For Each drow As DataRow In dtable.Rows
                cmbProductName.Items.Add(drow(0).ToString())
                'DocName.SelectedIndex = -1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Sub fillCategory()

        Try

            Dim CN As New OleDbConnection(cs)

            CN.Open()
            adp = New OleDbDataAdapter()
            adp.SelectCommand = New OleDbCommand("SELECT distinct  (Category) FROM stock", CN)
            ds = New DataSet("ds")

            adp.Fill(ds)
            dtable = ds.Tables(0)
           
            For Each drow As DataRow In dtable.Rows
                
                'DocName.SelectedIndex = -1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click

        txtProductName.Text = ""
        cmbProductName.Text = ""
        DataGridView1.DataSource = Nothing
        GroupBox2.Visible = False
    End Sub

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click

        txtProductName.Text = ""
        cmbProductName.Text = ""
        GroupBox2.Visible = False
        DataGridView1.DataSource = Nothing
        GroupBox5.Visible = False
        cmbWeight.Text = ""
        DataGridView2.DataSource = Nothing
        GroupBox7.Visible = False
        DataGridView3.DataSource = Nothing
    End Sub



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        GroupBox5.Visible = False
        cmbWeight.Text = ""
        DataGridView2.DataSource = Nothing
    End Sub



    Private Sub cmbWeight_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbWeight.SelectedIndexChanged
        Try


            con = New OleDbConnection(cs)

            con.Open()
            cmd = New OleDbCommand("SELECT (StockID)as [Stock ID],(StockDate)as [Entry Date],(ProductCode) as [Product Code],(ProductName) as [Product Name],(Category) as [Category],(Weight) as [Weight/Qty],(Cartons) as [Cartons],(Packets) as [Packets],(TotalPackets) as [Total Packets] from Stock where Weight = '" & cmbWeight.Text & "'order by weight", con)

            Dim myDA As OleDbDataAdapter = New OleDbDataAdapter(cmd)

            Dim myDataSet As DataSet = New DataSet()
            myDA.Fill(myDataSet, "Product")
            myDA.Fill(myDataSet, "Stock")
            DataGridView2.DataSource = myDataSet.Tables("Product").DefaultView
            DataGridView2.DataSource = myDataSet.Tables("Stock").DefaultView
            Dim sum As Int64 = 0
            Dim sum1 As Int64 = 0
            For Each r As DataGridViewRow In Me.DataGridView2.Rows
                sum = sum + r.Cells(6).Value
                sum1 = sum1 + r.Cells(8).Value

            Next
            txtC2.Text = sum
            txtP2.Text = sum1

            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        GroupBox7.Visible = False
        DataGridView3.DataSource = Nothing
    End Sub



    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            GroupBox7.Visible = True
            con = New OleDbConnection(cs)
            con.Open()
            cmd = New OleDbCommand("SELECT (StockID) as [Stock ID],(Product.ProductCode) as [Product Code],(Product.ProductName) as [Product Name],(Product.category) as [Category],(Product.Weight) as [Weight],(Price) as [Unit Price],sum(Cartons) as [Cartons],Packets as [Packets Per Carton],sum(TotalPackets) as [Total Packets]  from product,Stock  where product.productcode=stock.productcode and cartons > 0  group by Product.ProductCode,Product.Productname,Product.Weight,Product.Category,Product.Price,stockID,Packets order by Product.ProductName", con)
            Dim myDA As OleDbDataAdapter = New OleDbDataAdapter(cmd)
            Dim myDataSet As DataSet = New DataSet()
            myDA.Fill(myDataSet, "Product")
            myDA.Fill(myDataSet, "Stock")
            DataGridView3.DataSource = myDataSet.Tables("Product").DefaultView
            DataGridView3.DataSource = myDataSet.Tables("Stock").DefaultView
            Dim sum As Int64 = 0
            Dim sum1 As Int64 = 0
            For Each r As DataGridViewRow In Me.DataGridView3.Rows
                sum = sum + r.Cells(6).Value
                sum1 = sum1 + r.Cells(8).Value

            Next
            txtC3.Text = sum
            txtP3.Text = sum1

            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtProductName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProductName.TextChanged
        Try
            GroupBox2.Visible = True
            con = New OleDbConnection(cs)
            con.Open()
            cmd = New OleDbCommand("SELECT (StockID) as [Stock ID],(Product.ProductCode) as [Product Code],(Product.ProductName) as [Product Name],(Product.category) as [Category],(Product.Weight) as [Weight],(Price) as [Unit Price],sum(Cartons) as [Cartons],Packets as [Packets Per Carton],sum(TotalPackets) as [Total Packets]  from product,Stock  where product.productcode=stock.productcode and Product.ProductName like '" & txtProductName.Text & "%' group by Product.ProductCode,Product.Productname,Product.Weight,Product.Category,Product.Price,stockID,Packets order by Product.ProductName", con)
            Dim myDA As OleDbDataAdapter = New OleDbDataAdapter(cmd)
            Dim myDataSet As DataSet = New DataSet()
            myDA.Fill(myDataSet, "Product")
            myDA.Fill(myDataSet, "Stock")
            DataGridView1.DataSource = myDataSet.Tables("Product").DefaultView
            DataGridView1.DataSource = myDataSet.Tables("Stock").DefaultView
            Dim sum As Int64 = 0
            Dim sum1 As Int64 = 0
            For Each r As DataGridViewRow In Me.DataGridView1.Rows
                sum = sum + r.Cells(6).Value
                sum1 = sum1 + r.Cells(8).Value

            Next
            txtC1.Text = sum
            txtP1.Text = sum1

            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

  


  

     
    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        Try
            Dim dr As DataGridViewRow = DataGridView1.SelectedRows(0)
            Me.Hide()
            frmSales.Show()
            ' or simply use column name instead of index
            'dr.Cells["id"].Value.ToString();
            frmSales.txtProductCode.Text = dr.Cells(1).Value()
            frmSales.txtProductCode.Text = dr.Cells(2).Value()
            frmSales.txtProductName.Text = dr.Cells(4).Value()
            frmSales.txtWeight.Text = dr.Cells(5).Value()
            frmSales.txtAvailableCartons.Text = dr.Cells(6).Value()
            frmSales.txtAvailablePackets.Text = dr.Cells(8).Value()
            frmSales.txtCartons.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView2_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.RowHeaderMouseClick
        Try
            Dim dr As DataGridViewRow = DataGridView2.SelectedRows(0)
            Me.Hide()
            frmSales.Show()
            ' or simply use column name instead of index
            'dr.Cells["id"].Value.ToString();
            frmSales.txtProductCode.Text = dr.Cells(1).Value()
            frmSales.txtProductCode.Text = dr.Cells(2).Value()
            frmSales.txtProductName.Text = dr.Cells(4).Value()
            frmSales.txtWeight.Text = dr.Cells(5).Value()
            frmSales.txtAvailableCartons.Text = dr.Cells(6).Value()
            frmSales.txtAvailablePackets.Text = dr.Cells(8).Value()
            frmSales.txtCartons.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView3_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView3.RowHeaderMouseClick
        Try
            Dim dr As DataGridViewRow = DataGridView3.SelectedRows(0)
            Me.Hide()
            frmSales.Show()
            ' or simply use column name instead of index
            'dr.Cells["id"].Value.ToString();
            frmSales.txtProductCode.Text = dr.Cells(1).Value()
            frmSales.txtProductCode.Text = dr.Cells(2).Value()
            frmSales.txtProductName.Text = dr.Cells(4).Value()
            frmSales.txtWeight.Text = dr.Cells(5).Value()
            frmSales.txtAvailableCartons.Text = dr.Cells(6).Value()
            frmSales.txtAvailablePackets.Text = dr.Cells(8).Value()
            frmSales.txtCartons.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

  
End Class