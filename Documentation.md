# Usage

Using AdvanceGrid (AG for short) is simple and straight forward.


## To use AdvanceGrid in your WinForm do the following:


# Add AG to your WinForm
# Create a DataTable from any source, SQL, Excel, XML, manual,... etc.
# Assign that DataTable to the AG's DataSource
# Set AG properties (Optional) 

{code:vb.net}
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
{code:vb.net}

## TextChanged event and Filter

Whenever text changes in any of the filter Textboxes above the grid, the TextChanged event fires up.
I'm using this event here to track down the Filter property of the AG and showing it in a Multiline Textbox.

{code:vb.net}
 Private Sub AdvanceGrid1_TextChanged(sender As Object) Handles AdvanceGrid1.TextChanged

        'Display the filter in TextBox1
        TextBox1.Text = AdvanceGrid1.Filter

        'Diplay the Search values in TextBox2
        TextBox2.Text = ""
        For Each SearchValue As KeyValuePair(Of String, String) In AdvanceGrid1.SearcheValues
            TextBox2.Text &= SearchValue.Key & ":" & SearchValue.Value & vbCrLf
        Next
    End Sub
{code:vb.net} 

## Search/Filter programmatically

To search/filter results in AG programmatically, assigne the search value to the desired column in SearchValues dictionary.

{code:vb.net}
 Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Programaticaly search the grid by assigning values to one or more columns and calling the search function.
        AdvanceGrid1.SearcheValues("Col01_String") = "France"
        AdvanceGrid1.Search()
End Sub
{code:vb.net}

## Show/Hide columns (Columns visibility)
Right-click on the AG to see a list of columns. Use the checkbox beside the name of each column to show/hide it from view.
The columns visibility can be tracked using the AG.Columns collection.
Columns in AG.Columns collection are DataGridViewColumn type and contains (amongst other properties), Name and Visibility.
Here I'm using the ColumnsVisibilityChanged event to show the state of each column in TextBox3

{code:vb.net}
Private Sub AdvanceGrid1_ColumnsVisibilityChanged(sender As Object, e As ItemCheckEventArgs) Handles AdvanceGrid1.ColumnsVisibilityChanged
        TextBox3.Text = String.Empty
        For Each col As DataGridViewColumn In AdvanceGrid1.Columns
            TextBox3.Text &= col.Name & ":" & col.Visible & vbCrLf
        Next
End Sub
{code:vb.net}

## _Note about Source code_

* Please note that **Branch01** is the latest code committed.
* Ignore TESTING project in this branch, I'm having difficulties deleting it, but I'll get round to it soon!
  
