Imports System.Data

Class MainWindow

    Dim myDbConnection As New SqlClient.SqlConnection()

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        'AppDomain.CurrentDomain.SetData("DataDirectory", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))
        myDbConnection.ConnectionString = My.Settings.DBConnection
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Me.MW_Refresh()
    End Sub

    Private Sub MWExitButton_Click(sender As Object, e As RoutedEventArgs) Handles MWExitButton.Click
        End
    End Sub

    Private Sub MWHelp()
        Dim HF As New HelpFile()
        HF.ShowDialog()
    End Sub

    Private Sub MWInfo()
        Dim AB As New AboutBox()
        AB.ShowDialog()
    End Sub

    Private Sub MWKeys()
        Dim PK As New PresentationKeys()
        PK.ShowDialog()
    End Sub

    Private Sub MWPresentation()
        Dim PS As New PresentationSettings()
        PS.ShowDialog()
    End Sub

    Private Sub MWDB()
        Dim DB As New DBManager()
        'Dim DB As New BackgroundManager()
        DB.ShowDialog()
    End Sub

    Private Sub MWDBCanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
        e.CanExecute = False
        Me.MWDatabaseButton.IsEnabled = False
    End Sub

    Private Sub MWNewSong()
        Dim ES As New EditSong(True)
        ES.ShowDialog()
        Me.MWSongsListBox_Refresh()
    End Sub

    Private Sub MWEditSong()
        Dim selectedIndex As Integer = Me.MWSongsListBox.SelectedIndex
        Dim ES As New EditSong(False, Me.MWSongsListBox.SelectedItem(0), Me.MWSongsListBox.SelectedItem(1))
        ES.ShowDialog()
        Me.MWSongsListBox_Refresh()
        Me.MWSongsListBox.SelectedIndex = selectedIndex
    End Sub

    Private Sub MWEditSongCanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
        e.CanExecute = Me.MWSongsListBox.SelectedItems.Count
        Me.MWEditButton.IsEnabled = Me.MWSongsListBox.SelectedItems.Count
    End Sub

    Private Sub MWDeleteSong()
        Dim myCommand As New SqlClient.SqlCommand

        ' Prompt to confirm song delete
        If MsgBox("Are you sure you want to delete '" & Me.MWSongsListBox.SelectedItem(1) & "'", MsgBoxStyle.YesNo, "Confirm Song Delete") = MsgBoxResult.Yes Then
            myCommand.Connection = myDbConnection
            myCommand.Connection.Open()

            ' Delete from SONGS table
            myCommand.CommandText = "DELETE FROM SONGS WHERE ID = '" & Me.MWSongsListBox.SelectedItem(0) & "'"
            myCommand.ExecuteNonQuery()
            ' Delete from LYRICS table
            myCommand.CommandText = "DELETE FROM LYRICS WHERE ID = '" & Me.MWSongsListBox.SelectedItem(0) & "'"
            myCommand.ExecuteNonQuery()

            myCommand.Connection.Close()

            Me.MW_Refresh()
        End If
    End Sub

    Private Sub MWDeleteSongCanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
        e.CanExecute = Me.MWSongsListBox.SelectedItems.Count
        Me.MWDeleteButton.IsEnabled = Me.MWSongsListBox.SelectedItems.Count
    End Sub

    Private Sub MWShiftUpPlaylist()
        Dim myIndex As Integer

        myIndex = Me.MWPlaylistListBox.SelectedIndex

        ' Only if selected item is not the first one in the listview
        If myIndex <> 0 Then
            Me.MWPlaylistListBox.Items.Insert(myIndex - 1, Me.MWPlaylistListBox.Items(myIndex))

            ' Select item so that event SelectionChanged is activated
            Me.MWPlaylistListBox.SelectedIndex = myIndex - 1

            Me.MWPlaylistListBox.Items.RemoveAt(myIndex + 1)
        End If
    End Sub

    Private Sub MWShiftUpPlaylistCanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
        e.CanExecute = (Me.MWPlaylistListBox.SelectedItems.Count = 1 And Me.MWPlaylistListBox.Items.Count <> 1 And Me.MWPlaylistListBox.SelectedIndex <> 0)
        Me.MWUpButton.IsEnabled = (Me.MWPlaylistListBox.SelectedItems.Count = 1 And Me.MWPlaylistListBox.Items.Count <> 1 And Me.MWPlaylistListBox.SelectedIndex <> 0)
    End Sub

    Private Sub MWShiftDownPlaylist()
        Dim myIndex As Integer

        myIndex = Me.MWPlaylistListBox.SelectedIndex

        ' Only if selected item is not the last one in the listview
        If myIndex <> Me.MWPlaylistListBox.Items.Count - 1 Then
            Me.MWPlaylistListBox.Items.Insert(myIndex + 2, Me.MWPlaylistListBox.Items(myIndex))

            ' Select item so that event SelectionChanged is activated
            Me.MWPlaylistListBox.SelectedIndex = myIndex + 2

            Me.MWPlaylistListBox.Items.RemoveAt(myIndex)
        End If
    End Sub

    Private Sub MWShiftDownPlaylistCanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
        e.CanExecute = (Me.MWPlaylistListBox.SelectedItems.Count = 1 And Me.MWPlaylistListBox.Items.Count <> 1 And Me.MWPlaylistListBox.SelectedIndex <> Me.MWPlaylistListBox.Items.Count - 1)
        Me.MWDownButton.IsEnabled = (Me.MWPlaylistListBox.SelectedItems.Count = 1 And Me.MWPlaylistListBox.Items.Count <> 1 And Me.MWPlaylistListBox.SelectedIndex <> Me.MWPlaylistListBox.Items.Count - 1)
    End Sub

    Private Sub MWAddToPlaylist()
        Dim myDataTable As New DataTable("Table")
        Dim myRow As DataRow

        myDataTable.Columns.Add("ID", Type.GetType("System.Int32"))
        myDataTable.Columns.Add("Title", Type.GetType("System.String"))
        myRow = myDataTable.NewRow()
        myRow("ID") = Me.MWSongsListBox.SelectedItem(0)
        myRow("Title") = Me.MWSongsListBox.SelectedItem(1)
        myDataTable.Rows.Add(myRow)

        Me.MWPlaylistListBox.Items.Add(myDataTable)
    End Sub

    Private Sub MWAddToPlaylistCanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
        e.CanExecute = Me.MWSongsListBox.SelectedItems.Count
        Me.MWAddButton.IsEnabled = Me.MWSongsListBox.SelectedItems.Count
    End Sub

    Private Sub MWRemoveFromPlaylist()
        Me.MWPlaylistListBox.Items.RemoveAt(Me.MWPlaylistListBox.SelectedIndex)
    End Sub

    Private Sub MWRemoveFromPlaylistCanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
        e.CanExecute = (Me.MWPlaylistListBox.SelectedItems.Count = 1)
        Me.MWRemoveButton.IsEnabled = (Me.MWPlaylistListBox.SelectedItems.Count = 1)
    End Sub

    Private Sub MWBeginWorship()
        Dim PP As PresentationPage
        PP = New PresentationPage(Me.MWPlaylistListBox, 0, Me.MWPlaylistListBox.Items.Count)
        PP.ShowDialog()
    End Sub

    Private Sub MWBeginWorshipCanExecute(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
        e.CanExecute = Me.MWPlaylistListBox.Items.Count
        Me.MWBeginButton.IsEnabled = Me.MWPlaylistListBox.Items.Count
    End Sub

    Private Sub MWKeyComboBox_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles MWKeyComboBox.SelectionChanged
        If Not Me.MWKeyComboBox.SelectedValue = Nothing Then
            Me.MWSongsListBox_Refresh()
        End If
    End Sub

    Private Sub MWTimeSignComboBox_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles MWTimeSignComboBox.SelectionChanged
        If Not Me.MWTimeSignComboBox.SelectedValue = Nothing Then
            Me.MWSongsListBox_Refresh()
        End If
    End Sub

    Private Sub MWTitleTextBox_TextChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.TextChangedEventArgs) Handles MWTitleTextBox.TextChanged
        Me.MWSongsListBox_Refresh()
    End Sub

    Private Sub MWWriterTextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles MWWriterTextBox.TextChanged
        Me.MWSongsListBox_Refresh()
    End Sub

    Private Sub MWRefTextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles MWRefTextBox.TextChanged
        Me.MWSongsListBox_Refresh()
    End Sub

    Private Sub MWSongsListBox_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles MWSongsListBox.SelectionChanged
        Dim myCommand As New SqlClient.SqlCommand
        Dim myReader As SqlClient.SqlDataReader
        Dim mySeq As String
        Dim myPos1 As Integer = 0
        Dim myPos2 As Integer = 0
        Dim test As String

        ' Only one item selected
        If Me.MWSongsListBox.SelectedItems.Count = 1 Then
            Me.MWPreviewTextBox.Clear()

            myCommand.Connection = myDbConnection
            myCommand.Connection.Open()

            ' Determine lyrics to display from first in sequence
            myCommand.CommandText = "SELECT * FROM SONGS WHERE ID = '" & Me.MWSongsListBox.SelectedValue & "'"

            myReader = myCommand.ExecuteReader()
            myReader.Read()
            Me.MWPreviewTextBox.AppendText("Title: " & myReader.GetString(1))
            Me.MWPreviewTextBox.AppendText(Environment.NewLine & "Author: " & myReader.GetString(2))
            Me.MWPreviewTextBox.AppendText(Environment.NewLine & "Reference: " & myReader.GetString(3))
            Me.MWPreviewTextBox.AppendText(Environment.NewLine & "Key: " & myReader.GetString(4))
            Me.MWPreviewTextBox.AppendText(Environment.NewLine & "Time Signature: " & myReader.GetString(5))
            mySeq = myReader.GetString(6)
            myReader.Close()

            ' Next in sequence separated by ','
            Do
                myPos2 = InStr(myPos1 + 1, mySeq, ",", CompareMethod.Text)
                If myPos2 = 0 Then
                    test = Mid(mySeq, myPos1 + 1)
                Else
                    test = Mid(mySeq, myPos1 + 1, myPos2 - myPos1 - 1)
                End If
                myPos1 = myPos2

                ' Retrieve lyrics to display
                myCommand.CommandText = "SELECT Lyrics FROM LYRICS WHERE ID = '" & Me.MWSongsListBox.SelectedValue & "' AND Type = '" & test & "'"
                myReader = myCommand.ExecuteReader()

                ' Add lyrics to preview
                While myReader.Read()
                    Me.MWPreviewTextBox.Text = Me.MWPreviewTextBox.Text & Environment.NewLine & Environment.NewLine & myReader.GetString(0)
                End While
                myReader.Close()
            Loop While myPos2 <> 0

            myCommand.Connection.Close()
        End If
    End Sub

    Private Sub MWSongsListBox_Refresh()
        Dim myCommand As New SqlClient.SqlCommand
        Dim myAdapter As New SqlClient.SqlDataAdapter
        Dim myDataSet As DataSet

        myCommand.Connection = myDbConnection
        myCommand.Connection.Open()

        Dim whereStr As String
        whereStr = ""

        If Me.MWKeyComboBox.SelectedValue <> "All" And Me.MWKeyComboBox.SelectedValue <> "" Then
            whereStr = "KeyValue ='" & Me.MWKeyComboBox.SelectedValue.ToString.Replace("'", "''") & "'"
        End If

        If Me.MWTimeSignComboBox.SelectedValue <> "All" And Me.MWTimeSignComboBox.SelectedValue <> "" Then
            If whereStr <> "" Then
                whereStr = whereStr & " AND"
            End If
            whereStr = whereStr & " TimeSignature ='" & Me.MWTimeSignComboBox.SelectedValue.ToString.Replace("'", "''") & "'"
        End If

        If Me.MWTitleTextBox.Text <> "" Then
            If whereStr <> "" Then
                whereStr = whereStr & " AND"
            End If
            whereStr = whereStr & " Title LIKE N'" & Me.MWTitleTextBox.Text.Replace("'", "''") & "%'"
        End If

        If Me.MWWriterTextBox.Text <> "" Then
            If whereStr <> "" Then
                whereStr = whereStr & " AND"
            End If
            whereStr = whereStr & " Author LIKE N'" & Me.MWWriterTextBox.Text.Replace("'", "''") & "%'"
        End If

        If Me.MWRefTextBox.Text <> "" Then
            If whereStr <> "" Then
                whereStr = whereStr & " AND"
            End If
            whereStr = whereStr & " Reference LIKE N'" & Me.MWRefTextBox.Text.Replace("'", "''") & "%'"
        End If

        If whereStr <> "" Then
            whereStr = " WHERE " & whereStr
        End If

        myCommand.CommandText = "SELECT ID, Title FROM SONGS" & whereStr
        myAdapter.SelectCommand = myCommand
        myDataSet = New DataSet()
        myAdapter.Fill(myDataSet, "Table")
        Me.MWSongsListBox.DataContext = myDataSet.Tables(0).DefaultView
        Me.MWSongsListBox.Items.SortDescriptions.Add(New ComponentModel.SortDescription("Title", ComponentModel.ListSortDirection.Ascending))
        Me.MWPreviewTextBox.Clear()

        myCommand.Connection.Close()
    End Sub

    Private Sub MW_Refresh()
        Me.MWKeyComboBox_Refresh()
        Me.MWTimeSignComboBox_Refresh()
        Me.MWSongsListBox_Refresh()
    End Sub

    Private Sub MWKeyComboBox_Refresh()
        Dim myCommand As New SqlClient.SqlCommand
        Dim myReader As SqlClient.SqlDataReader

        myCommand.Connection = myDbConnection
        myCommand.Connection.Open()
        ' All keys found in database
        myCommand.CommandText = "SELECT DISTINCT KeyValue FROM SONGS"
        myReader = myCommand.ExecuteReader()

        Me.MWKeyComboBox.Items.Clear()
        While myReader.Read()
            Me.MWKeyComboBox.Items.Add(myReader.GetString(0))
        End While
        Me.MWKeyComboBox.Items.Add("All")

        myReader.Close()
        myCommand.Connection.Close()

        Me.MWKeyComboBox.Text = "All"
    End Sub

    Private Sub MWTimeSignComboBox_Refresh()
        Dim myCommand As New SqlClient.SqlCommand
        Dim myReader As SqlClient.SqlDataReader

        myCommand.Connection = myDbConnection
        myCommand.Connection.Open()
        ' All time signatures found in database
        myCommand.CommandText = "SELECT DISTINCT TimeSignature FROM SONGS"
        myReader = myCommand.ExecuteReader()

        Me.MWTimeSignComboBox.Items.Clear()
        While myReader.Read()
            Me.MWTimeSignComboBox.Items.Add(myReader.GetString(0))
        End While
        Me.MWTimeSignComboBox.Items.Add("All")

        myReader.Close()
        myCommand.Connection.Close()

        Me.MWTimeSignComboBox.Text = "All"
    End Sub
End Class
