import { HttpClient, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BandaDetalheResponse, BandaRequest, BandaResponse } from '../../models/banda';
import { Guid } from 'guid-typescript/dist/guid';
import { Album, AlbumRequest, AlbumResponse } from '../../models/album';
import { BandasComponent } from '../../admin/banda/bandas/bandas.component';

@Injectable({
  providedIn: 'root'
})
export class BandaService {

  private url = "https://localhost:7170/api/streaming"

  constructor(private httpClient: HttpClient) { }

  public GetBandas(): Observable<BandaResponse[]>{
    return this.httpClient.get<BandaResponse[]>(`${this.url}/bandas`);
  }

  public GetBandaDetalhe(id:Guid): Observable<BandaDetalheResponse>{
    console.log(id)
    return this.httpClient.get<BandaDetalheResponse>(`${this.url}/banda-detalhe/${id}`);
  }

  public GetBandaPorId(id:Guid): Observable<any>{
    return this.httpClient.get<any>(`${this.url}/banda/`+id);
  }

  public Criar(data:BandaRequest): Observable<any>{
    console.log(data)
    return this.httpClient.post<HttpEvent<any>>(`${this.url}/banda-criar`,data);
  }

  public Atualizar(data:BandaRequest): Observable<any> {
    console.log(data)
    return this.httpClient.put<any>(`${this.url}/banda-atualizar`,data);
  }

  public GetAlbumDetalhe(bandaId:Guid, albumId: Guid): Observable<AlbumResponse>{
    return this.httpClient.get<AlbumResponse>(`${this.url}/banda/${bandaId}/album-detalhe/${albumId}`);
  }


  public CriarAlbum(data:AlbumRequest):Observable<any>{
    return this.httpClient.put<any>(`${this.url}/banda/criar-album`,data);
  }

  public AtualizarAlbum(data:AlbumRequest):Observable<any>{
    return this.httpClient.put<any>(`${this.url}/banda/criar-album`,data);
  }

  public Remover(id:Guid):Observable<any>{
    console.log(id)
    return this.httpClient.delete<any>(`${this.url}/banda-remover/${id}`);
  }

}
