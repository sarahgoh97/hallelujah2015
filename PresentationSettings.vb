Public Class PresentationSettings
    Dim BGColorTemp, TitleColorTemp, LyricsColorTemp As System.Drawing.Color
    Dim TitleFontTemp, LyricsFontTemp As System.Drawing.Font
    Dim BGImgTemp As String
    Dim BGImgCheckTemp As Boolean

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.BGImgTemp = My.Settings.BGImg
        Me.BGImgCheckTemp = My.Settings.BGImgCheck
        Me.BGColorTemp = My.Settings.BGColor
        Me.TitleColorTemp = My.Settings.TitleColor
        Me.LyricsColorTemp = My.Settings.LyricsColor
        Me.TitleFontTemp = My.Settings.TitleFont
        Me.LyricsFontTemp = My.Settings.LyricsFont

        Me.PSOpenFileDialog.FileName = Me.BGImgTemp
        Me.PSBGImgCheckBox.Checked = Me.BGImgCheckTemp
        Me.PSBGColorDialog.Color = Me.BGColorTemp
        Me.PSTitleColorDialog.Color = Me.TitleColorTemp
        Me.PSTitleFontDialog.Font = Me.TitleFontTemp
        Me.PSTitleFontDialog.Color = Me.TitleColorTemp
        Me.PSLyricsColorDialog.Color = Me.LyricsColorTemp
        Me.PSLyricsFontDialog.Font = Me.LyricsFontTemp
        Me.PSLyricsFontDialog.Color = Me.LyricsColorTemp
        Me.PS_Refresh()
    End Sub

    Private Sub PS_Refresh()
        If Me.BGImgCheckTemp Then
            Me.BackgroundImage = System.Drawing.Image.FromFile(Me.BGImgTemp)
        Else
            Me.BackgroundImage = Nothing
        End If
        Me.BackColor = Me.BGColorTemp
        Me.PSTitleLabel.Font = Me.TitleFontTemp
        Me.PSTitleLabel.ForeColor = Me.TitleColorTemp
        Me.PSLyricsLabel.Font = Me.LyricsFontTemp
        Me.PSLyricsLabel.ForeColor = Me.LyricsColorTemp
        Me.PSBGImgCheckBox.ForeColor = Me.LyricsColorTemp
        Me.PSOKButton.ForeColor = Me.LyricsColorTemp
        Me.PSCancelButton.ForeColor = Me.LyricsColorTemp
    End Sub

    Private Sub PSTitleLabel_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PSTitleLabel.Click
        If e.Button = Forms.MouseButtons.Left Then
            If Me.PSTitleFontDialog.ShowDialog() = Forms.DialogResult.OK Then
                Me.TitleFontTemp = Me.PSTitleFontDialog.Font
                Me.TitleColorTemp = Me.PSTitleFontDialog.Color
                Me.PS_Refresh()
            End If
        ElseIf e.Button = Forms.MouseButtons.Right Then
            If Me.PSTitleColorDialog.ShowDialog() = Forms.DialogResult.OK Then
                Me.TitleColorTemp = Me.PSTitleColorDialog.Color
                Me.PS_Refresh()
            End If
        End If
    End Sub

    Private Sub PSLyricsLabel_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PSLyricsLabel.Click
        If e.Button = Forms.MouseButtons.Left Then
            If Me.PSLyricsFontDialog.ShowDialog() = Forms.DialogResult.OK Then
                Me.LyricsFontTemp = Me.PSLyricsFontDialog.Font
                Me.LyricsColorTemp = Me.PSLyricsFontDialog.Color
                Me.PS_Refresh()
            End If
        ElseIf e.Button = Forms.MouseButtons.Right Then
            If Me.PSLyricsColorDialog.ShowDialog() = Forms.DialogResult.OK Then
                Me.LyricsColorTemp = Me.PSLyricsColorDialog.Color
                Me.PS_Refresh()
            End If
        End If
    End Sub

    Private Sub PresentationSettings_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.Click
        If e.Button = Forms.MouseButtons.Left Then
            If Me.PSBGColorDialog.ShowDialog() = Forms.DialogResult.OK Then
                Me.BGColorTemp = Me.PSBGColorDialog.Color()
                Me.PS_Refresh()
            End If
        ElseIf e.Button = Forms.MouseButtons.Right Then
            If Me.PSOpenFileDialog.ShowDialog() = Forms.DialogResult.OK Then
                Me.BGImgTemp = Me.PSOpenFileDialog.FileName
                Me.PS_Refresh()
            End If
        End If
    End Sub

    Private Sub PSBGImgCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PSBGImgCheckBox.CheckedChanged
        Me.BGImgCheckTemp = Me.PSBGImgCheckBox.Checked
        Me.PS_Refresh()
    End Sub

    Private Sub PSOKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PSOKButton.Click
        My.Settings.BGImg = Me.BGImgTemp
        My.Settings.BGImgCheck = Me.BGImgCheckTemp
        My.Settings.BGColor = Me.BGColorTemp
        My.Settings.TitleColor = Me.TitleColorTemp
        My.Settings.TitleFont = Me.TitleFontTemp
        My.Settings.LyricsColor = Me.LyricsColorTemp
        My.Settings.LyricsFont = Me.LyricsFontTemp
        My.Settings.Save()

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub PSCancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PSCancelButton.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class