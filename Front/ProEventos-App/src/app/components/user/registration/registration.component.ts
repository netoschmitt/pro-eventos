import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  form!: FormGroup;

  constructor(public fb: FormBuilder) { }

  ngOnInit(): void {
    this.validation();
  }

  private validation(): void {
    this.form = this.fb.group({
      primeiroNome: ['', Validators.required],
      ultimonome: ['', Validators.required],
      email: ['', Validators.required, Validators.email],
      username: ['', Validators.required],
      senha: ['',
      Validators.required,
      Validators.minLength(6)],
      confirmeSenha: ['', Validators.required],
    });
  }
}
