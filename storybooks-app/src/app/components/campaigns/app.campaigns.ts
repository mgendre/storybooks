import {Component, OnDestroy, OnInit} from '@angular/core';
import {CampaignApiClient, CampaignListItemDto, UserProfileDto} from "../../services/api.generated.clients";
import {CampaignsDatastore} from "../../datastores/CampaignsDatastore";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-campaigns',
  templateUrl: './app.campaigns.html',
  styleUrls: ['./app.campaigns.scss']
})
export class CampaignsComponent implements OnInit, OnDestroy {

  readonly campaigns: CampaignListItemDto[] = [];
  ready = false;

  private readonly subscriptions: Subscription[] = [];

  constructor(readonly campaignsDatastore: CampaignsDatastore) {

  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.campaignsDatastore.campaigns.subscribe(campaigns => {
        this.campaigns.splice(0, this.campaigns.length);
        campaigns.forEach(c => this.campaigns.push(c));
      })
    );

    this.subscriptions.push(
      this.campaignsDatastore.ready.subscribe(r => {
        this.ready = r;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

}
