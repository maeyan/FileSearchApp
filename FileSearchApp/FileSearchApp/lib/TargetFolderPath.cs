﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FileSearchApp.lib {
    class TargetFolderPath {
        const string LB_FOLDER_NAME = "lb_FolderName@";
        const string LB_FOLDER_PATN = "lb_FolderPath@";
        const string LB_DEPTH = "lb_Depth@";
        const string PNL_FOLDER_PATH = "pnl_FolderPath@";

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


            //パスチェック
            if (folderPath == "") {
                return;
            }
            
            //末尾が\で終わっていたら取り除く
            if(folderPath.LastIndexOf('\\')==folderPath.Length-1){
                folderPath = folderPath.Substring(0, folderPath.Length - 1);
            }

            //パスが存在するかチェック
            if (!Directory.Exists(folderPath)) {
                throw new FileSearchException("フォルダが存在しません。");
            }

            bool includeSubFolder = false;
            if (depth == TargetIncluseSubFolder) {
                includeSubFolder = true;
            } else if (depth == TargetCurrentFolder) {
                includeSubFolder = false;
            } else {
                throw new FileSearchException("第二引数が不正な値です");
            }

            //既に登録されている場合はupdate
            if (!isExistsFolderPathAndUpdate(folderPath, includeSubFolder)) {
                addFolderpathNew(folderPath, includeSubFolder);
            }
        }

        /// <summary>
        /// 既に登録済みか現在表示中のControlにあるか調べ存在した時
        /// サブフォルダを対象とするか情報を書き換える
        /// </summary>
        /// <returns>存在すればTrue, 存在しなければFalse</returns>
        private bool isExistsFolderPathAndUpdate(string folderPath, bool includeSubFolder) {
            //flowLayoutPanel上のControlsで命名規則にしたがったPanelを調べて
            //そこに記載されているfolderPathを見て既に存在するか調べる
            foreach (Control c in _flp_targetFolderPath.Controls) {
                
                if (c.GetType().ToString() == "System.Windows.Forms.Panel"
                    && 0 == c.Name.IndexOf(PNL_FOLDER_PATH)) {

                    //Panelの中にあるLabel(FolderPath)を取得する
                    string id = c.Name.Split('@')[1];
                    string folderPathId = LB_FOLDER_PATN + id;
                    string depthId = LB_DEPTH + id;
                    Label lb_FolderPath = (Label)c.Controls[folderPathId];
                    Label lb_Depth = (Label)c.Controls[depthId];

                    //FolderPathが一致するかチェック
                    if (lb_FolderPath.Text == folderPath) {
                        //一致したらサブフォルダも対象とするか情報を更新する
                        lb_Depth.Text = (includeSubFolder) ? TargetIncluseSubFolder : TargetCurrentFolder;
                        return true; 
                    }
                }
            }

            return false;
        }

        public List<List<string>> getFolderPaths() {
            List<List<string>> folderPaths = new List<List<string>>();

            //Controlを操作
            foreach (Control c in _flp_targetFolderPath.Controls) {
                //命名規則に従ったPanelを探す
                if (c.GetType().ToString() == "System.Windows.Forms.Panel"
                    && 0 == c.Name.IndexOf(PNL_FOLDER_PATH)) {
                    
                        string id = c.Name.Split('@')[1];

                        List<string> data = new List<string>();
                        data.Add(c.Controls[LB_FOLDER_PATN + id].Text);
                        data.Add(c.Controls[LB_DEPTH + id].Text);
                        folderPaths.Add(data);
                }
            }

            return folderPaths;
        }

        /// <summary>
        /// Folderパスを追加
        /// </summary>
        /// <param name="folderPath">走査対象フォルダパス</param>
        /// <param name="depth">サブフォルダも走査対象に含むかどうか</param>
        private void addFolderpathNew(string folderPath, bool includeSubFolder) {
            //Addしたpanelの数(Panelコントローラの識別子に使用)
            counter++;

            //Label
            Label lb_FolderName = new Label();
            lb_FolderName.AutoSize = true;
            lb_FolderName.ForeColor = Color.FromArgb(0, 0, 192);
            lb_FolderName.Location = new Point(30, 5);
            lb_FolderName.Name = LB_FOLDER_NAME + counter.ToString();
            lb_FolderName.Size = new Size(500, 18);
            lb_FolderName.Text = Path.GetFileName(folderPath);
            lb_FolderName.MouseEnter += new System.EventHandler(this.pnl_Area_MouseEnter);
            lb_FolderName.MouseLeave += new System.EventHandler(this.pnl_Area_MouseLeave);
            lb_FolderName.MouseEnter += new System.EventHandler(this.lb_FolderName_MouseEnter);
            lb_FolderName.MouseLeave += new System.EventHandler(this.lb_FolderName_MouseLeave);
            lb_FolderName.Click += new System.EventHandler(this.lb_FolderName_Click);

            //Label
            Label lb_FolderPath = new Label();
            lb_FolderPath.AutoEllipsis = true;
            lb_FolderPath.ForeColor = Color.Green;
            lb_FolderPath.Location = new Point(30, 24);
            lb_FolderPath.Name = LB_FOLDER_PATN + counter.ToString();
            lb_FolderPath.Size = new Size(500, 18);
            lb_FolderPath.Text = folderPath;
            lb_FolderPath.MouseEnter += new System.EventHandler(this.pnl_Area_MouseEnter);
            lb_FolderPath.MouseLeave += new System.EventHandler(this.pnl_Area_MouseLeave);

            //Label
            Label lb_Depth = new Label();
            lb_Depth.AutoEllipsis = true;
            lb_Depth.Location = new Point(30, 43);
            lb_Depth.Name = LB_DEPTH + counter.ToString();
            lb_Depth.Size = new Size(200, 18);
            lb_Depth.Text = (includeSubFolder) ? TargetIncluseSubFolder : TargetCurrentFolder;
            lb_Depth.MouseEnter += new System.EventHandler(this.pnl_Area_MouseEnter);
            lb_Depth.MouseLeave += new System.EventHandler(this.pnl_Area_MouseLeave);

            //Panel
            Panel pnl_Area = new Panel();
            pnl_Area.Anchor = AnchorStyles.Right;
            pnl_Area.Name = PNL_FOLDER_PATH + counter.ToString();
            pnl_Area.Size = new Size(555, 65);
            pnl_Area.MouseEnter += new System.EventHandler(this.pnl_Area_MouseEnter);
            pnl_Area.MouseLeave += new System.EventHandler(this.pnl_Area_MouseLeave);
            
            //コントローラの追加
            pnl_Area.Controls.Add(lb_FolderName);
            pnl_Area.Controls.Add(lb_FolderPath);
            pnl_Area.Controls.Add(lb_Depth);
            _flp_targetFolderPath.Controls.Add(pnl_Area);
        }

        private void pnl_Area_MouseEnter(object sender, EventArgs e) {
            string pnl_AreaId = "";
            if (sender.GetType().ToString() == "System.Windows.Forms.Panel") {
                pnl_AreaId = ((Panel)sender).Name;
            } else if (sender.GetType().ToString() == "System.Windows.Forms.Label") {
                pnl_AreaId = PNL_FOLDER_PATH + ((Label)sender).Name.Split('@')[1];
            }

            Panel pnl_Area = (Panel)_flp_targetFolderPath.Controls[pnl_AreaId];
            pnl_Area.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void pnl_Area_MouseLeave(object sender, EventArgs e) {

            string pnl_AreaId = "";
            if (sender.GetType().ToString() == "System.Windows.Forms.Panel") {
                pnl_AreaId = ((Panel)sender).Name;
            } else if (sender.GetType().ToString() == "System.Windows.Forms.Label") {
                pnl_AreaId = PNL_FOLDER_PATH + ((Label)sender).Name.Split('@')[1];
            }

            Panel pnl_Area = (Panel)_flp_targetFolderPath.Controls[pnl_AreaId];
            pnl_Area.BackColor = Color.White;
        }

        private void lb_FolderName_MouseEnter(object sender, EventArgs e) {
            Label lb_FolderName = (Label)sender;
            float fontSize = lb_FolderName.Font.Size;
            lb_FolderName.Font = new Font("メイリオ", fontSize, FontStyle.Underline);
        }

        private void lb_FolderName_MouseLeave(object sender, EventArgs e) {
            Label lb_FolderName = (Label)sender;
            float fontSize = lb_FolderName.Font.Size;
            lb_FolderName.Font = new Font("メイリオ", fontSize);
        }

        private void lb_FolderName_Click(object sender, EventArgs e) {
            Label lb_FolderName = (Label)sender;
            string id = lb_FolderName.Name.Split('@')[1];
            Panel pnl_Area = (Panel)_flp_targetFolderPath.Controls[PNL_FOLDER_PATH + id];
            Label lb_FolderPath = (Label)pnl_Area.Controls[LB_FOLDER_PATN + id];
            
            //存在すれば開く
            if (Directory.Exists(lb_FolderPath.Text)) {
                System.Diagnostics.Process.Start(lb_FolderPath.Text);
            }

        }
    }
}