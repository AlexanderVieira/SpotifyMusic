import { Component, OnInit } from '@angular/core';
import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatListModule } from '@angular/material/list';
import { UploadService } from '../../services/upload/upload.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { BandaService } from '../../services/banda/banda.service';
import { BandaDetalheResponse } from '../../models/banda';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-banda',
  standalone: true,
  imports: [
    FormsModule,
    RouterModule,
    MatCardModule,
    MatToolbarModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    MatFormFieldModule,
    MatProgressBarModule,
    MatListModule],
  templateUrl: './banda.component.html',
  styleUrls: ['./banda.component.css']
})
export class BandaComponent implements OnInit {

  currentFile?: File;
  progress = 0;
  message = '';

  fileName = 'Select File';
  fileInfos?: Observable<any>;

  banda: BandaDetalheResponse;
  bandaId: Guid = Guid.createEmpty();

  constructor(private bandaService: BandaService,
              private uploadService: UploadService,
              private activeRouter: ActivatedRoute,
              private router: Router)
  {
    this.banda = new BandaDetalheResponse();
  }

  ngOnInit() {

    this.activeRouter.params.subscribe(params => {
      this.bandaId = params['id'];
      console.log(this.bandaId);
    });

    this.bandaService.GetBandaDetalhe(this.bandaId).subscribe(response => {
      console.log(response);
      this.banda = response;
    });
  }

  selectFile(event: any): void {
    this.progress = 0;
    this.message = "";

    if (event.target.files && event.target.files[0]) {
      const file: File = event.target.files[0];
      this.currentFile = file;
      this.fileName = this.currentFile.name;
    } else {
      this.fileName = 'Select File';
    }
  }

  upload(): void {
    if (this.currentFile) {
      this.uploadService.upload(this.currentFile).subscribe({
        next: (event: any) => {
          if (event.type === HttpEventType.UploadProgress) {
            this.progress = Math.round(100 * event.loaded / event.total);
          } else if (event instanceof HttpResponse) {
            this.message = event.body.message;
            this.fileInfos = this.uploadService.getFiles();
          }
        },
        error: (err: any) => {
          console.log(err);
          this.progress = 0;

          if (err.error && err.error.message) {
            this.message = err.error.message;
          } else {
            this.message = 'Could not upload the file!';
          }
        },
        complete: () => {
          this.currentFile = undefined;
        }
      });
    }
  }
}
