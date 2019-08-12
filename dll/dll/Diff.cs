using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _16._03._17
{
    class Diff<T>
    {

        public static List<int> GetDiff(List<T> lcs, List<T> source)
        {
            int i ;
            List<int> res = new List<int>();
            List<int> tmp = new List<int>();

            lcs=revers(lcs);

            if (lcs.Count == 0)
            {
                int ind = 0;
                foreach (T item in source)
                {
                    res.Add(ind++);
                }
            }
            else
            {
                int lcsInd = lcs.Count - 1;
                for (i = source.Count - 1; i >= 0 && lcsInd>=0; i--)
                {
                    if (source[i].Equals(lcs[lcsInd]))
                    {
                        lcsInd--;
                    }
                    else
                    {
                        res.Add(i);
                    }
                }
                for (; i >=0; i--)
                {
                    res.Add(i);
                }

            }
            for ( i = 0; i < res.Count; i++)
            {
                tmp.Add(res[res.Count - 1 - i]);
            }
            res = tmp;
            return res;
        }

        private static List<T> revers(List<T> lcs)
        {
            List<T> t = new List<T>();
            for (int i = lcs.Count()-1; i >= 0; i--)
            {
                t.Add(lcs[i]);
            }
            return t;
        }
    }
}
