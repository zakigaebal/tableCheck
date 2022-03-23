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
			this.panel2 = new System.Windows.Forms.Panel();
			this.buttonConnect = new System.Windows.Forms.Button();
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
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel7 = new System.Windows.Forms.Panel();
			this.splitter3 = new System.Windows.Forms.Splitter();
			this.dataGridView2 = new System.Windows.Forms.DataGridView();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel5);
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
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new System.Drawing.Size(986, 190);
			this.dataGridView1.TabIndex = 4;
			this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
			this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Controls.Add(this.buttonConnect);
			this.panel2.Controls.Add(this.checkBox1);
			this.panel2.Controls.Add(this.checkBox2);
			this.panel2.Controls.Add(this.checkBox3);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(986, 79);
			this.panel2.TabIndex = 10;
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(246, 53);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(39, 23);
			this.buttonConnect.TabIndex = 11;
			this.buttonConnect.Text = "연결";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(21, 12);
			this.label1.TabIndex = 6;
			this.label1.Text = "DB";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(110, 5);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(16, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "IP";
			this.label4.Click += new System.EventHandler(this.label4_Click);
			// 
			// textBoxIp1
			// 
			this.textBoxIp1.Location = new System.Drawing.Point(126, 1);
			this.textBoxIp1.Name = "textBoxIp1";
			this.textBoxIp1.Size = new System.Drawing.Size(70, 21);
			this.textBoxIp1.TabIndex = 7;
			this.textBoxIp1.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(468, 5);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 12);
			this.label7.TabIndex = 6;
			this.label7.Text = "PASSWORD";
			// 
			// textBoxDb1
			// 
			this.textBoxDb1.Location = new System.Drawing.Point(27, 1);
			this.textBoxDb1.Name = "textBoxDb1";
			this.textBoxDb1.Size = new System.Drawing.Size(70, 21);
			this.textBoxDb1.TabIndex = 7;
			this.textBoxDb1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(319, 5);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 12);
			this.label5.TabIndex = 6;
			this.label5.Text = "USERNAME";
			// 
			// textBoxUn1
			// 
			this.textBoxUn1.Location = new System.Drawing.Point(392, 1);
			this.textBoxUn1.Name = "textBoxUn1";
			this.textBoxUn1.Size = new System.Drawing.Size(70, 21);
			this.textBoxUn1.TabIndex = 7;
			this.textBoxUn1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// textBoxPort1
			// 
			this.textBoxPort1.Location = new System.Drawing.Point(244, 1);
			this.textBoxPort1.Name = "textBoxPort1";
			this.textBoxPort1.Size = new System.Drawing.Size(70, 21);
			this.textBoxPort1.TabIndex = 7;
			this.textBoxPort1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// textBoxPw1
			// 
			this.textBoxPw1.Location = new System.Drawing.Point(546, 1);
			this.textBoxPw1.Name = "textBoxPw1";
			this.textBoxPw1.Size = new System.Drawing.Size(70, 21);
			this.textBoxPw1.TabIndex = 7;
			this.textBoxPw1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(206, 5);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "PORT";
			this.label3.Click += new System.EventHandler(this.label3_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(12, 56);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(72, 16);
			this.checkBox1.TabIndex = 9;
			this.checkBox1.Text = "전체선택";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(90, 56);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(72, 16);
			this.checkBox2.TabIndex = 9;
			this.checkBox2.Text = "선택반전";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new System.Drawing.Point(168, 56);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(72, 16);
			this.checkBox3.TabIndex = 9;
			this.checkBox3.Text = "선택취소";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(21, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "DB";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(110, 4);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(16, 12);
			this.label6.TabIndex = 6;
			this.label6.Text = "IP";
			this.label6.Click += new System.EventHandler(this.label4_Click);
			// 
			// textBoxIp2
			// 
			this.textBoxIp2.Location = new System.Drawing.Point(126, 0);
			this.textBoxIp2.Name = "textBoxIp2";
			this.textBoxIp2.Size = new System.Drawing.Size(70, 21);
			this.textBoxIp2.TabIndex = 7;
			this.textBoxIp2.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(474, 7);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(72, 12);
			this.label8.TabIndex = 6;
			this.label8.Text = "PASSWORD";
			this.label8.Click += new System.EventHandler(this.label8_Click);
			// 
			// textBoxDb2
			// 
			this.textBoxDb2.Location = new System.Drawing.Point(27, 1);
			this.textBoxDb2.Name = "textBoxDb2";
			this.textBoxDb2.Size = new System.Drawing.Size(70, 21);
			this.textBoxDb2.TabIndex = 7;
			this.textBoxDb2.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(319, 5);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(73, 12);
			this.label9.TabIndex = 6;
			this.label9.Text = "USERNAME";
			// 
			// textBoxUn2
			// 
			this.textBoxUn2.Location = new System.Drawing.Point(392, 0);
			this.textBoxUn2.Name = "textBoxUn2";
			this.textBoxUn2.Size = new System.Drawing.Size(70, 21);
			this.textBoxUn2.TabIndex = 7;
			this.textBoxUn2.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// textBoxPort2
			// 
			this.textBoxPort2.Location = new System.Drawing.Point(244, 0);
			this.textBoxPort2.Name = "textBoxPort2";
			this.textBoxPort2.Size = new System.Drawing.Size(70, 21);
			this.textBoxPort2.TabIndex = 7;
			this.textBoxPort2.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// textBoxPw2
			// 
			this.textBoxPw2.Location = new System.Drawing.Point(546, 2);
			this.textBoxPw2.Name = "textBoxPw2";
			this.textBoxPw2.Size = new System.Drawing.Size(70, 21);
			this.textBoxPw2.TabIndex = 7;
			this.textBoxPw2.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(206, 5);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(38, 12);
			this.label10.TabIndex = 6;
			this.label10.Text = "PORT";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label1);
			this.panel3.Controls.Add(this.textBoxDb1);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Controls.Add(this.textBoxIp1);
			this.panel3.Controls.Add(this.textBoxPw1);
			this.panel3.Controls.Add(this.label7);
			this.panel3.Controls.Add(this.textBoxPort1);
			this.panel3.Controls.Add(this.textBoxUn1);
			this.panel3.Controls.Add(this.label5);
			this.panel3.Location = new System.Drawing.Point(12, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(626, 24);
			this.panel3.TabIndex = 15;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.label2);
			this.panel4.Controls.Add(this.textBoxDb2);
			this.panel4.Controls.Add(this.label6);
			this.panel4.Controls.Add(this.label10);
			this.panel4.Controls.Add(this.textBoxIp2);
			this.panel4.Controls.Add(this.textBoxPw2);
			this.panel4.Controls.Add(this.label8);
			this.panel4.Controls.Add(this.textBoxPort2);
			this.panel4.Controls.Add(this.textBoxUn2);
			this.panel4.Controls.Add(this.label9);
			this.panel4.Location = new System.Drawing.Point(12, 28);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(626, 25);
			this.panel4.TabIndex = 16;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.panel7);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(0, 79);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(986, 405);
			this.panel5.TabIndex = 17;
			// 
			// panel7
			// 
			this.panel7.Controls.Add(this.dataGridView2);
			this.panel7.Controls.Add(this.splitter3);
			this.panel7.Controls.Add(this.dataGridView1);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel7.Location = new System.Drawing.Point(0, 0);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(986, 405);
			this.panel7.TabIndex = 0;
			// 
			// splitter3
			// 
			this.splitter3.BackColor = System.Drawing.Color.MistyRose;
			this.splitter3.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter3.Location = new System.Drawing.Point(0, 190);
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new System.Drawing.Size(986, 5);
			this.splitter3.TabIndex = 0;
			this.splitter3.TabStop = false;
			// 
			// dataGridView2
			// 
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView2.Location = new System.Drawing.Point(0, 195);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.RowTemplate.Height = 23;
			this.dataGridView2.Size = new System.Drawing.Size(986, 210);
			this.dataGridView2.TabIndex = 15;
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
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel7.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGridView dataGridView1;
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
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
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
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.DataGridView dataGridView2;
		private System.Windows.Forms.Splitter splitter3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel3;
	}
}

