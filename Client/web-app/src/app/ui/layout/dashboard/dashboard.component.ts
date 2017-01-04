import { Component, OnInit } from '@angular/core';
import { TranslateService } from 'ng2-translate';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(public translate: TranslateService) {
    translate.addLangs(['en', 'bg']);
    translate.setDefaultLang('en');

    let browserLang = translate.getBrowserLang();
     translate.use(browserLang.match(/en|bg/) ? browserLang : 'en');
  }

  ngOnInit() {
  }

}
