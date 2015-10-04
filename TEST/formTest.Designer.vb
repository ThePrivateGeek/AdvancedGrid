<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formTest
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formTest))
        Me.AdvanceGrid1 = New AdvanceGrid.AdvanceGrid()
        Me.SuspendLayout()
        '
        'AdvanceGrid1
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.AdvanceGrid1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.AdvanceGrid1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithAutoHeaderText
        Me.AdvanceGrid1.DataMember = ""
        Me.AdvanceGrid1.DataSource = Nothing
        Me.AdvanceGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AdvanceGrid1.Location = New System.Drawing.Point(0, 0)
        Me.AdvanceGrid1.Name = "AdvanceGrid1"
        Me.AdvanceGrid1.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.AdvanceGrid1.SearchColor = System.Drawing.Color.LemonChiffon
        Me.AdvanceGrid1.SearcheValues = CType(resources.GetObject("AdvanceGrid1.SearcheValues"), System.Collections.Generic.Dictionary(Of String, String))
        Me.AdvanceGrid1.Size = New System.Drawing.Size(469, 314)
        Me.AdvanceGrid1.TabIndex = 0
        '
        'formTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(469, 314)
        Me.Controls.Add(Me.AdvanceGrid1)
        Me.Name = "formTest"
        Me.Text = "Test Form"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AdvanceGrid1 As AdvanceGrid.AdvanceGrid

End Class
