import {Injectable} from "@angular/core";
import {BehaviorSubject, Observable} from "rxjs";
import {GoogleLoginProvider, SocialAuthService, SocialUser} from "angularx-social-login";
import {HasInitialization} from "./HasInitialization";
import {UserProfileApiClient, UserProfileDto} from "./api.generated.clients";

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService implements HasInitialization {
  private readonly _user = new BehaviorSubject<UserProfileDto | null>(null);
  public readonly user: Observable<UserProfileDto | null> = this._user.asObservable();

  private readonly _ready = new BehaviorSubject(false);
  public readonly ready = this._ready.asObservable();

  constructor(private readonly authService: SocialAuthService,
              private readonly userProfileClient: UserProfileApiClient) {
    authService.authState.subscribe(async (su: SocialUser) => {
      if (su) {
        try {
          localStorage.setItem('jwt_token', su.idToken);
          await this.loadProfile();
        } catch (_) {
          this._user.next(null);
        } finally {
          this._ready.next(true);
        }
      } else {
        this._user.next(null);
        this._ready.next(true);
      }
    });
  }


  private async loadProfile(): Promise<UserProfileDto> {
    const user = await this.userProfileClient.ensureCreated().toPromise();
    this._user.next(user);
    return user;
  }

  public async login(): Promise<void> {
    await this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  public async logout(): Promise<void> {
    localStorage.setItem('jwt_token', '');
    await this.authService.signOut();
    this._user.next(null);
  }

  public async refreshToken(): Promise<void> {
    await this.authService.refreshAuthToken(GoogleLoginProvider.PROVIDER_ID);
  }

  public getToken(): string {
    return localStorage.get('jwt_token');
  }
}

