import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserLogin } from './../../models/identity/userLogin';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../services/auth/auth.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '../../helpers/ValidatorField';

@Component({
  selector: 'app-login',
  standalone: true,
  imports:[
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model = {} as UserLogin;
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toaster: ToastrService
  ) {}

  ngOnInit() {
    this.validation();
  }

  get f(): any { return this.form.controls; }

  private validation(): void {

    //const formOptions: AbstractControlOptions = {
    //  validators: ValidatorField.MustMatch('password', 'confirmePassword')
    //};

    this.form = this.fb.group({
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4)]]
    });//,formOptions);
  }


  public login(): void {

    this.authService.login(this.model).subscribe(
      () => {
        this.router.navigateByUrl('/perfil');
      },
      (error: any) => {
        if (error.status == 401)
          this.toaster.error('usuário ou senha inválido');
        else console.error(error);
      }
    );

  }

}
