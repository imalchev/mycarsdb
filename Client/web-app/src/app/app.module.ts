import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { LocalStorageService, AuthService } from './services';

import { AppComponent } from './app.component';
import { VehicleComponent } from './vehicle/vehicle.component';
import { DatepickerModule } from 'ng2-bootstrap/datepicker';

@NgModule({
  declarations: [
    AppComponent,
    VehicleComponent
  ],
  imports: [
    DatepickerModule.forRoot(),
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [ LocalStorageService, AuthService ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
