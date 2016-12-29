import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule }     from '@angular/router';

import { DatepickerModule }   from 'ng2-bootstrap/datepicker';

import { LocalStorageService, AuthService, VehicleService } from './services';
import { AppComponent } from './app.component';
import { VehicleComponent }   from './ui/vehicle/vehicle.component';
import { RegisterComponent }  from './ui/register/register.component';
import { LoginComponent }     from './ui/login/login.component';
import { DashboardComponent } from './ui/layout/dashboard/dashboard.component';
import { SidebarComponent }   from './ui/shared';
import { AppRoutingModule }   from './app-routing.module';
import { ObjectPipe } from './common/Pipes/object.pipe';
import { HomeComponent } from './ui/pages/home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    SidebarComponent,
    VehicleComponent,
    LoginComponent,
    RegisterComponent,
    ObjectPipe,
    HomeComponent
  ],
  imports: [
    DatepickerModule.forRoot(),
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule
  ],
  providers: [ LocalStorageService, AuthService ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
