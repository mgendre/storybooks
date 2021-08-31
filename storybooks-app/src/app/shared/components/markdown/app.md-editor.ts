import {Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild} from "@angular/core";
import {Subject, Subscription} from "rxjs";
import {debounceTime, distinctUntilChanged} from "rxjs/operators";
import {ActorPickerComponent} from "../../../components/actors/app.actor-picker";
import {AbstractActorDto, CharacterDto} from "../../../services/api.generated.clients";

@Component({
  selector: 'app-markdown-editor',
  templateUrl: './app.md-editor.html',
  styleUrls: ['./app.md-editor.scss']
})
export class MarkdownEditor implements OnInit, OnDestroy {

  previewMode = PreviewMode.COMBINED;
  previewModes = PreviewMode;

  private readonly subscriptions: Subscription[] = [];

  @Input()
  markdown: string | null = '';

  @Output('markdownChange') markdownChangeEmitter = new EventEmitter<string>();

  @ViewChild("textAreaElement") textAreaElement!: ElementRef;

  @ViewChild("actorPicker") actorPicker!: ActorPickerComponent;

  markdownValueChanged = new Subject<string>();

  markdownPreview = '';

  markdownInputChanged() {
    this.markdownValueChanged.next(this.markdown ?? '');
    this.markdownChangeEmitter.emit(this.markdown ?? '');
  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.markdownValueChanged.pipe(
        debounceTime(300),
        distinctUntilChanged()
      ).subscribe(() => {
        this.markdownPreview = this.markdown ?? '';
      })
    );
    this.markdownPreview = this.markdown ?? '';
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    })
  }

  selectCharacter() {
    this.actorPicker.pickCharacter();
  }

  actorPicked(actor: AbstractActorDto) {
    alert('Type: ' + (actor instanceof CharacterDto));

    // InputUtils.insertAtCaret(this.textAreaElement.nativeElement, 'ads');
    // this.textAreaElement.nativeElement.focus();
  }

  interceptEnter(event: Event) {
    event.preventDefault();
  }
}

export enum PreviewMode {
  EDITOR='editor', COMBINED='combined', PREVIEW='preview'
}
