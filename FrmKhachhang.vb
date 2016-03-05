Public Class frmKhachhang


    'KHAI BAO BIEN TRUY XUAT DULIEU TU DbACCESS
    Private _DBAccess As New DataBaseAccess

    'bien trang thai Add or Edit
    Private _Edit As Boolean = False

    Public Sub New(Edit As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _Edit = Edit
    End Sub

    'Dinh nghia Ham Insert into vào CSDL
    Private Function InsertKH()
        Dim sqlQuery As String = "INSERT INTO  dbo.KhachHang(MaKH,TenKH,DiaChi,SDT)"
        sqlQuery += String.Format("VALUES('{0}','{1}','{2}','{3}')", txtMaKH.Text, txtTenKH.Text, txtDiaChi.Text, txtSDT.Text)
        Return _DBAccess.ExecuteNoneQuery(sqlQuery)
    End Function


    'Dinh nghia ham update vao CSDL
    Private Function UpdateKH()
        Dim sqlQuery As String = String.Format("UPDATE dbo.KhachHang SET TenKH ='{0}', DiaChi = '{1}', SDT = '{2}' WHERE MaKH = '{3}'", _
                                    Me.txtTenKH.Text, Me.txtDiaChi.Text, Me.txtSDT.Text, Me.txtMaKH.Text)
        Return _DBAccess.ExecuteNoneQuery(sqlQuery)
    End Function

    'dinh nghia ham kiem tra gia tri truoc khi insert vao CSDL
    Private Function Trong() As Boolean
        Return String.IsNullOrEmpty(txtMaKH.Text) OrElse String.IsNullOrEmpty(txtTenKH.Text) OrElse String.IsNullOrEmpty(txtDiaChi.Text) OrElse String.IsNullOrEmpty(txtSDT.Text)

    End Function


    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If Trong() Then
            MessageBox.Show("Nhập giá trị vào", "Error", MessageBoxButtons.OK)
        Else
            If _Edit Then
                If UpdateKH() Then
                    MessageBox.Show("Sửa dữ liệu thành công!", "", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.Yes
                Else
                    MessageBox.Show("Lỗi sửa dữ liệu", "Error", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.No
                End If
            Else
                If InsertKH() Then
                    MessageBox.Show("Thêm dữ liệu thành công!", "", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.Yes
                Else
                    MessageBox.Show("Lỗi thêm dữ liệu", "Error", MessageBoxButtons.OK)
                    Me.DialogResult = Windows.Forms.DialogResult.No
                End If
            End If
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class

