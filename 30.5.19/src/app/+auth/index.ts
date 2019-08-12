import { Routes } from '@angular/router';
import { AuthComponent } from './auth.component';
import { LOGIN_COMPONENTS, LoginComponent } from './+login';
import { NewUserComponent } from '../new-user/new-user.component';
import { TimingComponent } from '../timing/timing.component';
import { RequestComponent } from '../requests/request/request.component';

export * from './+login';

export const AUTH_COMPONENTS =[
    AuthComponent,
    ...LOGIN_COMPONENTS
]

export const AUTH_ROUTES: Routes = [
    {
        path: '',
        component:LoginComponent
    }
]