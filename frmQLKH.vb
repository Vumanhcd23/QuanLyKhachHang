Public Class frmQLKH
    'khai bao bien de truy xuat du lieu vao DBaccess
    Private _DBAccess As New DataBaseAccess

    'dinh nghia thu tuc load du lieu tu bang KH vao gridview
    Private Sub LoadKH()
        Dim sqlQuery As String = String.Format("SELECT MaKH, TenKH, DiaChi, SDT FROM dbo.KhachHang")
        Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
        Me.dgvKH.DataSource = dTable
        With Me.dgvKH
            .Columns(0).HeaderText = "MaKH"
            .Columns(1).HeaderText = "TenKH"
            .Columns(2).HeaderText = "DiaChi"
            .Columns(3).HeaderText = "SDT"
        End With
    End Sub
    Private Sub frmQLBH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadKH()
    End Sub
    'dinh nghia thu tuc hien thi ket qua tim kiem
    Private Sub SearchKH(value As String)
        Dim sqlQuery As String = String.Format("SELECT MaKH, TenKH, DiaChi, SDT FROM dbo.KhachHang")
        If Me.cboSearch.SelectedIndex = 0 Then
            sqlQuery += String.Format(" where MaKH Like '%{0}%'", value)
        ElseIf Me.cboSearch.SelectedIndex = 1 Then
            sqlQuery += String.Format(" where TenKH Like '%{0}%'", value)
        End If
        Dim dTable As DataTable = _DBAccess.GetDataTable(sqlQuery)
        Me.dgvKH.DataSource = dTable
    End Sub
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        SearchKH(Me.txtSearch.Text)

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim AddKH As New FrmKhachhang(False)
        AddKH.ShowDialog()
        If AddKH.DialogResult = Windows.Forms.DialogResult.Yes Then
            LoadKH()
        End If
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        'khai bao bien lay makh can xoa
        Dim MaKh As String = Me.dgvKH.Rows(Me.dgvKH.CurrentCell.RowIndex).Cells("MaKH").Value
        'cau lenh xoa
        Dim sqlQuery As String = String.Format("DELETE KhachHang where MaKH = '{0}'", MaKh)

        If _DBAccess.ExecuteNoneQuery(sqlQuery) Then
            MessageBox.Show("Xóa thành công!", "", MessageBoxButtons.OK)
            LoadKH()
        Else
            MessageBox.Show("Lỗi!Hãy thư lại", "Error)", MessageBoxButtons.OK)
        End If
    End Sub
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim EditKH As New FrmKhachhang(True)
        With Me.dgvKH
            EditKH.txtMaKH.Text = .Rows(.CurrentCell.RowIndex).Cells("MaKH").Value
            EditKH.txtTenKH.Text = .Rows(.CurrentCell.RowIndex).Cells("TenKH").Value
            EditKH.txtDiaChi.Text = .Rows(.CurrentCell.RowIndex).Cells("DiaChi").Value
            EditKH.txtSDT.Text = .Rows(.CurrentCell.RowIndex).Cells("SDT").Value
        End With
        EditKH.ShowDialog()
        If EditKH.DialogResult = Windows.Forms.DialogResult.Yes Then
            LoadKH()
        End If
    End Sub


End Class