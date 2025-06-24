namespace HW_204N
{
    partial class _3GenerationMiniController
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLight1 = new System.Windows.Forms.TrackBar();
            this.comBoxPorts = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbState = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.numLight1 = new System.Windows.Forms.NumericUpDown();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.串口状态 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tbLight4 = new System.Windows.Forms.TrackBar();
            this.numLight4 = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbLight3 = new System.Windows.Forms.TrackBar();
            this.numLight3 = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbLight2 = new System.Windows.Forms.TrackBar();
            this.numLight2 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button8 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.tbLight1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.串口状态.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLight4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLight3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLight2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serial Port:";
            // 
            // tbLight1
            // 
            this.tbLight1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbLight1.LargeChange = 10;
            this.tbLight1.Location = new System.Drawing.Point(54, 69);
            this.tbLight1.Maximum = 999;
            this.tbLight1.Name = "tbLight1";
            this.tbLight1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbLight1.Size = new System.Drawing.Size(45, 193);
            this.tbLight1.TabIndex = 6;
            this.tbLight1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbLight1.ValueChanged += new System.EventHandler(this.tbLight1_ValueChanged);
            // 
            // comBoxPorts
            // 
            this.comBoxPorts.FormattingEnabled = true;
            this.comBoxPorts.Location = new System.Drawing.Point(109, 26);
            this.comBoxPorts.Name = "comBoxPorts";
            this.comBoxPorts.Size = new System.Drawing.Size(102, 20);
            this.comBoxPorts.TabIndex = 7;
            this.comBoxPorts.SelectedValueChanged += new System.EventHandler(this.comBoxPorts_SelectedValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(297, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "Status:";
            // 
            // tbState
            // 
            this.tbState.Location = new System.Drawing.Point(354, 26);
            this.tbState.Name = "tbState";
            this.tbState.Size = new System.Drawing.Size(200, 21);
            this.tbState.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(39, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 10;
            this.label12.Text = "Channel 1";
            // 
            // numLight1
            // 
            this.numLight1.Location = new System.Drawing.Point(32, 268);
            this.numLight1.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numLight1.Name = "numLight1";
            this.numLight1.Size = new System.Drawing.Size(64, 21);
            this.numLight1.TabIndex = 24;
            this.numLight1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numLight1.ValueChanged += new System.EventHandler(this.numLight1_ValueChanged);
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM9";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // trackBar2
            // 
            this.trackBar2.LargeChange = 10;
            this.trackBar2.Location = new System.Drawing.Point(143, -40);
            this.trackBar2.Maximum = 1000;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(341, 45);
            this.trackBar2.TabIndex = 32;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // 串口状态
            // 
            this.串口状态.Controls.Add(this.button5);
            this.串口状态.Controls.Add(this.label7);
            this.串口状态.Controls.Add(this.label1);
            this.串口状态.Controls.Add(this.trackBar2);
            this.串口状态.Controls.Add(this.tbState);
            this.串口状态.Controls.Add(this.comBoxPorts);
            this.串口状态.Location = new System.Drawing.Point(32, 12);
            this.串口状态.Name = "串口状态";
            this.串口状态.Size = new System.Drawing.Size(666, 64);
            this.串口状态.TabIndex = 34;
            this.串口状态.TabStop = false;
            this.串口状态.Text = "Serial Port Status";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(571, 18);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(82, 34);
            this.button5.TabIndex = 33;
            this.button5.Text = "Connect";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.button10);
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.tbLight4);
            this.groupBox1.Controls.Add(this.numLight4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tbLight3);
            this.groupBox1.Controls.Add(this.numLight3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbLight2);
            this.groupBox1.Controls.Add(this.numLight2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbLight1);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.numLight1);
            this.groupBox1.Location = new System.Drawing.Point(32, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(666, 383);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Brightness Control";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.button11);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.numericUpDown2);
            this.panel1.Controls.Add(this.numericUpDown4);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.numericUpDown3);
            this.panel1.Location = new System.Drawing.Point(435, 118);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(193, 259);
            this.panel1.TabIndex = 70;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(161, 216);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(17, 12);
            this.label23.TabIndex = 69;
            this.label23.Text = "ms";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(44, 3);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(111, 33);
            this.button11.TabIndex = 56;
            this.button11.Text = "Trigger mode 2";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(161, 173);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(17, 12);
            this.label22.TabIndex = 68;
            this.label22.Text = "ms";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(38, 53);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(119, 12);
            this.label16.TabIndex = 59;
            this.label16.Text = "Pulse-Width Setting";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(161, 132);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(17, 12);
            this.label21.TabIndex = 67;
            this.label21.Text = "ms";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(91, 90);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(64, 21);
            this.numericUpDown1.TabIndex = 57;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(161, 92);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(17, 12);
            this.label20.TabIndex = 66;
            this.label20.Text = "ms";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(24, 92);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 12);
            this.label15.TabIndex = 58;
            this.label15.Text = "Channel 1";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(24, 216);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 12);
            this.label19.TabIndex = 65;
            this.label19.Text = "Channel 4";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(91, 130);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(64, 21);
            this.numericUpDown2.TabIndex = 60;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(91, 214);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(64, 21);
            this.numericUpDown4.TabIndex = 64;
            this.numericUpDown4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown4.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown4.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(24, 132);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 12);
            this.label17.TabIndex = 61;
            this.label17.Text = "Channel 2";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(24, 173);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 12);
            this.label18.TabIndex = 63;
            this.label18.Text = "Channel 3";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(91, 171);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(64, 21);
            this.numericUpDown3.TabIndex = 62;
            this.numericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "999等级",
            "255等级"});
            this.comboBox1.Location = new System.Drawing.Point(6, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(87, 20);
            this.comboBox1.TabIndex = 73;
            this.comboBox1.Text = "999 Level";
            this.comboBox1.Visible = false;
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(479, 69);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(111, 33);
            this.button10.TabIndex = 55;
            this.button10.Text = "Trigger Mode 1";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(479, 20);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(111, 33);
            this.button9.TabIndex = 54;
            this.button9.Text = "Always-On Mode";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(22, 338);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(71, 33);
            this.button6.TabIndex = 53;
            this.button6.Text = "Refresh";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(352, 338);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(71, 33);
            this.button7.TabIndex = 52;
            this.button7.Text = "Quit";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.LimeGreen;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.Location = new System.Drawing.Point(362, 299);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(65, 26);
            this.button4.TabIndex = 50;
            this.button4.Text = "ON";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(360, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 12);
            this.label13.TabIndex = 49;
            this.label13.Text = "999";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(366, 242);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 12);
            this.label14.TabIndex = 48;
            this.label14.Text = "0";
            // 
            // tbLight4
            // 
            this.tbLight4.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbLight4.LargeChange = 10;
            this.tbLight4.Location = new System.Drawing.Point(384, 69);
            this.tbLight4.Maximum = 999;
            this.tbLight4.Name = "tbLight4";
            this.tbLight4.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbLight4.Size = new System.Drawing.Size(45, 193);
            this.tbLight4.TabIndex = 46;
            this.tbLight4.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbLight4.ValueChanged += new System.EventHandler(this.tbLight4_ValueChanged);
            // 
            // numLight4
            // 
            this.numLight4.Location = new System.Drawing.Point(362, 268);
            this.numLight4.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numLight4.Name = "numLight4";
            this.numLight4.Size = new System.Drawing.Size(64, 21);
            this.numLight4.TabIndex = 47;
            this.numLight4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numLight4.ValueChanged += new System.EventHandler(this.numLight4_ValueChanged);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LimeGreen;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.Location = new System.Drawing.Point(252, 299);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(65, 26);
            this.button3.TabIndex = 45;
            this.button3.Text = "ON";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(250, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 12);
            this.label10.TabIndex = 44;
            this.label10.Text = "999";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(256, 242);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 43;
            this.label11.Text = "0";
            // 
            // tbLight3
            // 
            this.tbLight3.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbLight3.LargeChange = 10;
            this.tbLight3.Location = new System.Drawing.Point(274, 69);
            this.tbLight3.Maximum = 999;
            this.tbLight3.Name = "tbLight3";
            this.tbLight3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbLight3.Size = new System.Drawing.Size(45, 193);
            this.tbLight3.TabIndex = 41;
            this.tbLight3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbLight3.Scroll += new System.EventHandler(this.tbLight3_Scroll);
            this.tbLight3.ValueChanged += new System.EventHandler(this.tbLight3_ValueChanged);
            // 
            // numLight3
            // 
            this.numLight3.Location = new System.Drawing.Point(252, 268);
            this.numLight3.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numLight3.Name = "numLight3";
            this.numLight3.Size = new System.Drawing.Size(64, 21);
            this.numLight3.TabIndex = 42;
            this.numLight3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numLight3.ValueChanged += new System.EventHandler(this.numLight3_ValueChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LimeGreen;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Location = new System.Drawing.Point(142, 298);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 26);
            this.button2.TabIndex = 40;
            this.button2.Text = "ON";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(140, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 39;
            this.label8.Text = "999";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(146, 241);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 38;
            this.label9.Text = "0";
            // 
            // tbLight2
            // 
            this.tbLight2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tbLight2.LargeChange = 10;
            this.tbLight2.Location = new System.Drawing.Point(164, 69);
            this.tbLight2.Maximum = 999;
            this.tbLight2.Name = "tbLight2";
            this.tbLight2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbLight2.Size = new System.Drawing.Size(45, 192);
            this.tbLight2.TabIndex = 36;
            this.tbLight2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbLight2.ValueChanged += new System.EventHandler(this.tbLight2_ValueChanged);
            // 
            // numLight2
            // 
            this.numLight2.Location = new System.Drawing.Point(142, 267);
            this.numLight2.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numLight2.Name = "numLight2";
            this.numLight2.Size = new System.Drawing.Size(64, 21);
            this.numLight2.TabIndex = 37;
            this.numLight2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numLight2.ValueChanged += new System.EventHandler(this.numLight2_ValueChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LimeGreen;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(32, 299);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 26);
            this.button1.TabIndex = 35;
            this.button1.Text = "ON";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 34;
            this.label6.Text = "999";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(366, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 27;
            this.label5.Text = "Channel 4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(259, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "Channel 3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "Channel 2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(32, 471);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(666, 132);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Response Information                 ";
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Turquoise;
            this.button8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button8.Location = new System.Drawing.Point(573, 20);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(54, 36);
            this.button8.TabIndex = 37;
            this.button8.Text = "Clear";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 16);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(645, 109);
            this.textBox1.TabIndex = 0;
            // 
            // _DPM3MiniController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(732, 613);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.串口状态);
            this.MaximizeBox = false;
            this.Name = "_DPM3MiniController";
            this.Text = "3 Generation Mini ControllerV2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this._3代迷你控制器_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.tbLight1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.串口状态.ResumeLayout(false);
            this.串口状态.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLight4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLight3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLight2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbLight1;
        private System.Windows.Forms.ComboBox comBoxPorts;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbState;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numLight1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.GroupBox 串口状态;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TrackBar tbLight4;
        private System.Windows.Forms.NumericUpDown numLight4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar tbLight3;
        private System.Windows.Forms.NumericUpDown numLight3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar tbLight2;
        private System.Windows.Forms.NumericUpDown numLight2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

