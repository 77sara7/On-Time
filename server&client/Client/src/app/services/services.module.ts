import { NgModule } from "@angular/core";
import { /*MAIN_SERVICES,*/ AUTH_SERVICES } from "./Auth";
import { RequestService } from './request.service';
import { InspectService } from './inspect.service';

const SERVICES =[
    //...MAIN_SERVICES,
    ...AUTH_SERVICES
]
@NgModule({
    providers: [...SERVICES,RequestService,InspectService]
})
export class ServicesModule { }

