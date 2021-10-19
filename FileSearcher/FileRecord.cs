/*
 * 由SharpDevelop创建。
 * 用户： yondor_74
 * 日期: 2017/11/13
 * 时间: 11:02
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace FileSearcher
{
	/// <summary>
	/// Description of FileRecord.
	/// </summary>
	public class FileRecord
	{
		public string name{ get; private set; }
		public string path{ get; private set; }
		public string ext{ get; private set; }
		public FileRecord(string n , string p , string e)
		{
			name = n;
			path = p;
			ext = e;
		}
		
		override public string ToString(){
			return "文件扫描记录："+path+"/"+name;
		}
	}
}
