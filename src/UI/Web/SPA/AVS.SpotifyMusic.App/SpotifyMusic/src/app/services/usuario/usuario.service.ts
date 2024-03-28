import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Playlist } from '../../models/playlist';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private url = "https://localhost:7170/api/contas"

  constructor(private httpClient: HttpClient) { }

  public GetPlaylistsAssociadas(id:Guid): Observable<Playlist[]>{
    return this.httpClient.get<Playlist[]>(`${this.url}/usuario/playlists/${id}`);
  }

}
