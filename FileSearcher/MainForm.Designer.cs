/*
 * 由SharpDevelop创建。
 * 用户： yondor_74
 * 日期: 2017/11/6
 * 时间: 11:16
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System.Windows.Forms; 
 
namespace FileSearcher
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button btnGlobalScan;
		private System.Windows.Forms.FolderBrowserDialog dialog;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader FileName;
		private System.Windows.Forms.ColumnHeader FilePath;
		private System.Windows.Forms.ColumnHeader FileExt;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Label tips;
		private System.Windows.Forms.Button btnSelectFolder;
		private System.Windows.Forms.Label labelErr;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnSelectFolder = new System.Windows.Forms.Button();
			this.dialog = new System.Windows.Forms.FolderBrowserDialog();
			this.listView = new System.Windows.Forms.ListView();
			this.FileName = new System.Windows.Forms.ColumnHeader();
			this.FilePath = new System.Windows.Forms.ColumnHeader();
			this.FileExt = new System.Windows.Forms.ColumnHeader();
			this.btnClear = new System.Windows.Forms.Button();
			this.tips = new System.Windows.Forms.Label();
			this.btnGlobalScan = new System.Windows.Forms.Button();
			this.labelErr = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnSelectFolder
			// 
			this.btnSelectFolder.Location = new System.Drawing.Point(150, 12);
			this.btnSelectFolder.Name = "btnSelectFolder";
			this.btnSelectFolder.Size = new System.Drawing.Size(129, 23);
			this.btnSelectFolder.TabIndex = 9;
			this.btnSelectFolder.Text = "选择文件夹扫描";
			this.btnSelectFolder.UseVisualStyleBackColor = true;
			this.btnSelectFolder.Click += new System.EventHandler(this.selectFolderHandler);
			// 
			// dialog
			// 
			this.dialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.dialog.ShowNewFolderButton = false;
			// 
			// listView
			// 
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.FileName,
			this.FilePath,
			this.FileExt});
			this.listView.FullRowSelect = true;
			this.listView.GridLines = true;
			this.listView.Location = new System.Drawing.Point(12, 41);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(892, 613);
			this.listView.TabIndex = 11;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
			// 
			// FileName
			// 
			this.FileName.Text = "文件名";
			this.FileName.Width = 355;
			// 
			// FilePath
			// 
			this.FilePath.Text = "路径";
			this.FilePath.Width = 355;
			// 
			// FileExt
			// 
			this.FileExt.Text = "文件类型";
			this.FileExt.Width = 178;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(285, 12);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(132, 25);
			this.btnClear.TabIndex = 12;
			this.btnClear.Text = "清除结果";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.listClearHandler);
			// 
			// tips
			// 
			this.tips.Font = new System.Drawing.Font("宋体", 12F);
			this.tips.Location = new System.Drawing.Point(433, 9);
			this.tips.Name = "tips";
			this.tips.Size = new System.Drawing.Size(1236, 28);
			this.tips.TabIndex = 13;
			this.tips.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnGlobalScan
			// 
			this.btnGlobalScan.Location = new System.Drawing.Point(13, 12);
			this.btnGlobalScan.Name = "btnGlobalScan";
			this.btnGlobalScan.Size = new System.Drawing.Size(131, 23);
			this.btnGlobalScan.TabIndex = 14;
			this.btnGlobalScan.Text = "全盘扫描";
			this.btnGlobalScan.UseVisualStyleBackColor = true;
			this.btnGlobalScan.Click += new System.EventHandler(this.globalScanHandler);
			// 
			// labelErr
			// 
			this.labelErr.Font = new System.Drawing.Font("宋体", 12F);
			this.labelErr.Location = new System.Drawing.Point(13, 661);
			this.labelErr.Name = "labelErr";
			this.labelErr.Size = new System.Drawing.Size(886, 23);
			this.labelErr.TabIndex = 15;
			this.labelErr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(911, 691);
			this.Controls.Add(this.labelErr);
			this.Controls.Add(this.btnGlobalScan);
			this.Controls.Add(this.tips);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.listView);
			this.Controls.Add(this.btnSelectFolder);
			this.Name = "MainForm";
			this.Text = "文档检查器v1.0";
			this.ResumeLayout(false);

		}
	}
}
