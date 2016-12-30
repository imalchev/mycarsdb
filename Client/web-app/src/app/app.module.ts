import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule }     from '@angular/router';

import { LocalStorageService, AuthService, VehicleService } from './services';
import { AuthGuard } from './common/auth.guard';
import { AppComponent } from './app.component';
import { VehicleComponent }   from './ui/vehicle/vehicle.component';
import { RegisterComponent }  from './ui/register/register.component';
import { LoginComponent }     from './ui/login/login.component';
import { DashboardComponent } from './ui/layout/dashboard/dashboard.component';
import { SidebarComponent }   from './ui/shared';
import { AppRoutingModule }   from './app-routing.module';
import { ObjectPipe } from './common/Pipes/object.pipe';
import { HomeComponent } from './ui/pages/home/home.component';
import { GarageComponent } from './ui/pages/garage/garage.component';


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    SidebarComponent,
    VehicleComponent,
    LoginComponent,
    RegisterComponent,
    ObjectPipe,
    HomeComponent,
    GarageComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule
  ],
  providers: [ LocalStorageService, AuthService, VehicleService, AuthGuard ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
