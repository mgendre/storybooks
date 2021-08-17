import {HttpClient} from '@angular/common/http';
import {Component, OnInit} from '@angular/core';
import {UserProfileDto} from './services/api.generated.clients';
import {AuthenticationService} from './services/AuthenticationService';
import {ServiceInitializer} from "./services/ServiceInitializer";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  loggedIn = false;
  title = 'storybooks-app';
  user: UserProfileDto | null = null;
  ready = false;

  constructor(
    private readonly http: HttpClient,
    private readonly authService: AuthenticationService,
    private readonly serviceInitializer: ServiceInitializer
  ) {
    this.authService.user.subscribe(user => {
      this.user = user;
      this.loggedIn = !!user;
    });
  }

  async ngOnInit(): Promise<void> {
    this.serviceInitializer.ready.subscribe(ready => this.ready = ready);
    await this.serviceInitializer.initialize();
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
}
