import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { HelperService } from '../../_core/_services/helper.service';
import { UserService } from '../../_core/_services/user.service';
import { FocusDto } from '@models/domain/focus-dto';
import { UserDto } from '@app/_models/domain/user-dto';
import { HostDto } from '@app/_models/domain/host-dto';
import { FocusUserMappingDto } from '@app/_models/domain/focus-user-mapping-dto';
import { FocusHostMappingDto } from '@app/_models/domain/focus-host-mapping-dto';
import { AccountTypeEnu } from '@app/_models/domain/account-type-enu';
import { Router } from '@angular/router';
import { TimezoneDto } from '@app/_models/domain/timezone-dto';
import { UserValidationService } from '@app/_core/_services/user-validation.service';
import { NotificationDialogService } from '@app/_core/_services/notification-dialog.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ProgressBarService } from '@app/_core/_services/progress-bar.service';
import { AuthService } from '@app/_core/_services/auth.service';
import { StorageService } from '@app/_core/_services/storage.service';
import { ImgService } from '@app/content/img.service';

@Component({
  selector: 'app-account',
  templateUrl: 'account.component.html',
  styleUrls: ['account.component.css'],
  providers: [{
    provide: STEPPER_GLOBAL_OPTIONS, useValue: {showError: true}
  }]
})
export class AccountComponent implements OnInit {

  basicInfoGroup: FormGroup;
  hostInfoGroup: FormGroup;
  //paymentInfoGroup: FormGroup;
  contractInfoGroup: FormGroup;

  alphaRegEx: RegExp = /^[a-zA-Z]*$/;
  numericRegEx: RegExp = /^[0-9]*$/;
  expDateRegEx: RegExp = /^\d{2}\/\d{2}$/;

  hidePwdText = true;
  hidePwdCtrls = false;
  //baseCardTypes: CardTypeDto[];
  model: UserDto;
  originalModel: UserDto;
  mode = 'create';
  selectedInterests: FocusDto[] = [];
  selectedSpecialities: FocusDto[] = [];
  imgFormData: FormData = null;

  get isEditOp(): boolean{
    return this.mode === 'edit';
  }

  get isCreateOp(): boolean{
    return this.mode === 'create';
  }

  get basicInfoSummaryError(): string{
    const invalidFields: string[] = [];
    if (this.basicInfoGroup.get('nameCtrl').invalid){
      invalidFields.push('Name');
    }
    if (this.basicInfoGroup.get('surnameCtrl').invalid){
      invalidFields.push('Surname');
    }
    if (this.basicInfoGroup.get('usernameCtrl').invalid){
      invalidFields.push('Username');
    }
    if (this.basicInfoGroup.get('emailCtrl').invalid){
      invalidFields.push('Email');
    }
    if (!this.hidePwdCtrls && this.basicInfoGroup.get('pwdCtrl').invalid){
      invalidFields.push('Password');
    }
    if (this.model?.interests == null || this.model?.interests?.length === 0){
      invalidFields.push('Interests');
    }
    if (invalidFields.length){
      return invalidFields.join(', ') + ' invalid';
    }
    return null;
  }

  get hostInfoSummaryError(): string{
    const invalidFields: string[] = [];
    if (this.hostInfoGroup.get('companyCtrl').invalid){
      invalidFields.push('Company');
    }
    if (this.hostInfoGroup.get('summaryCtrl').invalid){
      invalidFields.push('Career summary');
    }
    if (this.model?.host?.specialities == null || this.model?.host?.specialities?.length === 0){
      invalidFields.push('Specialities');
    }
    if (invalidFields.length){
      return invalidFields.join(', ') + ' invalid';
    }
    return null;
  }

  //get paymentInfoSummaryError(): string{
    //const invalidFields: string[] = [];
    //if (this.paymentInfoGroup.get('cardNoCtrl').invalid){
      //invalidFields.push('Card Number');
    //}
    //if (this.paymentInfoGroup.get('expDateCtrl').invalid){
      //invalidFields.push('Expiry Date');
    //}
    //if (this.paymentInfoGroup.get('cvcNoCtrl').invalid){
      //invalidFields.push('CVC');
    //}
    //if (invalidFields.length){
      //return invalidFields.join(', ') + ' invalid';
    //}
    //return null;
  //}

  get contractInfoSummaryError(): string{
    const invalidFields: string[] = [];
    if (this.contractInfoGroup.get('tncCtrl').invalid){
      invalidFields.push('Terms & Conditions');
    }
    if (invalidFields.length){
      return invalidFields.join(', ') + ' invalid';
    }
    return null;
  }

  constructor(
    private storageService: StorageService,
    private authService: AuthService,
    private router: Router,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private imgService: ImgService,
    private userValidation: UserValidationService,
    private notification: NotificationDialogService,
    private helper: HelperService,
    public progressBarService: ProgressBarService
    ) { }

