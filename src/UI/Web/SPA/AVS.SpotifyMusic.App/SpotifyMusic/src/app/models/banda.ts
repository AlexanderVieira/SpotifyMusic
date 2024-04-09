import { Album } from "./album";
import { Guid } from "guid-typescript/dist/guid";

export class BandaRequest {

  public id: string;
  public nome: string = '';
  public descricao: string = '';
  public foto?: string = '';

  constructor() {
    this.id = Guid.createEmpty().toString();
  }

}

export class BandaResponse {

  public id: Guid;
  public nome: string = '';
  public descricao: string = '';
  public foto?: string = '';

  constructor() {
    this.id = Guid.createEmpty();
  }

}

export class BandaDetalheResponse {

  public id: Guid;
  public nome: string = '';
  public descricao: string = '';
  public foto?: string = '';
  public albuns: Album[] = [];

  constructor() {
    this.id = Guid.createEmpty();
  }
}
