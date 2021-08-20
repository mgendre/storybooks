import {ScenarioDto} from "../../services/api.generated.clients";
import {Subscription} from "rxjs";
import {Component, OnDestroy, OnInit} from "@angular/core";
import {ScenariosDatastore} from "../../datastores/ScenariosDatastore";
import {ActivatedRoute, Params} from "@angular/router";
import {FormControl, FormGroup} from "@angular/forms";


@Component({
  selector: 'app-edit-scenario',
  templateUrl: './app.edit-scenario.html',
  styleUrls: ['./app.edit-scenario.scss']
})
export class EditScenarioComponent implements OnInit, OnDestroy {

  scenario: ScenarioDto = new ScenarioDto();

  scenarioForm = new FormGroup({
    title: new FormControl(''),
    markdown: new FormControl(''),
  });

  private readonly subscriptions: Subscription[] = [];

  constructor(private readonly scenariosDatastore: ScenariosDatastore,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.subscriptions.push(this.route.params.subscribe((params: Params) => {
      const id = params['scenarioId'];
      if (id) {
        this.scenario = this.scenariosDatastore.getScenario(id);
      } else {
        this.scenario = new ScenarioDto();
      }
    }));
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    });
  }


}
