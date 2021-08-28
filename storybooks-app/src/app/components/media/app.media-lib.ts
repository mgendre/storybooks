import {Subscription} from "rxjs";
import {Component, OnDestroy} from "@angular/core";


@Component({
  selector: 'app-media-lib',
  templateUrl: './app.media-lib.html',
  styleUrls: ['./app.media-lib.scss']
})
export class MediaLibComponent implements OnDestroy {

  private readonly subscriptions: Subscription[] = [];

  constructor() {

  }

  ngOnDestroy(): void {
  }
}
