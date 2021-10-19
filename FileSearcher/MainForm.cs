/*
 * 由SharpDevelop创建。
 * 用户： Cloud
 * 日期: 2017/11/6
 * 时间: 11:16
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
using System.Security.AccessControl;
namespace FileSearcher
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private DriveInfo[] drives = DriveInfo.GetDrives();
		private int globalIndex = -1;
		
		private Regex extReg = new Regex(".(pdf|ppt|pptx|doc|docx|xls|xlsx)");
		private String path = "";
		private bool scanning;
		private delegate void ScanDelegate(FileInfo[] files);
		private delegate void TxtDelegate(string tip);
		private Stopwatch sw;
		private IExporter exporter;
		
		private int scannedFile;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			Application.ApplicationExit += flushExportFile;
			//Object a = null;
			//trace(a.ToString());
			
//			Control.CheckForIllegalCrossThreadCalls = false ;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		
		
		void trace(Object str){
			System.Diagnostics.Debug.WriteLine(str);
		}
		
		/**开始扫描**/
		void StartScan(string pathToScan){
			
			exporter = new ResultExporter_XLS();
			exporter.init();
			if(!exporter.createFile((String)pathToScan)){
				
				exporter = new ResultExporter_TXT();
				exporter.init();
				if(!exporter.createFile((String)pathToScan)){
					this.labelErr.Text = "本工具在该目录下没有权限生成扫描结果文档，请剪切到其他路径再运行。";
					return ;
				}
			}					
			scannedFile = 0;
			ParameterizedThreadStart ts = new ParameterizedThreadStart(Scan);
			Thread td = new Thread(ts);
			td.IsBackground=true;
			sw = new Stopwatch();
			sw.Start();
			td.Start(pathToScan);
		}
		
		/**扫描pathToScan实现方法**/
		void Scan(object pathToScan)
		{
			trace("扫描路径 ："+pathToScan);
			
			scanning=true;
			DirectoryInfo di = new DirectoryInfo((String)pathToScan);
			
			DirectoryInfo[] dirs = new DirectoryInfo[]{di};
			int dirInd = 0;
			while(dirInd<dirs.Length){
				try{
					if ((dirs[dirInd].Attributes & FileAttributes.Hidden) == FileAttributes.Hidden //隐藏文件夹
//					     || dirs[dirInd].GetAccessControl().AreAccessRulesProtected)  //无访问权限文件夹
					     
					    &&  !String.Equals(dirs[dirInd].FullName,pathToScan))  //不是选定文件夹的文件夹 
					{
						//trace(dirs[dirInd].FullName + " 是隐藏文件夹");
						dirInd++;
						continue;
					}
					string fn = dirs[dirInd].FullName;
					TxtDelegate deltip = new TxtDelegate(setTips);
					Invoke(deltip  ,new object[] {"正在扫描路径 "+fn});
					trace("正在扫描路径 "+dirs[dirInd].FullName+" | Attributes = "+dirs[dirInd].Attributes);			

					//扫描子文件夹 以及 目录下的文件
					DirectoryInfo[] tmp = dirs[dirInd].GetDirectories();
					if(tmp!=null &&  tmp.Length>0){
						dirs = dirs.Concat(tmp).ToArray();
						FileInfo[] files = dirs[dirInd].GetFiles();
						if(files!=null && files.Length>0){
							ScanDelegate del = new ScanDelegate(ScanEnd);
							Invoke(del  ,new object[] {files});
						}
					}
					dirInd++;
				}
				catch(Exception err){
					trace(err.StackTrace);
					dirInd++;
					continue;
				}
			
			}
			
			
			trace("扫描 "+pathToScan+" 完毕");
			if(globalIndex>-1 ){  //全盘扫描的场合
				globalIndex++;
				if(globalIndex == drives.Length){
					
				}else{
					Scan(drives[globalIndex].Name);
					return;
				}				
			}
			
			sw.Stop();
			exporter.flush();
			exporter.save();
			trace("扫描完毕，耗时 = "+sw.Elapsed.TotalSeconds+" s");
			scanning=false;	
			TxtDelegate dlg = new TxtDelegate(setTips);
			Invoke(dlg  ,new object[] {"扫描完毕，共 "+listView.Items.Count+" 个文件被扫描到。双击项目可打开文件路径。"});		
			
			
		}

		
		
		void ScanEnd(FileInfo[] files){			
			if(files!=null){
				FileInfo[] result = files.Where(x=>extReg.IsMatch(x.Extension)).ToArray();
				if(result!=null && result.Length>0){
					foreach(FileInfo fi in result){
						//if(!extReg.IsMatch(fi.Extension))continue;
						ListViewItem item = new ListViewItem(fi.Name);
						
						exporter.addFile(fi.Name , result[0].DirectoryName , fi.Extension);
						scannedFile++;
						if(scannedFile>=50){
							trace("更新输出文件");
							exporter.flush();
							scannedFile=0;
						}
						//exporter.write("\r\n"+fi.Name);
						item.SubItems.Add(fi.DirectoryName);
						item.SubItems.Add(fi.Extension);
						listView.Items.Add(item);
					}
				}				
			}			
		} 
		
		void flushExportFile(object sender, EventArgs e){
			if(exporter!=null)exporter.flush();
		}
		
		void setTips(string tip){
			this.tips.Text = tip;
		}
		
		
		void selectFolderHandler(object sender, EventArgs e)
		{
			if(scanning)return;
            dialog.Description = "请选择扫描文件夹";
            DialogResult res = dialog.ShowDialog();
            if(res == DialogResult.OK ||res == DialogResult.Yes)
            {
            	if(dialog.SelectedPath==path)return;
                path=dialog.SelectedPath;
                globalIndex = -1;
                listView.Items.Clear();
           		this.tips.Text = "正在扫描"+path+",请稍候...";	
                StartScan(path);
            }else{
            	
            }
		}
		
		void globalScanHandler(object sender, EventArgs e)
		{
			if(scanning)return;
            listView.Items.Clear();
            globalIndex=0;
            StartScan(drives[globalIndex].Name);
            this.tips.Text = "正在全盘扫描,请稍候...";
		}
		
		void listClearHandler(object sender, EventArgs e)
		{
			if(scanning)return;
			path="";
			tips.Text = "";
			listView.Items.Clear();
		}
		
		void listView1_MouseDoubleClick(object sender, MouseEventArgs e)  {
			if(listView.SelectedItems.Count==0)return;
			try{
				System.Diagnostics.Process.Start("explorer.exe", "/select,"+listView.SelectedItems[0].SubItems[1].Text+"\\"+listView.SelectedItems[0].SubItems[0].Text);	
			}catch(Exception err){
				trace(err.StackTrace);
			}
			
		}


	}
}
