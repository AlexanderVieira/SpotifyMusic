import { Musica } from "./musica";

export class Album {

  public id: string = '';
  public titulo: string = '';
  public descricao: string = '';
  public foto?: string = '';
  // public bandaId!: string;
  public musicas: Musica[] = [];

}
