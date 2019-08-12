import { Component, OnInit} from '@angular/core';
import { FrequencyEnum, WeekDayEnum, RequestDto, UserDto } from '../../models';
import { Router } from '@angular/router';
import { RequestService } from '../../services/request.service';
import { GlobalService } from '../../shared';
import { ViewEncapsulation } from '@angular/core';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class RequestComponent implements OnInit {
  
  frequency = FrequencyEnum;
  currentUser: UserDto;
  request=new RequestDto();
  urlFile;
  enterUrl=false;
  answer=false;
 
  ngAfterViewInit() {
  }

  constructor(private router: Router, private requestService: RequestService, private globalService: GlobalService) {  
    this.currentUser = globalService.getUser();
    }
    
  ngOnInit() {
     
   //if(this.globalService.getRequest().request_id!=-1){
    if(this.globalService.getRequest().request_id!=0){
      this.request=this.globalService.getRequest();
      this.question();
    }
    else{
     this.info()  
    }
    this.answer=true;
  }

  onRequestChanged(request: RequestDto) {
     
    this.request = request;
  }
  
  info() {
    Swal.fire({
      title: 'Recording',
      text: "Do you want to use the a recording from the extention or enter use only url?",
      type: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      cancelButtonText: "I'll be back from the extension...",
      confirmButtonText: 'Yes, use only url!'
    }).then((result) => {
      if (result.value) {
        this.enterUrl=true;       
      }    
    })
  }
question() {
    Swal.fire({
      title: 'Recording',
      text: "Do you want to use the recording you recorded in the extension or enter new url?",
      type: 'question',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      cancelButtonText: 'new url',
      confirmButtonText: 'Yes, use my record!'
    }).then((result) => {
      if (result.value) {
        this.enterUrl=false;       
        console.log("showUrl "+this.enterUrl)
      }    
    })
  }
  AddOrUpdateRequest(){
     
    var v=this.frequency[this.request.frequency_id];
    this.request.frequency_id=parseInt(v);
     if(this.enterUrl==true){
      this.AddRequest();
     }
     else{
      this.UpdateRequest();
     }
  }


  AddRequest(){
     
    console.log(this.request);
   this.request.content=this.urlFile;
    this.requestService.addRequest(this.request).subscribe(
      (data: RequestDto) => {
        console.log(data);
        this.request = data;
        this.requestService.setCurrentRequest(data);
      },
      fail => alert("Request not found"));

  }
  UpdateRequest(){
     
    console.log(this.request);
    this.requestService.updateRequest(this.request).subscribe(
      (data: RequestDto) => {
        console.log(data);
        this.request = data;
        this.requestService.setCurrentRequest(data);
      },
      fail => alert("Request not found"));
  }
}
