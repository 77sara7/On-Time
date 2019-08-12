using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.DTO;
using BL;

namespace WebAPI.Controllers
{
    public class RequestController : ApiController
    {
        // GET: api/Request
        [HttpGet]
        [Route("api/Request/GetAllRequestByUserId")]
        public IEnumerable<RequestDto> GetAllRequestByUserId(int user_id)
        {
            return RequestBL.GetAllRequestByUserId(user_id);
        }

        //// GET: api/Request/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Request
        [HttpPost]                        
        [Route("api/Request/AddNewRequesr")]
        public RequestDto Post(RequestDto requestDto)
        {
            return RequestBL.AddNewRequest(requestDto);
        }

        // PUT: api/Request/5
        [HttpPut]
        [Route("api/Request/UpdateRequest")]
        public List<RequestDto> Put(RequestDto requestDto)
        {
            return RequestBL.UpdateRequest(requestDto);
        }
        //public RequestDto Put(RequestDto requestDto)
        //{
        //    return RequestBL.UpdateRequest(requestDto);
        //}
        // DELETE: api/Request/5
        [HttpPut]
        [Route("api/Request/DeleteRequest")]
        public List<RequestDto> Delete(RequestDto requestDto)
        {
            return RequestBL.DeleteRequest(requestDto);
        }
        //public RequestDto Delete(RequestDto requestDto)
        //{
        //    return RequestBL.DeleteRequest(requestDto);
        //}
    }
}