  ngOnInit(): void {
    this.initUserModel();
    this.initBasicInfoGroup();
    this.initHostInfoGroup();
    //this.iniPaymentInfoGroup();
    this.initContractInfoGroup();
  }

  private initHostModel(): void{
    this.model.host = new HostDto();
  }

  private initUserModel(): void{

    // determine if we are creating or modifying
    if (this.authService.isLoggedIn){

      this.model = this.helper.clone<UserDto>(this.authService.userValue);
      this.model.isHost = this.model.accountType === AccountTypeEnu.HOST;
      this.model.isContractSigned = true;
      this.selectedInterests = this.model.interests.map(v => v.focus);

      if (this.model.isHost){
        this.selectedSpecialities = this.model.host.specialities.map(v => v.focus);
      }

      this.originalModel = this.helper.clone<UserDto>(this.model);
      this.mode = 'edit';
      this.hidePwdCtrls = true;
    }
    else{
      this.model = new UserDto();
      this.model.hostId = null;
      this.model.inactive = true;
    }
  }

  initBasicInfoGroup(): void{
    this.basicInfoGroup = this.formBuilder.group({
      accTypeCtrl: ['', Validators.required],
      nameCtrl: ['', [Validators.required, Validators.pattern(this.alphaRegEx), Validators.maxLength(25)]],
      surnameCtrl: ['', [Validators.required, Validators.pattern(this.alphaRegEx), Validators.maxLength(25)]],
      usernameCtrl: ['', [Validators.required, Validators.pattern(this.alphaRegEx), Validators.maxLength(25)]],
      emailCtrl: ['', [Validators.required, Validators.email]],
      pwdCtrl: ['', null]
      //pwdConfirmCtrl: ['', null],
    });

    if (this.isCreateOp){
      this.basicInfoGroup.controls.pwdCtrl.setValidators([Validators.required, Validators.maxLength(25)]);
      //this.basicInfoGroup.controls.pwdConfirmCtrl.setValidators([Validators.required, Validators.maxLength(25)]);
    }
    else if (this.isEditOp){
      this.basicInfoGroup.controls.pwdCtrl.setValidators([Validators.maxLength(25)]);
      //this.basicInfoGroup.controls.pwdConfirmCtrl.setValidators([Validators.maxLength(25)]);
    }
  }

  initHostInfoGroup(): void{
    this.hostInfoGroup = this.formBuilder.group({
      companyCtrl: ['', [Validators.required, Validators.maxLength(50)]],
      summaryCtrl: ['', [Validators.required, Validators.maxLength(500)]],
      //isConsultantCtrl: ['', [Validators.required]]
    });
  }

  //iniPaymentInfoGroup(): void{
    //this.paymentInfoGroup = this.formBuilder.group({
      //cardNoCtrl: ['', [Validators.required, Validators.pattern(this.numericRegEx), Validators.maxLength(12)]],
      //expDateCtrl: ['', [Validators.required, Validators.pattern(this.expDateRegEx), Validators.maxLength(5)]],
      //cvcNoCtrl: ['', [Validators.required, Validators.pattern(this.numericRegEx), Validators.maxLength(4)]],
      //cardTypeCtrl: ['', Validators.required]
    //});
  //}

  initContractInfoGroup(): void{
    this.contractInfoGroup = this.formBuilder.group({
      tncCtrl: [{value: '', disabled: this.isEditOp}, [Validators.required]],
    });
  }

  onInterestFocusListUpdate(data: FocusDto[]): void{

    var newArr: FocusUserMappingDto[] = [];

    if(this.model.interests === null){
      newArr = data.map(f => new FocusUserMappingDto(f.id, this.model.id));
    }
    else{
      data.forEach(element => {
        // Let's check if the mappings already exist for this user...
        const existingMapping = this.model.interests.filter((v, i) => v.focusId === element.id);
        if(existingMapping.length){
          newArr.push(existingMapping[0]);
        }
        else{
          newArr.push(new FocusUserMappingDto(element.id, this.model.id));
        }
      });
    }

    this.model.interests = newArr; 
    console.log('interests', this.model.interests);
  }

  onSpecialityFocusListUpdate(data: FocusDto[]): void{
    
    var newArr: FocusHostMappingDto[] = [];

    if(this.model.host.specialities === null){
      newArr = data.map(f => new FocusHostMappingDto(f.id, this.model.host.id));
    }
    else{
      data.forEach(element => {
        // Let's check if the mappings already exist for this user...
        const existingMapping = this.model.host.specialities.filter((v, i) => v.focusId === element.id);
        if(existingMapping.length){
          newArr.push(existingMapping[0]);
        }
        else{
          newArr.push(new FocusHostMappingDto(element.id, this.model.host.id));
        }
      });
    }

    this.model.host.specialities = newArr; 
    console.log('specialities', this.model.host.specialities);
  }

