import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule} from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ToastrModule, ToastrService } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserService } from './user.service';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { AuthInterceptor } from './authinterceptor';
import { UpdateProfileComponent } from './user-profile/update-profile/update-profile.component';

import { FriendsListComponent } from './friend-list/friend-list.component';
import { FriendSearchComponent } from './friend-search/friend-search.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { AuthGuard } from './authguard';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { ChatComponent } from './chat/chat/chat.component';
import { FriendProfileComponent } from './friend-profile/friend-profile.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    
    UserProfileComponent,
    LoginComponent,

    RegistrationComponent,
    UpdateProfileComponent,

    FriendsListComponent,
    FriendSearchComponent,
    ForbiddenComponent,
    AdminPanelComponent,
    ChatComponent,
    FriendProfileComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate:[AuthGuard]},      

      { path: 'user-profile', component: UserProfileComponent },
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent},
      { path: 'update-profile', component: UpdateProfileComponent},
      { path: 'friends', component: FriendsListComponent, canActivate:[AuthGuard]},
      { path: 'search', component: FriendSearchComponent, canActivate:[AuthGuard]},
      { path: 'friend/profile', component: FriendProfileComponent, canActivate:[AuthGuard]},
      { path: 'registration', component: RegistrationComponent},
      { path: 'login', component: LoginComponent},
      { path: 'forbidden',component:ForbiddenComponent},
      { path: 'chat',component:ChatComponent, canActivate:[AuthGuard]},
      { path: 'admin',component:AdminPanelComponent, canActivate:[AuthGuard],data :{permittedRoles:['Administrator']}},
      { path: '**', component: HomeComponent, pathMatch: 'full', canActivate:[AuthGuard]}
    ])
  ],
  providers: [
    UserService, 
    { provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true },
    ToastrService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
