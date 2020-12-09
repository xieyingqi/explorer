using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string curPath; //当前所在路径

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* 获取所有驱动器，遍历并添加至列表 */
            DriveInfo[] disks = DriveInfo.GetDrives();
            foreach (DriveInfo disk in disks)
            {
                diskList.Items.Add(disk);
            }
            dirList.View = View.List;
        }

        private void diskList_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* 点击驱动器时，获取对应的目录内容 */
            curPath = diskList.SelectedItem.ToString();
            DirectoryInfo dir = new DirectoryInfo(curPath);
            DirectoryInfo[] dirs = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();

            dirList.Items.Clear();
            foreach (DirectoryInfo i in dirs)
            {
                if ((i.Name != "$RECYCLE.BIN") && (i.Name != "System Volume Information"))
                {
                    dirList.Items.Add(i.Name);
                }
            }

            foreach (FileInfo i in files)
            {
                dirList.Items.Add(i.Name);
            }
        }

        private void diskList_DrawItem(object sender, DrawItemEventArgs e)
        {
            /* 驱动器列表自定义重绘 */
            e.DrawBackground();
            e.DrawFocusRectangle();

            StringFormat strFmt = new System.Drawing.StringFormat();
            strFmt.Alignment = StringAlignment.Center;     //文本垂直居中
            strFmt.LineAlignment = StringAlignment.Center; //文本水平居中

            e.Graphics.DrawString(diskList.Items[e.Index].ToString(), new Font(this.Font.FontFamily, 14), new SolidBrush(e.ForeColor), e.Bounds, strFmt);
            //e.Graphics.DrawIcon();
        }

        private void dirList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            curPath = curPath + "\\" + dirList.FocusedItem.Text.ToString();
            FileInfo f = new FileInfo(curPath);
            if(f.Exists)
            {
                /* 如果是文件，则打开它，并把路径还原 */
                System.Diagnostics.Process.Start(curPath);
                curPath = curPath.Substring(0, curPath.LastIndexOf("\\"));
            }
            else
            {
                /* 如果是文件夹，向下遍历 */
                DirectoryInfo dir = new DirectoryInfo(curPath);
                DirectoryInfo[] dirs = dir.GetDirectories();
                FileInfo[] files = dir.GetFiles();

                dirList.Clear();
                foreach (DirectoryInfo i in dirs)
                {
                    if ((i.Name != "$RECYCLE.BIN") && (i.Name != "System Volume Information"))
                    {
                        dirList.Items.Add(i.Name);
                    }
                }

                foreach (FileInfo i in files)
                {
                    dirList.Items.Add(i.Name);
                }
            }
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if ((curPath != null) && (curPath.LastIndexOf("\\") > 0))
            {
                curPath = curPath.Substring(0, curPath.LastIndexOf("\\"));

                DirectoryInfo dir = new DirectoryInfo(curPath);
                DirectoryInfo[] dirs = dir.GetDirectories();
                FileInfo[] files = dir.GetFiles();

                dirList.Items.Clear();
                foreach (DirectoryInfo i in dirs)
                {
                    if ((i.Name != "$RECYCLE.BIN") && (i.Name != "System Volume Information"))
                    {
                        dirList.Items.Add(i.Name);
                    }
                }

                foreach (FileInfo i in files)
                {
                    dirList.Items.Add(i.Name);
                }
            }
        }
    }
}
