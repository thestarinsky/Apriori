using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combine
{
    class Apriori<T>
    {
        public static void CreateEvent(ref List<List<int>> Event)
        {
 
        }

        /// <summary>
        /// 交换两个变量
        /// </summary>
        /// <param name="a">变量1</param>
        /// <param name="b">变量2</param>
        public static void Swap(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// 经过数据摘选后的数据元，基于此部分数据进行规则分析
        /// </summary>
        private List<T> datasource;
        public List<T> DataSource { 
            get{return datasource; }
            set{datasource=value;} 
        }

        /// <summary>
        /// 计算最大K项集
        /// </summary>
        /// <param name="Event">事件集</param>
        /// <param name="list">元数据集</param>
        /// <param name="S_R">支持度</param>
        /// <returns>返回计算后的最大K项集</returns>
        public List<List<T>> CalMaxKSet(List<List<T>> Event, List<T> list, double S_R)
        {
            List<List<T>> r = new List<List<T>>();
            return r;
        }

        /// <summary>
        /// 获取元数据
        /// </summary>
        /// <param name="Event">事件集</param>
        /// <returns>根据事件集生成全部的元数据</returns>
        public static List<T> GetDataSource(List<List<T>> Event)
        {
            List<T> datasource = new List<T>();
            foreach (List<T> t in Event)
            {
                for (int i = 0; i < t.Count(); i++)
                {
                    if (datasource.Count() == 0)
                    {
                        datasource.Add(t[i]);
                    }
                    else if (datasource.IndexOf(t[i]) < 0)
                    {
                        datasource.Add(t[i]);
                    }
                }               
            }
            datasource.Sort();
            return datasource;
        }

        /// <summary>
        /// 获取递归元数据
        /// </summary>
        /// <param name="Event">事件集</param>
        /// <param name="result">上次K项集计算结果</param>
        /// <param name="S_R">支持度</param>
        /// <returns>返回剪枝后的元数据</returns>
        public static List<T> GetDataSource(List<List<T>> Event,List<List<T>> result, double S_R)
        {
            List<T> datasource = new List<T>();
            foreach (List<T> t in result)
            {
                bool tmp = CheckSupportRate(Event, t, S_R);
                if (tmp)
                {
                    for (int i = 0; i < t.Count(); i++)
                    {
                        if (datasource.Count() == 0)
                        {
                            datasource.Add(t[i]);
                        }
                        else if (datasource.IndexOf(t[i]) < 0)
                        {
                            datasource.Add(t[i]);
                        }
                    }
                }
            }
            return datasource;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T[] ConvertDataSource(List<T> list)
        {
            T[] result = new T[list.Count()];
            for(int i = 0;i<list.Count();i++)
            {
                result[i] = list[i];
            }
            return result;
        }

        /// <summary>
        /// 结果转换函数
        /// </summary>
        /// <param name="list">由组合公式计算出的结果（List<T[]>）</param>
        /// <returns></returns>
        public static List<List<T>> ConvertResult(List<T[]> list)
        {
            List<List<T>> r = new List<List<T>>();
            for (int i=0; i < list.Count(); i++)
            {
                List<T> tmp = new List<T>();
                for (int j = 0; j < list[i].Length; j++)
                {
                    tmp.Add(list[i][j]);
                }
                r.Add(tmp);
            }
            return r;
        }

        /// <summary>
        /// 检查支持度
        /// </summary>
        /// <param name="Event">事件集</param>
        /// <param name="one_of_result">K项集中的一个结果</param>
        /// <param name="S_R">预设支持度</param>
        /// <returns>当前该结果是否满足</returns>
        private static bool CheckSupportRate(List<List<T>> Event, List<T> one_of_result, double S_R)
        {
            bool check = false;
            int Event_count = Event.Count();
            double tmp_sum = 1.0,sum =0.0;
            foreach (List<T> t in Event)
            {
                foreach (T t_in in one_of_result)
                {
                    if (t.IndexOf(t_in) < 0)
                    {
                        tmp_sum = 0.0;
                        break;
                    }
                }
                sum += tmp_sum;
            }
            if (sum / Event_count > S_R)
            {
                check = true;
            }
            return check;
        }


        /// <summary>
        /// 递归算法求数组的组合(私有成员)
        /// </summary>
        /// <param name="list">返回的范型</param>
        /// <param name="t">所求数组</param>
        /// <param name="n">基础元素数量</param>
        /// <param name="m">组合目标数量</param>
        /// <param name="b">单次结果存储</param>
        /// <param name="M">组合目标数量</param>
        private static void GetCombination(ref List<T[]> list, T[] t, int n, int m, int[] b, int M)
        {
            for (int i = n; i >= m; i--)
            {
                b[m - 1] = i - 1;
                if (m > 1)
                {
                    GetCombination(ref list, t, i - 1, m - 1, b, M);
                }
                else
                {
                    if (list == null)
                    {
                        list = new List<T[]>();
                    }
                    T[] temp = new T[M];
                    for (int j = 0; j < b.Length; j++)
                    {
                        temp[j] = t[b[j]];
                    }
                    list.Add(temp);
                }
            }
        }

        /// <summary>
        /// 递归算法求排列(私有成员)
        /// </summary>
        /// <param name="list">返回的列表</param>
        /// <param name="t">所求数组</param>
        /// <param name="startIndex">起始标号</param>
        /// <param name="endIndex">结束标号</param>
        private static void GetPermutation(ref List<T[]> list, T[] t, int startIndex, int endIndex)
        {
            if (startIndex == endIndex)
            {
                if (list == null)
                {
                    list = new List<T[]>();
                }
                T[] temp = new T[t.Length];
                t.CopyTo(temp, 0);
                list.Add(temp);
            }
            else
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
                    Swap(ref t[startIndex], ref t[i]);
                    GetPermutation(ref list, t, startIndex + 1, endIndex);
                    Swap(ref t[startIndex], ref t[i]);
                }
            }
        }
        
        /// <summary>
        /// 求从起始标号到结束标号的排列，其余元素不变
        /// </summary>
        /// <param name="t">所求数组</param>
        /// <param name="startIndex">起始标号</param>
        /// <param name="endIndex">结束标号</param>
        /// <returns>从起始标号到结束标号排列的范型</returns>
        public static List<T[]> GetPermutation(T[] t, int startIndex, int endIndex)
        {
            if (startIndex < 0 || endIndex > t.Length - 1)
            {
                return null;
            }
            List<T[]> list = new List<T[]>();
            GetPermutation(ref list, t, startIndex, endIndex);
            return list;
        }

        /// <summary>
        /// 返回数组所有元素的全排列
        /// </summary>
        /// <param name="t">所求数组</param>
        /// <returns>全排列的范型</returns>
        public static List<T[]> GetPermutation(T[] t)
        {
            return GetPermutation(t, 0, t.Length - 1);
        }

        /// <summary>
        /// 求数组中n个元素的排列
        /// </summary>
        /// <param name="t">所求数组</param>
        /// <param name="n">元素个数</param>
        /// <returns>数组中n个元素的排列</returns>
        public static List<T[]> GetPermutation(T[] t, int n)
        {
            if (n > t.Length)
            {
                return null;
            }
            List<T[]> list = new List<T[]>();
            List<T[]> c = GetCombination(t, n);
            for (int i = 0; i < c.Count; i++)
            {
                List<T[]> l = new List<T[]>();
                GetPermutation(ref l, c[i], 0, n - 1);
                list.AddRange(l);
            }
            return list;
        }


        /// <summary>
        /// 求数组中n个元素的组合
        /// </summary>
        /// <param name="t">所求数组</param>
        /// <param name="n">元素个数</param>
        /// <returns>数组中n个元素的组合的范型</returns>
        public static List<T[]> GetCombination(T[] t, int n)
        {
            if (t.Length < n)
            {
                return null;
            }
            int[] temp = new int[n];
            List<T[]> list = new List<T[]>();
            GetCombination(ref list, t, t.Length, n, temp, n);
            return list;
        }
    } 
    }

