import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgModule, Component } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
 import { HttpModule } from '@angular/http';

//prime-ng
import { KeyFilterModule } from 'primeng/keyfilter';
import { CheckboxModule, MenuModule, ContextMenuModule, ButtonModule, PanelModule, InputTextModule, DropdownModule, StepsModule,SpinnerModule, DataTableModule, TabMenuModule, FileUploadModule, RadioButtonModule, InputMaskModule,
    ProgressSpinnerModule, LightboxModule
} from 'primeng/primeng';


//app components
import { AppComponent } from './app.component';

// import {AUTH_COMPONENTS} from './+auth';

//app modules
import { AppRoutingModule } from './app-routing.module';
import { ServicesModule, LoginService } from './services';
import {  SHARED_SERVICES } from './shared';
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
import { RequestsComponent } from './requests/requests.component';
import { HomeComponent } from './home/home.component';



@NgModule({
  declarations: [
    //app components
    AppComponent,
  
    // ...AUTH_COMPONENTS,
    TimingComponent,
    RequestsComponent,
    HomeComponent,
    
  ],

  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,

    //app modules
    ServicesModule,
   

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
    
    LoginService,
    SHARED_SERVICES
    //AutoService,
    //AuthGuard
  ], 
  bootstrap: [AppComponent]
})

export class AppModule { }
