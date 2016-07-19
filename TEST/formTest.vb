Public Class formTest

    Private Sub formTest_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Create a new Data Table
        Dim DT As New DataTable

        'Add some columns
        DT.Columns.Add("Col01_String", GetType(String))
        DT.Columns.Add("Col02_Integer", GetType(Integer))
        DT.Columns.Add("Col03_Boolean", GetType(Boolean))
        DT.Columns.Add("Col04_String", GetType(String))
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


        'Load columns properites
        Try
            For Each columnProperty In My.Settings.ColumnsProperties
                Dim colParts() As String = columnProperty.Split(":")
                Dim index As Integer = Val(colParts(0))
                With AdvanceGrid1.Columns(index)
                    .Width = colParts(1)
                    .DisplayIndex = colParts(2)
                    .Visible = colParts(3)
                End With
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        AdvanceGrid1.Arrange()



    End Sub
    Private Sub formTest_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        My.Settings.ColumnsProperties.Clear()
        For Each col As DataGridViewColumn In AdvanceGrid1.Columns
            My.Settings.ColumnsProperties.Add(col.Index & ":" & col.Width & ":" & col.DisplayIndex & ":" & col.Visible)
        Next
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


    Private Sub AdvanceGrid1_ColumnsVisibilityChanged(sender As Object, e As ItemCheckEventArgs) Handles AdvanceGrid1.ColumnsVisibilityChanged
        TextBox3.Text = String.Empty
        For Each col As DataGridViewColumn In AdvanceGrid1.Columns
            TextBox3.Text &= col.Name & ":" & col.Visible & vbCrLf
        Next
    End Sub
End Class
