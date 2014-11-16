namespace FileSearchApp {
    partial class fmFilePathSetting {
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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tb_FolderPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.flp_targetFolderPath = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_form = new System.Windows.Forms.Panel();
            this.rb_CurrentFolder = new System.Windows.Forms.RadioButton();
            this.rb_IncludeSubFolder = new System.Windows.Forms.RadioButton();
            this.bt_addFolderPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.flp_targetFolderPath.SuspendLayout();
            this.pnl_form.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(133)))), ((int)(((byte)(244)))));
            this.toolTip.ForeColor = System.Drawing.Color.White;
            // 
            // tb_FolderPath
            // 
            this.tb_FolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_FolderPath.Location = new System.Drawing.Point(60, 64);
            this.tb_FolderPath.Name = "tb_FolderPath";
            this.tb_FolderPath.Size = new System.Drawing.Size(419, 25);
            this.tb_FolderPath.TabIndex = 0;
            this.tb_FolderPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_FolderPath_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "収集対象のフォルダパスを指定してください";
            // 
            // flp_targetFolderPath
            // 
            this.flp_targetFolderPath.AutoScroll = true;
            this.flp_targetFolderPath.Controls.Add(this.pnl_form);
            this.flp_targetFolderPath.Controls.Add(this.label1);
            this.flp_targetFolderPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_targetFolderPath.Location = new System.Drawing.Point(0, 0);
            this.flp_targetFolderPath.Name = "flp_targetFolderPath";
            this.flp_targetFolderPath.Size = new System.Drawing.Size(584, 412);
            this.flp_targetFolderPath.TabIndex = 3;
            // 
            // pnl_form
            // 
            this.pnl_form.BackColor = System.Drawing.Color.White;
            this.pnl_form.Controls.Add(this.rb_CurrentFolder);
            this.pnl_form.Controls.Add(this.rb_IncludeSubFolder);
            this.pnl_form.Controls.Add(this.bt_addFolderPath);
            this.pnl_form.Controls.Add(this.tb_FolderPath);
            this.pnl_form.Controls.Add(this.label2);
            this.pnl_form.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.pnl_form.Location = new System.Drawing.Point(3, 3);
            this.pnl_form.Name = "pnl_form";
            this.pnl_form.Size = new System.Drawing.Size(555, 101);
            this.pnl_form.TabIndex = 3;
            // 
            // rb_CurrentFolder
            // 
            this.rb_CurrentFolder.AutoSize = true;
            this.rb_CurrentFolder.Location = new System.Drawing.Point(188, 36);
            this.rb_CurrentFolder.Name = "rb_CurrentFolder";
            this.rb_CurrentFolder.Size = new System.Drawing.Size(146, 22);
            this.rb_CurrentFolder.TabIndex = 5;
            this.rb_CurrentFolder.Text = "サブフォルダ含まない";
            this.rb_CurrentFolder.UseVisualStyleBackColor = true;
            // 
            // rb_IncludeSubFolder
            // 
            this.rb_IncludeSubFolder.AutoSize = true;
            this.rb_IncludeSubFolder.Checked = true;
            this.rb_IncludeSubFolder.Location = new System.Drawing.Point(60, 36);
            this.rb_IncludeSubFolder.Name = "rb_IncludeSubFolder";
            this.rb_IncludeSubFolder.Size = new System.Drawing.Size(122, 22);
            this.rb_IncludeSubFolder.TabIndex = 4;
            this.rb_IncludeSubFolder.TabStop = true;
            this.rb_IncludeSubFolder.Text = "サブフォルダ含む";
            this.rb_IncludeSubFolder.UseVisualStyleBackColor = true;
            // 
            // bt_addFolderPath
            // 
            this.bt_addFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_addFolderPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(133)))), ((int)(((byte)(244)))));
            this.bt_addFolderPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_addFolderPath.ForeColor = System.Drawing.Color.White;
            this.bt_addFolderPath.Location = new System.Drawing.Point(485, 64);
            this.bt_addFolderPath.Name = "bt_addFolderPath";
            this.bt_addFolderPath.Size = new System.Drawing.Size(58, 25);
            this.bt_addFolderPath.TabIndex = 3;
            this.bt_addFolderPath.Text = "追加";
            this.bt_addFolderPath.UseVisualStyleBackColor = false;
            this.bt_addFolderPath.Click += new System.EventHandler(this.bt_addFolderPath_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(555, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "▼収集対象フォルダパス一覧";
            // 
            // fmFilePathSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 412);
            this.Controls.Add(this.flp_targetFolderPath);
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(600, 450);
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "fmFilePathSetting";
            this.Text = "ファイルパス収集の設定";
            this.flp_targetFolderPath.ResumeLayout(false);
            this.pnl_form.ResumeLayout(false);
            this.pnl_form.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TextBox tb_FolderPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flp_targetFolderPath;
        private System.Windows.Forms.Panel pnl_form;
        private System.Windows.Forms.Button bt_addFolderPath;
        private System.Windows.Forms.RadioButton rb_CurrentFolder;
        private System.Windows.Forms.RadioButton rb_IncludeSubFolder;
        private System.Windows.Forms.Label label1;
    }
}