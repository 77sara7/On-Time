using Entities.DTO;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Data.Entity.Migrations;
using AutoMapper;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;

namespace DAL
{
    public static partial class RequestDAL
    {
        public static RequestDto UpdateHtmlFile(RequestDto requestDto, string html)
        {
            using (var db = new DBContext())
            {
                try
                {
                    requestDto.file_stream = Encoding.ASCII.GetBytes(html);
                    requestDto.file_id = AttachedFile(requestDto.file_stream, ".html");
                    var request = Mapper.Map<RequestDto, Request>(requestDto);
                    db.Requests.AddOrUpdate(request);
                    db.SaveChanges();
                    return requestDto;
                }
                catch (Exception ex)
                {
                    return null;
                };
            }
        }

        public static List<RequestDto> GetAllRequestByUserId(int userId)
        {
            using (var db = new DBContext())
            {
                try
                {
                    List<Request> userRequests = db.Requests.Where(request => request.is_relevant && request.user_id.Equals(userId)).ToList();
                    List<RequestDto> userRequestsDto = new List<RequestDto>();
                    for (int i = 0; i < userRequests.Count; i++)
                    {
                        userRequestsDto.Add(Mapper.Map<Request, RequestDto>(userRequests[i]));
                    }

                    return userRequestsDto;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static void OnInitMapper()
        {
            MapperClass.OnInit();
        }


        public static List<RequestDto> GetAllRelevantRequests(EventLog eventLog)
        {//צריך לבדוק את הפונקציה שוב
             eventLog.WriteEntry("DL:In GetAllRelevantRequests.");
            using (var db = new DBContext())
            {
                try
                {
                    DateTime dateTime = DateTime.Now;
                    var relevantRequests = (from Request request in db.Requests
                                            join v_attached_file f in db.v_attached_file on request.file_id equals f.stream_id
                                            join v_attached_file r in db.v_attached_file on request.recording_id equals r.stream_id
                                            join User user in db.Users on request.user_id equals user.user_id
                                            where request.is_relevant && (request.date_from.Value.CompareTo(dateTime.Date) <= 0 && request.date_to.Value.CompareTo(dateTime.Date) >= 0 &&
                                                  (request.frequency_id == (int)FrequencyEnum.TEN_MINUTE ||
                                                  (request.frequency_id == (int)FrequencyEnum.HALF_AN_HOUR && (dateTime.Date.Minute == 0 || dateTime.Date.Minute == 30)) ||
                                                  (request.frequency_id == (int)FrequencyEnum.HOUR && dateTime.Date.Minute == 0) ||
                                                  (request.frequency_id == (int)FrequencyEnum.ONCE_A_DAY && request.hour == dateTime.Date.Hour && dateTime.Date.Minute == 0) ||
                                                  (request.frequency_id == (int)FrequencyEnum.ONCE_A_WEEK && request.day == (int)dateTime.Date.DayOfWeek + 1 && request.hour == dateTime.Date.Hour && dateTime.Date.Minute == 0)))
                                            select new { record = r.file_stream, html = f.file_stream, request, user.mail }).ToList();

                    List<RequestDto> relevantRequestsDto = new List<RequestDto>();
                    for (int i = 0; i < relevantRequests.Count; i++)
                    {
                        relevantRequestsDto.Add(Mapper.Map<Request, RequestDto>(relevantRequests[i].request));
                        relevantRequestsDto[i].file_stream = relevantRequests[i].html;
                        relevantRequestsDto[i].recording_stream = relevantRequests[i].record;
                        relevantRequestsDto[i].recording_stream = Encoding.ASCII.GetBytes(Encrypt_Decrypt.DecryptStringAES(Encoding.UTF8.GetString(relevantRequestsDto[i].recording_stream), "onTime123"));
                        relevantRequestsDto[i].userMail = relevantRequests[i].mail;
                    }
                    return relevantRequestsDto;
                }
                catch (Exception ex)
                {   eventLog.WriteEntry("in catch.");
                     eventLog.WriteEntry(ex.ToString());
                    return null;
                }
            }
        }
        //public static List<RequestDto> GetAllRelevantRequests2()
        //{//צריך לבדוק את הפונקציה שוב
        //    // eventLog.WriteEntry("DL:In GetAllRelevantRequests.");
        //    using (var db = new DBContext())
        //    {
        //        try
        //        {
        //            DateTime dateTime = DateTime.Now;
        //            var relevantRequests = (from Request request in db.Requests
        //                                    join v_attached_file f in db.v_attached_file on request.file_id equals f.stream_id
        //                                    join v_attached_file r in db.v_attached_file on request.recording_id equals r.stream_id
        //                                    join User user in db.Users on request.user_id equals user.user_id
        //                                    where request.is_relevant && (request.date_from.Value.CompareTo(dateTime.Date) <= 0 && request.date_to.Value.CompareTo(dateTime.Date) >= 0 &&
        //                                          (request.frequency_id == 0 ||
        //                                          (request.frequency_id == 1 && (dateTime.Date.Minute == 0 || dateTime.Date.Minute == 30)) ||
        //                                          (request.frequency_id == 2 && dateTime.Date.Minute == 0) ||
        //                                          (request.frequency_id == 3 && request.hour == dateTime.Date.Hour && dateTime.Date.Minute == 0) ||
        //                                          (request.frequency_id == 4 && request.day == (int)dateTime.Date.DayOfWeek + 1 && request.hour == dateTime.Date.Hour && dateTime.Date.Minute == 0)))
        //                                    select new { record = r.file_stream, html = f.file_stream, request, user.mail }).ToList();

        //            List<RequestDto> relevantRequestsDto = new List<RequestDto>();
        //            for (int i = 0; i < 1; i++)
        //            {
        //                relevantRequestsDto.Add(Mapper.Map<Request, RequestDto>(relevantRequests[i].request));
        //                relevantRequestsDto[i].file_stream = relevantRequests[i].html;
        //                relevantRequestsDto[i].recording_stream = relevantRequests[i].record;
        //                relevantRequestsDto[i].recording_stream = Encoding.ASCII.GetBytes(Encrypt_Decrypt.DecryptStringAES(Encoding.UTF8.GetString(relevantRequestsDto[i].recording_stream), "onTime123"));
        //                relevantRequestsDto[i].userMail = relevantRequests[i].mail;
        //            }
        //            return relevantRequestsDto;
        //        }
        //        catch (Exception ex)
        //        {

        //            return null;

        //        }
        //    }
        //}
        public static RequestDto updateDetailsOfTimingRequest(RequestDto requestDto)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Request request = Mapper.Map<RequestDto, Request>(requestDto);
                    db.Requests.AddOrUpdate(request);
                    db.SaveChanges();
                    return requestDto;
                }
                catch (Exception ex)
                {
                    return null;
                };
            }
        }

