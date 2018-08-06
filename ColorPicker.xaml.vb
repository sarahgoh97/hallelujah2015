Public Class ColorPicker
    Private Sub CPOK()
        ' Save the settings
        Me.DialogResult = True
        Me.Close()
    End Sub

    Private Sub CPCancel()
        Me.DialogResult = False
        Me.Close()
    End Sub
End Class
