import { Guid } from 'guid-typescript/dist/guid';
import { Musica, MusicaRequest } from "./musica";

export class Album {

  public id: Guid = Guid.createEmpty();
  public titulo: string = '';
  public descricao: string = '';
  public foto?: string = '';
  public bandaId: Guid = Guid.createEmpty();
  public musicas: Musica[] = [];

}

export class AlbumRequest {
  
  public id!: string;
  public titulo!: string;
  public descricao!: string;
  public foto!: string;
  public bandaId!: Guid;
  public musicas: MusicaRequest[] = [];
  //public musica!: MusicaRequest;

}

export class AlbumResponse {
  
  public id: Guid = Guid.createEmpty();
  public titulo: string = '';
  public descricao: string = '';
  public foto: string = '';
  public bandaId: Guid = Guid.createEmpty();
  public musicas: Musica[] = [];

}
