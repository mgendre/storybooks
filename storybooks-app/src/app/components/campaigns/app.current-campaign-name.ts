import {Component, OnDestroy, OnInit} from '@angular/core';
import {CampaignsDatastore} from "../../datastores/CampaignsDatastore";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-current-campaign-name',
  template: '<span class="current-campaign-name">{{name}}</span>'
})
export class CurrentCampaignName implements OnInit, OnDestroy {

  private readonly subscriptions: Subscription[] = [];

  name: string = '';

  constructor(private readonly campaignsDatastore: CampaignsDatastore) {

  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.campaignsDatastore.selectedCampaign.subscribe(selected => {
        this.name = selected ? selected.name : '';
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }
}
