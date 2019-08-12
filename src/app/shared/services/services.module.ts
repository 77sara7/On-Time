import { NgModule } from "@angular/core";
import { BaseHttpService } from './base-http.service';
import { GlobalService } from './global.service';
import { BaseApiService } from '..';




export const SHARED_SERVICES=[
    GlobalService,
    BaseHttpService,
    BaseApiService
]

@NgModule({
    providers: [...SHARED_SERVICES]
})
export class SharedServicesModule { }
