import { NgModule } from "@angular/core";
import { BaseHttpService } from './base-http.service';
import { GlobalService } from './global.service';



export const SHARED_SERVICES=[
    GlobalService,
    BaseHttpService
]

@NgModule({
    providers: [...SHARED_SERVICES]
})
export class SharedServicesModule { }
