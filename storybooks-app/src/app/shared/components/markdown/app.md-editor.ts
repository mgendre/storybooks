import {Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild} from "@angular/core";
import {Subject, Subscription} from "rxjs";
import {debounceTime, distinctUntilChanged} from "rxjs/operators";
import {ActorPickerComponent} from "../../../components/actors/app.actor-picker";
import {AbstractActorDto} from "../../../services/api.generated.clients";
import {InputUtils} from "../../Utils/InputUtils";
import {MarkdownService} from "./MarkdownService";

@Component({
  selector: 'app-markdown-editor',
  templateUrl: './app.md-editor.html',
  styleUrls: ['./app.md-editor.scss']
})
export class MarkdownEditor implements OnInit, OnDestroy {

  previewMode = PreviewMode.COMBINED;
  previewModes = PreviewMode;

  private readonly subscriptions: Subscription[] = [];


  _markdown: string | null = '';
  @Input()
  set markdown(markdown: string | null) {
    this._markdown = markdown;
    this.markdownValueChanged.next(this.markdown ?? '');
  }
  get markdown() {
    return this._markdown;
  }

  @Output('markdownChange') markdownChangeEmitter = new EventEmitter<string>();

  @ViewChild("textAreaElement") textAreaElement!: ElementRef;

  @ViewChild("actorPicker") actorPicker!: ActorPickerComponent;

  markdownValueChanged = new Subject<string>();

  markdownPreview = '';

  constructor(private readonly markdownService: MarkdownService) {
  }

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
    const actorKey = this.markdownService.actor2Key(actor);
    InputUtils.insertAtCaret(this.textAreaElement.nativeElement, actorKey);
    this.markdown = this.textAreaElement.nativeElement.value;
    this.markdownInputChanged();

    setTimeout(() => {
      this.textAreaElement.nativeElement.focus();
    }, 50);
  }
}

export enum PreviewMode {
  EDITOR='editor', COMBINED='combined', PREVIEW='preview'
}
