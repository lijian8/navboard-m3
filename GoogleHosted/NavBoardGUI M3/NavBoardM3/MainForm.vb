'/**************************************************************************/
'/*! 
'    @file     MainForm.vb
'    @author   M. Ryan (ryanmechatronics.com)
'    @date     8 Aug 2010
'    @version  0.10

'    @section DESCRIPTION

'    Open Source main form for Navigation Board M3 GUI

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
Imports Microsoft.Win32

Public Class MainForm
    Dim dataincoming(100) As Byte
    Dim PlotBuffer(13, 100) As Integer
    Dim PlotBufferF(13, 100) As Single

    Dim gAttitude As Class_NavBoard.NAVBOARD_ATTITUDE_MSG

    Dim DefaultDirectory As String = "C:\NAVBOARD"
    Dim debugfile As System.IO.StreamWriter
    Dim debugfilefp As System.IO.StreamWriter
    Dim gSplitLogs As Boolean

   
    Dim PlotBufferIndex As Byte
    Dim ErrorFadeTime, TotalErrors As Integer
    Dim gXsumFail As Long, gXsumPass As Long
    Dim gSWVersion As String
    Dim gSerialNumber As String

    Dim gMode As Byte

    Dim gRunningBIT(0 To 8) As Int32

    Dim tempbitcounter As Int32 = 0
    Dim tempi2ccounter As Int32 = 0

    Dim gRawMsg As String = "0,0,0,0,0,0,0,0,0,0,0,0"
    Dim gAllowCalibrationScreen As Boolean = False

    Dim NavBoard_Sensordata As Class_NavBoard.NAVBOARD_IMU_FP_MSG

    Private Shared Timer_Logging As New System.Windows.Forms.Timer()


    'Below added to try to fix font change issue...
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Add any initialization after the InitializeComponent() call.
        AddHandler SystemEvents.UserPreferenceChanged, New UserPreferenceChangedEventHandler(AddressOf SystemEvents_UserPreferenceChangesEventHandler)

    End Sub

    Private Sub SystemEvents_UserPreferenceChangesEventHandler(ByVal sender As Object, ByVal e As UserPreferenceChangedEventArgs)
        If (e.Category = UserPreferenceCategory.Window) Then
            Me.Font = SystemFonts.IconTitleFont
        End If
    End Sub


    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        RemoveHandler SystemEvents.UserPreferenceChanged, New UserPreferenceChangedEventHandler(AddressOf SystemEvents_UserPreferenceChangesEventHandler)
    End Sub


    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            CheckForIllegalCrossThreadCalls = False
            SerialPort_QuickCheck_Available()
            If (ToolStripComboBoxSerialPorts.Items.Count > 0) Then ToolStripComboBoxSerialPorts.SelectedIndex = 0 'Highlight the first com port if it is there
            If (Not (My.Computer.FileSystem.DirectoryExists(DefaultDirectory))) Then
                MsgBox("Creating " & DefaultDirectory & " directory for file placement.")
                Try
                    My.Computer.FileSystem.CreateDirectory(DefaultDirectory)
                Catch ex As Exception
                    MsgBox("The directory could not be created due to the following error:" & vbCrLf & vbCrLf & ex.Message)
                    ErrorMsgHandling("The directory could not be created due to the following error:" & vbCrLf & ex.Message)
                End Try
            End If

            For j As Integer = 0 To 99
                For i As Byte = 0 To 12
                    PlotBuffer(i, j) = 0 * (Rnd(j + i) * 256 - 128) 'Bogus points...
                Next i
            Next j

            gAttitude.phi = 0
            gAttitude.theta = 0
            gAttitude.psi = 0


            ErrorFadeTime = 0
            TotalErrors = 0


            'Timer logging graphic flash
            AddHandler Timer_Logging.Tick, AddressOf Timer_Logging_Tick
            Timer_Logging.Interval = 1000
            Timer_Logging.Enabled = False
            'Timer_Logging.Start()


            'Highlight last serial port
            Dim kk As Integer = 0, choice As Integer = 0
            For Each listedport As String In ToolStripComboBoxSerialPorts.Items
                If listedport = My.Settings.LastSerialPort Then
                    choice = kk
                Else
                    kk = kk + 1
                End If
            Next
            ToolStripComboBoxSerialPorts.SelectedIndex = choice

        Catch ex As Exception
            Debug.Print("FORM DIES HERE")
        End Try

    End Sub

    Private Sub SerialPort_Check_Available()
        ' Get a list of serial port names.
        Dim ports As String() = IO.Ports.SerialPort.GetPortNames()
        ' Display each port name to the listbox.
        Dim port As String
        For Each port In ports
            Try
                SerialPort1.PortName = port
                SerialPort1.Open()
                SerialPort1.Close()
            Catch ex As Exception
                ToolStripComboBoxSerialPorts.Items.Remove(port)
            End Try
        Next port
    End Sub
    Private Sub SerialPort_QuickCheck_Available()
        ' Get a list of serial port names.
        Dim ports As String() = IO.Ports.SerialPort.GetPortNames()
        ' Display each port name to the listbox.
        Dim port As String
        For Each port In ports
            ToolStripComboBoxSerialPorts.Items.Add(port.ToString)
        Next port
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Clear all data

        TextBox_ADC.Clear()
        TextBox_GPIO.Clear()
        TextBox_GPS.Clear()

        RichTextBoxErrors.Text = ""
        TotalErrors = 0
        LabelAppErrors.Text = "App Errors: " & vbCrLf & TotalErrors.ToString
        gXsumFail = 0
        gXsumPass = 0
    End Sub

    Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Dim c As Byte
        Dim cirbuf() As Byte = New Byte(3) {0, 0, 0, 0} 'Empty, used to detect mode only
        'G, = text mode
        '0xAE, 0xAE = binary mode
        'Ryan = BIT mode
        Try
            While (SerialPort1.BytesToRead > 0) 'Loop while still receving characters
                c = SerialPort1.ReadByte
                cirbuf(0) = cirbuf(1)
                cirbuf(1) = cirbuf(2)
                cirbuf(2) = cirbuf(3)
                cirbuf(3) = c
                If ((cirbuf(0) = Asc("G")) And (cirbuf(1) = Asc(","))) Then
                    gMode = 0

                    TextBox_Serial.Text = "G"
                End If
                If ((cirbuf(0) = &HAE) And (cirbuf(1) = &HAE)) Then gMode = 1
                If ((cirbuf(0) = Asc("R")) And (cirbuf(1) = Asc("y")) And (cirbuf(2) = Asc("a")) And (cirbuf(3) = Asc("n"))) Then
                    gMode = 2

                    TextBox_Serial.Text = "Rya"
                End If

                UpdateMode()

                'If not binary messages, print text out to window
                If gMode <> 1 Then
                    If ((c = &HA) Or (c = &HD) Or ((c >= &H20) And (c <= &H7E))) Then
                        If (c = &HA) Or (c = &HD) Then
                            TextBox_Serial.AppendText(vbCrLf)
                        Else
                            TextBox_Serial.AppendText(Chr(c))
                        End If
                    End If
                    End If


                    'Process binary messages below.  Can let everything go thru, text messages won't pass criteria
                    Call NavBoard_process(c)


            End While
        Catch ex As Exception
            Debug.Print("oops...")
        End Try

    End Sub
    Private Sub NavBoard_process(ByVal c As Byte)
        Static bTerminate As Boolean = False, incindex As Byte = 0, dProgress As Byte = 0, mLength As Byte
        Dim pXsum As Byte
        Try
            Select Case (dProgress)
                Case 0 ' Waiting for start character 0
                    If (c = &HAE) Then
                        dataincoming(incindex) = c
                        incindex = incindex + 1
                        dProgress = dProgress + 1
                    End If
                    Exit Select
                Case 1 ' Waiting for start character 1
                    If (c = &HAE) Then
                        dataincoming(incindex) = c
                        dProgress = dProgress + 1 'State 2 - waiting for terminator characters
                        incindex = incindex + 1
                    Else
                        dProgress = 0 'Start over
                        incindex = 0
                    End If
                    Exit Select
                Case 2 ' Waiting for length information
                    mLength = c
                    dataincoming(incindex) = c
                    dProgress = dProgress + 1 'State 2 - waiting for terminator characters
                    incindex = incindex + 1
                    Exit Select
                Case 3 ' Read until length is complete
                    dataincoming(incindex) = c
                    If (incindex >= mLength) Then
                        bTerminate = True
                        pXsum = c
                    End If
                    incindex = incindex + 1
                    Exit Select
            End Select
            'Now, if we have reached the end of the message, verify the checksum
            If bTerminate Then
                Try
                    Dim doitbyte() As Byte
                    ReDim doitbyte(mLength)
                    Array.Copy(dataincoming, doitbyte, mLength)
                    'Handle data view here
                    NavBoardM3_Display(doitbyte)

                Catch ex As Exception
                    ErrorMsgHandling(ex.Message)
                End Try

            End If

            If (bTerminate) Then
                'Empty the buffer for the next set
                Array.Clear(dataincoming, 0, 100)
                bTerminate = False
                dProgress = 0
                incindex = 0
            End If

        Catch ex As Exception
            Debug.Print("Error in serial port: " & ex.Message & vbCrLf)
        End Try
    End Sub

    Public Function ByteToFloat(ByVal b() As Byte, ByVal index As Byte) As Single
        Dim byteArray(4) As Byte
        Try
            Array.Copy(b, index, byteArray, 0, 4)
            'Array.Reverse(byteArray, 0, 4)
            ByteToFloat = BitConverter.ToSingle(byteArray, 0)
        Catch ex As Exception

        End Try
    End Function
    Public Function ByteToInt16(ByVal b() As Byte, ByVal index As Byte) As Int16
        Dim byteArray(2) As Byte
        Try
            Array.Copy(b, index, byteArray, 0, 2)
            'Array.Reverse(byteArray, 0, 2)
            ByteToInt16 = BitConverter.ToInt16(byteArray, 0)
        Catch ex As Exception

        End Try
    End Function
    Public Function ByteToInt32(ByVal b() As Byte, ByVal index As Byte) As Int32
        Dim byteArray(4) As Byte
        Try
            Array.Copy(b, index, byteArray, 0, 4)
            'Array.Reverse(byteArray, 0, 4)
            ByteToInt32 = BitConverter.ToInt32(byteArray, 0)
        Catch ex As Exception

        End Try
    End Function

    Public Function ByteToUInt32(ByVal b() As Byte, ByVal index As Byte) As UInt32
        Dim byteArray(4) As Byte
        Try
            Array.Copy(b, index, byteArray, 0, 4)
            'Array.Reverse(byteArray, 0, 4)
            ByteToUInt32 = BitConverter.ToUInt32(byteArray, 0)
        Catch ex As Exception

        End Try
    End Function
    Private Sub NavBoardM3_Display(ByVal data() As Byte)
        Dim header(2) As Byte
        Dim length As Byte
        Dim phi As Int16, theta As Int16, psi As Int16
        Dim index As Byte = 0

        Dim gps_raw_lat As Int32
        Dim gps_raw_lon As Int32
        Dim gps_raw_alt As Int32

        Dim gps_fix As Int16
        Dim gps_sats As Int16
        Dim gps_utc As UInt32
        Dim gps_lat As Single
        Dim gps_long As Single
        Dim gps_alt As Single
        Dim gps_newdata As Int16

        Dim accel(2) As Single
        Dim gyro_temp_C As Single
        Dim gyro(2) As Single
        Dim mag(2) As Single
        Dim adc(1) As Int16
        Dim gpio As Byte

        Try
            'Must swap endianess - remember that this swaps the bytes for all, so mode, calstat, etc are swapped.
            header(0) = data(0)
            header(1) = data(1)
            length = data(2)

            'Skip raw data from GPS if you want, but we send it down anyway
            index = 3
            gps_raw_lat = ByteToInt32(data, index)
            index += 4
            gps_raw_lon = ByteToInt32(data, index)
            index += 4
            gps_raw_alt = ByteToInt32(data, index)
            index += 4


            gps_fix = ByteToInt16(data, index)
            index += 2
            gps_sats = ByteToInt16(data, index)
            index += 2
            gps_utc = ByteToUInt32(data, index)
            index += 4
            gps_lat = ByteToFloat(data, index)
            index += 4
            gps_long = ByteToFloat(data, index)
            index += 4
            gps_alt = ByteToFloat(data, index)
            index += 4
            gps_newdata = ByteToInt16(data, index)
            index += 2

            For j As Byte = 0 To 2
                accel(j) = ByteToFloat(data, index)
                index += 4
            Next

            gyro_temp_C = ByteToFloat(data, index)
            index += 4

            For j As Byte = 0 To 2
                gyro(j) = ByteToFloat(data, index)
                index += 4
            Next
            For j As Byte = 0 To 2
                mag(j) = ByteToFloat(data, index)
                index += 4
            Next
            For j As Byte = 0 To 1
                adc(j) = ByteToInt16(data, index)
                index += 2
            Next

            gpio = data(index)
            index += 1


            'Not really an attitude estimator, just a tilt sensor and simple heading calc
            phi = Math.Atan2(accel(1), accel(2)) * 180 / Math.PI
            theta = Math.Atan2(accel(0), accel(2)) * 180 / Math.PI
            psi = Math.Atan2(mag(1), mag(0)) * 180 / Math.PI


            'Go display them if needed...
            gAttitude.phi = phi
            gAttitude.theta = theta
            gAttitude.psi = psi

            TextBoxRoll.Text = gAttitude.phi.ToString("F2")
            TextBoxPitch.Text = gAttitude.theta.ToString("F2")
            TextBoxYaw.Text = gAttitude.psi.ToString("F2")

            TextBoxTime.Text = (gps_utc).ToString("F6")

            ReDim NavBoard_Sensordata.acc(0 To 2)
            ReDim NavBoard_Sensordata.rate(0 To 2)
            ReDim NavBoard_Sensordata.mag(0 To 2)
            ReDim NavBoard_Sensordata.spare(0 To 1)

            For j As Byte = 0 To 2
                NavBoard_Sensordata.acc(j) = accel(j)
                NavBoard_Sensordata.rate(j) = gyro(j) 'Make sure its in rad/s for proper display
                NavBoard_Sensordata.mag(j) = mag(j)
            Next
            NavBoard_Sensordata.gTimeCnts = CSng(gps_utc)
            NavBoard_Sensordata.spare(0) = adc(0)
            NavBoard_Sensordata.spare(1) = adc(1)
            NavBoard_Sensordata.cputemp = gyro_temp_C * 9 / 5 + 32

            HandlesBufferUpdateFP(NavBoard_Sensordata)

            TextBox_Serial.Text = "Binary Output Mode"


            TextBox_GPS.Text = "FIX: " & gps_fix.ToString & vbCrLf & _
                                "Sats: " & gps_sats.ToString & vbCrLf & _
                                "UTC: " & gps_utc.ToString & vbCrLf & _
                                "Lat: " & gps_lat.ToString & vbCrLf & _
                                "Long: " & gps_long.ToString & vbCrLf & _
                                "Alt: " & gps_alt.ToString

            TextBox_ADC.Text = "ADC0: " & adc(0).ToString & vbCrLf & _
                               "ADC1: " & adc(1).ToString

            ' The ExamineBit function will return True or False 
            ' depending on the value of the 1 based, nth bit (MyBit) 
            ' of an integer (MyByte).

            Dim testbit(0 To 7) As Boolean

            For i As Byte = 0 To 7
                testbit(i) = ExamineBit(gpio, i)
            Next
            TextBox_GPIO.Text = "AD0:    " & testbit(0).ToString & vbCrLf & _
                                "AD1:    " & testbit(1).ToString & vbCrLf & _
                                "GPS TP: " & testbit(2).ToString & vbCrLf & _
                                "MAG DR: " & testbit(3).ToString & vbCrLf & _
                                "P1.9:   " & testbit(4).ToString & vbCrLf & _
                                "P1.10:  " & testbit(5).ToString & vbCrLf & _
                                "P2.6    " & testbit(6).ToString



        Catch ex As Exception
            ErrorMsgHandling("Nav3 handling: " & ex.Message)
        End Try

    End Sub

    Private Sub UpdateMode()
        Try
            Dim modestr As String
            Dim colorlcl As Color
            Dim textcolor As Color
            modestr = "Unknown"
            colorlcl = Drawing.Color.AntiqueWhite
            textcolor = Drawing.Color.Black
            Select Case gMode
                Case Is = 0 'init
                    modestr = "Text Out"
                    colorlcl = Drawing.Color.AliceBlue
                    textcolor = Drawing.Color.Black
                Case Is = 1
                    modestr = "Binary"
                    colorlcl = Drawing.Color.LightGreen
                    textcolor = Drawing.Color.Black
                Case Is = 2
                    modestr = "BIT"
                    colorlcl = Drawing.Color.DarkGray
                    textcolor = Drawing.Color.White
                Case Else
                    modestr = "???"
                    colorlcl = Drawing.Color.Red
                    textcolor = Drawing.Color.Yellow
            End Select
            RichTextBoxMode.Text = modestr
            RichTextBoxMode.BackColor = colorlcl
            RichTextBoxMode.ForeColor = textcolor
        Catch ex As Exception
            ErrorMsgHandling("Mode handling: " & ex.Message)
        End Try
    End Sub





    Private Sub HandlesBufferUpdateFP(ByVal msgfp As Class_NavBoard.NAVBOARD_IMU_FP_MSG)
        Static j As Byte = 0
        Try
            PlotBufferF(0, j) = (msgfp.cputemp)
            PlotBufferF(1, j) = (msgfp.acc(0))
            PlotBufferF(2, j) = (msgfp.acc(1))
            PlotBufferF(3, j) = (msgfp.acc(2))
            PlotBufferF(4, j) = (msgfp.rate(0))
            PlotBufferF(5, j) = (msgfp.rate(1))
            PlotBufferF(6, j) = (msgfp.rate(2))
            PlotBufferF(7, j) = (msgfp.mag(0))
            PlotBufferF(8, j) = (msgfp.mag(1))
            PlotBufferF(9, j) = (msgfp.mag(2))
            PlotBufferF(10, j) = (msgfp.spare(0))
            PlotBufferF(11, j) = (msgfp.spare(1))
            PlotBufferF(12, j) = (msgfp.cputemp)

            PlotBufferIndex = j

            j = j + 1
            If j > 99 Then j = 0
        Catch ex As Exception
            ErrorMsgHandling(ex.Message)
        End Try

    End Sub



    Private Sub Timer_Logging_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If we are here, the logging has been enabled by definition (we only enable this timer when logging starts)
        Static pBackCol As Color
        Try
            If Not (ToolStripMenuItemDataLogging.BackColor = Color.LightGreen) Then
                pBackCol = ToolStripMenuItemDataLogging.BackColor
                ToolStripMenuItemDataLogging.BackColor = Color.LightGreen
            Else
                ToolStripMenuItemDataLogging.BackColor = pBackCol
            End If
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If TabControl1.SelectedIndex = 0 Then

                ArtificialHorizon1.roll_angle = -gAttitude.phi
                ArtificialHorizon1.pitch_angle = gAttitude.theta
                ArtificialHorizon1.yaw_angle = gAttitude.psi
                'Yaw update causes invalidate flag to go, it refreshes by itself

                'ArtificialHorizon1.Refresh()

                'Update compass picture
                PictureBoxHeading.Refresh()

            End If
            If TabControl1.SelectedIndex = 1 Then
                PictureBoxCPUTEMP.Refresh()
                PictureBoxAcc0.Refresh()
                PictureBoxAcc1.Refresh()
                PictureBoxAcc2.Refresh()
                PictureBoxRate0.Refresh()
                PictureBoxRate1.Refresh()
                PictureBoxRate2.Refresh()
                PictureBoxMag0.Refresh()
                PictureBoxMag1.Refresh()
                PictureBoxMag2.Refresh()
            End If


        Catch ex As Exception
            Debug.Print(ex.Message)

        End Try


    End Sub
    Private Sub PictureBoxAttitude_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        Try
            Dim cp As Point()
            ReDim cp(0 To 3) '4 points
            '--------- Draw a nice chart
            Dim canvas As Graphics = e.Graphics
            '--------- color background
            canvas.Clear(Color.SaddleBrown)
            ' Create pens.
            Dim blackPen As New Pen(Color.Black, -1)
            Dim usedpen As Pen
            usedpen = blackPen
            '--------- Get array points
            'Point 0 is left bottom, 1 is top left, 2 is top right, 3 is bottom right
            Dim ythetaoffset As Integer = -Math.Tan(gAttitude.theta * Math.PI / 180) * 100
            Dim leftoffset As Integer = -(100 / Math.Cos(45 * Math.PI / 180)) * Math.Sin(gAttitude.phi * Math.PI / 180)
            Dim rightoffset As Integer = (100 / Math.Cos(45 * Math.PI / 180)) * Math.Sin(gAttitude.phi * Math.PI / 180)



            cp(0) = New Point(0, 0 + ythetaoffset + leftoffset)
            cp(1) = New Point(0, 500 + ythetaoffset + leftoffset)
            cp(2) = New Point(100, 500 + ythetaoffset + rightoffset)
            cp(3) = New Point(100, 0 + ythetaoffset + rightoffset)
            '--------- Prep to add labels
            Dim x1, y1 As Single
            Dim x2, y2 As Single
            Dim scaleX, scaleY As Single
            '--------- Set scaling based on data:
            x1 = 0
            x2 = 100
            y1 = -100
            y2 = 100
            scaleX = sender.ClientSize.Width / (x2 - x1)
            scaleY = sender.ClientSize.Height / (y2 - y1)
            canvas.ScaleTransform(scaleX, -scaleY) 'inverted
            canvas.TranslateTransform(-x1, -y2) 'inverted
            '--------- draw chart outline
            Dim curvePoints As Point() = {cp(0), cp(1), cp(2), cp(3)}
            Dim blueBrush As New SolidBrush(Color.Blue)
            ' Draw polygon to screen.
            canvas.FillPolygon(blueBrush, curvePoints)
            'canvas.DrawClosedCurve(usedpen, cp)

            e.Dispose()
            blackPen.Dispose()
        Catch ex As Exception
            Debug.Print("Error: " & ex.Message & vbCrLf)
        End Try



    End Sub

    Private Sub PictureBoxUpdates(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs, ByVal field As Byte)
        Try
            'Dim cp As Point()
            Dim cpf As PointF()
            'ReDim cp(0 To 99) '100 samples per second, so this is 10 seconds
            ReDim cpf(0 To 99)
            '--------- Draw a nice chart
            Dim canvas As Graphics = e.Graphics
            '--------- color background
            canvas.Clear(Color.Black)
            ' Get size of graphics
            Dim picturewidth As Single = e.ClipRectangle.Width
            Dim pictureheight As Single = e.ClipRectangle.Height
            ' Create pens.
            Dim redPen As New Pen(Color.Red, -1)
            Dim greenPen As New Pen(Color.Green, -1)
            Dim bluePen As New Pen(Color.Blue, -1)
            Dim yellowPen As New Pen(Color.Yellow, -1)
            Dim gridpen As New Pen(Color.WhiteSmoke, -1)
            Dim usedpen As Pen
            usedpen = yellowPen
            '
            ' Declare a new font.
            Dim myFont As Font = New Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular)
            ' Set the TextRenderingHint property.
            canvas.TextRenderingHint = Drawing.Text.TextRenderingHint.SystemDefault
            ' Draw the value
            canvas.DrawString(Format(PlotBufferF(field, PlotBufferIndex), "###.###"), myFont, _
                Brushes.WhiteSmoke, picturewidth - 50.0F, pictureheight - 16.0F)


            '--------- Get array points
            For j As Integer = 0 To 99
                'cp(j) = New Point(j, PlotBuffer(field, j))
                cpf(j) = New PointF(j, PlotBufferF(field, j))
            Next
            '--------- Prep to add labels
            Dim x1, y1 As Single
            Dim x2, y2 As Single
            Dim scaleX, scaleY As Single
            Dim midline, lowerline, upperline As Single
            '--------- Set scaling based on data:
            Select Case field
                Case 0 'CPUTEMP
                    y1 = 50
                    y2 = 120
                    midline = 75
                    lowerline = 50
                    upperline = 100
                    usedpen = yellowPen
                Case 1 To 3 'Accels
                    y1 = -9.81 * 3
                    y2 = 9.81 * 3
                    midline = 0
                    lowerline = -9.81
                    upperline = 9.81
                    usedpen = bluePen
                Case 4 To 6 'Rate
                    y1 = -500 * Math.PI / 180.0
                    y2 = 500 * Math.PI / 180.0
                    midline = 0
                    lowerline = -150 * Math.PI / 180
                    upperline = 150 * Math.PI / 180
                    usedpen = greenPen
                Case 7 To 9 'Mags
                    y1 = -2
                    y2 = 2
                    midline = 0
                    lowerline = -1
                    upperline = 1
                    usedpen = redPen
                Case Else
                    y1 = -128
                    y2 = 128
                    midline = 0
                    lowerline = 64
                    upperline = 164
                    usedpen = yellowPen
            End Select
            x1 = 0
            x2 = 100

            scaleX = sender.ClientSize.Width / (x2 - x1)
            scaleY = sender.ClientSize.Height / (y2 - y1)
            canvas.ScaleTransform(scaleX, -scaleY) 'inverted
            canvas.TranslateTransform(-x1, -y2) 'inverted
            '--------- draw chart gridlines
            canvas.DrawLine(gridpen, 0, midline, 100, midline)
            canvas.DrawLine(gridpen, 0, lowerline, 100, lowerline)
            canvas.DrawLine(gridpen, 0, upperline, 100, upperline)
            '--------- draw chart data
            ' Draw curve to screen.
            'canvas.DrawCurve(usedpen, cp)
            canvas.DrawCurve(usedpen, cpf)

            ' Cleanup
            e.Dispose()
            yellowPen.Dispose()
            redPen.Dispose()
            bluePen.Dispose()
            greenPen.Dispose()
            gridpen.Dispose()
            myFont.Dispose()


        Catch ex As Exception
            Debug.Print("Error: " & ex.Message & vbCrLf)
        End Try

    End Sub

    Private Sub PictureBoxCPUTEMP_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBoxCPUTEMP.Paint
        PictureBoxUpdates(sender, e, 0)
    End Sub


    Private Sub PictureBoxHeading_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBoxHeading.Paint


        Static last_theta = 0

        Dim theta As Single = Single.Parse(TextBoxYaw.Text) * Math.PI / 180
        'Dim theta As Single
        'theta = gAttitude.theta * Math.PI / 180
        If (theta = last_theta) Then
            Exit Sub
        End If
        last_theta = theta
        'Rotation code from:
        'http://www.vb-helper.com/howto_net_image_rotate.html

        Try
            ' Copy the output bitmap from the source image.
            Dim bm_in As New Bitmap("compass_Smaller.bmp")

            ' Make an array of points defining the
            ' image's corners.
            Dim wid As Single = bm_in.Width
            Dim hgt As Single = bm_in.Height
            Dim corners As Point() = { _
                New Point(0, 0), _
                New Point(wid, 0), _
                New Point(0, hgt), _
                New Point(wid, hgt)}

            ' Translate to center the bounding box at the origin.
            Dim cx As Single = wid / 2
            Dim cy As Single = hgt / 2
            Dim i As Long
            For i = 0 To 3
                corners(i).X -= cx
                corners(i).Y -= cy
            Next i

            ' Rotate.
            'Dim theta As Single = Single.Parse(TextBoxYaw.Text) * Math.PI / 180
            Dim sin_theta As Single = Math.Sin(theta)
            Dim cos_theta As Single = Math.Cos(theta)
            Dim X As Single
            Dim Y As Single
            For i = 0 To 3
                X = corners(i).X
                Y = corners(i).Y
                corners(i).X = X * cos_theta + Y * sin_theta
                corners(i).Y = -X * sin_theta + Y * cos_theta
            Next i

            ' Translate so X >= 0 and Y >=0 for all corners.
            Dim xmin As Single = corners(0).X
            Dim ymin As Single = corners(0).Y
            For i = 1 To 3
                If xmin > corners(i).X Then xmin = corners(i).X
                If ymin > corners(i).Y Then ymin = corners(i).Y
            Next i
            For i = 0 To 3
                corners(i).X -= xmin
                corners(i).Y -= ymin
            Next i

            ' Create an output Bitmap and Graphics object.
            Dim bm_out As New Bitmap(CInt(-2 * xmin), CInt(-2 * _
                ymin))
            Dim gr_out As Graphics = Graphics.FromImage(bm_out)

            ' Drop the last corner lest we confuse DrawImage, 
            ' which expects an array of three corners.
            ReDim Preserve corners(2)

            ' Draw the result onto the output Bitmap.
            gr_out.DrawImage(bm_in, corners)

            ' Display the result.
            PictureBoxHeading.Image = bm_out
        Catch ex As Exception
            ErrorMsgHandling(ex.Message)
        End Try


    End Sub



    Private Sub ErrorMsgHandling(ByVal msg As String)
        If (ToolStripMenuItem_SoundOnOff.Checked = True) Then Beep()

        RichTextBoxErrors.Text = msg & vbCrLf & RichTextBoxErrors.Text
        ErrorFadeTime = 30 'Seconds since last error
        TotalErrors = TotalErrors + 1
    End Sub

    Private Sub PictureBoxAcc0_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBoxAcc0.Paint
        PictureBoxUpdates(sender, e, 1)
    End Sub

    Private Sub PictureBoxAcc1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBoxAcc1.Paint
        PictureBoxUpdates(sender, e, 2)
    End Sub

    Private Sub PictureBoxAcc2_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBoxAcc2.Paint
        PictureBoxUpdates(sender, e, 3)
    End Sub

    Private Sub PictureBoxRate0_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBoxRate0.Paint
        PictureBoxUpdates(sender, e, 4)
    End Sub

    Private Sub PictureBoxRate1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBoxRate1.Paint
        PictureBoxUpdates(sender, e, 5)
    End Sub
    Private Sub PictureBoxRate2_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBoxRate2.Paint
        PictureBoxUpdates(sender, e, 6)
    End Sub
    Private Sub PictureBoxMag0_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBoxMag0.Paint
        PictureBoxUpdates(sender, e, 7)
    End Sub
    Private Sub PictureBoxMag1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBoxMag1.Paint
        PictureBoxUpdates(sender, e, 8)
    End Sub
    Private Sub PictureBoxMag2_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBoxMag2.Paint
        PictureBoxUpdates(sender, e, 9)
    End Sub

    Private Sub TimerError_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerError.Tick

        Try


            ErrorFadeTime = ErrorFadeTime - 1
            If (ErrorFadeTime < 0) Then
                ErrorFadeTime = 0
            End If


            Dim r, g, b As Byte
            Dim colorcnt As Int16


            colorcnt = ErrorFadeTime * 16
            If (colorcnt > 480) Then colorcnt = 480

            If (colorcnt > 240) Then
                r = 240
            Else
                r = colorcnt
            End If
            If (colorcnt > 240) Then
                g = 480 - colorcnt
            Else
                g = 255
            End If
            b = 0

            RichTextBoxErrors.BackColor = Color.FromArgb(CType(r, Byte), CType(g, Byte), CType(b, Byte))


            LabelAppErrors.Text = "App Errors: " & vbCrLf & TotalErrors.ToString

        Catch ex As Exception
            Debug.Print("oops?")

        End Try

    End Sub



    Public Function XsumCRC32(ByVal CRC_acc As Integer, ByVal CRC_input As Byte()) As Integer

        Dim j As Integer
        Dim i As Byte
        Dim POLY As Integer = &HEDB88320

        Try

            For j = 0 To (CRC_input.GetUpperBound(0))
                CRC_acc = CRC_acc Xor CRC_input(j)
                For i = 0 To 7
                    If ((CRC_acc And &H1) = &H1) Then

                        CRC_acc = Convert.ToInt32(((CRC_acc And &HFFFFFFFE) \ 2&) And &H7FFFFFFF)
                        CRC_acc = CRC_acc Xor POLY

                        'CRC_acc = CRC_acc >> 1
                        'CRC_acc = CRC_acc Xor POLY
                    Else
                        CRC_acc = Convert.ToInt32(((CRC_acc And &HFFFFFFFE) \ 2&) And &H7FFFFFFF)

                        'CRC_acc = CRC_acc >> 1
                    End If
                Next i
            Next j
            XsumCRC32 = CInt(CRC_acc)
        Catch ex As Exception

            Debug.Print(ex.Message)

        End Try



        '    unsigned long UpdateCRC (unsigned long CRC_acc, unsigned char CRC_input)
        '{
        'unsigned char i; // loop counter
        '#define POLY 0xEDB88320 // bit-reversed version of the poly 0x04C11DB7
        '// Create the CRC "dividend" for polynomial arithmetic (binary arithmetic with no carries)
        'CRC_acc = CRC_acc ^ CRC_input;
        '// "Divide" the poly into the dividend using CRC XOR subtraction
        '// CRC_acc holds the "remainder" of each divide
        '//
        '// Only complete this division for 8 bits since input is 1 byte
        'for (i = 0; i < 8; i++)
        '{
        '// Check if the MSB is set (if MSB is 1, then the POLY can "divide"
        '// into the "dividend")
        'if ((CRC_acc & 0x00000001) == 0x00000001)
        '{
        '// if so, shift the CRC value, and XOR "subtract" the poly
        'CRC_acc = CRC_acc >> 1;
        'CRC_acc ^= POLY;
        '}
        '        Else
        '{
        '// if not, just shift the CRC value
        'CRC_acc = CRC_acc >> 1;
        '}
        '}
        '// Return the final remainder (CRC value)
        'return CRC_acc;
        '}

    End Function



    ' The ExamineBit function will return True or False 
    ' depending on the value of the 1 based, nth bit (MyBit) 
    ' of an integer (MyByte).
    Function ExamineBit(ByVal MyByte, ByVal MyBit) As Boolean
        Dim BitMask As Int16
        BitMask = 2 ^ (MyBit - 1)
        ExamineBit = ((MyByte And BitMask) > 0)
    End Function

    Sub VisitLinkRyanMechatronics()
        Try
            ' Change the color of the link text by setting LinkVisited 
            ' to True.
            'LinkLabelRyanMechatronics.LinkVisited = True
            ' Call the Process.Start method to open the default browser 
            ' with a URL:
            System.Diagnostics.Process.Start("http://www.ryanmechatronics.com")
        Catch ex As Exception
            Debug.Print(ex.Message)

        End Try

    End Sub

    Public Function GetBytesSingle(ByVal argument As Single, ByVal littleendiand As Boolean) As Byte()
        Dim byteArray As Byte() = BitConverter.GetBytes(argument)
        Dim reversed As Byte()
        reversed = byteArray.Clone
        If littleendiand = True Then
            Return byteArray
        Else
            Array.Reverse(reversed, 0, 4)
            Return reversed
        End If
    End Function

    Public Function GetBytesInt16(ByVal argument As Int16, ByVal littleendiand As Boolean) As Byte()
        Dim byteArray As Byte() = BitConverter.GetBytes(argument)
        Dim reversed As Byte()
        reversed = byteArray.Clone
        If littleendiand = True Then
            Return byteArray
        Else
            Array.Reverse(reversed, 0, 2)
            Return reversed
        End If
    End Function


    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click

        If OpenToolStripMenuItem.Text = "Open" Then
            Try
                If (SerialPort1.IsOpen = True) Then SerialPort1.Close()
                SerialPort1.PortName = ToolStripComboBoxSerialPorts.SelectedItem.ToString
                SerialPort1.Open()
                OpenToolStripMenuItem.Text = "Close"
                ToolStripComboBoxSerialPorts.BackColor = Color.AliceBlue

                CheckForNewPortsToolStripMenuItem.Enabled = False
                COMPortToolStripMenuItem.Image = My.Resources.Checkmark

            Catch ex As Exception
                MsgBox("Failure to Open: " & SerialPort1.PortName)
                ErrorMsgHandling("Failure to Open: " & SerialPort1.PortName)
                ToolStripComboBoxSerialPorts.BackColor = Color.White
                CheckForNewPortsToolStripMenuItem.Enabled = True
                COMPortToolStripMenuItem.Image = My.Resources.NoSave
            End Try
        ElseIf OpenToolStripMenuItem.Text = "Close" Then
            Try
                ToolStripComboBoxSerialPorts.BackColor = Color.White
                If (SerialPort1.IsOpen = True) Then
                    SerialPort1.Close()
                End If
                OpenToolStripMenuItem.Text = "Open"
                COMPortToolStripMenuItem.Image = My.Resources.NoSave
                CheckForNewPortsToolStripMenuItem.Enabled = True
                My.Settings.LastSerialPort = SerialPort1.PortName
                My.Settings.Save()

            Catch ex As Exception
                MsgBox("Failure to Close: " & SerialPort1.PortName)
                ErrorMsgHandling("Failure to Close: " & SerialPort1.PortName)
                CheckForNewPortsToolStripMenuItem.Enabled = False
            End Try
        End If
    End Sub

    Private Sub CheckForNewPortsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckForNewPortsToolStripMenuItem.Click
        If SerialPort1.IsOpen = True Then
            Exit Sub
        End If
        ToolStripComboBoxSerialPorts.Items.Clear()
        Call SerialPort_QuickCheck_Available()
    End Sub

    Private Sub TextFileSaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextFileSaveToolStripMenuItem.Click
        Static pBackCol As Color = ToolStripMenuItemDataLogging.BackColor
        Try
            'Note:  Checked on Click has to be enabled for this to work (it checks it before the routine gets here)
            If TextFileSaveToolStripMenuItem.Checked = True Then
                debugfile = My.Computer.FileSystem.OpenTextFileWriter(DefaultDirectory & "\" & ToolStripTextBoxFileName.Text & "_raw.dlm", False)
                debugfile.WriteLine("Time,CPU Temp,Accel X,Accel Y,Accel Z,Roll Rate,Pitch Rate,Psi Rate,Mag_X,Mag_Y,Mag_Z,Analog")
                debugfilefp = My.Computer.FileSystem.OpenTextFileWriter(DefaultDirectory & "\" & ToolStripTextBoxFileName.Text & "_fp.dlm", False)
                debugfilefp.WriteLine("Time,CPU Temp,Accel X,Accel Y,Accel Z,Phi_dot,Theta_dot,Psi_dot,Bias Phi_Dot,Bias Theta_dot,Bias Psi_dot,Mx,My,Mz,Analog,Phi,Theta,Psi,q0,q1,q2,q3")

  
                ToolStripMenuItemDataLogging.Image = My.Resources.Checkmark
                TextFileSaveToolStripMenuItem.Text = "Stop Log"
                pBackCol = ToolStripMenuItemDataLogging.BackColor
                ToolStripMenuItemDataLogging.BackColor = Color.LightGreen
                Timer_Logging.Enabled = True
            Else
                debugfile.Close()
                debugfilefp.Close()

                ToolStripMenuItemDataLogging.Image = My.Resources.NoSave
                TextFileSaveToolStripMenuItem.Text = "Start Log"
                Timer_Logging.Enabled = False
                ToolStripMenuItemDataLogging.BackColor = pBackCol

            End If

        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub
    Private Sub Logging_SplitFile()
        'If split file is checked, close current file after 1 hour and open a new one
        Static lasttime As Date
        Static LogId As Integer = 0
        Dim id As String
        'Get out if we aren't enabled
        If (gSplitLogs = False) Then Return

        Try
            If (DateDiff(DateInterval.Hour, lasttime, Now) >= 1) Then
                'It's been an hour, go ahead and start over
                LogId = LogId + 1
                id = LogId.ToString

                'Close existing logs
                debugfile.Close()
                debugfilefp.Close()

                'Open new ones
                debugfile = My.Computer.FileSystem.OpenTextFileWriter(DefaultDirectory & "\" & ToolStripTextBoxFileName.Text & "_raw" & id & ".dlm", False)
                debugfile.WriteLine("Time,CPU Temp,Accel X,Accel Y,Accel Z,Roll Rate,Pitch Rate,Psi Rate,Mag_X,Mag_Y,Mag_Z,Analog")
                debugfilefp = My.Computer.FileSystem.OpenTextFileWriter(DefaultDirectory & "\" & ToolStripTextBoxFileName.Text & "_fp" & id & ".dlm", False)
                debugfilefp.WriteLine("Time,CPU Temp,Accel X,Accel Y,Accel Z,Phi_dot,Theta_dot,Psi_dot,Bias Phi_Dot,Bias Theta_dot,Bias Psi_dot,Mx,My,Mz,Analog,Phi,Theta,Psi,q0,q1,q2,q3")

           
                lasttime = Now

            End If

        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub




    Private Sub ButtonClearAppErrors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClearAppErrors.Click
        RichTextBoxErrors.Text = ""
        TotalErrors = 0
        ErrorFadeTime = 0


        For kk As Byte = 1 To 8
            gRunningBIT(kk) = 0
        Next
    End Sub

    Private Sub ToolStripMenuItemWebsite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemWebsite.Click
        Try
            VisitLinkRyanMechatronics()
        Catch ex As Exception
            ' The error message
            MessageBox.Show("Unable to open link.  Try a Google search to get latest information for your area.")
        End Try

    End Sub

    Private Sub ToolStripMenuItem_UARTSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_UARTSettings.Click
        Dim oldbaud, newbaud As Integer
        oldbaud = SerialPort1.BaudRate
        If ToolStripMenuItem_UARTSettings.Checked = True Then newbaud = 115200 Else newbaud = 57600
        Try
            If (SerialPort1.IsOpen = True) Then
                SerialPort1.Close()
                SerialPort1.BaudRate = newbaud
                SerialPort1.Open()
            Else
                SerialPort1.BaudRate = newbaud
            End If



        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try
    End Sub







    Private Sub ToolStripMenuItem_SoundOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_SoundOnOff.Click
        If ToolStripMenuItem_SoundOnOff.Checked = True Then
            ToolStripMenuItem_SoundOnOff.Image = My.Resources.NoSave
            ToolStripMenuItem_SoundOnOff.Checked = False
        Else
            ToolStripMenuItem_SoundOnOff.Image = My.Resources.Checkmark
            ToolStripMenuItem_SoundOnOff.Checked = True

        End If
    End Sub


    Private Sub ToolStripMenuItem_DisplayOnOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem_DisplayOnOff.Click
        If ToolStripMenuItem_DisplayOnOff.Checked = True Then
            ToolStripMenuItem_DisplayOnOff.Image = My.Resources.NoSave
            ToolStripMenuItem_DisplayOnOff.Checked = False
            ArtificialHorizon1.enable_display = False
        Else
            ToolStripMenuItem_DisplayOnOff.Image = My.Resources.Checkmark
            ToolStripMenuItem_DisplayOnOff.Checked = True
            ArtificialHorizon1.enable_display = True
        End If
    End Sub


    Private Sub ButtonModeChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonModeChange.Click
        Static mMode As Byte = gMode
        Dim sMode As String
        mMode += 1
        Select Case mMode
            Case 0
                sMode = "0"
                ButtonModeChange.Text = "Text Out"
            Case 1
                sMode = "1"
                ButtonModeChange.Text = "Binary Out"
            Case 2
                sMode = "2"
                ButtonModeChange.Text = "BIT Out"
            Case Else
                mMode = 0
                sMode = "0"

        End Select

        SerialPort1.Write(sMode)

    End Sub

   
End Class


