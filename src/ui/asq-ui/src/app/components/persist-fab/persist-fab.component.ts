import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-persist-fab',
  templateUrl: './persist-fab.component.html',
  styleUrls: ['./persist-fab.component.css']
})
export class PersistFabComponent implements OnInit {

  @Output() clicked = new EventEmitter<Event>();
  //@Input() requiredError: string;

  constructor() { }

  ngOnInit(): void { }

  onClick(event: Event): void{
    this.clicked.emit(event);
  }
}
