import {HttpClient} from '@angular/common/http';
import {Component, OnInit} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import { UserProfileService } from './services/api.generated.clients';
import {AuthenticationService, User} from './services/AuthenticationService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  loggedIn = false;
  title = 'storybooks-app';
  user: User | null = null;
  ready = false;

  constructor(
    private readonly http: HttpClient,
    private readonly authService: AuthenticationService,
    private readonly translate: TranslateService,
    private readonly userProfileService: UserProfileService
  ) {
    this.authService.user.subscribe(user => {
      this.user = user;
      this.loggedIn = !!user;
    });
    this.authService
  }

  async ngOnInit(): Promise<void> {
    try {
      await this.translate.use('fr').toPromise();
    } finally {
      this.ready = true;
    }
  }

  async login(): Promise<void> {
    await this.authService.login();
  }

  async logout(): Promise<void> {
    await this.authService.logout();
  }

  public async getCampaigns() {
    await this.http.get('http://localhost:4200/api/campaigns').toPromise();
  }

  public test() {
    alert('TESTING...');
    this.userProfileService.ensureCreated().toPromise();
  }
}
