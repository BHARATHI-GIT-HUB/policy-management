import { count } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { UploadComponent } from '../../components/upload/upload.component';

@Component({
  selector: 'app-bulk-upload',
  templateUrl: './bulk-upload.component.html',
  styleUrl: './bulk-upload.component.scss',
})
export class BulkUploadComponent {
  selectedFiles: File[] = [];
  fileToUpload: File | null = null;
  uploading = false;
  errorMessage = '';
  responseMessage = '';

  onDrop(event: DragEvent): void {
    event.preventDefault();
    this.highlightDropzone(false); // Reset highlighting
    const files = event.dataTransfer?.files;

    if (files && files.length > 0) {
      this.handleFiles(files);
    }
  }

  constructor(private http: HttpClient) {}

  onDragOver(event: DragEvent): void {
    event.preventDefault();
    this.highlightDropzone(true); // Highlight dropzone
  }

  onDragLeave(event: DragEvent): void {
    event.preventDefault();
    this.highlightDropzone(false); // Reset highlighting
  }

  private highlightDropzone(highlight: boolean): void {
    // Add or remove CSS classes to highlight or reset the dropzone styling
    const dropzone = document.getElementById('dropzone-file');
    if (dropzone) {
      if (highlight) {
        dropzone.classList.add('dragover');
      } else {
        dropzone.classList.remove('dragover');
      }
    }
  }

  private handleFiles(files: FileList): void {
    // Handle the dropped files
    this.selectedFiles = Array.from(files);
    console.log('Selected files:', this.selectedFiles);
    // Perform further processing like uploading the files
  }

  handleFilesSelected(files: File[]): void {
    console.log(files.length);
    this.selectedFiles = files;
  }

  onFilesSelected(event: any): void {
    this.selectedFiles = event.target.files;
  }

  uploadFile() {
    this.uploading = true;

    if (this.selectedFiles && this.selectedFiles.length > 0) {
      const formData = new FormData();

      for (let i = 0; i < this.selectedFiles.length; i++) {
        console.log('files', this.selectedFiles[i]);
        formData.append('files', this.selectedFiles[i]);
      }

      this.http
        .post<any>('http://192.168.0.152:7154/api/policy/upload', formData)
        .subscribe(
          (response) => {
            this.uploading = false;
            console.log(response);
            this.responseMessage = response.message;
            this.responseMessage += ' for ids :';
            if (Array.isArray(response.enrolledids)) {
              response.enrolledids.forEach((id: any) => {
                this.responseMessage += ' ' + id;
              });
            }
          },
          (error) => {
            this.uploading = false;
            console.log(error);
            this.errorMessage = error.message;
          }
        );
    }
  }
}
