import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MeetingService } from '../meeting.service';
import { MeetingQueryDto } from '../../_models/domain/meeting-query-dto';
import { AuthService } from '@app/_core/_services/auth.service';

@Component({
  selector: 'app-meeting-list',
  templateUrl: './meeting-list.component.html',
  styleUrls: ['./meeting-list.component.css']
})
export class MeetingListComponent implements OnInit{

  @Input() queries: MeetingQueryDto[];

  constructor(
    public authService: AuthService,
    private router: Router,
    private meetingService: MeetingService
  ) { }

  ngOnInit(): void {
    console.log('meeting-list', 'ngOnInit', 'queries', this.queries);
    this.processQueries();
  }

  private processQueries(): void{
    this.queries.forEach((v, i) => {
      v.data$ = this.meetingService.getFilteredSummaries(v.config);
      v.id = 'caro_' + i;
    });
  }

  private get isMeetingView(): boolean
  {
    return true;
  }

  get isAddButtonVisible(): boolean{
    return this.authService.isLoggedIn && 
      this.authService.isHost && 
      this.isMeetingView;
  }

  onAddLectureClick(event: Event): void{
    this.router.navigate(['/meeting/new'], { queryParamsHandling: 'merge' });
  }
}
