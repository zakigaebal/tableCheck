﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DataGridView_component;
using MySql.Data.MySqlClient;
using tableCheck.Models;

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

		DataTable dt = new DataTable();
		MySqlConnection conn;
		MySqlConnection conn2;

		MySqlConnection con;
		MySqlConnection con2;

		// 체크박스 추가
		DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
		//버튼 추가
		DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
		//프로그레스 추가
		DataGridViewProgressColumn progressColumn = new DataGridViewProgressColumn();

		DataGridViewProgressColumn progressColumn2 = new DataGridViewProgressColumn();
		DataGridViewProgressColumn progressColumn3 = new DataGridViewProgressColumn();
		DataGridViewProgressColumn progressColumn4 = new DataGridViewProgressColumn();
		DataGridViewProgressColumn progressColumn5 = new DataGridViewProgressColumn();

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
			dataGridView1.Columns.Add("column0", "DB");
			dataGridView1.Columns.Add("column1", "테이블명");
			dataGridView1.Columns.Add("column5", "테이블커멘트");
			dataGridView1.Columns.Add("column2", "필드수1");
			dataGridView1.Columns.Add("column3", "필드수2");
			dataGridView1.Columns.Add(progressColumn);
			dataGridView1.Columns.Add("column4", "상태");
			dataGridView1.AllowUserToAddRows = false;

			dgvDesign();
			int i = 0;
			dataGridView1.Columns[i++].Width = 35;
			dataGridView1.Columns[i++].Width = 40;
			dataGridView1.Columns[i++].Width = 200;
			dataGridView1.Columns[i++].Width = 200;
			dataGridView1.Columns[i++].Width = 70;
			dataGridView1.Columns[i++].Width = 70;
			dataGridView1.Columns[i++].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns[i++].Width = 100;


			for (i = 0; i < 2; i++)
			{
				dataGridView2.Columns.Add("column0", "이름");
				dataGridView2.Columns.Add("column1", "유형");
				dataGridView2.Columns.Add("column7", "NULL 허용");
				dataGridView2.Columns.Add("column6", "기본값");
				dataGridView2.Columns.Add("column7", "코멘트");
				dataGridView2.Columns.Add("column7", "조합");
				dataGridView2.Columns.Add("column7", "EXTRA");
				dataGridView2.Columns.Add("column7", "위치");
			}

			dataGridView3.Columns.Add("proName", "프로시저 이름");
			dataGridView3.Columns.Add("proCreate", "생성됨");
			dataGridView3.Columns.Add("proUpdate", "업데이트됨");
			dataGridView3.Columns.Add("proComment", "코멘트");
			dataGridView3.Columns.Add("proQuery", "프로시저 쿼리");
			dataGridView3.Columns.Add("proName2", "프로시저 이름");
			dataGridView3.Columns.Add("proCreate2", "생성됨");
			dataGridView3.Columns.Add("proUpdate2", "업데이트됨");
			dataGridView3.Columns.Add("proComment2", "코멘트");
			dataGridView3.Columns.Add("proQuery2", "프로시저 쿼리");
			dataGridView3.Columns.Add(progressColumn2);
			progressColumn2.HeaderText = "진행중";
			progressColumn2.Name = "progress2";
			dataGridView3.Columns.Add("completed", "상태");
			dataGridView3.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView3.AllowUserToAddRows = false;




			dataGridView5.Columns.Add("proName", "이벤트 이름");
			dataGridView5.Columns.Add("proCreate", "생성됨");
			dataGridView5.Columns.Add("proUpdate", "업데이트됨");
			dataGridView5.Columns.Add("proName", "이벤트 이름");
			dataGridView5.Columns.Add("proCreate", "생성됨");
			dataGridView5.Columns.Add("proUpdate", "업데이트됨");
			dataGridView5.Columns.Add(progressColumn3);
			progressColumn3.HeaderText = "진행중";
			progressColumn3.Name = "progress3";
			dataGridView5.Columns.Add("completed", "상태");
			dataGridView5.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			dataGridView5.AllowUserToAddRows = false;

			dataGridView7.Columns.Add("proName", "함수 이름");
			dataGridView7.Columns.Add("proCreate", "생성됨");
			dataGridView7.Columns.Add("proUpdate", "업데이트됨");
			dataGridView7.Columns.Add("proName", "함수 이름");
			dataGridView7.Columns.Add("proCreate", "생성됨");
			dataGridView7.Columns.Add("proUpdate", "업데이트됨");
			dataGridView7.Columns.Add(progressColumn4);
			progressColumn4.HeaderText = "진행중";
			progressColumn4.Name = "progress4";
			dataGridView7.Columns.Add("completed", "상태");
			dataGridView7.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			dataGridView7.AllowUserToAddRows = false;

			dataGridView9.Columns.Add("proName", "뷰 이름");
			dataGridView9.Columns.Add("proName", "뷰 이름");
			dataGridView9.Columns.Add(progressColumn5);
			progressColumn5.HeaderText = "진행중";
			progressColumn5.Name = "progress5";
			dataGridView9.Columns.Add("completed", "상태");
			dataGridView9.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


			dataGridView9.AllowUserToAddRows = false;



			dataGridView3.Columns[0].Width = 150;
			dataGridView3.Columns[4].Width = 150;
			dataGridView3.Columns[5].Width = 150;
			dataGridView3.Columns[9].Width = 150;

			dataGridView2.AllowUserToAddRows = false;
			dataGridView3.AllowUserToAddRows = false;
			dataGridView5.AllowUserToAddRows = false;
			i = 0;
			dataGridView2.Columns[i++].Width = 60;
			dataGridView2.Columns[i++].Width = 60;
			dataGridView2.Columns[i++].Width = 60;
		}


		private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			try
			{
				if (dataGridView1.CurrentCell is DataGridViewCheckBoxCell)
				{
					dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
				}
			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}

		private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			try
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
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
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
			try
			{
				//마우스로 row header width 조절 못하게 하기.
				dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
				dataGridView1.AllowUserToAddRows = false;
				dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
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
				LogMgr.ExceptionLog(ex);
			}
		}



		private void initCloseMethod()
		{
			try
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
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}



		void GenerateData()
		{
			try
			{
				Random ran = new Random();
				for (int i = 0; i < 10; i++)
				{
					//users.Add(new info { check = false, tableName = i.ToString(), fieldNumber = i, Progress = i, status = "" });
				}
			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}


		private void buttonConnect_Click(object sender, EventArgs e)
		{
			try
			{
				if (textBoxIp1.Text == "")
				{
					MessageBox.Show("데이터베이스 서버를 입력해주세요.");
					textBoxIp1.Focus();
					return;
				}
				else if (textBoxPort1.Text == "")
				{
					MessageBox.Show("Port번호를 입력해 주십시오.");
					textBoxPort1.Focus();
					return;
				}
				else if (textBoxDb1.Text == "")
				{
					MessageBox.Show("Database를 입력해 주십시오.");
					textBoxDb1.Focus();
					return;
				}
				else if (textBoxUn1.Text == "")
				{
					MessageBox.Show("ID를 입력해 주십시오.");
					textBoxUn1.Focus();
					return;
				}
				else if (textBoxPw1.Text == "")
				{
					MessageBox.Show("Password를 입력해 주십시오.");
					textBoxPw1.Focus();
					return;
				}

				string connectionDb1 = "Server = " + textBoxIp1.Text + ";Port = " + textBoxPort1.Text + ";Database = " + textBoxDb1.Text + ";username = " + textBoxUn1.Text + ";password=" + textBoxPw1.Text + ";Charset=utf8;";
				string connectionDb2 = "Server = " + textBoxIp2.Text + ";Port = " + textBoxPort2.Text + ";Database = " + textBoxDb2.Text + ";username = " + textBoxUn2.Text + ";password=" + textBoxPw2.Text + ";Charset=utf8;";

				con = new MySqlConnection(connectionDb1);
				con2 = new MySqlConnection(connectionDb2);
				con.Open();
				con2.Open();


				string query1 = "SELECT b.table_name tbl, a.table_comment cmt, COUNT(*) cnt, b.EXTRA ex  FROM information_schema.tables a left JOIN information_schema.columns b ON a.TABLE_NAME=b.table_name WHERE a.table_schema = '" + textBoxDb1.Text + "' AND b.table_schema = '" + textBoxDb1.Text + "'  AND a.table_type='BASE TABLE' group BY b.TABLE_NAME ORDER BY b.TABLE_NAME asc;";
				string query2 = "SELECT b.table_name tbl, a.table_comment cmt, COUNT(*) cnt, b.EXTRA ex  FROM information_schema.tables a left JOIN information_schema.columns b ON a.TABLE_NAME=b.table_name WHERE a.table_schema = '" + textBoxDb2.Text + "' AND b.table_schema = '" + textBoxDb2.Text + "' AND a.table_type='BASE TABLE'  group BY b.TABLE_NAME ORDER BY b.TABLE_NAME asc;";
				//	MySqlDataAdapter adp = DBAdapter(conn, _HostName, _PORT, _DATABASE, _ID, _PWD, query1);
				//MySqlDataReader rdr = DBConnectTest(conn, _HostName, _PORT, _DATABASE, _ID, _PWD, query1);
				MySqlDataReader rdr = DBConnect(con, query1);
				List<ListInfo> listTable1 = new List<ListInfo>();
				List<ListInfo> listTable2 = new List<ListInfo>();
				List<ListInfoAll> listTableAll = new List<ListInfoAll>();

				while (rdr.Read())
				{
					ListInfo listInfo = new ListInfo() { tableName = rdr["tbl"].ToString(), fieldCount = Convert.ToInt32(rdr["cnt"].ToString()), tableCmt = rdr["cmt"].ToString() };
					listTable1.Add(listInfo);
					// dataGridView1.Rows[i].Cells[1].Value = rdr["cnt"].ToString();
				}

				MySqlDataReader rdr2 = DBConnect(con2, query2);
				while (rdr2.Read())
				{
					ListInfo listInfo = new ListInfo() { tableName = rdr2["tbl"].ToString(), fieldCount = Convert.ToInt32(rdr2["cnt"].ToString()), tableCmt = rdr["cmt"].ToString() };
					listTable2.Add(listInfo);
				}


				for (int k = 0; k < listTable1.Count; k++)
				{
					listTableAll.Add(new ListInfoAll() { db = "A", tableName = listTable1[k].tableName, fieldCount1 = listTable1[k].fieldCount, fieldCount2 = 0, tableCmt = listTable1[k].tableCmt });
				}
				for (int k = 0; k < listTable2.Count; k++)
				{
					bool found = false;
					for (int j = 0; j < listTableAll.Count; j++)
					{
						if (listTable2[k].tableName == listTableAll[j].tableName)
						{
							listTableAll[j].fieldCount2 = listTable2[k].fieldCount;
							found = true;
							listTableAll[j].db = "A+B";
							break;
						}
					}
					//listTableAll.Add(new ListInfoAll() { db = "A", tableName = listTable1[k].tableName, fieldCount1 = listTable1[k].fieldCount, fieldCount2 = 0 });
					if (found == false)
					{
						listTableAll.Add(new ListInfoAll() { db = "B", tableName = listTable2[k].tableName, fieldCount1 = 0, fieldCount2 = listTable2[k].fieldCount, tableCmt = listTable2[k].tableCmt });
					}
				}
				dataGridView1.Rows.Clear();
				for (int k = 0; k < listTableAll.Count; k++)
				{
					dataGridView1.Rows.Add(1);
					dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = true;
					if (listTableAll[k].db == "B")
					{
						dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = false;

					}
					dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = listTableAll[k].db;
					dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = listTableAll[k].tableName;
					dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = listTableAll[k].tableCmt;
					dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = listTableAll[k].fieldCount1;
					dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[5].Value = listTableAll[k].fieldCount2;
				}


				int i = 0;
				dataGridView1.Columns[i++].ReadOnly = false;
				dataGridView1.Columns[i++].ReadOnly = true;
				dataGridView1.Columns[i++].ReadOnly = true;
				dataGridView1.Columns[i++].ReadOnly = true;
				dataGridView1.Columns[i++].ReadOnly = true;
				dataGridView1.Columns[i++].ReadOnly = true;
				dataGridView1.Columns[i++].ReadOnly = true;
				dataGridView1.Columns[i++].ReadOnly = true;

				compare();

				//프로시저 보여주기메소드
				showProceaser();
				//이벤트 보여주기메소드
				showEvents();
				//함수 보여주기메소드
				showFunction();
				//뷰 보여주기메소드
				showView();

			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
			con.Close();
			con2.Close();
		}

		class functionInfo
		{
			public string FUNCTION_NAME;
			public string FUNCTION_CREATED;
			public string FUNCTION_MODIFIED;
		}

		class functionInfoAll
		{
			public string FUNCTION_NAME1;
			public string FUNCTION_NAME2;

			public string FUNCTION_CREATED1;
			public string FUNCTION_CREATED2;

			public string FUNCTION_MODIFIED1;
			public string FUNCTION_MODIFIED2;
		}

		class EventInfo
		{
			public string EVENT_NAME;
			public string CREATED;
			public string LAST_ALTERED;
		}

		class EventInfoAll
		{
			public string EVENT_NAME1;
			public string EVENT_NAME2;

			public string CREATED1;
			public string CREATED2;

			public string LAST_ALTERED1;
			public string LAST_ALTERED2;
		}

		class procedureInfo
		{
			public string procedureName;
			public string procedureCreated;
			public string procedureUpdate;
			public string procedureComment;

			public string procedureQuery;
		}

		class procedureInfoAll
		{
			public string procedureName1;
			public string procedureName2;
			public string procedureCreated1;
			public string procedureCreated2;
			public string procedureUpdate1;
			public string procedureUpdate2;
			public string procedureComment1;
			public string procedureComment2;

			public string procedureQuery1;
			public string procedureQuery2;

		}


		class ListInfo
		{
			public string tableName;
			public int fieldCount;
			public string tableCmt;

			public string SHOWTABLE;
			public string CREATETABLE;

			public string procedureName;
			public string procedureCreated;
			public string procedureUpdate;
			public string procedureComment;

			public string FUNCTION_NAME;
			public string FUNCTION_CREATED;
			public string FUNCTION_MODIFIED;

			public string viewName;
		}



		class ListInfoAll
		{
			public string db;
			public string tableName;
			public int fieldCount1;
			public int fieldCount2;

			public string tableCmt;

			public string SHOWTABLE;
			public string CREATETABLE1;
			public string CREATETABLE2;

			public string procedureName1;
			public string procedureName2;
			public string procedureCreated1;
			public string procedureCreated2;
			public string procedureUpdate1;
			public string procedureUpdate2;
			public string procedureComment1;
			public string procedureComment2;


			public string FUNCTION_NAME1;
			public string FUNCTION_NAME2;

			public string FUNCTION_CREATED1;
			public string FUNCTION_CREATED2;

			public string FUNCTION_MODIFIED1;
			public string FUNCTION_MODIFIED2;

			public string viewName1;
			public string viewName2;

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
			public string INCREMENT; //ex

			public string ORDINAL_POSITION;

			public string SHOWTABLE;
			public string CREATETABLE;
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
			public string INCREMENT1; // ex
			public string INCREMENT2; // ex

			public string ORDINAL_POSITION1;
			public string ORDINAL_POSITION2;


			public string SHOWTABLE1;
			public string SHOWTABLE2;
			public string CREATETABLE1;
			public string CREATETABLE2;
		}
		//


		//연결테스트 버튼 클릭
		private MySqlDataReader DBConnect(MySqlConnection con, string query)
		{
			try
			{
				string sql = query;
				MySqlCommand cmd = new MySqlCommand(sql, con);
				MySqlDataReader rdr = cmd.ExecuteReader();
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
		private MySqlDataReader DBConnectTest(MySqlConnection con, string hostname, string port, string database, string id, string pwd, string query)
		{
			try
			{

				StringBuilder _strArg = new StringBuilder("");
				MySqlDataReader rdr;
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
				_strArg.Append("Charset=utf8;");
				con = new MySqlConnection(_strArg.ToString());
				con.Open();
				//if (con == null)
				//{

				//}


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

		//DBConnectTest2 메소드
		//데이터베이스와 테이블인자를 받아서 컬럼테이블에 나타내기
		private MySqlDataReader DBConnectTest2(MySqlConnection con, string tableName, string database)
		{
			string sql2 = "SELECT COLUMN_NAME fieldName, COLUMN_TYPE dataType, CHARACTER_MAXIMUM_LENGTH length, COLUMN_DEFAULT default1, COLUMN_COMMENT comment, IS_NULLABLE nullable, COLLATION_NAME colName, EXTRA EX, ORDINAL_POSITION pos FROM information_schema.columns WHERE table_schema= '" + database + "' and TABLE_NAME = '" + tableName + "' ORDER BY TABLE_NAME asc";
			MySqlCommand cmd = new MySqlCommand(sql2, con);
			MySqlDataReader rdr = cmd.ExecuteReader();
			return rdr;
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
			try
			{
				if (dataGridView1.CurrentCell.ColumnIndex == 2)
				{
					if (e.RowIndex < 0) return;
					string tbl = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
					if (tbl == null) return;
					showFields(tbl);
				}
			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}
		void showFields(string tableName)
		{
			try
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

				StringBuilder _strArg = new StringBuilder("");
				StringBuilder _strArg2 = new StringBuilder("");
				_strArg.Append("Server = ");           // SQL
				_strArg.Append(_HostName);        // 서버
				_strArg.Append(";Port = ");
				_strArg.Append(_PORT);                 // 포트
				_strArg.Append(";Database = ");
				_strArg.Append(_DATABASE);          // 데이터베이스
				_strArg.Append(";username = ");
				_strArg.Append(_ID);                     // ID
				_strArg.Append(";password = ");
				_strArg.Append(_PWD);                 // PWD
				_strArg.Append(";");
				_strArg.Append("Charset=utf8;");

				_strArg2.Append("Server = ");           // SQL
				_strArg2.Append(_HostName2);        // 서버
				_strArg2.Append(";Port = ");
				_strArg2.Append(_PORT2);                 // 포트
				_strArg2.Append(";Database = ");
				_strArg2.Append(_DATABASE2);          // 데이터베이스
				_strArg2.Append(";username = ");
				_strArg2.Append(_ID2);                     // ID
				_strArg2.Append(";password = ");
				_strArg2.Append(_PWD2);                 // PWD
				_strArg2.Append(";");
				_strArg2.Append("Charset=utf8;");



				MySqlConnection con = new MySqlConnection(_strArg.ToString());
				con.Open();
				MySqlConnection con2 = new MySqlConnection(_strArg2.ToString());
				con2.Open();


				// 테이블명의 필드를 가져와서 데이터그리드뷰2에 보여준다
				MySqlDataReader rdr = DBConnectTest2(con, tableName, _DATABASE);
				List<Columns> listTable1 = new List<Columns>();
				List<Columns> listTable2 = new List<Columns>();
				List<ColumnsAll> listTableAll = new List<ColumnsAll>();
				while (rdr.Read())
				{
					Columns listInfo = new Columns() { COLUMN_NAME = rdr["fieldName"].ToString(), DATA_TYPE = rdr["dataType"].ToString(), CHARACTER_MAXIMUM_LENGTH = rdr["length"].ToString(), COLUMN_DEFAULT = rdr["default1"].ToString(), COLUMN_COMMENT = rdr["comment"].ToString(), IS_NULLABLE = rdr["nullable"].ToString(), COLLATION_NAME = rdr["colName"].ToString(), INCREMENT = rdr["EX"].ToString(), ORDINAL_POSITION = rdr["pos"].ToString() };
					listTable1.Add(listInfo);
				}

				MySqlDataReader rdr2 = DBConnectTest2(con2, tableName, _DATABASE2);

				while (rdr2.Read())
				{
					Columns listInfo = new Columns() { COLUMN_NAME = rdr2["fieldName"].ToString(), DATA_TYPE = rdr2["dataType"].ToString(), CHARACTER_MAXIMUM_LENGTH = rdr2["length"].ToString(), COLUMN_DEFAULT = rdr2["default1"].ToString(), COLUMN_COMMENT = rdr2["comment"].ToString(), IS_NULLABLE = rdr2["nullable"].ToString(), COLLATION_NAME = rdr2["colName"].ToString(), INCREMENT = rdr2["EX"].ToString(), ORDINAL_POSITION = rdr2["pos"].ToString() };
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
						COLLATION_NAME1 = listTable1[i].COLLATION_NAME,

						INCREMENT1 = listTable1[i].INCREMENT,
						ORDINAL_POSITION1 = listTable1[i].ORDINAL_POSITION
					});
				}
				for (int i = 0; i < listTable2.Count; i++)
				{
					bool found = false;
					for (int j = 0; j < listTableAll.Count; j++)
					{
						if (listTable2[i].COLUMN_NAME == listTableAll[j].COLUMN_NAME1)
						{
							listTableAll[j].COLUMN_NAME2 = listTable2[i].COLUMN_NAME;
							listTableAll[j].DATA_TYPE2 = listTable2[i].DATA_TYPE;
							listTableAll[j].CHARACTER_MAXIMUM_LENGTH2 = listTable2[i].CHARACTER_MAXIMUM_LENGTH;
							listTableAll[j].COLUMN_DEFAULT2 = listTable2[i].COLUMN_DEFAULT;
							listTableAll[j].COLUMN_COMMENT2 = listTable2[i].COLUMN_COMMENT;
							listTableAll[j].IS_NULLABLE2 = listTable2[i].IS_NULLABLE;
							listTableAll[j].COLLATION_NAME2 = listTable2[i].COLLATION_NAME;
							listTableAll[j].INCREMENT2 = listTable2[i].INCREMENT;
							listTableAll[j].ORDINAL_POSITION2 = listTable2[i].ORDINAL_POSITION;

							found = true;
							listTableAll[j].TABLE = "A+B";
							break;
						}
					}
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
							COLLATION_NAME2 = listTable2[i].COLLATION_NAME,
							INCREMENT2 = listTable2[i].INCREMENT,
							ORDINAL_POSITION2 = listTable2[i].ORDINAL_POSITION

						});
					}
				}



				dataGridView2.Rows.Clear();
				for (int i = 0; i < listTableAll.Count; i++)
				{
					dataGridView2.Rows.Add(1);
					int k = 0;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].COLUMN_NAME1;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].DATA_TYPE1;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].IS_NULLABLE1;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].COLUMN_DEFAULT1;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].COLUMN_COMMENT1;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].COLLATION_NAME1;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].INCREMENT1;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].ORDINAL_POSITION1;


					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].COLUMN_NAME2;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].DATA_TYPE2;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].IS_NULLABLE2;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].COLUMN_DEFAULT2;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].COLUMN_COMMENT2;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].COLLATION_NAME2;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].INCREMENT2;
					dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[k++].Value = listTableAll[i].ORDINAL_POSITION2;

				}
				con.Close();
				con2.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				LogMgr.ExceptionLog(ex);
			}
		}




		void allStrart()
		{
			if (tabControl1.SelectedTab == tabPage1)
			{
				//체크박스 벨류가 false라면
				if (dataGridView1.Rows[0].Cells[0].Value.ToString().Contains("False").ToString() == "False")
				{
					///데이터그리드뷰1에 로우줄을 다가지고온다
					///상태가 다름이면 해당 쇼테이블 을 가지고 온다
					///테이블의 생성문을 만들어서
					///함수에 넣는다
					///
					for (int i = 0; i < dataGridView1.Rows.Count; i++)
					{
						string tbl = dataGridView1.Rows[i].Cells[2].Value.ToString();
						if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "없음")
						{
							if (tbl == null) return;
							//생성테이블쿼리 저장하기
							string queryCreate = "SHOW CREATE TABLE " + tbl;
							ShowCreateTable(queryCreate);
							dataGridView1.Rows[i].Cells[6].Value = 100;
							dataGridView1.Rows[i].Cells[5].Value = "확인중";
						}

						if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "필드수 다름")
						{
							showFields(tbl);
							alterChange();
							changePosition();

							dataGridView1.Rows[i].Cells[6].Value = 100;
							dataGridView1.Rows[i].Cells[5].Value = "확인중";
						}
						else continue;
					}
				}
			}
			//탭페이지가 프로시저인경우
			if (tabControl1.SelectedTab == tabPage2)
			{
				for (int i = 0; i < dataGridView3.Rows.Count; i++)
				{
					string tbl = dataGridView3.Rows[i].Cells[0].Value.ToString();
					if (dataGridView3.Rows[i].Cells[5].Value == null)
					{
						if (tbl == null) return;
						//생성테이블쿼리 저장하기
						string queryCreate = "SHOW CREATE PROCEDURE " + tbl;
						procedureCreateTable(queryCreate);
						dataGridView3.Rows[i].Cells[10].Value = 100;
						dataGridView3.Rows[i].Cells[11].Value = "확인중";
					}
				}
				showProceaser();
			}
			//탭페이지가 이벤트인경우
			if (tabControl1.SelectedTab == tabPage3)
			{
				for (int i = 0; i < dataGridView5.Rows.Count; i++)
				{
					string tbl = dataGridView5.Rows[i].Cells[0].Value.ToString();
					if (dataGridView5.Rows[i].Cells[5].Value == null)
					{
						if (tbl == null) return;
						//생성테이블쿼리 저장하기
						string queryCreate = "SHOW CREATE EVENT " + tbl;
						eventCreateTable(queryCreate);
						dataGridView5.Rows[i].Cells[6].Value = 100;
						dataGridView5.Rows[i].Cells[7].Value = "확인중";
					}
				}
				showEvents();
			}

			//탭페이지가 함수인경우
			if (tabControl1.SelectedTab == tabPage4)
			{
				for (int i = 0; i < dataGridView7.Rows.Count; i++)
				{
					string tbl = dataGridView7.Rows[i].Cells[0].Value.ToString();
					if (dataGridView7.Rows[i].Cells[3].Value == null)
					{
						if (tbl == null) return;
						//생성테이블쿼리 저장하기
						string queryCreate = "show create function " + tbl;
						functionCreateTable(queryCreate);
						dataGridView7.Rows[i].Cells[6].Value = 100;
						dataGridView7.Rows[i].Cells[7].Value = "확인중";
					}
				}
				showFunction();
			}

			//탭페이지가 뷰인경우
			if (tabControl1.SelectedTab == tabPage5)
			{
				for (int i = 0; i < dataGridView9.Rows.Count; i++)
				{
					string tbl = dataGridView9.Rows[i].Cells[0].Value.ToString();
					if (dataGridView9.Rows[i].Cells[3].Value == null)
					{
						if (tbl == null) return;
						//생성테이블쿼리 저장하기
						string queryCreate = "show create view " + tbl;
						viewCreateTable(queryCreate);
						dataGridView9.Rows[i].Cells[2].Value = 100;
						dataGridView9.Rows[i].Cells[3].Value = "확인중";
					}
				}
				showView();
			}

		}

		private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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

		private void buttonStart_Click(object sender, EventArgs e)
		{
			allStrart();
			buttonConnect_Click(sender, e);
		}

		private void Create(string queryCreate)
		{
			try
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

				StringBuilder _strArg = new StringBuilder("");
				StringBuilder _strArg2 = new StringBuilder("");
				_strArg.Append("Server = ");           // SQL
				_strArg.Append(_HostName);        // 서버
				_strArg.Append(";Port = ");
				_strArg.Append(_PORT);                 // 포트
				_strArg.Append(";Database = ");
				_strArg.Append(_DATABASE);          // 데이터베이스
				_strArg.Append(";username = ");
				_strArg.Append(_ID);                     // ID
				_strArg.Append(";password = ");
				_strArg.Append(_PWD);                 // PWD
				_strArg.Append(";");
				_strArg.Append("Charset=utf8;");

				_strArg2.Append("Server = ");           // SQL
				_strArg2.Append(_HostName2);        // 서버
				_strArg2.Append(";Port = ");
				_strArg2.Append(_PORT2);                 // 포트
				_strArg2.Append(";Database = ");
				_strArg2.Append(_DATABASE2);          // 데이터베이스
				_strArg2.Append(";username = ");
				_strArg2.Append(_ID2);                     // ID
				_strArg2.Append(";password = ");
				_strArg2.Append(_PWD2);                 // PWD
				_strArg2.Append(";");
				_strArg2.Append("Charset=utf8;");


				//MySqlConnection con = new MySqlConnection(_strArg.ToString());
				//con.Open();
				MySqlConnection con2 = new MySqlConnection(_strArg2.ToString());
				con2.Open();


				//데이터베이스2에 생성
				MySqlDataReader rdr = DBConnect(con2, queryCreate);
				con2.Close();
			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
				MessageBox.Show(ex.ToString());
			}
		}

		private void dataGridView1_SelectionChanged(object sender, EventArgs e)
		{
			try
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
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}

		private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			for (int i = (dataGridView2.Columns.Count) / 2; i < dataGridView2.Columns.Count; i++)
			{
				dataGridView2.Columns[i].DefaultCellStyle.BackColor = Color.GreenYellow;
			}
		}

		private void buttonOnes_Click(object sender, EventArgs e)
		{
			try
			{
				int rowIndex = dataGridView1.CurrentCell.RowIndex;
				if (rowIndex < 0) return;
				if (dataGridView1.Rows[rowIndex].Cells[0].Value == null)
				{
					return;
				}
				string tbl = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
				if (tbl == null) return;
				string queryCreate = "SHOW CREATE TABLE " + tbl;
				ShowCreateTable(queryCreate);
				alterChange();
				changePosition();
				buttonConnect_Click(sender, e);
			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}

		private static DateTime Delay(int MS)
		{
			DateTime ThisMoment = DateTime.Now;
			TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
			DateTime AfterWards = ThisMoment.Add(duration);
			while (AfterWards >= ThisMoment)
			{
				System.Windows.Forms.Application.DoEvents();
				ThisMoment = DateTime.Now;
			}
			return DateTime.Now;
		}



		private void ShowCreateTable(string queryCreate)
		{
			try
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

				StringBuilder _strArg = new StringBuilder("");
				StringBuilder _strArg2 = new StringBuilder("");
				_strArg.Append("Server = ");           // SQL
				_strArg.Append(_HostName);        // 서버
				_strArg.Append(";Port = ");
				_strArg.Append(_PORT);                 // 포트
				_strArg.Append(";Database = ");
				_strArg.Append(_DATABASE);          // 데이터베이스
				_strArg.Append(";username = ");
				_strArg.Append(_ID);                     // ID
				_strArg.Append(";password = ");
				_strArg.Append(_PWD);                 // PWD
				_strArg.Append(";");
				_strArg.Append("Charset=utf8;");

				_strArg2.Append("Server = ");           // SQL
				_strArg2.Append(_HostName2);        // 서버
				_strArg2.Append(";Port = ");
				_strArg2.Append(_PORT2);                 // 포트
				_strArg2.Append(";Database = ");
				_strArg2.Append(_DATABASE2);          // 데이터베이스
				_strArg2.Append(";username = ");
				_strArg2.Append(_ID2);                     // ID
				_strArg2.Append(";password = ");
				_strArg2.Append(_PWD2);                 // PWD
				_strArg2.Append(";");
				_strArg2.Append("Charset=utf8;");



				MySqlConnection con = new MySqlConnection(_strArg.ToString());
				con.Open();
				//MySqlConnection con2 = new MySqlConnection(_strArg2.ToString());
				//con2.Open();


				MySqlDataReader rdr = DBConnect(con, queryCreate);
				List<Columns> listTable1 = new List<Columns>();
				List<Columns> listTable2 = new List<Columns>();
				List<ColumnsAll> listTableAll = new List<ColumnsAll>();
				Delay(200);
				while (rdr.Read())
				{
					Columns listInfo = new Columns() { CREATETABLE = rdr["Create Table"].ToString() };
					listTable1.Add(listInfo);
				}
				string createQuery = listTable1[0].CREATETABLE;
				Create(createQuery);


				con.Close();
			}
			catch (Exception e)
			{
				LogMgr.ExceptionLog(e);
				MessageBox.Show(e.ToString());
			}
		}



		/// <summary>
		/// compare 함수 
		/// 비교해서 필드수같음 나타냄
		/// </summary>
		void compare()
		{
			try
			{
				for (int i = 0; i < dataGridView1.Rows.Count; i++)
				{
					if (dataGridView1.Rows[i].Cells[4].Value.ToString().Trim() == dataGridView1.Rows[i].Cells[5].Value.ToString().Trim())
					{
						dataGridView1.Rows[i].Cells[7].Value = "작업완료";
						dataGridView1.Rows[i].Cells[6].Value = "100";
					}
					else if (dataGridView1.Rows[i].Cells[5].Value.ToString().Trim() == "0") dataGridView1.Rows[i].Cells[7].Value = "없음";
					else dataGridView1.Rows[i].Cells[7].Value = "필드수 다름";
				}

				for (int i = 0; i < dataGridView3.Rows.Count; i++)
				{
					if (dataGridView3.Rows[i].Cells[5].Value == null)
					{
						continue;
					}
					if (dataGridView3.Rows[i].Cells[0].Value.ToString().Trim() == dataGridView3.Rows[i].Cells[5].Value.ToString().Trim())
					{
						dataGridView3.Rows[i].Cells[10].Value = "100";
						dataGridView3.Rows[i].Cells[11].Value = "프로시저존재";
					}
				}

				for (int i = 0; i < dataGridView5.Rows.Count; i++)
				{
					if (dataGridView5.Rows[i].Cells[5].Value == null)
					{
						continue;
					}
					if (dataGridView5.Rows[i].Cells[0].Value.ToString().Trim() == dataGridView5.Rows[i].Cells[3].Value.ToString().Trim())
					{
						dataGridView5.Rows[i].Cells[6].Value = "100";
						dataGridView5.Rows[i].Cells[7].Value = "존재";
					}
				}

				for (int i = 0; i < dataGridView7.Rows.Count; i++)
				{
					if (dataGridView7.Rows[i].Cells[5].Value == null)
					{
						continue;
					}
					if (dataGridView7.Rows[i].Cells[0].Value.ToString().Trim() == dataGridView7.Rows[i].Cells[3].Value.ToString().Trim())
					{
						dataGridView7.Rows[i].Cells[6].Value = "100";
						dataGridView7.Rows[i].Cells[7].Value = "존재";
					}
				}

				for (int i = 0; i < dataGridView9.Rows.Count; i++)
				{
					if (dataGridView9.Rows[i].Cells[1].Value == null)
					{
						continue;
					}
					if (dataGridView9.Rows[i].Cells[0].Value.ToString().Trim() == dataGridView9.Rows[i].Cells[1].Value.ToString().Trim())
					{
						dataGridView9.Rows[i].Cells[2].Value = "100";
						dataGridView9.Rows[i].Cells[3].Value = "존재";
					}
				}


			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}
		private void buttonDelete_Click(object sender, EventArgs e)
		{
			try
			{
				int rowIndex = dataGridView1.CurrentCell.RowIndex;
				if (rowIndex < 0) return;
				if (dataGridView1.Rows[rowIndex].Cells[0].Value == null)
				{
					return;
				}
				string tbl = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
				if (tbl == null) return;

				string dropTable = "drop table " + tbl;
				Create(dropTable);

				buttonConnect_Click(sender, e);
			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}

		void changePosition()
		{
			for (int i = 0; i < dataGridView2.Rows.Count; i++)
			{
				int rowIndex = dataGridView1.CurrentCell.RowIndex;
				if (rowIndex < 0) return;
				if (dataGridView1.Rows[rowIndex].Cells[0].Value == null)
				{
					return;
				}
				string tbl = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
				if (tbl == null) return;

				if (dataGridView2.Rows[i].Cells[1].Value == null)
				{
					continue;
				}



				string changePosition;
				for (int k = 1; k < dataGridView2.Rows.Count; k++)
				{
					if (dataGridView2.Rows[0].Cells[15].Value == null)
					{
						continue;
					}
					if (dataGridView2.Rows[0].Cells[7].Value.ToString().Trim() != dataGridView2.Rows[0].Cells[15].Value.ToString().Trim())
					{
						changePosition = "ALTER TABLE " + tbl + " MODIFY COLUMN `"
					+ dataGridView2.Rows[0].Cells[0].Value.ToString() + "` `"
					+ dataGridView2.Rows[0].Cells[1].Value.ToString() + "`"
					+ " FIRST";
						Create(changePosition);
					}
					else if (dataGridView2.Rows[k].Cells[7].Value == null)
					{
						continue;
					}
					else if (dataGridView2.Rows[k].Cells[15].Value == null)
					{
						continue;
					}
					else if (dataGridView2.Rows[k].Cells[7].Value.ToString().Trim() != dataGridView2.Rows[k].Cells[15].Value.ToString().Trim())
					{
						changePosition = "ALTER TABLE " + tbl + " MODIFY COLUMN `"
				+ dataGridView2.Rows[k].Cells[0].Value.ToString() + "` "
				+ dataGridView2.Rows[k].Cells[1].Value.ToString()
				+ " AFTER " + dataGridView2.Rows[k - 1].Cells[0].Value.ToString();
						Create(changePosition);
					}
				}



			}
		}


		void alterChange()
		{
			try
			{
				for (int i = 0; i < dataGridView2.Rows.Count; i++)
				{
					int rowIndex = dataGridView1.CurrentCell.RowIndex;
					if (rowIndex < 0) return;
					if (dataGridView1.Rows[rowIndex].Cells[0].Value == null)
					{
						return;
					}
					string tbl = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
					if (tbl == null) return;

					if (dataGridView2.Rows[i].Cells[1].Value == null)
					{
						continue;
					}
					string columnType = dataGridView2.Rows[i].Cells[1].Value.ToString();
					if (columnType == null) columnType = "";
					else if (columnType.ToUpper().Contains("DATETIME"))
					{
						columnType = " DATETIME ";
					}

					string nullYN = dataGridView2.Rows[i].Cells[2].Value.ToString();
					if (nullYN == null) return;
					if (nullYN == "NO") nullYN = " NOT NULL";
					else if (nullYN == "YES") nullYN = " NULL";
					if (columnType.ToUpper().Contains("DATETIME"))
					{
						nullYN = "";
					}

					string nullable = dataGridView2.Rows[i].Cells[2].Value.ToString();
					if (nullable == null) return;
					if (nullable == "NO") nullable = " NOT NULL";
					else if (nullable == "YES") nullable = " NULL";
					if (columnType.ToUpper().Contains("DATETIME"))
					{
						nullable = "";
					}
					string def = dataGridView2.Rows[i].Cells[3].Value.ToString();
					if (def == null) def = "";
					if (columnType.ToUpper().Contains("INT"))
					{
						def = " DEFAULT 0 ";
					}
					if (columnType.ToUpper().Contains("SMALLINT") || columnType.ToUpper().Contains("DECIMAL"))
					{
						def = " 0";
					}

					else def = " DEFAULT '" + def + "'";

					if (nullable.ToUpper().Contains("NULL"))
					{
						def = " DEFAULT NULL";
					}
					if (nullable.ToUpper().Contains(" NOT NULL"))
					{
						def = "";
					}
					else if (columnType.ToUpper().Contains("DATETIME"))
					{
						def = " DEFAULT NULL ";

					}

					string alterTable = "";



					// db2 이름이 비었있으면 db1셀에 있는 컬럼내용을 db2에 추가해라
					if (dataGridView2.Rows[i].Cells[8].Value == null)
					{
						alterTable = "ALTER TABLE " + tbl + " ADD `" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "` " + dataGridView2.Rows[i].Cells[1].Value.ToString() + nullable + def;

						Create(alterTable);
					}

					//db2 유형이 비어있으면 넘어가라
					if (dataGridView2.Rows[i].Cells[9].Value == null)
					{
						continue;
					}

					//db1 유형과 db2유형이 다르면 db2유형을 db1유형으로 바꿔라
					if (dataGridView2.Rows[i].Cells[1].Value.ToString().Trim() != dataGridView2.Rows[i].Cells[9].Value.ToString().Trim())
					{
						alterTable = "ALTER TABLE " + tbl + " MODIFY COLUMN `" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "` " + dataGridView2.Rows[i].Cells[1].Value.ToString() + nullable + def;
						Create(alterTable);
					}

					// db1 널값이 다르면 no인지 yes인지 파악해서 db2의 맞는값으로 바꿔라
					if (dataGridView2.Rows[i].Cells[2].Value.ToString().Trim() != dataGridView2.Rows[i].Cells[10].Value.ToString().Trim())
					{
						if (dataGridView2.Rows[i].Cells[2].Value.ToString().Trim() == "NO")
						{
							alterTable = "ALTER TABLE " + tbl + " MODIFY `" + dataGridView2.Rows[i].Cells[8].Value.ToString() + "` " + dataGridView2.Rows[i].Cells[1].Value.ToString() + "NOT NULL";
							Create(alterTable);
						}
						else if (dataGridView2.Rows[i].Cells[2].Value.ToString().Trim() == "YES")
						{
							alterTable = "ALTER TABLE " + tbl + " MODIFY `" + dataGridView2.Rows[i].Cells[8].Value.ToString() + "` " + dataGridView2.Rows[i].Cells[1].Value.ToString();
							Create(alterTable);
						}
					}

					//db1 기본값과 db2의 기본값이 다르면 null값 유무와 db1셀의 default값을 넣어서 바꿔라
					if (dataGridView2.Rows[i].Cells[3].Value.ToString().Trim() != dataGridView2.Rows[i].Cells[11].Value.ToString().Trim())
					{
						alterTable = "ALTER TABLE " + tbl + " MODIFY COLUMN `" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "` " + dataGridView2.Rows[i].Cells[1].Value.ToString() + nullYN + " DEFAULT '" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "'";
						Create(alterTable);
					}

					//CHANGE로 커멘트 변경할려면 컬럼도 동시에 바꿔져야됨 
					//ALTER TABLE `user` CHANGE `id` `id` INT( 11 ) COMMENT 'user 테이블의 id';
					if (dataGridView2.Rows[i].Cells[4].Value.ToString().Trim() != dataGridView2.Rows[i].Cells[12].Value.ToString().Trim())
					{
						alterTable = "ALTER TABLE " + tbl + " MODIFY COLUMN `" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "` " + dataGridView2.Rows[i].Cells[1].Value.ToString() + nullable + " COMMENT '" + dataGridView2.Rows[i].Cells[4].Value.ToString() + "'";
						Create(alterTable);
					}

					//collate utf8로 전부 수정
					//ALTER TABLE 테이블명 MODIFY COLUMN 컬럼 VARCHAR(255) CHARACTER SET utf8 COLLATE utf8_general_ci
					if (dataGridView2.Rows[i].Cells[5].Value.ToString().Trim() != dataGridView2.Rows[i].Cells[13].Value.ToString().Trim())
					{
						alterTable = "ALTER TABLE " + tbl + " MODIFY COLUMN `" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "` " + dataGridView2.Rows[i].Cells[1].Value.ToString() + " CHARACTER SET utf8 COLLATE utf8_general_ci";
						Create(alterTable);
					}
					//autoincrement alter
					//ALTER TABLE 적용할테이블명칭 MODIFY 컬럼 INT NOT NULL AUTO_INCREMENT;
					if (dataGridView2.Rows[i].Cells[6].Value.ToString().Trim() != dataGridView2.Rows[i].Cells[14].Value.ToString().Trim())
					{
						alterTable = "ALTER TABLE " + tbl + " MODIFY COLUMN `" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "` " + dataGridView2.Rows[i].Cells[1].Value.ToString() + nullable + " AUTO_INCREMENT";
						Create(alterTable);
					}
					Create(alterTable);

				}

			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}


		private void button1_Click_1(object sender, EventArgs e)
		{
			alterChange();
			buttonConnect_Click(sender, e);
		}

		private void btnPositionChange_Click(object sender, EventArgs e)
		{
			changePosition();
			buttonConnect_Click(sender, e);
		}




		void createProceasor(string ProcedureName)
		{
			try
			{
				string connectionDb1 = "Server = " + textBoxIp1.Text + ";Port = " + textBoxPort1.Text + ";Database = " + textBoxDb1.Text + ";username = " + textBoxUn1.Text + ";password = " + textBoxPw1.Text + ";" + "Charset=utf8;";
				string connectionDb2 = "Server = " + textBoxIp2.Text + ";Port = " + textBoxPort2.Text + ";Database = " + textBoxDb2.Text + ";username = " + textBoxUn2.Text + ";password = " + textBoxPw2.Text + ";" + "Charset=utf8;";
				con = new MySqlConnection(connectionDb1);
				con2 = new MySqlConnection(connectionDb2);
				con.Open();
				con2.Open();

				string showProcedure = "SHOW CREATE PROCEDURE " + ProcedureName;

				MySqlDataReader rdrProcedure = DBConnect(con, showProcedure);
				List<procedureInfo> listTable1 = new List<procedureInfo>();
				List<procedureInfo> listTable2 = new List<procedureInfo>();
				List<procedureInfoAll> listTableAll = new List<procedureInfoAll>();
				while (rdrProcedure.Read())
				{
					procedureInfo listInfo = new procedureInfo() { procedureQuery = rdrProcedure["Create Procedure"].ToString() };
					listTable1.Add(listInfo);
				}

				string showProcedure2 = "SHOW CREATE PROCEDURE " + ProcedureName;
				MySqlDataReader rdrProcedure2 = DBConnect(con2, showProcedure2);

				if (rdrProcedure2 == null)
				{
					return;
				}

				while (rdrProcedure2.Read())
				{
					procedureInfo listInfo = new procedureInfo() { procedureQuery = rdrProcedure2["Create Procedure"].ToString() };
					listTable2.Add(listInfo);
				}
				for (int k = 0; k < listTable1.Count; k++)
				{
					listTableAll.Add(new procedureInfoAll()
					{
						procedureQuery1 = listTable1[k].procedureQuery
					});
				}
				for (int k = 0; k < listTable2.Count; k++)
				{
					bool found = false;
					for (int j = 0; j < listTableAll.Count; j++)
					{
						if (listTable2[k].procedureName == listTableAll[j].procedureName1)
						{
							listTableAll[j].procedureQuery2 = listTable2[k].procedureQuery;

							found = true;
							break;
						}
					}
					if (found == false)
					{
						listTableAll.Add(new procedureInfoAll()
						{
							procedureQuery2 = listTable2[k].procedureQuery
						});
					}
				}
				dataGridView3.Rows.Clear();
				for (int k = 0; k < listTableAll.Count; k++)
				{
					dataGridView3.Rows.Add(1);
					dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[4].Value = listTableAll[k].procedureQuery1;
					dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[9].Value = listTableAll[k].procedureQuery2;

				}
				int i = 0;
				dataGridView3.Columns[i++].ReadOnly = false;

				if (conn == null)
				{
				}
				else if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}

			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
			con.Close();
			con2.Close();
			compare();
		}


		private void showProceaser()
		{
			try
			{

				string connectionDb1 = "Server = " + textBoxIp1.Text + ";Port = " + textBoxPort1.Text + ";Database = " + textBoxDb1.Text + ";username = " + textBoxUn1.Text + ";password = " + textBoxPw1.Text + ";" + "Charset=utf8;";
				string connectionDb2 = "Server = " + textBoxIp2.Text + ";Port = " + textBoxPort2.Text + ";Database = " + textBoxDb2.Text + ";username = " + textBoxUn2.Text + ";password = " + textBoxPw2.Text + ";" + "Charset=utf8;";
				con = new MySqlConnection(connectionDb1);
				con2 = new MySqlConnection(connectionDb2);
				con.Open();
				con2.Open();

				string showProcedure = "show procedure status where DB='" + textBoxDb1.Text + "'";
				MySqlDataReader rdrProcedure = DBConnect(con, showProcedure);
				List<procedureInfo> listTable1 = new List<procedureInfo>();
				List<procedureInfo> listTable2 = new List<procedureInfo>();
				List<procedureInfoAll> listTableAll = new List<procedureInfoAll>();
				while (rdrProcedure.Read())
				{
					procedureInfo listInfo = new procedureInfo() { procedureName = rdrProcedure["Name"].ToString(), procedureCreated = rdrProcedure["Created"].ToString(), procedureUpdate = rdrProcedure["Modified"].ToString(), procedureComment = rdrProcedure["Comment"].ToString() };
					listTable1.Add(listInfo);
				}
				//
				//for (int w = 0; w < listTable1.Count; w++)
				//{
				//string showProcedureQuery = "SHOW CREATE PROCEDURE " + listTable1[0].procedureName;
				//MySqlDataReader rdrProcedure3 = DBConnect(con, showProcedureQuery);
				//while (rdrProcedure.Read())
				//{
				//	procedureInfo listInfo = new procedureInfo() { procedureQuery = rdrProcedure3["Create Procedure"].ToString() };
				//	listTable1.Add(listInfo);
				//}
				//}
				//



				string showProcedure2 = "show procedure status where DB='" + textBoxDb2.Text + "'";
				MySqlDataReader rdrProcedure2 = DBConnect(con2, showProcedure2);
				while (rdrProcedure2.Read())
				{
					procedureInfo listInfo = new procedureInfo() { procedureName = rdrProcedure2["Name"].ToString(), procedureCreated = rdrProcedure2["Created"].ToString(), procedureUpdate = rdrProcedure2["Modified"].ToString(), procedureComment = rdrProcedure2["Comment"].ToString() };
					listTable2.Add(listInfo);
				}


				//
				//for (int q = 0; q < listTable1.Count; q++)
				//{
				//	string showProcedureQuery2 = "SHOW CREATE PROCEDURE " + listTable1[q].procedureName;
				//	MySqlDataReader rdrProcedure4 = DBConnect(con, showProcedureQuery2);
				//	if (rdrProcedure4 == null)
				//	{
				//		continue;
				//	}
				//	while (rdrProcedure4.Read())
				//	{
				//		procedureInfo listInfo = new procedureInfo() { procedureQuery = rdrProcedure4["Create Procedure"].ToString() };
				//		listTable1.Add(listInfo);
				//	}
				//}
				//





				for (int k = 0; k < listTable1.Count; k++)
				{
					listTableAll.Add(new procedureInfoAll()
					{
						procedureName1 = listTable1[k].procedureName,
						procedureCreated1 = listTable1[k].procedureCreated,
						procedureUpdate1 = listTable1[k].procedureUpdate,
						procedureComment1 = listTable1[k].procedureComment,

						procedureQuery1 = listTable1[k].procedureQuery
					});
				}
				for (int k = 0; k < listTable2.Count; k++)
				{
					bool found = false;
					for (int j = 0; j < listTableAll.Count; j++)
					{
						if (listTable2[k].procedureName == listTableAll[j].procedureName1)
						{
							listTableAll[j].procedureName2 = listTable2[k].procedureName;
							listTableAll[j].procedureCreated2 = listTable2[k].procedureCreated;
							listTableAll[j].procedureUpdate2 = listTable2[k].procedureUpdate;
							listTableAll[j].procedureComment2 = listTable2[k].procedureComment;

							listTableAll[j].procedureQuery2 = listTable2[k].procedureQuery;

							found = true;
							break;
						}
					}
					if (found == false)
					{
						listTableAll.Add(new procedureInfoAll()
						{
							procedureName2 = listTable2[k].procedureName,
							procedureCreated2 = listTable2[k].procedureCreated,
							procedureUpdate2 = listTable2[k].procedureUpdate,
							procedureComment2 = listTable2[k].procedureComment,

							procedureQuery2 = listTable2[k].procedureQuery
						});
					}
				}
				dataGridView3.Rows.Clear();
				for (int k = 0; k < listTableAll.Count; k++)
				{
					dataGridView3.Rows.Add(1);
					int a = 0;
					dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[a++].Value = listTableAll[k].procedureName1;
					dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[a++].Value = listTableAll[k].procedureCreated1;
					dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[a++].Value = listTableAll[k].procedureUpdate1;
					dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[a++].Value = listTableAll[k].procedureComment1;
					dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[a++].Value = listTableAll[k].procedureQuery1;

					dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[a++].Value = listTableAll[k].procedureName2;
					dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[a++].Value = listTableAll[k].procedureCreated2;
					dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[a++].Value = listTableAll[k].procedureUpdate2;
					dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[a++].Value = listTableAll[k].procedureComment2;

					dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[a++].Value = listTableAll[k].procedureQuery2;

				}
				int i = 0;
				dataGridView3.Columns[i++].ReadOnly = false;

				if (conn == null)
				{
				}
				else if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}

			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
			con.Close();
			con2.Close();
			compare();
		}



		private void showEvents()
		{
			try
			{
				string connectionDb1 = "Server = " + textBoxIp1.Text + ";Port = " + textBoxPort1.Text + ";Database = " + textBoxDb1.Text + ";username = " + textBoxUn1.Text + ";password = " + textBoxPw1.Text + ";" + "Charset=utf8;";
				string connectionDb2 = "Server = " + textBoxIp2.Text + ";Port = " + textBoxPort2.Text + ";Database = " + textBoxDb2.Text + ";username = " + textBoxUn2.Text + ";password = " + textBoxPw2.Text + ";" + "Charset=utf8;";
				con = new MySqlConnection(connectionDb1);
				con2 = new MySqlConnection(connectionDb2);
				con.Open();
				con2.Open();



				string showEvent = "SELECT * FROM information_schema.EVENTS  WHERE event_schema='" + textBoxDb1.Text + "'";
				MySqlDataReader rdrEvent = DBConnect(con, showEvent);
				List<EventInfo> listTable1 = new List<EventInfo>();
				List<EventInfo> listTable2 = new List<EventInfo>();
				List<EventInfoAll> listTableAll = new List<EventInfoAll>();
				while (rdrEvent.Read())
				{
					EventInfo listInfo = new EventInfo() { EVENT_NAME = rdrEvent["EVENT_NAME"].ToString(), CREATED = rdrEvent["CREATED"].ToString(), LAST_ALTERED = rdrEvent["LAST_ALTERED"].ToString() };
					listTable1.Add(listInfo);
				}

				string showEvent2 = "SELECT * FROM information_schema.EVENTS  WHERE event_schema='" + textBoxDb2.Text + "'";
				MySqlDataReader rdrEvent2 = DBConnect(con2, showEvent2);
				while (rdrEvent2.Read())
				{
					EventInfo listInfo = new EventInfo() { EVENT_NAME = rdrEvent2["EVENT_NAME"].ToString(), CREATED = rdrEvent2["CREATED"].ToString(), LAST_ALTERED = rdrEvent2["LAST_ALTERED"].ToString() };
					listTable2.Add(listInfo);
				}


				for (int k = 0; k < listTable1.Count; k++)
				{
					listTableAll.Add(new EventInfoAll()
					{
						EVENT_NAME1 = listTable1[k].EVENT_NAME,
						CREATED1 = listTable1[k].CREATED,
						LAST_ALTERED1 = listTable1[k].LAST_ALTERED
					});
				}
				for (int k = 0; k < listTable2.Count; k++)
				{
					bool found = false;
					for (int j = 0; j < listTableAll.Count; j++)
					{
						if (listTable2[k].EVENT_NAME == listTableAll[j].EVENT_NAME1)
						{
							listTableAll[j].EVENT_NAME2 = listTable2[k].EVENT_NAME;
							listTableAll[j].CREATED2 = listTable2[k].CREATED;
							listTableAll[j].LAST_ALTERED2 = listTable2[k].LAST_ALTERED;

							found = true;
							break;
						}
					}
					if (found == false)
					{
						listTableAll.Add(new EventInfoAll()
						{
							EVENT_NAME2 = listTable2[k].EVENT_NAME,
							CREATED2 = listTable2[k].CREATED,
							LAST_ALTERED2 = listTable2[k].LAST_ALTERED,
						});
					}
				}
				dataGridView5.Rows.Clear();
				for (int k = 0; k < listTableAll.Count; k++)
				{
					dataGridView5.Rows.Add(1);
					int a = 0;
					dataGridView5.Rows[dataGridView5.Rows.Count - 1].Cells[a++].Value = listTableAll[k].EVENT_NAME1;
					dataGridView5.Rows[dataGridView5.Rows.Count - 1].Cells[a++].Value = listTableAll[k].CREATED1;
					dataGridView5.Rows[dataGridView5.Rows.Count - 1].Cells[a++].Value = listTableAll[k].LAST_ALTERED1;

					dataGridView5.Rows[dataGridView5.Rows.Count - 1].Cells[a++].Value = listTableAll[k].EVENT_NAME2;
					dataGridView5.Rows[dataGridView5.Rows.Count - 1].Cells[a++].Value = listTableAll[k].CREATED2;
					dataGridView5.Rows[dataGridView5.Rows.Count - 1].Cells[a++].Value = listTableAll[k].LAST_ALTERED2;
				}

				int i = 0;
				dataGridView5.Columns[i++].ReadOnly = false;

				if (conn == null)
				{
				}
				else if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}

			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
			con.Close();
			con2.Close();
			compare();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			showFunction();
		}

		private void showFunction()
		{
			try
			{
				string connectionDb1 = "Server = " + textBoxIp1.Text + ";Port = " + textBoxPort1.Text + ";Database = " + textBoxDb1.Text + ";username = " + textBoxUn1.Text + ";password = " + textBoxPw1.Text + ";" + "Charset=utf8;";
				string connectionDb2 = "Server = " + textBoxIp2.Text + ";Port = " + textBoxPort2.Text + ";Database = " + textBoxDb2.Text + ";username = " + textBoxUn2.Text + ";password = " + textBoxPw2.Text + ";" + "Charset=utf8;";

				con = new MySqlConnection(connectionDb1);
				con2 = new MySqlConnection(connectionDb2);
				con.Open();
				con2.Open();

				string showFunction = "show function status where Db='" + textBoxDb1.Text + "'";
				MySqlDataReader rdr = DBConnect(con, showFunction);
				List<ListInfo> listTable1 = new List<ListInfo>();
				List<ListInfo> listTable2 = new List<ListInfo>();
				List<ListInfoAll> listTableAll = new List<ListInfoAll>();

				while (rdr.Read())
				{
					ListInfo listInfo = new ListInfo() { FUNCTION_NAME = rdr["Name"].ToString(), FUNCTION_CREATED = rdr["Created"].ToString(), FUNCTION_MODIFIED = rdr["Modified"].ToString() };
					listTable1.Add(listInfo);
				}
				string showFunction2 = "show function status where Db='" + textBoxDb2.Text + "'";

				MySqlDataReader rdr2 = DBConnect(con2, showFunction2);
				while (rdr2.Read())
				{
					ListInfo listInfo = new ListInfo() { FUNCTION_NAME = rdr2["Name"].ToString(), FUNCTION_CREATED = rdr2["Created"].ToString(), FUNCTION_MODIFIED = rdr2["Modified"].ToString() };
					listTable2.Add(listInfo);
				}

				for (int k = 0; k < listTable1.Count; k++)
				{
					listTableAll.Add(new ListInfoAll()
					{
						FUNCTION_NAME1 = listTable1[k].FUNCTION_NAME,
						FUNCTION_CREATED1 = listTable1[k].FUNCTION_CREATED,
						FUNCTION_MODIFIED1 = listTable1[k].FUNCTION_MODIFIED
					});
				}
				for (int k = 0; k < listTable2.Count; k++)
				{
					bool found = false;
					for (int j = 0; j < listTableAll.Count; j++)
					{
						if (listTable2[k].FUNCTION_NAME == listTableAll[j].FUNCTION_NAME1)
						{
							listTableAll[j].FUNCTION_NAME2 = listTable2[k].FUNCTION_NAME;
							listTableAll[j].FUNCTION_CREATED2 = listTable2[k].FUNCTION_CREATED;
							listTableAll[j].FUNCTION_MODIFIED2 = listTable2[k].FUNCTION_MODIFIED;

							found = true;
							listTableAll[j].db = "A+B";
							break;
						}
					}
					//listTableAll.Add(new ListInfoAll() { db = "A", tableName = listTable1[k].tableName, fieldCount1 = listTable1[k].fieldCount, fieldCount2 = 0 });
					if (found == false)
					{
						listTableAll.Add(new ListInfoAll()
						{
							FUNCTION_NAME2 = listTable2[k].FUNCTION_NAME,
							FUNCTION_CREATED2 = listTable2[k].FUNCTION_CREATED,
							FUNCTION_MODIFIED2 = listTable2[k].FUNCTION_MODIFIED,
						});
					}
				}
				dataGridView7.Rows.Clear();
				for (int k = 0; k < listTableAll.Count; k++)
				{
					dataGridView7.Rows.Add(1);
					int a = 0;
					dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells[a++].Value = listTableAll[k].FUNCTION_NAME1;
					dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells[a++].Value = listTableAll[k].FUNCTION_CREATED1;
					dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells[a++].Value = listTableAll[k].FUNCTION_MODIFIED1;

					dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells[a++].Value = listTableAll[k].FUNCTION_NAME2;
					dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells[a++].Value = listTableAll[k].FUNCTION_CREATED2;
					dataGridView7.Rows[dataGridView7.Rows.Count - 1].Cells[a++].Value = listTableAll[k].FUNCTION_MODIFIED2;
				}

				int i = 0;
				dataGridView7.Columns[i++].ReadOnly = false;
				compare();
				if (conn == null)
				{
				}
				else if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
			con.Close();
			con2.Close();
		}

		void showQueryProcedure()
		{

			try
			{
				string connectionDb1 = "Server = " + textBoxIp1.Text + ";Port = " + textBoxPort1.Text + ";Database = " + textBoxDb1.Text + ";username = " + textBoxUn1.Text + ";password = " + textBoxPw1.Text + ";" + "Charset=utf8;";
				string connectionDb2 = "Server = " + textBoxIp2.Text + ";Port = " + textBoxPort2.Text + ";Database = " + textBoxDb2.Text + ";username = " + textBoxUn2.Text + ";password = " + textBoxPw2.Text + ";" + "Charset=utf8;";
				con = new MySqlConnection(connectionDb1);
				con2 = new MySqlConnection(connectionDb2);
				con.Open();
				con2.Open();

				for (int s = 0; s < dataGridView3.Rows.Count; s++)
				{
					if (dataGridView3.Rows[s].Cells[0].Value == null)
					{
						continue;
					}

					string tbl = dataGridView3.Rows[s].Cells[0].Value.ToString();

					string queryCreate = "SHOW CREATE PROCEDURE " + tbl;

					MySqlDataReader rdr = DBConnect(con, queryCreate);


					List<procedureInfo> listTable1 = new List<procedureInfo>();
					List<procedureInfo> listTable2 = new List<procedureInfo>();
					List<procedureInfoAll> listTableAll = new List<procedureInfoAll>();

					if (rdr == null)
					{
						continue;
					}
					while (rdr.Read())
					{
						procedureInfo listInfo = new procedureInfo() { procedureQuery = rdr["Create Procedure"].ToString() };
						listTable1.Add(listInfo);
					}

					
					MySqlDataReader rdr2 = DBConnect(con2, queryCreate);
					while (rdr2.Read())
					{
						procedureInfo listInfo = new procedureInfo() { procedureQuery = rdr2["Create Procedure"].ToString() };
						listTable2.Add(listInfo);
					}

					for (int k = 0; k < listTable1.Count; k++)
					{
						listTableAll.Add(new procedureInfoAll()
						{
							procedureQuery1 = listTable1[k].procedureQuery
						});
					}
					for (int k = 0; k < listTable2.Count; k++)
					{
						bool found = false;
						for (int j = 0; j < listTableAll.Count; j++)
						{
							if (listTable2[k].procedureName == listTableAll[j].procedureName1)
							{
								listTableAll[j].procedureQuery2 = listTable2[k].procedureQuery;

								found = true;
								break;
							}
						}
						if (found == false)
						{
							listTableAll.Add(new procedureInfoAll()
							{
								procedureQuery2 = listTable2[k].procedureQuery
							});
						}
					}
					dataGridView3.Rows.Add(1);
					dataGridView3.Rows[s].Cells[4].Value = listTableAll[s].procedureQuery1;
					dataGridView3.Rows[s].Cells[9].Value = listTableAll[s].procedureQuery2;

				}

				int i = 0;
				dataGridView3.Columns[i++].ReadOnly = false;



			}

			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
			con.Close();
			con2.Close();
			compare();

		}




		private void showView()
		{
			try
			{
				string connectionDb1 = "Server = " + textBoxIp1.Text + ";Port = " + textBoxPort1.Text + ";Database = " + textBoxDb1.Text + ";username = " + textBoxUn1.Text + ";password = " + textBoxPw1.Text + ";" + "Charset=utf8;";
				string connectionDb2 = "Server = " + textBoxIp2.Text + ";Port = " + textBoxPort2.Text + ";Database = " + textBoxDb2.Text + ";username = " + textBoxUn2.Text + ";password = " + textBoxPw2.Text + ";" + "Charset=utf8;";

				con = new MySqlConnection(connectionDb1);
				con2 = new MySqlConnection(connectionDb2);
				con.Open();
				con2.Open();

				string showView = "SELECT TABLE_NAME viewName FROM information_schema.`TABLES` WHERE TABLE_TYPE LIKE 'VIEW' AND TABLE_SCHEMA LIKE '"+ textBoxDb1.Text + "';";
				string showView2 = "SELECT TABLE_NAME viewName FROM information_schema.`TABLES` WHERE TABLE_TYPE LIKE 'VIEW' AND TABLE_SCHEMA LIKE '" + textBoxDb2.Text + "';";


				MySqlDataReader rdr = DBConnect(con, showView);
				List<ListInfo> listTable1 = new List<ListInfo>();
				List<ListInfo> listTable2 = new List<ListInfo>();
				List<ListInfoAll> listTableAll = new List<ListInfoAll>();

				while (rdr.Read())
				{
					ListInfo listInfo = new ListInfo() { viewName = rdr["viewName"].ToString() };
					listTable1.Add(listInfo);
					// dataGridView1.Rows[i].Cells[1].Value = rdr["cnt"].ToString();
				}

				MySqlDataReader rdr2 = DBConnect(con2, showView2);
				while (rdr2.Read())
				{
					ListInfo listInfo = new ListInfo() { viewName = rdr2["viewName"].ToString() };
					listTable2.Add(listInfo);
				}


				for (int k = 0; k < listTable1.Count; k++)
				{
					listTableAll.Add(new ListInfoAll()
					{
						viewName1 = listTable1[k].viewName,
					});
				}
				for (int k = 0; k < listTable2.Count; k++)
				{
					bool found = false;
					for (int j = 0; j < listTableAll.Count; j++)
					{
						if (listTable2[k].viewName == listTableAll[j].viewName1)
						{
							listTableAll[j].viewName2 = listTable2[k].viewName;

							found = true;
							listTableAll[j].db = "A+B";
							break;
						}
					}
					//listTableAll.Add(new ListInfoAll() { db = "A", tableName = listTable1[k].tableName, fieldCount1 = listTable1[k].fieldCount, fieldCount2 = 0 });
					if (found == false)
					{
						listTableAll.Add(new ListInfoAll()
						{
							viewName2 = listTable2[k].viewName
						});
					}
				}
				dataGridView9.Rows.Clear();
				for (int k = 0; k < listTableAll.Count; k++)
				{
					dataGridView9.Rows.Add(1);
					int a = 0;
					dataGridView9.Rows[dataGridView9.Rows.Count - 1].Cells[a++].Value = listTableAll[k].viewName1;

					dataGridView9.Rows[dataGridView9.Rows.Count - 1].Cells[a++].Value = listTableAll[k].viewName2;

				}

				int i = 0;
				dataGridView9.Columns[i++].ReadOnly = false;
				compare();


				if (conn == null)
				{
				}
				else if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}

			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
			con.Close();
			con2.Close();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			

		}

		private void procedureCreateTable(string queryCreate)
		{
			try
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

				StringBuilder _strArg = new StringBuilder("");
				StringBuilder _strArg2 = new StringBuilder("");
				_strArg.Append("Server = ");           // SQL
				_strArg.Append(_HostName);        // 서버
				_strArg.Append(";Port = ");
				_strArg.Append(_PORT);                 // 포트
				_strArg.Append(";Database = ");
				_strArg.Append(_DATABASE);          // 데이터베이스
				_strArg.Append(";username = ");
				_strArg.Append(_ID);                     // ID
				_strArg.Append(";password = ");
				_strArg.Append(_PWD);                 // PWD
				_strArg.Append(";");
				_strArg.Append("Charset=utf8;");

				_strArg2.Append("Server = ");           // SQL
				_strArg2.Append(_HostName2);        // 서버
				_strArg2.Append(";Port = ");
				_strArg2.Append(_PORT2);                 // 포트
				_strArg2.Append(";Database = ");
				_strArg2.Append(_DATABASE2);          // 데이터베이스
				_strArg2.Append(";username = ");
				_strArg2.Append(_ID2);                     // ID
				_strArg2.Append(";password = ");
				_strArg2.Append(_PWD2);                 // PWD
				_strArg2.Append(";");
				_strArg2.Append("Charset=utf8;");

				MySqlConnection con = new MySqlConnection(_strArg.ToString());
				con.Open();

				MySqlDataReader rdr = DBConnect(con, queryCreate);
				List<Columns> listTable1 = new List<Columns>();
				List<Columns> listTable2 = new List<Columns>();
				List<ColumnsAll> listTableAll = new List<ColumnsAll>();
				Delay(200);

				while (rdr.Read())
				{
					Columns listInfo = new Columns() { CREATETABLE = rdr["Create Procedure"].ToString() };
					listTable1.Add(listInfo);
					
				}
			
				string createQuery = listTable1[0].CREATETABLE;
				Create(createQuery);
				con.Close();
			}
			catch (Exception e)
			{
				LogMgr.ExceptionLog(e);
				MessageBox.Show(e.ToString());
			}
		}

		private void dataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			try
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
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}

		private void button7_Click(object sender, EventArgs e)
		{
			showQueryProcedure();
		}

	
		private void eventCreateTable(string queryCreate)
		{
			try
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

				StringBuilder _strArg = new StringBuilder("");
				StringBuilder _strArg2 = new StringBuilder("");
				_strArg.Append("Server = ");           // SQL
				_strArg.Append(_HostName);        // 서버
				_strArg.Append(";Port = ");
				_strArg.Append(_PORT);                 // 포트
				_strArg.Append(";Database = ");
				_strArg.Append(_DATABASE);          // 데이터베이스
				_strArg.Append(";username = ");
				_strArg.Append(_ID);                     // ID
				_strArg.Append(";password = ");
				_strArg.Append(_PWD);                 // PWD
				_strArg.Append(";");
				_strArg.Append("Charset=utf8;");

				_strArg2.Append("Server = ");           // SQL
				_strArg2.Append(_HostName2);        // 서버
				_strArg2.Append(";Port = ");
				_strArg2.Append(_PORT2);                 // 포트
				_strArg2.Append(";Database = ");
				_strArg2.Append(_DATABASE2);          // 데이터베이스
				_strArg2.Append(";username = ");
				_strArg2.Append(_ID2);                     // ID
				_strArg2.Append(";password = ");
				_strArg2.Append(_PWD2);                 // PWD
				_strArg2.Append(";");
				_strArg2.Append("Charset=utf8;");

				MySqlConnection con = new MySqlConnection(_strArg.ToString());
				con.Open();

				MySqlDataReader rdr = DBConnect(con, queryCreate);
				List<Columns> listTable1 = new List<Columns>();
				List<Columns> listTable2 = new List<Columns>();
				List<ColumnsAll> listTableAll = new List<ColumnsAll>();
				Delay(200);

				while (rdr.Read())
				{
					Columns listInfo = new Columns() { CREATETABLE = rdr["Create Event"].ToString() };
					listTable1.Add(listInfo);
				}
				string createQuery = listTable1[0].CREATETABLE;
				Create(createQuery);
				con.Close();
			}
			catch (Exception e)
			{
				LogMgr.ExceptionLog(e);
				MessageBox.Show(e.ToString());
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
					}

		private void functionCreateTable(string queryCreate)
		{
			try
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

				StringBuilder _strArg = new StringBuilder("");
				StringBuilder _strArg2 = new StringBuilder("");
				_strArg.Append("Server = ");           // SQL
				_strArg.Append(_HostName);        // 서버
				_strArg.Append(";Port = ");
				_strArg.Append(_PORT);                 // 포트
				_strArg.Append(";Database = ");
				_strArg.Append(_DATABASE);          // 데이터베이스
				_strArg.Append(";username = ");
				_strArg.Append(_ID);                     // ID
				_strArg.Append(";password = ");
				_strArg.Append(_PWD);                 // PWD
				_strArg.Append(";");
				_strArg.Append("Charset=utf8;");

				_strArg2.Append("Server = ");           // SQL
				_strArg2.Append(_HostName2);        // 서버
				_strArg2.Append(";Port = ");
				_strArg2.Append(_PORT2);                 // 포트
				_strArg2.Append(";Database = ");
				_strArg2.Append(_DATABASE2);          // 데이터베이스
				_strArg2.Append(";username = ");
				_strArg2.Append(_ID2);                     // ID
				_strArg2.Append(";password = ");
				_strArg2.Append(_PWD2);                 // PWD
				_strArg2.Append(";");
				_strArg2.Append("Charset=utf8;");

				MySqlConnection con = new MySqlConnection(_strArg.ToString());
				con.Open();

				MySqlDataReader rdr = DBConnect(con, queryCreate);
				List<Columns> listTable1 = new List<Columns>();
				List<Columns> listTable2 = new List<Columns>();
				List<ColumnsAll> listTableAll = new List<ColumnsAll>();
				Delay(200);

				while (rdr.Read())
				{
					Columns listInfo = new Columns() { CREATETABLE = rdr["Create Function"].ToString() };
					listTable1.Add(listInfo);
				}
				string createQuery = listTable1[0].CREATETABLE;
				Create(createQuery);
				con.Close();
			}
			catch (Exception e)
			{
				LogMgr.ExceptionLog(e);
				MessageBox.Show(e.ToString());
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
		
		}

		private void viewCreateTable(string queryCreate)
		{
			try
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

				StringBuilder _strArg = new StringBuilder("");
				StringBuilder _strArg2 = new StringBuilder("");
				_strArg.Append("Server = ");           // SQL
				_strArg.Append(_HostName);        // 서버
				_strArg.Append(";Port = ");
				_strArg.Append(_PORT);                 // 포트
				_strArg.Append(";Database = ");
				_strArg.Append(_DATABASE);          // 데이터베이스
				_strArg.Append(";username = ");
				_strArg.Append(_ID);                     // ID
				_strArg.Append(";password = ");
				_strArg.Append(_PWD);                 // PWD
				_strArg.Append(";");
				_strArg.Append("Charset=utf8;");

				_strArg2.Append("Server = ");           // SQL
				_strArg2.Append(_HostName2);        // 서버
				_strArg2.Append(";Port = ");
				_strArg2.Append(_PORT2);                 // 포트
				_strArg2.Append(";Database = ");
				_strArg2.Append(_DATABASE2);          // 데이터베이스
				_strArg2.Append(";username = ");
				_strArg2.Append(_ID2);                     // ID
				_strArg2.Append(";password = ");
				_strArg2.Append(_PWD2);                 // PWD
				_strArg2.Append(";");
				_strArg2.Append("Charset=utf8;");

				MySqlConnection con = new MySqlConnection(_strArg.ToString());
				con.Open();

				MySqlDataReader rdr = DBConnect(con, queryCreate);
				List<Columns> listTable1 = new List<Columns>();
				List<Columns> listTable2 = new List<Columns>();
				List<ColumnsAll> listTableAll = new List<ColumnsAll>();
				Delay(200);

				while (rdr.Read())
				{
					Columns listInfo = new Columns() { CREATETABLE = rdr["Create View"].ToString() };
					listTable1.Add(listInfo);
				}
				string createQuery = listTable1[0].CREATETABLE;
				Create(createQuery);

				



				con.Close();
			}
			catch (Exception e)
			{
				LogMgr.ExceptionLog(e);
				MessageBox.Show(e.ToString());
			}
		}

		private void dataGridView5_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			try
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
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}

		private void dataGridView7_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			try
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
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}

		private void dataGridView9_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			try
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
			catch (Exception ex)
			{
				LogMgr.ExceptionLog(ex);
			}
		}
	}
}


