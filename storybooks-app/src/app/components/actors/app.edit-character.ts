import {CharacterDto, CharacterUpdateDto, MediaDto} from "../../services/api.generated.clients";
import {Subscription} from "rxjs";
import {Component, OnDestroy, OnInit} from "@angular/core";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActorsDatastore} from "../../datastores/ActorDatastore";


@Component({
  selector: 'app-edit-character',
  templateUrl: './app.edit-character.html',
  styleUrls: ['./app.edit-character.scss']
})
export class EditCharacterComponent implements OnInit, OnDestroy {

  _actor: CharacterDto = new CharacterDto();
  set actor(actor: CharacterDto) {
    this._actor = actor;
    this.actorForm.setValue({
      'name': actor.name ?? ''
    });
  }
  get actor() {
    return this._actor;
  }


  actorForm = new FormGroup({
    name: new FormControl('', [
      Validators.required,
    ])
  });

  private readonly subscriptions: Subscription[] = [];

  private portraitMediaId: string | null = null;

  constructor(private readonly actorsDatastore: ActorsDatastore,
              private router: Router,
              private route: ActivatedRoute) {
  }

  async save() {
    const toUpdate = new CharacterUpdateDto();
    toUpdate.name = this.actorForm.get('name')?.value ?? '';
    toUpdate.descriptionMarkdown = this.actor.descriptionMarkdown;
    toUpdate.portraitMediaId = this.portraitMediaId;
    await this.actorsDatastore.saveCharacter(toUpdate, this.actor.id);
    await this.close();
  }

  async close() {
    await this.router.navigate(['/characters']);
  }

  ngOnInit(): void {
    let id : string | null = null;
    this.subscriptions.push(this.route.params.subscribe((params: Params) => {
      id = params['characterId'];

      if (!this.actorsDatastore.isReady()) {
        return;
      }

      if (id) {
        this.actor = this.actorsDatastore.getCharacter(id);
        this.portraitMediaId = this.actor.portraitMediaId ?? null;
      } else {
        this.actor = new CharacterDto();
      }
    }));

    this.subscriptions.push(this.actorsDatastore.ready.subscribe(r => {
      if (r && id) {
        this.actor = this.actorsDatastore.getCharacter(id);
      }
    }));
  }

  mediaPicked(media: MediaDto) {
    this.portraitMediaId = media.id;
    this._actor.portraitMediaId = media.id;
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    });
  }
}
