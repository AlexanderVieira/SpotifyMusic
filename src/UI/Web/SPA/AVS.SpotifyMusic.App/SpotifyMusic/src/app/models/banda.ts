import { Album } from "./album";
import { Guid } from "guid-typescript/dist/guid";

export class BandaRequest {

  public id: any;
  public nome: string;
  public descricao: string;
  public foto: string;

  constructor(nome: string, descricao: string, foto?: string, id?: any){
    this.id = id;
    this.nome = nome;
    this.descricao = descricao;
    this.foto = descricao;
  }

}

export class BandaResponse {

  public id: Guid;
  public nome: string = '';
  public descricao: string = '';
  public foto?: string = '';
  //private albuns: Album[] = [];

  constructor() {
    this.id = Guid.create(); // ==> b77d409a-10cd-4a47-8e94-b0cd0ab50aa1
  }

}

export class BandaDetalheResponse {

  public id: Guid;
  public nome: string = '';
  public descricao: string = '';
  public foto?: string = '';
  public albuns: Album[] = [];

  constructor() {
    this.id = Guid.create();
  }
}
