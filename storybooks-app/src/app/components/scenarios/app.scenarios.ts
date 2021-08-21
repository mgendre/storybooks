import {ScenarioDto} from "../../services/api.generated.clients";
import {Subscription} from "rxjs";
import {Component, OnDestroy} from "@angular/core";
import {ScenariosDatastore} from "../../datastores/ScenariosDatastore";
import {Router} from "@angular/router";


@Component({
  selector: 'app-scenarios',
  templateUrl: './app.scenarios.html',
  styleUrls: ['./app.scenarios.scss']
})
export class ScenariosComponent implements OnDestroy {

  scenarios: ScenarioItem[] = [];

  private readonly subscriptions: Subscription[] = [];

  constructor(private readonly scenariosDatastore: ScenariosDatastore,
              private readonly router: Router) {
    this.subscriptions.push(this.scenariosDatastore.scenarios.subscribe(scenarios => {
      this.scenarios = [];
      if (!scenarios) {
        return;
      }

      // Order them by date desc...
      scenarios.sort((a,b) => {
        if (a.creationDate === b.creationDate) {
          return 0;
        }
        if (a.creationDate < b.creationDate) {
          return 1;
        }
        return 0;
      });

      for (let i=0; i<scenarios.length; i++) {
        this.scenarios.push(new ScenarioItem(scenarios[i], i<1));
      }
    }));
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    });
  }

  async editScenario(sce: ScenarioDto) {
    await this.router.navigate(['scenarios/edit/' + sce.id]);
  }

  async createScenario() {
    await this.router.navigate(['scenarios/new']);
  }
}

class ScenarioItem {
  public readonly scenario: ScenarioDto;
  public expanded = false;

  constructor(scenario: ScenarioDto, expanded: boolean = false) {
    this.scenario = scenario;
    this.expanded = expanded;
  }
}
