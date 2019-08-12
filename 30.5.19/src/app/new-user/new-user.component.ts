import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
//import { NewUserService } from '..';
import { UserDto } from '../models';
import { NewUserService } from '..';

@Component({
  selector: 'new-user',
  templateUrl: './new-user.component.html',
  styleUrls: ['./new-user.component.css']
})
export class NewUserComponent implements OnInit {

  userToAdd: UserDto;
  user:UserDto;

  constructor(private router: Router, private newUserService: NewUserService) {
    console.log("newuser");
   }

  ngOnInit() {
   this.userToAdd=new UserDto();
  }
  
  AddUser() {
    debugger
    console.log("add");
    this.newUserService.addUser(this.userToAdd).subscribe(
  
      (data: UserDto) => {
        this.user = data; 
        this.newUserService.setCurrentUser(data);
        this.router.navigateByUrl("/request");
        // this.loginSucceed = this.currentUser.isAuthorized;    
        // if (this.currentUser.isAuthorized) {
        //     this.router.navigateByUrl("/main");
        // }
      },
      fail => alert("User not found")); 
  }
}
