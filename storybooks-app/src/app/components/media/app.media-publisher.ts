import {Subscription} from "rxjs";
import {Component, ElementRef, Input, ViewChild} from "@angular/core";
import {DocumentLibApiClient, MediaDto} from "../../services/api.generated.clients";
import {MediaDatastore} from "../../datastores/MediaDatastore";


@Component({
  selector: 'app-media-publisher',
  templateUrl: './app.media-publisher.html',
  styleUrls: ['./app.media-publisher.scss']
})
export class MediaPublisherComponent {

  _dialogOpened = false;
  get dialogOpened() {
    return this._dialogOpened;
  }
  set dialogOpened(opened: boolean) {
    this._dialogOpened = opened;
    this.selectedFile = null;

    // Clear the file upload
    this.fileUpload.nativeElement.value = "";
  }

  @Input()
  existingDocument : MediaDto | null = null;

  selectedFile: File | null = null;

  @ViewChild("fileUpload")
  private fileUpload!: ElementRef;

  constructor(private readonly mediaDatastore: MediaDatastore) {
  }

  openDialog() {
    this.dialogOpened = true;
  }

  close() {
    this.dialogOpened = false;
  }

  async publish() {
    if (this.selectedFile) {
      await this.mediaDatastore.upload(this.selectedFile, '');
    }
    this.close();
  }

  onFileSelected(fileChangeEvent: any) {
    this.selectedFile = null;
    const target = fileChangeEvent.target;
    if (target?.files && target?.files.length > 0) {
      this.selectedFile = target.files[0];
    }
  }
}
