import { Guid } from 'guid-typescript/dist/guid';
import { Musica } from "./musica";

export class Album {

  public id: Guid = Guid.createEmpty();
  public titulo: string = '';
  public descricao: string = '';
  public foto?: string = '';
  public bandaId: Guid = Guid.createEmpty();
  public musicas: Musica[] = [];

}
