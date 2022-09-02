import { Inject, Component, OnInit, Input, Optional } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { VideoService } from '../video.service';
import { VidService } from '../vid.service';
import { ImgService } from '../img.service';
import { NotificationDialogService } from './../../_core/_services/notification-dialog.service';
import { HelperService } from './../../_core/_services/helper.service';
import { VideoValidationService } from '../../_core/_services/video-validation.service';
import { AuthService } from './../../_core/_services/auth.service';
import { RegexConstants } from './../../_core/_constants/regex-constants';
import { FocusDto } from './../../_models/domain/focus-dto';
import { FocusVideoMappingDto } from './../../_models/domain/focus-video-mapping-dto';
import { VideoDto } from './../../_models/domain/video-dto';
import { ImgDto } from './../../_models/domain/img-dto';
import { HttpErrorResponse } from '@angular/common/http';
import { ProgressBarService } from './../../_core/_services/progress-bar.service';
import { VidDto } from '@app/_models/domain/vid-dto';

@Component({
  selector: 'app-video-detail',
  templateUrl: './video-detail.component.html',
  styleUrls: ['./video-detail.component.css']
})
export class VideoDetailComponent implements OnInit {

  // inputs
  @Input() inputId: string | null;

  public videoInfoGroup: FormGroup;
  readonly fileReader: FileReader = new FileReader();

  // models
  model: VideoDto;
  originalModel: VideoDto;
  file: File;
  imgPreviewUrl: any;
  private vidFormData: FormData = null;
  private imgFormData: FormData = null;

  // focus list to choose from
  public baseFocusList: FocusDto[];
  public selectedFocusList: FocusDto[] = [];

  private get id(): string | null{
    // query param
    if(this.router.url.match(this.regex.VIDEO_URL_REGEX)){
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

  // crud mode
  mode = 'view';

  get view(): boolean{
    return this.mode === 'view';
  }

  get edit(): boolean{
    return this.mode === 'edit';
  }

  get isDialog(): boolean{
    return this.injectedId !== null;
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private regex: RegexConstants,
    private videoService: VideoService,
    private vidService: VidService,
    private imgService: ImgService,
    private authService: AuthService,
    private notification: NotificationDialogService,
    private validation: VideoValidationService,
    private helper: HelperService,
    public progressBarService: ProgressBarService,
    // The following is necessary if meeting-detail is going to be opened in a dialog...
    @Optional() @Inject(MAT_DIALOG_DATA) private injectedId: string | null
  ) { }

  ngOnInit(): void {
    this.iniModel();
    this.iniVideoInfoGroup();
  }

  private iniModel(): void{
    if (this.id === 'new'){
      this.model = new VideoDto();
      // set user information & defaults
      this.model.creationUserId = this.authService.userValue.id;
      this.mode = 'edit';
    }
    else{
      this.videoService.get(this.id)
        .subscribe((data: VideoDto) => {
          console.log('video-detail', 'video', data);
          this.model = data;
          this.originalModel = this.helper.clone<VideoDto>(this.model);
          this.selectedFocusList = this.model.foci.map(v => v.focus);
        });
    }
  }

  iniVideoInfoGroup(): void{
    this.videoInfoGroup = this.formBuilder.group({
      titleCtrl: ['', [Validators.required, Validators.maxLength(50)]],
      descCtrl: ['', [Validators.required, Validators.maxLength(500)]]
    });
  }

  onVideoFileChanged(event: FormData): void{
    this.vidFormData = event;
  }

  onImgFileChanged(event: FormData): void{
    this.imgFormData = event;
  }

  onFocusListUpdate(data: FocusDto[]): void{
    // build up mappings
    if (data && data.length){
      this.model.foci = data.map((focus, i) => new FocusVideoMappingDto(focus.id, 0));
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

    if(this.edit && this.model.id <= 0){
      baseValidBit = baseValidBit && this.vidFormData !== null && this.imgFormData !== null;
    }

    return baseValidBit;
  }

  public get canEdit(): boolean{
    return this.model && this.model.creationUserId === this.authService.userValue.id;
  }

  onPersistClick(event: any): void{
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

  private persistOp(): void{

    const isUploadVid = this.vidFormData !== null;
    const isUploadImg = this.imgFormData !== null;

    const isPut = this.model.id > 0;
    const isDiff = this.validation.isDiff(this.model, this.originalModel)

    if(isUploadVid || isUploadImg){
      this.uploadFiles();
    }
    else if(isPut && isDiff)
    {
      this.persistModelOp();
    }
    else{
      throw 'Invalid operation';
    }
  }

  private uploadFiles(): void{

    var d = {};

    if(this.imgFormData != null){
      d['img'] = this.imgService.uploadImg(this.imgFormData);
    }

    if(this.vidFormData != null){
      d['vid'] = this.vidService.uploadVid(this.vidFormData);
    }

    forkJoin(d)
      .subscribe(resp => {
        console.log('forkJoin', 'resp', resp);
        this.model.imgId = (resp['img'] as ImgDto).id;
        this.model.vidId = (resp['vid'] as VidDto).id;
        this.imgFormData = null;
        this.vidFormData = null;
        this.persistModelOp();
      });
  }

  persistModelOp(): void{
    console.log('persistOp', 'video', this.model);

    // determine if this is a post or a put
    if (this.model.id > 0){
      // put
      this.videoService.put(this.model)
        .subscribe(data => {
          // this.router.navigate(['/meeting/' + ], { queryParams: { returnUrl: state.url }});
          this.notification.showDialogMessage({
            contentText: 'Video successfully updated',
            affirmBtnText: null,
            cancelBtnText: 'Ok',
            titleText: 'Success',
            additionalText: null
          });
          this.progressBarService.hideProgressBarDelayed();
          this.mode = 'view';
        }, error => {
          this.notification.showDialogMessage({
            contentText: 'Video update failed' + (<HttpErrorResponse>error).error,
            affirmBtnText: null,
            cancelBtnText: 'Ok',
            titleText: 'Error',
            additionalText: null
          });
          this.progressBarService.hideProgressBarDelayed();
        });
    }
    else{
      // post
      this.videoService.post(this.model)
        .subscribe(data => {
          this.notification.showDialogMessage({
            contentText: 'Video successfully created',
            affirmBtnText: null,
            cancelBtnText: 'Ok',
            titleText: 'Success',
            additionalText: null
          }, () => {
            this.model = data;
            this.router.navigate(['/video/' + this.model.uniqueId]);
          });
          this.mode = 'view';
          this.progressBarService.hideProgressBarDelayed();
        }, error => {
          this.notification.showDialogMessage({
            contentText: 'Video creation failed - ' + (<HttpErrorResponse>error).error,
            affirmBtnText: null,
            cancelBtnText: 'Ok',
            titleText: 'Error',
            additionalText: null
          });
          this.progressBarService.hideProgressBarDelayed();
        });
    }
  }

  onEditClick(event: Event): void{
    this.mode = 'edit';
  }
}
