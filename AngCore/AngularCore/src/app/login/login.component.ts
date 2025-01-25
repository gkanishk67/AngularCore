import { Component, OnInit } from '@angular/core';
import {FormControl,FormBuilder,FormGroup, Validators} from '@angular/forms'
import { AuthService } from '../services/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  animations: [
    trigger('fadeIn', [
      state('show', style({ opacity: 1 })),
      transition('void => *', [style({ opacity: 0 }), animate(300)]),
      transition('* => void', [animate(300, style({ opacity: 0 }))]),
    ]),
  ]
})
export class LoginComponent implements OnInit {
loginForm : FormGroup;
  constructor(private fb: FormBuilder,private authService: AuthService,public spinner: NgxSpinnerService,private router: Router) {
    this.loginForm = this.fb.group({
      'username' : ['',[Validators.required]],
      'password'  : ['',[Validators.required]]
    })
  }

  ngOnInit(){
    
  }

  validationErrors: string[] = [];
  login(){
    this.spinner.show();
    this.validationErrors = [];
    if (this.loginForm.invalid) {
      this.validationErrors = Object.keys(this.loginForm.controls)
        .filter(key => this.loginForm.controls[key].invalid)
        .map(key => `${key} is required`);
        window.scrollTo(0, 0);
        this.spinner.hide();
      return;
    }
    this.authService.login(this.loginForm.value).subscribe(data =>{
      this.authService.saveToken(data);
      this.router.navigate(['/Employee']);
    },
    (error) => {
      if (error.status === 401) {
        // Handle unauthorized error here
        this.validationErrors.push('Invalid username or password');
        console.log('Invalid username or password');
        window.scrollTo(0, 0);
        this.spinner.hide();
      return;
      } else {
        // Handle other errors here
        console.error(error);
      }
    })
    this.spinner.hide();
  }

  get username(){
    return this.loginForm.get('username');
  }

  get password(){
    return this.loginForm.get('password');
  }
  
}
