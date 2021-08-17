import {Component} from '@angular/core';
import {AuthenticationService} from 'src/app/services/AuthenticationService';
import {UserProfileDto} from "../../services/api.generated.clients";

@Component({
  selector: 'app-main-sidebar',
  templateUrl: './app.sidebar.html',
  styleUrls: ['./app.sidebar.scss']
})
export class MainSidebarComponent {
  user : UserProfileDto | null = null;

  constructor(private readonly authService: AuthenticationService) {
    authService.user.subscribe(user => {
      this.user = user;
    });
  }

  async logout(): Promise<void> {
    await this.authService.logout();
  }
}
