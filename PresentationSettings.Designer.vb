<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PresentationSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PresentationSettings))
        Me.PSTitleColorDialog = New System.Windows.Forms.ColorDialog()
        Me.PSTitleFontDialog = New System.Windows.Forms.FontDialog()
        Me.PSLyricsColorDialog = New System.Windows.Forms.ColorDialog()
        Me.PSLyricsFontDialog = New System.Windows.Forms.FontDialog()
        Me.PSBGColorDialog = New System.Windows.Forms.ColorDialog()
        Me.PSTitleLabel = New System.Windows.Forms.Label()
        Me.PSLyricsLabel = New System.Windows.Forms.Label()
        Me.PSOKButton = New System.Windows.Forms.Button()
        Me.PSCancelButton = New System.Windows.Forms.Button()
        Me.PSOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.PSBGImgCheckBox = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'PSTitleFontDialog
        '
        Me.PSTitleFontDialog.ShowColor = True
        '
        'PSLyricsFontDialog
        '
        Me.PSLyricsFontDialog.ShowColor = True
        '
        'PSTitleLabel
        '
        Me.PSTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PSTitleLabel.AutoSize = True
        Me.PSTitleLabel.BackColor = System.Drawing.Color.Transparent
        Me.PSTitleLabel.Location = New System.Drawing.Point(18, 14)
        Me.PSTitleLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PSTitleLabel.MaximumSize = New System.Drawing.Size(1380, 0)
        Me.PSTitleLabel.MinimumSize = New System.Drawing.Size(1380, 0)
        Me.PSTitleLabel.Name = "PSTitleLabel"
        Me.PSTitleLabel.Size = New System.Drawing.Size(1380, 20)
        Me.PSTitleLabel.TabIndex = 0
        Me.PSTitleLabel.Text = "Song Title"
        Me.PSTitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'PSLyricsLabel
        '
        Me.PSLyricsLabel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.PSLyricsLabel.AutoSize = True
        Me.PSLyricsLabel.BackColor = System.Drawing.Color.Transparent
        Me.PSLyricsLabel.Location = New System.Drawing.Point(18, 154)
        Me.PSLyricsLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PSLyricsLabel.MaximumSize = New System.Drawing.Size(1380, 0)
        Me.PSLyricsLabel.MinimumSize = New System.Drawing.Size(1380, 0)
        Me.PSLyricsLabel.Name = "PSLyricsLabel"
        Me.PSLyricsLabel.Size = New System.Drawing.Size(1380, 160)
        Me.PSLyricsLabel.TabIndex = 3
        Me.PSLyricsLabel.Text = resources.GetString("PSLyricsLabel.Text")
        Me.PSLyricsLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'PSOKButton
        '
        Me.PSOKButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PSOKButton.BackColor = System.Drawing.Color.Transparent
        Me.PSOKButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PSOKButton.Location = New System.Drawing.Point(1164, 998)
        Me.PSOKButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PSOKButton.Name = "PSOKButton"
        Me.PSOKButton.Size = New System.Drawing.Size(112, 35)
        Me.PSOKButton.TabIndex = 4
        Me.PSOKButton.Text = "OK"
        Me.PSOKButton.UseVisualStyleBackColor = False
        '
        'PSCancelButton
        '
        Me.PSCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PSCancelButton.BackColor = System.Drawing.Color.Transparent
        Me.PSCancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PSCancelButton.Location = New System.Drawing.Point(1286, 998)
        Me.PSCancelButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PSCancelButton.Name = "PSCancelButton"
        Me.PSCancelButton.Size = New System.Drawing.Size(112, 35)
        Me.PSCancelButton.TabIndex = 5
        Me.PSCancelButton.Text = "Cancel"
        Me.PSCancelButton.UseVisualStyleBackColor = False
        '
        'PSOpenFileDialog
        '
        Me.PSOpenFileDialog.FileName = "PSOpenFileDialog"
        '
        'PSBGImgCheckBox
        '
        Me.PSBGImgCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PSBGImgCheckBox.AutoSize = True
        Me.PSBGImgCheckBox.BackColor = System.Drawing.Color.Transparent
        Me.PSBGImgCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PSBGImgCheckBox.Location = New System.Drawing.Point(18, 1005)
        Me.PSBGImgCheckBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PSBGImgCheckBox.Name = "PSBGImgCheckBox"
        Me.PSBGImgCheckBox.Size = New System.Drawing.Size(226, 24)
        Me.PSBGImgCheckBox.TabIndex = 6
        Me.PSBGImgCheckBox.Text = "Use background image"
        Me.PSBGImgCheckBox.UseVisualStyleBackColor = False
        '
        'PresentationSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1416, 1052)
        Me.Controls.Add(Me.PSBGImgCheckBox)
        Me.Controls.Add(Me.PSCancelButton)
        Me.Controls.Add(Me.PSOKButton)
        Me.Controls.Add(Me.PSLyricsLabel)
        Me.Controls.Add(Me.PSTitleLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "PresentationSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PSTitleColorDialog As System.Windows.Forms.ColorDialog
    Friend WithEvents PSTitleFontDialog As System.Windows.Forms.FontDialog
    Friend WithEvents PSLyricsColorDialog As System.Windows.Forms.ColorDialog
    Friend WithEvents PSLyricsFontDialog As System.Windows.Forms.FontDialog
    Friend WithEvents PSBGColorDialog As System.Windows.Forms.ColorDialog
    Friend WithEvents PSTitleLabel As System.Windows.Forms.Label
    Friend WithEvents PSLyricsLabel As System.Windows.Forms.Label
    Friend WithEvents PSOKButton As System.Windows.Forms.Button
    Friend WithEvents PSCancelButton As System.Windows.Forms.Button
    Friend WithEvents PSOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents PSBGImgCheckBox As System.Windows.Forms.CheckBox
End Class
