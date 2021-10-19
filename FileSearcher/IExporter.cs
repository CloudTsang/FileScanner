/*
 * 由SharpDevelop创建。
 * 用户： yondor_74
 * 日期: 2017/11/10
 * 时间: 12:20
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace FileSearcher
{
	/// <summary>
	/// Description of IExporter.
	/// </summary>
	public interface IExporter
	{
		void init();
		bool createFile(string path);
		bool addFile(string filename , string path , string ext);
		bool write(string content);
		void flush();
		void save();
	}
}
