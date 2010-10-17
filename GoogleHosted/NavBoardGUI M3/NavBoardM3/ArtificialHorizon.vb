'/**************************************************************************/
'/*! 
'    @file     ArtificialHorizon.vb
'    @author   Tom Pycke (http://tom.pycke.be/?pg=2) with minor mods by M. Ryan (ryanmechatronics.com)
'    @date     8 Aug 2010
'    @version  0.10

'    @section DESCRIPTION

'    Open Source artificial horizon code for Navigation Board M3 GUI

'    @section Example

'    @code 

'    @endcode

'    @section LICENSE

'    Software License Agreement 

'    Copyright (c) 2010, Ryan Mechatronics LLC
'    All rights reserved.

'    Redistribution and use in source and binary forms, with or without
'    modification, are permitted provided that the following conditions are met:
'    1. Redistributions of source code must retain the above copyright
'    notice, this list of conditions and the following disclaimer.
'    2. Redistributions in binary form must reproduce the above copyright
'    notice, this list of conditions and the following disclaimer in the
'    documentation and/or other materials provided with the distribution.
'    3. Neither the name of the copyright holders nor the
'    names of its contributors may be used to endorse or promote products
'    derived from this software without specific prior written permission.

'    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS ''AS IS'' AND ANY
'    EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
'    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
'    DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER BE LIABLE FOR ANY
'    DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
'    (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
'    LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
'    ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
'    (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
'    SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'*/
'/**************************************************************************/
Public Class ArtificialHorizon

    Private g As Graphics

    Private _roll_angle As Double
    Public Property roll_angle() As Double
        Get
            Return _roll_angle
        End Get
        Set(ByVal value As Double)
            _roll_angle = value
            'Invalidate()
        End Set
    End Property
    Private _pitch_angle As Double
    Public Property pitch_angle() As Double
        Get
            Return _pitch_angle
        End Get
        Set(ByVal value As Double)
            _pitch_angle = value
            'Invalidate()
        End Set
    End Property
    Private _yaw_angle As Double
    Public Property yaw_angle() As Double
        Get
            Return _yaw_angle
        End Get
        Set(ByVal value As Double)
            _yaw_angle = value
            Invalidate() 'ONLY REDRAW ONCE YAW ANGLE HAS BEEN SET
        End Set
    End Property

    Private _enable_display As Boolean = True
    Public Property enable_display() As Boolean
        Get
            Return _enable_display
        End Get
        Set(ByVal value As Boolean)
            _enable_display = value
            Invalidate()
        End Set
    End Property

    Private Function pitch_to_pix(ByVal pitch As Double) As Integer
        Try
            Return pitch / 35.0 * Me.Height / 2
        Catch
            Debug.Print("oops")

        End Try
    End Function

    Private Sub ArtificialHorizon_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Not (_enable_display) Then Return
        Try
            g = e.Graphics
            g.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed


            g.Clear(Me.BackColor)
            'Dim sin As Double = Math.Sin(roll_angle / 180 * Math.PI)

            g.ResetTransform()
            'g.FillRegion(Brushes.White, New Region(New Rectangle(0, 0, Me.Width, Me.Height)))

            ' rounded rectangle
            Dim path As New Drawing2D.GraphicsPath()
            Dim r As Single = 50
            path.AddArc(0, 0, r, r, 180, 90)
            path.AddArc(Me.Width - r, 0, r, r, 270, 90)
            path.AddArc(Me.Width - r, Me.Height - r, r, r, 0, 90)
            path.AddArc(0, Me.Height - r, r, r, 90, 90)
            'path.AddEllipse(0, 0, Me.Width, Me.Height)
            path.CloseFigure()
            g.SetClip(path)

            g.TranslateTransform(Me.Width / 2, Me.Height / 2)

            g.RotateTransform(roll_angle)
            g.TranslateTransform(0, pitch_to_pix(pitch_angle))

            ' chocolate
            'GROUND COLOR HERE:
            'Dim b As New System.Drawing.Drawing2D.LinearGradientBrush(New RectangleF(-Me.Width, 0, Me.Height * 2, Me.Width * 2), Color.FromArgb(255, 219, 140, 21), Color.Brown, Drawing2D.LinearGradientMode.Vertical)
            Dim b As New System.Drawing.Drawing2D.LinearGradientBrush(New RectangleF(-Me.Width, 0, Me.Height * 2, Me.Width * 2), Color.Brown, Color.Brown, Drawing2D.LinearGradientMode.Vertical)
            g.FillRectangle(b, New RectangleF(-Me.Width * 2, +1, Me.Height * 4, Me.Width * 4))

            g.RotateTransform(180)

            ' color.aqua
            b = New System.Drawing.Drawing2D.LinearGradientBrush(New RectangleF(-Me.Width, -1, Me.Height * 2, Me.Width * 2), Color.FromArgb(255, 28, 134, 186), Color.DarkBlue, Drawing2D.LinearGradientMode.Vertical)
            g.FillRectangle(b, New RectangleF(-Me.Width * 2, 0, Me.Height * 4, Me.Width * 4))

            g.ResetTransform()
            Dim w2 As Single = Me.Width / 2
            Dim s As Single = Me.Width / 38
            g.TranslateTransform(Me.Width / 2, Me.Height / 2)
            g.RotateTransform(45)
            g.TranslateTransform(-w2 + s, 0)
            g.DrawLine(New Pen(Color.White, 2), 0, 0, s * 2, 0)
            g.TranslateTransform(+w2 - s, 0)
            g.RotateTransform(15)
            g.DrawLine(New Pen(Color.White, 2), -w2 + s, 0, -w2 + s * 2, 0)
            g.RotateTransform(15)
            g.DrawLine(New Pen(Color.White, 2), -w2 + s, 0, -w2 + s * 2, 0)
            g.RotateTransform(15)
            g.DrawLine(New Pen(Color.White, 2), -w2 + s, 0, -w2 + s * 3, 0)
            'g.DrawString("0°", New System.Drawing.Font("sans-serif", 9), Brushes.White, -w2 + 40, -4)
            g.RotateTransform(15)
            g.DrawLine(New Pen(Color.White, 2), -w2 + s, 0, -w2 + s * 2, 0)
            g.RotateTransform(15)
            g.DrawLine(New Pen(Color.White, 2), -w2 + s, 0, -w2 + s * 2, 0)
            g.RotateTransform(15)
            g.DrawLine(New Pen(Color.White, 2), -w2 + s, 0, -w2 + s * 3, 0)
            'g.DrawString("+45°", New System.Drawing.Font("sans-serif", 9), Brushes.White, -w2 + 40, -4)


            g.ResetTransform()

            Dim length As Single = Me.Width / 4
            Dim notch As Single = Me.Width / 30
            g.TranslateTransform(Me.Width / 2, Me.Height / 2)
            g.DrawLine(New Pen(Color.White, 3), -length + notch * 2, 0, -notch, 0)
            g.DrawLine(New Pen(Color.White, 3), notch, 0, length - notch * 2, 0)
            g.DrawArc(New Pen(Color.White, 3), -notch, -notch, notch * 2, notch * 2, 180, -180)

            g.ResetTransform()

            ' Heading Triangle
            Dim ww As Single = Me.Width / 38
            g.TranslateTransform(Me.Width / 2, Me.Height / 2)
            'g.RotateTransform(-90 + yaw_angle)
            g.RotateTransform(-90)
            path = New Drawing2D.GraphicsPath()
            path.AddLine(w2 - ww * 3, 0, w2 - ww * 4, ww)
            path.AddLine(w2 - ww * 4, -ww, w2 - ww * 4, ww)
            path.AddLine(w2 - ww * 4, -ww, w2 - ww * 3, 0)
            g.FillRegion(Brushes.Red, New Region(path))
            g.DrawLine(New Pen(Color.White, 1), w2 - ww * 3, 0, w2 - ww * 4, ww)
            g.DrawLine(New Pen(Color.White, 1), w2 - ww * 4, -ww, w2 - ww * 4, ww)
            g.DrawLine(New Pen(Color.White, 1), w2 - ww * 4, -ww, w2 - ww * 3, 0)
            g.RotateTransform(yaw_angle)
            For i As Integer = 0 To 359 Step 30
                'g.RotateTransform(i) 'FIX THIS SO HEADING IS ROTATED CORRECTLY
                drawpsitext(g, i) 'This draws the compass points around the circle
            Next i



            g.ResetTransform()
            g.ResetClip()
            path = New Drawing2D.GraphicsPath()
            path.AddPie(New Rectangle(ww * 3, ww * 3, Me.Width - ww * 6, Me.Height - ww * 6), 0, 360)
            g.SetClip(path)

            g.TranslateTransform(Me.Width / 2, Me.Height / 2)
            g.RotateTransform(roll_angle)
            g.TranslateTransform(0, pitch_to_pix(pitch_angle))
            For i As Integer = -80 To 80 Step 10
                drawpitchline(g, i) 'This draws the text for the pitch axis
            Next i

        Catch
        End Try

    End Sub

    Private Sub drawpitchline(ByVal g As Graphics, ByVal pitch As Double)
        Try
            Dim w As Single = Me.Width / 8
            g.DrawLine(Pens.White, -w, pitch_to_pix(-pitch + 5), w, pitch_to_pix(-pitch + 5))
            g.DrawLine(Pens.White, -w * 5 / 3, pitch_to_pix(-pitch), w * 5 / 3, pitch_to_pix(-pitch))
            g.DrawString(pitch, Me.Font, Brushes.White, -w * 75 / 30, pitch_to_pix(-pitch) - 5)
            g.DrawString(pitch, Me.Font, Brushes.White, w * 2, pitch_to_pix(-pitch) - 5)
        Catch
        End Try
    End Sub

    Private Sub drawrollline(ByVal g As Graphics, ByVal a As Single)
        Try
            Dim w2 As Single = Me.Width / 2
            g.RotateTransform(a + 90)
            g.TranslateTransform(-w2 + 10, 0)
            g.DrawLine(Pens.White, 0, 0, 20, 0)
            g.TranslateTransform(10, 5)
            g.RotateTransform(-a - 90)
            g.DrawString("" & (a) & "°", New System.Drawing.Font("sans-serif", 9), Brushes.White, 0, 0)
            g.RotateTransform(+90 + a)
            g.TranslateTransform(-10, -5)
            g.TranslateTransform(+w2 - 10, 0)
            g.RotateTransform(-a - 90)
        Catch
        End Try
    End Sub
    Private Sub drawpsitext(ByVal g As Graphics, ByVal psi As Double)
        Try
            Dim w As Single = Me.Width / 2.5
            g.DrawString(psi, Me.Font, Brushes.Yellow, w * Math.Cos(psi * Math.PI / 180), -w * Math.Sin(psi * Math.PI / 180))

        Catch ex As Exception

        End Try

    End Sub


End Class
