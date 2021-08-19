import {HasInitialization} from "../services/HasInitialization";
import {BehaviorSubject} from "rxjs";
import {
  CampaignApiClient,
  CampaignDto,
  CampaignListItemDto,
  CampaignUpdateDto
} from "../services/api.generated.clients";
import {Injectable} from "@angular/core";
import {AuthenticationService} from "../services/AuthenticationService";
import {LocalStorageService} from "ngx-webstorage";
import {NGXLogger} from "ngx-logger";

@Injectable({
  providedIn: 'root',
})
export class CampaignsDatastore implements HasInitialization {
  private readonly _ready = new BehaviorSubject(false);
  ready = this._ready.asObservable();

  private readonly _campaigns = new BehaviorSubject<CampaignListItemDto[]>([]);
  public readonly campaigns = this._campaigns.asObservable();

  private readonly _selectedCampaign = new BehaviorSubject<CampaignDto | null>(null);
  public readonly selectedCampaign = this._selectedCampaign.asObservable();

  constructor(private readonly campaignsApiClient: CampaignApiClient,
              private readonly authenticationService: AuthenticationService,
              private readonly storage: LocalStorageService,
              private readonly logger: NGXLogger) {
    this.authenticationService.user.subscribe(async (user) => {
      if (user) {
        await this.reload();
      } else {
        this._campaigns.next([]);
      }
    });
  }

  public async reload(): Promise<CampaignListItemDto[]> {

    const campaignId = this.storage.retrieve("selected_campaign");
    if (campaignId) {
      try {
        await this.selectCampaign(campaignId);
      } catch(e) {
        this.logger.warn('Could not load stored campaign id');
      }
    }

    const campaigns = await this.campaignsApiClient.listAll().toPromise();
    this._campaigns.next(campaigns);
    if (!this._ready.value) {
      this._ready.next(true);
    }
    return campaigns;
  }

  public async create(name: string): Promise<CampaignDto> {
    const campaign = new CampaignUpdateDto();
    campaign.name = name;
    const created = await this.campaignsApiClient.create(campaign).toPromise();
    await this.reload();
    return created;
  }

  public async selectCampaign(campaignId: string) {
    if (campaignId !== this._selectedCampaign.value?.id) {
      const campaign = await this.campaignsApiClient.get(campaignId).toPromise();
      this._selectedCampaign.next(campaign);
      this.storage.store("selected_campaign", campaignId);
    }
  }
}
