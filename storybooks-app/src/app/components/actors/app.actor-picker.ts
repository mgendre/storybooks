import {AbstractActorDto, CharacterDto, CharacterUpdateDto} from "../../services/api.generated.clients";
import {Subscription} from "rxjs";
import {Component, EventEmitter, OnDestroy, Output, ViewChild} from "@angular/core";
import {ActorsDatastore} from "../../datastores/ActorDatastore";
import {AutoComplete} from "primeng/autocomplete";


@Component({
  selector: 'app-actor-picker',
  templateUrl: './app.actor-picker.html',
  styleUrls: ['./app.actor-picker.scss']
})
export class ActorPickerComponent implements OnDestroy {

  lastCharacterInputValue = '';
  filteredCharacters: CharacterDto[] = [];
  selectedCharacter: CharacterDto | null = null;

  @ViewChild("characterAutoComplete")
  private characterAutoComplete!: AutoComplete;

  @Output()
  public actorPicked = new EventEmitter<AbstractActorDto>();

  private readonly subscriptions: Subscription[] = [];

  dialogOpened = false;

  constructor(private readonly actorsDatastore: ActorsDatastore) {
    this.subscriptions.push(this.actorsDatastore.characters.subscribe(() => {
      this.doFilterCharacters();
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
    this.selectedCharacter = null;
    this.lastCharacterInputValue = '';
  }

  characterPicked(character: AbstractActorDto) {
    this.close();
    this.actorPicked.emit(character);
  }

  filterCharacters(event: any) {
    this.lastCharacterInputValue = event.query;
    return this.doFilterCharacters();
  }

  doFilterCharacters() {
    const filtered: CharacterDto[] = [];
    this.actorsDatastore.allCharacters().forEach(c => {
      if (c.name.toLowerCase().indexOf(this.lastCharacterInputValue.toLowerCase()) >= 0) {
        filtered.push(c);
      }
    });
    return this.filteredCharacters = filtered;
  }

  async createNewCharacter() {
    const dto = new CharacterUpdateDto();
    dto.name = this.lastCharacterInputValue;
    const character = await this.actorsDatastore.saveCharacter(dto);
    this.close();
    this.actorPicked.emit(character);
  }
}
