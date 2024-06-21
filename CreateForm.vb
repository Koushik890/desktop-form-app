Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text

Public Class CreateForm
    Private WithEvents btnToggleStopwatch As RoundedButton
    Private WithEvents btnSubmit As RoundedButton
    Private txtName As TextBox
    Private txtEmail As TextBox
    Private txtPhone As TextBox
    Private txtGithub As TextBox
    Private lblStopwatch As Label
    Private stopwatch As New Stopwatch()
    Private ReadOnly httpClient As New HttpClient()
    Private WithEvents timer As New Timer()

    Private Sub CreateForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set form title and size
        Me.Text = "John Doe, Slidely Task 2 - Create Submission"
        Me.Size = New Size(500, 450)

        ' Label for the form title
        Dim lblTitle As New Label()
        lblTitle.Text = "John Doe, Slidely Task 2 - Create Submission"
        lblTitle.Font = New Font("Arial", 12, FontStyle.Bold)
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(40, 20)
        Me.Controls.Add(lblTitle)

        ' Name label and textbox
        AddLabel("Name", New Point(40, 60))
        txtName = AddTextBox(New Point(200, 60), False)

        ' Email label and textbox
        AddLabel("Email", New Point(40, 100))
        txtEmail = AddTextBox(New Point(200, 100), False)

        ' Phone Num label and textbox
        AddLabel("Phone Num", New Point(40, 140))
        txtPhone = AddTextBox(New Point(200, 140), False)

        ' GitHub Link label and textbox
        AddLabel("Github Link For Task 2", New Point(40, 180))
        txtGithub = AddTextBox(New Point(200, 180), False)

        ' Stopwatch button and label
        btnToggleStopwatch = New RoundedButton()
        btnToggleStopwatch.Text = "TOGGLE STOPWATCH (CTRL + T)"
        btnToggleStopwatch.Size = New Size(300, 30)
        btnToggleStopwatch.Location = New Point(40, 220)
        btnToggleStopwatch.BackColor = Color.FromArgb(255, 255, 153)
        Me.Controls.Add(btnToggleStopwatch)

        lblStopwatch = New Label()
        lblStopwatch.Size = New Size(100, 30)
        lblStopwatch.Location = New Point(350, 220)
        lblStopwatch.BorderStyle = BorderStyle.FixedSingle
        lblStopwatch.TextAlign = ContentAlignment.MiddleCenter
        lblStopwatch.BackColor = Color.LightGray
        lblStopwatch.Text = "00:00:00"
        Me.Controls.Add(lblStopwatch)

        ' Submit button
        btnSubmit = New RoundedButton()
        btnSubmit.Text = "SUBMIT (CTRL + S)"
        btnSubmit.Size = New Size(400, 50)
        btnSubmit.Location = New Point(45, 270)
        btnSubmit.BackColor = Color.FromArgb(153, 204, 255)
        Me.Controls.Add(btnSubmit)

        ' Timer setup
        timer.Interval = 1000 ' 1 second interval
        AddHandler timer.Tick, AddressOf Timer_Tick

        ' Enable keyboard shortcuts
        Me.KeyPreview = True
    End Sub

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            timer.Stop()
        Else
            stopwatch.Start()
            timer.Start()
        End If
        UpdateStopwatchLabel()
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Submit the form data
        Dim submission As New With {
            Key .name = txtName.Text,
            Key .email = txtEmail.Text,
            Key .phone = txtPhone.Text,
            Key .github_link = txtGithub.Text,
            Key .stopwatch_time = lblStopwatch.Text
        }

        Dim content As New StringContent(JsonConvert.SerializeObject(submission), Encoding.UTF8, "application/json")
        Dim response = Await httpClient.PostAsync("http://localhost:3000/submit", content)
        If response.IsSuccessStatusCode Then
            MessageBox.Show("Submission saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Failed to save submission.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        UpdateStopwatchLabel()
    End Sub

    Private Sub UpdateStopwatchLabel()
        lblStopwatch.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Sub CreateForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.T Then
            btnToggleStopwatch.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
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
        textBox.Size = New Size(250, 20)
        textBox.Location = location
        textBox.ReadOnly = isReadOnly
        textBox.BorderStyle = BorderStyle.FixedSingle
        Me.Controls.Add(textBox)
        Return textBox
    End Function
End Class
