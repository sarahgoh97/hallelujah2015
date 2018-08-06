Imports System.Data

Partial Public Class EditSong
    Dim myDbConnection As New SqlClient.SqlConnection()
    Dim NewSong As Boolean
    Dim SongID As Integer
    Dim songDataSet As DataSet
    Dim lyricsDataSet As DataSet

    Public Sub New(ByVal isNewSong As Boolean, Optional ByVal mySongID As Integer = 0, Optional ByVal mySongTitle As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        myDbConnection.ConnectionString = My.Settings.DBConnection
        Me.NewSong = isNewSong
        Me.SongID = mySongID
        If Not isNewSong And mySongTitle <> "" Then
            Me.Title = mySongTitle
        End If
        For Each myItem In My.Settings.KeyList
            Me.ESKeyComboBox.Items.Add(myItem)
        Next
        For Each myItem In My.Settings.TimeSignList
            Me.ESTimeSignComboBox.Items.Add(myItem)
        Next
        For Each myItem In My.Settings.LyricsTypeList
            Me.ESLyricsTypeComboBox.Items.Add(myItem)
        Next
        myDbConnection.Close()
        Me.ESContent_Initialize()
    End Sub

    Sub ESContent_Initialize()
        Dim myAdapter As New SqlClient.SqlDataAdapter
        Dim myCommand As New SqlClient.SqlCommand
        myCommand.Connection = myDbConnection
        If Me.NewSong Then
            myCommand.CommandText = "SELECT TOP 0 * FROM SONGS"
        Else
            myCommand.CommandText = "SELECT * FROM SONGS WHERE ID = '" & Me.SongID & "'"
        End If

        myAdapter.SelectCommand = myCommand

        songDataSet = New DataSet()
        myAdapter.Fill(songDataSet)

        If Not Me.NewSong Then
            Me.ESTitleTextBox.Text = songDataSet.Tables(0).Rows(0)("Title")
            Me.ESWriterTextBox.Text = songDataSet.Tables(0).Rows(0)("Author")
            Me.ESRefTextBox.Text = songDataSet.Tables(0).Rows(0)("Reference")
            Me.ESKeyComboBox.Text = songDataSet.Tables(0).Rows(0)("KeyValue")
            Me.ESTimeSignComboBox.Text = songDataSet.Tables(0).Rows(0)("TimeSignature")

            If songDataSet.Tables(0).Rows(0)("Sequence") <> "" Then
                Dim seq As String() = Split(songDataSet.Tables(0).Rows(0)("Sequence"), ",")
                For Each s As String In seq
                    Me.ESDefSeqListBox.Items.Add(s)
                Next
            End If
        End If
        myCommand.CommandText = "SELECT * FROM LYRICS WHERE ID = '" & SongID & "'"
        myAdapter.SelectCommand = myCommand

        lyricsDataSet = New DataSet
        myAdapter.Fill(lyricsDataSet)

        For Each dr As DataRow In lyricsDataSet.Tables(0).Rows
            Me.ESLyricsTypeListBox.Items.Add(dr("Type"))
        Next
    End Sub

    Private Sub ESUpButton_Click(sender As Object, e As RoutedEventArgs) Handles ESUpButton.Click
        Dim myIndex As Integer

        myIndex = Me.ESDefSeqListBox.SelectedIndex

        ' Only if selected item is not the first one in the listview
        If myIndex <> 0 Then
            Me.ESDefSeqListBox.Items.Insert(myIndex - 1, Me.ESDefSeqListBox.Items(myIndex))

            ' Select item so that event SelectionChanged is activated
            Me.ESDefSeqListBox.SelectedIndex = myIndex - 1

            Me.ESDefSeqListBox.Items.RemoveAt(myIndex + 1)
        End If
    End Sub

    Private Sub ESDownButton_Click(sender As Object, e As RoutedEventArgs) Handles ESDownButton.Click
        Dim myIndex As Integer

        myIndex = Me.ESDefSeqListBox.SelectedIndex

        ' Only if selected item is not the last one in the listview
        If myIndex <> Me.ESDefSeqListBox.Items.Count - 1 Then
            Me.ESDefSeqListBox.Items.Insert(myIndex + 2, Me.ESDefSeqListBox.Items(myIndex))

            ' Select item so that event SelectionChanged is activated
            Me.ESDefSeqListBox.SelectedIndex = myIndex + 2

            Me.ESDefSeqListBox.Items.RemoveAt(myIndex)
        End If
    End Sub

    Private Sub ESAddButton_Click(sender As Object, e As RoutedEventArgs) Handles ESAddButton.Click
        Me.ESDefSeqListBox.Items.Add(Me.ESLyricsTypeListBox.SelectedItem)
    End Sub

    Private Sub ESRemoveButton_Click(sender As Object, e As RoutedEventArgs) Handles ESRemoveButton.Click
        Me.ESDefSeqListBox.Items.RemoveAt(Me.ESDefSeqListBox.SelectedIndex)
        Me.ESRemoveButton.IsEnabled = False
    End Sub

    Private Sub ESSaveChangesButton_Click(sender As Object, e As RoutedEventArgs) Handles ESSaveChangesButton.Click
        ' Adding a new lyrics type to the song
        If Me.ESLyricsTypeComboBox.SelectedItem <> vbNullString And Me.ESLyricsTypeListBox.SelectedItems.Count <> 1 Then
            Dim dr As DataRow = lyricsDataSet.Tables(0).NewRow
            dr("ID") = Me.SongID

            Dim lyricsType As String = Me.ESLyricsTypeComboBox.SelectedItem
            Dim lyricsRow As DataRow()
            lyricsRow = lyricsDataSet.Tables(0).Select("Type LIKE '" & Me.ESLyricsTypeComboBox.SelectedValue & "*'")
            ' Loop through the number of similar lyrics type to get the possible numbered type
            For i As Integer = 1 To lyricsRow.Count()
                Dim matched As Boolean = False
                For Each lr As DataRow In lyricsRow
                    ' If the type match
                    If Not String.Compare(lyricsType & " " & i, lr(1)) Then
                        matched = True
                    End If
                Next
                ' Found an empty numbered type to use
                If Not matched Then
                    lyricsType = lyricsType & " " & i
                    Exit For
                End If
            Next
            ' If no other numbered type found
            If Not String.Compare(lyricsType, Me.ESLyricsTypeComboBox.SelectedItem) Then
                lyricsType = lyricsType & " " & lyricsRow.Count + 1
            End If
            dr("Type") = lyricsType
            dr("Lyrics") = Me.ESLyricsTextBox.Text

            lyricsDataSet.Tables(0).Rows.Add(dr)
            Me.ESLyricsTypeListBox.Items.Add(lyricsType)
            Me.ESLyricsTypeListBox.SelectedIndex = Me.ESLyricsTypeListBox.Items.Count - 1

            ' Updating existing lyrics type of the song
        ElseIf Me.ESLyricsTypeComboBox.SelectedItem = vbNullString And Me.ESLyricsTypeListBox.SelectedItems.Count = 1 Then
            Dim dr As DataRow() = lyricsDataSet.Tables(0).Select("Type = '" & Me.ESLyricsTypeListBox.SelectedValue & "'")
            dr(0)("Lyrics") = Me.ESLyricsTextBox.Text
        End If

        Me.ESSaveChangesButton.IsEnabled = False
    End Sub

    Private Sub ESDeleteButton_Click(sender As Object, e As RoutedEventArgs) Handles ESDeleteButton.Click
        lyricsDataSet.Tables(0).Rows.RemoveAt(Me.ESLyricsTypeListBox.SelectedIndex)
        For i As Integer = Me.ESDefSeqListBox.Items.Count - 1 To 0 Step -1
            If Me.ESDefSeqListBox.Items(i) = Me.ESLyricsTypeListBox.SelectedItem Then
                Me.ESDefSeqListBox.Items.RemoveAt(i)
            End If
        Next
        Me.ESLyricsTypeListBox.Items.RemoveAt(Me.ESLyricsTypeListBox.SelectedIndex)
    End Sub

    Private Sub SaveCanExecute(sender As Object, e As CanExecuteRoutedEventArgs)
        e.CanExecute = (ESTitleTextBox.Text <> vbNullString) And (ESKeyComboBox.SelectedValue <> vbNullString) And (ESTimeSignComboBox.SelectedValue <> vbNullString)
    End Sub

    Private Sub ESSave()
        Dim mySeq As String = ""
        Dim myAdapter As New SqlClient.SqlDataAdapter
        Dim songsCommand As New SqlClient.SqlCommand
        songsCommand.Connection = myDbConnection

        songsCommand.Parameters.Add("@Title", SqlDbType.NVarChar, 50, "Title")
        songsCommand.Parameters.Add("@Author", SqlDbType.NVarChar, 100, "Author")
        songsCommand.Parameters.Add("@Reference", SqlDbType.NVarChar, 100, "Reference")
        songsCommand.Parameters.Add("@KeyValue", SqlDbType.VarChar, 8, "KeyValue")
        songsCommand.Parameters.Add("@TimeSignature", SqlDbType.VarChar, 8, "TimeSignature")
        songsCommand.Parameters.Add("@Sequence", SqlDbType.Text, 0, "Sequence")

        If NewSong Then
            songsCommand.CommandText = "INSERT INTO SONGS (Title, Author, Reference, KeyValue, TimeSignature, Sequence) VALUES (@Title, @Author, @Reference, @KeyValue, @TimeSignature, @Sequence); SET @Identity = SCOPE_IDENTITY();"
            myAdapter.InsertCommand = songsCommand

            Dim parameter As SqlClient.SqlParameter = myAdapter.InsertCommand.Parameters.Add("@Identity", SqlDbType.Int, 0, "ID")
            parameter.Direction = ParameterDirection.Output

            Dim dr As DataRow = songDataSet.Tables(0).NewRow
            songDataSet.Tables(0).Rows.Add(dr)
        Else
            songsCommand.Parameters.Add("@SongID", SqlDbType.Int, 0, "ID")
            songsCommand.CommandText = "UPDATE SONGS SET Title = @Title, Author = @Author, Reference = @Reference, KeyValue = @KeyValue, TimeSignature = @TimeSignature, Sequence = @Sequence WHERE ID = @SongID"
            myAdapter.UpdateCommand = songsCommand
        End If

        ' Get the dataset values to insert or update
        songDataSet.Tables(0).Rows(0)("Title") = Me.ESTitleTextBox.Text
        songDataSet.Tables(0).Rows(0)("Author") = Me.ESWriterTextBox.Text
        songDataSet.Tables(0).Rows(0)("Reference") = Me.ESRefTextBox.Text
        songDataSet.Tables(0).Rows(0)("KeyValue") = Me.ESKeyComboBox.Text
        songDataSet.Tables(0).Rows(0)("TimeSignature") = Me.ESTimeSignComboBox.Text
        songDataSet.Tables(0).Rows(0)("Sequence") = ""
        For i As Integer = 0 To Me.ESDefSeqListBox.Items.Count - 1
            If i = 0 Then
                songDataSet.Tables(0).Rows(0)("Sequence") = Me.ESDefSeqListBox.Items(i)
            Else
                songDataSet.Tables(0).Rows(0)("Sequence") = songDataSet.Tables(0).Rows(0)("Sequence") & "," & Me.ESDefSeqListBox.Items(i)
            End If
        Next

        myAdapter.Update(songDataSet)
        'MsgBox(songDataSet.Tables(0).Rows(0)("ID"))
        If Me.NewSong Then
            For Each row As DataRow In lyricsDataSet.Tables(0).Rows
                row("ID") = songDataSet.Tables(0).Rows(0)("ID")
            Next
        End If

        ' Workaround solution to insert not working for dataAdapter Update method
        Dim myCommand As New SqlClient.SqlCommand
        myCommand.Connection = myDbConnection
        myCommand.Connection.Open()

        For Each row As DataRow In lyricsDataSet.Tables(0).Rows

            If row.RowState = DataRowState.Added Then
                myCommand.CommandText = "INSERT INTO LYRICS (ID, Type, Lyrics) VALUES (@SongID, @Type, @Lyrics);"
                myCommand.Parameters.AddWithValue("@SongID", row("ID"))
                myCommand.Parameters.AddWithValue("@Type", row("Type"))
                myCommand.Parameters.AddWithValue("@Lyrics", row("Lyrics"))
                'myCommand.ExecuteNonQuery()
                myCommand.ExecuteScalar()
                row.AcceptChanges()
            End If
        Next
        myCommand.Connection.Close()

        Dim lyricsCommand As New SqlClient.SqlCommand
        lyricsCommand.Connection = myDbConnection
        lyricsCommand.Parameters.Add("@SongID", SqlDbType.Int, 0, "ID")
        lyricsCommand.Parameters.Add("@Type", SqlDbType.VarChar, 15, "Type")
        lyricsCommand.Parameters.Add("@Lyrics", SqlDbType.NText, 0, "Lyrics")

        'lyricsCommand.CommandText = "INSERT INTO LYRICS (ID, Type, Lyrics) VALUES (@SongID, @Type, @Lyrics)"
        'myAdapter.InsertCommand = lyricsCommand
        lyricsCommand.CommandText = "UPDATE LYRICS SET Lyrics = @Lyrics WHERE ID = @SongID AND Type = @Type"
        myAdapter.UpdateCommand = lyricsCommand

        myAdapter.Update(lyricsDataSet)

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ESCancel()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ESLyricsTypeListBox_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles ESLyricsTypeListBox.SelectionChanged
        ' Set Lyrics Text Box if only one item selected in ListBox 
        If Me.ESLyricsTypeListBox.SelectedItems.Count = 1 Then
            Me.ESLyricsTextBox.Text = lyricsDataSet.Tables(0).Select("Type = '" & Me.ESLyricsTypeListBox.SelectedValue & "'")(0)("Lyrics")
            Me.ESLyricsTypeComboBox.SelectedValue = Nothing
        Else
            Me.ESLyricsTextBox.Clear()
        End If
        Me.ESAddButton.IsEnabled = (Me.ESLyricsTypeListBox.SelectedItems.Count = 1)
        Me.ESDeleteButton.IsEnabled = (Me.ESLyricsTypeListBox.SelectedItems.Count = 1)
    End Sub

    Private Sub ESDefSeqListBox_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles ESDefSeqListBox.SelectionChanged
        ' Only one item selected AND not the only item in ListBox
        If Me.ESDefSeqListBox.SelectedItems.Count = 1 And Me.ESDefSeqListBox.Items.Count <> 1 Then
            Me.ESUpButton.IsEnabled = (Me.ESDefSeqListBox.SelectedIndex <> 0)
            Me.ESDownButton.IsEnabled = (Me.ESDefSeqListBox.SelectedIndex <> Me.ESDefSeqListBox.Items.Count - 1)
            Me.ESRemoveButton.IsEnabled = True
        Else ' No item selected OR the only item in ListBox
            Me.ESUpButton.IsEnabled = False
            Me.ESDownButton.IsEnabled = False
            Me.ESRemoveButton.IsEnabled = Me.ESDefSeqListBox.SelectedItems.Count
        End If
    End Sub

    Private Sub ESLyricsTypeComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ESLyricsTypeComboBox.SelectionChanged
        If Not Me.ESLyricsTypeComboBox.SelectedItem = vbNullString Then
            Me.ESLyricsTypeListBox.UnselectAll()
        End If
    End Sub

    Private Sub ESLyricsTextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles ESLyricsTextBox.TextChanged
        ' If no lyrics type selected in listbox, text will always be considered as changed
        Dim textUnchanged As Boolean = False
        If Me.ESLyricsTypeListBox.SelectedItems.Count = 1 Then
            textUnchanged = Me.ESLyricsTextBox.Text = lyricsDataSet.Tables(0).Select("Type = '" & Me.ESLyricsTypeListBox.SelectedValue & "'")(0)("Lyrics")
        End If
        If ESLyricsTextBox.Text = vbNullString Or textUnchanged Then
            Me.ESSaveChangesButton.IsEnabled = False
        Else
            Me.ESSaveChangesButton.IsEnabled = True
        End If
    End Sub
End Class