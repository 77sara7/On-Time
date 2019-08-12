
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import { BaseHttpService } from "../shared/services";
import { ActivatedRoute, Router } from "@angular/router";
import { PagesRouter, BaseApiService } from '..';
import { RequestDto, UserDto } from '../models';

@Injectable()
export class CommonService extends BaseApiService{
    public currentUser:UserDto;

  constructor(private router: Router,private baseHttpService: BaseHttpService) {
    super("User")
   }

setCurrentUser(currentUser:UserDto){
    this.currentUser = currentUser;
}
getCurrentUser(){
    return this.currentUser;
}

 }
