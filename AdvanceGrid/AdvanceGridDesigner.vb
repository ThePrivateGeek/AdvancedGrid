Imports System.Windows.Forms.Design
Imports System.ComponentModel.Design
Imports System.ComponentModel
Public Class AdvanceGridDesigner
    Inherits ControlDesigner
    Private _lists As DesignerActionListCollection
    Public Overrides ReadOnly Property ActionLists() As DesignerActionListCollection
        Get
            If _lists Is Nothing Then
                _lists = New DesignerActionListCollection()
                _lists.Add(New AdvanceGridActionList(Me.Component))
            End If

            Return _lists
        End Get
    End Property
End Class

Friend Class AdvanceGridActionList
    Inherits DesignerActionList
    Private dgv As AdvanceGrid
    Private designerActionSvc As DesignerActionUIService

    Public Sub New(ByVal component As IComponent)
        MyBase.New(component)

        dgv = DirectCast(component, AdvanceGrid)
        designerActionSvc = CType(GetService(GetType(DesignerActionUIService)), DesignerActionUIService)

    End Sub
    Public Property SearchColor() As Color
        Get
            Return AdvanceGrid._searchColor
        End Get
        Set(ByVal value As Color)
            AdvanceGrid._searchColor = value
            designerActionSvc.Refresh(Me.Component)
        End Set
    End Property

    Public Property EnableAdding() As Boolean
        Get
            Return dgv.AllowUserToAddRows
        End Get
        Set(ByVal value As Boolean)
            dgv.AllowUserToAddRows = value
        End Set
    End Property
    Public Property EnableEditing() As Boolean
        Get
            Return Not dgv.ReadOnlyGrid
        End Get
        Set(ByVal value As Boolean)
            dgv.ReadOnlyGrid = Not value
        End Set
    End Property
    Public Property EnableDeleting() As Boolean
        Get
            Return dgv.AllowUserToDeleteRows
        End Get
        Set(ByVal value As Boolean)
            dgv.AllowUserToDeleteRows = value
        End Set
    End Property
    Public Property EnableColumnReordering() As Boolean
        Get
            Return dgv.AllowUserToOrderColumns
        End Get
        Set(ByVal value As Boolean)
            dgv.AllowUserToOrderColumns = value
        End Set
    End Property
    Public Sub OnDock()
        If dgv.Dock = DockStyle.Fill Then
            dgv.Dock = DockStyle.None
        Else
            dgv.Dock = DockStyle.Fill
        End If

        designerActionSvc.Refresh(dgv)
    End Sub
    Public Overrides Function GetSortedActionItems() As DesignerActionItemCollection
        Dim str As String
        Dim items As New DesignerActionItemCollection

        If dgv.Dock = DockStyle.Fill Then
            str = "Undock in parent container."
        Else
            str = "Dock in parent container."
        End If

        'Add a few Header Items (categories)
        '  items.Add(New DesignerActionHeaderItem("Category 1"))
        ' items.Add(New DesignerActionHeaderItem("Category 2"))

        'Add the properties
        items.Add(New DesignerActionPropertyItem("SearchColor", "Search Color", "", "Color of the search boxes."))
        items.Add(New DesignerActionPropertyItem("EnableAdding", "Enable Adding", "", "Enable users to add new rows."))
        items.Add(New DesignerActionPropertyItem("EnableEditing", "Enable Editing", "", "Enable users to edit cells."))
        items.Add(New DesignerActionPropertyItem("EnableDeleting", "Enable Deleting", "", "Enable users to delete rows."))
        items.Add(New DesignerActionPropertyItem("EnableColumnReordering", "Enable Column Reordering", "", "Enable users to reorder columns."))

        'Add the methods
        'items.Add(New DesignerActionMethodItem(Me, "OnClear", "Clear text", "Category 2", "Clears the text in the customTextBox."))
        'If txt.Multiline Then
        '    items.Add(New DesignerActionMethodItem(Me, "OnMakeSquare", "Make a square", "Category 2", "Changes the width or height of the customTextBox to resemble a square."))
        items.Add(New DesignerActionMethodItem(Me, "OnDock", str, "", "Docks or undocks the customTextBox in the parent container."))
        'End If

        'Return the ActionItemCollection
        Return items
    End Function
End Class