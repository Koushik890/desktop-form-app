Public Class MainForm
    Private WithEvents btnViewSubmissions As RoundedButton
    Private WithEvents btnCreateSubmission As RoundedButton

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set form title and size
        Me.Text = "Slidely Task 2 - Slidely Form App"
        Me.Size = New Size(500, 300)

        ' Add label for the form title
        Dim lblTitle As New Label()
        lblTitle.Text = "Slidely Task 2 - Slidely Form App"
        lblTitle.Font = New Font("Arial", 12, FontStyle.Bold)
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(85, 30)
        Me.Controls.Add(lblTitle)

        ' View Submissions button
        btnViewSubmissions = New RoundedButton()
        btnViewSubmissions.Text = "VIEW SUBMISSIONS (CTRL + V)"
        btnViewSubmissions.Size = New Size(400, 50)
        btnViewSubmissions.Location = New Point(40, 100)
        btnViewSubmissions.BackColor = Color.FromArgb(255, 255, 153)
        Me.Controls.Add(btnViewSubmissions)

        ' Create New Submission button
        btnCreateSubmission = New RoundedButton()
        btnCreateSubmission.Text = "CREATE NEW SUBMISSION (CTRL + N)"
        btnCreateSubmission.Size = New Size(400, 50)
        btnCreateSubmission.Location = New Point(40, 170)
        btnCreateSubmission.BackColor = Color.FromArgb(153, 204, 255)
        Me.Controls.Add(btnCreateSubmission)

        ' Enable keyboard shortcuts
        Me.KeyPreview = True
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        Dim viewForm As New ViewForm()
        viewForm.Show()
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateSubmission.Click
        Dim createForm As New CreateForm()
        createForm.Show()
    End Sub

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            btnViewSubmissions.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnCreateSubmission.PerformClick()
        End If
    End Sub
End Class
