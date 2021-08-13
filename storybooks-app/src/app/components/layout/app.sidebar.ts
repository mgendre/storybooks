import {HttpClient} from '@angular/common/http';
import {OnInit} from '@angular/core';
import {Component} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {GoogleLoginProvider, SocialAuthService, SocialUser} from 'angularx-social-login';
import { AuthenticationService, User } from 'src/app/services/AuthenticationService';

@Component({
  selector: 'app-main-sidebar',
  templateUrl: './app.sidebar.html',
  styleUrls: ['./app.sidebar.scss']
})
export class MainSidebarComponent {
  user : User | null = null;

  constructor(private readonly authService: AuthenticationService) {
    authService.user.subscribe(user => {
      this.user = user;
    });
  }

  async logout(): Promise<void> {
    await this.authService.logout();
  }
}
