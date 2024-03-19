import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BandaDetalheResponse, BandaResponse } from '../../models/banda';
import { Guid } from 'guid-typescript/dist/guid';
import { Album } from '../../models/album';

@Injectable({
  providedIn: 'root'
})
export class BandaService {

  private url = "https://localhost:7170/api/streaming"

  constructor(private httpClient: HttpClient) { }

  public getBandas(): Observable<BandaResponse[]>{
    return this.httpClient.get<BandaResponse[]>(`${this.url}/bandas`);
  }

  public getBandaDetalhe(id:Guid): Observable<BandaDetalheResponse>{
    return this.httpClient.get<BandaDetalheResponse>(`${this.url}/banda-detalhe/${id}`);
  }

  public getAlbumDetalhe(bandaId:Guid, albumId: Guid): Observable<Album>{
    return this.httpClient.get<Album>(`${this.url}/banda/${bandaId}/album-detalhe/${albumId}`);
  }

}
