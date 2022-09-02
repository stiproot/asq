import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { VideoDetailComponent } from '@app/content/video-detail/video-detail.component';

@Injectable({
  providedIn: 'root'
})
export class VideoDetailDialogService{

  private timer: number = null;

  constructor(
    private dialog: MatDialog
  ) { }

  public openDialog(id: string): void{
    this.clearDialogTimer();
    this.dialog.open(VideoDetailComponent, {
      width: '80%',
      data: id
    });
  }

  public clearDialogTimer(): void{
    clearTimeout(this.timer);
  }

  public startDialogTimer(id: string): void{
    this.clearDialogTimer();
    this.timer = setTimeout(() => {
      this.openDialog(id);
    }, 2500);
  }
}