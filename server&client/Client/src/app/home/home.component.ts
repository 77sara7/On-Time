import { Component, OnInit, Input } from '@angular/core';
import { LoginService } from '../services/Auth/login.service';
import Swal from 'sweetalert2';
import { UserDto } from '../models/dto/userDto';
import { GlobalService } from '../shared/services/global.service';
import { NewUserService } from '../shared';
import { ActivatedRoute, Router } from '@angular/router';
import { RequestDto } from '../models';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  regexp = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
  name: string;
  mail: string;
  password: string;
  loginSucceed = true;
  wasErrorPassword = true;
  requestId: string;
  wasErrorId: boolean;
  currentUser = new UserDto();
  constructor(private loginService: LoginService, private globalService: GlobalService, private newUserService: NewUserService, private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    debugger
    this.requestId = this.activatedRoute.snapshot.queryParamMap.get('contentId');
    debugger
    this.globalService.setRequestId(Number(this.requestId));
  }
  login() {
    Swal.fire({
      title: 'Login',
      html:
        ` 
        <input  id="name-input"  type="email" class="form-control swal2-input" placeholder="Mail"/>
        <input type="password" id="password-input" class="form-control swal2-input" placeholder="Password"/>
        `,
      focusConfirm: false,
      preConfirm: () => {
        var name = <HTMLInputElement>document.getElementById('name-input')
        this.mail = name.value
        var password = <HTMLInputElement>document.getElementById('password-input')
        this.password = password.value
        if (this.mail == "") {
          Swal.showValidationMessage(
            'Mail is required'
          );
          return;
        }
        else if (!this.regexp.test(this.mail)) {
          Swal.showValidationMessage(
            'Mail is not valid'
          );
          return;
        }
        if (this.password == "") {
          Swal.showValidationMessage(
            'Password is required'
          );
          return;
        }
        setTimeout(() => {
          Swal.showLoading();

          this.loginService.login(this.mail, this.password).subscribe(
            (data: UserDto) => {

              this.globalService.setUser(data);
              if (data != null) {
                Swal.fire({
                  type: 'success',
                  title: 'Hello ' + data.name + '!',
                  confirmButtonText: 'Next'
                }).then((result) => {
                  if (result.value) {
                    this.router.navigateByUrl("/requests");
                  }
                })
              }
              else {
                Swal.fire({
                  type: 'error',
                  title: 'The user is not exsist!',
                })
              }
            }
            ,
            fail => console.log(fail)
          )
        }, 200)
      }
    })
  }

  addNewUser() {
    
    Swal.fire({
      title: 'Add New User',
      html:
        `  
        <input type="text" id="name" class="form-control swal2-input" placeholder="Name"/>
        <input  id="name-input"  type="email" class="form-control swal2-input" placeholder="Mail"/>
        <input type="password" id="password-input" class="form-control swal2-input" placeholder="Password" maxlength="10"/>    
        `,
      focusConfirm: false,
      preConfirm: () => {
        var uname = <HTMLInputElement>document.getElementById('name')
        this.currentUser.name = uname.value
        var name = <HTMLInputElement>document.getElementById('name-input')
        this.currentUser.mail = name.value
        var password = <HTMLInputElement>document.getElementById('password-input')
        this.currentUser.password = password.value
        if (this.currentUser.mail == "") {
          Swal.showValidationMessage(
            'Mail is required'
          );
          return;
        }
        else if (!this.regexp.test(this.currentUser.mail)) {
          Swal.showValidationMessage(
            'Mail is not valid'
          );
          return;
        }
        if (this.currentUser.password == "") {
          Swal.showValidationMessage(
            'Password is required'
          );
          return;
        }

        setTimeout(() => {
          Swal.showLoading();
          this.newUserService.addUser(this.currentUser).subscribe(
            (data: UserDto) => {
              this.globalService.setUser(data);
              if (data != null) {
                Swal.fire({
                  type: 'success',
                  title: 'Hello ' + data.name + '!',
                  confirmButtonText: 'Next'


                }).then((result) => {
                  if (result.value) {
                    this.router.navigateByUrl("/requests");
                  }
                })
              }
              else {
                Swal.fire({
                  type: 'error',
                  title: 'Can not add, try again!',
                })
              }
            }
            ,
            fail => console.log(fail)
          )
        }, 200)
      }
    })
  }
}
