import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RegexConstants{
  public ALPHA_REGEX: RegExp = /^[a-zA-Z]*$/;
  public NUMERIC_REGEX: RegExp = /^[0-9]*$/;
  //public MEETING_URL_REGEX: RegExp = /^\/meeting\/([1-9]\d*|new)$/; 
  public MEETING_URL_REGEX: RegExp = /^\/meeting\/([{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?|new)$/; 
  //public PROFILE_URL_REGEX: RegExp = /^\/profile\/[1-9]\d*$/; 
  public PROFILE_URL_REGEX: RegExp = /^\/profile\/[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$/; 
  //public BLOG_POST_URL_REGEX: RegExp = /^\/blog-post\/[1-9]\d*$/; 
  public BLOG_POST_URL_REGEX: RegExp = /^\/blog-post\/([{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?|new)$/; 
  public VIDEO_URL_REGEX: RegExp = /^\/video\/([{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?|new)$/; 
  public ID_URL_REGEX: RegExp = /(?<=\/)[1-9]\d*$/;
}