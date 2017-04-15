using Configuration.Connetcion;
using Dao.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dao.Model;
using System.Data.SqlClient;
using Dapper;
using Extension;

namespace Dao.ProcessObject
{
    /// <summary>
    /// Microsoft SQL server 連接
    /// </summary>
    public class MSSqlTool : ITransaction
    {
        private IDbConnection _dbCon;
        private string ConnectionString { get; set; }
        private ConnectionContent Content { get; set; }
        private DateTime DaoTime { get; set; }

        public MSSqlTool() { }

        public MSSqlTool(ConnectionContent connectContent)
        {
            this.ConnectionString = connectContent.ConnectionString;
            this.Content = connectContent;
        }

        /// <summary>
        /// 執行DB transaction動作
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public int Execute(DaoSqlSetting settings)
        {
            int affectCount = 0;
            this.DaoTime = this.GetDbTime();
            using(_dbCon = new SqlConnection(this.ConnectionString))
            {
                _dbCon.Open();
                using(var transaction = _dbCon.BeginTransaction())
                {
                    try
                    {
                        Dictionary<string, object> parameters = settings.Parameters.Ext_ToDictionary();
                        parameters.Add("DaoTime", this.DaoTime);
                        affectCount += this.DbProcess(settings.Sql, parameters, _dbCon, transaction);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return affectCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public int Execute(IEnumerable<DaoSqlSetting> settings)
        {
            int affectionCount = 0;
            this.DaoTime = this.GetDbTime();
            Dictionary<string, object> parameters;
            using(_dbCon = new SqlConnection(this.ConnectionString))
            {
                _dbCon.Open();
                using(var transaction = _dbCon.BeginTransaction())
                {
                    try
                    {
                        foreach(var setting in settings)
                        {
                            if (setting.Sql.Ext_IsNullOrEmpty()) continue;

                            parameters = setting.Parameters.Ext_ToDictionary();
                            parameters.Add("DaoTime", this.DaoTime);

                            affectionCount += this.DbProcess(setting.Sql, parameters, _dbCon, transaction);
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return affectionCount;
        }

        /// <summary>
        /// 執行查詢SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object parameters)
        {
            using( _dbCon = new SqlConnection(this.ConnectionString))
            {
                //IDbConnection 要加入using Dapper才會有Query方法(Extension)
                return _dbCon.Query<T>(sql, parameters);
            }
        }

        /// <summary>
        /// 執行SQL動作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="dbCon"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        private int DbProcess(string sql, object parameters, IDbConnection dbCon, IDbTransaction trans)
        {
            return dbCon.Execute(sql, parameters, trans);
        }
        /// <summary>
        /// Dao取得DB時間
        /// </summary>
        /// <returns></returns>
        private DateTime GetDbTime()
        {
            return this.Query<DateTime>("SELECT GETDATE()", null).First();
        }
    }
}
