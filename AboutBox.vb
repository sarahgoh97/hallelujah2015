Public NotInheritable Class AboutBox

    Private Sub AboutBox_KeyDown(sender As Object, e As Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub AboutBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = My.Application.Info.Description

        ' To allow consistent look on the About Box
        Me.TextBoxDescription.Cursor = Forms.Cursors.Arrow
    End Sub

    Private Sub TableLayoutPanel_Click(sender As Object, e As EventArgs) Handles TableLayoutPanel.Click
        Me.Close()
    End Sub

    Private Sub LabelCompanyName_Click(sender As Object, e As EventArgs) Handles LabelCompanyName.Click
        Me.Close()
    End Sub

    Private Sub LabelCopyright_Click(sender As Object, e As EventArgs) Handles LabelCopyright.Click
        Me.Close()
    End Sub

    Private Sub LabelProductName_Click(sender As Object, e As EventArgs) Handles LabelProductName.Click
        Me.Close()
    End Sub

    Private Sub LabelVersion_Click(sender As Object, e As EventArgs) Handles LabelVersion.Click
        Me.Close()
    End Sub

    Private Sub LogoPictureBox_Click(sender As Object, e As EventArgs) Handles LogoPictureBox.Click
        Me.Close()
    End Sub

    Private Sub TextBoxDescription_Click(sender As Object, e As EventArgs) Handles TextBoxDescription.Click
        Me.Close()
    End Sub

End Class
