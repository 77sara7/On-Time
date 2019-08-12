import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';

import { BaseHttpService, BaseApiService } from "../../shared";
import { ActivatedRoute, Router } from "@angular/router";
import { UserDto } from "../../models"

@Injectable()
export class LoginService extends BaseApiService {
      
    currentUser : UserDto;
    constructor(private router: Router, private baseHttpService: BaseHttpService) {
        super('User');
     
    }
    login(userMail: string,password:string): Observable<UserDto> {

        return this.signIn(userMail,password)
    }
    addUser( user:UserDto) : Observable<UserDto>
    {   
        return this.sign(user)
    }

   sign( user:UserDto) : Observable<UserDto> {
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
    signIn(userMail: string,password:string): Observable<UserDto> {
        let url = this.actionUrl('Login');
        let params: URLSearchParams = new URLSearchParams();
        if (typeof userMail === "undefined") // עדיף לא לשלוח בכלל לשרת. יש לטפל בobservable במקרה כזה
        {
            userMail = "";
        }
        params.set('userMail', userMail);
        params.set('password', password);
        return this.baseHttpService.get<UserDto>(url, params);
    }

    signOut() {
        // LoginService.currentUser=null; 
        this.router.navigateByUrl('');

    }
    }