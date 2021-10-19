/*
 * 由SharpDevelop创建。
 * 用户： yondor_74
 * 日期: 2017/11/6
 * 时间: 11:16
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Windows.Forms;
using System.Threading;
namespace FileSearcher
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			Application.Run(new MainForm());
		}
		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs   e){
			System.Diagnostics.Debug.WriteLine(e.Exception.StackTrace);
			ResultExporter_TXT erroutput = new ResultExporter_TXT();
			erroutput.createFile("错误输出");
			erroutput.write(e.Exception.StackTrace);
		}
		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs   e){
		
			System.Diagnostics.Debug.WriteLine(e.ExceptionObject.ToString());
			ResultExporter_TXT erroutput = new ResultExporter_TXT();
			erroutput.createFile("错误输出");
			erroutput.write(e.ExceptionObject.ToString());
		}
		
		
	}
}
