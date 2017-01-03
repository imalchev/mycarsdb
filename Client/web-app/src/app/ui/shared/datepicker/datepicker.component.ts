import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

/** some kind of adapter to datepicker */
@Component({
  selector: 'app-datepicker',
  templateUrl: './datepicker.component.html',
  styleUrls: ['./datepicker.component.css']
})
export class DatepickerComponent implements OnInit {

  @Input() selectedDate: Date;
  @Output() selectedDateChange: EventEmitter<Date> = new EventEmitter<Date>();

  @Input() placeholder: string;

  get bindableDate() {
    return { year: this.selectedDate.getFullYear(), month: this.selectedDate.getMonth() + 1, day: this.selectedDate.getDay() };
  };

  options = {
    showTodayBtn: false,
    dateFormat: 'yyyy-mm',
    firstDayOfWeek: 'mo',
    sunHighlight: false,
    customPlaceholderTxt: this.placeholder, // 'Manufacture date',
      // height: '40px',
    width: '200px',
    inline: false,
    selectionTxtFontSize: '16px',
    showClearDateBtn: false,
  };

  constructor() { }

  ngOnInit() {
  }

  onDateChanged(event: any) {
    this.selectedDate = new Date(event.date.year, event.date.month - 1, event.date.day);
    this.selectedDateChange.emit(this.selectedDate);
  }
}
