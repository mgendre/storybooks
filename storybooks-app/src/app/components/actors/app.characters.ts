import {AbstractActorDto, CharacterDto} from "../../services/api.generated.clients";
import {Subscription} from "rxjs";
import {Component, OnDestroy} from "@angular/core";
import {Router} from "@angular/router";
import {ConfirmationService} from "primeng/api";
import {TranslateService} from "@ngx-translate/core";
import {ActorsDatastore} from "../../datastores/ActorDatastore";


@Component({
  selector: 'app-characters',
  templateUrl: './app.characters.html',
  styleUrls: ['./app.characters.scss']
})
export class CharactersComponent implements OnDestroy {

  actors: CharacterItem[] = [];

  private readonly subscriptions: Subscription[] = [];

  constructor(private readonly actorsDatastore: ActorsDatastore,
              private readonly router: Router,
              private readonly confirmationService: ConfirmationService,
              private readonly translate: TranslateService) {
    this.subscriptions.push(this.actorsDatastore.characters.subscribe(actors => {
      this.actors = [];
      if (!actors) {
        return;
      }

      // Order them by date desc...
      actors.sort((a,b) => {
        if (a.name === b.name) {
          return 0;
        }
        if (a.name < b.name) {
          return -1;
        }
        return 1;
      });

      this.actors = [];
      actors.forEach(a => {
        this.actors.push(new CharacterItem(a));
      });
    }));
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    });
  }

  async edit(actor: AbstractActorDto) {
    await this.router.navigate(['characters/edit/' + actor.id]);
  }

  confirmDelete(actor: AbstractActorDto) {
    this.confirmationService.confirm({
      message: this.translate.instant(
        'characters.list.delete.confirm.message',
        {name: actor.name}),
      header: this.translate.instant('characters.list.delete.confirm.title',
        {name: actor.name}),
      icon: 'bi bi-exclamation',
      accept: async () => {
        await this.actorsDatastore.deleteCharacter(actor.id);
      },
      reject: () => {
      }
    });
  }

  async create() {
    await this.router.navigate(['characters/new']);
  }
}

class CharacterItem {
  constructor(
    public actor: CharacterDto,
    public expanded = false
  ) {
  }
}
