import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateTimeFormatConstants{

  public readonly UTC_DATETIME_FORMAT: string = 'YYYY-MM-DDTHH:mm:ss[Z]';
}