import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent }       from './ui/login/login.component';
import { RegisterComponent }    from './ui/register/register.component';
import { VehicleComponent }     from  './ui/vehicle/vehicle.component';
import { DashboardComponent }   from './ui/layout/dashboard/dashboard.component';
import { HomeComponent }        from './ui/pages/home/home.component';

const routes: Routes = [
    { path: '', component: DashboardComponent, children: [
      { path: '', component: HomeComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'vehicle', component: VehicleComponent } ]}
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {

}
