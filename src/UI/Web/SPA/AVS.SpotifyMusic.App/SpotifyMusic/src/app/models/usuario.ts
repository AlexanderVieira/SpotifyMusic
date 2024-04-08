import { DateTimeFormatPipe } from "../helpers/DateTimeFormat.pipe";
import { Assinatura } from "./assinatura";
import { Cartao } from "./cartao";
import { Playlist } from "./playlist";

export class UsuarioDetalheResponse {

  id!: string;
  nome!: string;
  email!: string;
  cpf!: string;
  senha!:string;
  foto!: string;
  ativo!: boolean;
  dtNascimento!: string;
  cartoes: Cartao[] = [];
  assinaturas: Assinatura[] = [];
  playlists: Playlist[] = [];

}

export class UsuarioResponse {

  id!: string;
  primeiroNome!: string;
  ultimoNome!: string;
  userName!: string;
  email!: string;
  cpf!: string;
  senha!:string;
  foto!: string;
  ativo!: boolean;
  dtNascimento!: string;
  cartoes: any[] = [];
  assinaturas: any[] = [];
  playlists: any[] = [];

}

export class UsuarioRequest {

  id!: string;
  primeiroNome!: string;
  ultimoNome!: string;
  userName!: string;
  email!: string;
  cpf!: string;
  senha!:string;
  foto!: string;
  ativo!: boolean;
  dtNascimento!: string;

}
