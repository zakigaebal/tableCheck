using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tableCheck
{
	public class LogMgr

	{
		static string prefixMsg = "\r\n-------------------------------------------";
		// 일반 로그 남기기
		static public void Log(string msg)
		{
			try
			{
				string currentPath = System.IO.Directory.GetCurrentDirectory();
				string logName = @"" + currentPath + "\\Log\\" + "Error-MQTT-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
				string logFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
				StreamWriter tw = File.AppendText(logName);
				tw.WriteLine(prefixMsg);
				tw.WriteLine(msg);
				tw.Close();
			}
			catch (Exception)
			{
			}
		}
		// 예외 로그
		static public void ExceptionLog(Exception ex, string titleString = null)
		{
			string msg = "";
			if (!string.IsNullOrEmpty(msg))
			{
				msg = titleString + "\r\n";
			}
			msg += GetExceptionMessage(ex);
			msg = "예외 발생 일자 : " + DateTime.Now.ToString() + msg + "\r\n";
			Log(msg);
		}
		// 예외 메세지 만들기
		static private string GetExceptionMessage(Exception ex)
		{
			string err = "\r\n 에러 발생 원인 : " + ex.Source +
				"\r\n 에러 메시지 :" + ex.Message +
				"\r\n Stack Trace : " + ex.StackTrace.ToString();
			if (ex.InnerException != null)
			{
				err += GetExceptionMessage(ex.InnerException);
			}
			return err;
		}

		static public void mainLog(string msg)
		{
			try
			{
				string currentPath = System.IO.Directory.GetCurrentDirectory();
				string logName = @"" + currentPath + "\\Log\\" + "Main-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
				string logFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
				StreamWriter tw = File.AppendText(logName);
				//tw.WriteLine(prefixMsg);
				tw.WriteLine(msg);
				tw.Close();
			}
			catch (Exception)
			{
			}
		}
		static public void mainLogInfo(string msg, string titleString = null)
		{

			if (!string.IsNullOrEmpty(msg))
			{
				msg = titleString;
			}
			msg = "예외 발생 일자 : " + DateTime.Now.ToString() + msg + "\r\n";
			mainLog(msg);
		}

	}
}
