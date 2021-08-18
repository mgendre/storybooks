import {HttpClient, HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import {GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule} from 'angularx-social-login';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import { MainSidebarComponent } from './components/layout/app.sidebar';
import { AuthInterceptor } from './shared/auth/AuthInterceptor';
import {RouterModule, Routes} from "@angular/router";
import {CampaignsComponent} from "./components/campaigns/app.campaigns";
import {FormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {NgxWebstorageModule} from "ngx-webstorage";

// required for AOT compilation
export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http, './assets/i18n/');
}

const routes: Routes = [
  { path: '', redirectTo: 'campaigns', pathMatch: 'full' },
  { path: 'campaigns', component: CampaignsComponent },
  { path: 'persons', component: CampaignsComponent }
];


@NgModule({
  declarations: [
    AppComponent,
    MainSidebarComponent,
    CampaignsComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    SocialLoginModule,
    NgxWebstorageModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    RouterModule.forRoot(routes)
  ],
  providers: [
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: true,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '701684887114-m7fiktiogjv1nvi95jmanfop6anbengl.apps.googleusercontent.com'
            )
          }
        ]
      } as SocialAuthServiceConfig
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
