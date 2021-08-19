import {ScenarioDto} from "../../services/api.generated.clients";
import {Subscription} from "rxjs";
import {CampaignsDatastore} from "../../datastores/CampaignsDatastore";
import {Component, OnDestroy} from "@angular/core";


@Component({
  selector: 'app-scenarios',
  templateUrl: './app.scenarios.html',
  styleUrls: ['./app.scenarios.scss']
})
export class ScenariosComponent implements OnDestroy {

  scenarios: ScenarioDto[] = [];
  private readonly subscriptions: Subscription[] = [];

  constructor(private readonly campaignsDatastore: CampaignsDatastore) {
    this.subscriptions.push(this.campaignsDatastore.selectedCampaign.subscribe(c => {
      if (!c) {
        return;
      }
      this.scenarios = c.scenarios;
      // Order them by date desc...
      this.scenarios.sort((a,b) => {
        if (a.creationDate === b.creationDate) {
          return 0;
        }
        if (a.creationDate < b.creationDate) {
          return 1;
        }
        return 0;
      });
    }));
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    });
  }
}
