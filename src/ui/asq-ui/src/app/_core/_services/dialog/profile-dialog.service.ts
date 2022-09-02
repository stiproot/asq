import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProfileDetailComponent } from '../../../content/profile-detail/profile-detail.component';

@Injectable({
  providedIn: 'root'
})
export class ProfileDialogService {

  private timer: number = null;

  constructor(
    private dialog: MatDialog
  ) { }

  public openDialog(id: string){
    this.clearDialogTimer();
    this.dialog.open(ProfileDetailComponent, {
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
