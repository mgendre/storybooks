import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import {GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule} from 'angularx-social-login';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {MainSidebarComponent} from './components/layout/app.sidebar';
import {AuthInterceptor} from './shared/auth/AuthInterceptor';
import {CampaignsComponent} from "./components/campaigns/app.campaigns";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {NgxWebstorageModule} from "ngx-webstorage";
import {LoggerModule, NgxLoggerLevel} from "ngx-logger";
import {ScenariosComponent} from "./components/scenarios/app.scenarios";
import {CurrentCampaignName} from "./components/campaigns/app.current-campaign-name";
import {EditScenarioComponent} from "./components/scenarios/app.edit-scenario";
import {MarkdownEditor} from "./shared/markdown/app.md-editor";
import {MarkdownView} from "./shared/markdown/app.md-view";

// required for AOT compilation
export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http, './assets/i18n/');
}


@NgModule({
  declarations: [
    AppComponent,
    MainSidebarComponent,
    MarkdownEditor,
    MarkdownView,
    CampaignsComponent,
    CurrentCampaignName,
    ScenariosComponent,
    EditScenarioComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    SocialLoginModule,
    NgxWebstorageModule.forRoot(),
    LoggerModule.forRoot({
      level: NgxLoggerLevel.DEBUG
    }),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    ReactiveFormsModule
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
