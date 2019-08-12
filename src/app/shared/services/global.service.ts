import { Injectable } from "@angular/core";
import 'rxjs/add/operator/map';
import {MenuItem} from 'primeng/api';
import { Router } from "@angular/router";
import { UserDto, RequestDto } from '../../models';

@Injectable()
export class GlobalService{

    private  currentUser:UserDto;
    private  currentRequest=new RequestDto();
     steps: MenuItem[];
    
    constructor(private router: Router) {
        this.currentUser=new UserDto();
        this.steps = [
            {label: 'HOME',icon:"fas fa-home"},          
            {label: 'NEW REQUEST'},
            {label: 'ALL REQUESTS'}
              ];
    }
   
    getUser() {
        return this.currentUser;
    }

    setUser(user: UserDto) {
        this.currentUser = user;
    }
    getRequest() {
        return this.currentRequest;
    }

     setRequestId(requestId: number) {
        this.currentRequest.request_id=requestId;
    }

     setRequest(request: RequestDto) {
        this.currentRequest=request;     
    }

    getSteps() {
        return this.steps;
    }

    changeStep(item){
        var arrRouter=["","request","requests"];
         this.router.navigateByUrl(arrRouter[item])
       }
}
    