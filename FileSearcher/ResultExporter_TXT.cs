/*
 * 由SharpDevelop创建。
 * 用户： yondor_74
 * 日期: 2017/11/9
 * 时间: 14:34
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO; 
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Text;
namespace FileSearcher
{
	/// <summary>
	/// Description of ResulExporter.
	/// </summary>
	public class ResultExporter_TXT : IExporter
	{

		private FileStream fs;
		private StreamWriter sw;
		private Regex reg = new Regex(@"[:\\]+");
		private string resultToWrite;
		private string curPath;
		public ResultExporter_TXT()
		{
			
		}
		
		public void init(){}
		
		public bool createFile(string path){
			if(fs!=null && sw!=null){
				sw.Close();
				fs.Close();
			}			
			string p = "搜索结果_" + reg.Replace(path , "_") + "_"+ DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒") +".txt";
			resultToWrite="";
			
			try{
				fs = new FileStream(p , FileMode.Create);
				sw = new StreamWriter(fs);	
			}catch(Exception err){
				trace(err.StackTrace);
				return false;
			}
			
			return true;
		}
				
		public bool addFile(string filename , string path , string ext){
			if(curPath!=path){
				//改变了扫描文件夹 ， 添加标识
				curPath = path;
				resultToWrite += "\r\n\r\n--------------\r\n"+path + "\r\n--------------";				
			}
			resultToWrite += "\r\n"+filename;
			return true;
		}
		
		public bool write(string content){
			if(fs==null || sw==null)return false;
			try{
				sw.Write(content);
				sw.Flush();	
			}catch(Exception err){
				trace(err.StackTrace);
				return false;
			}
			return true;	
		}
		
		public void flush(){
			write(resultToWrite);
		}
		
		public void save(){
			if(fs==null || sw==null)return;
			sw.Close();
			fs.Close();
		}
		
		void trace(Object str){
			System.Diagnostics.Debug.WriteLine(str);
		}
		

		
	}
}
