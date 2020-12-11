import { Component, OnInit } from '@angular/core';
import {UserService} from './../../shared/user.service'
import {ToastrService} from 'ngx-toastr'
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit {

  constructor(public service: UserService, private toastr:ToastrService, private router: Router) { }

  ngOnInit() {
  }

  onSubmit(){
  	this.service.register()
         .subscribe(
         	/*(result)  => {
         	if(result){
         		localStorage.setItem('Bearer', result.token);
         		console.log(result);}*/
         (res: any) => {
        if (res.success) {
          this.service.formModel.reset();
          localStorage.setItem('token', res.token);
          this.router.navigateByUrl('/home');
        }
      },
      err => {
        console.log(typeof(err));
        console.log(err);
        if (err.status == 400){
          console.log(err);
          this.toastr.error(err.error.errors[0].description, 'Registration failed.');
        }
        else{
          this.toastr.error(err.error, 'Registration failed ...');
          console.log(err);
        }
      }
    );
         //}});
  }

}
