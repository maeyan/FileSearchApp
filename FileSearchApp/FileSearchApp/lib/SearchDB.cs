using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileSearchApp.lib {
    public class SearchDB : IDisposable {

        const string PASSWORD = "password";
        static string dbName = @"\search.db";
        private SQLiteConnection con = null;
        int _resultCount = 0;
        double _searchResult = 0;

        public double searchResult {
            get { return _searchResult; }
        }

        public int resultCount {
            get { return _resultCount; }
        }

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
                    cmd.CommandText = "DELETE FROM TargetFolderPath;"; //初期化
                    cmd.ExecuteNonQuery();

                    foreach (List<string> data in folderPaths) {

                        string sql = "INSERT INTO TargetFolderPath (FolderPath, SubFolder) VALUES(@folderPath, @subFolder);";
                        cmd.CommandText = sql;

                        cmd.Parameters.Add("folderPath", System.Data.DbType.String);
                        cmd.Parameters["folderPath"].Value = data[0];

                        cmd.Parameters.Add("subFolder", System.Data.DbType.String);
                        cmd.Parameters["subFolder"].Value = data[1];

                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                }
                trans.Commit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<List<string>> getFolderPath() {
            List<List<string>> folderPaths = new List<List<string>>();

            using (SQLiteCommand cmd = con.CreateCommand()) {
                string sql = @"SELECT FolderPath, SubFolder FROM TargetFolderPath;";
                cmd.CommandText = sql;
                using (SQLiteDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        string folderPath = reader["FolderPath"].ToString();
                        string subFolder = reader["SubFolder"].ToString();
                        List<string> data = new List<string>();
                        data.Add(folderPath);
                        data.Add(subFolder);
                        folderPaths.Add(data); 
                    }
                }
            }

            return folderPaths;
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

                    sql = "CREATE TABLE [FileList] (" +
                          "[FilePath]   VARCHAR(500) PRIMARY KEY," +
                          "[FolderName] VARCHAR(500) NOT NULL," +
                          "[FileName]   VARCHAR(50)  NOT NULL" +
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
        /// DBから対象フォルダパスを取得して全ての対象フォルダパスに対し
        /// ファイルパスを取得する
        /// </summary>
        public void UpdateAllFilePaths() {
            using (SQLiteTransaction trans = con.BeginTransaction()) {
                using (SQLiteCommand cmd = con.CreateCommand()) {
                    //TargetFolderPathテーブルから対象フォルダパスをすべて取得する
                    string sql = "";

                    //1.FileListテーブルを全て削除する
                    sql = "DELETE FROM FileList;";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    //2.TargetFolderPathテーブルから対象フォルダパス情報を取得する
                    sql = "SELECT FolderPath, SubFolder FROM TargetFolderPath;";
                    cmd.CommandText = sql;

                    using(SQLiteDataReader reader = cmd.ExecuteReader()){
                        while (reader.Read()) {
                            string folderPath = reader["FolderPath"].ToString();
                            string depth = reader["SubFolder"].ToString();

                            //フォルダが見当たらない時は次の処理に移動
                            if (!Directory.Exists(folderPath)) { continue; }

                            SearchOption option;
                            if (depth == TargetFolderPath.TargetIncluseSubFolder) {
                                option = SearchOption.AllDirectories;
                            } else if (depth == TargetFolderPath.TargetCurrentFolder) {
                                option = SearchOption.TopDirectoryOnly;
                            } else {
                                MessageBox.Show("FolderPathUpdate.exe - Updateメソッドの引数が不正です");
                                return;
                            }

                            //ファイルパスを取得
                            try {
                                string[] filePaths = Directory.GetFiles(folderPath, "*", option);
                                UpdateFilePaths(folderPath, filePaths);

                            } catch (Exception ex) {
                                MessageBox.Show(ex.Message);
                                return;
                            }
                        }
                    }
                }
                trans.Commit();
            }
        }

        /// <summary>
        /// ファイルパスを更新する
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="filePaths"></param>
        public void UpdateFilePaths(string folderPath, string[] filePaths) {
            using (SQLiteTransaction trans = con.BeginTransaction()) {
                using (SQLiteCommand cmd = con.CreateCommand()) {

                    foreach (string filePath in filePaths) {
                        string sql = "INSERT INTO FileList (FilePath, FolderName, FileName) VALUES(@FilePath, @FolderName, @FileName);";
                        cmd.CommandText = sql;

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("FilePath", System.Data.DbType.String);
                        cmd.Parameters["FilePath"].Value = filePath;

                        cmd.Parameters.Add("FolderName", System.Data.DbType.String);
                        cmd.Parameters["FolderName"].Value = Path.GetDirectoryName(filePath);

                        string fileName = Path.GetFileName(filePath);
                        cmd.Parameters.Add("FileName", System.Data.DbType.String);
                        cmd.Parameters["FileName"].Value = fileName;

                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                }
                trans.Commit();
            }
        }


        /// <summary>
        /// 特定のフォルダ配下のファイルパスのみ削除する
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="depth"></param>
        public void DeleteFilePathInTargetFolder(string folderPath, string depth) {
            using (SQLiteTransaction trans = con.BeginTransaction()) {
                using (SQLiteCommand cmd = con.CreateCommand()) {

                    string sql = "";
                    if (TargetFolderPath.TargetCurrentFolder == depth) {
                        sql = "DELETE FROM FileList WHERE FolderName = @folderName";

                        cmd.CommandText = sql;
                        cmd.Parameters.Add("folderName", System.Data.DbType.String);
                        cmd.Parameters["folderName"].Value = folderPath;

                    } else if (TargetFolderPath.TargetIncluseSubFolder == depth) {
                        sql = "DELETE FROM FileList WHERE FilePath GLOB @filePath";

                        cmd.CommandText = sql;
                        cmd.Parameters.Add("filePath", System.Data.DbType.String);
                        cmd.Parameters["filePath"].Value = folderPath + "*";

                    } else {
                        throw new FileSearchException("第二引数が不正な値です");
                    }

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
        }

        public void DeleteTargetFolderPath(string folderPath) {
            using (SQLiteTransaction trans = con.BeginTransaction()) {
                using (SQLiteCommand cmd = con.CreateCommand()) {
                    string sql = "DELETE FROM TargetFolderPath WHERE FolderPath = @folderPath";
                    cmd.CommandText = sql;

                    cmd.Parameters.Add("folderPath", System.Data.DbType.String);
                    cmd.Parameters["folderPath"].Value = folderPath;

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
        }


        public List<string> selectFileData(string searchWord, int offset, int resultPerPage) {
            //filePathのリストを返す
            List<string> filePaths = new List<string>();

            Stopwatch sw = Stopwatch.StartNew();

            //件数取得
            string sql = "";
            sql += string.Format("SELECT Count(*) ");
            sql += string.Format("FROM FileList ");
            if (searchWord != "") { sql += string.Format(" WHERE lower(FileName) GLOB '*{0}*' ", searchWord.ToLower()); }
            sql += string.Format(";");

            using (SQLiteCommand cmd = con.CreateCommand()) {
                cmd.CommandText = sql;
                _resultCount = Convert.ToInt32(cmd.ExecuteScalar());
            }

            //検索結果取得
            sql = "";
            sql += string.Format("SELECT  FilePath ");
            sql += string.Format("FROM FileList ");
            if (searchWord != "") { sql += string.Format(" WHERE lower(FileName) GLOB '*{0}*' ", searchWord.ToLower()); }
            sql += string.Format("LIMIT {0} OFFSET {1};", resultPerPage, offset);


            using (SQLiteCommand cmd = con.CreateCommand()) {
                cmd.CommandText = sql;
                using (SQLiteDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        filePaths.Add(reader.GetString(0));
                    }
                }    
            }
            
            _searchResult = sw.ElapsedMilliseconds;

            return filePaths;
        }

        /// <summary>
        /// コネクションを閉じる
        /// </summary>
        public void Dispose() {
            con.Close();
        }
    }
}
