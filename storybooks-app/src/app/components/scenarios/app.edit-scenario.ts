import {ScenarioDto, ScenarioUpdateDto} from "../../services/api.generated.clients";
import {Subscription} from "rxjs";
import {Component, OnDestroy, OnInit} from "@angular/core";
import {ScenariosDatastore} from "../../datastores/ScenariosDatastore";
import {ActivatedRoute, Params} from "@angular/router";
import {FormControl, FormGroup, Validators} from "@angular/forms";


@Component({
  selector: 'app-edit-scenario',
  templateUrl: './app.edit-scenario.html',
  styleUrls: ['./app.edit-scenario.scss']
})
export class EditScenarioComponent implements OnInit, OnDestroy {

  scenario: ScenarioDto = new ScenarioDto();

  scenarioForm = new FormGroup({
    title: new FormControl('', [
      Validators.required,
    ])
  });

  private readonly subscriptions: Subscription[] = [];

  constructor(private readonly scenariosDatastore: ScenariosDatastore,
              private route: ActivatedRoute) {
  }

  async save() {
    const toUpdate = new ScenarioUpdateDto();
    toUpdate.title = this.scenarioForm.get('title')?.value ?? '';
    toUpdate.markdown = this.scenario.markdown;
    await this.scenariosDatastore.saveScenario(toUpdate, this.scenario.id);
  }

  ngOnInit(): void {
    this.subscriptions.push(this.route.params.subscribe((params: Params) => {
      const id = params['scenarioId'];
      if (id) {
        this.scenario = this.scenariosDatastore.getScenario(id);
      } else {
        this.scenario = new ScenarioDto();
      }
      this.scenarioForm.setValue({
        'title': this.scenario.title
      });
    }));
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    });
  }
}
