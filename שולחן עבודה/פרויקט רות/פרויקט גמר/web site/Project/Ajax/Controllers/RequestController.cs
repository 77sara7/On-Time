using Ajax.Models;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ajax.Controllers
{
    enum EFrequency
    {
        WEEK = 1, DAY = 2, MONTH = 3
    }
    public class RequestController : Controller
    {
        TasksEntities db = new TasksEntities();

        public ActionResult RequestList()
        {
            if (Session["user"] != null)
            {
                return View(db.Request.ToList().FindAll(p => p.userId.Equals(Session["user"])));
            }
            else
            {
                @ViewBag.message = 2;
                return RedirectToAction("../Login/Login");
            }
        }

        [HttpPost]
        public ActionResult EditRequest(Request r)
        {
            Request request = db.Request.First(t => t.requestId == r.requestId);
            int week = 0, month = 0, day = 0;
            foreach (var item in request.FrequencyToRequest.ToList())
            {
                switch (item.Frequency.frequencyId)
                {
                    case 1:
                        week = item.FrequencyToRequestId;
                        break;
                    case 2:
                        day = item.FrequencyToRequestId;
                        break;
                    case 3:
                        month = item.FrequencyToRequestId;
                        break;
                    default:
                        break;
                }
                ViewBag.week = week;
                ViewBag.day = day;
                ViewBag.month = month;
            }
            Session["request"] = r.requestId;
            return PartialView("EditRequest", request);

        }
        [HttpGet]
        public ActionResult EditRequest(object r)
        {
            Request bead = null;
            return PartialView("EditRequest", bead);
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult CreateRequest()
        {
            return View("CreateRequest");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateRequest(RequestContent rc)
        {
            Request r = new Request();
            r.name = rc.name;
            r.userId = (String)Session["user"];
            r.date = DateTime.Now;
            db.Request.Add(r);
            setCont(r, rc.content);
            setPath(r, rc.path);
            //מימוש גלישה רובוטית
            db.SaveChanges();
            return RedirectToAction("RequestList");
        }
        [HttpPost]
        public ActionResult ViewFrequency(Frequency frequency)
        {
            return PartialView("ViewFrequency", frequency);
        }
        public ActionResult DeleteDay(FrequencyDays frequency)
        {
            FrequencyDays item = db.FrequencyDays.FirstOrDefault(f => f.frequencyDaysId.Equals(frequency.frequencyDaysId));
            db.FrequencyDays.Remove(item);
            db.SaveChanges();
            return RedirectToAction("RequestList");
        }
        [HttpPost]
        public ActionResult Weekly(int frequencyToRequestId)
        {
            FrequencyToRequest item = db.FrequencyToRequest.First(t => t.FrequencyToRequestId == frequencyToRequestId);
            return PartialView("Weekly", item.FrequencyDays.ToList());
        }
        [HttpPost]

        public ActionResult WeeklyPartial()
        {
            FrequencyToRequest item = null;
            return PartialView("WeeklyPartial", item.FrequencyDays.ToList());
        }
        [HttpGet]
        public ActionResult CreateWeekly(int dayy)
        {

            Request request = db.Request.ToList().FirstOrDefault(a => a.requestId.Equals(Session["request"]));
            FrequencyToRequest frequencyToRequest = request.FrequencyToRequest.FirstOrDefault(f => f.frequencyId.Equals((int)EFrequency.WEEK));
            if (frequencyToRequest == null)
            {
                frequencyToRequest = new FrequencyToRequest();
                Frequency frequency = db.Frequency.ToList().FirstOrDefault(f => f.frequencyId.Equals(EFrequency.WEEK));
                frequencyToRequest.Request = request;
                frequencyToRequest.requestId = request.requestId;
                frequencyToRequest.isComper = true;
                frequencyToRequest.frequencyId = (int)EFrequency.WEEK;
                frequencyToRequest.FrequencyDays = new List<FrequencyDays>();
                frequencyToRequest.Frequency = frequency;
                db.FrequencyToRequest.Add(frequencyToRequest);
                db.SaveChanges();
            }
            if (frequencyToRequest.FrequencyDays.ToList().FirstOrDefault(f => ((int)f.day).Equals(dayy)) == null)
            {
                FrequencyDays frequencyDays = new FrequencyDays();
                frequencyDays.FrequencyToRequest = frequencyToRequest;
                frequencyDays.day = dayy;
                frequencyDays.FrequencyToRequestId = frequencyToRequest.FrequencyToRequestId;
                frequencyToRequest.FrequencyDays.Add(frequencyDays);
                db.SaveChanges();
            }
            return PartialView("WeeklyPartial", frequencyToRequest.FrequencyDays.ToList());
        }

        [HttpGet]
        public ActionResult CreateMonthly(int day)
        {
            Request request = db.Request.ToList().FirstOrDefault(a => a.requestId.Equals(Session["request"]));
            FrequencyToRequest frequencyToRequest = request.FrequencyToRequest.FirstOrDefault(f => f.frequencyId.Equals((int)EFrequency.MONTH));
            if (frequencyToRequest == null)
            {
                frequencyToRequest = new FrequencyToRequest();
                Frequency frequency = db.Frequency.ToList().FirstOrDefault(f => f.frequencyId.Equals(EFrequency.MONTH));
                frequencyToRequest.Request = request;
                frequencyToRequest.requestId = request.requestId;
                frequencyToRequest.isComper = true;
                frequencyToRequest.frequencyId = (int)EFrequency.MONTH;
                frequencyToRequest.FrequencyDays = new List<FrequencyDays>();
                frequencyToRequest.Frequency = frequency;
                db.FrequencyToRequest.Add(frequencyToRequest);
                db.SaveChanges();
            }
            if (frequencyToRequest.FrequencyDays.ToList().FirstOrDefault(f => ((int)f.day).Equals(day)) == null)
            {
                FrequencyDays frequencyDays = new FrequencyDays();
                frequencyDays.FrequencyToRequest = frequencyToRequest;
                frequencyDays.day = day;
                frequencyDays.FrequencyToRequestId = frequencyToRequest.FrequencyToRequestId;
                frequencyToRequest.FrequencyDays.Add(frequencyDays);
                db.SaveChanges();
            }

            return PartialView("WeeklyPartial", frequencyToRequest.FrequencyDays.ToList());
        }
        public ActionResult CreateDayly(int? c)
        {
            Request r = db.Request.ToList().FirstOrDefault(a => a.requestId.Equals(Session["request"]));
            FrequencyToRequest df = new FrequencyToRequest();
            df.frequencyId = 2;
            df.requestId = r.requestId;
            db.FrequencyToRequest.Add(df);
            db.SaveChanges();
            return PartialView("MonthlyPartial", df.FrequencyDays.ToList());
        }
        public ActionResult DelDayly(int? c)
        {
           FrequencyToRequest df = db.FrequencyToRequest.ToList().FirstOrDefault(a => a.requestId.Equals(Session["request"])&&a.frequencyId==2);
            db.FrequencyToRequest.Remove(df);
            db.SaveChanges();
            return PartialView("MonthlyPartial", df.FrequencyDays.ToList());
        }
        public ActionResult Delete(Request r)
        {
            Request del = db.Request.FirstOrDefault(item => item.requestId.Equals(r.requestId));

            for (int i = 0; i < del.Path.Count; i++)
            {
                db.Path.ToList().RemoveAt(i);
            }

            for (int i = 0; i < del.Contents.Count; i++)
            {
                db.Contents.ToList().RemoveAt(i);
            }
            for (int i = 0; i < del.FrequencyToRequest.Count; i++)
            {
                for (int j = 0; j < del.FrequencyToRequest.ToList().ElementAt(i).FrequencyDays.Count; j++)
                {
                    db.FrequencyDays.ToList().RemoveAt(j);
                }
                db.FrequencyToRequest.ToList().RemoveAt(i);
            }
            db.Request.Remove(del);
            db.SaveChanges();
            return RedirectToAction("RequestList");
        }
        public void setPath(Request r, string str)
        {
            List<Models.Path> list = r.Path.ToList();


            int ind = 1; string tmp; Models.Path path;
            while (str.Length > 4000)
            {
                tmp = str.Substring(0, 4000);
                path = new Models.Path();
                path.requestId = r.requestId;
                path.Request = r;
                path.ordinalNum = ind++;
                path.path1 = tmp;
                r.Path.Add(path);


                str = str.Substring(4000, str.Length - 4000);
            }
            if (str.Length > 0)
            {
                path = new Models.Path();
                path.requestId = r.requestId;
                path.Request = r;
                path.ordinalNum = ind++;
                path.path1 = str;
                r.Path.Add(path);
            }

        }
        public void setCont(Request r, string str)
        {
            List<Contents> list = r.Contents.ToList();


            int ind = 1; string tmp; Contents contents;
            while (str.Length > 4000)
            {
                tmp = str.Substring(0, 4000);
                contents = new Contents();
                contents.requestId = r.requestId;
                contents.Request = r;
                contents.ordinalNum = ind++;
                contents.contens = tmp;
                r.Contents.Add(contents);
                str = str.Substring(4000, str.Length - 4000);
            }
            if (str.Length > 0)
            {
                contents = new Contents();
                contents.requestId = r.requestId;
                contents.Request = r;
                contents.ordinalNum = ind++;
                contents.contens = str;
                r.Contents.Add(contents);
            }
        }
        [HttpGet]
        public ActionResult DownLoud()
        {
            return View("DownLoud");
        }

        //[HttpGet]
        //public ActionResult DelMe(string num)
        //{
        //    return View("DownLoud");
        //}
    }
}
