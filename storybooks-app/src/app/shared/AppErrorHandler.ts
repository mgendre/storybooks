import {MessageService} from "primeng/api";
import {TranslateService} from "@ngx-translate/core";
import {NGXLogger} from "ngx-logger";
import {MessageFactory} from "./MessageFactory";
import {ErrorHandler, NgZone} from "@angular/core";

export class AppErrorHandler implements ErrorHandler {

  constructor(private readonly messageService: MessageService,
              private readonly translate: TranslateService,
              private readonly logger: NGXLogger,
              private readonly zone: NgZone) {
  }

  handleError(error: any) {
    this.logger.error('Unexpected error occurred', error);

    this.zone.run(() => {
      this.messageService.add(MessageFactory.ErrorMessage(this.translate.instant('common.error.message')));
    });
  }
}
