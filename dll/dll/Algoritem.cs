using _16._03._17;
using _16._03._17.Class;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace dll
{
    public class Algoritem
    {
        public bool MyComparer(string sorceHtml, string newHtml)
        {
            myThread<char> dinamicPrograming = new myThread<char>(null, new System.Threading.ManualResetEvent(false));
            EditHtml editHtml;
            Stopwatch sw = Stopwatch.StartNew();
            editHtml = new EditHtml(sorceHtml, newHtml);
            List<char> res = dinamicPrograming.Hirschberge(editHtml.EditeSourse.ToList(), editHtml.EditeNewHtml.ToList());
            List<int> arr = Diff<char>.GetDiff(res, editHtml.EditeNewHtml.ToList());
            int i = 0;
            for (; i < arr.Count && (i == 0 || arr[i] != 0); i++)
            {
                arr[i] = editHtml.Arr[arr[i]];
            }
            if (arr.Count > 0)
            {
                return true;
            }
            else
                return false;
        }
    }


}
