import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BlogDetailComponent } from '../../../content/blog-detail/blog-detail.component';

@Injectable({
  providedIn: 'root'
})
export class BlogPostDialogService {

  private timer: number = null;

  constructor(
    private dialog: MatDialog
  ) { }

  public openDialog(id: string): void{
    this.clearDialogTimer();
    this.dialog.open(BlogDetailComponent, {
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
