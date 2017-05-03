using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HBProject.Model
{
    public class Common
    {
        /// <summary>  
        /// 将Datatable转换为List集合  
        /// </summary>  
        /// <typeparam name="T">类型参数</typeparam>  
        /// <param name="dt">datatable表</param>  
        /// <returns></returns>  
        public static List<T> DataTableToList<T>(DataTable dt)
        {
            var list = new List<T>();
            Type t = typeof(T);
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());

            foreach (DataRow item in dt.Rows)
            {
                T s = System.Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                    if (info != null)
                    {
                        if (!Convert.IsDBNull(item[i]))
                        {
                            info.SetValue(s, item[i], null);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }
        public static IList<T> ConvertToList<T>(DataTable dt) where T : new()
        {
            // 定义集合 
            List<T> ts = new List<T>();

            // 获得此模型的类型 
            Type type = typeof(T);
            //定义一个临时变量 
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行  
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性 
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性 
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量   
                    //检查DataTable是否包含此列（列名==对象的属性名）     
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter   
                        if (!pi.CanWrite) continue;//该属性不可写，直接跳出   
                        //取值   
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性   
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                //对象添加到泛型集合中 
                ts.Add(t);
            }
            return ts;
        }
        public static List<T> DataTableToT<T>(DataTable source) where T : class, new()
        {
            List<T> itemlist = null;
            if (source == null || source.Rows.Count == 0)
            {
                return itemlist;
            }
            itemlist = new List<T>();
            T item = null;
            Type targettype = typeof(T);
            Type ptype = null;
            Object value = null;
            foreach (DataRow dr in source.Rows)
            {
                item = new T();
                foreach (PropertyInfo pi in targettype.GetProperties())
                {
                    if (pi.CanWrite && source.Columns.Contains(pi.Name))
                    {
                        ptype = Type.GetType(pi.PropertyType.FullName);
                        value = Convert.ChangeType(dr[pi.Name], ptype);
                        pi.SetValue(item, value, null);
                    }
                }
                itemlist.Add(item);
            }

            return itemlist;
        }
        public static T DataRowToT<T>(DataRow source) where T : class,new()
        {
            T item = null;
            if (source == null)
            {
                return item;
            }
            item = new T();
            Type targettype = typeof(T);
            Type ptype = null;
            Object value = null;

            foreach (PropertyInfo pi in targettype.GetProperties())
            {
                if (pi.CanWrite && source.Table.Columns.Contains(pi.Name))
                {
                    ptype = Type.GetType(pi.PropertyType.FullName);
                    value = Convert.ChangeType(source[pi.Name], ptype);
                    pi.SetValue(item, value, null);
                }
            }
            return item;
        }
    }

}
