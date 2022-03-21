using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace tableCheck
{
	/// <summary>
	/// 테이블 dawoon과 dawoon2 비교하기
	/// dawoon 테이블2의 컬럼 갯수가 다르면 컬럼추가하기
	/// 상태에 진행중 진행완료
	/// 진행중 컬럼은 프로그레스바로 보여주기
	/// </summary>


	public partial class Form1 : Form
	{
			MySqlConnection connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=ekdnsel;Charset=utf8");
		public Form1()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			button1_Click(sender, e);
		}
		//private void connect(string selectQuery, string account)
		//{
		//	selectQuery = "SELECT * FROM dawoon.dc_items where flagYN = 'Y';";
		//	connection.Open();
		//	MySqlCommand cmd = new MySqlCommand(selectQuery, connection);
		//	MySqlDataReader reader = cmd.ExecuteReader();
		//	while (reader.Read())
		//	{
		//	}
		//	connection.Close();
		//}

		//		QuerySearch = "SELECT EXISTS ( SELECT 1 FROM Information_schema.tables WHERE table_schema = 'dawoon' AND TABLE_NAME = 'dc_account') AS 테이블확인";
		private void button1_Click(object sender, EventArgs e)
		{
			using (MySqlConnection connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=ekdnsel;Charset=utf8"))
			{
				try//예외 처리
				{
					connection.Open();
					string strSql = "SHOW TABLES IN dawoon;";
					MySqlCommand cmd = new MySqlCommand(strSql, connection);

					MySqlDataAdapter da = new MySqlDataAdapter(cmd);
					//  MySqlCommandBuilder _bd = new MySqlCommandBuilder(da);
					DataTable dt = new DataTable();
					da.Fill(dt);

			


					dataGridView1.DataSource = dt;
					connection.Close();


					dataGridView1.ReadOnly = true;
					dataGridView1.RowHeadersVisible = false;
					//dataGridViewMessage.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					//dataGridViewMessage.Columns[dataGridViewMessage.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
					//dataGridViewMessage.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
					dataGridView1.Columns[0].Width = 280;
					dt.Columns.Add("필드수");
					dt.Columns.Add("progressbar");
					dt.Columns.Add("상태");

					connection.Open();
					for (int i = 0; i < dataGridView1.Rows.Count; i++)
					{
						string fieldSql = "SELECT COUNT(*) cnt	FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + dataGridView1.Rows[i].Cells[0].Value +"';";
						MySqlCommand cmd2 = new MySqlCommand(fieldSql, connection);
						MySqlDataReader rdr = cmd2.ExecuteReader();
						while (rdr.Read())
						{
							dataGridView1.Rows[i].Cells[1].Value = rdr["cnt"].ToString();
						}
						rdr.Close();
					}

					connection.Close();

					//SELECT COUNT(*)	FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'account_book';
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString(),"실패");
				}

			}
		}



		private void progressBar1_Click(object sender, EventArgs e)
		{

		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}

	
	}
}
/*
 * 
 * 
 * 	//ExecuteReader를 이용하여
					//연결 모드로 데이타 가져오기
					MySqlCommand cmd = new MySqlCommand(sql, connection);
					MySqlDataReader table = cmd.ExecuteReader();
					MySqlDataAdapter da = new MySqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);
					dataGridView1.DataSource = dt;
					connection.Close();
					//while (table.Read())
					//{
					//	textBox3.Text = table["table_name"].ToString();
					//	Console.WriteLine(table["table_name"].ToString());
					//}
					//table.Close();
 */