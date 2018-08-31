using System;
using System.Collections.Generic;

namespace Utility
{
    public class RandomHelper
    {
        private static Random rd = new Random();

        public static IList<int> Get(int total, int count, int min)
        {
            if (total <= 0 || count <= 0 || min <= 0) return null;

            IList<int> list = new List<int>();

            int safe_total = 0;
            int min_total = min;
            int money = 0;
            for (int i = 1; i < count; i++)
            {
                safe_total = (int)Math.Ceiling((double)(total - (count - i) * min_total) / (double)(count - i));
                //min_total = (int)Math.Ceiling(((double)safe_total / 2) * ((double)(count - i) / (double)count));
                money = rd.Next(min_total, safe_total);
                total = total - money;
                InsertList(list, money);
            }

            InsertList(list, total);

            return list;
        }

        private static void InsertList(IList<int> list, int value)
        {
            if (list == null || list.Count < 2)
            {
                list.Add(value);
            }
            else
            {
                list.Insert(rd.Next(0, list.Count - 1), value);
            }
        }

        public static IList<int> Get2(int total, int count, int min)
        {
            if (total <= 0 || count <= 0 || min <= 0 || total < count) return null;

            IList<int> list = new List<int>();

            double value = (double)total / (double)count;
            if ((value * 10) % 2 != 0)
            {
                return null;
            }

            for (int i = 0; i < count; i++)
            {
                list.Add((int)value);
            }

            return list;
        }
    }
}
