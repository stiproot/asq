import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { CardTypeDto } from '@app/_models/domain/card-type-dto';
import { FocusDto } from '@app/_models/domain/focus-dto';
import { TimezoneDto } from '@app/_models/domain/timezone-dto';
import { UserDto } from '@models/domain/user-dto';
import { MeetingQueryDto } from '@app/_models/domain/meeting-query-dto';
import { BlogPostQueryDto } from '@app/_models/domain/blog-post-query-dto';
import { HostQueryDto } from '@app/_models/domain/host-query-dto';
import { VideoQueryDto } from '@app/_models/domain/video-query-dto';
import * as moment from 'moment';
import { StorageKeys } from '@app/_core/_constants/storage-keys';
import { MeetingSummaryQueryBuilderConfigDto } from '@app/_models/domain/meeting-summary-query-builder-config-dto';
import { BlogSummaryQueryBuilderConfigDto } from '@app/_models/domain/blog-summary-query-builder-config-dto';
import { VideoSummaryQueryBuilderConfigDto } from '@app/_models/domain/video-summary-query-builder-config-dto';

class DataWithExpirationDto{
  public utc_time_added: moment.Moment;
  public data: any[] 
}

class StorageDuration{
  public seconds: number = 0;
  public minutes: number = 0;
  public hours: number = 0;
  public days: number = 0;
}

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  private storage: Storage;
  private storageDurationRules: { [key: string]: StorageDuration } = {};

  constructor(
    private keys: StorageKeys
  ) {
    this.storage = localStorage;
    this.storageDurationRules[this.keys.LECTURE_SUMMARY_BASE_QUERY_KEY] = { 
      seconds: 0, 
      minutes: environment.cache.expiration.meetingSummaries.min, 
      hours: 0, days: 0 
    };
    this.storageDurationRules[this.keys.BLOG_POST_SUMMARY_BASE_QUERY_KEY] = { 
      seconds: 0, 
      minutes: environment.cache.expiration.blogSummaries.min, 
      hours: 0, 
      days: 0 
    };
    this.storageDurationRules[this.keys.VIDEO_SUMMARY_BASE_QUERY_KEY] = { 
      seconds: 0, 
      minutes: environment.cache.expiration.videoSummaries.min, 
      hours: 0, 
      days: 0 
    };
    this.storageDurationRules[this.keys.HOST_SUMMARY_BASE_QUERY_KEY] = { 
      seconds: 0, 
      minutes: environment.cache.expiration.hostSummaries.min, 
      hours: 0, days: 0 
    };
  }

  private _serialize(obj: object): string{
    return JSON.stringify(obj);
  }

  private _deserialize<T>(obj: string): T{
    return JSON.parse(obj) as T;
  }

  private _get<T>(key: string): T{
    const val = this.storage.getItem(key);
    if (val === null){
      return null;
    }
    return this._deserialize<T>(val);
  }

  private _set(key: string, val: object): void{
    let v: string = null;
    if (val !== null){
      v = this._serialize(val);
    }
    this.storage.setItem(key, v);
  }

  private _remove(key: string): void{
    this.storage.removeItem(key);
  }

  private _item<T>(key: string, val?: object){
    if (val !== undefined && val !== null){
      // console.log('_item', 'val, not undefined and not null');
      // console.log('_item', 'attempting set');
      this._set(key, val);
      return null;
    }
    else if (val === null){
      // console.log('_item', 'val, null');
      // console.log('_item', 'attempting remove');
      this._remove(key);
      return null;
    }
    else{
      // console.log('_item', 'val, undefined');
      // console.log('_item', 'attempting get');
      return this._get<T>(key);
    }
  }

  private _storedDataIsValid(utcTimeAdded: moment.Moment, allowedDuration: StorageDuration): boolean{

    const now = moment.utc();

    const d = now.diff(utcTimeAdded, 'days');
    const h = now.diff(utcTimeAdded, 'hours');
    const m = now.diff(utcTimeAdded, 'minutes');
    const s = now.diff(utcTimeAdded, 'seconds');

    const valid = 
           d <= allowedDuration.days && 
           h <= allowedDuration.hours &&
           m <= allowedDuration.minutes &&
           s <= allowedDuration.seconds;

    console.log('utcTimeAdded', utcTimeAdded, 'allowedDuration', allowedDuration, 'now', now, '|', 'data is valid check', 'd', d, 'h', h, 'm', m, 's', s, 'valid', valid);

    return valid;
  }

  public user(v?: UserDto): UserDto{
    return this._item<UserDto>(this.keys.USER_KEY, v);
  }

  public foci(v?: FocusDto[]): FocusDto[]{
    return this._item<FocusDto[]>(this.keys.FOCI_KEY, v);
  }

  public timezones(v?: TimezoneDto[]): TimezoneDto[]{
    return this._item<TimezoneDto[]>(this.keys.TIMEZONE_KEY, v);
  }

  public cardTypes(v?: CardTypeDto[]): CardTypeDto[]{
    return this._item<CardTypeDto[]>(this.keys.CARDTYPE_KEY, v);
  }

  public lectureSummaryQueries(config: MeetingSummaryQueryBuilderConfigDto, v?: MeetingQueryDto[]): MeetingQueryDto[]{
    if(v === undefined){
      const stored = this._item<DataWithExpirationDto>(this.keys.LECTURE_SUMMARY_BASE_QUERY_KEY + config.generateCacheKey());
      console.log('stored', stored);
      if(stored === null) return null;
      else if(this._storedDataIsValid(moment(stored.utc_time_added), this.storageDurationRules[this.keys.LECTURE_SUMMARY_BASE_QUERY_KEY])){
        return <MeetingQueryDto[]>stored.data;
      }
      else return null;
    }
    else if(v !== null && v !== undefined){
      const newData: DataWithExpirationDto = { utc_time_added: moment.utc(), data: v };  
      this._item<DataWithExpirationDto>(this.keys.LECTURE_SUMMARY_BASE_QUERY_KEY + config.generateCacheKey(), newData);
      return;
    }
    else{
      this._item<DataWithExpirationDto>(this.keys.LECTURE_SUMMARY_BASE_QUERY_KEY + config.generateCacheKey(), null);
      return;
    } 
  }

  public blogPostSummaryQueries(config: BlogSummaryQueryBuilderConfigDto, v?: BlogPostQueryDto[]): BlogPostQueryDto[]{
    if(v === undefined){
      const stored = this._item<DataWithExpirationDto>(this.keys.BLOG_POST_SUMMARY_BASE_QUERY_KEY + config.generateCacheKey());
      console.log('blogPostSummaryQueries', stored);
      if(stored === null) return null;
      else if(this._storedDataIsValid(moment(stored.utc_time_added), this.storageDurationRules[this.keys.BLOG_POST_SUMMARY_BASE_QUERY_KEY])){
        return <BlogPostQueryDto[]>stored.data;
      }
      else return null;
    }
    else if(v !== null && v !== undefined){
      const newData: DataWithExpirationDto = { utc_time_added: moment.utc(), data: v };  
      this._item<DataWithExpirationDto>(this.keys.BLOG_POST_SUMMARY_BASE_QUERY_KEY + config.generateCacheKey(), newData);
      return;
    }
    else{
      this._item<DataWithExpirationDto>(this.keys.BLOG_POST_SUMMARY_BASE_QUERY_KEY + config.generateCacheKey(), null);
      return;
    } 
  }

  public videoSummaryQueries(config: VideoSummaryQueryBuilderConfigDto, v?: VideoQueryDto[]): VideoQueryDto[]{
    if(v === undefined){
      const stored = this._item<DataWithExpirationDto>(this.keys.VIDEO_SUMMARY_BASE_QUERY_KEY + config.generateCacheKey());
      console.log('videoSummaryQueries', stored);
      if(stored === null) return null;
      else if(this._storedDataIsValid(moment(stored.utc_time_added), this.storageDurationRules[this.keys.VIDEO_SUMMARY_BASE_QUERY_KEY])){
        return <VideoQueryDto[]>stored.data;
      }
      else return null;
    }
    else if(v !== null && v !== undefined){
      const newData: DataWithExpirationDto = { utc_time_added: moment.utc(), data: v };  
      this._item<DataWithExpirationDto>(this.keys.VIDEO_SUMMARY_BASE_QUERY_KEY + config.generateCacheKey(), newData);
      return;
    }
    else{
      this._item<DataWithExpirationDto>(this.keys.VIDEO_SUMMARY_BASE_QUERY_KEY + config.generateCacheKey(), null);
      return;
    } 
  }

  public hostSummaryQueries(v?: HostQueryDto[]): HostQueryDto[]{
    if(v === undefined){
      const stored = this._item<DataWithExpirationDto>(this.keys.HOST_SUMMARY_BASE_QUERY_KEY);
      console.log('hostSummaryQueries', 'stored', stored);
      if(stored === null) return null;
      else if(this._storedDataIsValid(moment(stored.utc_time_added), this.storageDurationRules[this.keys.HOST_SUMMARY_BASE_QUERY_KEY])){
        return <HostQueryDto[]>stored.data;
      }
      else return null;
    }
    else if(v !== null && v !== undefined){
      const newData: DataWithExpirationDto = { utc_time_added: moment.utc(), data: v };  
      this._item<DataWithExpirationDto>(this.keys.HOST_SUMMARY_BASE_QUERY_KEY, newData);
      return;
    }
    else{
      this._item<DataWithExpirationDto>(this.keys.HOST_SUMMARY_BASE_QUERY_KEY, null);
      return;
    } 
  }

  public clearAll(): void{
    this.user(null);
    this.foci(null);
    this.timezones(null);
    this.cardTypes(null);
  }

  public clearUserData(): void{
    this.user(null);
  }
}
