import { ValidatorField } from '@app/helpers/validatorField';
import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../services/account.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserUpdate } from '../../../models/identity/UserUpdate';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  userUpdate = {} as UserUpdate;
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    public accountService: AccountService,
    private router: Router,
    private toaster: ToastrService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.validation();
    this.carregarUsuario();
  }

  private carregarUsuario(): void {
    this.spinner.show();
    this.accountService
      .getUser()
      .subscribe(
          (userRetorno: UserUpdate) => {
            console.log(userRetorno);
            this.userUpdate = userRetorno;
            this.form.patchValue(this.userUpdate);
            this.toaster.success('Usuário Carregado', 'Sucesso');
          },
          (error) => {
            console.error(error);
            this.toaster.error('Usuário não Carregado', 'Erro');
            this.router.navigate(['/dashboard']);
          }
        )
        .add(() => this.spinner.hide());
    }

  private validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password' , 'passwordConfirm')
    };

    this.form = this.fb.group({
      userName: [''],
      title: ['NaoInformado', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email] ],
      fone: ['', Validators.required],
      description: ['', Validators.required],
      function: ['NaoInformado', Validators.required],
      password: ['', [Validators.nullValidator, Validators.minLength(4)]],
      passwordConfirm: ['', Validators.nullValidator],
    }, formOptions);
  }

  // Conveniente para pegar um FormField apenas com a letra F
  get f(): any { return this.form.controls; }

  onSubmit(): void {
    this.atualizarUsuario();
  }

  public atualizarUsuario(): void {
    this.userUpdate = { ...this.form.value }
    this.spinner.show();
    this.accountService
      .updateUser(this.userUpdate)
      .subscribe(
        () => this.toaster.success('Usuário atualizado!', 'Sucesso'),
        (error) => {
          this.toaster.error(error.error);
          console.error(error);
        },
      )
      .add(() => this.spinner.hide());
  }

  public resetForm(event: any): void {
      event.preventDefault();
      this.form.reset();
  }
}

