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
        MyTabPage tabPage = new MyTabPage();

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

            tabPage.AddTabPage(tabControl1);
        }

        private void diskList_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* 点击驱动器时，获取对应的目录内容 */
            tabPage.setCurPath(diskList.SelectedItem.ToString());

            DirectoryInfo dir = new DirectoryInfo(tabPage.getCurPath());
            DirectoryInfo[] dirs = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();
            ListView list = tabPage.GetDirList(tabControl1);

            if(list != null)
            {
                list.Items.Clear();

                foreach (DirectoryInfo i in dirs)
                {
                    if ((i.Name != "$RECYCLE.BIN") && (i.Name != "System Volume Information"))
                    {
                        list.Items.Add(i.Name);
                    }
                }

                foreach (FileInfo i in files)
                {
                    list.Items.Add(i.Name);
                }
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

        private void btnAddTab_Click(object sender, EventArgs e)
        {
            /* 添加tab按钮 */
            tabPage.AddTabPage(tabControl1);
        }

        private void tabControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /* 双击标签关闭 */
            int index;
            if (tabControl1.TabCount > 1)
            {
                index = tabControl1.SelectedIndex;
                tabControl1.TabPages.RemoveAt(index); //双击关闭页面
                tabControl1.SelectedIndex = index - 1;//索引到最后一个页面
            }
        }
    }

    public class MyTabPage
    {
        string curPath;   //当前所在路径
        public MyTabPage()
        {
            
        }

        public string getCurPath()
        {
            return curPath;
        }

        public void setCurPath(string path)
        {
            curPath = path;
        }

        public void AddTabPage(TabControl tabCtr)
        {
            TabPage page = new TabPage();
            page.Text = "Home";

            /* 增加ListView控件 */
            ListView list = new ListView();
            list.Name = "list";
            list.Dock = DockStyle.Fill;
            list.View = View.List;
            list.MouseDoubleClick += DirList_MouseDoubleClick; //绑定右侧文件夹双击事件
            
            //增加其他按钮
            Button btn_back = new Button();
            btn_back.Width = 30;
            btn_back.Height = 30;
            btn_back.Location = new Point(10, 10);
            btn_back.BringToFront();

            /* 向page中添加控件 */
            page.Controls.Add(list);
            page.Controls.Add(btn_back);

            /* 将page添加到tabControl中 */
            tabCtr.TabPages.Add(page);
            tabCtr.SelectedTab = page;
        }

        public ListView GetDirList(TabControl tabCtr) //获取当前所选page的ListView控件
        {
            Control[] list = tabCtr.SelectedTab.Controls.Find("list", true);

            foreach (Control i in list)
            {
                if (i is ListView view)
                {
                    return view;
                }
            }
            return null;
        }

        private void DirList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView list = sender as ListView;

            if (list != null)
            {
                curPath = curPath + "\\" + list.FocusedItem.Text.ToString();
                FileInfo f = new FileInfo(curPath);
                if (f.Exists)
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

                    list.Clear();
                    foreach (DirectoryInfo i in dirs)
                    {
                        if ((i.Name != "$RECYCLE.BIN") && (i.Name != "System Volume Information"))
                        {
                            list.Items.Add(i.Name);
                        }
                    }

                    foreach (FileInfo i in files)
                    {
                        list.Items.Add(i.Name);
                    }
                }
            }
        }

        //private void btnBack_Click(object sender, EventArgs e)
        //{
        //    if ((curPath != null) && (curPath.LastIndexOf("\\") > 0))
        //    {
        //        curPath = curPath.Substring(0, curPath.LastIndexOf("\\"));

        //        DirectoryInfo dir = new DirectoryInfo(curPath);
        //        DirectoryInfo[] dirs = dir.GetDirectories();
        //        FileInfo[] files = dir.GetFiles();
        //        ListView list = GetDirList();

        //        if (list != null)
        //        {
        //            list.Items.Clear();
        //            foreach (DirectoryInfo i in dirs)
        //            {
        //                if ((i.Name != "$RECYCLE.BIN") && (i.Name != "System Volume Information"))
        //                {
        //                    list.Items.Add(i.Name);
        //                }
        //            }

        //            foreach (FileInfo i in files)
        //            {
        //                list.Items.Add(i.Name);
        //            }
        //        }
        //    }
        //}
    }
}
