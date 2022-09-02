import { Injectable } from '@angular/core';
import { HostDto } from '@app/_models/domain/host-dto';

@Injectable({
  providedIn: 'root'
})
export class HostValidationService {

  constructor() { }

  public validate(v: HostDto, throwError: boolean = false): boolean{
      const valid = v.company && v.company.length > 0 && v.isConsultant !== null
          && v.careerSummary && v.careerSummary.length > 0
          && v.specialities && v.specialities.length > 0;
      if (valid){
          return true;
      }
      if (throwError){
          throw Error('Invalid host structure');
      }
      else{
          return false;
      }
  }
}
