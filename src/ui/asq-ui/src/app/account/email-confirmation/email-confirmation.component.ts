import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '@app/_core/_services/user.service';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.css']
})
export class EmailConfirmationComponent implements OnInit {

  activationMessage: string = null;

  private get activationKey(): string | null{
    return this.route.snapshot.paramMap.get('activationKey');
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.attemptActivation();
  }

  attemptActivation(): void{
    console.log('activationKey', this.activationKey);
    this.userService.activate(this.activationKey)
      .subscribe(resp => {
        if (resp){
          this.activationMessage = 'Account successfully activated!';
          // this.router.navigate(['/login'], {queryParamsHandling: 'merge'});
        }
      }, err => {
        this.activationMessage = 'Account activation failed!';
      });
  }
}
