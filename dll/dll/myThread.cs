using System;
using System.Collections.Generic;
using System.Threading;
namespace _16._03._17.Class
{
    class myThread<T>
    {
        private static int insertion = -2;
        private static int deletion = -2;
        private static int substitution = -1;
        private static int match = 2;
        public MyLists<T> N { get { return _n; } }
        private MyLists<T> _n;
        ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();
        private static int threadNum = 3;

        public List<T> HirshbergRes { get { return _hirshbergRes; } }
        private List<T> _hirshbergRes;

        private ManualResetEvent _doneEvent;
        public myThread(MyLists<T> n, ManualResetEvent doneEvent)
        {
            _n = n;
            _doneEvent = doneEvent;
        }
        // Wrapper method for use with thread pool.
        public void ThreadPoolCallback(object threadContext)
        {
            _hirshbergRes = Hirschberge(N.X, N.Y);
            _doneEvent.Set();
        }

        public List<T> Hirschberge(List<T> x, List<T> y)
        {
            List<T> resultl = new List<T>(), resultr = new List<T>(), result = new List<T>();
            if (x.Count <= 2 || y.Count <= 2) 
            {
                result = Lcs(x, y);
            }
            else if (x.Count > 0 && y.Count > 0)
            {
                int xlen = x.Count;
                int xmid = xlen / 2;
                int ylen = y.Count;
                int[] scoreL = lastLineAlign(CutArr(x, 0, xmid - 1, false), y);
                int[] scoreR = lastLineAlign(CutArr(x, xmid, xlen - 1, true), CutArr(y, 0, ylen - 1, true));
                int ymid = PartitionY(scoreL, scoreR);
              
                readerWriterLock.EnterWriteLock();
                if (threadNum > 0)
                {
                    threadNum--;
                    readerWriterLock.ExitWriteLock();
                    ManualResetEvent[] doneEvents = new ManualResetEvent[2];
                    myThread<T>[] hirshbergArray = new myThread<T>[2];
                    doneEvents[0] = new ManualResetEvent(false);
                    myThread<T> f1 = new myThread<T>(new MyLists<T>(CutArr(x, 0, xmid - 1, false), CutArr(y, 0, ymid, false)), doneEvents[0]);
                    hirshbergArray[0] = f1;
                    ThreadPool.QueueUserWorkItem(f1.ThreadPoolCallback, 1);
                    doneEvents[1] = new ManualResetEvent(false);
                    myThread<T> f2 = new myThread<T>(new MyLists<T>(CutArr(x, xmid, xlen - 1, false), CutArr(y, ymid + 1, ylen - 1, false)), doneEvents[1]);
                    hirshbergArray[1] = f2;
                    ThreadPool.QueueUserWorkItem(f2.ThreadPoolCallback, 2);
                    ThreadPool.SetMaxThreads(8, 8);
                    // Wait for all threads in pool to calculation...
                    WaitHandle.WaitAll(doneEvents);

                    result = MyAddRange( hirshbergArray[1].HirshbergRes,hirshbergArray[0].HirshbergRes, hirshbergArray[0].HirshbergRes.Count);
                }
                else
                {
                    readerWriterLock.ExitWriteLock();
                    resultl = Hirschberge(CutArr(x, 0, xmid - 1, false), CutArr(y, 0, ymid, false));
                    resultr = Hirschberge(CutArr(x, xmid, xlen - 1, false), CutArr(y, ymid + 1, ylen - 1, false));
                    result = MyAddRange(resultr,resultl,  resultl.Count);
                }
            }
            return result;
        }
        /// <summary>
        /// find lcs
        /// </summary>
        /// <param name="x">source List</param>
        /// <param name="y">new List</param>
        /// <returns>lcs</returns>

