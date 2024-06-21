Imports System.Drawing.Drawing2D

Public Class RoundedButton
    Inherits Button

    Public Property BorderRadius As Integer = 20

    Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
        MyBase.OnPaint(pevent)

        Dim graphics As Graphics = pevent.Graphics
        graphics.SmoothingMode = SmoothingMode.AntiAlias

        Dim rect As Rectangle = New Rectangle(0, 0, Me.Width, Me.Height)
        Dim path As GraphicsPath = GetRoundedRectPath(rect, BorderRadius)

        Me.Region = New Region(path)

        ' Draw button background
        Using brush As New SolidBrush(Me.BackColor)
            graphics.FillPath(brush, path)
        End Using

        ' Draw button text
        Dim sf As New StringFormat()
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        Using brush As New SolidBrush(Me.ForeColor)
            graphics.DrawString(Me.Text, Me.Font, brush, rect, sf)
        End Using
    End Sub

    Private Function GetRoundedRectPath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        path.StartFigure()
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
        path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90)
        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
        path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90)
        path.CloseFigure()
        Return path
    End Function
End Class
