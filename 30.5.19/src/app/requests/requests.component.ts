import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { RequestDto, TimingClass } from '../models';
import { Router } from '@angular/router';
import { RequestService, GlobalService } from '..';
import { LoginService } from '../services';
import { TimingComponent } from '../timing/timing.component';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent implements OnInit {

  timing:TimingClass;
  // @Output()
  // fillTiming=new EventEmitter();

  constructor(private router: Router, private requestService: RequestService, private loginService: LoginService,private globalService:GlobalService) {
    console.log("request");
  }
  ngOnInit() {
this.getAllRequests();
  }

  @ViewChild(TimingComponent) timingComponent:TimingComponent;
  ngAfterViewInit() {
  }

  isClicked:boolean;
  requests: RequestDto[];
  public currentRequest:RequestDto;

  saveData(timing){
  this.timing=timing;
 }

  fillTiming(){
    this.currentRequest.date_from= this.timing.date_from;
    this.currentRequest.date_to= this.timing.date_to;
    this.currentRequest.day= this.timing.day;
    this.currentRequest.day_in_month= this.timing.day_in_month;
    this.currentRequest.hour= this.timing.hour;
    this.currentRequest.frequency_id= this.timing.frequency_id;
  }
fillData(request:RequestDto)
{ 
  this.isClicked=true;
  this.currentRequest=request;
  this.timing=new TimingClass();
  this.timing.date_from=new Date(this.currentRequest.date_from);
  this.timing.date_to=new Date(this.currentRequest.date_to);
  this.timing.day=this.currentRequest.day;
  this.timing.day_in_month=this.currentRequest.day_in_month;
  this.timing.frequency_id=this.currentRequest.frequency_id;
  this.timing.hour=this.currentRequest.hour;
  // this.fillTiming.emit(this.timing)
}

  getAllRequests() {
    this.requestService.getAllRequests(this.globalService.getUser().user_id).subscribe(
      (data: RequestDto[]) => {
        this.requests = data;
        // this.loginSucceed = this.currentUser.isAuthorized;    
        // if (this.currentUser.isAuthorized) {
        //     this.router.navigateByUrl("/main");
        // }
      },
      fail => alert("Requests not found"));
  }
  newRequest(){
    this.router.navigateByUrl("/request");
  }
  updateRequest() {
    this.timingComponent.onSelectTiming()
    this.fillTiming();
    this.requestService.updateRequest(this.currentRequest).subscribe(
      (data: RequestDto[]) => {
        this.requests = data;
       // this.currentRequest = data;
      //  this.requests.find(request=>request.request_id==this.currentRequest.request_id);
        // this.newUserService.setCurrentUser(data);
        // this.loginSucceed = this.currentUser.isAuthorized;    
        // if (this.currentUser.isAuthorized) {
        //     this.router.navigateByUrl("/main");
        // }
      },
      fail => alert("Request not found"));
  }
  deleteRequest() {
    this.currentRequest.is_relevant=false;
    this.requestService.deleteRequest(this.currentRequest).subscribe(
      (data: RequestDto[]) => {
        this.requests = data;
        // this.currentRequest = data;
        // this.newUserService.setCurrentUser(data);
        // this.loginSucceed = this.currentUser.isAuthorized;    
        // if (this.currentUser.isAuthorized) {
        //     this.router.navigateByUrl("/main");
        // }
      },
      fail => alert("Request not found"));
  }
}

