using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileSearchApp.lib {
    class Pagenate {
        FlowLayoutPanel _flp_result = null;
        int _startCount = 0;
        int _endCount = 0;
        int _allCount = 0;
        int _resultPerPage = 0;
        Action<int> _CallBackLinkClick = null;

        public Pagenate(FlowLayoutPanel flp_result, int startCount, int allCount, int resultPerPage) {
            _flp_result = flp_result;
            _allCount = allCount;
            _startCount = startCount;
            _resultPerPage = resultPerPage;
            _endCount = Math.Min(_startCount + _resultPerPage - 1, _allCount);
            _allCount = allCount;
        }

        public void AddPanel(Action<int> CallBackLinkClick) {
            //検索結果が1Pageに表示する件数以下だとPageLinkは不要
            if (_allCount <= _resultPerPage) { return; }

            //Linkをクリックした際の関数予備出し先を保存しておく
            _CallBackLinkClick = CallBackLinkClick;

            Panel pnl_PageArea = new Panel();
            pnl_PageArea.Name = "pnl_PageArea";
            pnl_PageArea.Width = 650;
            pnl_PageArea.Height = 80;
            _flp_result.Controls.Add(pnl_PageArea);

            //PageリンクをFlowLayoutPanelに格納する
            FlowLayoutPanel flp_PageLink = new FlowLayoutPanel();
            flp_PageLink.Location = new Point(10, 10);
            flp_PageLink.Size = new Size(720, 30);
            pnl_PageArea.Controls.Add(flp_PageLink);


            int currentPage = _endCount / _resultPerPage;
            int linkStartPage = (currentPage < 6) ? 1 : currentPage - 5;
            int lastPage = _allCount / _resultPerPage;
            if (0 < _allCount % _resultPerPage) { lastPage++; }//端数があればページを+1する
            int linkEndPage = Math.Min(linkStartPage + 10 - 1, lastPage);

            int endLinkPosition = 0;
            for (int page = linkStartPage; page <= linkEndPage; page++) {
                Label lbl_Link = new Label();
                lbl_Link.AutoSize = true;
                lbl_Link.Name = "lbl_Page@" + page.ToString();
                lbl_Link.Text = page.ToString();

                if (currentPage == page) {
                    lbl_Link.Font = new Font("Arial Narrow", 15, FontStyle.Bold);
                    lbl_Link.ForeColor = Color.FromArgb(54, 54, 54);
                    lbl_Link.Tag = "";
                } else {
                    lbl_Link.Font = new Font("Arial Narrow", 15);
                    lbl_Link.Tag = (Convert.ToInt32(lbl_Link.Text) - 1) * _resultPerPage + 1;
                    lbl_Link.ForeColor = Color.FromArgb(0, 0, 192);
                    lbl_Link.Click += new System.EventHandler(this.lbl_Link_Click);
                    lbl_Link.MouseEnter += new System.EventHandler(this.lbl_Link_MouseEnter);
                    lbl_Link.MouseLeave += new System.EventHandler(this.lbl_Link_MouseLeave);
                }

                flp_PageLink.Controls.Add(lbl_Link);
                endLinkPosition = lbl_Link.Location.X + lbl_Link.Size.Width;
            }

            //FlowLayoutPanelの幅を調整する
            flp_PageLink.Width = endLinkPosition + 10;


            //センタリングする
            flp_PageLink.Location = new Point((pnl_PageArea.Size.Width - endLinkPosition) / 2, flp_PageLink.Location.Y);
        }

        private void lbl_Link_MouseEnter(object sender, EventArgs e) {
            Label lbl_Link = (Label)sender;
            lbl_Link.Font = new Font("Arial Narrow", 15, FontStyle.Underline);

        }

        private void lbl_Link_MouseLeave(object sender, EventArgs e) {
            Label lbl_Link = (Label)sender;
            lbl_Link.Font = new Font("Arial Narrow", 15);

        }

        private void lbl_Link_Click(object sender, EventArgs e) {
            Label lbl_Link = (Label)sender;
            if (lbl_Link.Tag.ToString() == "") { return; }
            int filrstItemNumber = Convert.ToInt32(lbl_Link.Tag.ToString());
            _CallBackLinkClick(filrstItemNumber);
        }

        /// <summary>
        /// 現在の表示
        /// </summary>
        /// <returns></returns>
        public string SearchRange() {
            if (_endCount == 0) {
                return "0件";
            } else {
                return string.Format("{0} - {1}件を表示", _startCount, _endCount);
            }
        }
    }
}
