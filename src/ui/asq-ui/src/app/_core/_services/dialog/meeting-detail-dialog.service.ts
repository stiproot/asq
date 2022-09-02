import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MeetingDetailComponent } from '@app/content/meeting-detail/meeting-detail.component';

@Injectable({
  providedIn: 'root'
})
export class MeetingDetailDialogService{

  private timer: number = null;

  constructor(
    private dialog: MatDialog
  ) { }

  public openDialog(id: string): void{
    this.clearDialogTimer();
    this.dialog.open(MeetingDetailComponent, {
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