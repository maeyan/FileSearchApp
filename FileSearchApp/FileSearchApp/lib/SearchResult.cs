using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileSearchApp.lib {
    class SearchResult {
        private int coutner = 0;
        FlowLayoutPanel _flp_result = null;
        const string BT_FOCUS = "bt_Focus@";
        const string LB_FILE_NAME = "lb_FileName@";
        const string LB_FOLDER_PATH = "lb_FolderPath@";
        const string PNL_RESULT = "pnl_result@";

        public SearchResult(FlowLayoutPanel flp_result) {
            _flp_result = flp_result;
        }

        public void deleteSearchData() {
            //flowLayoutPanel上のControlsでパネルを削除したいので後ろからアクセス
            for (int i = _flp_result.Controls.Count - 1; -1 < i; i--) {
                //Panelかつpnl_resultの名前を持つと削除
                if (_flp_result.Controls[i].GetType().ToString() == "System.Windows.Forms.Panel"
                        && -1 < _flp_result.Controls[i].Name.IndexOf(PNL_RESULT)) {
                    _flp_result.Controls.Remove(_flp_result.Controls[i]);
                }
            }
        }

        public void outputData(List<string> filePaths) {
            foreach (string filePath in filePaths) {
                coutner++;

                string fileName = Path.GetFileName(filePath);
                string directoryName = Path.GetDirectoryName(filePath);

                //Button
                Button bt_Focus = new Button();
                bt_Focus.BackColor = Color.White;
                bt_Focus.FlatStyle = FlatStyle.Flat;
                bt_Focus.Name = BT_FOCUS + coutner.ToString();
                bt_Focus.Location = new System.Drawing.Point(1, 1);
                bt_Focus.Size = new Size(1, 1);

                //Label
                Label lb_FileName = new Label();
                lb_FileName.AutoEllipsis = true;
                lb_FileName.Cursor = Cursors.Hand;
                lb_FileName.Font = new Font("メイリオ", 12);
                lb_FileName.ForeColor = Color.FromArgb(0, 0, 192);
                lb_FileName.Location = new System.Drawing.Point(50, 6);
                lb_FileName.Name = LB_FILE_NAME + coutner.ToString();
                lb_FileName.Size = new Size(650, 24);
                lb_FileName.Tag = filePath;
                lb_FileName.Text = fileName;
                lb_FileName.Click += new System.EventHandler(this.lbl_Path_Click);
                lb_FileName.MouseEnter += new System.EventHandler(this.lbl_Path_MouseEnter);
                lb_FileName.MouseLeave += new System.EventHandler(this.lbl_Path_MouseLeave);

                //Label
                Label lb_FolderPath = new Label();
                lb_FolderPath.AutoEllipsis = true;
                lb_FolderPath.Cursor = Cursors.Hand;
                lb_FolderPath.Font = new Font("メイリオ", 9);
                lb_FolderPath.ForeColor = Color.Green;
                lb_FolderPath.Location = new System.Drawing.Point(50, 32);
                lb_FolderPath.Name = LB_FOLDER_PATH + coutner.ToString();
                lb_FolderPath.Size = new Size(650, 18);
                lb_FolderPath.Tag = directoryName;
                lb_FolderPath.Text = directoryName;
                lb_FolderPath.Click += new System.EventHandler(this.lbl_Path_Click);
                lb_FolderPath.MouseEnter += new System.EventHandler(this.lbl_Path_MouseEnter);
                lb_FolderPath.MouseLeave += new System.EventHandler(this.lbl_Path_MouseLeave);

                //Panel
                Panel pnl_Area = new Panel();
                pnl_Area.Anchor = AnchorStyles.Right;
                pnl_Area.Name = PNL_RESULT + coutner.ToString();

                pnl_Area.Controls.Add(bt_Focus);
                pnl_Area.Controls.Add(lb_FileName);
                pnl_Area.Controls.Add(lb_FolderPath);
                pnl_Area.Width = 650;
                pnl_Area.Height = 55;

                _flp_result.Controls.Add(pnl_Area);
            }
        }

        /// <summary>
        /// パスを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_Path_Click(object sender, EventArgs e) {
            Label lbl = (Label)sender;
            string path = lbl.Tag.ToString();

            if (File.Exists(path) || Directory.Exists(path)) {
                System.Diagnostics.Process.Start(path);
            } else {
                MessageBox.Show("ネットワークの不調または、パスが存在しませんでした");
            }
        }

        private void lbl_Path_MouseEnter(object sender, EventArgs e) {
            Label lbl = (Label)sender;
            float fontSize = lbl.Font.Size;
            lbl.Font = new Font("メイリオ", fontSize, FontStyle.Underline);
            lbl.BackColor = Color.FromArgb(245, 245, 245);

            string id = lbl.Name.Split('@')[1];
            Panel pnl = (Panel)_flp_result.Controls[PNL_RESULT + id];
            Button bt = (Button)pnl.Controls[BT_FOCUS + id];
            bt.Focus();
        }

        private void lbl_Path_MouseLeave(object sender, EventArgs e) {
            Label lbl = (Label)sender;
            float fontSize = lbl.Font.Size;
            lbl.Font = new Font("メイリオ", fontSize);
            lbl.BackColor = Color.White;
        }
    }
}
