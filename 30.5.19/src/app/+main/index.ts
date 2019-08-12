import { Routes } from '@angular/router';
import { MainComponent } from './main.component';

import { DASHBOARD_ROUTES, DASHBOARD_COMPONENTS } from './+dashboard';
//import { AuthGuard } from '../services/auto.guard'

export * from './+dashboard';

export const MAIN_COMPONENTS = [
    MainComponent,
    ...DASHBOARD_COMPONENTS,
]

export const MAIN_ROUTES: Routes = [
    {
        path: 'main',
        component: MainComponent,
        //canActivate: [AuthGuard],
        children: [
            {
                path: '',
                pathMatch: 'full',
                redirectTo: 'dashboard'
            },
            ...DASHBOARD_ROUTES,
        ]
    }
]
