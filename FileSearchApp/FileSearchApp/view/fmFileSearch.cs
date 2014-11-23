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
            this.ExecuteSearch(1); //検索実行
        }

        private void ExecuteSearch(int firstItemNumber) {
            this.lbl_Count.Text = "0件";
            this.lbl_ShowRange.Text = "";
            Application.DoEvents();

            int offset = firstItemNumber - 1;
            int resultPerPage = 100;

            //検索する
            string searchWord = tb_SearchWord.Text.Trim();
            SearchDB db = new SearchDB();
            List<string> filePaths = db.selectFileData(searchWord, offset, resultPerPage);
            
            //検索結果の情報を表示
            int allCount = db.resultCount;
            double resultMiliSecond = db.searchResult;
            this.lbl_Count.Text = string.Format("{0}件 ({1}秒)", allCount, (resultMiliSecond / 1000).ToString());
            
            SearchResult sr = new SearchResult(this.flp_SearchResult);
            sr.deleteSearchData();
            sr.outputData(filePaths);

            int startCount = offset + 1;

            Pagenate page = new Pagenate(this.flp_SearchResult, startCount, allCount, resultPerPage);
            page.AddPanel(this.ExecuteSearch);
            this.lbl_ShowRange.Text = page.SearchRange();
        }

        private void tb_SearchWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ExecuteSearch(1); //検索実行
            }
        }
    }
}
