using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileSearchApp.lib;
using System.IO;
using System.Windows.Forms;

namespace FolderPathUpdate {
    class Program {
        static void Main(string[] args) {
            //引数が２の時(フォルダパス、サブフォルダを対象とするか？)
            if (args.Length == 2) {
                try {
                    Update(args[0], args[1]);
                    MessageBox.Show("更新完了しました");
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                    return;
                }

            } else if (args.Length == 0 || args.Length == 1) {
                try {
                    SearchDB db = new SearchDB();
                    db.UpdateAllFilePaths();                    

                    if (args.Length == 1 && args[0] == "true") {
                        MessageBox.Show("更新完了しました");
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                    return;
                }
            } else {
                throw new FileSearchException("UpdateFolder.exeの引数は、0または2つ必要です");
            }
        }

        static void Update(string folderPath, string depth) {
            //存在しない、またはネットワークの不調でファイルサーバーに
            //アクセスできない場合は、そのまま終了
            if (!Directory.Exists(folderPath)) { return; }

            SearchOption option; 
            if (depth == TargetFolderPath.TargetIncluseSubFolder) {
                option = SearchOption.AllDirectories;
            } else if (depth == TargetFolderPath.TargetCurrentFolder) {
                option = SearchOption.TopDirectoryOnly;
            } else {
                MessageBox.Show("FolderPathUpdate.exe - Updateメソッドの引数が不正です");
                return;
            }

            try {
                string[] filePaths = Directory.GetFiles(folderPath, "*", option);

                SearchDB db = new SearchDB();
                db.DeleteFilePathInTargetFolder(folderPath, depth);
                
                db.UpdateFilePaths(folderPath, filePaths);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
