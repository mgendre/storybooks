import {Component, OnDestroy, OnInit} from '@angular/core';
import {CampaignListItemDto} from "../../services/api.generated.clients";
import {CampaignsDatastore} from "../../datastores/CampaignsDatastore";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-scenarios',
  templateUrl: './app.scenarios.html',
  styleUrls: ['./app.scenarios.scss']
})
export class ScenariosComponent implements OnInit, OnDestroy {

  readonly scenarios: CampaignListItemDto[] = [];
  ready = false;

  private readonly subscriptions: Subscription[] = [];

  newCampaignName: string = '';

  constructor(private readonly campaignsDatastore: CampaignsDatastore) {

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

  async createCampaign() {
    const campaign = await this.campaignsDatastore.create(this.newCampaignName);
    await this.campaignsDatastore.selectCampaign(campaign.id);
    this.newCampaignName = '';
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  async selectCampaign(campaign: CampaignListItemDto) {
    await this.campaignsDatastore.selectCampaign(campaign.id);
  }
}
