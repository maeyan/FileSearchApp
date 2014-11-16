using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
