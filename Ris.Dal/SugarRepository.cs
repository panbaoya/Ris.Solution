using Ris.Tools;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Dal
{
    public class Sugar
    {
        public void setCon(string str)
        {
            _connectionString = str;
        }
        public string _connectionString { get; set; }
        SqlSugarClient client;

        public ISqlSugarClient sugarClient => GetSugarClient();

        private SqlSugarClient GetSugarClient()
        {
            //if (client == null)
            //{
            client = new SqlSugarClient(new ConnectionConfig
            {
                DbType = SqlSugar.DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,
                ConnectionString = _connectionString,
            });
            //}
            return client;
        }
    }

    public class SugarRepository<T> : SimpleClient<T>,IRepository<T> where T : class, new()
    {
        public ISqlSugarClient db => Context;
        public SugarRepository(ISqlSugarClient context = null) : base(context)//注意这里要有默认值等于null
        {
            if (context == null)
            {
                base.Context = new SqlSugarClient(new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.SqlServer,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true,
                    ConnectionString = AppConfSetting.ConnectionString
                });
            }
        }
        public DateTime GetDateTime()
        {
            return db.Ado.GetDateTime("Select Getdate()");
        }
        public int Insert(T entity)
        {
            try
            {
                return db.Insertable<T>(entity).ExecuteCommand();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        int IRepository<T>.Delete(T entity)
        {
            throw new NotImplementedException();
        }

        int IRepository<T>.Update(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetModel(Expression<Func<T, bool>> lambda)
        {
            throw new NotImplementedException();
        }

        public T GetModelBySql(string sql, params object[] param)
        {
            throw new NotImplementedException();
        }

        public List<T> GetListBySql(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public List<F> GetListBySql<F>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}