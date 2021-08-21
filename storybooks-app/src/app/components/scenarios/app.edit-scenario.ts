import {ScenarioDto, ScenarioUpdateDto} from "../../services/api.generated.clients";
import {Subscription} from "rxjs";
import {Component, OnDestroy, OnInit} from "@angular/core";
import {ScenariosDatastore} from "../../datastores/ScenariosDatastore";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {FormControl, FormGroup, Validators} from "@angular/forms";


@Component({
  selector: 'app-edit-scenario',
  templateUrl: './app.edit-scenario.html',
  styleUrls: ['./app.edit-scenario.scss']
})
export class EditScenarioComponent implements OnInit, OnDestroy {

  _scenario: ScenarioDto = new ScenarioDto();
  set scenario(scenario: ScenarioDto) {
    this._scenario = scenario;
    this.scenarioForm.setValue({'title': scenario.title ?? ''});
  }
  get scenario() {
    return this._scenario;
  }


  scenarioForm = new FormGroup({
    title: new FormControl('', [
      Validators.required,
    ])
  });

  private readonly subscriptions: Subscription[] = [];

  constructor(private readonly scenariosDatastore: ScenariosDatastore,
              private router: Router,
              private route: ActivatedRoute) {
  }

  async save() {
    const toUpdate = new ScenarioUpdateDto();
    toUpdate.title = this.scenarioForm.get('title')?.value ?? '';
    toUpdate.markdown = this.scenario.markdown;
    await this.scenariosDatastore.saveScenario(toUpdate, this.scenario.id);
    await this.close();
  }

  async close() {
    await this.router.navigate(['/scenarios']);
  }

  ngOnInit(): void {
    let id : string | null = null;
    this.subscriptions.push(this.route.params.subscribe((params: Params) => {
      id = params['scenarioId'];

      if (!this.scenariosDatastore.isReady()) {
        return;
      }

      if (id) {
        this.scenario = this.scenariosDatastore.getScenario(id);
      } else {
        this.scenario = new ScenarioDto();
      }
      this.scenarioForm.setValue({
        'title': this.scenario.title ?? ''
      });
    }));

    this.subscriptions.push(this.scenariosDatastore.ready.subscribe(r => {
      if (r && id) {
        this.scenario = this.scenariosDatastore.getScenario(id);
      }
    }));
  }


  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    });
  }
}
