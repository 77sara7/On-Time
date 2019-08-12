using Entities.DTO;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.Entity.Migrations;
using AutoMapper;
using System.Data.Entity.Core.Objects;
using System.Net.Mail;
using System.Diagnostics;

namespace DAL
{
    public static partial class RequestDAL
    {
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
                    return new List<RequestDto>
                    {
                        new RequestDto
                        {
                        IsAuthorized = false,
                        ErrorMessage = "שגיאה בהתחברות לשרת"
                        }

                    };
                }
            }
        }

        public static void OnInitMapper()
        {
            MapperClass.OnInit();
        }


        public static List<RequestDto> GetAllRelevantRequests(EventLog eventLog)
        {
            eventLog.WriteEntry("DL:In GetAllRelevantRequests.");
            using (var db = new DBContext())
            {
                try
                {
                    DateTime dateTime = DateTime.Now;
                    var relevantRequests = (from Request request in db.Requests
                                            join v_attached_file f in db.v_attached_file on request.file_id equals f.stream_id
                                            join User user in db.Users on request.user_id equals user.user_id
                                            where request.is_relevant && (request.date_from.CompareTo(dateTime.Date) <= 0 && request.date_to.CompareTo(dateTime.Date) >= 0 &&
                                                  (request.frequency_id == 1 ||
                                                  (request.frequency_id == 2 && (dateTime.Date.Minute == 0 || dateTime.Date.Minute == 30)) ||
                                                  (request.frequency_id == 3 && dateTime.Date.Minute == 0) ||
                                                  (request.frequency_id == 4 && request.hour == dateTime.Date.Hour) ||
                                                  (request.frequency_id == 5 && request.day == (int)dateTime.Date.DayOfWeek + 1 && request.hour == dateTime.Date.Hour) ||
                                                  (request.frequency_id == 6 && request.day_in_month == dateTime.Date.Day && request.day == (int)dateTime.Date.DayOfWeek + 1 && request.hour == dateTime.Date.Hour)))
                                            select new { f.file_stream, request, user.mail }).ToList();

                    List<RequestDto> relevantRequestsDto = new List<RequestDto>();
                    for (int i = 0; i < relevantRequests.Count; i++)
                    {
                        relevantRequestsDto.Add(Mapper.Map<Request, RequestDto>(relevantRequests[i].request));
                        relevantRequestsDto[i].file_stream = relevantRequests[i].file_stream;
                        relevantRequestsDto[i].userMail = relevantRequests[i].mail;
                    }
                    return relevantRequestsDto;
                }
                catch (Exception ex)
                {
                    eventLog.WriteEntry("in catch.");
                    eventLog.WriteEntry(ex.ToString());
                    return new List<RequestDto>
                    {
                        new RequestDto
                        {
                        IsAuthorized = false,
                        ErrorMessage = "שגיאה בהתחברות לשרת"
                        }

                    };
                }
            }
        }
        //public static RequestDto UpdateRequest(RequestDto requestDto)
        //{
        //    using (var db = new DBContext())
        //    {
        //        try
        //        {
        //            Request request = Mapper.Map<RequestDto, Request>(requestDto);
        //            db.Requests.AddOrUpdate(request);
        //            //db.AcceptChanges();
        //            db.SaveChanges();
        //            return requestDto;
        //        }
        //        catch (Exception ex)
        //        {
        //            return new RequestDto
        //            {
        //                IsAuthorized = false,
        //                ErrorMessage = "שגיאה בהתחברות לשרת"
        //            };
        //        };
        //    }
        //}
        public static List<RequestDto> UpdateRequest(RequestDto requestDto)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Request request = Mapper.Map<RequestDto, Request>(requestDto);
                    db.Requests.AddOrUpdate(request);
                    //db.AcceptChanges();
                    db.SaveChanges();
                    List<RequestDto> userRequestsDto = new List<RequestDto>();
                    foreach (var r in db.Requests.ToList())
                    {
                        userRequestsDto.Add(Mapper.Map<Request, RequestDto>(r));
                    }


                    return userRequestsDto;
                }
                catch (Exception ex)
                {
                    return new List<RequestDto>
                    {
                        new RequestDto
                        {
                        IsAuthorized = false,
                        ErrorMessage = "שגיאה בהתחברות לשרת"
                        }

                    };
                };
            }
        }
        public static List<RequestDto> DeleteRequest(RequestDto requestDto)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Request request = Mapper.Map<RequestDto, Request>(requestDto);
                    db.Requests.AddOrUpdate(request);
                    db.SaveChanges();
                    List<RequestDto> userRequestsDto = new List<RequestDto>();
                    foreach (var r in db.Requests.Where(r=>r.is_relevant==true).ToList())
                    {
                        userRequestsDto.Add(Mapper.Map<Request, RequestDto>(r));
                    }


                    return userRequestsDto;
                }
                catch (Exception ex)
                {
                    return new List<RequestDto>
                    {
                        new RequestDto
                        {
                        IsAuthorized = false,
                        ErrorMessage = "שגיאה בהתחברות לשרת"
                        }

                    };
                };
            }
        }
        //public static RequestDto DeleteRequest(RequestDto requestDto)
        //{
        //    using (var db = new DBContext())
        //    {
        //        try
        //        {
        //            Request request = Mapper.Map<RequestDto, Request>(requestDto);
        //            db.Requests.AddOrUpdate(request);
        //            db.SaveChanges();
        //            return requestDto;
        //        }
        //        catch (Exception ex)
        //        {
        //            return new RequestDto
        //            {
        //                IsAuthorized = false,
        //                ErrorMessage = "שגיאה בהתחברות לשרת"
        //            };
        //        };
        //    }
        //}
        public static RequestDto AddNewRequest(RequestDto requestDto)
        {
            //GetAllRelevantRequests();
            using (var db = new DBContext())
            {
                try
                {
                    ObjectParameter file_id = new ObjectParameter("file_id", typeof(Guid));
                    db.attached_file_add(String.Format("{0:dMyyyyHHmmssF}", DateTime.Now) + ".pdf", requestDto.file_stream, file_id);
                    requestDto.file_id = (Guid)file_id.Value;
                    db.attached_file_add(String.Format("{0:dMyyyyHHmmssR}", DateTime.Now) + ".pdf", requestDto.recording_stream, file_id);
                    requestDto.recording_id = (Guid)file_id.Value;
                    var request = Mapper.Map<RequestDto, Request>(requestDto);
                    //למחיקה
                    //request.recording_id = null;
                    db.Requests.Add(request);
                    db.SaveChanges();
                    return requestDto;
                }
                catch (Exception ex)
                {
                    return new RequestDto
                    {
                        IsAuthorized = false,
                        ErrorMessage = "שגיאה בהתחברות לשרת"
                    };
                };
            }
        }

        //public Guid AddAttachedFileToDB(byte[] file, string fileName)
        //מציבי
        //{

        //    v_attached_file newFile = new v_attached_file();
        //    try
        //    {
        //        newFile.stream_id = Guid.NewGuid();
        //        newFile.file_stream = file;
        //        newFile.name = fileName;

        //        try
        //        {
        //            dbService.Entities.v_attached_file.Add(newFile);
        //            dbService.Entities.SaveChanges();
        //        }
        //        catch (DbUpdateException)
        //        {
        //            string fName = Path.GetFileNameWithoutExtension(fileName);
        //            string exten = Path.GetExtension(fileName);
        //            //newFile.name = string.Format("{0}_1{1}", fName, exten);
        //            var timeDiff = DateTime.Now - new DateTime(2000, 1, 1);
        //            double totaltime = timeDiff.TotalMilliseconds;
        //            newFile.name = fName + "_" + totaltime + exten;
        //            dbService.Entities.v_attached_file.Add(newFile);
        //            dbService.Entities.SaveChanges();
        //        }
        //        return newFile.stream_id;
        //    }

        //    catch (Exception ex)
        //    {
        //        string s = ex.ToString();
        //        SessionHelp sessionHelp = new SessionHelp();
        //        sessionHelp.AddSessionMessage("בעיה בקליטת הקובץ ");
        //        return new Guid();
        //    }

        //}
    }
}



