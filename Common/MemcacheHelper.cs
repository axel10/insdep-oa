using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace Common
{
    public sealed class MemcachedHelper
    {
        private static MemcachedClient MemClient;
        private MemcachedHelper(){}
        static readonly object padlock = new object();

        //线程安全的单例模式
        public static MemcachedClient getInstance()
        {
            if (MemClient == null)
            {
                lock (padlock)
                {
                    if (MemClient == null)
                    {
                        MemClientInit();
                    }
                }
            }
            return MemClient;
        }

        static void MemClientInit()
        {
            try
            {
                MemClient = new MemcachedClient();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 插入指定值
        /// </summary>
        /// <param name="key">缓存名称 </param>
        /// <param name="value">缓存值</param>
        /// <param name="dateTime">过期时间</param>
        /// <returns>返回是否成功</returns>
//        public static bool Set(string key, string value, int minutes = 10080)
        public static bool Set(string key, string value, DateTime dateTime)
        {
            MemcachedClient mc = getInstance();
            var data = mc.Get(key);

            if (data == null)
                return mc.Store(StoreMode.Add, key, value, dateTime);
            else
                return mc.Store(StoreMode.Replace, key, value, dateTime);
        }


        /// <summary>
        /// 插入指定值
        /// </summary>
        /// <param name="key">缓存名称 </param>
        /// <param name="value">缓存值</param>
        /// <returns>返回是否成功</returns>
        //        public static bool Set(string key, string value, int minutes = 10080)
        public static bool Set(string key, string value)
        {
            MemcachedClient mc = getInstance();
            var data = mc.Get(key);

            DateTime dateTime = DateTime.Now.AddMinutes(10080);
            if (data == null)
                return mc.Store(StoreMode.Add, key, value, dateTime);
            else
                return mc.Store(StoreMode.Replace, key, value, dateTime);
        }


        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            MemcachedClient mc = getInstance();
            return mc.Get(key);
        }

        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(string key)
        {
            MemcachedClient mc = getInstance();

            return mc.Remove(key);
        }

        /// <summary>
        /// 清空缓存服务器上的缓存
        /// </summary>
        public static void FlushCache()
        {
            MemcachedClient mc = getInstance();

            mc.FlushAll();
        }
    }

}
