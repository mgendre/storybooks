import {Message} from "primeng/api";

export class MessageFactory {
  public static ErrorMessage(message: string): Message {
    return {
      severity: 'error',
      closable: true,
      summary: message,
      icon: 'pi pi-exclamation-circle'
    }
  }
}
