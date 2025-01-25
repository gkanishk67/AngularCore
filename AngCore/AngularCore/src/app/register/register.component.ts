import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm : FormGroup;
  constructor(private fb: FormBuilder,private authService: AuthService,private router: Router){
    this.registerForm = this.fb.group({
      'username':['',[Validators.required]],
      'email':['',[Validators.required]],
      'password':['',[Validators.required]]
    })
  }

  ngOnInit(): void {
    
  }
  validationErrors: string[] = [];
  register(){
    this.validationErrors = [];
    if (this.registerForm.invalid) {
      this.validationErrors = Object.keys(this.registerForm.controls)
        .filter(key => this.registerForm.controls[key].invalid)
        .map(key => `${key} is required`);
        window.scrollTo(0, 0);
        
      return;
    }
    this.authService.register(this.registerForm.value).subscribe(data =>{
      console.log(data);
      this.router.navigate(['/login']);
    },
    error => {
      if (error.error && Array.isArray(error.error)) {
        this.validationErrors = error.error.map((e: { description: any; }) => e.description);
      } else {
        this.validationErrors = ['An error occurred. Please try again later.'];
      }
      window.scrollTo(0, 0);
    })
  }

  get username(){
    return this.registerForm.get('username');
  }

  get email(){
    return this.registerForm.get('email');
  }

  get password(){
    return this.registerForm.get('password');
  }

}
