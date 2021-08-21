import {Observable} from "rxjs";

export interface HasInitialization {
  ready: Observable<boolean>;
  isReady(): boolean;
}
