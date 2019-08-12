import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';


import { AppComponent } from "./app.component";

import { MAIN_ROUTES } from "./+main";
import {AUTH_ROUTES} from "./+auth";
import { NewUserComponent } from './new-user/new-user.component';
import { RequestComponent } from './requests/request/request.component';
import { RequestsComponent } from './requests/requests.component';

const APP_ROUTES: Routes = [
    {
        path:'home',
        component:AppComponent,
   
    },
    ...AUTH_ROUTES, 
    ...MAIN_ROUTES,    
    {
        path:'new-user',
        component:NewUserComponent,
   
    },
    {
        path:'request',
        component:RequestComponent,
   
    },
    {
        path:'requests',
        component:RequestsComponent,
   
    },
];


@NgModule({
    imports: [
        RouterModule.forRoot(APP_ROUTES/*, { useHash: true }*/)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }