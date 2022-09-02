import { Injectable } from '@angular/core';
import { BlogPostDto } from './../../_models/domain/blog-post-dto';
import { DiffService } from './diff.service';

@Injectable({
  providedIn: 'root'
})
export class BlogPostValidationService {

  constructor(
    private diff: DiffService
  ) { }

  public validate(v: BlogPostDto, throwError: boolean = false): boolean{
      if (v.title != null && v.title.length
          && v.content != null && v.content.length
          //&& v.img != null
          && v. foci != null && v.foci.length){
              return true;
          }

      if (throwError){
          throw new Error('Invalid blog post structure');
      }
      else{
          return false;
      }
  }

  public isDiff(newObj: BlogPostDto, oldObj: BlogPostDto): boolean{
    return this.diff.isDiff(newObj, oldObj);
  }
}
