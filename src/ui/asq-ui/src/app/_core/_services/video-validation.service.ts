import { Injectable } from '@angular/core';
import { VideoDto } from '../../_models/domain/video-dto';
import { DiffService } from './diff.service';

@Injectable({
  providedIn: 'root'
})
export class VideoValidationService {

  constructor(
    private diff: DiffService
  ) { }

  public validate(v: VideoDto, throwError: boolean = false): boolean{
      if (v.title != null && v.title.length
          && v.description != null && v.description.length
          //&& v.img != null
          //&& v.vid != null
          && v.creationUserId > 0
          && v.foci != null && v.foci.length){
              return true;
          }

      if (throwError){
          throw new Error('Invalid video structure');
      }
      else{
          return false;
      }
  }

  public isDiff(newObj: VideoDto, oldObj: VideoDto): boolean{
    return this.diff.isDiff(newObj, oldObj);
  }
}
