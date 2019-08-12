
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import { BaseApiService } from "../shared";
import { BaseHttpService } from "../shared/services";
import { ActivatedRoute, Router } from "@angular/router";
import { PagesRouter } from '..';
import { RequestDto } from '../models';

@Injectable()
export class RequestService extends BaseApiService {

    public requestName:string;
    public page:PagesRouter;
    public static currentRequest:RequestDto;

  constructor(private router: Router,private baseHttpService: BaseHttpService) {
    super('Request');
   }

setCurrentRequest(request:RequestDto){
    RequestService.currentRequest = request;
}
//TODO.....s
addRequest(request:RequestDto) : Observable<RequestDto>
{
    let url = this.actionUrl('AddNewRequesr');
    this.requestName = request.request_name;  
    let params: URLSearchParams = new URLSearchParams();
    // if (typeof userName === "undefined" || typeof password === "undefined"||typeof mail === "undefined") // עדיף לא לשלוח בכלל לשרת. יש לטפל בobservable במקרה כזה
    // {
    //     userName = "";
    //     password = "";
    //     mail="";
    // }
    // var requestDto= this.fillData( requestName,dateFrom,dateTo,frequencyId,day,dayInMonth,hour);
    return this.baseHttpService.post<RequestDto>(url, request);          
}
updateRequest( request:RequestDto) : Observable<RequestDto[]>
{
    this.requestName = request.request_name;    
    let url = this.actionUrl('UpdateRequest');
    let params: URLSearchParams = new URLSearchParams();
        // if (typeof userName === "undefined" || typeof password === "undefined"||typeof mail === "undefined") // עדיף לא לשלוח בכלל לשרת. יש לטפל בobservable במקרה כזה
        // {
        //     userName = "";
        //     password = "";
        //     mail="";
        // }
    //   var requestDto= this.fillData( requestName,dateFrom,dateTo,frequencyId,day,dayInMonth,hour);
      return this.baseHttpService.put<RequestDto[]>(url, request);
}
deleteRequest( request:RequestDto) : Observable<RequestDto[]>
{
    this.requestName = request.request_name;    
    let url = this.actionUrl('DeleteRequest');
    let params: URLSearchParams = new URLSearchParams();
    // var requestDto= this.fillData( requestName,dateFrom,dateTo,frequencyId,day,dayInMonth,hour);
    return this.baseHttpService.put<RequestDto[]>(url, request);
}
getAllRequests(user_id:number) : Observable<RequestDto[]>
{ 
    let url = this.actionUrl('GetAllRequestByUserId');
        let params: URLSearchParams = new URLSearchParams();
        params.set('user_id', user_id.toString());
        return this.baseHttpService.get<RequestDto[]>(url,params);
}
// fillData(requestName: string,dateFrom:Date,dateTo:Date,frequencyId:number,day?:number,dayInMonth?:number,hour?:number){
//     let request:RequestDto=
//     {
//         request_name:requestName,
//         ErrorMessage:"no error",
//         user_id:0,
//         IsAuthorized:false,          
//         date_from:dateFrom,
//         date_to:dateTo,
//         day:day,
//         hour:hour,
//         day_in_month:dayInMonth,
//         file_id:0,
//         frequency_id:frequencyId,
//         recording_id:0,
//         request_id:0
//         };

//         return request; 
//     }
 }
