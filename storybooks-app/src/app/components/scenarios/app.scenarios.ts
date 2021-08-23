import {ScenarioDto} from "../../services/api.generated.clients";
import {Subscription} from "rxjs";
import {Component, OnDestroy} from "@angular/core";
import {ScenariosDatastore} from "../../datastores/ScenariosDatastore";
import {Router} from "@angular/router";
import {ConfirmationService} from "primeng/api";
import {TranslateService} from "@ngx-translate/core";


@Component({
  selector: 'app-scenarios',
  templateUrl: './app.scenarios.html',
  styleUrls: ['./app.scenarios.scss']
})
export class ScenariosComponent implements OnDestroy {

  scenarios: ScenarioItem[] = [];

  private readonly subscriptions: Subscription[] = [];

  constructor(private readonly scenariosDatastore: ScenariosDatastore,
              private readonly router: Router,
              private readonly confirmationService: ConfirmationService,
              private readonly translate: TranslateService) {
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
        return -1;
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

  confirmDelete(scenario: ScenarioDto) {
    this.confirmationService.confirm({
      message: this.translate.instant(
        'scenarios.list.delete.confirm.message',
        {scenarioTitle: scenario.title}),
      header: this.translate.instant('scenarios.list.delete.confirm.title',
        {scenarioTitle: scenario.title}),
      icon: 'bi bi-exclamation',
      accept: async () => {
        await this.scenariosDatastore.deleteScenario(scenario.id);
      },
      reject: () => {
      }
    });
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
