import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Employee} from '../Models/Employee';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
@Injectable({
  providedIn: 'root'
})
export class AddEmployeeService {
  private addPath = 'https://localhost:44303/home/create';
  private getPath = 'https://localhost:44303/home';
  private editPath = 'https://localhost:44303/home/edit';
  private deletePath = 'https://localhost:44303/home/delete';
  constructor(private http: HttpClient) {

   }

   add(data:any): Observable<Employee>{
    return this.http.post<Employee>(this.addPath,data)
   }

   editEmployee(data:any){
    return this.http.put(this.editPath,data)
   }

   getEmployees(): Observable<Array<Employee>>{
    return this.http.get<Array<Employee>>(this.getPath)
   }

   getEmployee(id:string):Observable<Employee>{
    return this.http.get<Employee>(this.editPath + '/' + id)
   }

   deleteEmployee(id:string){
    return this.http.delete(this.deletePath + '/' + id)
   }
}
