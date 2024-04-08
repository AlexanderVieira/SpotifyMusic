import { Guid } from 'guid-typescript';
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { AlbumRequest } from '../../models/album';


@Injectable({
  providedIn: 'root'
})
export class UploadService {

  private baseUrlContas = "https://localhost:7170/api/contas";
  private baseUrlStreaming = "https://localhost:7170/api/streaming"

  constructor(private httpClient: HttpClient) { }

  UploadFotoPerfil(file: File): Observable<HttpEvent<any>> {

    const formData: FormData = new FormData();
    formData.append('file', file);
    const req = new HttpRequest('POST', `${this.baseUrlContas}/upload-image`, formData, {
      reportProgress: true,
      responseType: 'json'

    });

    return this.httpClient.request(req);
  }

  UploadFotoBanda(file: File): Observable<HttpEvent<any>> {

    const formData: FormData = new FormData();
    formData.append('file', file);
    const req = new HttpRequest('POST', `${this.baseUrlStreaming}/upload-image`, formData, {
      reportProgress: true,
      responseType: 'json'

    });

    return this.httpClient.request(req);
  }

  UploadImagePerfil(userId: Guid, file: File): Observable<HttpEvent<any>> {
    const fileToUpload = file as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this.httpClient
      .post<HttpEvent<any>>(`${this.baseUrlContas}/upload-image/${userId}`, formData)
      .pipe(take(1));
  }

  UploadImageBanda(bandaId: Guid, file: File): Observable<HttpEvent<any>> {
    const fileToUpload = file as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this.httpClient
      .post<HttpEvent<any>>(`${this.baseUrlStreaming}/upload-image/${bandaId}`, formData)
      .pipe(take(1));
  }

  UploadImageAlbum(albumRequest: AlbumRequest, file: File): Observable<HttpEvent<any>> {
    const fileToUpload = file as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this.httpClient
      .post<HttpEvent<any>>(
        `${this.baseUrlStreaming}/upload-image/album/${albumRequest.id}/${albumRequest.bandaId}`, formData
      )
      .pipe(take(1));
  }

  getFiles(): Observable<any> {
    return this.httpClient.get(`${this.baseUrlContas}/files`);
  }

}
