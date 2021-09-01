import {Injectable} from "@angular/core";
import {ActorsDatastore} from "../../../datastores/ActorDatastore";
import {AbstractActorDto, CharacterDto} from "../../../services/api.generated.clients";

@Injectable({
  providedIn: 'root',
})
export class MarkdownService {
  constructor(private readonly actorsDatastore: ActorsDatastore) {
  }

  private static getType(actor: AbstractActorDto) {
    if(actor instanceof CharacterDto) {
      return 'character';
    }
    return 'actor';
  }

  public actor2Key(actor: AbstractActorDto): string {
    return `[${actor.name}][${MarkdownService.getType(actor)}/${actor.id}]`
  }

  public actor2Link(actor: AbstractActorDto): string {
    const type = MarkdownService.getType(actor);
    return `[${type}/${actor.id}]: ${type}/${actor.id}`;
  }

  public getAllActorLinks(): string {
    const links: string[] = [];
    this.actorsDatastore.allActors().forEach(a => {
      links.push(this.actor2Link(a))
    });
    return links.join('\n');
  }
}
