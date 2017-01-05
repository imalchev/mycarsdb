import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { helpers } from '../../../common';

/** interface from mydatepicker component - that is hard to import */
interface IDate {
  year: number;
  month: number;
  day: number;
}

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
  @Input() dateFormat: string;
  @Input() width: string;
  @Input() showTodayBtn: boolean = true;  

  get bindableDate(): IDate {
    if (typeof this.selectedDate === 'string') {
      this.selectedDate = new Date(this.selectedDate);
    }

    if (helpers.isDate(this.selectedDate)) {
      let year = this.selectedDate.getFullYear();
      let month = this.selectedDate.getMonth() + 1;
      let day = this.selectedDate.getDate();

      return { year: year, month: month, day: day };
    }

    return null;
  };

  options: any = {
    showTodayBtn: true,
    todayBtnTxt: 'Today',
    dateFormat: 'yyyy-mm-dd',
    firstDayOfWeek: 'mo',
    sunHighlight: false,
    markCurrentDay: true,
    height: '34px',
    width: '210px',
    selectionTxtFontSize: '18px',
    alignSelectorRight: false,
    indicateInvalidDate: true,
    showDateFormatPlaceholder: true,
    editableMonthAndYear: true,
    minYear: 1900,
    componentDisabled: false,
    inputValueRequired: false,
    showClearDateBtn: true,
    showSelectorArrow: true
  };

  constructor() { }

  ngOnInit(): void {
    if (this.dateFormat) {
      this.options.dateFormat = this.dateFormat;
    }

    if (this.width) {
      this.options.width = this.width;
    }

    this.options.showTodayBtn = this.showTodayBtn;

    if (this.placeholder) {
      this.options.showDateFormatPlaceholder = false;
      this.options.customPlaceholderTxt = this.placeholder;
    }
  }

  onDateChanged(event: any): void {
    if (typeof event.date.year === 'number'
      && typeof event.date.month === 'number'
      && typeof event.date.day === 'number') {
      this.selectedDate = new Date(event.date.year, event.date.month - 1, event.date.day);
    } else {
      this.selectedDate = null;
    }

    this.selectedDateChange.emit(this.selectedDate);
  }
}
