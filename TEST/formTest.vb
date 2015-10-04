Public Class formTest



    Private Sub formTest_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim DT As New DataTable
        DT.Columns.Add("Col01_String", GetType(String))
        DT.Columns.Add("Col02_Integer", GetType(Integer))
        DT.Columns.Add("Col03_Boolean", GetType(Boolean))

        DT.Rows.Add("France", 4500, True)
        DT.Rows.Add("Germny", 5100, True)
        DT.Rows.Add("UK", 9515, False)
        DT.Rows.Add("Finland", 1400, True)
        DT.Rows.Add("Italy", 6730, False)
        DT.Rows.Add("Norway", 1500, True)

        AdvanceGrid1.DataSource = DT

        MessageBox.Show("Done loading")

    End Sub
End Class
