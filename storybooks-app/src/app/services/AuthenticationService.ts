import {Injectable} from "@angular/core";
import {Observable, ReplaySubject} from "rxjs";
import {GoogleLoginProvider, SocialAuthService, SocialUser} from "angularx-social-login";

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private readonly _user = new ReplaySubject<User | null>();
  public readonly user: Observable<User | null> = this._user.asObservable();

  constructor(private readonly authService: SocialAuthService) {
    authService.authState.subscribe((su: SocialUser) => {
      if (su) {
        this._user.next(new User(su.email, su.firstName, su.lastName));
      } else {
        this._user.next(null);
      }
    });
  }

  public async login(): Promise<User> {
    const su = await this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
    localStorage.setItem('jwt_token', su.idToken);
    const user = new User(su.email, su.firstName, su.lastName);
    this._user.next(user);
    return user;
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

export class User {
  constructor(
    public readonly email: string,
    public readonly firstName: string,
    public readonly lastNamt: string
  ) {
  }
}
