﻿
namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.diskList = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnAddTab = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // diskList
            // 
            this.diskList.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.diskList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.diskList.FormattingEnabled = true;
            this.diskList.ItemHeight = 35;
            this.diskList.Location = new System.Drawing.Point(12, 12);
            this.diskList.Name = "diskList";
            this.diskList.Size = new System.Drawing.Size(171, 424);
            this.diskList.TabIndex = 0;
            this.diskList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.diskList_DrawItem);
            this.diskList.SelectedIndexChanged += new System.EventHandler(this.diskList_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(189, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(599, 424);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDoubleClick);
            // 
            // btnAddTab
            // 
            this.btnAddTab.Location = new System.Drawing.Point(63, 368);
            this.btnAddTab.Name = "btnAddTab";
            this.btnAddTab.Size = new System.Drawing.Size(75, 23);
            this.btnAddTab.TabIndex = 2;
            this.btnAddTab.Text = "addTab";
            this.btnAddTab.UseVisualStyleBackColor = true;
            this.btnAddTab.Click += new System.EventHandler(this.btnAddTab_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 447);
            this.Controls.Add(this.btnAddTab);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.diskList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox diskList;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnAddTab;
    }
}

