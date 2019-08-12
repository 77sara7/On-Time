
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import { BaseApiService } from "../shared";
import { BaseHttpService } from "../shared/services";
import { ActivatedRoute, Router } from "@angular/router";
import { RequestDto } from '../models';

@Injectable()
export class RequestService extends BaseApiService {

    public requestName:string;
    public static currentRequest:RequestDto;

  constructor(private router: Router,private baseHttpService: BaseHttpService) {
    super('Request');
   }

setCurrentRequest(request:RequestDto){
    RequestService.currentRequest = request;
}
addRequest(request:RequestDto) : Observable<RequestDto>
{
    let url = this.actionUrl('AddNewRequesr');
    this.requestName = request.request_name;  
    let params: URLSearchParams = new URLSearchParams();
    return this.baseHttpService.post<RequestDto>(url, request);          
}
updateRequest( request:RequestDto) : Observable<RequestDto>
{
    this.requestName = request.request_name;    
    let url = this.actionUrl('UpdateRequest');
    let params: URLSearchParams = new URLSearchParams();
      return this.baseHttpService.put<RequestDto>(url, request);
}
updateDetailsOfTimingRequest( request:RequestDto) : Observable<RequestDto>
{
    this.requestName = request.request_name;    
    let url = this.actionUrl('updateDetailsOfTimingRequest');
    let params: URLSearchParams = new URLSearchParams();
      return this.baseHttpService.put<RequestDto>(url, request);
}
deleteRequest( request:RequestDto) : Observable<RequestDto>
{
    this.requestName = request.request_name;    
    let url = this.actionUrl('DeleteRequest');
    let params: URLSearchParams = new URLSearchParams();
    return this.baseHttpService.put<RequestDto>(url, request);
}
getAllRequests(user_id:number) : Observable<RequestDto[]>
{ 
    let url = this.actionUrl('GetAllRequestByUserId');
        let params: URLSearchParams = new URLSearchParams();
        params.set('user_id', user_id.toString());
        return this.baseHttpService.get<RequestDto[]>(url,params);
}
 }
