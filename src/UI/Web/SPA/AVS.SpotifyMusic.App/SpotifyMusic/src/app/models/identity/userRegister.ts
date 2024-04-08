import { Assinatura } from "../assinatura";
import { Cartao } from "../cartao";

export class UserRegister {

  primeiroNome!: string;
  ultimoNome!: string;
  userName!: string;
  cpf!: string;
  email!: string;
  dtNascimento!: string;
  ativo!: boolean;
  password!: string;
  confirmePassword!: string;
  cartao!: Cartao;
  assinatura!: Assinatura

}
