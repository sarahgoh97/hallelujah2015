Public Class BackgroundManager

    Public ImageDictionary As Concurrent.ConcurrentDictionary(Of String, BitmapImage)

    Public Sub Initialize_Photos()
        ImageDictionary = New Concurrent.ConcurrentDictionary(Of String, BitmapImage)

        ImageDictionary.Clear()

        'try get resources
        GetAllBitmaps()

    End Sub

    Public Sub GetAllBitmaps()
        Try
            ' ImageDictionary.Clear()
            With ImageDictionary

                Dim ImageDir() As System.IO.FileInfo
                Dim di As New IO.DirectoryInfo("Background/")

                ImageDir = di.GetFiles("*.jpg").Concat(di.GetFiles("*.bmp")).Concat(di.GetFiles("*.png")).Concat(di.GetFiles("*.gif")).ToArray


                ' BM is a new bitmap
                Dim BM As BitmapImage
                For Each FileInfo In ImageDir.OrderBy(Function(i) i.Name)
                    ' Create a new bitmap and set it to be the one from the file.
                    ' this will allow you to save with the same filename and overwrite what was there.
                    BM = New BitmapImage
                    BM.BeginInit()
                    BM.CacheOption = BitmapCacheOption.OnLoad
                    BM.UriSource = New Uri(FileInfo.FullName)
                    BM.EndInit()

                    If .ContainsKey(FileInfo.Name) = False Then
                        .TryAdd(FileInfo.Name, BM)
                        '  .TryAdd(FileInfo.Name, New BitmapImage(New Uri(FileInfo.FullName, UriKind.Absolute)))
                    End If
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BMCancel()
        Dim scroller As ScrollViewer = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(BMList, 0), 0)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class
