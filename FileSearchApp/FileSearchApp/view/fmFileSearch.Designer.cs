namespace FileSearchApp {
    partial class fmFileSearch {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.flp_SearchResult = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_form = new System.Windows.Forms.Panel();
            this.pb_Search = new System.Windows.Forms.PictureBox();
            this.tb_SearchWord = new System.Windows.Forms.TextBox();
            this.pb_TargetFolderPath = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnl_form.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Search)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_TargetFolderPath)).BeginInit();
            this.SuspendLayout();
            // 
            // flp_SearchResult
            // 
            this.flp_SearchResult.Location = new System.Drawing.Point(1, 95);
            this.flp_SearchResult.Name = "flp_SearchResult";
            this.flp_SearchResult.Size = new System.Drawing.Size(783, 366);
            this.flp_SearchResult.TabIndex = 0;
            // 
            // pnl_form
            // 
            this.pnl_form.Controls.Add(this.pb_Search);
            this.pnl_form.Controls.Add(this.tb_SearchWord);
            this.pnl_form.Controls.Add(this.pb_TargetFolderPath);
            this.pnl_form.Location = new System.Drawing.Point(1, 1);
            this.pnl_form.Name = "pnl_form";
            this.pnl_form.Size = new System.Drawing.Size(783, 93);
            this.pnl_form.TabIndex = 0;
            // 
            // pb_Search
            // 
            this.pb_Search.Image = global::FileSearchApp.Properties.Resources.search;
            this.pb_Search.Location = new System.Drawing.Point(559, 36);
            this.pb_Search.Name = "pb_Search";
            this.pb_Search.Size = new System.Drawing.Size(60, 38);
            this.pb_Search.TabIndex = 3;
            this.pb_Search.TabStop = false;
            this.toolTip.SetToolTip(this.pb_Search, "検索");
            this.pb_Search.Click += new System.EventHandler(this.pb_Search_Click);
            // 
            // tb_SearchWord
            // 
            this.tb_SearchWord.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tb_SearchWord.Location = new System.Drawing.Point(98, 36);
            this.tb_SearchWord.Name = "tb_SearchWord";
            this.tb_SearchWord.Size = new System.Drawing.Size(521, 39);
            this.tb_SearchWord.TabIndex = 2;
            // 
            // pb_TargetFolderPath
            // 
            this.pb_TargetFolderPath.Image = global::FileSearchApp.Properties.Resources.database;
            this.pb_TargetFolderPath.Location = new System.Drawing.Point(636, 33);
            this.pb_TargetFolderPath.Name = "pb_TargetFolderPath";
            this.pb_TargetFolderPath.Size = new System.Drawing.Size(45, 45);
            this.pb_TargetFolderPath.TabIndex = 0;
            this.pb_TargetFolderPath.TabStop = false;
            this.toolTip.SetToolTip(this.pb_TargetFolderPath, "ファイルパス収集の設定");
            this.pb_TargetFolderPath.Click += new System.EventHandler(this.pb_TargetFolderPath_Click);
            // 
            // toolTip
            // 
            this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(133)))), ((int)(((byte)(244)))));
            this.toolTip.ForeColor = System.Drawing.Color.White;
            // 
            // fmFileSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.flp_SearchResult);
            this.Controls.Add(this.pnl_form);
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "fmFileSearch";
            this.Text = "ファイル検索";
            this.pnl_form.ResumeLayout(false);
            this.pnl_form.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Search)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_TargetFolderPath)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flp_SearchResult;
        private System.Windows.Forms.Panel pnl_form;
        private System.Windows.Forms.PictureBox pb_TargetFolderPath;
        private System.Windows.Forms.PictureBox pb_Search;
        private System.Windows.Forms.TextBox tb_SearchWord;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

