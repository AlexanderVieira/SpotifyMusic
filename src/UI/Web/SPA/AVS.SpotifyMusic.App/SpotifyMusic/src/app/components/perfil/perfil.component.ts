import { Guid } from 'guid-typescript';
import { UploadService } from './../../services/upload/upload.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../services/auth/auth.service';
import { PlaylistComponent } from "../contas/playlist/playlist.component";
import { PlaylistDetalheComponent } from "../contas/playlist/playlist-detalhe/playlist-detalhe.component";
import { PerfilDetalheComponent } from "./perfil-detalhe/perfil-detalhe.component";
import { TabsModule, TabsetConfig } from 'ngx-bootstrap/tabs';
import { UsuarioDetalheResponse, UsuarioResponse } from '../../models/usuario';
import { ActivatedRoute } from '@angular/router';
import { UserResponseLogin } from '../../models/identity/userResponseLogin';
import { UsuarioService } from '../../services/usuario/usuario.service';


@Component({
    selector: 'app-perfil',
    standalone: true,
    templateUrl: './perfil.component.html',
    styleUrls: ['./perfil.component.css'],
    imports: [PlaylistComponent, PlaylistDetalheComponent, PerfilDetalheComponent, TabsModule]
})
export class PerfilComponent implements OnInit {

  public usuarioResponse = {} as UsuarioResponse;
  public file!: File;
  public imagemURL = '';
  userId!: string;

  constructor(
    private toastr: ToastrService,
    private uploadService: UploadService,
    private usuarioService: UsuarioService,
    private activeRouter: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  public getCurrentUser() {
    let text = localStorage.getItem('user');
    if(text != null) {
      let model: UserResponseLogin = JSON.parse(text);
      this.userId = model.userToken.id;
    }
  }

  private carregarUsuario(): void {
    //this.spinner.show();
    this.usuarioService
      .GetUsuarioDetalhe(this.userId)
      .subscribe(
        (response: UsuarioDetalheResponse) => {
          console.log(response);
          //this.usuarioResponse = response;
          this.toUsuarioResponse(response);    
              
          //this.toaster.success('Usuário Carregado', 'Sucesso');
        },
        (error: any) => {
          console.error(error);
          //this.toaster.error('Usuário não Carregado', 'Erro');
          //this.router.navigate(['/perfil']);
        }
      );
      //.add(this.spinner.hide());
  }

  public setFormValue(usuario: UsuarioResponse): void {
    this.usuarioResponse = usuario;
    if (this.usuarioResponse.foto)
      this.imagemURL = 'https://localhost:7170' + `/resources/images/perfil/${this.usuarioResponse.foto}`;
    else
      this.imagemURL = '../../../assets/img/perfil.png';

  }

  onFileChange(ev: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemURL = event.target.result;

    this.file = ev.target.files[0];
    reader.readAsDataURL(this.file);

    this.uploadImagem();
  }

  private uploadImagem(): void {
    //this.spinner.show();
    this.uploadService
      .UploadImagePerfil(Guid.parse(this.userId), this.file)
      .subscribe(
        (response: any) => {
          this.toastr.success('Imagem atualizada com Sucesso', 'Sucesso!');
          this.carregarUsuario();          
        },
        (error: any) => {
          this.toastr.error('Erro ao fazer upload de imagem','Erro!');
          console.error(error);
        }
      ); //.add(() => this.spinner.hide());
  }

  private toUsuarioResponse(response: UsuarioDetalheResponse): void{

    const primeiroNome = response.nome.split(' ')[0];
    const ultimoNome = response.nome.split(' ')[1];
    this.usuarioResponse.id = response.id;
    this.usuarioResponse.primeiroNome = primeiroNome;
    this.usuarioResponse.ultimoNome = ultimoNome;
    this.usuarioResponse.email = response.email;
    this.usuarioResponse.cpf = response.cpf;
    this.usuarioResponse.dtNascimento = response.dtNascimento;
    this.usuarioResponse.ativo = response.ativo;
    this.usuarioResponse.foto = response.foto;
    this.usuarioResponse.senha = response.senha;
    //this.usuarioResponse.cartoes = response.cartoes;
    //this.usuarioResponse.assinaturas = response.assinaturas;
    //this.usuarioResponse.playlists = response.playlists;

  }

}
