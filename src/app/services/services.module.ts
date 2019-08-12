import { NgModule } from "@angular/core";
import { RequestService } from './request.service';

import { LoginService } from './login.service';

@NgModule({
    providers: [RequestService,LoginService]
})
export class ServicesModule { }

