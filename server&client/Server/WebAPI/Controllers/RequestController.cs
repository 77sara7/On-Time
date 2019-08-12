using System.Collections.Generic;
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

        // POST: api/Request
        [HttpPost]
        [Route("api/Request/AddNewRequesr")]
        public RequestDto Post(RequestDto requestDto)
        {
            return RequestBL.AddNewRequest(requestDto);
        }
        [HttpPost]
        [Route("api/Request/AddNewRecorde")]
        public int recorde(RequestDto requestDto)
        {
            return RequestBL.AddNewRecording(requestDto);
        }

        // PUT: api/Request/5
        [HttpPut]
        [Route("api/Request/UpdateRequest")]
        public RequestDto Put(RequestDto requestDto)
        {
            return RequestBL.UpdateRequestFromTheExtension(requestDto);
        }
        [HttpPut]
        [Route("api/Request/updateDetailsOfTimingRequest")]
        public RequestDto updateDetailsOfTimingRequest(RequestDto requestDto)
        {
            return RequestBL.updateDetailsOfTimingRequest(requestDto);
        }
        
        // DELETE: api/Request/5
        [HttpPut]
        [Route("api/Request/DeleteRequest")]
        public RequestDto Delete(RequestDto requestDto)
        {
            return RequestBL.DeleteRequest(requestDto);
        }
    }
}
