namespace HW_204N
{
    partial class _BrighteningStroboscopicController
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
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbLight3 = new System.Windows.Forms.TrackBar();
            this.numLight3 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbLight2 = new System.Windows.Forms.TrackBar();
            this.numLight2 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button8 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.trackBar2_3 = new System.Windows.Forms.TrackBar();
            this.numLight2_3 = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.trackBar2_2 = new System.Windows.Forms.TrackBar();
            this.numLight2_2 = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.trackBar2_1 = new System.Windows.Forms.TrackBar();
            this.label26 = new System.Windows.Forms.Label();
            this.numLight2_1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.tbLight1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.串口状态.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLight3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLight2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight2_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight2_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight2_1)).BeginInit();
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
            this.tbLight1.Location = new System.Drawing.Point(32, 46);
            this.tbLight1.Maximum = 999;
            this.tbLight1.Name = "tbLight1";
            this.tbLight1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbLight1.Size = new System.Drawing.Size(45, 216);
            this.tbLight1.TabIndex = 6;
            this.tbLight1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbLight1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbLight1_MouseUp);
            // 
            // comBoxPorts
            // 
            this.comBoxPorts.FormattingEnabled = true;
            this.comBoxPorts.Location = new System.Drawing.Point(106, 26);
            this.comBoxPorts.Name = "comBoxPorts";
            this.comBoxPorts.Size = new System.Drawing.Size(102, 20);
            this.comBoxPorts.TabIndex = 7;
            this.comBoxPorts.SelectedValueChanged += new System.EventHandler(this.comBoxPorts_SelectedValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(266, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "Status:";
            // 
            // tbState
            // 
            this.tbState.Location = new System.Drawing.Point(319, 26);
            this.tbState.Name = "tbState";
            this.tbState.Size = new System.Drawing.Size(200, 21);
            this.tbState.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 12);
            this.label12.TabIndex = 10;
            this.label12.Text = "Pulse width";
            // 
            // numLight1
            // 
            this.numLight1.Location = new System.Drawing.Point(10, 268);
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
            this.串口状态.Location = new System.Drawing.Point(3, 3);
            this.串口状态.Name = "串口状态";
            this.串口状态.Size = new System.Drawing.Size(666, 64);
            this.串口状态.TabIndex = 34;
            this.串口状态.TabStop = false;
            this.串口状态.Text = "Serial Port Status";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(551, 18);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(76, 34);
            this.button5.TabIndex = 33;
            this.button5.Text = "Connect";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tbLight3);
            this.groupBox1.Controls.Add(this.numLight3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbLight2);
            this.groupBox1.Controls.Add(this.numLight2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbLight1);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.numLight1);
            this.groupBox1.Location = new System.Drawing.Point(3, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 341);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Channel 1";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(276, 276);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 12);
            this.label17.TabIndex = 53;
            this.label17.Text = "ms";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(182, 273);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(17, 12);
            this.label16.TabIndex = 52;
            this.label16.Text = "ms";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Location = new System.Drawing.Point(6, 294);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 40);
            this.panel1.TabIndex = 55;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(139, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(119, 16);
            this.radioButton1.TabIndex = 56;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "External trigger";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(10, 12);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(119, 16);
            this.radioButton2.TabIndex = 57;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Internal trigger";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(80, 273);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 12);
            this.label15.TabIndex = 51;
            this.label15.Text = "us";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(207, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 12);
            this.label10.TabIndex = 44;
            this.label10.Text = "999";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(211, 242);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 43;
            this.label11.Text = "0";
            // 
            // tbLight3
            // 
            this.tbLight3.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbLight3.LargeChange = 10;
            this.tbLight3.Location = new System.Drawing.Point(229, 46);
            this.tbLight3.Maximum = 999;
            this.tbLight3.Name = "tbLight3";
            this.tbLight3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbLight3.Size = new System.Drawing.Size(45, 216);
            this.tbLight3.TabIndex = 41;
            this.tbLight3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbLight3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbLight3_MouseUp);
            // 
            // numLight3
            // 
            this.numLight3.Location = new System.Drawing.Point(207, 268);
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(112, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 39;
            this.label8.Text = "999";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(116, 241);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 38;
            this.label9.Text = "5";
            // 
            // tbLight2
            // 
            this.tbLight2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tbLight2.LargeChange = 10;
            this.tbLight2.Location = new System.Drawing.Point(134, 45);
            this.tbLight2.Maximum = 999;
            this.tbLight2.Minimum = 5;
            this.tbLight2.Name = "tbLight2";
            this.tbLight2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbLight2.Size = new System.Drawing.Size(45, 216);
            this.tbLight2.TabIndex = 36;
            this.tbLight2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbLight2.Value = 5;
            this.tbLight2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbLight2_MouseUp);
            // 
            // numLight2
            // 
            this.numLight2.Location = new System.Drawing.Point(112, 267);
            this.numLight2.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numLight2.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numLight2.Name = "numLight2";
            this.numLight2.Size = new System.Drawing.Size(64, 21);
            this.numLight2.TabIndex = 37;
            this.numLight2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numLight2.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numLight2.ValueChanged += new System.EventHandler(this.numLight2_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 34;
            this.label6.Text = "999";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "Delay";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "Cycle";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(25, 423);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(71, 33);
            this.button6.TabIndex = 53;
            this.button6.Text = "Refresh";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(575, 423);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(71, 33);
            this.button7.TabIndex = 52;
            this.button7.Text = "Quit";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(3, 462);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(666, 132);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Response Information                  ";
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.trackBar2_3);
            this.groupBox3.Controls.Add(this.numLight2_3);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.trackBar2_2);
            this.groupBox3.Controls.Add(this.numLight2_2);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.trackBar2_1);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.numLight2_1);
            this.groupBox3.Location = new System.Drawing.Point(353, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 341);
            this.groupBox3.TabIndex = 54;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Channel 2";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButton3);
            this.panel2.Controls.Add(this.radioButton4);
            this.panel2.Location = new System.Drawing.Point(7, 294);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(270, 40);
            this.panel2.TabIndex = 58;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(143, 12);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(119, 16);
            this.radioButton3.TabIndex = 56;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "External trigger";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(10, 12);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(119, 16);
            this.radioButton4.TabIndex = 57;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Internal trigger";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(276, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 53;
            this.label5.Text = "ms";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(182, 273);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 12);
            this.label13.TabIndex = 52;
            this.label13.Text = "ms";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(80, 273);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 12);
            this.label14.TabIndex = 51;
            this.label14.Text = "us";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(207, 51);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(23, 12);
            this.label18.TabIndex = 44;
            this.label18.Text = "999";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(211, 242);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(11, 12);
            this.label19.TabIndex = 43;
            this.label19.Text = "0";
            // 
            // trackBar2_3
            // 
            this.trackBar2_3.Cursor = System.Windows.Forms.Cursors.Default;
            this.trackBar2_3.LargeChange = 10;
            this.trackBar2_3.Location = new System.Drawing.Point(229, 46);
            this.trackBar2_3.Maximum = 999;
            this.trackBar2_3.Name = "trackBar2_3";
            this.trackBar2_3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar2_3.Size = new System.Drawing.Size(45, 216);
            this.trackBar2_3.TabIndex = 41;
            this.trackBar2_3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar2_3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar2_3_MouseUp);
            // 
            // numLight2_3
            // 
            this.numLight2_3.Location = new System.Drawing.Point(207, 268);
            this.numLight2_3.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numLight2_3.Name = "numLight2_3";
            this.numLight2_3.Size = new System.Drawing.Size(64, 21);
            this.numLight2_3.TabIndex = 42;
            this.numLight2_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numLight2_3.ValueChanged += new System.EventHandler(this.numLight2_3_ValueChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(112, 50);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(23, 12);
            this.label20.TabIndex = 39;
            this.label20.Text = "999";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(116, 241);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(11, 12);
            this.label21.TabIndex = 38;
            this.label21.Text = "5";
            // 
            // trackBar2_2
            // 
            this.trackBar2_2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.trackBar2_2.LargeChange = 10;
            this.trackBar2_2.Location = new System.Drawing.Point(134, 45);
            this.trackBar2_2.Maximum = 999;
            this.trackBar2_2.Minimum = 5;
            this.trackBar2_2.Name = "trackBar2_2";
            this.trackBar2_2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar2_2.Size = new System.Drawing.Size(45, 216);
            this.trackBar2_2.TabIndex = 36;
            this.trackBar2_2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar2_2.Value = 5;
            this.trackBar2_2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar2_2_MouseUp);
            // 
            // numLight2_2
            // 
            this.numLight2_2.Location = new System.Drawing.Point(118, 268);
            this.numLight2_2.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numLight2_2.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numLight2_2.Name = "numLight2_2";
            this.numLight2_2.Size = new System.Drawing.Size(64, 21);
            this.numLight2_2.TabIndex = 37;
            this.numLight2_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numLight2_2.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numLight2_2.ValueChanged += new System.EventHandler(this.numLight2_2_ValueChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(10, 51);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(23, 12);
            this.label22.TabIndex = 34;
            this.label22.Text = "999";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(14, 242);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(11, 12);
            this.label23.TabIndex = 33;
            this.label23.Text = "0";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(223, 31);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(35, 12);
            this.label24.TabIndex = 26;
            this.label24.Text = "Delay";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(128, 30);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(35, 12);
            this.label25.TabIndex = 25;
            this.label25.Text = "Cycle";
            // 
            // trackBar2_1
            // 
            this.trackBar2_1.Cursor = System.Windows.Forms.Cursors.Default;
            this.trackBar2_1.LargeChange = 10;
            this.trackBar2_1.Location = new System.Drawing.Point(32, 46);
            this.trackBar2_1.Maximum = 999;
            this.trackBar2_1.Name = "trackBar2_1";
            this.trackBar2_1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar2_1.Size = new System.Drawing.Size(45, 216);
            this.trackBar2_1.TabIndex = 6;
            this.trackBar2_1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar2_1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar2_1_MouseUp);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(5, 31);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(71, 12);
            this.label26.TabIndex = 10;
            this.label26.Text = "Pulse width";
            // 
            // numLight2_1
            // 
            this.numLight2_1.Location = new System.Drawing.Point(10, 268);
            this.numLight2_1.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numLight2_1.Name = "numLight2_1";
            this.numLight2_1.Size = new System.Drawing.Size(64, 21);
            this.numLight2_1.TabIndex = 24;
            this.numLight2_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numLight2_1.ValueChanged += new System.EventHandler(this.numLight2_1_ValueChanged);
            // 
            // _BrighteningStroboscopicController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(680, 598);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.串口状态);
            this.Name = "_BrighteningStroboscopicController";
            this.Text = "2-Channel Brightening Stroboscopic ControllerV1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.tbLight1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.串口状态.ResumeLayout(false);
            this.串口状态.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLight3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLight2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight2_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight2_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLight2_1)).EndInit();
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar tbLight3;
        private System.Windows.Forms.NumericUpDown numLight3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar tbLight2;
        private System.Windows.Forms.NumericUpDown numLight2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TrackBar trackBar2_3;
        private System.Windows.Forms.NumericUpDown numLight2_3;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TrackBar trackBar2_2;
        private System.Windows.Forms.NumericUpDown numLight2_2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TrackBar trackBar2_1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.NumericUpDown numLight2_1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
    }
}

