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
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tb_SearchWord = new System.Windows.Forms.TextBox();
            this.pb_TargetFolderPath = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.flp_SearchResult.SuspendLayout();
            this.pnl_form.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_TargetFolderPath)).BeginInit();
            this.SuspendLayout();
            // 
            // flp_SearchResult
            // 
            this.flp_SearchResult.Controls.Add(this.pnl_form);
            this.flp_SearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_SearchResult.Location = new System.Drawing.Point(0, 0);
            this.flp_SearchResult.Name = "flp_SearchResult";
            this.flp_SearchResult.Size = new System.Drawing.Size(784, 462);
            this.flp_SearchResult.TabIndex = 0;
            // 
            // pnl_form
            // 
            this.pnl_form.Controls.Add(this.pictureBox3);
            this.pnl_form.Controls.Add(this.tb_SearchWord);
            this.pnl_form.Controls.Add(this.pb_TargetFolderPath);
            this.pnl_form.Location = new System.Drawing.Point(3, 3);
            this.pnl_form.Name = "pnl_form";
            this.pnl_form.Size = new System.Drawing.Size(769, 100);
            this.pnl_form.TabIndex = 0;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::FileSearchApp.Properties.Resources.search;
            this.pictureBox3.Location = new System.Drawing.Point(559, 36);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(60, 38);
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBox3, "検索");
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
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "fmFileSearch";
            this.Text = "ファイル検索";
            this.flp_SearchResult.ResumeLayout(false);
            this.pnl_form.ResumeLayout(false);
            this.pnl_form.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_TargetFolderPath)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flp_SearchResult;
        private System.Windows.Forms.Panel pnl_form;
        private System.Windows.Forms.PictureBox pb_TargetFolderPath;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TextBox tb_SearchWord;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

