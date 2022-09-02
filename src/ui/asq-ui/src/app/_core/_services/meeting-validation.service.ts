import { Injectable } from '@angular/core';
import { MeetingDto } from './../../_models/domain/meeting-dto';
import { DiffService } from './diff.service';

@Injectable({
  providedIn: 'root'
})
export class MeetingValidationService {

  constructor(
    private diff: DiffService
  ) { }

  public validate(v: MeetingDto, throwError: boolean = false): boolean{
      if (v.title != null && v.title.length
          && v.description != null && v.description.length
          && v.startDateUtc != null
          && v.estimatedDuration != null && v.estimatedDuration > 0
          //&& v.img != null
          && v.timezoneId > 0
          && v.hostId > 0
          && v.creationUserId > 0
          && v.foci != null && v.foci.length){
              return true;
          }

      if (throwError){
          throw new Error('Invalid meeting structure');
      }
      else{
          return false;
      }
  }

  public isDiff(newObj: MeetingDto, oldObj: MeetingDto): boolean{
    return this.diff.isDiff(newObj, oldObj);
  }
}
