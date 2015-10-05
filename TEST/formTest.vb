Public Class formTest



    Private Sub formTest_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Create a new Data Table
        Dim DT As New DataTable

        'Add some columns
        DT.Columns.Add("Col01_String", GetType(String))
        DT.Columns.Add("Col02_Integer", GetType(Integer))
        DT.Columns.Add("Col03_Boolean", GetType(Boolean))

        'Add some rows
        DT.Rows.Add("France", 4500, True)
        DT.Rows.Add("Germny", 5100, True)
        DT.Rows.Add("UK", 9515, False)
        DT.Rows.Add("Finland", 1400, True)
        DT.Rows.Add("Italy", 6730, False)
        DT.Rows.Add("Norway", 1500, True)

        'Bind the AdvanceGrid's data source to your table
        AdvanceGrid1.DataSource = DT

        AdvanceGrid1.Dock = DockStyle.Left

    End Sub


    Private Sub AdvanceGrid1_TextChanged(sender As Object) Handles AdvanceGrid1.TextChanged

        'Display the filter in TextBox1
        TextBox1.Text = AdvanceGrid1.Filter

        'Diplay the Search values in TextBox2
        TextBox2.Text = ""
        For Each SearchValue As KeyValuePair(Of String, String) In AdvanceGrid1.SearcheValues
            TextBox2.Text &= SearchValue.Key & ":" & SearchValue.Value & vbCrLf
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Programaticaly search the grid by assigning values to one or more columns and calling the search function.
        AdvanceGrid1.SearcheValues("Col01_String") = "France"
        AdvanceGrid1.Search()
    End Sub



End Class
