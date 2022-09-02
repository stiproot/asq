import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProgressBarService {

  public progressBarVisible: boolean = false;

  constructor() { }

  public showProgressBar(): void{
    this.progressBarVisible = true;
  } 

  public hideProgressBar(): void{
    this.progressBarVisible = false;
  }

  public toggleProgressBar(): void{
    this.progressBarVisible = !this.progressBarVisible;
  }

  public hideAfterXSec(sec: number): void{
    setTimeout(() => { this.hideProgressBar(); }, sec * 1000);
  }

  public hideProgressBarDelayed(): void{
    this.hideAfterXSec(2);
  }
}
