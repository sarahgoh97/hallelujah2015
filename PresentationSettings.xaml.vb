Public Class PresentationSettings
    Dim BGColorTemp, TitleColorTemp, LyricsColorTemp As System.Drawing.Color
    Dim TitleFontTemp, LyricsFontTemp As System.Drawing.Font
    Dim BGImgTemp As String
    Dim BGImgCheckTemp As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.PS_SetBG()
        Me.PSTitleTextBlock.Foreground = New SolidColorBrush(Windows.Media.Color.FromArgb(My.Settings.TitleColor.A, My.Settings.TitleColor.R, My.Settings.TitleColor.G, My.Settings.TitleColor.B))
        Me.PSLyricsTextBlock.Foreground = New SolidColorBrush(Windows.Media.Color.FromArgb(My.Settings.LyricsColor.A, My.Settings.LyricsColor.R, My.Settings.LyricsColor.G, My.Settings.LyricsColor.B))
    End Sub

    Private Sub PS_SetBG()
        If My.Settings.BGImgCheck Then
            Dim myImgSrc As ImageSource = New BitmapImage(New Uri(My.Settings.BGImg))
            Dim myImgBrush As New ImageBrush(myImgSrc)
            Me.Background = myImgBrush
        Else
            Me.Background = New SolidColorBrush(Windows.Media.Color.FromArgb(My.Settings.BGColor.A, My.Settings.BGColor.R, My.Settings.BGColor.G, My.Settings.BGColor.B))
        End If
    End Sub

    Private Sub PSTitleTextBlock_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles PSTitleTextBlock.MouseUp
        If e.ChangedButton = MouseButton.Left Then
            Dim CP As New ColorPicker()
            CP.CPColorPicker.SelectedColor = Windows.Media.Color.FromArgb(My.Settings.TitleColor.A, My.Settings.TitleColor.R, My.Settings.TitleColor.G, My.Settings.TitleColor.B)
            If CP.ShowDialog() Then
                Me.TitleColorTemp = System.Drawing.Color.FromArgb(CP.CPColorPicker.SelectedColor.Value.A, CP.CPColorPicker.SelectedColor.Value.R, CP.CPColorPicker.SelectedColor.Value.G, CP.CPColorPicker.SelectedColor.Value.B)
                Me.PSTitleTextBlock.Foreground = New SolidColorBrush(CP.CPColorPicker.SelectedColor)
            End If
        ElseIf e.ChangedButton = MouseButton.Right Then

        End If
    End Sub

    Private Sub PSLyricsTextBlock_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles PSLyricsTextBlock.MouseUp
        If e.ChangedButton = MouseButton.Left Then
            Dim CP As New ColorPicker()
            CP.CPColorPicker.SelectedColor = Windows.Media.Color.FromArgb(My.Settings.LyricsColor.A, My.Settings.LyricsColor.R, My.Settings.LyricsColor.G, My.Settings.LyricsColor.B)
            If CP.ShowDialog() Then
                Me.LyricsColorTemp = System.Drawing.Color.FromArgb(CP.CPColorPicker.SelectedColor.Value.A, CP.CPColorPicker.SelectedColor.Value.R, CP.CPColorPicker.SelectedColor.Value.G, CP.CPColorPicker.SelectedColor.Value.B)
                Me.PSLyricsTextBlock.Foreground = New SolidColorBrush(CP.CPColorPicker.SelectedColor)
            End If
        ElseIf e.ChangedButton = MouseButton.Right Then

        End If
    End Sub

    Private Sub PresentationSettings_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseUp
        If e.ChangedButton = MouseButton.Left Then
            Dim CP As New ColorPicker()
            CP.CPColorPicker.SelectedColor = Windows.Media.Color.FromArgb(My.Settings.BGColor.A, My.Settings.BGColor.R, My.Settings.BGColor.G, My.Settings.BGColor.B)
            If CP.ShowDialog() Then
                Me.BGColorTemp = System.Drawing.Color.FromArgb(CP.CPColorPicker.SelectedColor.Value.A, CP.CPColorPicker.SelectedColor.Value.R, CP.CPColorPicker.SelectedColor.Value.G, CP.CPColorPicker.SelectedColor.Value.B)
                Me.Background = New SolidColorBrush(CP.CPColorPicker.SelectedColor)
            End If
        ElseIf e.ChangedButton = MouseButton.Right Then

        End If
    End Sub

    Private Sub PSSave()
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

    Private Sub PSCancel()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class
