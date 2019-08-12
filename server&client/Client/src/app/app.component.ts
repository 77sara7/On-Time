import { Component, OnInit } from '@angular/core';
import {MenuItem} from 'primeng/api';
import { GlobalService } from './shared/services/global.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html', 
   styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  items: MenuItem[];

  ngOnInit() {
    this.items = this.globalService.getSteps();
  }

  constructor(private globalService:GlobalService){
    console.log("app");
  }

  changeStep(item){
    if (this.globalService.getUser().user_id==undefined ) {
      this.infoLogin();
      return;
    } 
    else {
      this.globalService.changeStep(item);
    }
  }
  infoLogin(){
    Swal.fire({
      title: 'You are an anonymous user, please identify yourself!',
      type: 'info',
      html:'You can join or login to our customers',
       
      // showCloseButton: true,
      // showCancelButton: true,
      // focusConfirm: false,
      // confirmButtonText:
      //   '<i class="fa fa-thumbs-up"></i> login',
      // confirmButtonAriaLabel: 'Thumbs up, great!',
      // //איך לקשר ללוגין?
      // cancelButtonText:
      //   '<i class="fa fa-thumbs-down"></i> add',
      // cancelButtonAriaLabel: 'Thumbs down',
      //     //איך לקשר לadd?
  
    })
   }

  
}
