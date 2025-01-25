import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { AddEmployeeService } from '../services/add-employee.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from '../Models/Employee';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent  {
  detailForm : FormGroup;
  id : string = "0";
  employee: Employee | null = null;
  showNotFound: boolean = false;
  constructor(private fb: FormBuilder, private AddEmployeeService: AddEmployeeService,private route: ActivatedRoute,private router: Router) {
    this.detailForm = this.fb.group({
      'Id': ['0'],
      'FirstName' : ['',[Validators.required]],
      'MiddleName' : ['',[Validators.required]],
      'LastName' : ['',[Validators.required]],
      'Father' : ['',[Validators.required]],
      'Email' : ['',[Validators.required]],
      'DOB' : ['',[Validators.required]],
      'Program' : ['',[Validators.required]],
      'Region' : ['',[Validators.required]],
      'Address' : ['',[Validators.required]],
      'Contact' : ['',[Validators.required]],
      'Gender' : ['',[Validators.required]]
    })
    
    this.route.params.subscribe(res =>{
      this.id = res['id'];
      
      if(this.id != undefined){
        console.log("test")
         this.AddEmployeeService.getEmployee(this.id).subscribe(res =>{
        this.employee = res;
        // console.log(this.employee);
       
        this.detailForm = this.fb.group({
          'Id': [this.employee.Id],
          'FirstName' : [this.employee.FirstName,[Validators.required]],
          'MiddleName' : [this.employee.MiddleName,[Validators.required]],
          'LastName' : [this.employee.LastName,[Validators.required]],
          'Father' : [this.employee.Father,[Validators.required]],
          'Email' : [this.employee.Email,[Validators.required]],
          'DOB' : [this.employee.DOB,[Validators.required]],
          'Program' : [this.employee.Program,[Validators.required]],
          'Region' : [this.employee.Region,[Validators.required]],
          'Address' : [this.employee.Address,[Validators.required]],
          'Contact' : [this.employee.Contact,[Validators.required]],
          'Gender' : [this.employee.Gender,[Validators.required]]
        });
        console.log(this.detailForm.value);

      },
      error => {
        if (error.status === 404) {
          this.showNotFound = true;
     setTimeout(() => {
      this.showNotFound = false;
      this.id = "0";
    }, 5000);
          return;
        }
        console.log('An error occurred while getting the employee', error);
      }
      )
      }
      else{
        this.id = "0";
      }
     
    })
    console.log(this.id);
  }
  validationErrors: string[] = [];

  add(){

    if (this.detailForm.invalid) {
      console.log(this.detailForm.controls)
      this.validationErrors = Object.keys(this.detailForm.controls)
        .filter(key => this.detailForm.controls[key].invalid)
        .map(key => `${key} is required`);
        window.scrollTo(0, 0);
      return;
    }
    
    this.AddEmployeeService.add(this.detailForm.value).subscribe(res => {
      this.router.navigate(['/Employee'], { queryParams: { message: 'Added' } });
    }

    )
  }

  editEmployee(){
    if (this.detailForm.invalid) {
      this.validationErrors = Object.keys(this.detailForm.controls)
        .filter(key => this.detailForm.controls[key].invalid)
        .map(key => `${key} is also required`);
        window.scrollTo(0, 0);
      return;
    }
    this.AddEmployeeService.editEmployee(this.detailForm.value).subscribe(res =>{
      this.router.navigate(['/Employee'], { queryParams: { message: 'updated' } });
    })
  }

}
