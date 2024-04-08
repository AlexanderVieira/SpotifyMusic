import { Guid } from 'guid-typescript/dist/guid';
import { Playlist } from "./playlist";

export class Musica {

  public id: Guid = Guid.createEmpty();
  public nome: string = '';
  public duracao: number = 0;
  public playlists: Playlist[] = [];

}

export class MusicaRequest {
  
  public nome: string = '';
  public duracao: number = 0;  

}
