import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule }     from '@angular/router';

import { LocalStorageService, AuthService } from './services';

import { AppComponent } from './app.component';
import { VehicleComponent } from './vehicle/vehicle.component';
import { DatepickerModule } from 'ng2-bootstrap/datepicker';
import { RegisterComponent } from './ui/register/register.component';
import { LoginComponent } from './ui/login/login.component';
import { AppRoutingModule }     from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    VehicleComponent,
    LoginComponent,
    RegisterComponent
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
