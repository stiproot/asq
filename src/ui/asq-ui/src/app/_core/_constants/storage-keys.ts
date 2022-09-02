import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageKeys{

  public readonly USER_KEY: string = "__user_key__";
  public readonly FOCI_KEY: string = "__foci_key__";
  public readonly TIMEZONE_KEY: string = "__timezones_key__";
  public readonly CARDTYPE_KEY: string = "__cardtypes_key__";
  public readonly LECTURE_SUMMARY_BASE_QUERY_KEY: string = "__lecture_summary_base_query_key__";
  public readonly BLOG_POST_SUMMARY_BASE_QUERY_KEY: string = "__blog_post_summary_base_query_key__";
  public readonly VIDEO_SUMMARY_BASE_QUERY_KEY: string = "__video_summary_base_query_key__";
  public readonly HOST_SUMMARY_BASE_QUERY_KEY: string = "__host_summary_base_query_key__";
}