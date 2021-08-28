import {Component, Input} from "@angular/core";
import {MediaDto, MediaStorageType} from "../../services/api.generated.clients";


@Component({
  selector: 'app-media-renderer',
  templateUrl: './app.media-renderer.html',
  styleUrls: ['./app.media-renderer.scss']
})
export class MediaRendererComponent {

  private _media: MediaDto | null = null;

  imgUrl = '';

  @Input()
  class: any;

  get media() {
    return this._media;
  }

  @Input()
  set media(media: MediaDto | null) {
    this._media = media;
    this.createUrl();
  }

  private createUrl() {
    if (this._media?.storageType === MediaStorageType.Document) {
      this.imgUrl = `/api/campaigns/${this.media?.campaignId}/media/${this.media?.id}/download`;
    } else {
      this.imgUrl = this._media?.externalUri ?? '';
    }
  }
}
