import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';
import {ErrorHandler, NgModule, NgZone} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {TranslateLoader, TranslateModule, TranslateService} from '@ngx-translate/core';
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
import {LoggerModule, NGXLogger, NgxLoggerLevel} from "ngx-logger";
import {ScenariosComponent} from "./components/scenarios/app.scenarios";
import {CurrentCampaignName} from "./components/campaigns/app.current-campaign-name";
import {EditScenarioComponent} from "./components/scenarios/app.edit-scenario";
import {MarkdownEditor} from "./shared/components/markdown/app.md-editor";
import {MarkdownView} from "./shared/components/markdown/app.md-view";
import {ConfirmDialogModule} from "primeng/confirmdialog";
import {ConfirmationService, MessageService} from "primeng/api";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ToastModule} from "primeng/toast";
import {AppErrorHandler} from "./shared/AppErrorHandler";
import {CharactersComponent} from "./components/actors/app.characters";
import {EditCharacterComponent} from "./components/actors/app.edit-character";
import {MediaLibComponent} from "./components/media/app.media-lib";
import {MediaPublisherComponent} from "./components/media/app.media-picker";
import {DialogModule} from "primeng/dialog";
import {MediaRendererComponent} from "./components/media/app.media-renderer";
import {ActorPickerComponent} from "./components/actors/app.actor-picker";
import {TabViewModule} from "primeng/tabview";
import {AutoCompleteModule} from "primeng/autocomplete";
import {PreventDefaultEnter} from "./shared/Utils/prevent-default.directive";
import {ActorPreviewComponent} from "./components/actors/app.actor-preview";

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
    PreventDefaultEnter,
    CampaignsComponent,
    CurrentCampaignName,
    ScenariosComponent,
    EditScenarioComponent,
    CharactersComponent,
    EditCharacterComponent,
    ActorPickerComponent,
    ActorPreviewComponent,
    MediaLibComponent,
    MediaPublisherComponent,
    MediaRendererComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
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
    ConfirmDialogModule,
    ToastModule,
    DialogModule,
    TabViewModule,
    AutoCompleteModule
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
    },
    {
      provide: ErrorHandler, useClass: AppErrorHandler, deps: [MessageService, TranslateService, NGXLogger, NgZone]
    },
    ConfirmationService,
    MessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
