import {Component, Input, OnDestroy, OnInit} from "@angular/core";
import {Subject, Subscription} from "rxjs";
import {debounceTime, distinctUntilChanged} from "rxjs/operators";

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
  markdown = '';

  markdownValueChanged = new Subject<string>();

  markdownPreview = '';

  markdownInputChanged() {
    this.markdownValueChanged.next(this.markdown);
  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.markdownValueChanged.pipe(
        debounceTime(300),
        distinctUntilChanged()
      ).subscribe(() => {
        this.markdownPreview = this.markdown;
      })
    );
    this.markdownPreview = this.markdown;
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    })
  }
}

export enum PreviewMode {
  EDITOR='editor', COMBINED='combined', PREVIEW='preview'
}
