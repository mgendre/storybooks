import {HasInitialization} from "../services/HasInitialization";
import {BehaviorSubject} from "rxjs";
import {DocumentLibApiClient, FileParameter, MediaDto} from "../services/api.generated.clients";
import {Injectable} from "@angular/core";
import {CampaignsDatastore} from "./CampaignsDatastore";

@Injectable({
  providedIn: 'root',
})
export class MediaDatastore implements HasInitialization {
  private readonly _ready = new BehaviorSubject(false);
  ready = this._ready.asObservable();

  private readonly _mediaItems = new BehaviorSubject<MediaDto[]>([]);
  public readonly mediaItems = this._mediaItems.asObservable();

  constructor(private readonly campaignsDatastore: CampaignsDatastore,
              private readonly mediaLibClient: DocumentLibApiClient) {
    this.campaignsDatastore.selectedCampaign.subscribe(async (campaign) => {
      await this.reload(campaign?.id);
    });
  }

  private async reload(campaignId: string | undefined): Promise<MediaDto[]> {
    if (campaignId) {
      const mediaItems = await this.mediaLibClient.list(campaignId).toPromise();
      this._mediaItems.next(mediaItems);
      this._ready.next(true);
      return mediaItems;
    } else {
      this._mediaItems.next([]);
      return [];
    }
  }

  public async upload(file: File, label: string | null) {
    const campaignId = this.campaignsDatastore.selectedCampaignValue.id;

    const fileParam : FileParameter = {
      data: file,
      fileName: file.name
    };
    const published = await this.mediaLibClient.uploadAndCreate(campaignId, [fileParam], label).toPromise();
    await this.reload(campaignId);
    return published;
  }

  public findMedia(mediaId: string) : MediaDto | null {
    return this._mediaItems.value.find(m => m.id === mediaId) ?? null;
  }

  isReady(): boolean {
    return this._ready.value;
  }


}