  onFileChanged(event: FormData): void{
    this.imgFormData = event;
  }

  onAccountTypeToggleChange(event: any): void{
    //console.log('toggle change', this.model.isHost);
    this.model.accountType = !this.model.isHost ? AccountTypeEnu.HOST : AccountTypeEnu.STUDENT;
    if (this.model.accountType === AccountTypeEnu.STUDENT){
      this.model.hostId = this.model.host = null;
    }
    else if (this.model.accountType === AccountTypeEnu.HOST){
      if (this.originalModel && this.originalModel !== null && this.originalModel.host !== null){
        this.model.host = this.originalModel.host;
        this.model.hostId = this.originalModel.hostId;
      }
      else{
        this.initHostModel();
      }
    }
  }

  onTimezoneSelectionChange(event: TimezoneDto): void{
    this.model.timezoneId = event.id;
  }

  public get isModelValid(): boolean{
    // the idea here is that the model will contain all the information necessary to determine persist validity
    // return this.model && this.userValidation.validate(this.model) && this.userValidation.isDiff(this.model, this.originalModel);
    // console.log('model', this.model, 'original_model', this.originalModel);
    let baseValidBit = this.model && this.userValidation.validate(this.model);

    if (this.isEditOp){
      baseValidBit = baseValidBit && this.userValidation.isDiff(this.model, this.originalModel);
    }

    if(this.model.id <= 0){
      baseValidBit = baseValidBit && this.imgFormData !== null;
    }

    return baseValidBit;
  }

  private persistOp(): void{

    const isUploadImg = this.imgFormData !== null;

    const isPut = this.model.id > 0;
    const isDiff = this.userValidation.isDiff(this.model, this.originalModel)

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

  private persistModelOp(): void{
    console.log('user', this.model);

    // determine if put or post
    if (this.model.id <= 0 && this.isCreateOp){
      this.userService.create(this.model)
        .subscribe(resp => {
          console.log('create user response', resp);
          if (resp){
            // redirect back to login page
            this.notification.showDialogMessage({
              contentText: 'Account successfully created',
              affirmBtnText: null,
              cancelBtnText: 'Ok',
              titleText: 'Please check your inbox to confirm your email address',
              additionalText: null
            }, () => {
              this.router.navigate(['/login'], {queryParamsHandling: 'merge'});
              this.progressBarService.hideProgressBarDelayed();
            });
          }
        }, error => {
          console.error('account', 'persistUser', 'error', error);
          this.notification.showDialogMessage({
            contentText: 'Unable to create user - ' + (<HttpErrorResponse>error).error,
            affirmBtnText: null,
            cancelBtnText: 'Ok',
            titleText: 'Error',
            additionalText: null
          });
          this.progressBarService.hideProgressBarDelayed();
        });
    }
    else{
      this.userService.update(this.model)
        .subscribe(resp => {
          this.userService.get(this.model.uniqueId)
            .subscribe(user => {
              user.token = this.authService.userValue.token;
              this.storageService.user(user);
              this.authService.setUser(user);
              this.initUserModel();
              this.progressBarService.hideProgressBarDelayed();
            });
          // redirect back to login page
          // this.router.navigate(['/login'], {queryParamsHandling: 'merge'});
        }, error => {
          this.progressBarService.hideProgressBarDelayed();
          console.error('account', 'persistOp', 'update', 'error', error);
          this.notification.showDialogMessage({
            contentText: 'Unable to update user - ' + (<HttpErrorResponse>error).error,
            affirmBtnText: null,
            cancelBtnText: 'Ok',
            titleText: 'Error',
            additionalText: null
          });
        });
    }
  }
  
  //expirationDateModelChange(): void{
    //if(this.model.paymentInfo.expirationDateModel.match(this.expDateRegEx)){
      //const split = this.model.paymentInfo.expirationDateModel.split('/');
      //const month = split[0];
      //const year = '20' + split[1];
      //const day = '01';
      //const m = moment(year + '-' + month + '-' + day, 'YYYY-MM-DD')
      ////console.log('expirationDateModel', );
      //this.model.paymentInfo.expirationDate = m;
    //}
  //}

  confirmPersist(): void{
    const content = {
      contentText: 'Everything is order, happy with all information captured?',
      affirmBtnText: 'Confirm',
      cancelBtnText: 'Cancel',
      titleText: 'Confirmation',
      additionalText: null
    };
    const callback: CallableFunction = () => {
      this.progressBarService.showProgressBar();
      this.persistOp();
    };

    this.notification.showDialogConfirmation(content, callback);
  }

  onPersistClicked(event: Event): void{
    if (this.isModelValid){
      this.confirmPersist();
    }
   }
}