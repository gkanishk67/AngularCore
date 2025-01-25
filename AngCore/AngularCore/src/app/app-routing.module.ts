import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeDetailComponent } from './employee-detail/employee-detail.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuardService } from './services/auth-guard.service';
import {DefaultComponent} from './default/default.component';
const routes: Routes = [
  {path: 'login',component:LoginComponent},
  {path: 'register',component: RegisterComponent},
  {path: 'EmployeeDetail',component: EmployeeDetailComponent, canActivate: [AuthGuardService]},
  {path: 'Employee',component: DefaultComponent, canActivate: [AuthGuardService]},
  {path: 'EmployeeDetail/:id',component: EmployeeDetailComponent, canActivate: [AuthGuardService]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
