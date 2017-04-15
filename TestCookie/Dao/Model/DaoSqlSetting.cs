using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao.Model
{
    /// <summary>
    /// SQL暫存容器
    /// </summary>
    public class DaoSqlSetting
    {
        #region properties
        public string Sql { get; set; }
        public Object Parameters { get; set; }
        #endregion

        #region note 20170410
        /**
         * 繼承、多型、介面
         * 
         * 成員存取限制修飾詞：
         * private/protected/public/internal
         * 
         * 多載overloading
         * 同樣方法名，給不同的參數個數或型別
         * 
         * 多型polymorphism
         * 表現出來是用相同的類別呼叫同樣的方法名稱(給相同的參數)卻有不同的結果
         * 其原因是因為是不同的實作實體 所呼叫的方法寫在父類別，實作於不同的子類別
         * 
         * : base(...) 呼叫 base constructor 給不同的參數會呼叫到不同的實作建構子
         * base.methor(...)
         * base.property
         * 
         * : this(...) 呼叫自己的其他建構子
         * !! base是子類別呼叫父類別，this用在自己類別呼叫自己類別的其他建構子
         * class Mouse
         * {
         *   public Mouse()
         *      : this(-1, "")
         *   {
         *      // Uses constructor initializer.
         *   }
         *   public Mouse(int weight, string name)
         *   {
         *      Console.WriteLine("Contructor weight = {0}, name = {1}", weight, name);
         *   }
         * }
         * 在main中呼叫兩個不同建構子
         * Mouse m1 = new Mouse();
         * // Constructor weight = -1, name =
         * Mouse m2 = new Mouse(10, "Sam");
         * // Constructor weight = 10, name = Sam
         * **/
        #endregion

        #region constructors

        ///建構子
        public DaoSqlSetting()
        {
            this.Parameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// 給一個參數sql的建構子，:this()會再呼叫沒有參數的建構子
        /// </summary>
        /// <param name="sql"></param>
        public DaoSqlSetting(string sql): this()
        {
            this.Sql = sql;
        }
        
        public DaoSqlSetting(string sql, object parameters)
            : this(sql)//由constructor 再呼叫constructor的方法 :this(sql)呼叫有一個參數的建構子
        {
            this.Parameters = parameters;
        }
        #endregion

        #region static method
        //overloadable operators 運算子多載化
        //https://msdn.microsoft.com/en-us/library/8edha89s.aspx
        //https://msdn.microsoft.com/zh-tw/library/aa288467(v=vs.71).aspx
        //簡單說重新定義運算子在此類別運算時，要做的行為
        //此例為兩個DaoSqlSetting使用加法(+)運算子時的運算方式

        public static IEnumerable<DaoSqlSetting> operator +(IEnumerable<DaoSqlSetting> array, DaoSqlSetting item)
        {
            List<DaoSqlSetting> list = array.ToList();
            list.Add(item);
            return list;
        }
        
        public static IEnumerable<DaoSqlSetting> operator +(DaoSqlSetting item1, DaoSqlSetting item2)
        {
            return new DaoSqlSetting[] { item1, item2 };
        }

        public static IEnumerable<DaoSqlSetting> operator +(DaoSqlSetting item, IEnumerable<DaoSqlSetting> array)
        {
            return new DaoSqlSetting[] { item }.Concat(array);
        }
        #endregion
    }
}
