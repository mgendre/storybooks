import {HasInitialization} from "../services/HasInitialization";
import {BehaviorSubject} from "rxjs";
import {CampaignApiClient, CampaignListItemDto, CampaignUpdateDto} from "../services/api.generated.clients";
import {Injectable} from "@angular/core";
import {AuthenticationService} from "../services/AuthenticationService";

@Injectable({
  providedIn: 'root',
})
export class CampaignsDatastore implements HasInitialization {
  private readonly _ready = new BehaviorSubject(false);
  ready = this._ready.asObservable();

  private readonly _campaigns = new BehaviorSubject<CampaignListItemDto[]>([]);
  public readonly campaigns = this._campaigns.asObservable();

  constructor(private readonly campaignsApiClient: CampaignApiClient,
              private readonly authenticationService: AuthenticationService) {
    this.authenticationService.user.subscribe(async (user) => {
      if (user) {
        await this.reload();
      } else {
        this._campaigns.next([]);
      }
    });
  }

  public async reload(): Promise<CampaignListItemDto[]> {
    const campaigns = await this.campaignsApiClient.listAll().toPromise();
    this._campaigns.next(campaigns);
    if (!this._ready.value) {
      this._ready.next(true);
    }
    return campaigns;
  }

  public async create(name: string): Promise<void> {
    const campaign = new CampaignUpdateDto();
    campaign.name = name;
    await this.campaignsApiClient.create(campaign).toPromise();
    await this.reload();
  }
}
