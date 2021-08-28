import {Subscription} from "rxjs";
import {Component, OnDestroy} from "@angular/core";
import {MediaDatastore} from "../../datastores/MediaDatastore";
import {MediaDto} from "../../services/api.generated.clients";


@Component({
  selector: 'app-media-lib',
  templateUrl: './app.media-lib.html',
  styleUrls: ['./app.media-lib.scss']
})
export class MediaLibComponent implements OnDestroy {

  mediaItems: MediaDto[] = [];

  private readonly subscriptions: Subscription[] = [];

  constructor(private readonly mediaDatastore: MediaDatastore) {
    this.subscriptions.push(this.mediaDatastore.mediaItems.subscribe(items => {
      this.mediaItems = items;
    }));
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    });
  }
}
