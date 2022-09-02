import { EventEmitter, Output, Input, Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { TimezoneService } from '@app/_core/_services/timezone.service';
import { TimezoneDto } from '@app/_models/domain/timezone-dto';

@Component({
  selector: 'app-timezone-select',
  templateUrl: './timezone-select.component.html',
  styleUrls: ['./timezone-select.component.css']
})
export class TimezoneSelectComponent implements OnInit {

  @Output() timezoneSelectionChange = new EventEmitter<TimezoneDto>();
  @Input() public selectedTimezoneId: number = null;
  @Input() public locked: boolean = false;
  data: TimezoneDto[];
  public formGroup: FormGroup;

  constructor(
    public formBuilder: FormBuilder,
    private timezoneService: TimezoneService
  ) { }

  ngOnInit(): void {
    this.buildFormGroup();
    
    this.timezoneService.getAll()
      .subscribe(data => {
        this.data = data;
        if(this.selectedTimezoneId){
          this.timezoneSelectionChange.emit(this.getSelectedTimezoneObj);
        }
      });
  }

  private buildFormGroup(): void{
    this.formGroup = this.formBuilder.group({
      timezoneCtrl: [{value: '', disabled: this.locked}, Validators.required],
    });
  }

  private get getSelectedTimezoneObj(): TimezoneDto{
    return this.data.find(el => el.id === this.selectedTimezoneId);
  }

  onTimezoneSelectionChange(event: MatSelectChange): void{
      console.log('getSelectedTimezoneObj', this.getSelectedTimezoneObj);
      this.timezoneSelectionChange.emit(this.getSelectedTimezoneObj);
  }
}
