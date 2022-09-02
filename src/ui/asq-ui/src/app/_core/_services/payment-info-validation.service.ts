import { Injectable } from '@angular/core';
import { PaymentInfoDto } from '@app/_models/domain/payment-info-dto';

@Injectable({
  providedIn: 'root'
})
export class PaymentInfoValidationService {

  constructor() { }

  public validate(v: PaymentInfoDto, throwError: boolean = false): boolean{
      const valid: boolean = v.cardTypeId != null && v.cardTypeId > 0
          && v.cardNumber != null && v.cardNumber.length > 0
          && v.cvc != null && v.cvc.length > 0
          && v.expirationDate != null;
      if (valid){
          return true;
      }
      if (throwError){
          throw new Error('Invalid payment info structure');
      }
      else{
          return false;
      }
  }
}
