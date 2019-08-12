import { DashboardComponent } from './dashboard.component';
import { Routes } from '@angular/router';

export * from './dashboard.component';

export const DASHBOARD_COMPONENTS =[
    DashboardComponent
]

export const DASHBOARD_ROUTES: Routes = [
    {
        path: 'dashboard',
        component: DashboardComponent
    }
]