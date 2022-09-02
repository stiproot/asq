import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { DevService } from '../dev.service';
import { DateTimeContainerDto } from '@app/_models/temp/datetime-container-dto';

@Component({
  selector: 'app-temp',
  templateUrl: './temp.component.html',
  styleUrls: ['./temp.component.css']
})
export class TempComponent implements OnInit {

  constructor(
    private service: DevService
  ) { }

  ngOnInit(): void { }

  onLogDatetimeClick(): void{

    //var date = moment(); 
    var date = moment().format('YYYY-MM-DDTHH:mm:ss[Z]');
    console.log('temp', 'date', date);

    var dateTimeContainer = new DateTimeContainerDto();
    dateTimeContainer.dateTime = date;

    this.service.post(dateTimeContainer)
      .subscribe(resp =>{
        console.log('temp', 'resp', resp);
      })
  }
}
