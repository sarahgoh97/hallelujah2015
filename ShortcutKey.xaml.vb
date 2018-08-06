Partial Public Class ShortcutKey
    Public myKey As String
    Private Sub ShortcutKey_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles Me.KeyDown
        Me.myKey = e.Key.ToString
        Me.Close()
    End Sub

    Private Sub ShortcutKey_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Me.Focus()
    End Sub
End Class
