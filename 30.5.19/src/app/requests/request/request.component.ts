import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { TimingClass, RequestDto, UserDto } from '../../models';
import { Router } from '@angular/router';
import { RequestService } from '../../services/request.service';
import { TimingComponent } from '../../timing/timing.component';
import { InspectService } from '../../services/inspect.service';
import { LoginService } from '../../services';
import { GlobalService } from '../../shared';
import { ViewEncapsulation } from '@angular/core';


@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class RequestComponent implements OnInit {

  public requestName: string;
  public currentRequest: RequestDto;
  currentUser: UserDto;
  page: string;
  iframe;

  @Input()
  timing: TimingClass;

  @ViewChild(TimingComponent) timingComponent: TimingComponent;

  ngAfterViewInit() {
  }
  saveData(timing) {
    this.timing = timing;
  }
  constructor(private router: Router, private requestService: RequestService, private loginService: LoginService, private globalService: GlobalService,private inspectService:InspectService) {
    console.log("request");
    this.currentUser = globalService.getUser();
    this.ShowPage();
  }

  ngOnInit() {
    this.currentRequest = new RequestDto();
    this.inspectService.onInit()
  }
  fillTiming() {
    this.currentRequest.date_from = this.timing.date_from;
    this.currentRequest.date_to = this.timing.date_to;
    this.currentRequest.day = this.timing.day;
    this.currentRequest.day_in_month = this.timing.day_in_month;
    this.currentRequest.hour = this.timing.hour;
    this.currentRequest.frequency_id = this.timing.frequency_id;
  }
  AddRequest() {
    debugger
    var a =
      this.timingComponent.onSelectTiming()
    this.fillTiming();
    console.log("addRequest");
    this.currentRequest.file_stream = [0, 1, 0, 1];
    this.currentRequest.recording_stream = [1, 1, 1, 1];
    this.currentRequest.user_id = this.globalService.getUser().user_id;
    debugger
    if (this.currentRequest.frequency_id == undefined)
      this.currentRequest.frequency_id = 1;
    //TODO FILE
    this.requestService.addRequest(this.currentRequest).subscribe(
      (data: RequestDto) => {
        this.currentRequest = data;
        this.requestService.setCurrentRequest(data);
        // this.loginSucceed = this.currentUser.isAuthorized;    
        // if (this.currentUser.isAuthorized) {
        //     this.router.navigateByUrl("/main");
        // }
      },
      fail => alert("Request not found"));
  }
  UpdateRequest(requestToUpdate: RequestDto) {
    debugger
    this.timingComponent.onSelectTiming()
    this.fillTiming();
    console.log("updateRequest");
    this.requestService.updateRequest(requestToUpdate).subscribe(
      (data: RequestDto[]) => {
      },
      fail => alert("Request not found"));
  }

  ShowPage() {

    this.page = "<h1 style='color:red'>Yehudit Cohen</h1></br><h2>Sara Gurwicz</h2>";

    // this.page=`<html>
    // <body>
    //   <div id="demo" onclick="myFunction()">Click me to change my HTML content (innerHTML).</div>
    //   <script>
    //     function myFunction() {
    //       document.getElementById("demo").innerHTML = "<h1 style='color:red'>Yehudit Cohen</h1></br><h2>Sara Gurwicz</h2> ";
    //     }
    //   </script>
    // </body>
    // </html>`;

  }
}
