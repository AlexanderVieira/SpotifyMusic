import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BandaDetalheResponse, BandaRequest, BandaResponse } from '../../models/banda';
import { Guid } from 'guid-typescript/dist/guid';
import { Album } from '../../models/album';
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

  public GetBandaPorId(id:string):Observable<any>{
    return this.httpClient.get<any>(`${this.url}/banda/`+id);
  }

  public GetAlbumDetalhe(bandaId:Guid, albumId: Guid): Observable<Album>{
    return this.httpClient.get<Album>(`${this.url}/banda/${bandaId}/album-detalhe/${albumId}`);
  }

  public Criar(data:BandaRequest){
    console.log(data)
    return this.httpClient.post(`${this.url}/banda-criar`,data);
  }

  public Atualizar(data:BandaRequest){
    console.log(data)
    return this.httpClient.put(`${this.url}/banda-atualizar`,data);
  }

  public CriarAlbum(data:Album):Observable<Album>{
    return this.httpClient.put<Album>(`${this.url}/banda/criar-album`,data);
  }

  public Remover(id:Guid):Observable<any>{
    console.log(id)
    return this.httpClient.delete<any>(`${this.url}/banda-remover/${id}`);
  }

}
