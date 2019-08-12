using DAL;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class RequestBL
    {
        public static List<RequestDto> GetAllRequestByUserId(int userId)
        {
            List<RequestDto> requestsDto = RequestDAL.GetAllRequestByUserId(userId);
            return requestsDto;
        }
        public static RequestDto AddNewRequest(RequestDto requestDto)
        {
            RequestDto requestsDto = RequestDAL.AddNewRequest(requestDto);
            return requestsDto;
        }
        public static List<RequestDto> UpdateRequest(RequestDto requestDto)
        {
            List<RequestDto> requestsDto = RequestDAL.UpdateRequest(requestDto);
            return requestsDto;
        }
        //public static RequestDto UpdateRequest(RequestDto requestDto)
        //{
        //    RequestDto requestsDto = RequestDAL.UpdateRequest(requestDto);
        //    return requestsDto;
        //}
        //public static RequestDto DeleteRequest(RequestDto requestDto)
        //{
        //    return RequestDAL.DeleteRequest(requestDto);
        //}
        public static List<RequestDto> DeleteRequest(RequestDto requestDto)
        {
           return RequestDAL.DeleteRequest(requestDto);       
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
        public static void OnInitMapper()
        {
            RequestDAL.OnInitMapper();
        }
    }
}