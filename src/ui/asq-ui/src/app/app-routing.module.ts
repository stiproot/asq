import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '@app/_core/_guards/auth.guard';
import { MeetingListQueryProviderComponent } from './content/meeting-list-query-provider/meeting-list-query-provider.component';
import { MeetingDetailComponent } from './content/meeting-detail/meeting-detail.component';
import { BlogDetailComponent } from './content/blog-detail/blog-detail.component';
import { MeetingGatewayComponent } from './meeting/meeting-gateway/meeting-gateway.component';
import { ProfileDetailComponent } from './content/profile-detail/profile-detail.component';
import { BlogListQueryProviderComponent } from './content/blog-list-query-provider/blog-list-query-provider.component';
//import { MeetingStatusEnum } from '@app/_models/domain/meeting-status-enum';
import { ProfileListComponent } from './content/profile-list/profile-list.component';
import { MeetingSearchRootComponent } from './content/meeting-search-root/meeting-search-root.component';
//import { LayoutPlaceholderComponent } from './content/layout-placeholder/layout-placeholder.component';
//import { TempComponent } from './content/temp/temp.component';
//import { VideoSelectComponent } from './components/video-select/video-select.component';
import { VideoListQueryProviderComponent } from './content/video-list-query-provider/video-list-query-provider.component';
import { VideoDetailComponent } from './content/video-detail/video-detail.component';
import { BlogSearchRootComponent } from './content/blog-search-root/blog-search-root.component';
import { VideoSearchRootComponent } from './content/video-search-root/video-search-root.component';
import { ProfileSearchRootComponent } from './content/profile-search-root/profile-search-root.component';

const routes: Routes = [
  {
    //path: 'meetings', component: MeetingListQueryProviderComponent, data: { meetingStatusId: MeetingStatusEnum.AWAITING }
    path: 'meetings', component: MeetingListQueryProviderComponent
  },
  {
    path: 'meeting/:id', component: MeetingDetailComponent, canActivate: [AuthGuard]
  },
  {
    path: 'meeting-search', component: MeetingSearchRootComponent
  },
  {
    path: 'meeting-gateway/:id', component: MeetingGatewayComponent, canActivate: [AuthGuard]
  },
  //{
    //path: 'temp', component: TempComponent
  //},
  {
    path: 'blog-posts', component: BlogListQueryProviderComponent
  },
  {
    path: 'blog-post/:id', component: BlogDetailComponent, canActivate: [AuthGuard]
  },
  {
    path: 'blog-post-search', component: BlogSearchRootComponent
  },
  //{
    //path: 'layout-placeholder', component: LayoutPlaceholderComponent
  //},
  {
    path: 'videos', component: VideoListQueryProviderComponent
  },
  {
    path: 'video/:id', component: VideoDetailComponent, canActivate: [AuthGuard]
  },
  {
    path: 'video-search', component: VideoSearchRootComponent
  },
  {
    path: 'hosts', component: ProfileListComponent
  },
  {
    path: 'profile/:id', component: ProfileDetailComponent, canActivate: [AuthGuard]
  },
  {
    path: 'profile-search', component: ProfileSearchRootComponent
  },
  {
    path: '', redirectTo: '/videos', pathMatch: 'full',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
