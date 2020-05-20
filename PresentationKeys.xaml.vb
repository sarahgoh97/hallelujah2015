Partial Public Class PresentationKeys
    Dim NextSlide, PrevSlide, NextSong, PrevSong, Song1, Song2, Song3, Song4, Song5, Song6, Song7, Song8, Song9, Song10, BlackScr, Repeat, Display, Help, ExitPage As String

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Initialize the variables
        Me.NextSlide = My.Settings.NextSlideKey
        Me.PrevSlide = My.Settings.PrevSlideKey
        Me.NextSong = My.Settings.NextSongKey
        Me.PrevSong = My.Settings.PrevSongKey
        Me.Song1 = My.Settings.JumpSong1Key
        Me.Song2 = My.Settings.JumpSong2Key
        Me.Song3 = My.Settings.JumpSong3Key
        Me.Song4 = My.Settings.JumpSong4Key
        Me.Song5 = My.Settings.JumpSong5Key
        Me.Song6 = My.Settings.JumpSong6Key
        Me.Song7 = My.Settings.JumpSong7Key
        Me.Song8 = My.Settings.JumpSong8Key
        Me.Song9 = My.Settings.JumpSong9Key
        Me.Song10 = My.Settings.JumpSong10Key
        Me.BlackScr = My.Settings.BlackScrKey
        Me.Repeat = My.Settings.RepeatKey
        Me.Display = My.Settings.DisplayKey
        Me.Help = My.Settings.HelpKey
        Me.ExitPage = My.Settings.ExitPageKey

        ' Initialize the content of the buttons
        Me.PKNextSlideButton.Content = Me.NextSlide
        Me.PKPrevSlideButton.Content = Me.PrevSlide
        Me.PKNextSongButton.Content = Me.NextSong
        Me.PKPrevSongButton.Content = Me.PrevSong
        Me.PKSong1Button.Content = Me.Song1
        Me.PKSong2Button.Content = Me.Song2
        Me.PKSong3Button.Content = Me.Song3
        Me.PKSong4Button.Content = Me.Song4
        Me.PKSong5Button.Content = Me.Song5
        Me.PKSong6Button.Content = Me.Song6
        Me.PKSong7Button.Content = Me.Song7
        Me.PKSong8Button.Content = Me.Song8
        Me.PKSong9Button.Content = Me.Song9
        Me.PKSong10Button.Content = Me.Song10
        Me.PKBlackScrButton.Content = Me.BlackScr
        Me.PKRepeatButton.Content = Me.Repeat
        Me.PKDisplayButton.Content = Me.Display
        Me.PKHelpButton.Content = Me.Help
        Me.PKExitPageButton.Content = Me.ExitPage
    End Sub

    Private Sub PKOK()
        ' Save the settings
        My.Settings.NextSlideKey = Me.NextSlide
        My.Settings.PrevSlideKey = Me.PrevSlide
        My.Settings.NextSongKey = Me.NextSong
        My.Settings.PrevSongKey = Me.PrevSong
        My.Settings.JumpSong1Key = Me.Song1
        My.Settings.JumpSong2Key = Me.Song2
        My.Settings.JumpSong3Key = Me.Song3
        My.Settings.JumpSong4Key = Me.Song4
        My.Settings.JumpSong5Key = Me.Song5
        My.Settings.JumpSong6Key = Me.Song6
        My.Settings.JumpSong7Key = Me.Song7
        My.Settings.JumpSong8Key = Me.Song8
        My.Settings.JumpSong9Key = Me.Song9
        My.Settings.JumpSong10Key = Me.Song10
        My.Settings.BlackScrKey = Me.BlackScr
        My.Settings.RepeatKey = Me.Repeat
        My.Settings.DisplayKey = Me.Display
        My.Settings.HelpKey = Me.Help
        My.Settings.ExitPageKey = Me.ExitPage
        My.Settings.Save()

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub PKCancel()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Function GetKey(ByVal myButton As Windows.Controls.Button) As String
        Dim SK As New ShortcutKey()
        SK.ShowDialog()

        ' Check for clash with existing shortcut key assignment
        ' Remove old assignment if clash exists
        If Me.NextSlide = SK.myKey Then
            Me.NextSlide = ""
            Me.PKNextSlideButton.Content = ""
        ElseIf Me.PrevSlide = SK.myKey Then
            Me.PrevSlide = ""
            Me.PKPrevSlideButton.Content = ""
        ElseIf Me.NextSong = SK.myKey Then
            Me.NextSong = ""
            Me.PKNextSongButton.Content = ""
        ElseIf Me.PrevSong = SK.myKey Then
            Me.PrevSong = ""
            Me.PKPrevSongButton.Content = ""
        ElseIf Me.Song1 = SK.myKey Then
            Me.Song1 = ""
            Me.PKSong1Button.Content = ""
        ElseIf Me.Song2 = SK.myKey Then
            Me.Song2 = ""
            Me.PKSong2Button.Content = ""
        ElseIf Me.Song3 = SK.myKey Then
            Me.Song3 = ""
            Me.PKSong3Button.Content = ""
        ElseIf Me.Song4 = SK.myKey Then
            Me.Song4 = ""
            Me.PKSong4Button.Content = ""
        ElseIf Me.Song5 = SK.myKey Then
            Me.Song5 = ""
            Me.PKSong5Button.Content = ""
        ElseIf Me.Song6 = SK.myKey Then
            Me.Song6 = ""
            Me.PKSong6Button.Content = ""
        ElseIf Me.Song7 = SK.myKey Then
            Me.Song7 = ""
            Me.PKSong7Button.Content = ""
        ElseIf Me.Song8 = SK.myKey Then
            Me.Song8 = ""
            Me.PKSong8Button.Content = ""
        ElseIf Me.Song9 = SK.myKey Then
            Me.Song9 = ""
            Me.PKSong9Button.Content = ""
        ElseIf Me.Song10 = SK.myKey Then
            Me.Song10 = ""
            Me.PKSong10Button.Content = ""
        ElseIf Me.BlackScr = SK.myKey Then
            Me.BlackScr = ""
            Me.PKBlackScrButton.Content = ""
        ElseIf Me.Repeat = SK.myKey Then
            Me.Repeat = ""
            Me.PKRepeatButton.Content = ""
        ElseIf Me.Display = SK.myKey Then
            Me.Display = ""
            Me.PKDisplayButton.Content = ""
        ElseIf Me.Help = SK.myKey Then
            Me.Help = ""
            Me.PKHelpButton.Content = ""
        ElseIf Me.ExitPage = SK.myKey Then
            Me.ExitPage = ""
            Me.PKExitPageButton.Content = ""
        End If

        myButton.Content = SK.myKey
        Return SK.myKey
    End Function

    Private Sub PKNextSlideButton_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKNextSlideButton.Click
        Me.NextSlide = Me.GetKey(Me.PKNextSlideButton)
    End Sub

    Private Sub PKPrevSlideButton_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKPrevSlideButton.Click
        Me.PrevSlide = Me.GetKey(Me.PKPrevSlideButton)
    End Sub

    Private Sub PKNextSongButton_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKNextSongButton.Click
        Me.NextSong = Me.GetKey(Me.PKNextSongButton)
    End Sub

    Private Sub PKPrevSongButton_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKPrevSongButton.Click
        Me.PrevSong = Me.GetKey(Me.PKPrevSongButton)
    End Sub

    Private Sub PKSong1Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKSong1Button.Click
        Me.Song1 = Me.GetKey(Me.PKSong1Button)
    End Sub

    Private Sub PKSong2Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKSong2Button.Click
        Me.Song2 = Me.GetKey(Me.PKSong2Button)
    End Sub

    Private Sub PKSong3Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKSong3Button.Click
        Me.Song3 = Me.GetKey(Me.PKSong3Button)
    End Sub

    Private Sub PKSong4Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKSong4Button.Click
        Me.Song4 = Me.GetKey(Me.PKSong4Button)
    End Sub

    Private Sub PKSong5Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKSong5Button.Click
        Me.Song5 = Me.GetKey(Me.PKSong5Button)
    End Sub

    Private Sub PKSong6Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKSong6Button.Click
        Me.Song6 = Me.GetKey(Me.PKSong6Button)
    End Sub

    Private Sub PKSong7Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKSong7Button.Click
        Me.Song7 = Me.GetKey(Me.PKSong7Button)
    End Sub

    Private Sub PKSong8Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKSong8Button.Click
        Me.Song8 = Me.GetKey(Me.PKSong8Button)
    End Sub

    Private Sub PKSong9Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKSong9Button.Click
        Me.Song9 = Me.GetKey(Me.PKSong9Button)
    End Sub

    Private Sub PKSong10Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKSong10Button.Click
        Me.Song10 = Me.GetKey(Me.PKSong10Button)
    End Sub

    Private Sub PKBlackScrButton_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKBlackScrButton.Click
        Me.BlackScr = Me.GetKey(Me.PKBlackScrButton)
    End Sub

    Private Sub PKRepeatButton_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKRepeatButton.Click
        Me.Repeat = Me.GetKey(Me.PKRepeatButton)
    End Sub

    Private Sub PKDisplayButton_Click(sender As Object, e As RoutedEventArgs) Handles PKDisplayButton.Click
        Me.Display = Me.GetKey(Me.PKDisplayButton)
    End Sub

    Private Sub PKHelpButton_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKHelpButton.Click
        Me.Help = Me.GetKey(Me.PKHelpButton)
    End Sub

    Private Sub PKExitPageButton_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PKExitPageButton.Click
        Me.ExitPage = Me.GetKey(Me.PKExitPageButton)
    End Sub
End Class