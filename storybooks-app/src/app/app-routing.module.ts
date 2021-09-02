import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CampaignsComponent} from "./components/campaigns/app.campaigns";
import {ScenariosComponent} from "./components/scenarios/app.scenarios";
import {EditScenarioComponent} from "./components/scenarios/app.edit-scenario";
import {CharactersComponent} from "./components/actors/app.characters";
import {EditCharacterComponent} from "./components/actors/app.edit-character";
import {MediaLibComponent} from "./components/media/app.media-lib";

const routes: Routes = [
  { path: '', redirectTo: 'campaigns', pathMatch: 'full' },
  { path: 'campaigns', component: CampaignsComponent },
  { path: 'scenarios', component: ScenariosComponent },
  { path: 'scenarios/new', component: EditScenarioComponent },
  { path: 'scenarios/edit/:scenarioId', component: EditScenarioComponent },
  { path: 'characters', component: CharactersComponent },
  { path: 'characters/new', component: EditCharacterComponent },
  { path: 'characters/edit/:characterId', component: EditCharacterComponent },
  { path: 'media-lib', component: MediaLibComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
