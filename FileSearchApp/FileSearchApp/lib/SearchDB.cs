using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace FileSearchApp.lib {
    class SearchDB : IDisposable {
        const string PASSWORD = "password";
        static string dbName = @"\search.db";
        private SQLiteConnection con = null;

        /// <summary>
        /// DB Open、プラグマの設定をしておく
        /// </summary>
        public SearchDB() {
            string currentFolderPath = System.Windows.Forms.Application.StartupPath;
            string dbPath = currentFolderPath + dbName;

            if (!File.Exists(dbPath)) {
                initDB();
            }

            con = new SQLiteConnection("Data Source = " + dbPath + ";password=" + PASSWORD);
            con.Open();

            using (SQLiteCommand cmd = con.CreateCommand()) {                
                cmd.CommandText = "PRAGMA journal_mode = MEMORY;";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "PRAGMA synchronous = NORMAL;";
                cmd.ExecuteNonQuery();

                //cmd.CommandText = "PRAGMA foreign_keys = ON;";
                //cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// フォルダパスをDBに登録する
        /// </summary>
        /// <param name="folderPaths"></param>
        public void setFolderPath(List<List<string>> folderPaths) {
            if (folderPaths.Count == 0) { return; }
            
            using (SQLiteTransaction trans = con.BeginTransaction()) {
                using (SQLiteCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = @"DELETE FROM TargetFolderPath;"; //初期化
                    cmd.ExecuteNonQuery();

                    foreach (List<string> data in folderPaths) {

                        string sql = "INSERT INTO TargetFolderPath (FolderPath, SubFolder) VALUES(@folderPath, @subFolder);";
                        cmd.CommandText = sql;

                        cmd.Parameters.Add("folderPath", System.Data.DbType.String);
                        cmd.Parameters["folderPath"].Value = data[0];

                        cmd.Parameters.Add("subFolder", System.Data.DbType.String);
                        cmd.Parameters["subFolder"].Value = data[1];

                        cmd.ExecuteNonQuery();
                    }
                }
                trans.Commit();
            }
        }


        /// <summary>
        /// 初期状態のDBを作成する
        /// </summary>
        public void initDB() {
            string currentFolderPath = System.Windows.Forms.Application.StartupPath;
            string dbPath = currentFolderPath + dbName;

            using (SQLiteConnection con = new SQLiteConnection("Data Source = " + dbPath + ";password=" + PASSWORD)) {
                con.Open();

                using (SQLiteTransaction trans = con.BeginTransaction()) {
                    string sql = "CREATE TABLE [TargetFolderPath] (" +
                                 "[FolderPath]  VARCHAR(100)  PRIMARY KEY," +
                                 "[SubFolder]   VARCHAR(10)  NOT NULL" +
                                 ");";

                    using (SQLiteCommand cmd = con.CreateCommand()) {
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }

                    trans.Commit();
                }
            }
        }


        /// <summary>
        /// コネクションを閉じる
        /// </summary>
        public void Dispose() {
            con.Close();
        }
    }
}
