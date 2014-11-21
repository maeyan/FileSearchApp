using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FileSearchApp.lib;

namespace FileSearchApp {
    public partial class fmFileSearch : Form {
        public fmFileSearch() {
            InitializeComponent();
        }


        private void pb_TargetFolderPath_Click(object sender, EventArgs e) {
            fmFilePathSetting form = new fmFilePathSetting();
            form.ShowDialog(this);
            form.Dispose();
        }

        private void pb_Search_Click(object sender, EventArgs e) {
            this.ExecuteSearch(); //検索実行
        }

        private void ExecuteSearch() {
            this.lbl_Count.Text = "0件";
            this.lbl_ShowRange.Text = "";
            Application.DoEvents();

            int offset = 0;

            string searchWord = tb_SearchWord.Text.Trim();
            SearchDB db = new SearchDB();
            List<string> filePaths = db.selectFileData(searchWord, offset);
            this.lbl_Count.Text = string.Format("{0}件 ({1}秒)",db.resultCount, (db.searchResult / 1000).ToString());
            
            this.lbl_ShowRange.Text = string.Format("{0}-{1}件を表示", offset + 1, offset + 100);
            SearchResult sr = new SearchResult(this.flp_SearchResult);
            sr.deleteSearchData();
            sr.outputData(filePaths);
        }

        private void tb_SearchWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ExecuteSearch(); //検索実行
            }
        }

        private void fmFileSearch_Deactivate(object sender, EventArgs e)
        {
            //閉じる時エラーになるのでnull回避
            if (this.ActiveControl != null)
            {
                ////MessageBox.Show(this.ActiveControl.Name);
                //Point ScreenPoint = this.ActiveControl.PointToScreen(this.ActiveControl.Location);
                //MessageBox.Show(this.PointToClient(ScreenPoint).Y.ToString());
            }
        }
    }
}
