import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-datetime-selector',
  templateUrl: './datetime-selector.component.html',
  styleUrls: ['./datetime-selector.component.css']
})
export class DatetimeSelectorComponent implements OnInit {

  public hours: string[] = [
    '01', 
    '02', 
    '03', 
    '04', 
    '05', 
    '06',
    '07',
    '08',
    '09',
    '10',
    '11',
    '12'
  ];
  public minutes: string[] = ['00', '15', '30', '45'];

  public datetimeGroup: FormGroup;

  selectedDate: moment.Moment = null;
  selectedHour: string = null;
  selectedMin: string = null;
  selectedTimePeriod: string = null;

  constructor(
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit(): void { 
    this.iniDatetimeGroup();
  }

  iniDatetimeGroup(): void{
    this.datetimeGroup = this.formBuilder.group({
      dateCtrl: ['', [Validators.required]],
      hourCtrl: ['', [Validators.required]],
      minCtrl: ['', [Validators.required]],
      periodCtrl: ['', [Validators.required]]
    });
  }
}
