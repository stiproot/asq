import { Component, OnInit, Input, Inject, Optional } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPostService } from './../blog-post.service';
import { NotificationDialogService } from '../../_core/_services/notification-dialog.service';
import { HelperService } from './../../_core/_services/helper.service';
import { BlogPostValidationService } from './../../_core/_services/blog-post-validation.service';
import { AuthService } from './../../_core/_services/auth.service';
import { BlogPostDto } from './../../_models/domain/blog-post-dto';
import { FocusDto } from '@app/_models/domain/focus-dto';
import { FocusBlogPostMappingDto } from '@app/_models/domain/focus-blog-post-mapping-dto';
import { RegexConstants } from '@app/_core/_constants/regex-constants';
import { ProgressBarService } from '@app/_core/_services/progress-bar.service';
import { ImgService } from '../img.service';

@Component({
  selector: 'app-blog-detail',
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.css']
})
export class BlogDetailComponent implements OnInit {

  // inputs
  @Input() inputId: string | null;

  public blogPostGroup: FormGroup;
  readonly fileReader: FileReader = new FileReader();

  // models
  model: BlogPostDto;
  originalModel: BlogPostDto;
  imgFormData: FormData = null;

  // focus list to choose from
  public baseFocusList: FocusDto[];
  public selectedFocusList: FocusDto[] = [];
  // crud mode
  mode = 'view';

  private get id(): string | null{
    // query param
    if(this.router.url.match(this.regex.BLOG_POST_URL_REGEX)){
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
      throw Error('No blog id provided');
    }
  }

  get isDialog(): boolean{
    //return this.router.url.match(this.regex.BLOG_POST_URL_REGEX) === null;
    return this.injectedId !== null;
  }

  get view(): boolean{
    return this.mode === 'view';
  }

  get edit(): boolean{
    return this.mode === 'edit';
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private regex: RegexConstants,
    private blogPostService: BlogPostService,
    private imgService: ImgService,
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private notification: NotificationDialogService,
    private validation: BlogPostValidationService,
    private helper: HelperService,
    public progressBarService: ProgressBarService,
    @Optional() @Inject(MAT_DIALOG_DATA) public injectedId: string | null
  ) { }

  ngOnInit(): void {
    this.iniModel();
    this.iniBlogPostGroup();
  }

  private iniModel(): void{
    if (this.id === 'new'){
      this.model = new BlogPostDto();
      this.model.creationUserId = this.authService.userValue.id;
      // set user information & defaults
      this.mode = 'edit';
    }
    else{
      this.blogPostService.get(this.id)
        .subscribe((data: BlogPostDto) => {
          this.model = data;
          this.originalModel = this.helper.clone<BlogPostDto>(this.model);
          this.selectedFocusList = this.model.foci.map(v => v.focus);
        });
    }
  }

  iniBlogPostGroup(): void{
    this.blogPostGroup = this.formBuilder.group({
      titleCtrl: ['', [Validators.required, Validators.maxLength(50)]],
      contentCtrl: ['', [Validators.required, Validators.maxLength(500)]]
    });
  }

  onFileChanged(event: FormData): void{
    this.imgFormData = event;
  }

  onFocusListUpdate(data: FocusDto[]): void{
    // build up mappings
    if (data && data.length){
      this.model.foci = data.map((focus, i) => new FocusBlogPostMappingDto(focus.id, 0));
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
    return this.model && this.model.creationUserId === this.authService.userValue.id;
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
    // determine if this is a post or a put
    if (this.model.id > 0){
      // put
      this.blogPostService.put(this.model)
        .subscribe(data => {
          console.log(data);
          this.notification.showDialogMessage({
            contentText: 'Blog post successfully updated',
            affirmBtnText: null,
            cancelBtnText: 'Ok',
            titleText: 'Success',
            additionalText: null
          });
          this.mode = 'view';
          this.progressBarService.hideProgressBarDelayed();
          // since we have updated a new post, we need to clear local blog post data so that we reolad with newly updated blog post
          //this.authService.clearBlogPostData();
        }, error => {
          this.notification.showDialogMessage({
            contentText: 'Blog post update failed - ' + (<HttpErrorResponse>error).error,
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
      this.blogPostService.post(this.model)
        .subscribe(data => {
          console.log(data);
          this.notification.showDialogMessage({
            contentText: 'Blog post successfully created',
            affirmBtnText: null,
            cancelBtnText: 'Ok',
            titleText: 'Success',
            additionalText: null
          }, () => {
            this.model = data;
            this.router.navigate(['/blog-post/' + this.model.uniqueId]);
            this.progressBarService.hideProgressBarDelayed();
          });
          this.mode = 'view';
          // since we have added a new post, we need to clear local blog post data so that we reolad with newly addded blog post
          //this.authService.clearBlogPostData();
        }, error => {
          this.notification.showDialogMessage({
            contentText: 'Blog post creation failed - ' + (<HttpErrorResponse>error).error,
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