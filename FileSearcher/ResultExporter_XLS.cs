/*
 * 由SharpDevelop创建。
 * 用户： yondor_74
 * 日期: 2017/11/13
 * 时间: 11:14
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
using ClosedXML.Excel;
namespace FileSearcher
{
	/// <summary>
	/// Description of ResultExporter_XLS.
	/// </summary>
	public class ResultExporter_XLS : IExporter
	{
		private Regex reg = new Regex(@"[:\\]+");
		private string date;
		private XLWorkbook book;
		private IXLWorksheet sheet;
		private string _curSheet;
		private int _curRow;
		private List<FileRecord> list;
		public ResultExporter_XLS()
		{
			
			
		}
		
		public void init(){
			
		}
		
		
		public bool createFile(string path){
			
			
			if(book==null){
				
				try{
					date = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");
					book = new XLWorkbook();
					sheet = initWorkSheet(path);
					book.SaveAs("搜索结果_"+date+".xlsx");
				}catch(Exception err){
					trace(err.StackTrace);
					return false;
				}
				
				
			}else{
				sheet = initWorkSheet(path);
			}
			_curRow = 2;
			list = new List<FileRecord>();
			_curSheet = path;
			return true;
		}
		
		private IXLWorksheet initWorkSheet(string sheetname){
			IXLWorksheet ws = book.AddWorksheet(reg.Replace(sheetname , "_"));
			ws.Columns("A").Width = 50;
			ws.Columns("B").Width = 80;
			ws.Columns("C").Width = 20;
			ws.Cell("A1").Value = "文件名";
			ws.Cell("B1").Value = "文件路径";
			ws.Cell("C1").Value = "文件类型";
			return ws;
		}
		
		public bool addFile(string filename , string path , string ext){
			list.Add(new FileRecord(filename , path , ext));
			return false;
		}
		
		public bool write(string content){
			return false;
		}
		
		public void flush(){
			if(sheet == null)return;
			foreach(FileRecord rec in list){
				trace(rec);
				sheet.Cell("A"+_curRow).Value = rec.name;
				sheet.Cell("B"+_curRow).Value = rec.path;
				sheet.Cell("C"+_curRow).Value = rec.ext;
				_curRow++;
			}
			list.Clear();
			book.Save();
		}
		
		public void save(){
			book = null;
			sheet = null;
			_curSheet="";
			_curRow=1;
		}
		void trace(Object str){
			System.Diagnostics.Debug.WriteLine(str);
		}
	}
}
