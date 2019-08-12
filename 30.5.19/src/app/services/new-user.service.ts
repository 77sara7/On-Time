
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import { BaseApiService } from "../shared";
import { BaseHttpService } from "../shared/services";
import { ActivatedRoute, Router } from "@angular/router";
import { UserDto } from '../models';
import { PagesRouter } from '..';



@Injectable()
export class NewUserService extends BaseApiService {

    public userName:string;
    public page:PagesRouter;
    public currentUser:UserDto;

  constructor(private router: Router,private baseHttpService: BaseHttpService) {
    super('User');
   }

setCurrentUser(user:UserDto){
    this.currentUser = user;
}
addUser( user:UserDto) : Observable<UserDto>
    {
        this.userName = user.name;    
        return this.signIn(user)
    }

signIn( user:UserDto) : Observable<UserDto> {
    let url = this.actionUrl('AddNewUser');
    let params: URLSearchParams = new URLSearchParams();

    //?
    if (typeof user.name === "undefined" || typeof user.password === "undefined"||typeof user.name === "undefined") // עדיף לא לשלוח בכלל לשרת. יש לטפל בobservable במקרה כזה
    {
        user.name = "";
        user.password = "";
        user.mail="";
    }
    return this.baseHttpService.post<UserDto>(url, user);
}
}
