import { Injectable } from '@angular/core';
import { ImgDto } from '@app/_models/domain/img-dto';

@Injectable({
  providedIn: 'root'
})
export class ImgValidationService {

  constructor() { }

  public validate(v: ImgDto, throwError: boolean = false): boolean{
      if (v.path != null && v.fileName != null){
          return true;
      }
      if (throwError){
          throw new Error('Invalid img structure');
      }
      return false;
  }
}
