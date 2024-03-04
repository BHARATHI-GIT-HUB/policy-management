import { Component, EventEmitter, Output } from '@angular/core';

import { NzMessageService } from 'ng-zorro-antd/message';
import { NzUploadChangeParam } from 'ng-zorro-antd/upload';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrl: './upload.component.scss',
})
export class UploadComponent {
  @Output() filesSelected: EventEmitter<File[]> = new EventEmitter<File[]>();
  selectedFiles: File[] = [];

  onFilesSelected(event: any): void {
    this.selectedFiles = event.target.files;
  }
  constructor(private msg: NzMessageService) {}

  handleChange({ file, fileList }: NzUploadChangeParam): void {
    const status = file.status;

    if (status !== 'uploading') {
      console.log(file, fileList);
    }
    if (status === 'done') {
      this.msg.success(`${file.name} file uploaded successfully.`);
    } else if (status === 'error') {
      this.msg.success(`${file.name} file uploaded successfully.`);

      if (file && fileList.length > 0) {
        fileList.forEach((file) => {
          this.selectedFiles.push(file.originFileObj!);
        });
        this.filesSelected.emit(this.selectedFiles);
      }
    }
  }
}
