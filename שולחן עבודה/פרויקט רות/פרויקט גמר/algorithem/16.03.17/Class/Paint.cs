using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _16._03._17
{
    class Paint
    {
        public string Html { get; set; }
        public int[] result;
        public int size;
        public int[] castArr;

        private string backGround = " style='background-color:yellow'";
        private string backGround2 = " ;background-color:yellow";

        public Paint(string html, int[] result, int[] arr, int size)
        {
            this.Html = html;
            this.result = result;
            this.size = size;
            this.castArr = arr;
        }
        public void PaintYellow()
        {
            int beginChar = 0, endChar = 0, pos, tmpPos;
            bool cont = false;

            for (int i = size-1; i >=0; i--)
            {
                pos = result[i];

                switch (Html[pos])
                {
                    /// remove < and runon this char again
                    case '<':
                        {
                            int ind = castArr.ToList().IndexOf(pos);
                            pos = castArr[ind - 1];
                            i--;
                            continue;
                        }
                    ///end of atribut or element
                    case '>':
                        {
                            endChar = pos;
                            beginChar = endChar;
                            while ((beginChar > 0) && (!Html[beginChar].Equals('<')))
                            { beginChar--; }

                            if (Html[beginChar + 1].Equals('/'))//element
                            {
                                endChar = beginChar;
                                while ((endChar > 0) && (!Html[endChar].Equals('>')))
                                { endChar--; }
                                beginChar = endChar;
                                while ((beginChar > 0) && (!Html[beginChar].Equals('<')))
                                { beginChar--; }
                            }
                        }
                        break;
                    case '/':
                        {
                            endChar = pos + 1;
                            beginChar = endChar;
                            while ((beginChar > 0) && (!Html[beginChar].Equals('<')))
                            { beginChar--; }
                            cont = true;
                        }
                        break;
                    default:
                        {
                            tmpPos = pos;
                            while ((tmpPos > 0) && (!Html[tmpPos].Equals('<')) && (!Html[tmpPos].Equals('>')) && (!Html[tmpPos].Equals('/')))
                            { tmpPos--; }
                            switch (Html[tmpPos])
                            {
                                ///open tag
                                case '<':
                                    {
                                        beginChar = tmpPos;
                                        endChar = beginChar;
                                        while ((endChar < Html.Length) && (!Html[endChar].Equals('>')))
                                        { endChar++; }
                                    }
                                    break;
                                ///in tag
                                case '>':
                                    {
                                        endChar = tmpPos;
                                        beginChar = endChar;
                                        while ((beginChar > 0) && (!Html[beginChar].Equals('<')))
                                        { beginChar--; }
                                    }
                                    break;
                                ///new tag
                                case '/':
                                    {
                                        endChar = pos;
                                        while ((endChar > 0) && (!Html[endChar].Equals('>')))
                                        { endChar--; }
                                        beginChar = endChar;
                                        while ((beginChar > 0) && (!Html[beginChar].Equals('<')))
                                        { beginChar--; }
                                       // cont = true;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                }
                addColor(beginChar, endChar);
                while ((i >0) && (result[i -1] >= beginChar))
                {
                    i--;
                }
                if (cont)
                {
                    cont = false;
                    i--;
                }
            }
        }
        private void addColor(int begin, int end)
        {
            int ind = Html.IndexOf("style", begin, end - begin + 1), pos;

            if (ind == -1)
            {
                pos = Html.IndexOf("/", begin, end - begin + 1);
                if (pos == -1)
                {
                    Html = Html.Insert(end, backGround);
                }
                else
                {
                    Html = Html.Insert(pos, backGround);
                }
            }
            else
            {
                char[] c = new char[2];
                c[0] = '"';
                c[1] = "'".ToCharArray()[0];
                pos = Html.IndexOfAny(c, ind, end - begin + 1);
                pos = Html.IndexOfAny(c, pos + 1, end - begin + 1);
                if (pos != -1)
                { Html = Html.Insert(pos, backGround2); }
            }
        }
    }
}
