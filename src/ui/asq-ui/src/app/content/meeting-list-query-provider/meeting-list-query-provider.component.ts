import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { MeetingService } from '../meeting.service';
import { MeetingQueryDto } from '../../_models/domain/meeting-query-dto';
//import { ActivatedRoute } from '@angular/router';
import { MeetingSummaryQueryBuilderConfigDto } from '@app/_models/domain/meeting-summary-query-builder-config-dto';

@Component({
  selector: 'app-meeting-list-query-provider',
  templateUrl: './meeting-list-query-provider.component.html',
  styleUrls: ['./meeting-list-query-provider.component.css']
})
export class MeetingListQueryProviderComponent implements OnInit {

  @Input() inputUserUniqueId: string | null = null;
  private config: MeetingSummaryQueryBuilderConfigDto = new MeetingSummaryQueryBuilderConfigDto();

  // models
  queries$: Observable<MeetingQueryDto[]>;

  constructor(
    private meetingService: MeetingService
    //private route: ActivatedRoute
  ) { }

  ngOnInit(): void {

    this.config.creationUserUniqueId = this.inputUserUniqueId;
    //this.config.meetingStatusId = this.meetingStatusId;

    this.buildQueries();
  }

  private buildQueries(): void{
    this.queries$ = this.meetingService.buildSummaryQueries(this.config);
  }
}
