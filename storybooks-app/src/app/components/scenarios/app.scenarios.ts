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

  scenarios: ScenarioDto[] = [];

  private readonly subscriptions: Subscription[] = [];

  constructor(private readonly scenariosDatastore: ScenariosDatastore,
              private readonly router: Router) {
    this.subscriptions.push(this.scenariosDatastore.scenarios.subscribe(scenarios => {
      if (!scenarios) {
        this.scenarios = [];
        return;
      }
      this.scenarios = scenarios;
      // Order them by date desc...
      this.scenarios.sort((a,b) => {
        if (a.creationDate === b.creationDate) {
          return 0;
        }
        if (a.creationDate < b.creationDate) {
          return 1;
        }
        return 0;
      });
    }));
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    });
  }

  openScenario(sce: ScenarioDto) {
    alert('OPEN');
  }

  async createScenario() {
    await this.router.navigate(['scenarios/new']);
  }
}
