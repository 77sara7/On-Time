import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from "./app.component";
import { RequestsComponent } from './requests/requests.component';
import { HomeComponent } from './home/home.component';
import { TimingComponent } from './timing/timing.component';

const APP_ROUTES: Routes = [
    {
        path:'home',
        component:AppComponent,
      
    },

    {
        path:'t',
        component:TimingComponent,
   
    },
    {
        path:'requests',
        component:RequestsComponent,
   
    },
    {
        path:'',
        component:HomeComponent
    }
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