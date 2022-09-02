import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Component, ViewChild, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FormControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent, MatChipList } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { FocusDto } from '@models/domain/focus-dto';
import { FocusService } from '@app/_core/_services/focus.service';

@Component({
  selector: 'app-focus-list',
  templateUrl: 'focus-list.component.html',
  styleUrls: ['focus-list.component.css'],
})
export class FocusListComponent implements OnInit {

  visible = true;
  selectable = true;
  removable = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];

  public formGroup: FormGroup;

  public filteredItems$: Observable<FocusDto[]>;
  @Input() public selectedItems: FocusDto[] = [];
  @Input() public locked: boolean = false;

  data: FocusDto[];
  @Input() inputPlaceholder: string;
  @Input() requiredError: string;
  @Output() updateSelectedItems = new EventEmitter<FocusDto[]>();

  @ViewChild('itemList') itemList: MatChipList;

  private extractCtrl = (ctrlName: string) => {
    return this.formGroup.get(ctrlName);
  }

  private itemInputCtrl = () => {
    return this.extractCtrl('itemInputCtrl');
  }

  private itemsCtrl = () => {
    return this.extractCtrl('itemsCtrl');
  }

  constructor(
    public formBuilder: FormBuilder,
    private focusService: FocusService) {}

  ngOnInit(): void{
    this.buildFormGroup();
    this.focusService.getAll()
      .subscribe(data => {
        this.data = data;
        this.iniFilteredItems();
      });
  }

  private iniFilteredItems(): void{
    this.filteredItems$ = this.itemInputCtrl().valueChanges
      .pipe(
        startWith(''),
        map(value => this.itemFilter(value))
      );
  }

  private buildFormGroup(): void{
    this.formGroup = this.formBuilder.group({
      itemInputCtrl: [{value: '', disabled: this.locked}],
      itemsCtrl: [this.selectedItems, this.validateItems]
    });

    this.itemsCtrl().statusChanges.subscribe(
      status => this.itemList.errorState = status === 'INVALID'
    );
  }

  private validateItems(items: FormControl): any{
    if (items.value && items.value.length === 0) {
      return {
        validateItemArray: { valid: false }
      };
    }

    return null;
  }

  private itemFilter(value: any): FocusDto[] {

    const filterValue = (value === null || value instanceof Object) ? '' : value.toLowerCase();

    const matches = this.data.filter(i =>
      i.description.toLowerCase().includes(filterValue));

    const formValue = this.itemsCtrl().value;

    return formValue === null ? matches : matches.filter(x =>
      !(formValue.find(y => y.Id === x.id))
    );
  }

  public selectItem(event: MatAutocompleteSelectedEvent): void {

    if (!event.option) {
      return;
    }

    const value = event.option.value;
    if (value && value instanceof Object && !this.selectedItems.includes(value)) {

      this.selectedItems.push(value);
      this.itemsCtrl().setValue(this.selectedItems);
      this.itemInputCtrl().setValue('');

      this.updateSelectedItems.emit(this.selectedItems);
    }
  }

  public addItem(event: MatChipInputEvent): void {

    const input = event.input;
    const value = event.value;

    if (value.trim()) {

      const matches = this.data.filter(i =>
        i.description.toLowerCase() === value);

      const formValue = this.itemsCtrl().value;

      const matchesNotYetSelected = formValue === null ? matches : matches.filter(x =>
        !(formValue.find(y => y.Id === x.id)));

      if (matchesNotYetSelected.length === 1) {
        this.selectedItems.push(matchesNotYetSelected[0]);

        console.log(this.selectedItems);

        this.itemsCtrl().setValue(this.selectedItems);
        this.itemInputCtrl().setValue('');

        this.updateSelectedItems.emit(this.selectedItems);
      }
  }

    // Reset the input value
    if (input){
      input.value = '';
    }
  }

  public removeItem(item: FocusDto): void {

    const index = this.selectedItems.indexOf(item);

    if (index >= 0) {

      this.selectedItems.splice(index, 1);
      this.itemsCtrl().setValue(this.selectedItems);
      this.itemInputCtrl().setValue('');

      this.updateSelectedItems.emit(this.selectedItems);
    }
  }

  inputBlur(event: any): void{
    this.itemList.errorState = this.selectedItems.length === 0;
  }
}
