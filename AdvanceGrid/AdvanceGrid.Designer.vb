<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdvanceGrid
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
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
        Me.panUpper = New System.Windows.Forms.Panel()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.panLower = New System.Windows.Forms.Panel()
        Me.lblErrors = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panLower.SuspendLayout()
        Me.SuspendLayout()
        '
        'panUpper
        '
        Me.panUpper.Dock = System.Windows.Forms.DockStyle.Top
        Me.panUpper.Location = New System.Drawing.Point(0, 0)
        Me.panUpper.Name = "panUpper"
        Me.panUpper.Size = New System.Drawing.Size(450, 20)
        Me.panUpper.TabIndex = 4
        '
        'dgv
        '
        Me.dgv.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.Location = New System.Drawing.Point(0, 20)
        Me.dgv.Name = "dgv"
        Me.dgv.Size = New System.Drawing.Size(450, 407)
        Me.dgv.TabIndex = 6
        '
        'panLower
        '
        Me.panLower.Controls.Add(Me.lblErrors)
        Me.panLower.Controls.Add(Me.lblCount)
        Me.panLower.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panLower.Location = New System.Drawing.Point(0, 427)
        Me.panLower.Name = "panLower"
        Me.panLower.Size = New System.Drawing.Size(450, 20)
        Me.panLower.TabIndex = 7
        '
        'lblErrors
        '
        Me.lblErrors.AutoSize = True
        Me.lblErrors.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblErrors.Location = New System.Drawing.Point(0, 0)
        Me.lblErrors.Name = "lblErrors"
        Me.lblErrors.Size = New System.Drawing.Size(38, 13)
        Me.lblErrors.TabIndex = 1
        Me.lblErrors.Text = "Ready"
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblCount.Location = New System.Drawing.Point(437, 0)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(13, 13)
        Me.lblCount.TabIndex = 0
        Me.lblCount.Text = "0"
        '
        'AdvanceGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.panLower)
        Me.Controls.Add(Me.panUpper)
        Me.Name = "AdvanceGrid"
        Me.Size = New System.Drawing.Size(450, 447)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panLower.ResumeLayout(False)
        Me.panLower.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panUpper As System.Windows.Forms.Panel
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents panLower As System.Windows.Forms.Panel
    Friend WithEvents lblErrors As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label

End Class
