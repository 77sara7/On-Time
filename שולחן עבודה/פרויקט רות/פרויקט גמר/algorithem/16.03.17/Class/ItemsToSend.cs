using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _16._03._17
{
    class ItemsToSend
    {
        public List<Request> CheckList { get; set; }
        public List<Request> NotCheckList { get; set; }
        public static TasksEntities1 Tasks
        {
            get { return tasks; }
            set
            {
                if (tasks == null)
                    tasks = new TasksEntities1();
            }
        }
        private static TasksEntities1 tasks;
        public ItemsToSend()
        {
            CheckList = new List<Request>();//Reqest to check
            NotCheckList = new List<Request>();//Reqest to send with out check
            Tasks = new TasksEntities1();
            List<Request> requestList = new List<Request>();
            Boolean flag;
            var t = Tasks.FrequencyToRequest;
            var yyy = Tasks.FrequencyToRequest.ToList();
            Tasks.Frequency.ToList();
            Tasks.Path.ToList();
            Tasks.Contents.ToList();
            Tasks.FrequencyDays.ToList();
            Tasks.User.ToList();
            var tmp = Tasks.Request.ToList();
            foreach (Request request in tmp)
            {
                flag = false;
                var frequencyToRequestTmp = request.FrequencyToRequest.ToList();
                foreach (FrequencyToRequest frequencyToRequest in frequencyToRequestTmp)
                {
                    switch (frequencyToRequest.Frequency.frequencyId)
                    {
                        case (int)Frequency.DAYLY:
                            flag = true;
                            break;
                        case (int)Frequency.WEEKLY:
                            foreach (FrequencyDays day in frequencyToRequest.FrequencyDays.ToList())
                            {
                                if (day.day == (int)DateTime.Today.DayOfWeek)
                                {
                                    flag = true;
                                    break;

                                }
                            }
                            break;
                        case (int)Frequency.MONTHLY:
                            var frequencyDaysTmp = frequencyToRequest.FrequencyDays.ToList();
                            foreach (FrequencyDays day in frequencyDaysTmp)
                            {
                                if ((day.day == (int)DateTime.Now.Day))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    if (flag)
                    {
                        if (frequencyToRequest.isComper == true)
                        {
                            CheckList.Add(request);
                        }
                        else
                        {
                            NotCheckList.Add(request);
                        }
                        break;

                    }
                }
            }

        }
        enum Frequency
        {
            WEEKLY = 1, DAYLY = 2, MONTHLY = 3
        }

    }
}
