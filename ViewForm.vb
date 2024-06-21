Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text

Public Class ViewForm
    Private currentIndex As Integer = 0

    Private WithEvents btnPrevious As RoundedButton
    Private WithEvents btnNext As RoundedButton
    Private WithEvents btnDelete As RoundedButton
    Private WithEvents btnEdit As RoundedButton
    Private txtName As TextBox
    Private txtEmail As TextBox
    Private txtPhone As TextBox
    Private txtGithub As TextBox
    Private txtStopwatch As TextBox
    Private ReadOnly httpClient As New HttpClient()
    Private isEditMode As Boolean = False

    Private Sub ViewForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set form title and size
        Me.Text = "John Doe, Slidely Task 2 - View Submissions"
        Me.Size = New Size(500, 500)

        ' Label for the form title
        Dim lblTitle As New Label()
        lblTitle.Text = "John Doe, Slidely Task 2 - View Submissions"
        lblTitle.Font = New Font("Arial", 12, FontStyle.Bold)
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(40, 20)
        Me.Controls.Add(lblTitle)

        ' Name label and textbox
        AddLabel("Name", New Point(40, 60))
        txtName = AddTextBox(New Point(200, 60), True)

        ' Email label and textbox
        AddLabel("Email", New Point(40, 100))
        txtEmail = AddTextBox(New Point(200, 100), True)

        ' Phone Num label and textbox
        AddLabel("Phone Num", New Point(40, 140))
        txtPhone = AddTextBox(New Point(200, 140), True)

        ' GitHub Link label and textbox
        AddLabel("Github Link For Task 2", New Point(40, 180))
        txtGithub = AddTextBox(New Point(200, 180), True)

        ' Stopwatch time label and textbox
        AddLabel("Stopwatch time", New Point(40, 220))
        txtStopwatch = AddTextBox(New Point(200, 220), True)

        ' Previous button
        btnPrevious = New RoundedButton()
        btnPrevious.Text = "PREVIOUS (CTRL + P)"
        btnPrevious.Size = New Size(200, 50)
        btnPrevious.Location = New Point(21, 280)
        btnPrevious.BackColor = Color.FromArgb(255, 255, 153)
        Me.Controls.Add(btnPrevious)

        ' Next button
        btnNext = New RoundedButton()
        btnNext.Text = "NEXT (CTRL + N)"
        btnNext.Size = New Size(200, 50)
        btnNext.Location = New Point(250, 280)
        btnNext.BackColor = Color.FromArgb(153, 204, 255)
        Me.Controls.Add(btnNext)

        ' Delete button
        btnDelete = New RoundedButton()
        btnDelete.Text = "DELETE (CTRL + D)"
        btnDelete.Size = New Size(200, 50)
        btnDelete.Location = New Point(21, 350)
        btnDelete.BackColor = Color.FromArgb(255, 102, 102)
        Me.Controls.Add(btnDelete)

        ' Edit button
        btnEdit = New RoundedButton()
        btnEdit.Text = "EDIT (CTRL + E)"
        btnEdit.Size = New Size(200, 50)
        btnEdit.Location = New Point(250, 350)
        btnEdit.BackColor = Color.FromArgb(102, 204, 255)
        Me.Controls.Add(btnEdit)

        ' Enable keyboard shortcuts
        Me.KeyPreview = True

        ' Load the first submission
        LoadSubmission(currentIndex)
    End Sub

    Private Async Function LoadSubmission(index As Integer) As Task
        Try
            Dim response = Await httpClient.GetStringAsync($"http://localhost:3000/read?index={index}")
            Dim submission = JsonConvert.DeserializeObject(Of Submission)(response)
            If submission IsNot Nothing Then
                txtName.Text = submission.name
                txtEmail.Text = submission.email
                txtPhone.Text = submission.phone
                txtGithub.Text = submission.github_link
                txtStopwatch.Text = submission.stopwatch_time
                ToggleFields(False)
            Else
                MessageBox.Show("No more submissions to display.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                currentIndex -= 1 ' Revert the index increment
            End If
        Catch ex As HttpRequestException
            MessageBox.Show("No more submissions to display.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            currentIndex -= 1 ' Revert the index increment
        End Try
    End Function

    Private Async Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            Await LoadSubmission(currentIndex)
        Else
            MessageBox.Show("This is the first submission.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Async Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        currentIndex += 1
        Await LoadSubmission(currentIndex)
    End Sub

    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim response = Await httpClient.DeleteAsync($"http://localhost:3000/delete?index={currentIndex}")
        If response.IsSuccessStatusCode Then
            MessageBox.Show("Submission deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If currentIndex > 0 Then
                currentIndex -= 1
            End If
            Await LoadSubmission(currentIndex)
        Else
            MessageBox.Show("Failed to delete submission.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Async Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If Not isEditMode Then
            ' Enable edit mode
            isEditMode = True
            btnEdit.Text = "SAVE (CTRL + S)"
            ToggleFields(True)
        Else
            Await SaveSubmission()
        End If
    End Sub

    Private Async Function SaveSubmission() As Task
        Dim updatedSubmission As New Submission With {
            .name = txtName.Text,
            .email = txtEmail.Text,
            .phone = txtPhone.Text,
            .github_link = txtGithub.Text,
            .stopwatch_time = txtStopwatch.Text
        }
        Dim content As New StringContent(JsonConvert.SerializeObject(updatedSubmission), Encoding.UTF8, "application/json")
        Dim response = Await httpClient.PutAsync($"http://localhost:3000/edit?index={currentIndex}", content)
        If response.IsSuccessStatusCode Then
            MessageBox.Show("Submission updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Await LoadSubmission(currentIndex)
        Else
            MessageBox.Show("Failed to update submission.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        ' Disable edit mode
        isEditMode = False
        btnEdit.Text = "EDIT (CTRL + E)"
        ToggleFields(False)
    End Function

    Private Async Sub ViewForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
            btnDelete.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.E Then
            btnEdit.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.S AndAlso isEditMode Then
            Await SaveSubmission()
        End If
    End Sub

    Private Sub AddLabel(text As String, location As Point)
        Dim label As New Label()
        label.Text = text
        label.Location = location
        label.AutoSize = True
        Me.Controls.Add(label)
    End Sub

    Private Function AddTextBox(location As Point, isReadOnly As Boolean) As TextBox
        Dim textBox As New TextBox()
        textBox.Size = New Size(250, 30)
        textBox.Location = location
        textBox.ReadOnly = isReadOnly
        textBox.BorderStyle = BorderStyle.FixedSingle
        textBox.BackColor = If(isReadOnly, Color.LightGray, Color.White)
        textBox.TextAlign = HorizontalAlignment.Center
        AddHandler textBox.GotFocus, AddressOf TextBox_GotFocus ' Prevent cursor from being active in read-only mode
        Me.Controls.Add(textBox)
        Return textBox
    End Function

    Private Sub TextBox_GotFocus(sender As Object, e As EventArgs)
        Dim textBox = DirectCast(sender, TextBox)
        If textBox.ReadOnly Then
            Me.SelectNextControl(textBox, True, True, True, True)
        End If
    End Sub

    Private Sub ToggleFields(isEditable As Boolean)
        txtName.ReadOnly = Not isEditable
        txtEmail.ReadOnly = Not isEditable
        txtPhone.ReadOnly = Not isEditable
        txtGithub.ReadOnly = Not isEditable
        txtStopwatch.ReadOnly = Not isEditable

        If isEditable Then
            txtName.BackColor = Color.White
            txtEmail.BackColor = Color.White
            txtPhone.BackColor = Color.White
            txtGithub.BackColor = Color.White
            txtStopwatch.BackColor = Color.White
        Else
            txtName.BackColor = Color.LightGray
            txtEmail.BackColor = Color.LightGray
            txtPhone.BackColor = Color.LightGray
            txtGithub.BackColor = Color.LightGray
            txtStopwatch.BackColor = Color.LightGray
        End If
    End Sub
End Class

Public Class Submission
    Public Property name As String
    Public Property email As String
    Public Property phone As String
    Public Property github_link As String
    Public Property stopwatch_time As String
End Class
