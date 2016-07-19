Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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

    End Sub

    Private Sub AdvanceGrid1_Load(sender As Object, e As EventArgs)

    End Sub
End Class

'Dim DS As New DataSet1
'Dim TA_CC As New DataSet1TableAdapters.FortollingTableAdapter
'TA_CC.Fill(DS.Fortolling)

'AdvanceGrid1.DataSource = DS.Fortolling