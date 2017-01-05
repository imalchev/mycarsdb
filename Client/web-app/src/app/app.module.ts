import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule }     from '@angular/router';

import { MyDatePickerModule } from 'mydatepicker';
import {TranslateModule} from 'ng2-translate';

import { LocalStorageService, AuthService, VehicleService } from './services';
import { AuthGuard }          from './common';
import { AppComponent }       from './app.component';
import { AppRoutingModule }   from './app-routing.module';

import { VehicleComponent }   from './ui/pages/vehicle/vehicle.component';
import { RegisterComponent }  from './ui/register/register.component';
import { LoginComponent }     from './ui/login/login.component';
import { DashboardComponent } from './ui/layout/dashboard/dashboard.component';
import { SidebarComponent }   from './ui/layout/sidebar/sidebar.component';
import { HomeComponent }      from './ui/pages/home/home.component';
import { GarageComponent }    from './ui/pages/garage/garage.component';
import { FilterVehiclePipe } from './ui/shared/pipes/filter-vehicle.pipe';
import { FuelingComponent } from './ui/pages/fueling/fueling.component';
import { DatepickerComponent } from './ui/shared/datepicker/datepicker.component';
import { ErrorComponent } from './ui/shared/error/error.component';
import { VehicleListComponent } from './ui/shared/vehicle-list/vehicle-list.component';


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    SidebarComponent,
    VehicleComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    GarageComponent,
    FilterVehiclePipe,
    FuelingComponent,
    DatepickerComponent,
    ErrorComponent,
    VehicleListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule,
    MyDatePickerModule,
    TranslateModule.forRoot()
  ],
  providers: [ LocalStorageService, AuthService, VehicleService, AuthGuard ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
