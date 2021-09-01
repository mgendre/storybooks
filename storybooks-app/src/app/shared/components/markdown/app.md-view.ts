import {Component, ElementRef, Input, OnDestroy, OnInit, ViewChild} from "@angular/core";
import md from 'markdown-it';
import {MarkdownService} from "./MarkdownService";
import {ActorsDatastore} from "../../../datastores/ActorDatastore";
import {Subscription} from "rxjs";
import {NGXLogger} from "ngx-logger";

@Component({
  selector: 'app-markdown-view',
  templateUrl: './app.md-view.html',
  styleUrls: ['./app.md-view.scss']
})
export class MarkdownView implements OnDestroy, OnInit {
  private readonly markdownRenderer;

  private subscriptions: Subscription[] = [];

  private _renderer!: ElementRef;

  @ViewChild("renderer")
  set renderer(elt: ElementRef) {
    this._renderer = elt;
    this.renderMarkdown();
  }
  get renderer() {
    return this._renderer;
  }

  _markdown = '';

  @Input('markdown')
  set markdown(value: string) {
    this._markdown = value;
    this.renderMarkdown();
    console.log(value);
  }

  get markdown(): string {
    return this._markdown;
  }

  constructor(private readonly markdownService: MarkdownService,
              private readonly actorsDatastore: ActorsDatastore,
              private readonly logger: NGXLogger) {
    this.markdownRenderer = this.createMarkdownRenderer();
    this.subscriptions.push(this.actorsDatastore.actors.subscribe(() => {
      this.renderMarkdown();
    }));
  }

  ngOnInit(): void {
    this.renderMarkdown();
  }

  private createMarkdownRenderer() {
    const editor = md({
      breaks: true,
      xhtmlOut: true,
      html: true
    });
    editor.renderer.rules.link_open = (tokens, idx, options, env, self) => {
      try {
        const linkOpen = tokens[idx];
        const linkContent = tokens[idx].attrGet('href')?.split('/');
        if (linkContent) {
          const type = linkContent[0];
          const id = linkContent[1];
          linkOpen.attrSet('onclick', `event.preventDefault(); window.openActor("${type}", "${id}");`);
        }
      }
      catch(e) {
        this.logger.error(e, 'Could not create link')
      }
      return self.renderToken(tokens, idx, options);
    }
    return editor;
  }

  private renderMarkdown() {

    let userInput = this._markdown;
    userInput += '\n\n\n\n';
    userInput += this.markdownService.getAllActorLinks();

    if (this.renderer) {
      this.renderer.nativeElement.innerHTML = this.markdownRenderer.render(userInput);
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => {
      s.unsubscribe();
    });
  }
}
