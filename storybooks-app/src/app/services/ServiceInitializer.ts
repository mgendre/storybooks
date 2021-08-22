import {Injectable} from "@angular/core";
import {HasInitialization} from "./HasInitialization";
import {BehaviorSubject, combineLatest, Observable} from "rxjs";
import {AuthenticationService} from "./AuthenticationService";
import {TranslateService} from "@ngx-translate/core";
import {PrimeNGConfig} from "primeng/api";

@Injectable({
  providedIn: 'root',
})
export class ServiceInitializer implements HasInitialization {
  private readonly _ready = new BehaviorSubject(false);

  private readonly _translationReady = new BehaviorSubject(false);

  ready: Observable<boolean> = this._ready.asObservable();

  constructor(authenticationService: AuthenticationService,
              private readonly translate: TranslateService,
              private readonly primeConfig: PrimeNGConfig) {
    combineLatest([authenticationService.ready, this._translationReady])
      .subscribe((all => {
          if (all.filter(v => !v).length === 0) {
            this._ready.next(true);
          }
        })
      );
  }

  public async initialize(): Promise<void> {
    await this.translate.use('fr').toPromise();
    this._translationReady.next(true);

    // Put translations into Prime components
    this.primeConfig.setTranslation({
      accept: this.translate.instant('common.confirm.accept'),
      reject: this.translate.instant('common.confirm.reject')
    });
  }

  isReady(): boolean {
    return this._ready.value;
  }
}
