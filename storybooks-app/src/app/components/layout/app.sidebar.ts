import {Component, OnDestroy} from '@angular/core';
import {AuthenticationService} from 'src/app/services/AuthenticationService';
import {CampaignDto, UserProfileDto} from "../../services/api.generated.clients";
import {CampaignsDatastore} from "../../datastores/CampaignsDatastore";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-main-sidebar',
  templateUrl: './app.sidebar.html',
  styleUrls: ['./app.sidebar.scss']
})
export class MainSidebarComponent implements OnDestroy {

  user : UserProfileDto | null = null;
  selectedCampaign: CampaignDto | null = null;

  private readonly subscriptions: Subscription[] = [];

  constructor(private readonly authService: AuthenticationService,
              private readonly campaignsDatastore: CampaignsDatastore) {
    this.subscriptions.push(authService.user.subscribe(user => {
      this.user = user;
    }));
    this.subscriptions.push(campaignsDatastore.selectedCampaign.subscribe(campaign => {
      this.selectedCampaign = campaign;
    }));
  }

  async logout(): Promise<void> {
    await this.authService.logout();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    });
  }
}
