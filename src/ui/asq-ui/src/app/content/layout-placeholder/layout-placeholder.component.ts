import { Component, OnInit, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-layout-placeholder',
  templateUrl: './layout-placeholder.component.html',
  styleUrls: ['./layout-placeholder.component.css']
})
export class LayoutPlaceholderComponent implements OnInit {

  constructor(
    @Inject(DOCUMENT) document
  ) { }

  ngOnInit(): void { }
}
