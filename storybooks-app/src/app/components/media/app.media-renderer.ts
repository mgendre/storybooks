import {Component, Input, OnDestroy} from "@angular/core";
import {MediaDto, MediaStorageType} from "../../services/api.generated.clients";
import {MediaDatastore} from "../../datastores/MediaDatastore";
import {Subscription} from "rxjs";


@Component({
  selector: 'app-media-renderer',
  templateUrl: './app.media-renderer.html',
  styleUrls: ['./app.media-renderer.scss']
})
export class MediaRendererComponent implements OnDestroy {

  imgUrl = '';

  @Input()
  class: any;

  private _mediaToRender: MediaDto | null = null;

  private _media: MediaDto | null = null;
  get media() {
    return this._media;
  }
  @Input()
  set media(media: MediaDto | null) {
    this._media = media;
    this._mediaToRender = media;
    this.createUrl();
  }

  private _mediaId: string | null | undefined;
  get mediaId() {
    return this._media?.id ?? this._mediaId;
  }
  @Input()
  set mediaId(mediaId: string | null | undefined) {
    this._mediaId = mediaId;
    this.reloadMedia();
  }

  subscriptions: Subscription[] = [];

  constructor(private readonly mediaDatastore: MediaDatastore) {
    this.mediaDatastore.mediaItems.subscribe(() => {
      this.reloadMedia();
    });
  }

  private createUrl() {
    if (this._mediaToRender?.storageType === MediaStorageType.Document) {
      this.imgUrl = `/api/campaigns/${this._mediaToRender?.campaignId}/media/${this._mediaToRender?.id}/download`;
    } else {
      this.imgUrl = this._mediaToRender?.externalUri ?? '';
    }
  }

  private reloadMedia() {
    if (this._media) {
      return; // Don't update if the component is directly used with media elements
    }
    if (this._mediaId) {
      this._mediaToRender = this.mediaDatastore.findMedia(this._mediaId);
      this.createUrl();
    } else {
      this._mediaToRender = null;
    }
  }

  ngOnDestroy() {
  }
}
