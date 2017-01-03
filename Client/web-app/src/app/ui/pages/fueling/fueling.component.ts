import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-fueling',
  templateUrl: './fueling.component.html',
  styleUrls: ['./fueling.component.css']
})
export class FuelingComponent implements OnInit {

  model: Object = {
    date: new Date(),
    odometer: 123456,
    isSeriesBegining: false,
    fuelType: 1,
    isFull: true,
    isEmpty: false,
    quantity: 12.45,
    amount: 40.00
  };

  constructor() { }

  ngOnInit() {
  }

  saveFueling(fueling) {
    console.log(fueling);
  }
}
