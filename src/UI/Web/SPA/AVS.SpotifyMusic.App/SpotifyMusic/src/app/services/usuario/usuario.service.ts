import { HttpClient, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Playlist } from '../../models/playlist';
import { Observable } from 'rxjs';
import { UsuarioDetalheResponse, UsuarioRequest, UsuarioResponse } from '../../models/usuario';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private url = "https://localhost:7170/api/contas"

  constructor(private httpClient: HttpClient) { }

  public GetUsuarios(): Observable<UsuarioResponse[]>{
    return this.httpClient.get<UsuarioResponse[]>(`${this.url}/usuarios`);
  }

  public GetUsuarioDetalhe(id:string): Observable<UsuarioDetalheResponse>{
    console.log(id)
    return this.httpClient.get<UsuarioDetalheResponse>(`${this.url}/usuario-detalhe/${id}`);
  }

  public GetUsuarioPorId(id:string): Observable<UsuarioResponse>{
    return this.httpClient.get<UsuarioResponse>(`${this.url}/usuario/`+id);
  }

  public Criar(data:UsuarioRequest): Observable<HttpEvent<any>>{
    console.log(data)
    return this.httpClient.post<HttpEvent<any>>(`${this.url}/usuario-criar`,data);
  }

  public Atualizar(data:UsuarioRequest): Observable<HttpEvent<any>> {
    console.log(data)
    return this.httpClient.put<HttpEvent<any>>(`${this.url}/usuario-atualizar`,data);
  }

  public Remover(id:string):Observable<any>{
    console.log(id)
    return this.httpClient.delete<any>(`${this.url}/usuario-remover/${id}`);
  }

  public CriarPlaylist(data:Playlist):Observable<Playlist>{
    return this.httpClient.put<Playlist>(`${this.url}/usuario/criar-playlist`,data);
  }

  public GetPlaylistDetalhe(userId: string, playlistId: string): Observable<Playlist>{
    return this.httpClient.get<Playlist>(`${this.url}/usuario/${userId}/playlist-detalhe/${playlistId}`);
  }

  public GetPlaylistsAssociadas(id:Guid): Observable<Playlist[]>{
    return this.httpClient.get<Playlist[]>(`${this.url}/usuario/playlists/${id}`);
  }

  public AdicionarMusicaPlaylist(bandaId:Guid, musicaId: Guid): Observable<any>{
    return this.httpClient.put<any>(`${this.url}/usuario-adicionar-musica/${bandaId}/${musicaId}`, null);
  }

}
