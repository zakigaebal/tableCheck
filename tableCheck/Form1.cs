using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
    //MySQL mysql = new MySQL("localhost", "dawoon", "root", "ekdnsel");
    bool result = false;


    DataTable dt = new DataTable();

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

      this.FormClosed += Form_Closing;


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

      button2_Click(sender, e);
      dataGridView2.Rows.Add(1);
      dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Value = 50;

      dt.Columns.Add("체크", typeof(bool)); // 선택 체크박스 용

      iniload();
      //	button1_Click(sender, e);

      GenerateData();
      dataGridView2.DataSource = _infoList;

      //			dataGridView2.Columns.Insert(3, column);

      //	dataGridView2.Rows.a
      //	dataGridView2.Rows[0].Cells[0].Value = "1";

      //		column.HeaderText = "Progress";

      dgvDesign();
    }
    // checkBox 변경 상태를 확실히 반영하기 위함.

    private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
      if (dataGridView1.CurrentCell is DataGridViewCheckBoxCell)
      {
        dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
      }
    }

    private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
    {// row header 에 자동 일련번호 넣기
      StringFormat drawFormat = new StringFormat();
      //drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
      drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

      using (Brush brush = new SolidBrush(Color.Red))
      {
        e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font,
brush, e.RowBounds.Location.X + 35, e.RowBounds.Location.Y + 4, drawFormat);
      }
    }


    void dgvDesign()
    {
      //컬럼 수정 못하게 하기
      //this.dataGridView1.Columns[1].ReadOnly = true;

      //마우스로 row header width 조절 못하게 하기.
      this.dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

      //마우스로 column size 조절 못하게 하기
      //this.dataGridView1.Columns[0].Resizable = DataGridViewTriState.False;

      //dataGridView1.ReadOnly = true;
      //dataGridView1.RowHeadersVisible = false;

      this.dataGridView1.AllowUserToAddRows = false;
      //this.dataGridView1.Columns[0].Width = 20;
      this.dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      //this.dataGridView1.Columns[0].Resizable = DataGridViewTriState.False;
      //	this.dataGridView1.Columns[1].ReadOnly = true;
      //this.dataGridView1.Columns[2].ReadOnly = true;
      //	this.dataGridView1.Columns[3].ReadOnly = true;

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

          da.Fill(dt);
          dataGridView1.DataSource = dt;
          connection.Close();

          //dataGridView1.Columns[0].Width = 40;
          //dataGridView1.Columns[1].Width = 280;
          dt.Columns.Add("필드수");
          dt.Columns.Add("progressbar");
          dataGridView2.Rows.Add(1);



          dt.Columns.Add("상태");
          connection.Open();
          for (int i = 0; i < dataGridView1.Rows.Count; i++)
          {
            string fieldSql = "SELECT COUNT(*) cnt	FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + dataGridView1.Rows[i].Cells[0].Value + "';";
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



          //dataGridView2.Columns.Insert(3, column);
          // 선택 체크박스 용 _dt.Columns.Add("종목코드", typeof(string));
          // _dt.Columns.Add("종목명", typeof(string));
          // _dt.Columns.Add("가격", typeof(int));
          // foreach (var stock in _stockList)
          // { _dt.Rows.Add(false, stock.StockCode, stock.StockName, stock.Price); }



        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.ToString(), "실패");
        }

      }
    }
    private void progressBar1_Click(object sender, EventArgs e)
    {

    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void label4_Click(object sender, EventArgs e)
    {

    }

    private void textBox4_TextChanged(object sender, EventArgs e)
    {

    }

    private void textBox9_TextChanged(object sender, EventArgs e)
    {

    }



    private void buttonConnect_Click(object sender, EventArgs e)
    {
      if (textBoxIp1.Text == "")
      {
        MessageBox.Show("데이터베이스 서버를 입력해주세요.");
        this.textBoxIp1.Focus();
        return;
      }
      if (this.textBoxPort1.Text == "")
      {
        MessageBox.Show("Port번호를 입력해 주십시오.");
        this.textBoxPort1.Focus();
        return;
      }
      if (this.textBoxDb1.Text == "")
      {
        MessageBox.Show("Database를 입력해 주십시오.");
        this.textBoxDb1.Focus();
        return;
      }
      if (this.textBoxUn1.Text == "")
      {
        MessageBox.Show("ID를 입력해 주십시오.");
        this.textBoxUn1.Focus();
        return;
      }
      if (this.textBoxPw1.Text == "")
      {
        MessageBox.Show("Password를 입력해 주십시오.");
        this.textBoxPw1.Focus();
        return;
      }
      btn_TEST_Click(sender, e);

    }


    //연결테스트 버튼 클릭
    private void btn_TEST_Click(object sender, EventArgs e)
    {

      _HostName = this.textBoxIp1.Text;
      _PORT = this.textBoxPort1.Text;
      _DATABASE = this.textBoxDb1.Text;
      _ID = this.textBoxUn1.Text;
      _PWD = this.textBoxPw1.Text;

      _HostName2 = this.textBoxIp2.Text;
      _PORT2 = this.textBoxPort2.Text;
      _DATABASE2 = this.textBoxDb2.Text;
      _ID2 = this.textBoxUn2.Text;
      _PWD2 = this.textBoxPw2.Text;

      this.DBConnectTest(_HostName, _PORT, _DATABASE, _ID, _PWD);
     // this.DBConnectTest(_HostName2, _PORT2, _DATABASE2, _ID2, _PWD2);
    }

    //DBConnectTest 메소드
    private void DBConnectTest(string hostname, string port, string database, string id, string pwd)
    {
      //_HostName = hostname;
      _DATABASE = database;
      _PORT = port;
      _ID = id;
      _PWD = pwd;

      StringBuilder _strArg = new StringBuilder("");
      _strArg.Append("Server = ");           // SQL
      _strArg.Append(_ServerName);        // 서버
      _strArg.Append(";Port = ");
      _strArg.Append(_PORT);                 // 포트
      _strArg.Append(";Database = ");
      _strArg.Append(_DATABASE);          // 데이터베이스
      _strArg.Append(";username = ");
      _strArg.Append(_ID);                     // ID
      _strArg.Append(";password = ");
      _strArg.Append(_PWD);                 // PWD
      _strArg.Append(";");

      MySqlConnection conn = new MySqlConnection(_strArg.ToString());

      try
      {
        conn.Open();
        string sql = "select a.Table_NAME as 'table', count(b.table_name) as 'cnt'  from INFORMATION_SCHEMA.tables a left outer join information_schema.columns b on a.Table_NAME = b.table_name where a.table_schema='dawoon' group by b.table_name order by '테이블명' desc;";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
          // 첫번째 방법
          //string[] row01 = {false,"","","","" };
          //row0[0] = rdr["cr_no"].ToString();
          //row0[1] = rdr["cr_fnumber"].ToString();
          //row0[2] = rdr["cr_number"].ToString();
          //row0[3] = rdr["cr_tongs"].ToString();
          // row0[4] = rdr["cr_date"].ToString();
          // 두번째 방법
          string row0 = rdr["table"].ToString();
          string row1 = rdr["cnt"].ToString();

          // dataGridView1.Rows[i].Cells[1].Value = rdr["cnt"].ToString();
          dataGridView1.Rows.Add(false, row0, row1, "", "");
        }
        rdr.Close();
        conn.Close();
      }
      catch (Exception Ex)
      {
        conn.Close();
        MessageBox.Show(Ex.ToString());
        //MessageBox.Show("DB 접속이 불가능합니다.");
        //isTested = false;
      }
      finally
      {
        conn.Close();
      }
    }

    // 체크박스 추가
    DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
    //버튼 추가
    DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
    //프로그레스 추가
    DataGridViewProgressColumn progressColumn = new DataGridViewProgressColumn();





    private void button2_Click(object sender, EventArgs e)
    {
      checkBoxColumn.HeaderText = "체크";
      checkBoxColumn.Name = "check";

      buttonColumn.HeaderText = "Button";
      buttonColumn.Name = "button";

      progressColumn.HeaderText = "진행중";
      progressColumn.Name = "progress";

      dataGridView1.Columns.Add(checkBoxColumn);
      dataGridView1.Columns.Add("column0", "DB1");
      dataGridView1.Columns.Add("column1", "필드수");
      dataGridView1.Columns.Add("column2", "DB2");
      dataGridView1.Columns.Add("column3", "필드수");
      dataGridView1.Columns.Add(progressColumn);
      dataGridView1.Columns.Add("column4", "상태");
      dataGridView1.Columns.Add(buttonColumn);

      dataGridView1.Columns[0].Width = 50;
      dataGridView1.Columns[1].Width = 200;
      dataGridView1.Columns[2].Width = 80;
      dataGridView1.Columns[3].Width = 80;
      dataGridView1.Columns[4].Width = 200;
      dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      dataGridView1.Columns[6].Width = 100;
      dataGridView1.Columns[7].Width = 50;

      dataGridView1.AllowUserToAddRows = false;
      dataGridView1.Rows.Add();

      dataGridView1[0, 0].Value = false;
      dataGridView1[1, 0].Value = "수정";
      dataGridView1[2, 0].Value = "수정";
      dataGridView1[3, 0].Value = "수정";
      dataGridView1[4, 0].Value = "수정";
      dataGridView1[5, 0].Value = 50;
      dataGridView1[6, 0].Value = "비교하기";
      dataGridView1[7, 0].Value = "수정";


    }

    private void buttonConnect2_Click(object sender, EventArgs e)
    {
      //연결 Test
      //result = mysql.DBConnection();

      if (result == true)
      {
        MessageBox.Show("연결 성공");
        string str_result = "";
        //검색
        //str_result = mysql.Select_Sql("select * from INFORMATION_SCHEMA.tables where table_schema='dawoon2' group by Table_NAME");


        if (textBoxIp2.Text == "")
        {
          MessageBox.Show("데이터베이스 서버를 입력해주세요.");
          this.textBoxIp2.Focus();
          return;
        }
        if (this.textBoxPort2.Text == "")
        {
          MessageBox.Show("Port번호를 입력해 주십시오.");
          this.textBoxPort2.Focus();
          return;
        }
        if (this.textBoxDb2.Text == "")
        {
          MessageBox.Show("Database를 입력해 주십시오.");
          this.textBoxDb2.Focus();
          return;
        }
        if (this.textBoxUn2.Text == "")
        {
          MessageBox.Show("ID를 입력해 주십시오.");
          this.textBoxUn2.Focus();
          return;
        }
        if (this.textBoxPw1.Text == "")
        {
          MessageBox.Show("Password를 입력해 주십시오.");
          this.textBoxPw2.Focus();
          return;
        }
        btn_TEST_Click(sender, e);




      }
      else
      {
        MessageBox.Show("연결 실패");
      }
    }

    private void buttonSearch_Click(object sender, EventArgs e)
    {
      string str_result = "";
      //검색
      //str_result = mysql.Select_Sql("select * from INFORMATION_SCHEMA.tables where table_schema='dawoon2' group by Table_NAME");

      MessageBox.Show(str_result);
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
			var data = new List<Data>() {
			new Data() { Progress = 50 },
			new Data() { Progress = 60 },
			new Data() { Progress = 30 },
			new Data() { Progress = 92 },
};

			this.dataGridView1.DataSource = data;

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
 */