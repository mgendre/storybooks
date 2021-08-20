import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {CampaignsComponent} from "./components/campaigns/app.campaigns";
import {ScenariosComponent} from "./components/scenarios/app.scenarios";
import {EditScenarioComponent} from "./components/scenarios/app.edit-scenario";

const routes: Routes = [
  { path: '', redirectTo: 'campaigns', pathMatch: 'full' },
  { path: 'campaigns', component: CampaignsComponent },
  { path: 'scenarios', component: ScenariosComponent },
  { path: 'scenarios/edit/:scenarioId', component: EditScenarioComponent },
  { path: 'scenarios/new', component: EditScenarioComponent },
  { path: 'persons', component: CampaignsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
