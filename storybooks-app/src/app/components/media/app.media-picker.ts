import {Component, ElementRef, EventEmitter, Input, Output, ViewChild} from "@angular/core";
import {MediaDto} from "../../services/api.generated.clients";
import {MediaDatastore} from "../../datastores/MediaDatastore";


@Component({
  selector: 'app-media-picker',
  templateUrl: './app.media-picker.html',
  styleUrls: ['./app.media-picker.scss']
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

  @Output() onMediaPicked: EventEmitter<MediaDto> = new EventEmitter();

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
      const published = await this.mediaDatastore.upload(this.selectedFile, '');
      this.onMediaPicked.emit(published);
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
