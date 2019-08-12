using _16._03._17.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace _16._03._17
{
    class Program
    {
        static void Main(string[] args)
        {
            RunImacro runImacro;
            List<KeyValuePair<Request, string>> sendList = new List<KeyValuePair<Request, string>>();
            ItemsToSend itemsToSend;
            myThread<char> dinamicPrograming = new myThread<char>(null, new System.Threading.ManualResetEvent(false));
            //DinamicPrograming<char> dinamicPrograming = new DinamicPrograming<char>();

            EditHtml editHtml;

            Paint paintDiff;
           // RunImacro runImacro;
            // int[] arr;
            string fname = "", content = "", newHtml = "";
            itemsToSend = new ItemsToSend();

            foreach (Request request in itemsToSend.NotCheckList)
            {
                sendList.Add(new KeyValuePair<Request, string>(request, GetSet.GetPath(request)));
            }
            foreach (Request request in itemsToSend.CheckList)
            {
                Stopwatch sw = Stopwatch.StartNew();
                // rest of the code

                fname = "newHtml" + request.requestId;
                content = GetSet.GetContent(request);
                string appDirectory = Directory.GetCurrentDirectory();
                content += " \n SAVEAS TYPE = HTML FOLDER =" + appDirectory + " FILE =" + fname;
                //גלישה-
                runImacro = new RunImacro(content);
                if (runImacro.run() != iMacros.Status.sOk)
                    continue;
                newHtml = File.ReadAllText(fname + ".html");
                //newHtml = File.ReadAllText("C:\\Users\\318343407\\Desktop\\t1.html");
                //editHtml = new EditHtml(File.ReadAllText("C:\\Users\\318343407\\Desktop\\t.html"), newHtml);
                editHtml = new EditHtml(GetSet.GetPath(request), newHtml);
                List<char> res = dinamicPrograming.Hirschberge(editHtml.EditeSourse.ToList(), editHtml.EditeNewHtml.ToList());
                List<int> arr = Diff<char>.GetDiff(res, editHtml.EditeNewHtml.ToList());
                int i = 0;
                for (; i < arr.Count && (i == 0 || arr[i] != 0); i++)
                {
                    arr[i] = editHtml.Arr[arr[i]];
                }

                if (arr.Count > 0)
                {
                    paintDiff = new Paint(newHtml, arr.ToArray(), editHtml.Arr, i);
                    paintDiff.PaintYellow();
                    sendList.Add(new KeyValuePair<Request, string>(request, paintDiff.Html));
                }
                else
                {
                    if (editHtml.EditeSourse.Length > res.Count)
                        sendList.Add(new KeyValuePair<Request, string>(request, GetSet.GetPath(request)));
                }
                sw.Stop();
                //Console.WriteLine("Total time (ms): {0}", (long)sw.ElapsedMilliseconds);

            }
            foreach (var item in sendList)
            {
                SendMail.Send(item.Key.User.mail, item.Value, item.Key.name);
                GetSet.setPath(item.Key, item.Value);
            }
            TasksEntities1 task = new TasksEntities1();

        }
   }
}
