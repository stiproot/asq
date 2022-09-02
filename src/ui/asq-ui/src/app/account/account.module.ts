import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatModule } from '../shared/mat.module';
import { AccountRoutingModule } from './account-routing.module';
import { SharedComponentModule } from '@app/components/shared-component.module';
import { AccountComponent } from './account/account.component';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';

@NgModule({
  declarations: [
    AccountComponent,
    EmailConfirmationComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MatModule,
    AccountRoutingModule,
    SharedComponentModule
  ]
})
export class AccountModule { }
