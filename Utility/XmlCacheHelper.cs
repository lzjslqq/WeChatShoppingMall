using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Utility
{
    public class XmlCacheHelper
    {
        /// <summary>
        /// 从xml获取配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="xmlPath">xml物理路径</param>
        /// <returns></returns>
        public static T Get<T>(string key, string xmlPath) where T : class
        {
            // 首先尝试从缓存中获取运行参数
            T t = HttpRuntime.Cache[key].To<T>();

            if (t == null)
            {
                // 缓存中没有，则从文件中加载
                t = XmlHelper.DeserializeFromFile<T>(xmlPath, Encoding.UTF8);

                // 把从文件中读到的结果放入缓存，并设置与文件的依赖关系。
                CacheDependency dep = new CacheDependency(xmlPath);
                // 如果您的参数较复杂，与多个文件相关，那么也可以使用下面的方式，传递多个文件路径。
                //CacheDependency dep = new CacheDependency(new string[] { path });
                HttpRuntime.Cache.Insert(key, t, dep);
            }

            return t;
        }
    }
}