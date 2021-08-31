import {Directive, HostListener} from "@angular/core";

@Directive({
  selector: '[preventEnterDefault]'
})
export class PreventDefaultEnter {
  @HostListener('keydown.enter', ['$event']) onEnter(event: Event) {
    event.preventDefault();
  }
}
