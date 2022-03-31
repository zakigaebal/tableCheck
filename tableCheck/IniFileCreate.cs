using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tableCheck
{
	public class IniFileCreate
	{
		string startupPath = Application.StartupPath + @"\tableCheck.ini";
		string mc = "tableCheck";

		#region ini 입력 메소드
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
		#endregion


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

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

		}
		public void initCloseMethod()
		{
			// ini파일에 등록
			// WritePrivateProfileString("카테고리", "Key값", "Value", "저장할 경로");

		}
	}
}
