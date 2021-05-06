using AutoMapper;
using System;
using System.Collections.Generic;

namespace Nursing.Tools
{
    public static class MapperUnit
    {
        /// <summary>
        /// 对象映射
        /// </summary>
        /// <typeparam name="From">源对象</typeparam>
        /// <typeparam name="To">映射成的对象</typeparam>
        /// <param name="source">源对象实例</param>
        /// <returns></returns>
        public static To MapTo<From, To>(this From source)
            where From : class
            where To : class
        {
            if (source == null) return default(To);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ForAllMaps((a, b) => b.ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null)));
                cfg.CreateMap<From, To>();//创建映射关系
            });
            var mapper = config.CreateMapper();
            return mapper.Map<To>(source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="To"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static To MapTo<To>(this object source)
            //where From : class
            where To : class
        {
            if (source == null) return default(To);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ForAllMaps((a, b) => b.ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null)));
                cfg.CreateMap(source.GetType(), typeof(To));//创建映射关系
            });
            var mapper = config.CreateMapper();
            return mapper.Map<To>(source);
        }

        /// <summary>
        /// 列表映射
        /// </summary>
        /// <typeparam name="From"></typeparam>
        /// <typeparam name="To"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<To> MapListTo<From, To>(this IEnumerable<From> source)
           where From : class
           where To : class
        {
            if (source == null) return default(List<To>);
            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.ForAllMaps((a, b) => b.ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null)));
                var ss = cfg.CreateMap<From, To>();         //以上都是配置表达式
                ss.ForAllMembers(options => options.Condition(f => f != null));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<List<To>>(source);
        }

        /// <summary>
        /// 合并映射
        /// </summary>
        /// <typeparam name="From"></typeparam>
        /// <typeparam name="To"></typeparam>
        /// <param name="source"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static To MapTo<From, To>(this From source, To to)
        {

            if (source == null) return default(To);
            var config = new MapperConfiguration(cfg =>
            {
                //没错就是这句话
                cfg.ForAllMaps((a, b) => b.ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null)));
                cfg.CreateMap<From, To>();         //以上都是配置表达式
            });
            var mapper = new Mapper(config);// config.CreateMapper();
            return mapper.Map(source, to);
        }
    }
}
