import {HttpClient} from '@angular/common/http';
import {Component} from '@angular/core';
import {GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'storybooks-app';

  user: SocialUser | null = null;

  constructor(
    private http: HttpClient,
    private authService: SocialAuthService
  ) {

    this.authService.authState.subscribe((user: SocialUser) => {
      console.log(user);
      localStorage.setItem('jwt_token', user.idToken);
      this.user = user;
    });
  }

  login(): void {
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID).then((x: any) => console.log(x));
  }

  logout(): void {
    this.authService.signOut();
    localStorage.setItem('jwt_token', '');
  }

  public async getCampaigns() {
    await this.http.get('http://localhost:4200/api/campaigns').toPromise();
  }
}
