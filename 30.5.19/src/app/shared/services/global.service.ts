import { Injectable } from "@angular/core";
import 'rxjs/add/operator/map';
import { UserDto } from '../../models';

export class GlobalService{
    public currentUser:UserDto;
    constructor() {
      
        this.currentUser=new UserDto();
    }
    
    getUser() {
        return this.currentUser;
    }

    setUser(user: UserDto) {
        this.currentUser = user;
    }
    
}
    