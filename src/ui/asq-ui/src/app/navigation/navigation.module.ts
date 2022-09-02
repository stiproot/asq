import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatModule } from './../shared/mat.module';
import { NotificationModule } from '../notification/notification.module';
import { HeaderComponent } from './header/header.component';
import { LayoutComponent } from './layout/layout.component';
import { SidenavListComponent } from './sidenav-list/sidenav-list.component';

@NgModule({
  declarations: [
    HeaderComponent,
    LayoutComponent,
    SidenavListComponent
  ],
  imports: [
    FormsModule, 
    ReactiveFormsModule,
    CommonModule,
    RouterModule,
    FlexLayoutModule,
    MatModule,
    NotificationModule
  ],
  exports: [
    HeaderComponent,
    LayoutComponent,
    SidenavListComponent
  ]
})
export class NavigationModule { }
