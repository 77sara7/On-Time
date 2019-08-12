using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _16._03._17
{
    class EditHtml
    {
        static Dictionary<string, string> tlist;
        static List<char> clist;
        private String Sourse { get; }
        private String NewHtml { get; }
        public int[] Arr { get; }
        public String EditeSourse { get; }
        public String EditeNewHtml { get; }
        private Boolean[] tmp;
        private int _preSkip;
        private int _postSkip;
        private int SavePostSkip;
        public int ArrInd { get; set; }

        public EditHtml(String sourse, String newHtml)
        {
            tlist = new Dictionary<string, string>();
            tlist.Add("script", "</script");
            tlist.Add("style", "</style");
            tlist.Add("!--", "-->");
            tlist.Add("br", "/");

            clist = new List<char>();
            clist.Add('\n');
            clist.Add(' ');
            clist.Add('\r');

            this.Sourse = sourse;
            this.NewHtml = newHtml;
            tmp = new Boolean[NewHtml.Count()];

            EditeNewHtml = CharEdit(NewHtml, true);
            Arr = new int[EditeNewHtml.Length + 1];

            EditeSourse = CharEdit(Sourse, false);
            UpdateArr();
            CalculateSkip();
            EditeNewHtml = DelSkip(EditeNewHtml);
            EditeSourse= DelSkip(EditeSourse);
            EditeNewHtml = TagEdit(EditeNewHtml, true);
            EditeSourse = TagEdit(EditeSourse, false);
            UpdateArr2();


        }

        private string DelSkip(string str)
        {
            str = str.Substring(_preSkip);
            str = str.Substring(0, str.Length - _postSkip);
            return str;
        }

        private string CharEdit(String str, Boolean IsNewHtml)
        {
            string res = null;
            bool flag;
            for (int i = 0; i < str.Length; i++)
            {
                flag = false;
                foreach (char c in clist)
                {

                    if (c.Equals(str[i]))
                    {
                        flag = true;
                        if (IsNewHtml) tmp[i] = true;

                        break;
                    }
                }
                if (!flag)
                {
                    res += str[i];
                }
            }
            return res;
        }
        /// <summary>
        ///remove list tags
        /// </summary>
        /// <param name="str">string to edite</param>
        /// <param name="IsNewHtml">is  new html</param>
        /// <returns></returns>
        private string TagEdit(String str, Boolean IsNewHtml)
        {
            if (Sourse.Equals(NewHtml))
            {
                return "";
            }
            int ind = 0;
            string res = null;
            int i;
            bool flag;
            ind = _preSkip;
            while ((i = str.IndexOf("<")) != -1)  //ריצה על כל התגיות 
            {
                flag = false;
                foreach (var item in tlist)//ריצה על רשימת התגיות להסרה
                {
                    if (str.Length - i - item.Key.Length > 0 && str.Substring(i + 1, item.Key.Length).ToLower().Equals(item.Key))//בדיקה האם התגית להסרה

                    {
                        int j = str.IndexOf(item.Value, i);
                        res = res + str.Substring(0, i);
                        if (str.Length >= j + item.Value.Length + 1)
                        {
                            str = str.Substring(j + item.Value.Length + 1);
                            if (IsNewHtml)//המחרוזת שהתקבלה היא החדשה
                            {

                                for (int ii = i; ii <= j + item.Value.Length; ii++)
                                {
                                    tmp[Arr[ind + ii]] = true;

                                }
                            }
                            ind += j + item.Value.Length + 1;
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag)
                {

                    res = res + str.Substring(0, i + 1);
                    str = str.Substring(i + 1);
                    ind += i + 1;

                }
            }
            res += str;
            return res;// + pos;
        }
        /// <summary>
        /// update arr from tmp
        /// </summary>

        private void UpdateArr()
        {
            int ind = 0;
            for (int i = _preSkip; i < NewHtml.Length - _postSkip; i++)
            {
                if (!tmp[i])
                    Arr[ind++] = i;
            }
        }

        private void UpdateArr2()
        {
            int ind = 0;
            for (int i = Arr[_preSkip]; i < NewHtml.Length - (_postSkip + SavePostSkip); i++)
            {
                if (!tmp[i])
                    Arr[ind++] = i;
            }
        }
        private void CalculateSkip()
        {
            CalculatePostSkip();
            CalculatePreSkip();
            if (_preSkip>0 && EditeNewHtml[_preSkip - 1] == '<' && EditeNewHtml[EditeNewHtml.Length - _postSkip - 1] == '>')
            {
                _postSkip++;
                _preSkip--;
            }
        }
        /// <summary>
        /// This method is an optimization that
        /// skips matching elements at the end of the 
        /// two arrays being diff'ed.
        /// Care's taken so that this will never
        /// overlap with the pre-skip.
        /// </summary>
        private void CalculatePostSkip()
        {
            int leftLen = EditeSourse.Length;
            int rightLen = EditeNewHtml.Length;
            int ind = rightLen;
            while (_postSkip < (leftLen) && _postSkip < (rightLen) &&
                EditeSourse[leftLen - _postSkip - 1].Equals(EditeNewHtml[rightLen - _postSkip - 1]))
            {
                SavePostSkip += tmp[(ind--) - 1] ? 1 : 0;
                _postSkip++;
            }

        }

        /// <summary>
        /// This method is an optimization that
        /// skips matching elements at the start of
        /// the arrays being diff'ed
        /// </summary>
        private void CalculatePreSkip()
        {
            int leftLen = EditeSourse.Length;
            int rightLen = EditeNewHtml.Length;
            while (_preSkip < leftLen-_postSkip && _preSkip < rightLen-_postSkip &&
                EditeSourse[_preSkip].Equals(EditeNewHtml[_preSkip]))
            {
                _preSkip++;
            }
        }

    }
}
