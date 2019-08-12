<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.browserPane = New System.Windows.Forms.Panel()
        Me.buttonsPane = New System.Windows.Forms.TableLayoutPanel()
        Me.labelStatus = New System.Windows.Forms.Label()
        Me.labelReplay = New System.Windows.Forms.Label()
        Me.labelPlay = New System.Windows.Forms.Label()
        Me.errorBox = New System.Windows.Forms.TextBox()
        Me.playerBox = New System.Windows.Forms.TextBox()
        Me.pause = New System.Windows.Forms.Button()
        Me.stopBt = New System.Windows.Forms.Button()
        Me.play = New System.Windows.Forms.Button()
        Me.browserBox = New System.Windows.Forms.TextBox()
        Me.openFile = New System.Windows.Forms.OpenFileDialog()
        Me.subtitle = New System.Windows.Forms.Label()
        Me.buttonsPane.SuspendLayout()
        Me.SuspendLayout()
        '
        'browserPane
        '
        Me.browserPane.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.browserPane.Location = New System.Drawing.Point(0, 24)
        Me.browserPane.Name = "browserPane"
        Me.browserPane.Size = New System.Drawing.Size(905, 380)
        Me.browserPane.TabIndex = 0
        '
        'buttonsPane
        '
        Me.buttonsPane.ColumnCount = 3
        Me.buttonsPane.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.buttonsPane.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.buttonsPane.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.buttonsPane.Controls.Add(Me.labelStatus, 1, 2)
        Me.buttonsPane.Controls.Add(Me.labelReplay, 1, 1)
        Me.buttonsPane.Controls.Add(Me.labelPlay, 1, 0)
        Me.buttonsPane.Controls.Add(Me.errorBox, 2, 2)
        Me.buttonsPane.Controls.Add(Me.playerBox, 2, 1)
        Me.buttonsPane.Controls.Add(Me.pause, 0, 2)
        Me.buttonsPane.Controls.Add(Me.stopBt, 0, 1)
        Me.buttonsPane.Controls.Add(Me.play, 0, 0)
        Me.buttonsPane.Controls.Add(Me.browserBox, 2, 0)
        Me.buttonsPane.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.buttonsPane.Location = New System.Drawing.Point(0, 410)
        Me.buttonsPane.Name = "buttonsPane"
        Me.buttonsPane.RowCount = 3
        Me.buttonsPane.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.buttonsPane.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.buttonsPane.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.buttonsPane.Size = New System.Drawing.Size(905, 100)
        Me.buttonsPane.TabIndex = 1
        '
        'labelStatus
        '
        Me.labelStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.labelStatus.AutoSize = True
        Me.labelStatus.Location = New System.Drawing.Point(103, 66)
        Me.labelStatus.Name = "labelStatus"
        Me.labelStatus.Size = New System.Drawing.Size(40, 34)
        Me.labelStatus.TabIndex = 10
        Me.labelStatus.Text = "Status:"
        Me.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labelReplay
        '
        Me.labelReplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.labelReplay.AutoSize = True
        Me.labelReplay.Location = New System.Drawing.Point(103, 33)
        Me.labelReplay.Name = "labelReplay"
        Me.labelReplay.Size = New System.Drawing.Size(57, 33)
        Me.labelReplay.TabIndex = 9
        Me.labelReplay.Text = "Replaying:"
        Me.labelReplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labelPlay
        '
        Me.labelPlay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.labelPlay.AutoSize = True
        Me.labelPlay.Location = New System.Drawing.Point(103, 0)
        Me.labelPlay.Name = "labelPlay"
        Me.labelPlay.Size = New System.Drawing.Size(143, 33)
        Me.labelPlay.TabIndex = 8
        Me.labelPlay.Text = "Click Play to load macro from file"
        Me.labelPlay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'errorBox
        '
        Me.errorBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.errorBox.Location = New System.Drawing.Point(253, 69)
        Me.errorBox.Multiline = True
        Me.errorBox.Name = "errorBox"
        Me.errorBox.ReadOnly = True
        Me.errorBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.errorBox.Size = New System.Drawing.Size(649, 28)
        Me.errorBox.TabIndex = 5
        '
        'playerBox
        '
        Me.playerBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.playerBox.Location = New System.Drawing.Point(253, 36)
        Me.playerBox.Multiline = True
        Me.playerBox.Name = "playerBox"
        Me.playerBox.ReadOnly = True
        Me.playerBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.playerBox.Size = New System.Drawing.Size(649, 27)
        Me.playerBox.TabIndex = 4
        '
        'pause
        '
        Me.pause.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pause.Location = New System.Drawing.Point(3, 69)
        Me.pause.Name = "pause"
        Me.pause.Size = New System.Drawing.Size(94, 28)
        Me.pause.TabIndex = 2
        Me.pause.Text = "Pause"
        Me.pause.UseVisualStyleBackColor = True
        '
        'stopBt
        '
        Me.stopBt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.stopBt.Location = New System.Drawing.Point(3, 36)
        Me.stopBt.Name = "stopBt"
        Me.stopBt.Size = New System.Drawing.Size(94, 27)
        Me.stopBt.TabIndex = 1
        Me.stopBt.Text = "Stop"
        Me.stopBt.UseVisualStyleBackColor = True
        '
        'play
        '
        Me.play.Dock = System.Windows.Forms.DockStyle.Fill
        Me.play.Location = New System.Drawing.Point(3, 3)
        Me.play.Name = "play"
        Me.play.Size = New System.Drawing.Size(94, 27)
        Me.play.TabIndex = 0
        Me.play.Text = "Play"
        Me.play.UseVisualStyleBackColor = True
        '
        'browserBox
        '
        Me.browserBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.browserBox.Location = New System.Drawing.Point(253, 3)
        Me.browserBox.Multiline = True
        Me.browserBox.Name = "browserBox"
        Me.browserBox.ReadOnly = True
        Me.browserBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.browserBox.Size = New System.Drawing.Size(649, 27)
        Me.browserBox.TabIndex = 3
        '
        'openFile
        '
        Me.openFile.DefaultExt = "iim"
        '
        'subtitle
        '
        Me.subtitle.AutoSize = True
        Me.subtitle.Location = New System.Drawing.Point(0, 4)
        Me.subtitle.Name = "subtitle"
        Me.subtitle.Size = New System.Drawing.Size(379, 13)
        Me.subtitle.TabIndex = 2
        Me.subtitle.Text = "This .NET application uses the iMacros Component to automate web browsing."
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(905, 510)
        Me.Controls.Add(Me.subtitle)
        Me.Controls.Add(Me.buttonsPane)
        Me.Controls.Add(Me.browserPane)
        Me.Name = "Form1"
        Me.Text = "iMacros Component Test Application"
        Me.buttonsPane.ResumeLayout(False)
        Me.buttonsPane.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents browserPane As System.Windows.Forms.Panel
    Friend WithEvents buttonsPane As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents errorBox As System.Windows.Forms.TextBox
    Friend WithEvents playerBox As System.Windows.Forms.TextBox
    Friend WithEvents pause As System.Windows.Forms.Button
    Friend WithEvents stopBt As System.Windows.Forms.Button
    Friend WithEvents play As System.Windows.Forms.Button
    Friend WithEvents browserBox As System.Windows.Forms.TextBox
    Friend WithEvents openFile As System.Windows.Forms.OpenFileDialog
	Private WithEvents labelStatus As System.Windows.Forms.Label
	Private WithEvents labelReplay As System.Windows.Forms.Label
	Private WithEvents labelPlay As System.Windows.Forms.Label
	Friend WithEvents subtitle As System.Windows.Forms.Label

End Class
