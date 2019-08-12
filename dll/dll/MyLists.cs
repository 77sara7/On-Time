using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _16._03._17.Class
{
    class MyLists<T>
    {
        public List<T> X { get; set; }
        public List<T> Y { get; set; }
        public MyLists( List<T> x,List<T> y)
        {
            X = x;
            Y = y;
        }
    }
}
