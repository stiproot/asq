import { Component, OnInit } from '@angular/core';
import { HostService } from '../host.service';
import { HostQueryDto } from '@app/_models/domain/host-query-dto';

@Component({
  selector: 'app-profile-list',
  templateUrl: './profile-list.component.html',
  styleUrls: ['./profile-list.component.css']
})
export class ProfileListComponent implements OnInit {

  queries: HostQueryDto[] = [];

  constructor(
    private hostService: HostService
  ) { }

  ngOnInit(): void {
    this.buildQueies();
  }

  private buildQueies(): void{
    this.hostService.buildSummaryQueries()
      .subscribe(resp => {
        this.queries = resp;
        this.processQueries();
      });
  }

  processQueries(): void{
    this.queries.forEach((v, i) => {
      this.hostService.getFilteredSummaries(v.config)
        .subscribe(resp => {
          v.id = 'caro_' + i;
          v.data = resp;
        });
    });
  }
}
