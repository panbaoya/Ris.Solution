using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Dal
{
    //https://www.cnblogs.com/jiagoushi/p/4051270.html
    public class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// 数据上下文字段
        /// </summary>
        protected DbContext _context;
        /// <summary>
        /// 实体集合字段
        /// </summary>
        private IDbSet<T> _entities;

        /// <summary>
        /// 实体属性
        /// </summary>
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        /// <summary>
        /// 仓储构造
        /// </summary>
        /// <param name="connectionString"></param>
        public Repository(string connectionString=null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                _context = new DbContext("name=DB_RisEntities");
            }
            else
            {
                _context = new DbContext(connectionString);
            }
        }

        public DbContextTransaction BeginTran()
        {
            return _context.Database.BeginTransaction();
        }

        /// <summary>
        /// 仓储实例
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        public IRepository<T> Instance(string connectionString)
        {
            _context = new DbContext(connectionString);
            return this;
        }

        /// <summary>
        /// 扩展方法，自带方法不能满足的时候可以添加新方法
        /// </summary>
        /// <returns></returns>
        public List<T> CommQuery(string json)
        {
            //base.Context.Queryable<T>().ToList();可以拿到SqlSugarClient 做复杂操作
            return null;
        }

        /// <summary>
        /// 扩展方法，自带方法不能满足的时候可以添加新方法
        /// </summary>
        /// <returns></returns>
        public DateTime GetDate()
        {
            var now = this.Entities.Select(t => SqlFunctions.GetDate()).FirstOrDefault();
            if (!now.HasValue)
            {
                now = new DateTime(1900, 1, 1, 0, 0, 0);
            }
            return now.Value;
        }

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>受影响行数</returns>
        public int Insert(T entity)
        {
            try
           {
               if (entity == null)
                   throw new ArgumentNullException("entity");

                this._context.Configuration.AutoDetectChangesEnabled = false;
                this._context.Configuration.ValidateOnSaveEnabled = false;
                this.Entities.Add(entity); 
                this._context.SaveChanges();
                return 1;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);

                // Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>受影响行数</returns>
        public int Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity为空!");
            }
            this.Entities.Attach(entity);
            this.Entities.Remove(entity);
            return this._context.SaveChanges();
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>受影响行数</returns>
        public int Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity实体为空!");
            }
            this._context.Configuration.ValidateOnSaveEnabled = false;
            this._context.Configuration.AutoDetectChangesEnabled = false;
            var entry = this._context.Entry<T>(entity);
            entry.State = EntityState.Modified;
            return this._context.SaveChanges();
        }

        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>实体</returns>
        public T GetById(object id)
        {
            return _entities.Find(id);
        }

        /// <summary>
        /// 根据表达式获取实体
        /// </summary>
        /// <param name="lambda">表达式</param>
        /// <returns>实体</returns>
        public T GetModel(Expression<Func<T, bool>> lambda)
        {
            return this.Entities.FirstOrDefault(lambda);
        }

        /// <summary>
        /// 根据表达式获取实体列表
        /// </summary>
        /// <param name="lambda">表达式</param>
        /// <returns>实体列表</returns>
        public List<T> GetList(Expression<Func<T, bool>> lambda)
        {
            return this.Entities.AsNoTracking().Where(lambda).ToList();
        }

        public List<T> GetList()
        {
            return this.Entities.AsNoTracking().ToList();
        }

        public List<T> GetListBySql(string sql, params object[] param)
        {
            return _context.Database.SqlQuery<T>(sql, param).ToList();
        }

        /// <summary>
        /// 根据sql语句获取指定实体列表F
        /// </summary>
        /// <typeparam name="F">指定实体</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数列表</param>
        /// <returns>实体列表</returns>
        public List<F> GetListBySql<F>(string sql,params object[] param)
        {
            return _context.Database.SqlQuery<F>(sql, param).ToList<F>();
        }

        /// <summary>
        /// 根据sql语句获取实体
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数列表</param>
        /// <returns>实体列表</returns>
        public T GetModelBySql(string sql, params object[] param)
        {
            return this._context.Database.SqlQuery<T>(sql, param).FirstOrDefault<T>();
        }
    }
}
