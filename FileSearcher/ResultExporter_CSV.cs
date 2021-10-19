/*
 * 由SharpDevelop创建。
 * 用户： yondor_74
 * 日期: 2017/11/10
 * 时间: 17:36
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
//using CsvHelper;
namespace FileSearcher
{
	/// <summary>
	/// Description of ResultExporter_CSV.
	/// </summary>
	public class ResultExporter_CSV: IExporter
	{
		private string _path;
		public ResultExporter_CSV()
		{
			
			
		}
		
		public void init(){}
		
		public bool createFile(string path){
			_path = path;
			return true;
		}
		
		public bool addFile(string filename , string path , string ext){
			return false;
		}
		
		public bool write(string content){
			return false;
		}
		
		public void flush(){
			
		}
		
		public void save(){
			
		}
	}
}
