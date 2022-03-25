﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using DataGridView_component;
using MySql.Data.MySqlClient;
using tableCheck.Models;
using Ubiety.Dns.Core;

namespace tableCheck
{
	/// <summary>
	/// db1 db2 port username ip password 맞으면 연결
	/// 테이블 dawoon과 dawoon2 비교하기
	/// dawoon 테이블2의 컬럼 갯수가 다르면 컬럼추가하기
	/// 상태에 진행중 진행완료
	/// 진행중 컬럼은 프로그레스바로 보여주기
	/// </summary>


	public partial class Form1 : Form
	{
		//MySQL mysql = new MySQL("localhost", "dawoon", "root", "ekdnsel");
		bool result = false;


		DataTable dt = new DataTable();

		MySqlConnection conn;
		MySqlConnection conn2;

		// 체크박스 추가
		DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
		//버튼 추가
		DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
		//프로그레스 추가
		DataGridViewProgressColumn progressColumn = new DataGridViewProgressColumn();

		private string _HostName = "";    //서버연결방식 도메인
		private string _ServerName = "";  //서버연결방식 아이피
		private string _CONNECT = "";    //도메인인지 아이피인지 여부
		private string _ID = "";              //로그인아이디
		private string _PWD = "";          //패스워드
		private string _PORT = "";          //포트
		private string _DATABASE = "";   //데이터베이스명

		private string _HostName2 = "";    //서버연결방식 도메인
		private string _ServerName2 = "";  //서버연결방식 아이피
		private string _CONNECT2 = "";    //도메인인지 아이피인지 여부
		private string _ID2 = "";              //로그인아이디
		private string _PWD2 = "";          //패스워드
		private string _PORT2 = "";          //포트
		private string _DATABASE2 = "";   //데이터베이스명

		MySqlConnection connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=ekdnsel;Charset=utf8");
		string startupPath = Application.StartupPath + @"\tableCheck.ini";
		string mc = "tableCheck";
		List<info> _infoList = new List<info>();
		public Form1()
		{
			InitializeComponent();
			FormClosed += Form_Closing;
		}

		private void Form_Closing(object sender, FormClosedEventArgs e)
		{
			initCloseMethod();
		}


		#region ini 입력 메소드
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
		#endregion
		class Data
		{
			public int Progress { get; set; }
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			//button2_Click(sender, e);
			dt.Columns.Add("체크", typeof(bool)); // 선택 체크박스 용
			iniload();
			GenerateData();
			checkBoxColumn.HeaderText = "체크";
			checkBoxColumn.Name = "check";
			buttonColumn.HeaderText = "Button";
			buttonColumn.Name = "button";
			progressColumn.HeaderText = "진행중";
			progressColumn.Name = "progress";
			dataGridView1.Columns.Add(checkBoxColumn);
			dataGridView1.Columns.Add("column0", "db");
			dataGridView1.Columns.Add("column1", "테이블명");
			dataGridView1.Columns.Add("column5", "테이블커멘트");
			dataGridView1.Columns.Add("column2", "fildCount1");
			dataGridView1.Columns.Add("column3", "fildCount2");
			dataGridView1.Columns.Add(progressColumn);
			dataGridView1.Columns.Add("column4", "상태");
			dataGridView1.AllowUserToAddRows = false;
			dgvDesign();
			int i = 0;
			dataGridView1.Columns[i++].Width = 50;
			dataGridView1.Columns[i++].Width = 80;
			dataGridView1.Columns[i++].Width = 200;
			dataGridView1.Columns[i++].Width = 200;
			dataGridView1.Columns[i++].Width = 80;
			dataGridView1.Columns[i++].Width = 100;
			dataGridView1.Columns[i++].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns[i++].Width = 100;
			for (i = 0; i < 2; i++)
			{
				dataGridView2.Columns.Add("column0", "이름");
				dataGridView2.Columns.Add("column1", "데이터 유형");
				dataGridView2.Columns.Add("column2", "길이/설정");
				dataGridView2.Columns.Add("column6", "기본값");
				dataGridView2.Columns.Add("column7", "코멘트");
				dataGridView2.Columns.Add("column7", "NULL 허용");
				dataGridView2.Columns.Add("column7", "조합");
			}
			dataGridView2.AllowUserToAddRows = false;
		}
		private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			if (dataGridView1.CurrentCell is DataGridViewCheckBoxCell)
			{
				dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
			}
		}
		private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			// row header 에 자동 일련번호 넣기
			StringFormat drawFormat = new StringFormat();
			//drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
			drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

