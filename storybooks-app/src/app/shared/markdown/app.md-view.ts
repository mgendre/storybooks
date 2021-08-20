import {Component, Input} from "@angular/core";
import md from 'markdown-it';

@Component({
  selector: 'app-markdown-view',
  templateUrl: './app.md-view.html',
  styleUrls: ['./app.md-view.scss']
})
export class MarkdownView {
  private readonly markdownRenderer = MarkdownView.createMarkdownRenderer();

  _markdown = '';

  @Input('markdown')
  set markdown(value: string) {
    this._markdown = value;
    this.renderMarkdown();
  }
  get markdown(): string {
    return this._markdown;
  }

  constructor() {
    this.renderMarkdown();
  }

  markdownHtml = '';

  private static createMarkdownRenderer() {
    return md({
      breaks: true,
      xhtmlOut: true,
      html: true
    });
  }

  private renderMarkdown() {
    this.markdownHtml = this.markdownRenderer.render(this._markdown);
  }
}
