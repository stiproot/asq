import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { NgxMatMomentAdapter, NgxMatMomentModule } from '@angular-material-components/moment-adapter';
import { MatModule } from '../shared/mat.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { NgxMatDateAdapter, NgxMatDateFormats, NgxMatDatetimePickerModule, NgxMatTimepickerModule, NGX_MAT_DATE_FORMATS } from '@angular-material-components/datetime-picker';
import { SharedComponentModule } from '@app/components/shared-component.module';
import { MeetingDetailComponent} from './meeting-detail/meeting-detail.component';
import { MeetingSummaryComponent } from './meeting-summary/meeting-summary.component';
import { MeetingListComponent } from './meeting-list/meeting-list.component';
import { MeetingPanelComponent } from './meeting-panel/meeting-panel.component';
import { MeetingListQueryProviderComponent } from './meeting-list-query-provider/meeting-list-query-provider.component';
import { MeetingSearchRootComponent } from './meeting-search-root/meeting-search-root.component';
import { MeetingSearchResultsComponent } from './meeting-search-results/meeting-search-results.component';
import { ProfileDetailComponent } from './profile-detail/profile-detail.component';
import { BlogDetailComponent } from './blog-detail/blog-detail.component';
import { BlogListComponent } from './blog-list/blog-list.component';
import { BlogPanelComponent } from './blog-panel/blog-panel.component';
import { BlogListQueryProviderComponent } from './blog-list-query-provider/blog-list-query-provider.component';
import { ProfileListComponent } from './profile-list/profile-list.component';
import { ProfilePanelComponent } from './profile-panel/profile-panel.component';
import { SearchCriteriaComponent } from './search-criteria/search-criteria.component';
import { AutoSizeInputModule } from 'ngx-autosize-input';
import { LayoutPlaceholderComponent } from './layout-placeholder/layout-placeholder.component';
import { BlogSummaryComponent } from './blog-summary/blog-summary.component';
import { TempComponent } from './temp/temp.component';
import { VideoDetailComponent } from './video-detail/video-detail.component';
import { VideoSummaryComponent } from './video-summary/video-summary.component';
import { VideoPanelComponent } from './video-panel/video-panel.component';
import { VideoListComponent } from './video-list/video-list.component';
import { VideoListQueryProviderComponent } from './video-list-query-provider/video-list-query-provider.component';
import { ProfileSummaryComponent } from './profile-summary/profile-summary.component';
import { VideoSearchRootComponent } from './video-search-root/video-search-root.component';
import { ProfileSearchRootComponent } from './profile-search-root/profile-search-root.component';
import { VideoSearchResultsComponent } from './video-search-results/video-search-results.component';
import { ProfileSearchResultsComponent } from './profile-search-results/profile-search-results.component';
import { BlogSearchResultsComponent } from './blog-search-results/blog-search-results.component';
import { BlogSearchRootComponent } from './blog-search-root/blog-search-root.component';

const CUSTOM_MOMENT_FORMATS: NgxMatDateFormats = {
  parse: {
    dateInput: "l, LTS"
  },
  display: {
    dateInput: "l, LTS",
    monthYearLabel: "MMM YYYY",
    dateA11yLabel: "LL",
    monthYearA11yLabel: "MMMM YYYY"
  }
};

@NgModule({
  declarations: [
    MeetingListComponent,
    MeetingDetailComponent,
    MeetingSummaryComponent,
    MeetingPanelComponent,
    MeetingListQueryProviderComponent,
    MeetingSearchRootComponent,
    MeetingSearchResultsComponent,
    ProfileDetailComponent,
    BlogDetailComponent,
    BlogListComponent,
    BlogPanelComponent,
    BlogListQueryProviderComponent,
    ProfileListComponent,
    ProfilePanelComponent,
    SearchCriteriaComponent,
    LayoutPlaceholderComponent,
    BlogSummaryComponent,
    TempComponent,
    VideoDetailComponent,
    VideoSummaryComponent,
    VideoPanelComponent,
    VideoListComponent,
    VideoListQueryProviderComponent,
    ProfileSummaryComponent,
    VideoSearchRootComponent,
    ProfileSearchRootComponent,
    VideoSearchResultsComponent,
    ProfileSearchResultsComponent,
    BlogSearchResultsComponent,
    BlogSearchRootComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatModule,
    FlexLayoutModule,
    RouterModule,
    CarouselModule,
    NgxMatMomentModule,
    NgxMatTimepickerModule,
    NgxMatDatetimePickerModule,
    SharedComponentModule,
    AutoSizeInputModule
  ],
  providers: [
    { provide: NGX_MAT_DATE_FORMATS, useValue: CUSTOM_MOMENT_FORMATS },
    { provide: NgxMatDateAdapter, useClass: NgxMatMomentAdapter }
  ]
})
export class ContentModule { }
