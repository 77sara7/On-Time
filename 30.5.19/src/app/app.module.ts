import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgModule, Component } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

//prime-ng
import { KeyFilterModule } from 'primeng/keyfilter';
import { CheckboxModule, MenuModule, ContextMenuModule, ButtonModule, PanelModule, InputTextModule, DropdownModule, StepsModule,SpinnerModule, DataTableModule, TabMenuModule, FileUploadModule, RadioButtonModule, InputMaskModule,
    ProgressSpinnerModule, LightboxModule
} from 'primeng/primeng';
import { CardModule } from 'primeng/card';
//app components
import { AppComponent } from './app.component';
import { MAIN_COMPONENTS } from './+main';
import { SHARED_COMPONENTS } from '../app/shared/uix/components/index'
import {AUTH_COMPONENTS} from './+auth';

//app modules
import { AppRoutingModule } from './app-routing.module';
import { ServicesModule } from './services';
import { SharedModule, NewUserService } from './shared';
import {MessagesModule} from 'primeng/messages';
import {MessageModule} from 'primeng/message';
import {DataViewModule} from 'primeng/dataview';
import { CalendarModule } from 'primeng/components/calendar/calendar';
import { DialogModule } from 'primeng/components/dialog/dialog';
import { AutoCompleteModule } from 'primeng/components/autocomplete/autocomplete';
import {TabViewModule} from 'primeng/tabview';
import { SliderModule } from 'primeng/slider';
import {InputSwitchModule} from 'primeng/inputswitch';
import { TimingComponent } from './timing/timing.component';
import { NewUserComponent } from './new-user/new-user.component';
import { RequestsComponent } from './requests/requests.component';
import { RequestComponent } from './requests/request/request.component';
import { SafeHtmlPipe } from './requests/request/safeHtml';
import { IframeComponent } from './iframe/iframe.component';



@NgModule({
  declarations: [
    //app components
    AppComponent,
    ...MAIN_COMPONENTS,
    ...SHARED_COMPONENTS,
    ...AUTH_COMPONENTS,
    TimingComponent,
    NewUserComponent,
    RequestsComponent,
    RequestComponent,
    SafeHtmlPipe,
    IframeComponent
  ],
  entryComponents:[NewUserComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,

    //app modules
    ServicesModule,
    SharedModule,

    //prime-ng modules
    MenuModule,
    CheckboxModule,
    ContextMenuModule,
    ButtonModule,
    PanelModule,
    InputTextModule,
    RadioButtonModule,
    DropdownModule,
    StepsModule,
    TabMenuModule,
    DataTableModule,
    DataViewModule,
    CalendarModule,
    DialogModule,
    AutoCompleteModule,
    MessagesModule,
    MessageModule,
    KeyFilterModule,
    TabViewModule,

    RadioButtonModule,
    InputMaskModule,
    FileUploadModule,
    SliderModule,
    SpinnerModule,
    ProgressSpinnerModule,
    LightboxModule,
    BrowserAnimationsModule,
      InputSwitchModule
  ],
  providers: [
    NewUserService,
    
    //AutoService,
    //AuthGuard
  ], 
  bootstrap: [AppComponent]
})
// @Component({ providers: [ NewUserService ]})
export class AppModule { }
