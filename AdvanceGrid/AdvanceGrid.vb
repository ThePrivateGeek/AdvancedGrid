<System.ComponentModel.Designer(GetType(AdvanceGridDesigner))>
<ToolboxBitmap(GetType(AdvanceGrid), "AdvanceGrid.png")>
Public Class AdvanceGrid
    Friend popup As New ToolStripDropDown With {.Padding = Padding.Empty}
    Friend WithEvents popupAllColumns As New CheckedListBox With {.CheckOnClick = True}

    Shadows Event TextChanged(ByVal sender As Object)
    Shadows Event SelectionChanged(sender As Object, e As EventArgs)
    Event ColumnsVisibilityChanged(ByVal sender As Object, e As ItemCheckEventArgs)
    'dgv methods
    Public Sub BeginEdit(ByVal selectAll As Boolean)
        dgv.BeginEdit(selectAll)
    End Sub
    Public Sub EndEdit()
        dgv.EndEdit()
    End Sub
    Public Sub CancelEdit()
        dgv.CancelEdit()
    End Sub
    Public Function AreAllCellsSelected(ByVal includeInvisibleCells As Boolean) As Boolean
        Return dgv.AreAllCellsSelected(includeInvisibleCells)
    End Function
    'dgv events
    Private Sub dgv_DataSourceChanged(sender As Object, e As EventArgs) Handles dgv.DataSourceChanged
        _searchevalues.Clear()
        Dim DType As String = String.Empty

        For Each col As DataGridViewColumn In dgv.Columns
            DType = col.ValueType.ToString
            Select Case DType

                Case "System.Boolean"
                    Dim chk As New CheckBox With {.Name = col.Name, .Top = 0, .FlatStyle = FlatStyle.Flat, .CheckAlign = ContentAlignment.MiddleCenter, .ThreeState = True, .CheckState = CheckState.Indeterminate}
                    AddHandler chk.CheckStateChanged, AddressOf txt_TextChanged
                    chk.DataBindings.Add("Visible", col, "Visible")
                    _searchevalues.Add(chk.Name, chk.Checked.ToString)
                    panUpper.Controls.Add(chk)
                Case Else ' "System.String", "System.Int32", "System.DateTime"
                    Dim txt As New TextBox With {.Name = col.Name, .BackColor = SearchColor, .Top = 0, .BorderStyle = Windows.Forms.BorderStyle.FixedSingle}
                    AddHandler txt.TextChanged, AddressOf txt_TextChanged
                    AddHandler txt.KeyDown, AddressOf txt_KeyDown
                    AddHandler txt.Enter, AddressOf txt_Enter
                    txt.DataBindings.Add("Visible", col, "Visible")
                    _searchevalues.Add(txt.Name, txt.Text)
                    panUpper.Controls.Add(txt)
            End Select
        Next

        'dgv.AllowUserToAddRows = AllowUserToAddRows
        'dgv.AllowUserToDeleteRows = AllowUserToDeleteRows
        'dgv.AllowUserToOrderColumns = AllowUserToOrderColumns
        'dgv.ReadOnly = ReadOnlyGrid

        Arrange()
        lblCount.Text = dgv.Rows.Count

        'Pop up
        Dim popupHost As New ToolStripControlHost(popupAllColumns) With {.Padding = Padding.Empty, .Margin = Padding.Empty, .AutoSize = True}
        popup.Items.Add(popupHost)
    End Sub
    Private Sub dgv_MouseClick(sender As Object, e As MouseEventArgs) Handles dgv.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then 'And Control.ModifierKeys = Keys.Control Then
            popupAllColumns.Items.Clear()
            For Each col As DataGridViewColumn In dgv.Columns
                popupAllColumns.Items.Add(col.Name, col.Visible)
            Next
            popup.Show(dgv.PointToScreen(New Point(e.X, e.Y)))
            Dim ht As DataGridView.HitTestInfo = dgv.HitTest(e.X, e.Y)

            popupAllColumns.SelectedIndex = ht.ColumnIndex
        End If
    End Sub

    Private Sub dgv_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgv.DataError
        lblErrors.Text = e.Exception.Message
    End Sub
    Private Sub dgv_KeyUp(sender As Object, e As KeyEventArgs) Handles dgv.KeyUp
        If e.KeyCode = Keys.Up Then
            If dgv.CurrentRow.Index = 0 Then
                For Each txt As Control In panUpper.Controls
                    If txt.Name = dgv.CurrentCell.OwningColumn.Name Then
                        txt.Focus()
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub dgv_Arrange() Handles dgv.ColumnWidthChanged,
                                      dgv.Scroll,
                                      dgv.ColumnDisplayIndexChanged,
                                      dgv.RowHeadersWidthChanged,
                                      dgv.Resize
        Arrange()
    End Sub

    Private Sub dgv_SelectionChanged(sender As Object, e As EventArgs) Handles dgv.SelectionChanged
        RaiseEvent SelectionChanged(sender, e)
    End Sub

    Private Sub dgv_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellLeave
        If dgv.IsCurrentCellDirty Then dgv.CurrentCell.Style.BackColor = Color.Pink
    End Sub

    'popup events
    Private Sub popupAllColoumns_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles popupAllColumns.ItemCheck
        dgv.Columns(e.Index).Visible = e.NewValue = CheckState.Checked
        Arrange()
        RaiseEvent ColumnsVisibilityChanged(sender, e)
    End Sub

    'search pannel handelers
    Sub txt_TextChanged(sender As System.Object, e As System.EventArgs)

        _searchevalues.Clear()

        Dim MyBindingSource As New BindingSource
        MyBindingSource.DataSource = dgv.DataSource

        Dim DType As String = String.Empty

        _filter = String.Empty
        For Each txt In panUpper.Controls

            DType = dgv.Columns(txt.Name).ValueType.ToString

            Select Case DType
                Case "System.Int32"
                    If Val(txt.Text) <> Nothing Then _filter &= String.Format("{0} ={1} AND ", txt.Name, txt.Text)
                    _searchevalues.Add(txt.Name, txt.Text)
                Case "System.DateTime"
                    If txt.Text <> "" Then _filter &= String.Format("CONVERT({0}, 'System.String') LIKE '%{1}%' AND ", txt.Name, txt.Text)
                    _searchevalues.Add(txt.Name, txt.Text)
                Case "System.Boolean"
                    If Not txt.CheckState = CheckState.Indeterminate Then
                        _filter &= String.Format("{0}={1} AND ", txt.Name, txt.Checked)
                    Else
                        _filter = _filter.Replace(txt.Name & "=" & txt.Checked & " AND ", "")
                    End If
                    _searchevalues.Add(txt.Name, txt.Checked)
                Case "System.String"
                    _filter &= String.Format("({0} LIKE '%{1}%' OR {0} IS NULL) AND ", txt.Name, txt.Text)
                    _searchevalues.Add(txt.Name, txt.Text)
            End Select

        Next
        '
        _filter = _filter.Substring(0, _filter.Length - 4)
        MyBindingSource.Filter = _filter
        lblCount.Text = dgv.Rows.Count

        RaiseEvent TextChanged(sender)

    End Sub
    Sub txt_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Down Then
            dgv.ClearSelection()
            dgv.Focus()
            dgv.CurrentCell = dgv.Rows(0).Cells(sender.Name)
        End If
    End Sub
    Sub txt_Enter(sender As Object, e As EventArgs)
        sender.SelectAll()
    End Sub

    'properties
    Public Property DataSource() As Object
        Get
            Return dgv.DataSource
        End Get
        Set(ByVal value As Object)
            dgv.DataSource = value
        End Set
    End Property
    Public Property DataMember() As String
        Get
            Return dgv.DataMember
        End Get
        Set(ByVal value As String)
            dgv.DataMember = value
        End Set
    End Property
    ReadOnly Property Rows As DataGridViewRowCollection
        Get
            Return dgv.Rows
        End Get
    End Property
    ReadOnly Property SelectedRows As DataGridViewSelectedRowCollection
        Get
            Return dgv.SelectedRows
        End Get
    End Property
    ReadOnly Property RowCount As Integer
        Get
            Return dgv.RowCount
        End Get
    End Property
    ReadOnly Property Columns As DataGridViewColumnCollection
        Get
            Return dgv.Columns
        End Get
    End Property
    <System.ComponentModel.DefaultValue(True)>
    Property AllowUserToDeleteRows As Boolean
        Get
            Return dgv.AllowUserToDeleteRows
        End Get
        Set(value As Boolean)
            dgv.AllowUserToDeleteRows = value
        End Set
    End Property
    <System.ComponentModel.DefaultValue(True)>
    Property AllowUserToAddRows As Boolean
        Get
            Return dgv.AllowUserToAddRows
        End Get
        Set(value As Boolean)
            dgv.AllowUserToAddRows = value
        End Set
    End Property
    <System.ComponentModel.DefaultValue(True)>
    Property AllowUserToOrderColumns As Boolean
        Get
            Return dgv.AllowUserToOrderColumns
        End Get
        Set(value As Boolean)
            dgv.AllowUserToOrderColumns = value
        End Set
    End Property
    <System.ComponentModel.DefaultValue(True)>
    Property AllowUserToResizeColumns As Boolean
        Get
            Return dgv.AllowUserToResizeColumns
        End Get
        Set(value As Boolean)
            dgv.AllowUserToResizeColumns = value
        End Set
    End Property
    <System.ComponentModel.DefaultValue(True)>
    Property AllowUserToResizeRows As Boolean
        Get
            Return dgv.AllowUserToResizeRows
        End Get
        Set(value As Boolean)
            dgv.AllowUserToResizeRows = value
        End Set
    End Property
    <System.ComponentModel.DefaultValue(False)>
    Property ReadOnlyGrid As Boolean
        Get
            Return dgv.ReadOnly
        End Get
        Set(value As Boolean)
            dgv.ReadOnly = value
        End Set
    End Property
    Friend _filter As String
    ''' <summary>
    ''' Read only property returens the complete filter string
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Filter As String
        Get
            Return _filter
        End Get
    End Property
    Friend Shared _searchColor As Color = Color.LightGreen
    ''' <summary>
    ''' Gets or sets the color for the search boxes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SearchColor As Color
        Get
            Return _searchColor
        End Get
        Set(value As Color)
            _searchColor = value
        End Set
    End Property
    Public ReadOnly Property SelectedCells As DataGridViewSelectedCellCollection
        Get
            Return dgv.SelectedCells
        End Get
    End Property
    Public ReadOnly Property CurrentCellAddress() As Point
        Get
            Return dgv.CurrentCellAddress
        End Get
    End Property
    Public ReadOnly Property CurrentCell() As DataGridViewCell
        Get
            Return dgv.CurrentCell()
        End Get
    End Property
    Dim _searchevalues As New Dictionary(Of String, String)
    ''' <summary>
    ''' Gets or sets Dictionary of all columns and their search values 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SearcheValues() As Dictionary(Of String, String)
        Get
            Return _searchevalues
        End Get
        Set(value As Dictionary(Of String, String))
            _searchevalues = value
        End Set
    End Property
    Property AlternatingRowsDefaultCellStyle As DataGridViewCellStyle
        Get
            Return dgv.AlternatingRowsDefaultCellStyle
        End Get
        Set(value As DataGridViewCellStyle)
            dgv.AlternatingRowsDefaultCellStyle = value
        End Set
    End Property
    Property RowsDefaultCellStyle As DataGridViewCellStyle
        Get
            Return dgv.RowsDefaultCellStyle
        End Get
        Set(value As DataGridViewCellStyle)
            dgv.RowsDefaultCellStyle = value
        End Set
    End Property
    ReadOnly Property SortedColumn As DataGridViewColumn
        Get
            Return dgv.SortedColumn
        End Get
    End Property
    ReadOnly Property SortOrder As SortOrder
        Get
            Return dgv.SortOrder
        End Get
    End Property

    ReadOnly Property CurrentRow As DataGridViewRow
        Get
            Return dgv.CurrentRow
        End Get
    End Property

    Public Property ClipboardCopyMode
        Get
            Return dgv.ClipboardCopyMode
        End Get
        Set(value)
            dgv.ClipboardCopyMode = value
        End Set
    End Property
    'subs
    ''' <summary>
    ''' Arranges Left and Width of search boxes to match the grid's columns.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Arrange()
        For Each Ctrl As Control In panUpper.Controls
            Dim colName As String = Ctrl.Name.ToString
            Dim col As DataGridViewColumn = dgv.Columns(colName)
            Dim rec As Rectangle = dgv.GetColumnDisplayRectangle(col.Index, True)
            Ctrl.Width = rec.Width
            Ctrl.Left = rec.Left
        Next
    End Sub
    ''' <summary>
    ''' Sorts the contents of the System.Windows.Forms.DataGridView control in ascending or descending order based on the contents of the specified column.
    ''' </summary>
    ''' <param name="dataGridViewColumn">he column by which to sort the contents of the System.Windows.Forms.DataGridView.</param>
    ''' <param name="direction">One of the System.ComponentModel.ListSortDirection values.</param>
    ''' <remarks></remarks>
    Public Sub Sort(ByVal dataGridViewColumn As DataGridViewColumn, direction As System.ComponentModel.ListSortDirection)
        dgv.Sort(dataGridViewColumn, direction)
    End Sub

    ''' <summary>
    ''' Performs search on the grid by applying all searches found in SearchValues dictionary.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Search()
        For Each ctrl In panUpper.Controls
            Dim DType As String = String.Empty
            DType = dgv.Columns(ctrl.Name).ValueType.ToString
            Select Case DType
                Case "System.Boolean"
                    ctrl.checked = _searchevalues.Item(ctrl.Name)
                Case Else ' "System.String", "System.Int32", "System.DateTime"
                    ctrl.Text = _searchevalues.Item(ctrl.Name)
            End Select
        Next
    End Sub
End Class



