import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';
import { Token } from '@angular/compiler';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginPath = 'https://localhost:44303/identity/login'
  private registerPath = 'https://localhost:44303/identity/register'
  constructor(private http: HttpClient) { }

  login(data: any): Observable<any> {
    return this.http.post(this.loginPath,data)
  }

  register(data: any): Observable<any> {
    return this.http.post(this.registerPath,data)
  }

  saveToken(token: string){
    
    const data= JSON.parse(JSON.stringify(token));
    const token1 = data.token;
    localStorage.setItem('token',token1)
  }
  
  getToken(){
    return localStorage.getItem('token')
  }
  

  isAuthenticated(){
    if (this.getToken()){
      return true
    }
    return false
  }
}


