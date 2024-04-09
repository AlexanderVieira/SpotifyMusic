import { Component, Inject, LOCALE_ID, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ValidatorField } from './../../helpers/ValidatorField';
import { NgxCurrencyDirective } from "ngx-currency";
import { UserRegister } from '../../models/identity/userRegister';
import { AuthService } from '../../services/auth/auth.service';
import { ToastrService } from 'ngx-toastr';
import { TipoPlano } from '../../models/enums/tipoPlano.enum';
import { Cartao } from '../../models/cartao';
import { Assinatura } from '../../models/assinatura';
import { Plano } from '../../models/plano';

@Component({
  selector: 'app-register',
  standalone: true,
  imports:[
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    NgxCurrencyDirective
  ],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {

  user = {} as UserRegister;
  form!: FormGroup;
  tipoPlano = TipoPlano;
  valor!: any;

  constructor(private fb: FormBuilder,
              private authService: AuthService,
              private router: Router,
              private toaster: ToastrService
            ) {

             }

  get f(): any { return this.form.controls; }

  ngOnInit(): void {
    this.validation();
  }

  private validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmePassword')
    };

    this.form = this.fb.group({
      primeiroNome: ['', Validators.required],
      ultimoNome: ['', Validators.required],
      email: ['',
        [Validators.required, Validators.email]
      ],
      userName: ['', Validators.required],
      cpf: ['', Validators.required],
      dtNascimento: [''],
      ativo: [''],
      password: ['',
        [Validators.required, Validators.minLength(4)]
      ],
      confirmePassword: ['', Validators.required],
      numeroCartao: ['', Validators.required],
      nomeCartao: ['', Validators.required],
      expiracao: ['', Validators.required],
      cvv: ['', Validators.required],
      ativoCartao: [''],
      limite: ['', Validators.required],
      nomePlano: ['', Validators.required],
      descricaoPlano: ['', Validators.required],
      valorPlano: ['', Validators.required],
      tipoPlano: ['', Validators.required],
      ativoAssinatura: ['', Validators.required],
    }, formOptions);
  }

  register(): void {

    const user = { ...this.form.value };
    this.user = new UserRegister();
    this.user.primeiroNome = user.primeiroNome;
    this.user.ultimoNome = user.ultimoNome;
    this.user.userName = user.userName;
    this.user.cpf = user.cpf;
    this.user.email = user.email;
    this.user.dtNascimento = user.dtNascimento;
    this.user.ativo = user.ativo;
    this.user.password = user.password;
    this.user.confirmePassword = user.confirmePassword;
    this.user.cartao = new Cartao();
    this.user.cartao.numero = user.numeroCartao;
    this.user.cartao.nome = user.nomeCartao;
    this.user.cartao.expiracao = user.expiracao;
    this.user.cartao.cvv = user.cvv;
    this.user.cartao.limite = user.limite;
    this.user.cartao.ativo = user.ativoCartao;
    this.user.assinatura = new Assinatura();
    this.user.assinatura.ativo = user.ativoAssinatura;
    this.user.assinatura.plano = new Plano();
    this.user.assinatura.plano.nome = user.nomePlano;
    this.user.assinatura.plano.descricao = user.descricaoPlano;
    this.user.assinatura.plano.valor = user.valorPlano;
    this.user.assinatura.plano.tipoPlano = user.tipoPlano;


    this.authService.register(this.user).subscribe(

      () => {

        console.log()
        this.router.navigateByUrl('/perfil')
      },
      (error: any) => {
        this.toaster.error(error.error)
      }
    )

  }

}
