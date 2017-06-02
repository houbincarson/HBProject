using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBProject.DAL;
using System.Data.SQLite;
using System.Security.Cryptography;
using HBProject.Model;
using System.Windows.Forms;

namespace HBProject.DAL
{
    public class DbHelper
    {
        private readonly string _dbpath = "HBDB.db";
        private SqLiteHelper _helper = null;
        public DbHelper()
        {
          
        }

        public void CreateDbFile()
        {
            var path = string.Format("data source={0}\\{1}", Application.StartupPath, _dbpath);
            _helper = new SqLiteHelper(path); 
        }
        //创建数据库；
        public void CreateDb()
        {
             
        }

        public void DeleteDb()
        {
            var path = string.Format("data source={0}\\{1}", Application.StartupPath, _dbpath);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _helper = null;
        }

        //创建表
        public void CreateTable()
        {
            var conn = _helper.CreateCommand("create table if not exists userinfo (id integer PRIMARY KEY autoincrement , name varchar(200),password varchar(200), createdate datetime default (datetime('now', 'localtime')))");
            _helper.ExecuteNonQuery(conn);
        }
        
        
        //数据查询 
        public DataSet QueryUserInfo()
        {
            var conn = _helper.CreateCommand("select * from userinfo");  
            return _helper.ExecuteDataSet(conn);
        }
        //数据新增
        public object InsertUserInfo(UserInfo userInfo)
        {
            var sql = "insert into userinfo(name,password) values(@name,@password);select last_insert_rowid() AS id;";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@name",userInfo.UserName),
                new SQLiteParameter("@password",userInfo.Password) 
            };
            var conn = _helper.CreateCommand(sql, parameters);
            return _helper.ExecuteScalar(conn);
        }
        //数据修改
        public int UpdateUserInfo(UserInfo userInfo)
        {
            if (string.IsNullOrEmpty(userInfo.id))
            {
                return -1;
            }
            var sql = "update userinfo set name = '" + userInfo.UserName + "',password = '" + userInfo.Password +
                      "' where id = " + userInfo.id;
            var conn = _helper.CreateCommand(sql);
            return _helper.ExecuteNonQuery(conn);
        }
        //数据删除
        public object DeleteUserInfo(UserInfo userInfo)
        {
            if (string.IsNullOrEmpty(userInfo.id))
            {
                return -1;
            }
            var sql = "delete from  userinfo where id = " + userInfo.id + "; select count(*) AS Id  from userinfo;";
            var conn = _helper.CreateCommand(sql);
            return _helper.ExecuteScalar(conn);
        }
    }
}
