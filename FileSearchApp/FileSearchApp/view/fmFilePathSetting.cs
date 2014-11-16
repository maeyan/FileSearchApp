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
    public partial class fmFilePathSetting : Form {
        public fmFilePathSetting() {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_addFolderPath_Click(object sender, EventArgs e) {
            addFolderPath();
        }

        private void addFolderPath() {
            TargetFolderPath tfp = new TargetFolderPath(this.flp_targetFolderPath);

            try {
                string depth = (this.rb_IncludeSubFolder.Checked)
                                ? TargetFolderPath.TargetIncluseSubFolder
                                : TargetFolderPath.TargetCurrentFolder;
                tfp.addFolderPath(this.tb_FolderPath.Text, depth);
                this.tb_FolderPath.Text = "";

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

        }

        private void tb_FolderPath_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                addFolderPath();
            }
        }

    }
}
