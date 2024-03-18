import { Musica } from "./musica";

export class Playlist {

  public id: string = '';
  public titulo: string = '';
  public descricao: string = '';
  public foto?: string = '';
  public publica = false;
  public musicas: Musica[] = [];

}
