import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationDialogService } from '@app/_core/_services/notification-dialog.service';
import { AuthService } from '@core/_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginGroup: FormGroup;
  hidePwd: boolean = true;
  alphaRegEx: RegExp = /^[a-zA-Z]*$/;

  username: string = null;
  password: string = null;

  constructor(
    private formBuilder: FormBuilder, 
    private authService: AuthService,
    private notification: NotificationDialogService
  ) { }

  ngOnInit(): void {
    this.iniLoginGroup();
  }

  iniLoginGroup(): void{
    this.loginGroup = this.formBuilder.group({
      usernameCtrl: ['', [
        Validators.required,
        Validators.maxLength(50)]],
      pwdCtrl: ['', [
        Validators.required,
        Validators.maxLength(50)]]
    });
  }

  login(): void{
    if (this.loginGroup.valid){
      this.authService.login(
        this.username, 
        this.password, 
        (error: any) => {
          this.notification.showDialogMessage({
              contentText: (<HttpErrorResponse>error).error,
              affirmBtnText: null,
              cancelBtnText: 'Ok',
              titleText: 'Login Failure',
              additionalText: null
            });
        });
    }
  }
}