			using (Brush brush = new SolidBrush(Color.Red))
			{
				e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, brush, e.RowBounds.Location.X + 35, e.RowBounds.Location.Y + 4, drawFormat);
			}
		}
		private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{// row header 에 자동 일련번호 넣기
			StringFormat drawFormat = new StringFormat();
			//drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
			drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
			using (Brush brush = new SolidBrush(Color.Red))
			{
				e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, brush, e.RowBounds.Location.X + 35, e.RowBounds.Location.Y + 4, drawFormat);
			}
		}


		void dgvDesign()
		{
			//마우스로 row header width 조절 못하게 하기.
			dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		}


		private void iniload()
		{
			try
			{
				// ini값을 집어넣을 변수 선언
				StringBuilder db1 = new StringBuilder();
				StringBuilder ip1 = new StringBuilder();
				StringBuilder port1 = new StringBuilder();
				StringBuilder username1 = new StringBuilder();
				StringBuilder password1 = new StringBuilder();

				StringBuilder db2 = new StringBuilder();
				StringBuilder ip2 = new StringBuilder();
				StringBuilder port2 = new StringBuilder();
				StringBuilder username2 = new StringBuilder();
				StringBuilder password2 = new StringBuilder();

				// ini파일에서 데이터를 불러옴
				// GetPrivateProfileString("카테고리", "Key값", "기본값", "저장할 변수", "불러올 경로");
				GetPrivateProfileString(mc, "textBoxDb1", "", db1, 3200, startupPath);
				GetPrivateProfileString(mc, "textBoxIp1", "", ip1, 3200, startupPath);
				GetPrivateProfileString(mc, "textBoxPort1", "", port1, 3200, startupPath);
				GetPrivateProfileString(mc, "textBoxUn1", "", username1, 3200, startupPath);
				GetPrivateProfileString(mc, "textBoxPw1", "", password1, 3200, startupPath);

				GetPrivateProfileString(mc, "textBoxDb2", "", db2, 3200, startupPath);
				GetPrivateProfileString(mc, "textBoxIp2", "", ip2, 3200, startupPath);
				GetPrivateProfileString(mc, "textBoxPort2", "", port2, 3200, startupPath);
				GetPrivateProfileString(mc, "textBoxUn2", "", username2, 3200, startupPath);
				GetPrivateProfileString(mc, "textBoxPw2", "", password2, 3200, startupPath);

				// 텍스트박스에 ini파일에서 가져온 데이터를 넣는다
				textBoxDb1.Text = db1.ToString();
				textBoxIp1.Text = ip1.ToString();
				textBoxPort1.Text = port1.ToString();
				textBoxUn1.Text = username1.ToString();
				textBoxPw1.Text = password1.ToString();

				textBoxDb2.Text = db2.ToString();
				textBoxIp2.Text = ip2.ToString();
				textBoxPort2.Text = port2.ToString();
				textBoxUn2.Text = username2.ToString();
				textBoxPw2.Text = password2.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());

			}

		}
		private void initCloseMethod()
		{
			// ini파일에 등록
			// WritePrivateProfileString("카테고리", "Key값", "Value", "저장할 경로");
			WritePrivateProfileString(mc, "textBoxDb1", textBoxDb1.Text, startupPath);
			WritePrivateProfileString(mc, "textBoxIp1", textBoxIp1.Text, startupPath);
			WritePrivateProfileString(mc, "textBoxPort1", textBoxPort1.Text, startupPath);
			WritePrivateProfileString(mc, "textBoxUn1", textBoxUn1.Text, startupPath);
			WritePrivateProfileString(mc, "textBoxPw1", textBoxPw1.Text, startupPath);

			WritePrivateProfileString(mc, "textBoxDb2", textBoxDb2.Text, startupPath);
			WritePrivateProfileString(mc, "textBoxIp2", textBoxIp2.Text, startupPath);
			WritePrivateProfileString(mc, "textBoxPort2", textBoxPort2.Text, startupPath);
			WritePrivateProfileString(mc, "textBoxUn2", textBoxUn2.Text, startupPath);
			WritePrivateProfileString(mc, "textBoxPw2", textBoxPw2.Text, startupPath);
		}
		void GenerateData()
		{
			Random ran = new Random();
			for (int i = 0; i < 10; i++)
			{
				//users.Add(new info { check = false, tableName = i.ToString(), fieldNumber = i, Progress = i, status = "" });
			}
		}

		private void buttonConnect_Click(object sender, EventArgs e)
		{
			if (textBoxIp1.Text == "")
			{
				MessageBox.Show("데이터베이스 서버를 입력해주세요.");
				textBoxIp1.Focus();
				return;
			}
			if (textBoxPort1.Text == "")
			{
				MessageBox.Show("Port번호를 입력해 주십시오.");
				textBoxPort1.Focus();
				return;
			}
			if (textBoxDb1.Text == "")
			{
				MessageBox.Show("Database를 입력해 주십시오.");
				textBoxDb1.Focus();
				return;
			}
			if (textBoxUn1.Text == "")
			{
				MessageBox.Show("ID를 입력해 주십시오.");
				textBoxUn1.Focus();
				return;
			}
			if (textBoxPw1.Text == "")
			{
				MessageBox.Show("Password를 입력해 주십시오.");
				textBoxPw1.Focus();
				return;
			}
			btn_TEST_Click(sender, e);
			//btn_TEST_Click2(sender, e);

		}

		private void btn_TEST_Click2(object sender, EventArgs e)
		{

		}

		class ListInfo
		{
			public string tableName;
			public int fieldCount;
			public string tableCmt;
		}

		class ListInfoAll
		{
			public string db;
			public string tableName;
			public int fieldCount1;
			public int fieldCount2;

			public string tableCmt;
		}
		class Columns
		{
			public string COLUMN_NAME; //컬럼명
			public string DATA_TYPE; //데이터 타입
			public string CHARACTER_MAXIMUM_LENGTH; //길이설정 
			public string COLUMN_DEFAULT; //기본값
			public string COLUMN_COMMENT; //코멘트

			public string IS_NULLABLE; //NULL 허용
			public string COLLATION_NAME; //조합
		}

		class ColumnsAll
		{
			public string TABLE_SCHEMA; //데이터베이스
			public string TABLE; //테이블명

			public string COLUMN_NAME1; //컬럼명
			public string COLUMN_NAME2; //컬럼명    

			public string DATA_TYPE1; //데이터 타입1
			public string DATA_TYPE2; //데이터 타입2

			public string CHARACTER_MAXIMUM_LENGTH1; //길이설정1
			public string CHARACTER_MAXIMUM_LENGTH2; //길이설정2

			public string COLUMN_DEFAULT1; //기본값1
			public string COLUMN_DEFAULT2; //기본값2
			public string COLUMN_COMMENT1; //코멘트1
			public string COLUMN_COMMENT2; //코멘트2

			public string IS_NULLABLE1; //NULL 허용1
			public string IS_NULLABLE2; //NULL 허용1
			public string COLLATION_NAME1; //조합
			public string COLLATION_NAME2; //조합
		}



		//연결테스트 버튼 클릭
		private void btn_TEST_Click(object sender, EventArgs e)
		{

			_HostName = textBoxIp1.Text;
			_PORT = textBoxPort1.Text;
			_DATABASE = textBoxDb1.Text;
			_ID = textBoxUn1.Text;
			_PWD = textBoxPw1.Text;

			_HostName2 = textBoxIp2.Text;
			_PORT2 = textBoxPort2.Text;
			_DATABASE2 = textBoxDb2.Text;
			_ID2 = textBoxUn2.Text;
			_PWD2 = textBoxPw2.Text;
			string query1 = "SELECT b.table_name tbl, a.table_comment cmt, COUNT(*) cnt  FROM information_schema.tables a left JOIN information_schema.columns b ON a.TABLE_NAME=b.table_name WHERE a.table_schema = '" + _DATABASE + "' AND b.table_schema = '" + _DATABASE + "' group BY b.TABLE_NAME ORDER BY b.TABLE_NAME asc;";
			string query2 = "SELECT b.table_name tbl, a.table_comment cmt, COUNT(*) cnt  FROM information_schema.tables a left JOIN information_schema.columns b ON a.TABLE_NAME=b.table_name WHERE a.table_schema = '" + _DATABASE2 + "' AND b.table_schema = '" + _DATABASE2 + "' group BY b.TABLE_NAME ORDER BY b.TABLE_NAME asc;";


			MySqlDataReader rdr = DBConnectTest(conn, _HostName, _PORT, _DATABASE, _ID, _PWD, query1);
			List<ListInfo> listTable1 = new List<ListInfo>();
			List<ListInfo> listTable2 = new List<ListInfo>();
			List<ListInfoAll> listTableAll = new List<ListInfoAll>();
			while (rdr.Read())
			{
				ListInfo listInfo = new ListInfo() { tableName = rdr["tbl"].ToString(), fieldCount = Convert.ToInt32(rdr["cnt"].ToString()), tableCmt = rdr["cmt"].ToString() };
				listTable1.Add(listInfo);

				// dataGridView1.Rows[i].Cells[1].Value = rdr["cnt"].ToString();
			}

			MySqlDataReader rdr2 = DBConnectTest(conn2, _HostName2, _PORT2, _DATABASE2, _ID2, _PWD2, query2);
			while (rdr2.Read())
			{
				ListInfo listInfo = new ListInfo() { tableName = rdr2["tbl"].ToString(), fieldCount = Convert.ToInt32(rdr2["cnt"].ToString()), tableCmt = rdr["cmt"].ToString() };
				listTable2.Add(listInfo);
			}


			for (int i = 0; i < listTable1.Count; i++)
			{
				listTableAll.Add(new ListInfoAll() { db = "A", tableName = listTable1[i].tableName, fieldCount1 = listTable1[i].fieldCount, fieldCount2 = 0, tableCmt = listTable1[i].tableCmt });
			}
			for (int i = 0; i < listTable2.Count; i++)
			{
				bool found = false;
				for (int j = 0; j < listTableAll.Count; j++)
				{
					if (listTable2[i].tableName == listTableAll[j].tableName)
					{
						listTableAll[j].fieldCount2 = listTable2[i].fieldCount;
						found = true;
						listTableAll[j].db = "A+B";
						break;
					}

				}
				//listTableAll.Add(new ListInfoAll() { db = "A", tableName = listTable1[i].tableName, fieldCount1 = listTable1[i].fieldCount, fieldCount2 = 0 });
				if (found == false)
				{
					listTableAll.Add(new ListInfoAll() { db = "B", tableName = listTable2[i].tableName, fieldCount1 = 0, fieldCount2 = listTable2[i].fieldCount, tableCmt = listTable2[i].tableCmt });
				}

			}
			dataGridView1.Rows.Clear();
			for (int i = 0; i < listTableAll.Count; i++)
			{
				dataGridView1.Rows.Add(1);
				dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = true;
				if (listTableAll[i].db == "B")
				{
					dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = false;

				}
				dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = listTableAll[i].db;
				dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = listTableAll[i].tableName;
				dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = listTableAll[i].tableCmt;
				dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = listTableAll[i].fieldCount1;
				dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[5].Value = listTableAll[i].fieldCount2;
			}



		}

		//DBConnectTest 메소드
		private MySqlDataReader DBConnectTest(MySqlConnection con, string hostname, string port, string database, string id, string pwd, string query)
		{
			StringBuilder _strArg = new StringBuilder("");
			MySqlDataReader rdr;
			MySqlDataReader rdr3;
			_strArg.Append("Server = ");           // SQL
			_strArg.Append(hostname);        // 서버
			_strArg.Append(";Port = ");
			_strArg.Append(port);                 // 포트
			_strArg.Append(";Database = ");
			_strArg.Append(database);          // 데이터베이스
			_strArg.Append(";username = ");
			_strArg.Append(id);                     // ID
			_strArg.Append(";password = ");
			_strArg.Append(pwd);                 // PWD
			_strArg.Append(";");

			con = new MySqlConnection(_strArg.ToString());
			try
			{
				con.Open();
				string sql = query;
				MySqlCommand cmd = new MySqlCommand(sql, con);
				rdr = cmd.ExecuteReader();
				return rdr;
			}
			catch (Exception Ex)
			{
				return null;
				con.Close();
				MessageBox.Show("DB 접속이 불가능합니다.");
				LogMgr.ExceptionLog(Ex);
			}
		}


		//DBConnectTest 메소드
		//데이터베이스와 테이블인자를 받아서 컬럼테이블에 나타내기
		private MySqlDataReader DBConnectTest2(MySqlConnection con, string hostname, string port, string database, string id, string pwd, string tableName)
		{
			StringBuilder _strArg = new StringBuilder("");
			MySqlDataReader rdr;
			MySqlDataReader rdr3;
			_strArg.Append("Server = ");           // SQL
			_strArg.Append(hostname);        // 서버
			_strArg.Append(";Port = ");
			_strArg.Append(port);                 // 포트
			_strArg.Append(";Database = ");
			_strArg.Append(database);          // 데이터베이스
			_strArg.Append(";username = ");
			_strArg.Append(id);                     // ID
			_strArg.Append(";password = ");
			_strArg.Append(pwd);                 // PWD
			_strArg.Append(";");

			con = new MySqlConnection(_strArg.ToString());
			try
			{
				con.Open();
				string sql2 = "SELECT COLUMN_NAME fieldName, COLUMN_TYPE dataType, CHARACTER_MAXIMUM_LENGTH length, COLUMN_DEFAULT default1, COLUMN_COMMENT comment, IS_NULLABLE nullable, COLLATION_NAME colName FROM information_schema.columns WHERE table_schema= '" + database + "' and TABLE_NAME = '" + tableName + "' ORDER BY TABLE_NAME asc";
				MySqlCommand cmd = new MySqlCommand(sql2, con);
				rdr = cmd.ExecuteReader();
				return rdr;
			}
			catch (Exception Ex)
			{
				return null;
				con.Close();
				MessageBox.Show(Ex.ToString());
				//MessageBox.Show("DB 접속이 불가능합니다.");
				//isTested = false;
			}
		}

		private void buttonClear_Click(object sender, EventArgs e)
		{
			dataGridView1.Rows.Clear();
			dataGridView2.Rows.Clear();

		}

		private void CheckBox_All_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < dataGridView1.Rows.Count; i++)
				{
					dataGridView1.Rows[i].Cells[0].Value = true;
				}
			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < dataGridView1.Rows.Count; i++)
				{
					dataGridView1.Rows[i].Cells[0].Value = !Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
				}
			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}

		private void checkBoxCancel_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < dataGridView1.Rows.Count; i++)
				{
					dataGridView1.Rows[i].Cells[0].Value = false;
				}
			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;
			string tbl = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
			if (tbl == null) return;
			showFields(tbl);
		}
		void showFields(string tableName)
		{
			try
			{
				// 테이블명의 필드를 가져와서 데이터그리드뷰2에 보여준다
				//MessageBox.Show(tableName);
				MySqlDataReader rdr = DBConnectTest2(conn, _HostName, _PORT, _DATABASE, _ID, _PWD, tableName);
				List<Columns> listTable1 = new List<Columns>();
				List<Columns> listTable2 = new List<Columns>();
				List<ColumnsAll> listTableAll = new List<ColumnsAll>();
				while (rdr.Read())
				{
					Columns listInfo = new Columns() { COLUMN_NAME = rdr["fieldName"].ToString(), DATA_TYPE = rdr["dataType"].ToString(), CHARACTER_MAXIMUM_LENGTH = rdr["length"].ToString(), COLUMN_DEFAULT = rdr["default1"].ToString(), COLUMN_COMMENT = rdr["comment"].ToString(), IS_NULLABLE = rdr["nullable"].ToString(), COLLATION_NAME = rdr["colName"].ToString() };
					listTable1.Add(listInfo);
				}

				MySqlDataReader rdr2 = DBConnectTest2(conn2, _HostName2, _PORT2, _DATABASE2, _ID2, _PWD2, tableName);


				while (rdr2.Read())
				{
					Columns listInfo = new Columns() { COLUMN_NAME = rdr2["fieldName"].ToString(), DATA_TYPE = rdr2["dataType"].ToString(), CHARACTER_MAXIMUM_LENGTH = rdr2["length"].ToString(), COLUMN_DEFAULT = rdr2["default1"].ToString(), COLUMN_COMMENT = rdr2["comment"].ToString(), IS_NULLABLE = rdr2["nullable"].ToString(), COLLATION_NAME = rdr2["colName"].ToString() };
					listTable2.Add(listInfo);
				}

				for (int i = 0; i < listTable1.Count; i++)
				{
					listTableAll.Add(new ColumnsAll()
					{
						COLUMN_NAME1 = listTable1[i].COLUMN_NAME,
						DATA_TYPE1 = listTable1[i].DATA_TYPE,
						CHARACTER_MAXIMUM_LENGTH1 = listTable1[i].CHARACTER_MAXIMUM_LENGTH,
						COLUMN_DEFAULT1 = listTable1[i].COLUMN_DEFAULT,
						COLUMN_COMMENT1 = listTable1[i].COLUMN_COMMENT,
						IS_NULLABLE1 = listTable1[i].IS_NULLABLE,
						COLLATION_NAME1 = listTable1[i].COLLATION_NAME
					});

				}
				for (int i = 0; i < listTable2.Count; i++)
				{
					bool found = false;
					for (int j = 0; j < listTableAll.Count; j++)
					{
						//for (int i = 0; i < listTable2.Count; i++)
						//{
						//  for (int j = 0; j < listTableAll.Count; j++)
						//  {
						if (listTable2[i].COLUMN_NAME == listTableAll[j].COLUMN_NAME1)
						{
							listTableAll[j].COLUMN_NAME2 = listTable2[i].COLUMN_NAME;
							listTableAll[j].DATA_TYPE2 = listTable2[i].DATA_TYPE;
							listTableAll[j].CHARACTER_MAXIMUM_LENGTH2 = listTable2[i].CHARACTER_MAXIMUM_LENGTH;
							listTableAll[j].COLUMN_DEFAULT2 = listTable2[i].COLUMN_DEFAULT;
							listTableAll[j].COLUMN_COMMENT2 = listTable2[i].COLUMN_COMMENT;

							listTableAll[j].IS_NULLABLE2 = listTable2[i].IS_NULLABLE;
							listTableAll[j].COLLATION_NAME2 = listTable2[i].COLLATION_NAME;
							found = true;
							listTableAll[j].TABLE = "A+B";
							break;
						}
					}
					//listTableAll.Add(new ListInfoAll() { db = "A", tableName = listTable1[i].tableName, fieldCount1 = listTable1[i].fieldCount, fieldCount2 = 0 });
					if (found == false)
					{
						listTableAll.Add(new ColumnsAll()
						{
							COLUMN_NAME2 = listTable2[i].COLUMN_NAME,
							DATA_TYPE2 = listTable2[i].DATA_TYPE,
							CHARACTER_MAXIMUM_LENGTH2 = listTable2[i].CHARACTER_MAXIMUM_LENGTH,
							COLUMN_DEFAULT2 = listTable2[i].COLUMN_DEFAULT,
							COLUMN_COMMENT2 = listTable2[i].COLUMN_COMMENT,

							IS_NULLABLE2 = listTable2[i].IS_NULLABLE,
							COLLATION_NAME2 = listTable2[i].COLLATION_NAME
						});
					}
				}


				dataGridView2.Rows.Clear();
				for (int i = 0; i < listTableAll.Count; i++)
				{
					dataGridView2.Rows.Add(1);
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Value = listTableAll[i].COLUMN_NAME1;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[1].Value = listTableAll[i].DATA_TYPE1;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2].Value = listTableAll[i].CHARACTER_MAXIMUM_LENGTH1;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = listTableAll[i].COLUMN_DEFAULT1;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Value = listTableAll[i].COLUMN_COMMENT1;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[5].Value = listTableAll[i].IS_NULLABLE1;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[6].Value = listTableAll[i].COLLATION_NAME1;

					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[7].Value = listTableAll[i].COLUMN_NAME2;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[8].Value = listTableAll[i].DATA_TYPE2;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[9].Value = listTableAll[i].CHARACTER_MAXIMUM_LENGTH2;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[10].Value = listTableAll[i].COLUMN_DEFAULT2;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[11].Value = listTableAll[i].COLUMN_COMMENT2;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[12].Value = listTableAll[i].IS_NULLABLE2;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[13].Value = listTableAll[i].COLLATION_NAME2;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				LogMgr.ExceptionLog(ex);
			}


		}
		void runFields(string tableName, string fieldName, string fieldType, string nullable, string tableComment, string collate)
		{
			//실행버튼 이벤트 실행
			string query5 = "SELECT EXISTS (SELECT 1 FROM Information_schema.tables WHERE table_schema = 'dawoon' AND table_name = '" + tableName + "')" + " AS exist";
			//string query5 = "CREATE TABLE" + "`" + tableName + "`" + "(`" + fieldName + "` " + fieldType + " " + nullable  + "," + "PRIMARY KEY (`" + dataGridView2.Rows[0].Cells[0].Value.ToString() + "`)" + "COMMENT='" + tableComment + "' COLLATE='"+collate+"' ENGINE=InnoDB;" ;


			MySqlDataReader rdr = DBConnectTest(conn, _HostName, _PORT, _DATABASE, _ID, _PWD, query5);

			while (rdr.Read())
			{
				if (rdr["exist"].ToString() == "0")
				{
					MessageBox.Show("없다");
				}
			}
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < dataGridView2.Rows.Count; i++)
			{
				//	string tbl = dataGridView1.Rows[i].Cells[2].ToString();
				//	if (tbl == null) return;
				//	showFields(tbl);

				int rowIndex = dataGridView1.CurrentCell.RowIndex;
				if (rowIndex < 0) return;
				if (dataGridView1.Rows[rowIndex].Cells[0].Value == null)
				{
					return;
				}
				string tbl = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
				if (tbl == null) return;
				//

				string tableComment = dataGridView1.Rows[i].Cells[3].Value.ToString();
				if (tableComment == null) return;
				//
				string fdl = dataGridView2.Rows[i].Cells[0].Value.ToString();
				if (fdl == null) return;

				string columnType = dataGridView2.Rows[i].Cells[1].Value.ToString();
				if (columnType == null) return;


				string columnComment = dataGridView2.Rows[i].Cells[3].Value.ToString();
				if (columnComment == null) return;

				string utf8 = dataGridView2.Rows[i].Cells[6].Value.ToString();
				if (utf8 == null) return;

				string def = dataGridView2.Rows[i].Cells[3].Value.ToString();
				if (def == null) return;

				string nullable = dataGridView2.Rows[i].Cells[5].Value.ToString();
				if (nullable == null) return;
				if (nullable == "NO") nullable = "NOT NULL";
				else if (nullable == "YES") nullable = " NULL";




				string queryCreate = "CREATE TABLE `" + tbl
					+ "` (`" + fdl + "` " + columnType + " " + nullable
					+ ")"
					+ " COMMENT = '" + tableComment + "'" + " ENGINE = InnoDB;";
				Create(queryCreate);
			}
		}

		//string ftype = dataGridView2.Rows[i].Cells[1].ToString();
		//string nullable = dataGridView2.Rows[i].Cells[5].ToString();
		//string comment = dataGridView2.Rows[i].Cells[4].ToString();
		//string tableComment = dataGridView1.Rows[i].Cells[3].ToString();
		//string COLLATE = dataGridView2.Rows[i].Cells[6].ToString();
		//if (nullable == "NO")
		//{
		//	nullable = "NOT NULL " + comment + " ";
		//}
		//else if (nullable == "YES") nullable = " NULL";
		//if (fdl == null) return;
		//if (ftype == null) return;
		//if (nullable == null) return;
		//if (comment == null) return;
		//if (tableComment == null) return;
		//if (COLLATE == null) return;
		//runFields(tbl, fdl, ftype, nullable, tableComment, COLLATE);

		private void Create(string queryCreate)
		{
			try
			{
				MySqlDataReader rdr = DBConnectTest(conn, _HostName, _PORT, _DATABASE2, _ID, _PWD, queryCreate);

			}

			catch (Exception e) { MessageBox.Show(e.ToString()); }
		}

		private void dataGridView1_SelectionChanged(object sender, EventArgs e)
		{
			if (dataGridView1.CurrentCell.ColumnIndex == 2)
			{
				int rowIndex = dataGridView1.CurrentCell.RowIndex;
				if (rowIndex < 0) return;
				if (dataGridView1.Rows[rowIndex].Cells[0].Value == null)
				{
					return;
				}
				string tbl = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
				if (tbl == null) return;
				showFields(tbl);



			}

		}

		private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			for (int i = 7; i < 14; i++)
			{
				dataGridView2.Columns[i].DefaultCellStyle.BackColor = Color.Aqua;

			}

		}

		private void panel4_Paint(object sender, PaintEventArgs e)
		{

		}

		private void buttonOnes_Click(object sender, EventArgs e)
		{
			int rowIndex = dataGridView1.CurrentCell.RowIndex;
			if (rowIndex < 0) return;
			if (dataGridView1.Rows[rowIndex].Cells[0].Value == null)
			{
				return;
			}
			string tbl = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
			if (tbl == null) return;
				string tableComment = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
				if (tableComment == null) return;
			string fields = "";

			string primarykey = "";

			for (int i = 0; i < dataGridView2.Rows.Count; i++)
			{
	
				//
				string fieldName = dataGridView2.Rows[i].Cells[0].Value.ToString();
				if (fieldName == null) return ;

				if (i ==0)
				{
					primarykey = fieldName;
				}

				string columnType = dataGridView2.Rows[i].Cells[1].Value.ToString();
				if (columnType == null) columnType = "";
				else if (columnType.ToUpper().Contains("DATETIME"))
				{
					columnType = " DATETIME ";
				}
				

				string columnComment = dataGridView2.Rows[i].Cells[4].Value.ToString();
				if (columnComment == null) columnComment = "";
				else columnComment = " COMMENT '" + columnComment + "'";

				string utf8 = dataGridView2.Rows[i].Cells[6].Value.ToString();
				if (utf8 == null) utf8 = "";
				else if (utf8 == "") utf8 = "";
				else utf8 = " COLLATE '" + utf8 + "'";

				string def = dataGridView2.Rows[i].Cells[3].Value.ToString();
				if (def == null) def = "";
				if (columnType.ToUpper().Contains("INT") || columnType.ToUpper().Contains("DECIMAL"))
				{
					def = " DEFAULT 0 ";

				}
				else if (columnType.ToUpper().Contains("DATETIME"))
				{
					def = " DEFAULT NULL ";

				}
				else def = " DEFAULT '" + def + "'";


				string nullable = dataGridView2.Rows[i].Cells[5].Value.ToString();
				if (nullable == null) return;
				if (nullable == "NO") nullable = " NOT NULL";
				else if (nullable == "YES") nullable = " NULL";
				if (columnType.ToUpper().Contains("DATETIME"))
				{
					nullable = "";
				}

					//fields = fields + "`" + fieldName + "` " + columnType + " " + nullable + " DEFAULT '" + def + "' COMMENT '" + columnComment + "' ,";

					fields = fields + "`" + fieldName + "` " + columnType + " " + nullable + def +  columnComment + " charset utf8,\n";  
			}
			///
			string queryCreate = "SET NAMES utf8; CREATE TABLE `" + tbl
				+ "` \n(" + fields + " PRIMARY KEY (`" + primarykey + "`) USING BTREE\n"
				+ ")\n ENGINE = InnoDB DEFAULT CHARSET=utf8";
				//+ " COMMENT = '" + tableComment + "'" + " \nENGINE = InnoDB;";
			Create(queryCreate);
		}

	}
}
