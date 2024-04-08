import { UsuarioResponse, UsuarioRequest, UsuarioDetalheResponse } from './../../../models/usuario';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UsuarioService } from '../../../services/usuario/usuario.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ValidatorField } from '../../../helpers/ValidatorField';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-perfil-detalhe',
  standalone: true,
  imports:[FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './perfil-detalhe.component.html',
  styleUrls: ['./perfil-detalhe.component.css']
})
export class PerfilDetalheComponent implements OnInit {

  @Input() userId!: string;
  @Output() changeFormValue = new EventEmitter();
  usuarioRequest = {} as UsuarioRequest;
  usuarioResponse = {} as UsuarioResponse;
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private usuarioService: UsuarioService,
    private router: Router,
    private toaster: ToastrService
  ) { }

  ngOnInit() {
    this.validation();
    this.carregarUsuario();
    this.verificaForm();
  }

  private verificaForm(): void {
    this.form.valueChanges
      .subscribe(() => this.changeFormValue.emit({...this.form.value}))
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
          this.form.patchValue(this.usuarioResponse);
          this.toaster.success('Usuário Carregado', 'Sucesso');
        },
        (error: any) => {
          console.error(error);
          this.toaster.error('Usuário não Carregado', 'Erro');
          this.router.navigate(['/perfil']);
        }
      );
      //.add(this.spinner.hide());
  }

  private validation(): void {
    // const formOptions: AbstractControlOptions = {
    //   validators: ValidatorField.MustMatch('password', 'confirmePassword'),
    // };

    this.form = this.fb.group(
      {
        id: [''],
        userName: [''],
        foto: [''],
        primeiroNome: ['', Validators.required],
        ultimoNome: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        cpf: ['', [Validators.required]],
        ativo: ['', Validators.required],
        dtNascimento: ['', Validators.required],
        senha: ['', [Validators.minLength(4), Validators.nullValidator]],
        cartoes: [''],
        assinaturas: [''],
        playlists: ['']
      },
      //formOptions
    );
  }

  public toUsuarioResponse(response: UsuarioDetalheResponse): void{

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
    this.usuarioResponse.cartoes = response.cartoes;
    this.usuarioResponse.assinaturas = response.assinaturas;
    this.usuarioResponse.playlists = response.playlists;

  }

  // Conveniente para pegar um FormField apenas com a letra F
  public get f(): any {
    return this.form.controls;
  }

  onSubmit(): void {
    this.atualizarUsuario();
  }

  public atualizarUsuario() {
    this.usuarioRequest = { ...this.form.value };
    //this.spinner.show();

    this.usuarioService
      .Atualizar(this.usuarioRequest)
      .subscribe(
        () => this.toaster.success('Usuário atualizado!', 'Sucesso'),
        (error) => {
          this.toaster.error(error.error);
          console.error(error);
        }
      );
      //.add(() => this.spinner.hide());
  }

  public resetForm(event: any): void {
    event.preventDefault();
    this.form.reset();
  }

}
