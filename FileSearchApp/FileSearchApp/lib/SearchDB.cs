﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace FileSearchApp.lib {
    public class SearchDB : IDisposable {
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
        /// ファイルパスを更新する
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="filePaths"></param>
        public void UpdateFilePaths(string folderPath, string[] filePaths) {
            using (SQLiteTransaction trans = con.BeginTransaction()) {
                using (SQLiteCommand cmd = con.CreateCommand()) {
                    string sql = "DELETE FROM FileList WHERE upper(FilePath) GLOB upper(@folderPath)";
                    cmd.CommandText = sql;

                    cmd.Parameters.Add("folderPath", System.Data.DbType.String);
                    cmd.Parameters["folderPath"].Value = folderPath + "*";

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    foreach (string filePath in filePaths) {
                        sql = "INSERT INTO FileList (FilePath, FolderName, FileName) VALUES(@FilePath, @FolderName, @FileName);";
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
        /// コネクションを閉じる
        /// </summary>
        public void Dispose() {
            con.Close();
        }
    }
}
