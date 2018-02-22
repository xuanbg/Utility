using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Insight.Utils.Common
{
    public class SqlHelper
    {

        // 数据库连接字符串
        private readonly string conStr;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="con">数据库连接字符串</param>
        public SqlHelper(string con)
        {
            conStr = con;
        }

        /// <summary>
        /// 返回DataTable的带可变参数组查询方法
        /// </summary>
        /// <param name="sql">sql命令</param>
        /// <param name="parameters">可变长参数组</param>
        /// <returns>DataTable 查询结果集</returns>
        public DataTable SqlQuery(string sql, params SqlParameter[] parameters)
        {
            var cmd = MakeCommand(sql, parameters);
            return SqlQuery(cmd);
        }

        /// <summary>
        /// 返回DataTable的带可变参数组查询方法
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <returns>DataTable 查询结果集</returns>
        public DataTable SqlQuery(SqlCommand cmd)
        {
            using (var conn = new SqlConnection(conStr))
            {
                conn.Open();
                cmd.Connection = conn;
                try
                {
                    var table = new DataTable("DataTable");
                    var adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(table);
                    table.PrimaryKey = new[] {table.Columns["ID"]};
                    return table;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 返回受影响行数的方法
        /// </summary>
        /// <param name="sql">sql命令</param>
        /// <param name="parameters">可变长参数组</param>
        /// <returns>int 受影响行数</returns>
        public int SqlNonQuery(string sql, params SqlParameter[] parameters)
        {
            var cmd = MakeCommand(sql, parameters);
            return SqlNonQuery(cmd);
        }

        /// <summary>
        /// 返回受影响行数的方法
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <returns>int 受影响行数</returns>
        public int SqlNonQuery(SqlCommand cmd)
        {
            using (var conn = new SqlConnection(conStr))
            {
                conn.Open();
                cmd.Connection = conn;
                try
                {
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 返回第一行第一列内容的方法
        /// </summary>
        /// <param name="sql">sql命令</param>
        /// <param name="parameters">可变长参数组</param>
        /// <returns>执行SQL语句后的第一行第一列内容</returns>
        public object SqlScalar(string sql, params SqlParameter[] parameters)
        {
            var cmd = MakeCommand(sql, parameters);
            return SqlScalar(cmd);
        }

        /// <summary>
        /// 返回第一行第一列内容的方法
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <returns>执行SQL语句后的第一行第一列内容</returns>
        public object SqlScalar(SqlCommand cmd)
        {
            using (var conn = new SqlConnection(conStr))
            {
                conn.Open();
                cmd.Connection = conn;
                try
                {
                    return cmd.ExecuteScalar();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="cmds">多条SQL语句</param>
        /// <returns>bool 执行是否成功</returns>
        public bool SqlExecute(IEnumerable<SqlCommand> cmds)
        {
            using (var conn = new SqlConnection(conStr))
            {
                conn.Open();
                var ids = new List<object>();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var cmd in cmds)
                    {
                        if (cmd.Parameters.IndexOf("@Read") > 0 && (Guid)cmd.Parameters[0].Value == Guid.Empty)
                            cmd.Parameters[0].Value = ids[(int)cmd.Parameters["@Read"].Value];

                        if (cmd.Parameters.IndexOf("@Get") > 0 && (Guid)cmd.Parameters[1].Value == Guid.Empty)
                            cmd.Parameters[1].Value = ids[(int)cmd.Parameters["@Get"].Value];

                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        var obj = cmd.ExecuteScalar();
                        if (cmd.Parameters.IndexOf("@Write") <= 0) continue;

                        var val = (int)cmd.Parameters["@Write"].Value;
                        if (ids.Count <= val) ids.Add(obj); else ids[val] = obj;
                    }
                    tran.Commit();
                    return true;
                }
                catch(Exception)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="cmds">多条SQL语句</param>
        /// <param name="index">返回ID索引位置</param>
        /// <returns>object 指定索引位置的ID</returns>
        public object SqlExecute(IEnumerable<SqlCommand> cmds, int index)
        {
            using (var conn = new SqlConnection(conStr))
            {
                conn.Open();
                var ids = new List<object>();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var cmd in cmds)
                    {
                        if (cmd.Parameters.IndexOf("@Read") > 0 && (Guid)cmd.Parameters[0].Value == Guid.Empty)
                            cmd.Parameters[0].Value = ids[(int)cmd.Parameters["@Read"].Value];

                        if (cmd.Parameters.IndexOf("@Get") > 0 && (Guid)cmd.Parameters[1].Value == Guid.Empty)
                            cmd.Parameters[1].Value = ids[(int)cmd.Parameters["@Get"].Value];

                        cmd.Connection = conn;
                        cmd.Transaction = tran;
                        var obj = cmd.ExecuteScalar();
                        if (cmd.Parameters.IndexOf("@Write") <= 0) continue;

                        var val = (int)cmd.Parameters["@Write"].Value;
                        if (ids.Count <= val) ids.Add(obj); else ids[val] = obj;
                    }
                    tran.Commit();
                    return ids[index];
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return null;
                }
            }
        }

        /// <summary>
        /// 组装SqlCommand对象
        /// </summary>
        /// <param name="sql">sql命令</param>
        /// <param name="parameters">可变长参数组</param>
        /// <returns>SqlCommand 组装完成的SqlCommand对象</returns>
        public SqlCommand MakeCommand(string sql, params SqlParameter[] parameters)
        {
            var cmd = new SqlCommand(sql);
            foreach (var p in parameters)
            {
                if (p.Value == null || p.Value.ToString() == "")
                {
                    p.Value = DBNull.Value;
                }
                cmd.Parameters.Add(p);
            }
            return cmd;
        }
    }
}
