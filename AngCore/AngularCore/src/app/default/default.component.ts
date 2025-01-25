import { Component,OnInit } from '@angular/core';
import { AddEmployeeService } from '../services/add-employee.service';
import {Employee} from '../Models/Employee'
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router,RouterStateSnapshot  } from '@angular/router';
@Component({
  selector: 'app-default',
  templateUrl: './default.component.html',
  styleUrls: ['./default.component.css']
})
export class DefaultComponent implements OnInit {
  employees: Array<Employee> = [];
  FirstName: any;
  p:number = 1;
  message: string | null = null;
  showDeleteMessage: boolean = false;
  constructor(private EmployeeService: AddEmployeeService,private router: Router,private route: ActivatedRoute){}
  
  ngOnInit(){
    this.getEmployees()
     this.route.queryParams.subscribe(params => {
      this.message = params['message'] || null;
      this.ResetMessage();
    });
    
  }

  getEmployees(){
    this.EmployeeService.getEmployees().subscribe(employees => {
      this.employees = employees;
      console.log(this.employees)
    })
  }
  deleteEmployee(id:string){
    if (confirm('Are you sure you want to delete this employee?')){
      this.EmployeeService.deleteEmployee(id).subscribe(res =>{
        this.getEmployees();
        // this.showDeleteMessage = true;
        this.message = "Deleted";
        this.ResetMessage()
      })
      console.log(id)
    }
    
  }

  Search(){
    if(this.FirstName==""){
      this.ngOnInit();
    }else{
      this.employees = this.employees.filter(res =>{
        // return res.Email.toLocaleLowerCase().match(this.FirstName.toLocaleLowerCase());
        return res.FirstName.toLocaleLowerCase().match(this.FirstName.toLocaleLowerCase()) || res.Email.toLocaleLowerCase().match(this.FirstName.toLocaleLowerCase());
      })
    }
  }

  key: string = '';
  reverse: boolean = false;
  sort(key:string){
    this.key = key;
    this.reverse = !this.reverse;
  }

  ResetMessage(){
    if (this.message != null){
      this.message = this.message + " succesfully!"
    }
   
    setTimeout(() => {
      this.message = null;
    }, 5000);
  }
  
}
