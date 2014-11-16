using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FileSearchApp.lib {
    class TargetFolderPath {
        static public string TargetIncluseSubFolder = "[サブフォルダも含む]";
        static public string TargetCurrentFolder = "[サブフォルダは含まない]";

        static private int counter = 0;
        FlowLayoutPanel _flp_targetFolderPath = null;

        public TargetFolderPath(FlowLayoutPanel flp_targetFolderPath) {
            _flp_targetFolderPath = flp_targetFolderPath;
        }

        /// <summary>
        /// FolderPathを出力
        /// </summary>
        /// <param name="folderPath"></param>
        public void addFolderPath(string folderPath, string depth) {

            if (folderPath == "") {
                return;
            }else if (!Directory.Exists(folderPath)) {
                throw new FileSearchException("フォルダが存在しません。");
            }

            //Addしたpanelの数(Panelコントローラの識別子に使用)
            counter++;

            //Label
            Label lb_FolderPath = new Label();
            lb_FolderPath.AutoEllipsis = true;
            lb_FolderPath.ForeColor = Color.Green;
            lb_FolderPath.Location = new Point(60, 3);
            lb_FolderPath.Name = "lb_FolderPath@" + counter.ToString();
            lb_FolderPath.Size = new Size(300, 18);
            lb_FolderPath.Text = folderPath;

            //Label
            string targetDepth = "";
            if (depth == TargetIncluseSubFolder) {
                targetDepth = "[サブフォルダも含む]";
            } else if (depth == TargetCurrentFolder) {
                targetDepth = "[サブフォルダは含まない]";
            } else {
                string className = this.GetType().FullName;
                string methodName = MethodBase.GetCurrentMethod().Name;
                throw new FileSearchException("第二引数が不正な値です");
            }
            Label lb_Depth = new Label();
            lb_Depth.AutoEllipsis = true;
            lb_Depth.Location = new Point(60, 22);
            lb_Depth.Name = "lb_Depth@" + counter.ToString();
            lb_Depth.Size = new Size(300, 18);
            lb_Depth.Text = targetDepth;


            //Panel
            Panel pnl_Area = new Panel();
            pnl_Area.Anchor = AnchorStyles.Right;
            pnl_Area.Name = "pnl_FolderPath@" + counter.ToString();
            pnl_Area.Size = new Size(555, 40);
            pnl_Area.MouseEnter += new System.EventHandler(this.pnl_Area_MouseEnter);
            pnl_Area.MouseLeave += new System.EventHandler(this.pnl_Area_MouseLeave);

            //コントローラの追加
            pnl_Area.Controls.Add(lb_FolderPath);
            pnl_Area.Controls.Add(lb_Depth);
            _flp_targetFolderPath.Controls.Add(pnl_Area);
        }

        private void pnl_Area_MouseEnter(object sender, EventArgs e) {
            Panel pnl_Area = (Panel)sender;
            pnl_Area.BackColor = Color.FromArgb(240, 240, 240);
        }
        private void pnl_Area_MouseLeave(object sender, EventArgs e) {
            Panel pnl_Area = (Panel)sender;
            pnl_Area.BackColor = Color.White;
        }
    }
}
