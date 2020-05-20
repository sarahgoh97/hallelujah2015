Imports System.Data

Partial Public Class PresentationPage
    Private currentIndex As Integer
    Private totalCount As Integer
    Private currentSeq As Integer
    Private originalListBox As ListBox
    Private myCurrentDataTable As New DataTable("Table")
    Private black As Boolean = False
    Private display As Boolean = True

    Dim myDbConnection As New SqlClient.SqlConnection()

    Public Sub New(ByVal playlist As ListBox, ByVal index As Integer, ByVal count As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent()
        Dim myFFC As New FontFamilyConverter()

        myDbConnection.ConnectionString = My.Settings.DBConnection

        Me.PP_SetBG()
        Me.PPSeqListBox.Background = Nothing
        Me.PPTitleTextBlock.Foreground = New SolidColorBrush(Windows.Media.Color.FromArgb(My.Settings.TitleColor.A, My.Settings.TitleColor.R, My.Settings.TitleColor.G, My.Settings.TitleColor.B))
        Me.PPTitleTextBlock.FontFamily = myFFC.ConvertFromString(My.Settings.TitleFont.FontFamily.Name)
        Me.PP_FontStyleConverter(Me.PPTitleTextBlock, My.Settings.TitleFont)
        Me.PPLyricsTextBlock.Foreground = New SolidColorBrush(Windows.Media.Color.FromArgb(My.Settings.LyricsColor.A, My.Settings.LyricsColor.R, My.Settings.LyricsColor.G, My.Settings.LyricsColor.B))
        Me.PPLyricsTextBlock.FontFamily = myFFC.ConvertFromString(My.Settings.LyricsFont.FontFamily.Name)
        Me.PP_FontStyleConverter(Me.PPLyricsTextBlock, My.Settings.LyricsFont)
        Me.PP_SetHelpKeys()

        Me.totalCount = count
        Me.originalListBox = playlist
        Me.PP_Initialize(index)
    End Sub

    Private Sub PP_SetBG()
        If My.Settings.BGImgCheck Then
            Dim myImgSrc As ImageSource = New BitmapImage(New Uri(My.Settings.BGImg))
            Dim myImgBrush As New ImageBrush(myImgSrc)
            Me.Background = myImgBrush
        Else
            Me.Background = New SolidColorBrush(Windows.Media.Color.FromArgb(My.Settings.BGColor.A, My.Settings.BGColor.R, My.Settings.BGColor.G, My.Settings.BGColor.B))
        End If
    End Sub

    Private Sub PP_FontStyleConverter(ByRef myTextBlock As TextBlock, ByVal myFont As System.Drawing.Font)
        If myFont.Bold Then
            myTextBlock.FontWeight = FontWeights.Bold
        End If
        If myFont.Italic Then
            myTextBlock.FontStyle = FontStyles.Italic
        End If
        If myFont.Strikeout Then
            myTextBlock.TextDecorations.Add(TextDecorations.Strikethrough)
        End If
        If myFont.Underline Then
            myTextBlock.TextDecorations.Add(TextDecorations.Underline)
        End If
    End Sub

    Private Class myItem
        Public Property Name As String
        Public Property Key As String
        Public Sub New(ByVal myName As String, ByVal myKey As String)
            Me.Name = myName
            Me.Key = myKey
        End Sub
    End Class

    Private Sub PP_SetHelpKeys()
        Me.PPHelpListView.Items.Add(New myItem("Next Slide", My.Settings.NextSlideKey))
        Me.PPHelpListView.Items.Add(New myItem("Previous Slide", My.Settings.PrevSlideKey))
        Me.PPHelpListView.Items.Add(New myItem("Next Song", My.Settings.NextSongKey))
        Me.PPHelpListView.Items.Add(New myItem("Previous Song", My.Settings.PrevSongKey))
        Me.PPHelpListView.Items.Add(New myItem("Jump to Song 1", My.Settings.JumpSong1Key))
        Me.PPHelpListView.Items.Add(New myItem("Jump to Song 2", My.Settings.JumpSong2Key))
        Me.PPHelpListView.Items.Add(New myItem("Jump to Song 3", My.Settings.JumpSong3Key))
        Me.PPHelpListView.Items.Add(New myItem("Jump to Song 4", My.Settings.JumpSong4Key))
        Me.PPHelpListView.Items.Add(New myItem("Jump to Song 5", My.Settings.JumpSong5Key))
        Me.PPHelpListView.Items.Add(New myItem("Jump to Song 6", My.Settings.JumpSong6Key))
        Me.PPHelpListView.Items.Add(New myItem("Jump to Song 7", My.Settings.JumpSong7Key))
        Me.PPHelpListView.Items.Add(New myItem("Jump to Song 8", My.Settings.JumpSong8Key))
        Me.PPHelpListView.Items.Add(New myItem("Jump to Song 9", My.Settings.JumpSong9Key))
        Me.PPHelpListView.Items.Add(New myItem("Jump to Song 10", My.Settings.JumpSong10Key))
        Me.PPHelpListView.Items.Add(New myItem("Black Screen", My.Settings.BlackScrKey))
        Me.PPHelpListView.Items.Add(New myItem("Toggle Repeat", My.Settings.RepeatKey))
        Me.PPHelpListView.Items.Add(New myItem("Toggle Display", My.Settings.DisplayKey))
        Me.PPHelpListView.Items.Add(New myItem("Toggle Help", My.Settings.HelpKey))
        Me.PPHelpListView.Items.Add(New myItem("Exit", My.Settings.ExitPageKey))
    End Sub

    Private Sub PP_Initialize(ByVal index As Integer, Optional ByVal seqNum As Integer = 0)
        Dim myPrevDataTable As New DataTable("Table")
        Dim myNextDataTable As New DataTable("Table")

        ' Get content of ListBox as DataTable with (0) as ID and (1) as Title
        Me.myCurrentDataTable = Me.originalListBox.Items(index)

        ' Clear form for new slide
        Me.PPSeqListBox.Items.Clear()

        Me.currentIndex = index
        Me.currentSeq = seqNum

        Me.Cursor = System.Windows.Input.Cursors.None

        Me.PPTitleTextBlock.Text = myCurrentDataTable.Rows(0)(1)

        If index > 0 Then
            myPrevDataTable = Me.originalListBox.Items(index - 1)
            Me.PPPrevTextBlock.Text = "Previous Song: " & myPrevDataTable.Rows(0)(1)
        Else
            Me.PPPrevTextBlock.Text = ""
        End If
        If index < Me.totalCount - 1 Then
            myNextDataTable = Me.originalListBox.Items(index + 1)
            Me.PPNextTextBlock.Text = "Next Song: " & myNextDataTable.Rows(0)(1)
        Else
            Me.PPNextTextBlock.Text = ""
        End If

        Dim myCommand As New SqlClient.SqlCommand
        Dim myReader As SqlClient.SqlDataReader
        Dim mySeq As String
        Dim myPos1 As Integer = 0
        Dim myPos2 As Integer = 0

        myCommand.Connection = myDbConnection
        myCommand.Connection.Open()

        ' Determine sequence of lyrics to display
        myCommand.CommandText = "SELECT Sequence FROM SONGS WHERE ID = '" & myCurrentDataTable.Rows(0)(0) & "'"
        myReader = myCommand.ExecuteReader()
        myReader.Read()
        mySeq = myReader.GetString(0)

        myReader.Close()
        myCommand.Connection.Close()

        ' Next in sequence separated by ','
        Do
            myPos2 = InStr(myPos1 + 1, mySeq, ",", CompareMethod.Text)
            If myPos2 = 0 Then
                Me.PPSeqListBox.Items.Add(Mid(mySeq, myPos1 + 1))
            Else
                Me.PPSeqListBox.Items.Add(Mid(mySeq, myPos1 + 1, myPos2 - myPos1 - 1))
            End If
            myPos1 = myPos2
        Loop While myPos2 <> 0

        Me.PPSeqListBox_Refresh()
        Me.PPLyricsTextBlock_Refresh()
    End Sub

    Private Sub PPSeqListBox_Refresh(Optional ByVal prevSeq As Integer = -1)
        If prevSeq <> -1 Then
            Me.PPSeqListBox.Items(prevSeq) = Me.PPSeqListBox.Items(prevSeq).ToString.Remove(Me.PPSeqListBox.Items(prevSeq).ToString.Count - 3)
        End If
        Me.PPSeqListBox.Items(Me.currentSeq) = Me.PPSeqListBox.Items(Me.currentSeq).ToString.Insert(Me.PPSeqListBox.Items(Me.currentSeq).ToString.Count, " <<")
    End Sub

    'Private Sub PPSeqListBox_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles PPSeqListBox.SelectionChanged
    Private Sub PPLyricsTextBlock_Refresh()
        Dim myCommand As New SqlClient.SqlCommand
        myCommand.Connection = myDbConnection
        myCommand.Connection.Open()

        ' Retrieve lyrics to display
        Dim myType As String = Me.PPSeqListBox.Items(Me.currentSeq).ToString.Remove(Me.PPSeqListBox.Items(Me.currentSeq).ToString.Count - 3).Replace("'", "''")
        Me.PPLyricsTextBlock.Text = ""
        myCommand.CommandText = "SELECT Lyrics FROM LYRICS WHERE ID = '" & Me.myCurrentDataTable.Rows(0)(0) & "' AND Type = '" & myType & "'"
        Dim myReader As SqlClient.SqlDataReader = myCommand.ExecuteReader()

        If myReader.Read() Then
            Me.PPLyricsTextBlock.Text = myReader.GetString(0)
        End If

        myReader.Close()
        myCommand.Connection.Close()
    End Sub

    Private Sub PP_Submit(ByVal e As String)
        Select Case e
            Case My.Settings.JumpSong1Key
                Me.PP_JumpSong(0)
            Case My.Settings.JumpSong2Key
                Me.PP_JumpSong(1)
            Case My.Settings.JumpSong3Key
                Me.PP_JumpSong(2)
            Case My.Settings.JumpSong4Key
                Me.PP_JumpSong(3)
            Case My.Settings.JumpSong5Key
                Me.PP_JumpSong(4)
            Case My.Settings.JumpSong6Key
                Me.PP_JumpSong(5)
            Case My.Settings.JumpSong7Key
                Me.PP_JumpSong(6)
            Case My.Settings.JumpSong8Key
                Me.PP_JumpSong(7)
            Case My.Settings.JumpSong9Key
                Me.PP_JumpSong(8)
            Case My.Settings.JumpSong10Key
                Me.PP_JumpSong(9)
            Case My.Settings.NextSlideKey
                Me.PP_NextSlide()
            Case My.Settings.PrevSlideKey
                Me.PP_PreviousSlide()
            Case My.Settings.NextSongKey
                Me.PP_NextSong()
            Case My.Settings.PrevSongKey
                Me.PP_PreviousSong()
            Case My.Settings.ExitPageKey
                Me.Close()
            Case My.Settings.BlackScrKey
                Me.PP_BlackScreen()
            Case My.Settings.RepeatKey
                Me.PP_Repeat()
            Case My.Settings.DisplayKey
                Me.PP_DisplayInfo()
            Case My.Settings.HelpKey
                Me.PP_Help()
            Case "mouseLeft"
                Me.PP_NextSlide()
            Case "mouseRight"
                Me.PP_PreviousSlide()
            Case "mouseDoubleLeft"
                Me.PP_NextSong()
            Case "mouseDoubleRight"
                Me.PP_PreviousSong()
            Case Else
                ' To show the key pressed
                ' MsgBox(e, MsgBoxStyle.Information)
        End Select
    End Sub

    Private Sub PP_NextSlide()
        ' Only works if command not invoked from a black screen
        If Not Me.black Then
            If Me.currentSeq + 1 = Me.PPSeqListBox.Items.Count Then
                Me.PP_NextSong()
            Else
                Me.currentSeq += 1
                Me.PPSeqListBox_Refresh(Me.currentSeq - 1)
                Me.PPLyricsTextBlock_Refresh()
            End If
        End If
    End Sub

    Private Sub PP_PreviousSlide()
        ' Only works if command not invoked from a black screen
        If Not Me.black Then
            If Me.currentSeq = 0 Then
                Me.PP_PreviousSong()
            Else
                Me.currentSeq -= 1
                Me.PPSeqListBox_Refresh(Me.currentSeq + 1)
                Me.PPLyricsTextBlock_Refresh()
            End If
        End If
    End Sub

    Private Sub PP_NextSong()
        ' Only works if command not invoked from a black screen
        If Not Me.black Then
            If Me.currentIndex < Me.totalCount - 1 Then
                Me.PP_Initialize(Me.currentIndex + 1)
            Else
                Me.Close()
            End If
        End If
    End Sub

    Private Sub PP_PreviousSong()
        ' Only works if command not invoked from a black screen
        If Not Me.black Then
            If Me.currentIndex > 0 Then
                Me.PP_Initialize(Me.currentIndex - 1)
            End If
        End If
    End Sub

    Private Sub PP_JumpSong(ByVal index As Integer)
        ' Only works if command not invoked from a black screen
        If Not Me.black Then
            If index > -1 And index < Me.totalCount Then
                Me.PP_Initialize(index)
            End If
        End If
    End Sub

    Private Sub PP_BlackScreen()
        If Me.black Then
            Me.PPTitleTextBlock.Visibility = Windows.Visibility.Visible
            Me.PPLyricsTextBlock.Visibility = Windows.Visibility.Visible
            Me.PPSeqListBox.Visibility = Windows.Visibility.Visible
            Me.PPPrevTextBlock.Visibility = Windows.Visibility.Visible
            Me.PPNextTextBlock.Visibility = Windows.Visibility.Visible
            Me.PP_SetBG()
            Me.black = False
        Else
            Me.PPTitleTextBlock.Visibility = Windows.Visibility.Hidden
            Me.PPLyricsTextBlock.Visibility = Windows.Visibility.Hidden
            Me.PPSeqListBox.Visibility = Windows.Visibility.Hidden
            Me.PPPrevTextBlock.Visibility = Windows.Visibility.Hidden
            Me.PPNextTextBlock.Visibility = Windows.Visibility.Hidden
            Me.PPHelpListView.Visibility = Windows.Visibility.Hidden
            Dim myBC As New BrushConverter()
            Me.Background = myBC.ConvertFromString("Black")
            Me.black = True
        End If
    End Sub

    Private Sub PP_Repeat()
        ' Only works if command not invoked from a black screen
        If Not Me.black Then
            If Me.PPRepeatTextBlock.Visibility = Windows.Visibility.Hidden Then
                Me.PPRepeatTextBlock.Visibility = Windows.Visibility.Visible
            Else
                Me.PPRepeatTextBlock.Visibility = Windows.Visibility.Hidden
            End If
        End If
    End Sub

    Private Sub PP_DisplayInfo()
        ' Only works if command not invoked from a black screen
        If Not Me.black Then
            If Me.display Then
                Me.PPSeqListBox.Visibility = Windows.Visibility.Hidden
                Me.PPPrevTextBlock.Visibility = Windows.Visibility.Hidden
                Me.PPNextTextBlock.Visibility = Windows.Visibility.Hidden
                Me.display = False
            Else
                Me.PPSeqListBox.Visibility = Windows.Visibility.Visible
                Me.PPPrevTextBlock.Visibility = Windows.Visibility.Visible
                Me.PPNextTextBlock.Visibility = Windows.Visibility.Visible
                Me.display = True
            End If
        End If
    End Sub

    Private Sub PP_Help()
        ' Only works if command not invoked from a black screen
        If Not Me.black Then
            If Me.PPHelpListView.Visibility = Windows.Visibility.Hidden Then
                Me.PPHelpListView.Visibility = Windows.Visibility.Visible
            Else
                Me.PPHelpListView.Visibility = Windows.Visibility.Hidden
            End If
        End If
    End Sub

    Private Sub PresentationPage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles Me.KeyDown
        Me.PP_Submit(e.Key.ToString)
        e.Handled = True
    End Sub

    Private Sub PresentationPage_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Me.MouseDoubleClick
        If e.LeftButton Then
            Me.PP_Submit("mouseDoubleLeft")
        ElseIf e.RightButton Then
            Me.PP_Submit("mouseDoubleRight")
        End If
        e.Handled = True
    End Sub

    Private Sub PresentationPage_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Me.MouseDown
        If e.LeftButton Then
            Me.PP_Submit("mouseLeft")
        ElseIf e.RightButton Then
            Me.PP_Submit("mouseRight")
        End If
        e.Handled = True
    End Sub
End Class