        public List<T> Lcs(List<T> x, List<T> y)
        {
            int i, j;
            int[,] M = new int[x.Count + 1, y.Count + 1];
            char[,] Path = new char[x.Count + 1, y.Count + 1];
            for (i = 1; i <= y.Count; i++)
            {
                M[0, i] = M[0, i - 1] + insertion;
                Path[0, i] = 'l';
            }
            for (j = 1; j <= x.Count; j++)
            {
                M[j, 0] = M[j - 1, 0] + deletion;
                Path[j, 0] = 'u';
            }

            for (i = 1; i < x.Count + 1; i++)
            {
                for (j = 1; j < y.Count + 1; j++)
                {
                    if (x[i - 1].Equals(y[j - 1]))
                    {
                        M[i, j] = Max(M[i - 1, j - 1] + match, M[i - 1, j] + insertion, M[i, j - 1] + deletion);
                        if (M[i, j] == M[i - 1, j - 1] + match)
                        { Path[i, j] = 'd'; }
                        else if (M[i, j] == M[i - 1, j] + insertion)
                        { Path[i, j] = 'u'; }
                        else
                        { Path[i, j] = 'l'; }
                    }
                    else
                    {
                        M[i, j] = Max(M[i - 1, j - 1] + substitution, M[i - 1, j] + insertion, M[i, j - 1] + deletion);
                        if (M[i, j] == M[i - 1, j - 1] + substitution)
                        { Path[i, j] = 'd'; }
                        else if (M[i, j] == M[i - 1, j] + insertion)
                        { Path[i, j] = 'u'; }
                        else
                        { Path[i, j] = 'l'; }
                    }
                }
            }
            i = x.Count;
            j = y.Count;
            List<T> res = new List<T>();
            while (i > 0 && j > 0)
            {
                if (Path[i, j] == 'd')
                {
                    if (x[i - 1].Equals(y[j - 1]))
                    {
                        res.Add(x[i - 1]);
                    }
                    i -= 1;
                    j -= 1;
                }
                else if (Path[i, j] == 'u')
                {
                    i -= 1;

                }
                else if (Path[i, j] == 'l')
                {
                    j -= 1;
                }
            }
            return res;

        }
        /// <summary>
        /// splite arrey
        /// </summary>
        /// <param name="arr">arrey</param>
        /// <param name="begin">begin index</param>
        /// <param name="end">end index</param>
        /// <param name="IsReversed">from begin/ from end</param>
        /// <returns>splited arrey</returns>
        private List<T> CutArr(List<T> arr, int begin, int end, bool IsReversed)
        {
            int lim = end - begin + 1;
            if (end >= arr.Count)
                return new List<T>();
            List<T> res = new List<T>();
            if (IsReversed)
            {
                for (int i = 0; i < lim; i++)
                {
                    res.Add(arr[end - i]);
                }
            }
            else
            {
                for (int i = 0; i < lim; i++)
                {
                    res.Add(arr[begin + i]);
                }
            }
            return res;
        }
        /// <summary>
        /// calculate the last line points
        /// </summary>
        /// <param name="x">column</param>
        /// <param name="y">row</param>
        /// <returns></returns>
        public int[] lastLineAlign(List<T> x, List<T> y)
        {
            List<T> row = y;
            List<T> column = x;
            int minLen = y.Count;
            int[] prev = new int[minLen + 1];
            int[] current = new int[minLen + 1];
            //initialization of the first row
            for (int i = 1; i <= minLen; i++)
            {
                prev[i] = prev[i - 1] + insertion;
            }
            current[0] = 0;
            for (int j = 1; j < column.Count + 1; j++)
            {
                current[0] += deletion;
                //points
                for (int i = 1; i < minLen + 1; i++)
                {
                    if (row[i - 1].ToString() == column[j - 1].ToString())
                    {
                        current[i] = Max(current[i - 1] + insertion, prev[i - 1] + match, prev[i] + deletion);
                    }
                    else
                    {
                        current[i] = Max(current[i - 1] + insertion, prev[i - 1] + substitution, prev[i] + deletion);
                    }
                }
                //ready to next row
                current.CopyTo(prev, 0);
            }
            return current;
        }
        /// <summary>
        /// connect two lists type T
        /// </summary>
        /// <param name="firstList">first list</param>
        /// <param name="lastList">last list</param>
        /// <param name="lSize">last list size</param>
        /// <returns></returns>
        public List<T> MyAddRange(List<T> firstList, List<T> lastList, int lSize)
        {
            for (int i = 0; i < lSize; i++)
            {
                firstList.Add(lastList[i]);
            }
            return firstList;
        }
        /// <summary>
        /// connect two char lists 
        /// </summary>
        /// <param name="firstList">first list</param>
        /// <param name="lastList">last list</param>
        /// <param name="lSize">last list size</param>
        /// <returns></returns>
        public List<char> MyAddRange(List<char> firstList, List<char> lastList, int lSize)
        {
            for (int i = 0; i < lSize; i++)
            {
                firstList.Add(lastList[i]);
            }
            return firstList;
        }
        /// <summary>
        /// splite row index
        /// </summary>
        /// <param name="scorelL">row</param>
        /// <param name="scorelR">column</param>
        /// <returns>index</returns>
        public int PartitionY(int[] scorelL, int[] scorelR)
        {
            int max_index = 0;
            int max_sum = int.MinValue;
            for (int i = 0; i < scorelL.Length - 1; i++)
            {
                if (scorelL[i] + scorelR[scorelR.Length - i - 1] > max_sum)
                {
                    max_index = i;
                    max_sum = scorelL[i] + scorelR[scorelR.Length - i - 1];
                }
            }
            return max_index - 1;
        }
        /// <summary>
        /// return max value
        /// </summary>
        private int Max(int a, int b, int c)
        {
            if (a >= b && a >= c)
            {
                return a;
            }
            else if (b >= a && b >= c)
            {
                return b;
            }
            else
            {
                return c;
            }
        }
    }
}
