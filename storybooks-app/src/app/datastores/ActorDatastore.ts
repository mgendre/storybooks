import {HasInitialization} from "../services/HasInitialization";
import {BehaviorSubject} from "rxjs";
import {CharacterApiClient, CharacterDto, CharacterUpdateDto} from "../services/api.generated.clients";
import {Injectable} from "@angular/core";
import {CampaignsDatastore} from "./CampaignsDatastore";

@Injectable({
  providedIn: 'root',
})
export class ActorsDatastore implements HasInitialization {
  private readonly _ready = new BehaviorSubject(false);
  ready = this._ready.asObservable();

  private readonly _characters = new BehaviorSubject<CharacterDto[]>([]);
  public readonly characters = this._characters.asObservable();

  constructor(private readonly charactersApiClient: CharacterApiClient,
              private readonly campaignsDatastore: CampaignsDatastore) {
    this.campaignsDatastore.selectedCampaign.subscribe(async (campaign) => {
      if (campaign?.id) {
        await this.reload(campaign?.id);
      }
    });
  }

  private async reload(campaignId: string | undefined): Promise<void> {
    const promises: Promise<void>[] = [];

    promises.push(this.reloadCharacters(campaignId));

    await Promise.all(promises);
    this._ready.next(true);
  }

  private async reloadCharacters(campaignId: string | undefined): Promise<void> {
    if (campaignId) {
      const actors = await this.charactersApiClient.findAll(campaignId).toPromise();
      this._characters.next(actors);
    } else {
      this._characters.next([]);
    }
  }

  isReady(): boolean {
    return this._ready.value;
  }

  public async saveCharacter(actor: CharacterUpdateDto, actorId: string | null = null) {
    const campaignId = this.campaignsDatastore.selectedCampaignValue.id;

    if (actorId) {
      await this.charactersApiClient.update(campaignId, actorId, actor).toPromise();
    } else {
      await this.charactersApiClient.create(campaignId, actor).toPromise();
    }

    await this.reloadCharacters(campaignId);
  }

  async deleteCharacter(id: string) {
    const campaignId = this.campaignsDatastore.selectedCampaignValue.id;
    await this.charactersApiClient.delete(campaignId, id).toPromise();
    await this.reloadCharacters(campaignId);
  }

  public getCharacter(id: string) {
    const actor = this._characters.value.find(a => a.id === id);
    if (!actor) {
      throw new Error('Could not find character with id ' + id);
    }
    return actor;
  }
}
