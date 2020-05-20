Imports System.Data

Public Class DBManager
    Private Sub DBBackup()
        Dim sfd As Microsoft.Win32.SaveFileDialog = New Microsoft.Win32.SaveFileDialog
        Dim filename As String
        Dim sourceDirectory As String = AppDomain.CurrentDomain.GetData("DataDirectory")
        MessageBox.Show("Database was successfully backed up to: " + sourceDirectory, "Info")
        sfd.Filter = "SQL Server Database File (*.mdf)|*.mdf"
        sfd.FileName = "Hallelujah Backup " + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".mdf"
        'sfd.InitialDirectory = "pack://application:,,,/Background"
        If sfd.ShowDialog = True Then
            filename = sfd.FileName
            'System.IO.File.Copy(System.IO.Path.Combine(sourceDirectory, "Hallelujah.mdf"), filename, True)
            Dim myDbConnection As New SqlClient.SqlConnection(My.Settings.DBConnection)
            Dim mySqlCmd As New SqlClient.SqlCommand()
            mySqlCmd.Connection = myDbConnection
            myDbConnection.Open()

            MessageBox.Show("Database was successfully backed up to: " + filename, "Info")
        End If
    End Sub

    Private Sub DBRestore()
        Dim ofd As Microsoft.Win32.OpenFileDialog = New Microsoft.Win32.OpenFileDialog
        Dim filename As String
        Dim destinationDirectory As String = AppDomain.CurrentDomain.GetData("DataDirectory")
        ofd.Filter = "SQL Server Database File (*.mdf)|*.mdf"

        If ofd.ShowDialog = True Then
            filename = ofd.FileName
            System.IO.File.Copy(filename, System.IO.Path.Combine(destinationDirectory, "Hallelujah.mdf"), True)
            MessageBox.Show("Database was successfully restored from: " + filename, "Info")
        End If
    End Sub

    Private Sub DBExit()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class