        public static Guid AttachedFile(byte[] stream, string end)
        {
            using (var db = new DBContext())
            {
                ObjectParameter file_id = new ObjectParameter("file_id", typeof(Guid));
                try
                {
                    db.attached_file_add(String.Format("{0:dMyyyyHHmmss}", DateTime.Now) + end, stream, file_id);
                    return (Guid)file_id.Value;
                }
                catch
                {
                    return new Guid();
                }
            }
        }
        public static byte[] StreamFromFile(Guid fileId)
        {
            using (var db = new DBContext())
            {
                try
                {
                    v_attached_file vFile = db.v_attached_file.Where(v => v.stream_id == fileId).FirstOrDefault();
                    return Encoding.ASCII.GetBytes(Encrypt_Decrypt.DecryptStringAES(Encoding.UTF8.GetString(vFile.file_stream), "onTime123"));
                }
                catch (Exception ex)
                {
                    return null;

                }
            }
        }
        public static RequestDto FillInDataRequest(RequestDto requestDto)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Request requestFromDB = db.Requests.Find(requestDto.request_id);
                    requestDto.recording_id = requestFromDB.recording_id;
                    requestDto.recording_stream= StreamFromFile(requestFromDB.recording_id);
                    return requestDto;
                }
                catch (Exception ex)
                {
                    return null;

                }
            }
        }
        public static RequestDto DeleteRequest(RequestDto requestDto)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Request request = Mapper.Map<RequestDto, Request>(requestDto);
                    db.Requests.AddOrUpdate(request);
                    db.SaveChanges();
                    return requestDto;
                }
                catch (Exception ex)
                {
                    return null;
                };
            }
        }
        public static RequestDto AddNewRequest(RequestDto requestDto)
        {
            using (var db = new DBContext())
            {
                try
                {//הצפנה
                    string a = Encoding.UTF8.GetString(requestDto.recording_stream);
                    string b = Encrypt_Decrypt.EncryptStringAES(Encoding.UTF8.GetString(requestDto.recording_stream), "onTime123");
                    requestDto.recording_stream = Encoding.ASCII.GetBytes(Encrypt_Decrypt.EncryptStringAES(Encoding.UTF8.GetString(requestDto.recording_stream), "onTime123"));
                   // requestDto.recording_stream = Encoding.ASCII.GetBytes(Encrypt_Decrypt.DecryptStringAES(Encoding.UTF8.GetString(requestDto.recording_stream), "onTime123"));
                    requestDto.file_id= AttachedFile(requestDto.file_stream, ".html");
                    requestDto.recording_id = AttachedFile(requestDto.recording_stream, ".iim");
                    var request = Mapper.Map<RequestDto, Request>(requestDto);
                    //למחיקה
                    //request.recording_id = null;
                    db.Requests.Add(request);
                    db.SaveChanges();
                    return requestDto;
                }
                catch (Exception ex)
                {
                    return null;
                };
            }
        }


        public static int AddNewRecording(RequestDto requestDto)
        {
            using (var db = new DBContext())
            {
                try
                {
                    requestDto.recording_stream = Encoding.ASCII.GetBytes(Encrypt_Decrypt.EncryptStringAES(Encoding.UTF8.GetString(requestDto.recording_stream), "onTime123"));
                    requestDto.recording_id = AttachedFile(requestDto.recording_stream, ".iim");
                    requestDto.frequency_id = 1;

                    var request = Mapper.Map<RequestDto, Request>(requestDto);
                    //למחיקה
                    request.file_id = null;
                    var req = db.Requests.Add(request);
                    db.SaveChanges();
                    return req.request_id;
                }
                catch (Exception ex)
                {
                    return -1;
                };
            }
        }

    }
}



