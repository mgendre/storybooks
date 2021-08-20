import {HasInitialization} from "../services/HasInitialization";
import {BehaviorSubject} from "rxjs";
import {CampaignApiClient, ScenarioDto} from "../services/api.generated.clients";
import {Injectable} from "@angular/core";
import {CampaignsDatastore} from "./CampaignsDatastore";

@Injectable({
  providedIn: 'root',
})
export class ScenariosDatastore implements HasInitialization {
  private readonly _ready = new BehaviorSubject(false);
  ready = this._ready.asObservable();

  private readonly _scenarios = new BehaviorSubject<ScenarioDto[]>([]);
  public readonly scenarios = this._scenarios.asObservable();

  constructor(private readonly campaignsApiClient: CampaignApiClient,
              private readonly campaignsDatastore: CampaignsDatastore) {
    this.campaignsDatastore.selectedCampaign.subscribe(async (campaign) => {
      await this.reload(campaign?.id);
    });
  }

  private async reload(campaignId: string | undefined): Promise<ScenarioDto[]> {
    if (campaignId) {
      const scenarios = await this.campaignsApiClient.listScenarios(campaignId).toPromise();
      this._scenarios.next(scenarios);
      return scenarios;
    } else {
      this._scenarios.next([]);
      return [];
    }
  }

  public getScenario(id: string) {
    const scenario = this._scenarios.value.find(s => s.id === id);
    if (!scenario) {
      throw new Error('Could not find scenario with id ' + id);
    }
    return scenario;
  }
}
