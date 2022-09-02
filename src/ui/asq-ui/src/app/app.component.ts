import { Component, OnInit } from '@angular/core';
import { ProgressBarService } from './_core/_services/progress-bar.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  title = 'asq-ui';

  constructor(
    public progressBarService: ProgressBarService
  ){ }

  ngOnInit(): void{ }
}
