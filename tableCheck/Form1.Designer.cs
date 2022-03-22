namespace tableCheck
{
	partial class Form1
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel2 = new System.Windows.Forms.Panel();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.dataGridView2 = new System.Windows.Forms.DataGridView();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxIp1 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxDb1 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxUn1 = new System.Windows.Forms.TextBox();
			this.textBoxPort1 = new System.Windows.Forms.TextBox();
			this.textBoxPw1 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxIp2 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBoxDb2 = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxUn2 = new System.Windows.Forms.TextBox();
			this.textBoxPort2 = new System.Windows.Forms.TextBox();
			this.textBoxPw2 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.Column1 = new DataGridView_component.DataGridViewProgressColumn();
			this.dataGridView3 = new System.Windows.Forms.DataGridView();
			this.Column2 = new DataGridView_component.DataGridViewProgressColumn();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.dataGridView1);
			this.panel1.Controls.Add(this.splitter1);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(986, 484);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 180);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new System.Drawing.Size(986, 304);
			this.dataGridView1.TabIndex = 4;
			this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
			this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
			// 
			// splitter1
			// 
			this.splitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 169);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(986, 11);
			this.splitter1.TabIndex = 5;
			this.splitter1.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.dataGridView3);
			this.panel2.Controls.Add(this.button2);
			this.panel2.Controls.Add(this.buttonConnect);
			this.panel2.Controls.Add(this.dataGridView2);
			this.panel2.Controls.Add(this.button1);
			this.panel2.Controls.Add(this.groupBox1);
			this.panel2.Controls.Add(this.checkBox1);
			this.panel2.Controls.Add(this.checkBox2);
			this.panel2.Controls.Add(this.checkBox3);
			this.panel2.Controls.Add(this.groupBox2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(986, 169);
			this.panel2.TabIndex = 10;
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(360, 12);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(41, 23);
			this.buttonConnect.TabIndex = 11;
			this.buttonConnect.Text = "연결";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// dataGridView2
			// 
			this.dataGridView2.AllowUserToAddRows = false;
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
			this.dataGridView2.Location = new System.Drawing.Point(436, 11);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.RowTemplate.Height = 23;
			this.dataGridView2.Size = new System.Drawing.Size(198, 109);
			this.dataGridView2.TabIndex = 10;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(314, 145);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(40, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "체크";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textBoxIp1);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textBoxDb1);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textBoxUn1);
			this.groupBox1.Controls.Add(this.textBoxPort1);
			this.groupBox1.Controls.Add(this.textBoxPw1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(12, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(168, 145);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "DB1";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(68, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(21, 12);
			this.label1.TabIndex = 6;
			this.label1.Text = "DB";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(73, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(16, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "IP";
			this.label4.Click += new System.EventHandler(this.label4_Click);
			// 
			// textBoxIp1
			// 
			this.textBoxIp1.Location = new System.Drawing.Point(89, 52);
			this.textBoxIp1.Name = "textBoxIp1";
			this.textBoxIp1.Size = new System.Drawing.Size(70, 21);
			this.textBoxIp1.TabIndex = 7;
			this.textBoxIp1.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(17, 123);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 12);
			this.label7.TabIndex = 6;
			this.label7.Text = "PASSWORD";
			// 
			// textBoxDb1
			// 
			this.textBoxDb1.Location = new System.Drawing.Point(89, 30);
			this.textBoxDb1.Name = "textBoxDb1";
			this.textBoxDb1.Size = new System.Drawing.Size(70, 21);
			this.textBoxDb1.TabIndex = 7;
			this.textBoxDb1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 101);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 12);
			this.label5.TabIndex = 6;
			this.label5.Text = "USERNAME";
			// 
			// textBoxUn1
			// 
			this.textBoxUn1.Location = new System.Drawing.Point(89, 96);
			this.textBoxUn1.Name = "textBoxUn1";
			this.textBoxUn1.Size = new System.Drawing.Size(70, 21);
			this.textBoxUn1.TabIndex = 7;
			this.textBoxUn1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// textBoxPort1
			// 
			this.textBoxPort1.Location = new System.Drawing.Point(89, 74);
			this.textBoxPort1.Name = "textBoxPort1";
			this.textBoxPort1.Size = new System.Drawing.Size(70, 21);
			this.textBoxPort1.TabIndex = 7;
			this.textBoxPort1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// textBoxPw1
			// 
			this.textBoxPw1.Location = new System.Drawing.Point(89, 118);
			this.textBoxPw1.Name = "textBoxPw1";
			this.textBoxPw1.Size = new System.Drawing.Size(70, 21);
			this.textBoxPw1.TabIndex = 7;
			this.textBoxPw1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(51, 79);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "PORT";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(12, 148);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(72, 16);
			this.checkBox1.TabIndex = 9;
			this.checkBox1.Text = "전체선택";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(90, 148);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(72, 16);
			this.checkBox2.TabIndex = 9;
			this.checkBox2.Text = "선택반전";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new System.Drawing.Point(168, 148);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(72, 16);
			this.checkBox3.TabIndex = 9;
			this.checkBox3.Text = "선택취소";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.textBoxIp2);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.textBoxDb2);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.textBoxUn2);
			this.groupBox2.Controls.Add(this.textBoxPort2);
			this.groupBox2.Controls.Add(this.textBoxPw2);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Location = new System.Drawing.Point(186, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(168, 145);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "DB2";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(68, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(21, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "DB";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(73, 56);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(16, 12);
			this.label6.TabIndex = 6;
			this.label6.Text = "IP";
			this.label6.Click += new System.EventHandler(this.label4_Click);
			// 
			// textBoxIp2
			// 
			this.textBoxIp2.Location = new System.Drawing.Point(89, 52);
			this.textBoxIp2.Name = "textBoxIp2";
			this.textBoxIp2.Size = new System.Drawing.Size(70, 21);
			this.textBoxIp2.TabIndex = 7;
			this.textBoxIp2.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(17, 123);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(72, 12);
			this.label8.TabIndex = 6;
			this.label8.Text = "PASSWORD";
			// 
			// textBoxDb2
			// 
			this.textBoxDb2.Location = new System.Drawing.Point(89, 30);
			this.textBoxDb2.Name = "textBoxDb2";
			this.textBoxDb2.Size = new System.Drawing.Size(70, 21);
			this.textBoxDb2.TabIndex = 7;
			this.textBoxDb2.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(16, 101);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(73, 12);
			this.label9.TabIndex = 6;
			this.label9.Text = "USERNAME";
			// 
			// textBoxUn2
			// 
			this.textBoxUn2.Location = new System.Drawing.Point(89, 96);
			this.textBoxUn2.Name = "textBoxUn2";
			this.textBoxUn2.Size = new System.Drawing.Size(70, 21);
			this.textBoxUn2.TabIndex = 7;
			this.textBoxUn2.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// textBoxPort2
			// 
			this.textBoxPort2.Location = new System.Drawing.Point(89, 74);
			this.textBoxPort2.Name = "textBoxPort2";
			this.textBoxPort2.Size = new System.Drawing.Size(70, 21);
			this.textBoxPort2.TabIndex = 7;
			this.textBoxPort2.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// textBoxPw2
			// 
			this.textBoxPw2.Location = new System.Drawing.Point(89, 118);
			this.textBoxPw2.Name = "textBoxPw2";
			this.textBoxPw2.Size = new System.Drawing.Size(70, 21);
			this.textBoxPw2.TabIndex = 7;
			this.textBoxPw2.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(51, 79);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(38, 12);
			this.label10.TabIndex = 6;
			this.label10.Text = "PORT";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(399, 127);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 12;
			this.button2.Text = "button2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// Column1
			// 
			this.Column1.HeaderText = "Column1";
			this.Column1.Name = "Column1";
			this.Column1.ProgressBarColor = System.Drawing.Color.Empty;
			// 
			// dataGridView3
			// 
			this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2});
			this.dataGridView3.Location = new System.Drawing.Point(692, 11);
			this.dataGridView3.Name = "dataGridView3";
			this.dataGridView3.RowTemplate.Height = 23;
			this.dataGridView3.Size = new System.Drawing.Size(240, 150);
			this.dataGridView3.TabIndex = 13;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "Column2";
			this.Column2.Name = "Column2";
			this.Column2.ProgressBarColor = System.Drawing.Color.Empty;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(986, 484);
			this.Controls.Add(this.panel1);
			this.Name = "Form1";
			this.Text = "테이블체크";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.TextBox textBoxDb1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxIp1;
		private System.Windows.Forms.TextBox textBoxPort1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxPw1;
		private System.Windows.Forms.TextBox textBoxUn1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxIp2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxDb2;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxUn2;
		private System.Windows.Forms.TextBox textBoxPort2;
		private System.Windows.Forms.TextBox textBoxPw2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.DataGridView dataGridView2;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Button button2;
		private DataGridView_component.DataGridViewProgressColumn Column1;
		private System.Windows.Forms.DataGridView dataGridView3;
		private DataGridView_component.DataGridViewProgressColumn Column2;
	}
}

