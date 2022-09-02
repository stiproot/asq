import { Injectable } from '@angular/core';
import { AccountTypeEnu } from '@app/_models/domain/account-type-enu';
import { UserDto } from '@app/_models/domain/user-dto';
import { HostValidationService } from './host-validation.service';
import { ImgValidationService } from './img-validation.service';
//import { PaymentInfoValidationService } from './payment-info-validation.service';
import { DiffService } from './diff.service';

@Injectable({
  providedIn: 'root'
})
export class UserValidationService {

  constructor(
    //private paymentInfoValidation: PaymentInfoValidationService,
    private imgValidation: ImgValidationService,
    private hostValidation: HostValidationService,
    private diff: DiffService
  ) { }

  public validate(v: UserDto, throwError: boolean = false): boolean{
      const valid: boolean = v.username != null && v.username.length > 0
          && v.accountType != null
          && v.password != null && v.password.length > 0
          && v.name != null && v.name.length > 0
          && v.surname != null && v.surname.length > 0
          //&& v.paymentInfo != null && this.paymentInfoValidation.validate(v.paymentInfo, throwError)
          && (v.accountType === AccountTypeEnu.HOST ? v.host != null && this.hostValidation.validate(v.host, throwError) : true)
          && v.img != null && this.imgValidation.validate(v.img, throwError)
          && v.timezoneId > 0
          && v.interests != null && v.interests.length > 0
          && v.isContractSigned;

      if (valid){
          return true;
      }

      if (throwError){
          throw new Error('Invalid meeting structure');
      }
      else{
          return false;
      }
  }

  public isDiff(newObj: UserDto, oldObj: UserDto): boolean{
    return this.diff.isDiff(newObj, oldObj);
  }
}
