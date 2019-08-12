using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _16._03._17
{
    class GetSet
    {
        public static string GetPath(Request r)
        {
            string str = "";
            List<Path> list = r.Path.ToList();
            for (int i = 0; i < list.Count(); i++)
            {
                str += list.Where(p => p.ordinalNum == i + 1).ToList()[0].path1;
            }

            return str;
        }
        public static string GetContent(Request r)
        {
            string str = "";
            var list = r.Contents.ToList();
            for (int i = 0; i < list.Count(); i++)
            {
                str += list.Where(p => p.ordinalNum == i + 1).ToList()[0].contens;
            }

            return str;
        }
        public static void setPath(Request r, string str)
        {
            List<Path> list = r.Path.ToList();

            for (int i = 0; i < list.Count(); i++)
            {
                r.Path.Remove(list[i]);
            }
            ItemsToSend.Tasks.SaveChanges();
            int ind = 1; string tmp; Path path;
            while (str.Length > 4000)
            {
                tmp = str.Substring(0, 4000);
                path = new Path();
                path.requestId = r.requestId;
                path.Request = r;
                path.ordinalNum = ind++;
                path.path1 = tmp;
                r.Path.Add(path);
                ItemsToSend.Tasks.SaveChanges();

                str = str.Substring(4000, str.Length - 4000);
            }
            if (str.Length > 0)
            {
                path = new Path();
                path.requestId = r.requestId;
                path.Request = r;
                path.ordinalNum = ind++;
                path.path1 = str;
                r.Path.Add(path);
                ItemsToSend.Tasks.SaveChanges();
            }

        }
    }
}
