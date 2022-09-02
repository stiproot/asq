import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContentFilterConfigDto } from '@app/_models/domain/content-filter-config-dto';
import { FocusDto } from '@app/_models/domain/focus-dto';

@Component({
  selector: 'app-search-criteria',
  templateUrl: './search-criteria.component.html',
  styleUrls: ['./search-criteria.component.css']
})
export class SearchCriteriaComponent implements OnInit {

  //public searchCriteriaGroup: FormGroup;
  public model: ContentFilterConfigDto;
  @Output() searchCriteriaFilterClick = new EventEmitter<ContentFilterConfigDto>();

  constructor(
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.iniModel();
    //this.iniSearchCriteriaGroup();
  }

  private iniModel(): void{
    this.model = new ContentFilterConfigDto();
  }

  //iniSearchCriteriaGroup(): void{
    //this.searchCriteriaGroup = this.formBuilder.group({
      //elasticCtrl: ['', [Validators.required, Validators.maxLength(50)]],
    //});
  //}

  onFocusListUpdate(data: FocusDto[]): void{
    // build up mappings
    if (data && data.length){
      this.model.foci = data.map((focus, i) => focus.id);
    }
    else{
      this.model.foci = [];
    }
  }

  onSearchClick($event:any): void{
    console.log('search-criteria', 'filter', this.model);
    this.searchCriteriaFilterClick.emit(this.model);
  }
}
