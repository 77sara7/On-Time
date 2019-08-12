import { Component , OnInit, ViewChild, AfterViewInit} from '@angular/core';
import {MenuItem} from "primeng/primeng";
import {Menu} from "primeng/components/menu/menu";
import { LoginService, NewUserService} from '../../services';
import { Router } from "@angular/router";
import {UserDto } from  "../../models"
import { GlobalService } from '../../shared';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Alert } from 'selenium-webdriver';

declare var jQuery :any;

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
  //styleUrls: ['../../main.component.scss']
})
export class LoginComponent implements OnInit, AfterViewInit {
  menuItems: MenuItem[];
  miniMenuItems: MenuItem[];

  @ViewChild('bigMenu') bigMenu : Menu;
  @ViewChild('smallMenu') smallMenu : Menu;

  userName: string;
  password: string;
  loginSucceed = true;


  constructor(private router: Router, private loginService: LoginService,private globalService:GlobalService) {

  }

  ngOnInit() {

    let handleSelected = function(event) {
      let allMenus = jQuery(event.originalEvent.target).closest('ul');
      let allLinks = allMenus.find('.menu-selected');

      allLinks.removeClass("menu-selected");
      let selected = jQuery(event.originalEvent.target).closest('a');
      selected.addClass('menu-selected');
    }

    this.menuItems = [
    ]

    this.miniMenuItems = [];
    this.menuItems.forEach( (item : MenuItem) => {
      let miniItem = { icon: item.icon, routerLink: item.routerLink }
      this.miniMenuItems.push(miniItem);
    })

  }

  selectInitialMenuItemBasedOnUrl() {
    let path = document.location.pathname;
    let menuItem = this.menuItems.find( (item) => { return item.routerLink[0] == path });
    if (menuItem) {
      let selectedIcon = this.bigMenu.container.querySelector(`.${menuItem.icon}`);
      jQuery(selectedIcon).closest('li').addClass('menu-selected');
    }
  }

  onSubmit() {
    debugger;
    this.loginService.login(this.userName, this.password).subscribe(
      (data: UserDto) => {
        console.log(data);
        
        debugger;
        this.globalService.setUser(data);
        var a=this.globalService.getUser();
        
        this.loginSucceed = this.globalService.getUser().isAuthorized;
        // if (this.currentUser.isAuthorized) {
            // this.router.navigateByUrl("/requests");
        // }
      },
      fail => alert("User not found")); 
  } 

  ngAfterViewInit() {
    this.selectInitialMenuItemBasedOnUrl();
  }

  login() {

    this.loginService.login(this.userName, this.password).subscribe(
      (data: UserDto) => {
        console.log(data);
        debugger;
       
        this.globalService.setUser(data);
        var a=this.globalService.getUser();
        
        this.loginSucceed =  this.globalService.getUser().isAuthorized;    
        // if (this.currentUser.isAuthorized) {
            // this.router.navigateByUrl("/requests");
        // }
      },
      fail => alert("User not found")); 
  }

  signOut(){
    this.loginService.signOut();
  }

}
