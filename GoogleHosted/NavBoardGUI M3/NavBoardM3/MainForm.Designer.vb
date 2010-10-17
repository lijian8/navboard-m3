<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label34 = New System.Windows.Forms.Label
        Me.Graphs = New System.Windows.Forms.TabPage
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.PictureBoxMag0 = New System.Windows.Forms.PictureBox
        Me.PictureBoxMag1 = New System.Windows.Forms.PictureBox
        Me.PictureBoxMag2 = New System.Windows.Forms.PictureBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.PictureBoxRate2 = New System.Windows.Forms.PictureBox
        Me.PictureBoxRate1 = New System.Windows.Forms.PictureBox
        Me.PictureBoxRate0 = New System.Windows.Forms.PictureBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBoxAcc0 = New System.Windows.Forms.PictureBox
        Me.PictureBoxAcc2 = New System.Windows.Forms.PictureBox
        Me.PictureBoxAcc1 = New System.Windows.Forms.PictureBox
        Me.PictureBoxCPUTEMP = New System.Windows.Forms.PictureBox
        Me.Main = New System.Windows.Forms.TabPage
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox_GPIO = New System.Windows.Forms.TextBox
        Me.TextBox_ADC = New System.Windows.Forms.TextBox
        Me.TextBox_GPS = New System.Windows.Forms.TextBox
        Me.TextBox_Serial = New System.Windows.Forms.TextBox
        Me.ArtificialHorizon1 = New NavBoardM3.ArtificialHorizon
        Me.PictureBoxHeading = New System.Windows.Forms.PictureBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBoxImmediate = New System.Windows.Forms.GroupBox
        Me.ButtonModeChange = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.TextBoxYaw = New System.Windows.Forms.TextBox
        Me.TextBoxPitch = New System.Windows.Forms.TextBox
        Me.TextBoxRoll = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.Label37 = New System.Windows.Forms.Label
        Me.RichTextBoxMode = New System.Windows.Forms.RichTextBox
        Me.RichTextBoxErrors = New System.Windows.Forms.RichTextBox
        Me.TimerError = New System.Windows.Forms.Timer(Me.components)
        Me.LabelAppErrors = New System.Windows.Forms.Label
        Me.GroupBoxStatus = New System.Windows.Forms.GroupBox
        Me.Label66 = New System.Windows.Forms.Label
        Me.Label65 = New System.Windows.Forms.Label
        Me.TextBoxTimeBig = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.TextBoxTime = New System.Windows.Forms.TextBox
        Me.OpenFileDialogBiasSettings = New System.Windows.Forms.OpenFileDialog
        Me.MenuStripNavBoard = New System.Windows.Forms.MenuStrip
        Me.ToolStripComboBoxSerialPorts = New System.Windows.Forms.ToolStripComboBox
        Me.COMPortToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CheckForNewPortsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem_UARTSettings = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItemDataLogging = New System.Windows.Forms.ToolStripMenuItem
        Me.TextFileSaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_LogOptions = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_LogOption_RAW = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripTextBoxFileName = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripMenuItem_UserCustom = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_SoundOnOff = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem_DisplayOnOff = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FAQToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SoftwareVersionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CheckForUpdatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItemWebsite = New System.Windows.Forms.ToolStripMenuItem
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.ButtonClearAppErrors = New System.Windows.Forms.Button
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.WAYPOINTBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.WAYPOINTBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Graphs.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBoxMag0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxMag1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxMag2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBoxRate2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxRate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxRate0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBoxAcc0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxAcc2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxAcc1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxCPUTEMP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Main.SuspendLayout()
        CType(Me.PictureBoxHeading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxImmediate.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.GroupBoxStatus.SuspendLayout()
        Me.MenuStripNavBoard.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.WAYPOINTBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WAYPOINTBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SerialPort1
        '
        Me.SerialPort1.BaudRate = 115200
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 5000
        Me.ToolTip1.ForeColor = System.Drawing.SystemColors.Desktop
        Me.ToolTip1.InitialDelay = 1000
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.ReshowDelay = 100
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(18, 39)
        Me.Label34.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(44, 25)
        Me.Label34.TabIndex = 79
        Me.Label34.Text = "Roll"
        Me.ToolTip1.SetToolTip(Me.Label34, "Phi angle")
        '
        'Graphs
        '
        Me.Graphs.Controls.Add(Me.Label33)
        Me.Graphs.Controls.Add(Me.Label17)
        Me.Graphs.Controls.Add(Me.Label16)
        Me.Graphs.Controls.Add(Me.Label15)
        Me.Graphs.Controls.Add(Me.Panel3)
        Me.Graphs.Controls.Add(Me.Panel2)
        Me.Graphs.Controls.Add(Me.Panel1)
        Me.Graphs.Controls.Add(Me.PictureBoxCPUTEMP)
        Me.Graphs.Location = New System.Drawing.Point(4, 25)
        Me.Graphs.Margin = New System.Windows.Forms.Padding(4)
        Me.Graphs.Name = "Graphs"
        Me.Graphs.Padding = New System.Windows.Forms.Padding(4)
        Me.Graphs.Size = New System.Drawing.Size(818, 730)
        Me.Graphs.TabIndex = 1
        Me.Graphs.Text = "Data Graphs"
        Me.Graphs.UseVisualStyleBackColor = True
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(157, 664)
        Me.Label33.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(103, 18)
        Me.Label33.TabIndex = 80
        Me.Label33.Text = "Temperature"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(588, 20)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(116, 18)
        Me.Label17.TabIndex = 65
        Me.Label17.Text = "Magnetometer"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(337, 20)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(98, 18)
        Me.Label16.TabIndex = 64
        Me.Label16.Text = "Rate (rad/s)"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(51, 20)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(163, 18)
        Me.Label15.TabIndex = 63
        Me.Label15.Text = "Acceleration (m/s^2)"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.PictureBoxMag0)
        Me.Panel3.Controls.Add(Me.PictureBoxMag1)
        Me.Panel3.Controls.Add(Me.PictureBoxMag2)
        Me.Panel3.Location = New System.Drawing.Point(527, 42)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(249, 582)
        Me.Panel3.TabIndex = 62
        '
        'PictureBoxMag0
        '
        Me.PictureBoxMag0.BackColor = System.Drawing.SystemColors.ControlText
        Me.PictureBoxMag0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxMag0.Cursor = System.Windows.Forms.Cursors.No
        Me.PictureBoxMag0.Location = New System.Drawing.Point(4, 7)
        Me.PictureBoxMag0.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxMag0.Name = "PictureBoxMag0"
        Me.PictureBoxMag0.Size = New System.Drawing.Size(239, 184)
        Me.PictureBoxMag0.TabIndex = 33
        Me.PictureBoxMag0.TabStop = False
        '
        'PictureBoxMag1
        '
        Me.PictureBoxMag1.BackColor = System.Drawing.SystemColors.ControlText
        Me.PictureBoxMag1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxMag1.Cursor = System.Windows.Forms.Cursors.No
        Me.PictureBoxMag1.Location = New System.Drawing.Point(4, 199)
        Me.PictureBoxMag1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxMag1.Name = "PictureBoxMag1"
        Me.PictureBoxMag1.Size = New System.Drawing.Size(239, 184)
        Me.PictureBoxMag1.TabIndex = 34
        Me.PictureBoxMag1.TabStop = False
        '
        'PictureBoxMag2
        '
        Me.PictureBoxMag2.BackColor = System.Drawing.SystemColors.ControlText
        Me.PictureBoxMag2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxMag2.Cursor = System.Windows.Forms.Cursors.No
        Me.PictureBoxMag2.Location = New System.Drawing.Point(4, 391)
        Me.PictureBoxMag2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxMag2.Name = "PictureBoxMag2"
        Me.PictureBoxMag2.Size = New System.Drawing.Size(239, 184)
        Me.PictureBoxMag2.TabIndex = 35
        Me.PictureBoxMag2.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.PictureBoxRate2)
        Me.Panel2.Controls.Add(Me.PictureBoxRate1)
        Me.Panel2.Controls.Add(Me.PictureBoxRate0)
        Me.Panel2.Location = New System.Drawing.Point(269, 42)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(249, 582)
        Me.Panel2.TabIndex = 61
        '
        'PictureBoxRate2
        '
        Me.PictureBoxRate2.BackColor = System.Drawing.SystemColors.ControlText
        Me.PictureBoxRate2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxRate2.Cursor = System.Windows.Forms.Cursors.No
        Me.PictureBoxRate2.Location = New System.Drawing.Point(4, 391)
        Me.PictureBoxRate2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxRate2.Name = "PictureBoxRate2"
        Me.PictureBoxRate2.Size = New System.Drawing.Size(239, 184)
        Me.PictureBoxRate2.TabIndex = 32
        Me.PictureBoxRate2.TabStop = False
        '
        'PictureBoxRate1
        '
        Me.PictureBoxRate1.BackColor = System.Drawing.SystemColors.ControlText
        Me.PictureBoxRate1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxRate1.Cursor = System.Windows.Forms.Cursors.No
        Me.PictureBoxRate1.Location = New System.Drawing.Point(4, 199)
        Me.PictureBoxRate1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxRate1.Name = "PictureBoxRate1"
        Me.PictureBoxRate1.Size = New System.Drawing.Size(239, 184)
        Me.PictureBoxRate1.TabIndex = 31
        Me.PictureBoxRate1.TabStop = False
        '
        'PictureBoxRate0
        '
        Me.PictureBoxRate0.BackColor = System.Drawing.SystemColors.ControlText
        Me.PictureBoxRate0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxRate0.Cursor = System.Windows.Forms.Cursors.No
        Me.PictureBoxRate0.Location = New System.Drawing.Point(7, 7)
        Me.PictureBoxRate0.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxRate0.Name = "PictureBoxRate0"
        Me.PictureBoxRate0.Size = New System.Drawing.Size(239, 184)
        Me.PictureBoxRate0.TabIndex = 30
        Me.PictureBoxRate0.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PictureBoxAcc0)
        Me.Panel1.Controls.Add(Me.PictureBoxAcc2)
        Me.Panel1.Controls.Add(Me.PictureBoxAcc1)
        Me.Panel1.Location = New System.Drawing.Point(16, 42)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(245, 582)
        Me.Panel1.TabIndex = 60
        '
        'PictureBoxAcc0
        '
        Me.PictureBoxAcc0.BackColor = System.Drawing.SystemColors.ControlText
        Me.PictureBoxAcc0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxAcc0.Cursor = System.Windows.Forms.Cursors.No
        Me.PictureBoxAcc0.Location = New System.Drawing.Point(5, 7)
        Me.PictureBoxAcc0.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxAcc0.Name = "PictureBoxAcc0"
        Me.PictureBoxAcc0.Size = New System.Drawing.Size(239, 184)
        Me.PictureBoxAcc0.TabIndex = 100
        Me.PictureBoxAcc0.TabStop = False
        '
        'PictureBoxAcc2
        '
        Me.PictureBoxAcc2.BackColor = System.Drawing.SystemColors.ControlText
        Me.PictureBoxAcc2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxAcc2.Cursor = System.Windows.Forms.Cursors.No
        Me.PictureBoxAcc2.Location = New System.Drawing.Point(5, 391)
        Me.PictureBoxAcc2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxAcc2.Name = "PictureBoxAcc2"
        Me.PictureBoxAcc2.Size = New System.Drawing.Size(239, 184)
        Me.PictureBoxAcc2.TabIndex = 32
        Me.PictureBoxAcc2.TabStop = False
        '
        'PictureBoxAcc1
        '
        Me.PictureBoxAcc1.BackColor = System.Drawing.SystemColors.ControlText
        Me.PictureBoxAcc1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxAcc1.Cursor = System.Windows.Forms.Cursors.No
        Me.PictureBoxAcc1.Location = New System.Drawing.Point(5, 199)
        Me.PictureBoxAcc1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxAcc1.Name = "PictureBoxAcc1"
        Me.PictureBoxAcc1.Size = New System.Drawing.Size(239, 184)
        Me.PictureBoxAcc1.TabIndex = 31
        Me.PictureBoxAcc1.TabStop = False
        '
        'PictureBoxCPUTEMP
        '
        Me.PictureBoxCPUTEMP.BackColor = System.Drawing.SystemColors.ControlText
        Me.PictureBoxCPUTEMP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxCPUTEMP.Cursor = System.Windows.Forms.Cursors.No
        Me.PictureBoxCPUTEMP.Location = New System.Drawing.Point(273, 628)
        Me.PictureBoxCPUTEMP.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxCPUTEMP.Name = "PictureBoxCPUTEMP"
        Me.PictureBoxCPUTEMP.Size = New System.Drawing.Size(239, 91)
        Me.PictureBoxCPUTEMP.TabIndex = 79
        Me.PictureBoxCPUTEMP.TabStop = False
        '
        'Main
        '
        Me.Main.Controls.Add(Me.Label6)
        Me.Main.Controls.Add(Me.Label5)
        Me.Main.Controls.Add(Me.Label1)
        Me.Main.Controls.Add(Me.TextBox_GPIO)
        Me.Main.Controls.Add(Me.TextBox_ADC)
        Me.Main.Controls.Add(Me.TextBox_GPS)
        Me.Main.Controls.Add(Me.TextBox_Serial)
        Me.Main.Controls.Add(Me.ArtificialHorizon1)
        Me.Main.Controls.Add(Me.PictureBoxHeading)
        Me.Main.Controls.Add(Me.Label3)
        Me.Main.Controls.Add(Me.Label4)
        Me.Main.Controls.Add(Me.Label34)
        Me.Main.Controls.Add(Me.TextBoxYaw)
        Me.Main.Controls.Add(Me.TextBoxPitch)
        Me.Main.Controls.Add(Me.TextBoxRoll)
        Me.Main.Controls.Add(Me.Button2)
        Me.Main.Controls.Add(Me.Label2)
        Me.Main.Location = New System.Drawing.Point(4, 25)
        Me.Main.Margin = New System.Windows.Forms.Padding(4)
        Me.Main.Name = "Main"
        Me.Main.Padding = New System.Windows.Forms.Padding(4)
        Me.Main.Size = New System.Drawing.Size(818, 730)
        Me.Main.TabIndex = 0
        Me.Main.Text = "Main"
        Me.Main.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(535, 370)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 17)
        Me.Label1.TabIndex = 122
        Me.Label1.Text = "GPIO Status"
        '
        'TextBox_GPIO
        '
        Me.TextBox_GPIO.BackColor = System.Drawing.SystemColors.Info
        Me.TextBox_GPIO.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_GPIO.Location = New System.Drawing.Point(538, 391)
        Me.TextBox_GPIO.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox_GPIO.Multiline = True
        Me.TextBox_GPIO.Name = "TextBox_GPIO"
        Me.TextBox_GPIO.Size = New System.Drawing.Size(167, 239)
        Me.TextBox_GPIO.TabIndex = 121
        '
        'TextBox_ADC
        '
        Me.TextBox_ADC.BackColor = System.Drawing.SystemColors.Info
        Me.TextBox_ADC.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_ADC.Location = New System.Drawing.Point(280, 391)
        Me.TextBox_ADC.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox_ADC.Multiline = True
        Me.TextBox_ADC.Name = "TextBox_ADC"
        Me.TextBox_ADC.Size = New System.Drawing.Size(188, 80)
        Me.TextBox_ADC.TabIndex = 120
        '
        'TextBox_GPS
        '
        Me.TextBox_GPS.BackColor = System.Drawing.SystemColors.Info
        Me.TextBox_GPS.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_GPS.Location = New System.Drawing.Point(19, 391)
        Me.TextBox_GPS.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox_GPS.Multiline = True
        Me.TextBox_GPS.Name = "TextBox_GPS"
        Me.TextBox_GPS.Size = New System.Drawing.Size(208, 151)
        Me.TextBox_GPS.TabIndex = 119
        '
        'TextBox_Serial
        '
        Me.TextBox_Serial.BackColor = System.Drawing.SystemColors.Info
        Me.TextBox_Serial.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_Serial.Location = New System.Drawing.Point(538, 54)
        Me.TextBox_Serial.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox_Serial.Multiline = True
        Me.TextBox_Serial.Name = "TextBox_Serial"
        Me.TextBox_Serial.Size = New System.Drawing.Size(276, 291)
        Me.TextBox_Serial.TabIndex = 118
        '
        'ArtificialHorizon1
        '
        Me.ArtificialHorizon1.AutoScroll = True
        Me.ArtificialHorizon1.enable_display = True
        Me.ArtificialHorizon1.Location = New System.Drawing.Point(205, 20)
        Me.ArtificialHorizon1.Margin = New System.Windows.Forms.Padding(4)
        Me.ArtificialHorizon1.Name = "ArtificialHorizon1"
        Me.ArtificialHorizon1.pitch_angle = 0
        Me.ArtificialHorizon1.roll_angle = 0
        Me.ArtificialHorizon1.Size = New System.Drawing.Size(325, 325)
        Me.ArtificialHorizon1.TabIndex = 99
        Me.ArtificialHorizon1.yaw_angle = 0
        '
        'PictureBoxHeading
        '
        Me.PictureBoxHeading.BackgroundImage = Global.NavBoardM3.My.Resources.Resources.compass_Smaller
        Me.PictureBoxHeading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBoxHeading.ErrorImage = Nothing
        Me.PictureBoxHeading.InitialImage = Nothing
        Me.PictureBoxHeading.Location = New System.Drawing.Point(19, 183)
        Me.PictureBoxHeading.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxHeading.Name = "PictureBoxHeading"
        Me.PictureBoxHeading.Size = New System.Drawing.Size(179, 162)
        Me.PictureBoxHeading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxHeading.TabIndex = 86
        Me.PictureBoxHeading.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 123)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 25)
        Me.Label3.TabIndex = 81
        Me.Label3.Text = "Yaw"
        '
        'GroupBoxImmediate
        '
        Me.GroupBoxImmediate.Controls.Add(Me.ButtonModeChange)
        Me.GroupBoxImmediate.Location = New System.Drawing.Point(49, 338)
        Me.GroupBoxImmediate.Name = "GroupBoxImmediate"
        Me.GroupBoxImmediate.Size = New System.Drawing.Size(158, 81)
        Me.GroupBoxImmediate.TabIndex = 117
        Me.GroupBoxImmediate.TabStop = False
        Me.GroupBoxImmediate.Text = "Immediate Cmds"
        '
        'ButtonModeChange
        '
        Me.ButtonModeChange.Location = New System.Drawing.Point(33, 26)
        Me.ButtonModeChange.Name = "ButtonModeChange"
        Me.ButtonModeChange.Size = New System.Drawing.Size(89, 49)
        Me.ButtonModeChange.TabIndex = 106
        Me.ButtonModeChange.Text = "Change Mode"
        Me.ButtonModeChange.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 81)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 25)
        Me.Label4.TabIndex = 80
        Me.Label4.Text = "Pitch"
        '
        'TextBoxYaw
        '
        Me.TextBoxYaw.BackColor = System.Drawing.SystemColors.Info
        Me.TextBoxYaw.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxYaw.Location = New System.Drawing.Point(69, 120)
        Me.TextBoxYaw.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxYaw.Name = "TextBoxYaw"
        Me.TextBoxYaw.Size = New System.Drawing.Size(97, 30)
        Me.TextBoxYaw.TabIndex = 62
        Me.TextBoxYaw.Text = "0"
        Me.TextBoxYaw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxPitch
        '
        Me.TextBoxPitch.BackColor = System.Drawing.SystemColors.Info
        Me.TextBoxPitch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxPitch.Location = New System.Drawing.Point(69, 78)
        Me.TextBoxPitch.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxPitch.Name = "TextBoxPitch"
        Me.TextBoxPitch.Size = New System.Drawing.Size(97, 30)
        Me.TextBoxPitch.TabIndex = 61
        Me.TextBoxPitch.Text = "0"
        Me.TextBoxPitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxRoll
        '
        Me.TextBoxRoll.BackColor = System.Drawing.SystemColors.Info
        Me.TextBoxRoll.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxRoll.Location = New System.Drawing.Point(69, 36)
        Me.TextBoxRoll.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxRoll.Name = "TextBoxRoll"
        Me.TextBoxRoll.Size = New System.Drawing.Size(97, 30)
        Me.TextBoxRoll.TabIndex = 60
        Me.TextBoxRoll.Text = "0"
        Me.TextBoxRoll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(19, 574)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(96, 56)
        Me.Button2.TabIndex = 16
        Me.Button2.Text = "Clear Data Boxes"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(36, 368)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 17)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "GPS Data"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Main)
        Me.TabControl1.Controls.Add(Me.Graphs)
        Me.TabControl1.Location = New System.Drawing.Point(268, 30)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(826, 759)
        Me.TabControl1.TabIndex = 0
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(8, 26)
        Me.Label37.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(47, 17)
        Me.Label37.TabIndex = 84
        Me.Label37.Text = "Mode"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'RichTextBoxMode
        '
        Me.RichTextBoxMode.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.RichTextBoxMode.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.RichTextBoxMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxMode.Location = New System.Drawing.Point(63, 23)
        Me.RichTextBoxMode.Margin = New System.Windows.Forms.Padding(4)
        Me.RichTextBoxMode.Multiline = False
        Me.RichTextBoxMode.Name = "RichTextBoxMode"
        Me.RichTextBoxMode.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.RichTextBoxMode.Size = New System.Drawing.Size(124, 30)
        Me.RichTextBoxMode.TabIndex = 85
        Me.RichTextBoxMode.Text = "-"
        Me.RichTextBoxMode.WordWrap = False
        '
        'RichTextBoxErrors
        '
        Me.RichTextBoxErrors.BackColor = System.Drawing.SystemColors.Info
        Me.RichTextBoxErrors.Font = New System.Drawing.Font("Arial", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxErrors.Location = New System.Drawing.Point(5, 40)
        Me.RichTextBoxErrors.Margin = New System.Windows.Forms.Padding(4)
        Me.RichTextBoxErrors.Name = "RichTextBoxErrors"
        Me.RichTextBoxErrors.ReadOnly = True
        Me.RichTextBoxErrors.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.RichTextBoxErrors.Size = New System.Drawing.Size(256, 136)
        Me.RichTextBoxErrors.TabIndex = 1
        Me.RichTextBoxErrors.Text = ""
        '
        'TimerError
        '
        Me.TimerError.Enabled = True
        Me.TimerError.Interval = 1000
        '
        'LabelAppErrors
        '
        Me.LabelAppErrors.AutoSize = True
        Me.LabelAppErrors.Location = New System.Drawing.Point(2, 1)
        Me.LabelAppErrors.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelAppErrors.Name = "LabelAppErrors"
        Me.LabelAppErrors.Size = New System.Drawing.Size(77, 34)
        Me.LabelAppErrors.TabIndex = 88
        Me.LabelAppErrors.Text = "Application" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Errors"
        '
        'GroupBoxStatus
        '
        Me.GroupBoxStatus.Controls.Add(Me.Label66)
        Me.GroupBoxStatus.Controls.Add(Me.Label65)
        Me.GroupBoxStatus.Controls.Add(Me.TextBoxTimeBig)
        Me.GroupBoxStatus.Controls.Add(Me.RichTextBoxMode)
        Me.GroupBoxStatus.Controls.Add(Me.Label32)
        Me.GroupBoxStatus.Controls.Add(Me.Label37)
        Me.GroupBoxStatus.Controls.Add(Me.TextBoxTime)
        Me.GroupBoxStatus.Location = New System.Drawing.Point(0, 54)
        Me.GroupBoxStatus.Name = "GroupBoxStatus"
        Me.GroupBoxStatus.Size = New System.Drawing.Size(264, 174)
        Me.GroupBoxStatus.TabIndex = 102
        Me.GroupBoxStatus.TabStop = False
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(166, 95)
        Me.Label66.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(58, 17)
        Me.Label66.TabIndex = 98
        Me.Label66.Text = "h : m : s"
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(166, 67)
        Me.Label65.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(30, 17)
        Me.Label65.TabIndex = 97
        Me.Label65.Text = "sec"
        '
        'TextBoxTimeBig
        '
        Me.TextBoxTimeBig.BackColor = System.Drawing.SystemColors.Info
        Me.TextBoxTimeBig.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxTimeBig.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxTimeBig.Location = New System.Drawing.Point(63, 89)
        Me.TextBoxTimeBig.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxTimeBig.Name = "TextBoxTimeBig"
        Me.TextBoxTimeBig.Size = New System.Drawing.Size(95, 26)
        Me.TextBoxTimeBig.TabIndex = 82
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(12, 55)
        Me.Label32.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(43, 34)
        Me.Label32.TabIndex = 77
        Me.Label32.Text = "Run" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Time"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBoxTime
        '
        Me.TextBoxTime.BackColor = System.Drawing.SystemColors.Info
        Me.TextBoxTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxTime.Location = New System.Drawing.Point(63, 58)
        Me.TextBoxTime.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxTime.Name = "TextBoxTime"
        Me.TextBoxTime.Size = New System.Drawing.Size(95, 26)
        Me.TextBoxTime.TabIndex = 76
        '
        'OpenFileDialogBiasSettings
        '
        Me.OpenFileDialogBiasSettings.FileName = "CHIMUBias"
        Me.OpenFileDialogBiasSettings.InitialDirectory = "C:\CHIMU"
        Me.OpenFileDialogBiasSettings.Title = "Bias Setting Open"
        '
        'MenuStripNavBoard
        '
        Me.MenuStripNavBoard.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripComboBoxSerialPorts, Me.COMPortToolStripMenuItem, Me.ToolStripMenuItemDataLogging, Me.ToolStripMenuItem_UserCustom, Me.HelpToolStripMenuItem})
        Me.MenuStripNavBoard.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripNavBoard.Name = "MenuStripNavBoard"
        Me.MenuStripNavBoard.Size = New System.Drawing.Size(1094, 30)
        Me.MenuStripNavBoard.TabIndex = 104
        Me.MenuStripNavBoard.Text = "NavBoard"
        '
        'ToolStripComboBoxSerialPorts
        '
        Me.ToolStripComboBoxSerialPorts.Name = "ToolStripComboBoxSerialPorts"
        Me.ToolStripComboBoxSerialPorts.Size = New System.Drawing.Size(121, 26)
        '
        'COMPortToolStripMenuItem
        '
        Me.COMPortToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.CheckForNewPortsToolStripMenuItem, Me.ToolStripSeparator2, Me.ToolStripMenuItem_UARTSettings})
        Me.COMPortToolStripMenuItem.Image = Global.NavBoardM3.My.Resources.Resources.NoSave
        Me.COMPortToolStripMenuItem.Name = "COMPortToolStripMenuItem"
        Me.COMPortToolStripMenuItem.Size = New System.Drawing.Size(135, 26)
        Me.COMPortToolStripMenuItem.Text = "Communication"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.CheckOnClick = True
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'CheckForNewPortsToolStripMenuItem
        '
        Me.CheckForNewPortsToolStripMenuItem.Name = "CheckForNewPortsToolStripMenuItem"
        Me.CheckForNewPortsToolStripMenuItem.Size = New System.Drawing.Size(248, 22)
        Me.CheckForNewPortsToolStripMenuItem.Text = "Check for New Ports"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(245, 6)
        '
        'ToolStripMenuItem_UARTSettings
        '
        Me.ToolStripMenuItem_UARTSettings.Checked = True
        Me.ToolStripMenuItem_UARTSettings.CheckOnClick = True
        Me.ToolStripMenuItem_UARTSettings.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripMenuItem_UARTSettings.Name = "ToolStripMenuItem_UARTSettings"
        Me.ToolStripMenuItem_UARTSettings.Size = New System.Drawing.Size(248, 22)
        Me.ToolStripMenuItem_UARTSettings.Text = "UART Baud Rate - 115k"
        '
        'ToolStripMenuItemDataLogging
        '
        Me.ToolStripMenuItemDataLogging.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TextFileSaveToolStripMenuItem, Me.ToolStripMenuItem_LogOptions, Me.ToolStripTextBoxFileName})
        Me.ToolStripMenuItemDataLogging.Image = Global.NavBoardM3.My.Resources.Resources.NoSave
        Me.ToolStripMenuItemDataLogging.Name = "ToolStripMenuItemDataLogging"
        Me.ToolStripMenuItemDataLogging.Size = New System.Drawing.Size(121, 26)
        Me.ToolStripMenuItemDataLogging.Text = "Data Logging"
        '
        'TextFileSaveToolStripMenuItem
        '
        Me.TextFileSaveToolStripMenuItem.CheckOnClick = True
        Me.TextFileSaveToolStripMenuItem.Name = "TextFileSaveToolStripMenuItem"
        Me.TextFileSaveToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.TextFileSaveToolStripMenuItem.Text = "Start Log"
        '
        'ToolStripMenuItem_LogOptions
        '
        Me.ToolStripMenuItem_LogOptions.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_LogOption_RAW})
        Me.ToolStripMenuItem_LogOptions.Name = "ToolStripMenuItem_LogOptions"
        Me.ToolStripMenuItem_LogOptions.Size = New System.Drawing.Size(168, 22)
        Me.ToolStripMenuItem_LogOptions.Text = "Log Options"
        '
        'ToolStripMenuItem_LogOption_RAW
        '
        Me.ToolStripMenuItem_LogOption_RAW.Checked = True
        Me.ToolStripMenuItem_LogOption_RAW.CheckOnClick = True
        Me.ToolStripMenuItem_LogOption_RAW.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripMenuItem_LogOption_RAW.Name = "ToolStripMenuItem_LogOption_RAW"
        Me.ToolStripMenuItem_LogOption_RAW.Size = New System.Drawing.Size(118, 22)
        Me.ToolStripMenuItem_LogOption_RAW.Text = "Raw"
        '
        'ToolStripTextBoxFileName
        '
        Me.ToolStripTextBoxFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToolStripTextBoxFileName.Name = "ToolStripTextBoxFileName"
        Me.ToolStripTextBoxFileName.Size = New System.Drawing.Size(100, 24)
        Me.ToolStripTextBoxFileName.Text = "NavM3_File"
        '
        'ToolStripMenuItem_UserCustom
        '
        Me.ToolStripMenuItem_UserCustom.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_SoundOnOff, Me.ToolStripMenuItem_DisplayOnOff, Me.ToolStripSeparator5, Me.ToolStripSeparator4})
        Me.ToolStripMenuItem_UserCustom.Image = Global.NavBoardM3.My.Resources.Resources.NoSave
        Me.ToolStripMenuItem_UserCustom.Name = "ToolStripMenuItem_UserCustom"
        Me.ToolStripMenuItem_UserCustom.Size = New System.Drawing.Size(85, 26)
        Me.ToolStripMenuItem_UserCustom.Text = "Options"
        Me.ToolStripMenuItem_UserCustom.ToolTipText = "Enter password to enable custom user interface"
        '
        'ToolStripMenuItem_SoundOnOff
        '
        Me.ToolStripMenuItem_SoundOnOff.Image = Global.NavBoardM3.My.Resources.Resources.NoSave
        Me.ToolStripMenuItem_SoundOnOff.Name = "ToolStripMenuItem_SoundOnOff"
        Me.ToolStripMenuItem_SoundOnOff.Size = New System.Drawing.Size(257, 22)
        Me.ToolStripMenuItem_SoundOnOff.Text = "Sound On / Off"
        '
        'ToolStripMenuItem_DisplayOnOff
        '
        Me.ToolStripMenuItem_DisplayOnOff.Checked = True
        Me.ToolStripMenuItem_DisplayOnOff.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripMenuItem_DisplayOnOff.Image = Global.NavBoardM3.My.Resources.Resources.Checkmark
        Me.ToolStripMenuItem_DisplayOnOff.Name = "ToolStripMenuItem_DisplayOnOff"
        Me.ToolStripMenuItem_DisplayOnOff.Size = New System.Drawing.Size(257, 22)
        Me.ToolStripMenuItem_DisplayOnOff.Text = "Artificial Horizon Displayed"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(254, 6)
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(254, 6)
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FAQToolStripMenuItem, Me.SoftwareVersionToolStripMenuItem, Me.CheckForUpdatesToolStripMenuItem, Me.ToolStripMenuItemWebsite})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(48, 26)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'FAQToolStripMenuItem
        '
        Me.FAQToolStripMenuItem.Name = "FAQToolStripMenuItem"
        Me.FAQToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.FAQToolStripMenuItem.Text = "FAQ"
        '
        'SoftwareVersionToolStripMenuItem
        '
        Me.SoftwareVersionToolStripMenuItem.Name = "SoftwareVersionToolStripMenuItem"
        Me.SoftwareVersionToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.SoftwareVersionToolStripMenuItem.Text = "Software Version"
        '
        'CheckForUpdatesToolStripMenuItem
        '
        Me.CheckForUpdatesToolStripMenuItem.Name = "CheckForUpdatesToolStripMenuItem"
        Me.CheckForUpdatesToolStripMenuItem.Size = New System.Drawing.Size(271, 22)
        Me.CheckForUpdatesToolStripMenuItem.Text = "Check For Updates"
        '
        'ToolStripMenuItemWebsite
        '
        Me.ToolStripMenuItemWebsite.Image = Global.NavBoardM3.My.Resources.Resources.ryanmechlogo
        Me.ToolStripMenuItemWebsite.Name = "ToolStripMenuItemWebsite"
        Me.ToolStripMenuItemWebsite.Size = New System.Drawing.Size(271, 22)
        Me.ToolStripMenuItemWebsite.Text = "Ryan Mechatronics Website"
        '
        'Panel4
        '
        Me.Panel4.AutoSize = True
        Me.Panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel4.Controls.Add(Me.ButtonClearAppErrors)
        Me.Panel4.Controls.Add(Me.LabelAppErrors)
        Me.Panel4.Controls.Add(Me.RichTextBoxErrors)
        Me.Panel4.Location = New System.Drawing.Point(0, 609)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(265, 180)
        Me.Panel4.TabIndex = 105
        '
        'ButtonClearAppErrors
        '
        Me.ButtonClearAppErrors.Location = New System.Drawing.Point(158, 8)
        Me.ButtonClearAppErrors.Name = "ButtonClearAppErrors"
        Me.ButtonClearAppErrors.Size = New System.Drawing.Size(75, 25)
        Me.ButtonClearAppErrors.TabIndex = 89
        Me.ButtonClearAppErrors.Text = "Clear"
        Me.ButtonClearAppErrors.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(277, 370)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 17)
        Me.Label5.TabIndex = 123
        Me.Label5.Text = "ADC"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(634, 33)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 17)
        Me.Label6.TabIndex = 124
        Me.Label6.Text = "Raw Output"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.ClientSize = New System.Drawing.Size(1094, 859)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.GroupBoxStatus)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStripNavBoard)
        Me.Controls.Add(Me.GroupBoxImmediate)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "MainForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "Navigation Board M3"
        Me.Graphs.ResumeLayout(False)
        Me.Graphs.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.PictureBoxMag0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxMag1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxMag2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.PictureBoxRate2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxRate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxRate0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBoxAcc0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxAcc2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxAcc1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxCPUTEMP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Main.ResumeLayout(False)
        Me.Main.PerformLayout()
        CType(Me.PictureBoxHeading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxImmediate.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.GroupBoxStatus.ResumeLayout(False)
        Me.GroupBoxStatus.PerformLayout()
        Me.MenuStripNavBoard.ResumeLayout(False)
        Me.MenuStripNavBoard.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.WAYPOINTBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WAYPOINTBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Graphs As System.Windows.Forms.TabPage
    Friend WithEvents Main As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxYaw As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPitch As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxRoll As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents RichTextBoxMode As System.Windows.Forms.RichTextBox
    Friend WithEvents RichTextBoxErrors As System.Windows.Forms.RichTextBox
    Friend WithEvents PictureBoxHeading As System.Windows.Forms.PictureBox
    Friend WithEvents TimerError As System.Windows.Forms.Timer
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents PictureBoxMag0 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxMag1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxMag2 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents PictureBoxRate2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxRate1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxRate0 As System.Windows.Forms.PictureBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents PictureBoxCPUTEMP As System.Windows.Forms.PictureBox
    Friend WithEvents LabelAppErrors As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBoxAcc2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxAcc1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxAcc0 As System.Windows.Forms.PictureBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ArtificialHorizon1 As NavBoardM3.ArtificialHorizon
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ButtonModeChange As System.Windows.Forms.Button
    Friend WithEvents GroupBoxStatus As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxImmediate As System.Windows.Forms.GroupBox
    Friend WithEvents OpenFileDialogBiasSettings As System.Windows.Forms.OpenFileDialog
    Friend WithEvents MenuStripNavBoard As System.Windows.Forms.MenuStrip
    Friend WithEvents COMPortToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FAQToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SoftwareVersionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckForUpdatesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripComboBoxSerialPorts As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckForNewPortsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemDataLogging As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextFileSaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripTextBoxFileName As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents ButtonClearAppErrors As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem_UARTSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemWebsite As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents WAYPOINTBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents WAYPOINTBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ToolStripMenuItem_UserCustom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_SoundOnOff As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem_DisplayOnOff As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_LogOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem_LogOption_RAW As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTimeBig As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTime As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Serial As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_ADC As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_GPS As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_GPIO As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label

End Class
