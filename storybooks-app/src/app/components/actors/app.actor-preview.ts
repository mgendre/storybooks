import {Component, OnDestroy} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {ActorsDatastore} from "../../datastores/ActorDatastore";
import {Subscription} from "rxjs";
import {AbstractActorDto} from "../../services/api.generated.clients";


@Component({
  selector: 'app-actor-preview',
  templateUrl: './app.actor-preview.html',
  styleUrls: ['./app.actor-preview.scss']
})
export class ActorPreviewComponent implements OnDestroy {

  private subscriptions: Subscription[] = [];

  actor: AbstractActorDto | null = null;

  constructor(private readonly actorsDatastore: ActorsDatastore,
              private router: Router,
              private route: ActivatedRoute) {
    this.subscriptions.push(this.actorsDatastore.actorSelectedForPreview.subscribe((actor: AbstractActorDto) => {
      this.actor = actor;
    }));
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }
}
