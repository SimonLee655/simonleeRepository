using Dao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao.Interface
{
    public interface ITransaction
    {
        /// <summary>
        /// 執行SQL指令
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        int Execute(IEnumerable<DaoSqlSetting> settings);

        /// <summary>
        /// 執行SQL指令
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        int Execute(DaoSqlSetting settings);

        /// <summary>
        /// 執行查詢SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, object parameters);
    }
}
