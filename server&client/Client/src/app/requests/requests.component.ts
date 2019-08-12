import { Component, OnInit } from '@angular/core';
import { RequestDto, FrequencyEnum, UserDto } from '../models';
import { Router } from '@angular/router';
import { RequestService, GlobalService } from '..';
import { LoginService } from '../services';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent implements OnInit {

  currentUser: UserDto;
  request: RequestDto = new RequestDto();
  urlFile: string;
  enterUrl: boolean = false;
  answer: boolean = false;
  popupUrl: boolean = false;
  enableEditArr: boolean[]= [];
  isClicked: boolean;
  isFull:boolean=false;
  requests: RequestDto[];
  public currentRequest: RequestDto;
  regexp = new RegExp(/(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/);

  constructor(private router: Router, private requestService: RequestService, private loginService: LoginService, private globalService: GlobalService) {
    this.currentUser = globalService.getUser();
    console.log("request");
  }
  ngOnInit() {
    if(this.globalService.getRequest().request_id!=0){//זאת אומרת שהגיע דרך התוסף
      debugger;
      this.request = this.globalService.getRequest();
      this.question();
    }
    else {//זאת אומרת שהגיע דרך האתר
      this.enterUrl = true;
    }
    this.answer = true;
    for (let i = 0; i < 20; i++) {
      this.enableEditArr.push(true);
   }
    this.getAllRequests();
  }

  getAllRequests() {
     this.requestService.getAllRequests(this.globalService.getUser().user_id).subscribe(
      (data: RequestDto[]) => {
        if(data==null){
          this.isFull=false;
        }
        else
        this.isFull=true;
        this.requests = data;
        debugger
        this.requests.forEach(r => {
          
          r.date_from = new Date(r.date_from);
          r.date_to = new Date(r.date_to);
        })
      },
      fail => alert("Requests not found"));
  }

  updateDetailsOfTimingRequest(request: RequestDto) {
    this.requestService.updateDetailsOfTimingRequest(request).subscribe(
      (data: RequestDto) => {
        this.currentRequest = data;
      },
      fail => alert("Request not found"));
  }
  deleteRequest(request: RequestDto) {
    request.is_relevant = false;
    this.requestService.deleteRequest(request).subscribe(
      (data: RequestDto) => {
        this.currentRequest = data;
        this.requests = this.requests.filter(req => req.is_relevant == true)
      },
      fail => alert("Request not found"));
  }

  onRequestChanged(request: RequestDto) {
    this.currentRequest = request;
  }

  onRequestChanged1(request: RequestDto) {

    this.request.date_from = request.date_from;
    this.request.date_to = request.date_to;
    this.request.day = request.day;
    this.request.hour = request.hour;
    this.request.frequency_id = request.frequency_id;
    this.request.request_name = request.request_name;
  }
  onInput() {
    if (this.popupUrl == false && this.globalService.getRequest().request_id == 0) {
      this.info();
      this.popupUrl = true;
    }
  }
  OnEnableEdit(index: number) {
    this.enableEditArr[index] = false;
  }
  info() {
    Swal.fire({
      title: 'Recording',
      text: "Note that you can set up a detailed browsing furthermore the URL field by using the extension",
      type: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      cancelButtonText: "<a href='/assets/OnTimeExtensions.zip' download style='text-decoration-line: none;color:white'>I'll be back through the extension...</a>",
      confirmButtonText: 'use only url!'
    }).then((result) => {
      if (result.value) {
        this.enterUrl = true;
      }
    })
  }
  question() {
    Swal.fire({
      title: 'Do not worry',
      text: "We have not forgotten your recording. Just select Schedule ...\n Did you regret it? You can set up tracking only through the URL field.",
      type: 'question',
      //type: 'info',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      cancelButtonText: 'new url',
      confirmButtonText: 'Yes, use my record!'
    }).then((result) => {
      if (result.value == undefined) {
        setTimeout(() => {
          this.sure();
        }, 0);

      }
    })
  }
  sure() {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this! Your recording is going to be erased...",
      type: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {

      if (result.value) {
        this.enterUrl = true;
      }
      else {
        this.enterUrl = false;
      }
    })
  }
  AddOrUpdateRequest() {

    this.request.user_id = this.currentUser.user_id;
    if (this.enterUrl == true) {
      this.AddRequest();
    }
    else {
      this.UpdateRequest();
    }
  }

  AddRequest() {
    if (!this.regexp.test(this.urlFile)) {
      console.log('URL is not valid');
      Swal.fire({
        type:"error",
        title: 'URL is not valid',
      })
      return;
    }
    console.log(this.request);
    this.request.content = this.urlFile;
    this.requestService.addRequest(this.request).subscribe(
      (data: RequestDto) => {
        console.log(data);
        this.request = data;
        this.requestService.setCurrentRequest(data);
        this.getAllRequests();
      },
      fail => alert("Request not found"));

  }
  UpdateRequest() {

    console.log(this.request);
    this.requestService.updateRequest(this.request).subscribe(
      (data: RequestDto) => {
        console.log(data);
        this.request = data;
        this.requestService.setCurrentRequest(data);
        this.getAllRequests();
      },
      fail => alert("Request not found"));
  }
}

