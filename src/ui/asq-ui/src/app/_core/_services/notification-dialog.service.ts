import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../components/confirm-dialog/confirm-dialog.component';
import { MessageDialogComponent } from '../../components/message-dialog/message-dialog.component';
import { DialogData } from '../../_models/dialog-data';

@Injectable({
  providedIn: 'root'
})
export class NotificationDialogService {

  constructor(
    public dialog: MatDialog,
  ) { }

  public showDialogMessage(content: DialogData, callback: CallableFunction = null): void{
    this.dialog.open(MessageDialogComponent, {
      width: '250px',
      data: content
    }).afterClosed()
      .subscribe(result => {
        if(callback){
          callback();
        }
      });
  }

  public showDialogConfirmation(content: DialogData, callback: CallableFunction): void{
    this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: content
    }).afterClosed()
      .subscribe(result => {
        if (Boolean(result)){
          callback();
        }
      });
  }
}
