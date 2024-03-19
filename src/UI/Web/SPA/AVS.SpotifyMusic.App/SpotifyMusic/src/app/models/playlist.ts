import { Guid } from 'guid-typescript/dist/guid';
import { Musica } from "./musica";

export class Playlist {

  public id: Guid = Guid.createEmpty();
  public titulo: string = '';
  public descricao: string = '';
  public foto?: string = '';
  public publica = false;
  public musicas: Musica[] = [];

}
