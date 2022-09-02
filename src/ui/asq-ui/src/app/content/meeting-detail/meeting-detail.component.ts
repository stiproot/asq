import { Inject, Component, OnInit, Input, Optional } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSelectChange } from '@angular/material/select';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ThemePalette } from '@angular/material/core';
import * as moment from 'moment';
import { MeetingService } from '../meeting.service';
import { NotificationDialogService } from '@app/_core/_services/notification-dialog.service';
import { HelperService } from '@app/_core/_services/helper.service';
import { MeetingValidationService } from '../../_core/_services/meeting-validation.service';
import { AuthService } from '@app/_core/_services/auth.service';
import { RegexConstants } from '@app/_core/_constants/regex-constants';
import { DateTimeFormatConstants } from '@app/_core/_constants/datetime-format-contants';
import { FocusDto } from '@app/_models/domain/focus-dto';
import { FocusMeetingMappingDto } from '@app/_models/domain/focus-meeting-mapping-dto';
import { MeetingDto } from '@app/_models/domain/meeting-dto';
import { TimezoneDto } from '@app/_models/domain/timezone-dto';
import { HttpErrorResponse } from '@angular/common/http';
import { ProgressBarService } from '@app/_core/_services/progress-bar.service';
import { ImgService } from '../img.service';

@Component({
  selector: 'app-meeting-detail',
  templateUrl: './meeting-detail.component.html',
  styleUrls: ['./meeting-detail.component.css']
})
export class MeetingDetailComponent implements OnInit {

  // inputs
  @Input() inputId: string | null;

  public meetingInfoGroup: FormGroup;
  readonly fileReader: FileReader = new FileReader();

  // models
  hours: number[] = [0, 1, 2, 3, 4, 5];
  minutes: number[] = [0, 15, 30, 45];
  model: MeetingDto;
  originalModel: MeetingDto;
  estHr: number;
  estMin: number;
  imgFormData: FormData = null;
  imgPreviewUrl: any;

  // focus list to choose from
  public baseFocusList: FocusDto[];
  public selectedFocusList: FocusDto[] = [];

  private get id(): string | null{
    // query param
    if(this.router.url.match(this.regex.MEETING_URL_REGEX)){
      return this.route.snapshot.paramMap.get('id');
    }
    // dialog
    else if(this.injectedId){
      return this.injectedId;
    }
    // component
    else if(this.inputId){
      return this.inputId;
    }
    else{
      throw Error('No meeting id provided');
    }
  }

  // datetime picker config
  public date: moment.Moment;
  public minDate: moment.Moment;
  public maxDate: moment.Moment;
  public color: ThemePalette = 'accent';

  // crud mode
  mode = 'view';

  get view(): boolean{
    return this.mode === 'view';
  }

  get edit(): boolean{
    return this.mode === 'edit';
  }

  get isDialog(): boolean{
    //return this.router.url.match(this.regex.LECTURE_URL_REGEX) === null;
    return this.injectedId !== null;
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private regex: RegexConstants,
    private formats: DateTimeFormatConstants,
    private meetingService: MeetingService,
    private imgService: ImgService,
    private authService: AuthService,
    private notification: NotificationDialogService,
    private validation: MeetingValidationService,
    private helper: HelperService,
    public progressBarService: ProgressBarService,
    // The following is necessary if meeting-detail is going to be opened in a dialog...
    @Optional() @Inject(MAT_DIALOG_DATA) private injectedId: string | null
  ) { }

  ngOnInit(): void {
    this.iniModel();
    this.iniMeetingInfoGroup();
  }

  private iniModel(): void{
    if (this.id === 'new'){
      this.model = new MeetingDto();
      // set user information & defaults
      this.model.hostId = this.authService.userValue.hostId;
      this.model.creationUserId = this.authService.userValue.id;
      this.model.timezoneId = this.authService.userValue.timezoneId;
      this.mode = 'edit';
    }
    else{
      this.meetingService.get(this.id)
        .subscribe((data: MeetingDto) => {
          console.log('meeting-detail', 'meeting', data);
          this.model = data;
          this.originalModel = this.helper.clone<MeetingDto>(this.model);
          this.refreshUiEst();
          this.selectedFocusList = this.model.foci.map(v => v.focus);
        });
    }
  }

  iniMeetingInfoGroup(): void{
    this.meetingInfoGroup = this.formBuilder.group({
      titleCtrl: ['', [Validators.required, Validators.maxLength(50)]],
      descCtrl: ['', [Validators.required, Validators.maxLength(500)]],
      startDateCtrl: ['', Validators.required],
      estHrCtrl: ['', [Validators.required, Validators.pattern(this.regex.NUMERIC_REGEX)]],
      estMinCtrl: ['', [Validators.required, Validators.pattern(this.regex.NUMERIC_REGEX)]]
    });
  }

  private refreshModelEst(): void{
    if (this.estHr === undefined || this.estMin === undefined){
      this.model.estimatedDuration = null;
    }
    else{
      this.model.estimatedDuration = this.estHr * 60 + this.estMin;
    }
  }

