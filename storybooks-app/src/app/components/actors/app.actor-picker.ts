import {AbstractActorDto, CharacterDto} from "../../services/api.generated.clients";
import {Subscription} from "rxjs";
import {Component, EventEmitter, OnDestroy, Output} from "@angular/core";
import {ActorsDatastore} from "../../datastores/ActorDatastore";


@Component({
  selector: 'app-actor-picker',
  templateUrl: './app.actor-picker.html',
  styleUrls: ['./app.actor-picker.scss']
})
export class ActorPickerComponent implements OnDestroy {

  characters: CharacterDto[] = [];

  @Output()
  public actorPicked = new EventEmitter<AbstractActorDto>();

  private readonly subscriptions: Subscription[] = [];

  dialogOpened = false;

  constructor(private readonly actorsDatastore: ActorsDatastore) {
    this.subscriptions.push(this.actorsDatastore.characters.subscribe(data => {
      this.characters = data;
    }));
  }

  public pickCharacter() {
    this.dialogOpened = true;
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    });
  }

  close() {
    this.dialogOpened = false;
  }

  picked($event: AbstractActorDto) {
    console.log($event);
  }
}
