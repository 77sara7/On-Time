using DAL;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using iMacros;
using System.Net.Mail;
using System.IO;
using dll;
using System.Net.Mime;
using System.Threading;

namespace BL
{
    public static class RequestBL
    {
       static EventLog Log;
        // get all specific user requests
        public static List<RequestDto> GetAllRequestByUserId(int userId)
        {
            List<RequestDto> requestsDto = RequestDAL.GetAllRequestByUserId(userId);
            return requestsDto;
        }

        // add new request
        public static RequestDto AddNewRequest(RequestDto requestDto)
        {

            try
            {
                // converts the URL to a recording format
                requestDto.content = "VERSION BUILD = 1005 RECORDER = CR\nURL GOTO =  " + requestDto.content + " \nSAVEAS TYPE = HTML FOLDER =D:\\Files FILE =newHtml";
                requestDto.recording_stream = Encoding.ASCII.GetBytes(requestDto.content);
                Status status = Run(requestDto.content, "");
                // Robotic surfing for keeping the initial state of the page
                if (status == Status.sOk)
                {
                    // Read the page content
                    string newPlay = File.ReadAllText("D:\\Files\\newHtml.htm");
                    // fill in the data
                    requestDto.file_stream = Encoding.ASCII.GetBytes(newPlay);
                    requestDto.file_id = RequestDAL.AttachedFile(requestDto.file_stream, ".html");
                    // add to database
                    RequestDto requestsDto = RequestDAL.AddNewRequest(requestDto);
                    return requestsDto;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        // adding a recording - is called from the extension without filling request details yet
        public static int AddNewRecording(RequestDto requestDto)
        {
            try
            {
                //Chaining to Record A command that will save the page as HTML
                requestDto.content += " \nSAVEAS TYPE = HTML FOLDER =D:\\Files FILE =newHtml";
                requestDto.recording_stream = Encoding.ASCII.GetBytes(requestDto.content);
                // add to database
                return RequestDAL.AddNewRecording(requestDto);
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
            
        }

        // update the request's timing details
        public static RequestDto updateDetailsOfTimingRequest(RequestDto requestDto)
        {
            return RequestDAL.updateDetailsOfTimingRequest(requestDto);
        }

        // update request details - Recording was done through the extension
        public static RequestDto UpdateRequestFromTheExtension(RequestDto requestDto)
        {
            try
            {
                if (requestDto.recording_id == new Guid())
                {
                    RequestDto requestsDto = RequestDAL.FillInDataRequest(requestDto);
                    Status status = Run(Encoding.UTF8.GetString(requestsDto.recording_stream), "");
                    if (status == Status.sOk)
                    {
                        string newPlay = File.ReadAllText("D:\\Files\\newHtml.htm");
                        RequestDAL.UpdateHtmlFile(requestsDto, newPlay);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return requestDto;
        }

        public static RequestDto DeleteRequest(RequestDto requestDto)
        {
            return RequestDAL.DeleteRequest(requestDto);
        }
        public static bool Compere(string newPlay, string file_stream,EventLog eventLog)
        {
            try
            {
                eventLog.WriteEntry("in BL:Compere ");
                Algoritem m = new Algoritem();
                bool ret = m.MyComparer(file_stream, newPlay);
                eventLog.WriteEntry("ret: "+ret);
                return ret;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public static string Play(byte[] recording,string requestId,EventLog eventLog)
        {
            try
            {
               eventLog.WriteEntry("in Play");
                string newPlay;
                recording = Encoding.ASCII.GetBytes(Encoding.UTF8.GetString(recording).Replace("newHtml", "newHtml" + requestId));
                //TODOהפונקציה מקבלת הקלטה שהתוסף הוציא מומרת למערך של בתים 
                //צריך גלישה רובוטית 
                // הפונקציה מחזירה את הגלישה החדשה לצורך השוואה
                Status status = Run(Encoding.UTF8.GetString(recording), requestId);
                eventLog.WriteEntry("status: " + status);
                if (status == Status.sOk)
                {
                    newPlay = File.ReadAllText("D:\\Files\\newHtml" + requestId + ".htm");
                    return newPlay;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        public static void UpdatHtmlFile(RequestDto request, string newPlay, EventLog eventLog)
        {
            eventLog.WriteEntry("BL:In UpdatHtmlFile.");
            RequestDAL.UpdateHtmlFile(request, newPlay);
        }

        public static void SendMail(string userMail, string html, string title,EventLog eventLog)
        {
            try
            {
                eventLog.WriteEntry("BL:In SendMail.");
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("ontimemonitor@gmail.com", "onTime123");
                MailMessage mm = new MailMessage("ontimemonitor@gmail.com", userMail, "Your request " + title + " has been updated", html);
                mm.IsBodyHtml = true;
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.AlternateViews.Add(getEmbeddedImage("D:/Files/logo.png"));

                File.WriteAllText("D:/Files/" + title + ".html", html);

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment("D:/Files/" + title + ".html");
                mm.Attachments.Add(attachment);

                client.Send(mm);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
        private static AlternateView getEmbeddedImage(String filePath)
        {
            try
            {
                LinkedResource inline = new LinkedResource(filePath);
                inline.ContentId = Guid.NewGuid().ToString();
                string htmlBody = @"<img src='cid:" + inline.ContentId + @"'/>";
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
                alternateView.LinkedResources.Add(inline);
                return alternateView;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<RequestDto> GetAllRelevantRequests(EventLog eventLog)
        {
            try
            {
                eventLog.WriteEntry("BL:In GetAllRelevantRequests.");
                List<RequestDto> requestDtos = RequestDAL.GetAllRelevantRequests(eventLog);
                return requestDtos;
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry(ex.ToString());
                return null;
            }

        }
        public static void OnInitMapper(EventLog eventLog)
        {
            Log = eventLog;
            Log.WriteEntry("OnInitMapper");
            RequestDAL.OnInitMapper();
        }
        static public Status Run(string content,string fileId)
        {
            try
            {
                File.WriteAllText("D:/Files/mac" + fileId + ".iim", content);
                int timeout = 60, errors = 0;
                iMacros.Status status;
                var app = new iMacros.App();
                status = app.iimInit("-V7", true, "", "", "", timeout);
                if (status != iMacros.Status.sOk) errors++;
                string macro = "D:/Files/mac" + fileId + ".iim";
                status = app.iimDisplay("Interface version =\n" + app.iimGetInterfaceVersion().ToString(), timeout);
                if (status != Status.sOk) return status;
                status = app.iimPlay(macro, timeout);
                if (status != Status.sOk) return status;
                status = app.iimExit(timeout);
                if (status != Status.sOk) return status;
                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }
     
        public static void ThreadFunction(object request)
        {
            try
            {
                Log.WriteEntry("ThreadFunction");
                RequestDto requestDto = (RequestDto)request;
                requestDto.eventLog.WriteEntry("in BL: ThreadFunction");
                requestDto.eventLog.WriteEntry("request_id: " + requestDto.request_id.ToString());
                bool isDiff = false;
                string newPlay = Play(requestDto.recording_stream, requestDto.request_id.ToString(), requestDto.eventLog);

                if (newPlay != null)
                {
                    isDiff = Compere(newPlay, Encoding.UTF8.GetString(requestDto.file_stream), requestDto.eventLog);
                }
                requestDto.eventLog.WriteEntry("isDiff: " + isDiff);
                if (isDiff)
                {
                    SendMail(requestDto.userMail, newPlay, requestDto.request_name, requestDto.eventLog);
                    UpdatHtmlFile(requestDto, newPlay, requestDto.eventLog);
                }
            }
            catch (Exception ex)
            {
                Log.WriteEntry(ex.ToString());
               // throw;
            }
        }
     
        //static void GetAllRelevantRequests2()
        //{
        //    string isSame;
        //    List<RequestDto> requestDtos = RequestDAL.GetAllRelevantRequests2();
        //    foreach (var request in requestDtos)
        //    {
        //        isSame = "";

        //        string newPlay = RequestBL.Play(request.recording_stream, request.request_id.ToString());

        //        if (newPlay != null)
        //        {
        //            isSame = RequestBL.Compere(newPlay, Encoding.UTF8.GetString(request.file_stream));
        //        }

        //        if (isSame != "")
        //        {
        //            RequestBL.SendMail(request.userMail, isSame, request.request_name);
        //            RequestBL.UpdatHtmlFile(request, newPlay);
        //        }
        //    }

        //}
        //static void GetAllRelevantRequests3()
        //{
        //    List<RequestDto> requestDtos = RequestDAL.GetAllRelevantRequests2();
        //    foreach (var request in requestDtos)
        //    {
        //        Thread thread = new Thread(ThreadFunction);
        //        thread.Start(request);

        //    }

        //}


    }

}