  private refreshUiEst(): void{
    if (this.model?.estimatedDuration !== null){
      const newEstHr = Math.floor(this.model.estimatedDuration / 60);
      const newEstMin = this.model.estimatedDuration % 60;
      if (!this.hours.includes(newEstHr)){
        throw Error('Invalid hours calculated');
      }
      if (!this.minutes.includes(newEstMin)){
        throw Error('Invalid mins calculated');
      }
      this.estHr = newEstHr;
      this.estMin = newEstMin;
    }
  }

  onHrSelectionChange(event: MatSelectChange): void{
    this.refreshModelEst();
  }

  onMinSelectionChange(event: MatSelectChange): void{
    this.refreshModelEst();
  }

  onFileChanged(event: FormData): void{
    this.imgFormData = event;
  }

  onTimezoneSelectionChange(event: TimezoneDto): void{
    this.model.timezoneId = event.id;
    this.model.timezone = event;
  }

  onFocusListUpdate(data: FocusDto[]): void{
    // build up mappings
    if (data && data.length){
      this.model.foci = data.map((focus, i) => new FocusMeetingMappingDto(focus.id, 0));
    }
    else{
      this.model.foci = [];
    }
  }

  public get isModelValid(): boolean{
    let baseValidBit = this.model && this.validation.validate(this.model);
    // if we are creating then we do not need to run diff
    if (this.model?.id > 0){
      baseValidBit = baseValidBit && this.validation.isDiff(this.model, this.originalModel);
    }

    if(this.model?.id <= 0){
      baseValidBit = baseValidBit && this.imgFormData !== null;
    }

    return baseValidBit;
  }

  public get canEdit(): boolean{
    return this.model && this.model.hostId === this.authService.userValue.hostId;
  }

  onPersistClick(event: any): void{
    // console.log(this.model);
    if (this.isModelValid){
      this.confirmPersist();
    }
  }

  confirmPersist(): void{
    const content = { 
      contentText: 'Confirm you would like to save?', 
      affirmBtnText: 'Confirm', 
      cancelBtnText: 'Cancel', 
      titleText: 'Confirmation', 
      additionalText: null 
    };
    const confirmCallback: CallableFunction = () => {
      this.progressBarService.showProgressBar();
      this.persistOp();
    };
    this.notification.showDialogConfirmation(content, confirmCallback);
  }

  private consolidateModel(): void{
    var dt = moment(this.model.startDateUtc);
    dt = dt.subtract(this.model.timezone.utcOffset, 'hours');
    const dt_str = dt.format(this.formats.UTC_DATETIME_FORMAT);
    this.model.startDateUtc = dt_str; 
  }

  private persistOp(): void{

    const isUploadImg = this.imgFormData !== null;
    const isPut = this.model.id > 0;
    const isDiff = this.validation.isDiff(this.model, this.originalModel)

    if(isUploadImg){
      this.uploadImg();
    }
    else if(isPut && isDiff)
    {
      this.persistModelOp();
    }
    else{
      throw 'Invalid operation';
    }
  }

  private uploadImg(): void{

    this.imgService.uploadImg(this.imgFormData)
      .subscribe(resp => {
        this.model.imgId = resp.id;
        this.imgFormData = null;
        this.persistModelOp();
      });
  }

  persistModelOp(): void{
    console.log('persistOp', 'meeting', this.model);

    this.consolidateModel();

    // determine if this is a post or a put
    if (this.model.id > 0){
      // PUT
      this.meetingService.put(this.model)
        .subscribe(data => {
          // this.router.navigate(['/meeting/' + ], { queryParams: { returnUrl: state.url }});
          this.notification.showDialogMessage({
            contentText: 'Meeting successfully updated',
            affirmBtnText: null,
            cancelBtnText: 'Ok',
            titleText: 'Success',
            additionalText: null
          });
          this.progressBarService.hideProgressBarDelayed();
          this.mode = 'view';
        }, error => {
          this.notification.showDialogMessage({
            contentText: 'Meeting update failed' + (<HttpErrorResponse>error).error,
            affirmBtnText: null,
            cancelBtnText: 'Ok',
            titleText: 'Error',
            additionalText: null
          });
          this.progressBarService.hideProgressBarDelayed();
        });
    }
    else{
      // POST
      this.meetingService.post(this.model)
        .subscribe(data => {
          this.notification.showDialogMessage({
            contentText: 'Meeting successfully created',
            affirmBtnText: null,
            cancelBtnText: 'Ok',
            titleText: 'Success',
            additionalText: null
          }, () => {
            this.model = data;
            this.router.navigate(['/meeting/' + this.model.uniqueId]);
          });
          this.mode = 'view';
          this.progressBarService.hideProgressBarDelayed();
        }, error => {
          this.notification.showDialogMessage({
            contentText: 'Meeting creation failed - ' + (<HttpErrorResponse>error).error,
            affirmBtnText: null,
            cancelBtnText: 'Ok',
            titleText: 'Error',
            additionalText: null
          });
          this.progressBarService.hideProgressBarDelayed();
        });
    }
  }

  onRegistrationSuccessful(): void{
    console.log('meeting-detail', 'onRegistrationSuccessful');
    this.iniModel();
  }

  onDeregistrationSuccessful(): void{
    console.log('meeting-detail', 'onDeregistrationSuccessful');
    this.iniModel();
  }

  onRegistrationFailure(): void{
    console.log('meeting-detail', 'onRegistrationFailure');
  }

  onEditClick(event: Event): void{
    this.mode = 'edit';
  }
}
