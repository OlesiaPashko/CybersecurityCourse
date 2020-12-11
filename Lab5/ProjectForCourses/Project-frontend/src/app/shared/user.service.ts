import { Injectable } from '@angular/core';
import {FormBuilder, Validators, FormGroup} from '@angular/forms';
import {HttpClient, HttpHeaders} from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb:FormBuilder, private http:HttpClient) { }

  readonly BaseURI = 'https://localhost:44361/api';

  formModel = this.fb.group({
  	UserName : ['', Validators.required],
  	Email : ['', [Validators.email, Validators.required]],
  	FirstName : [''],
    LastName: [''],
  	Passwords: this.fb.group({
		Password : ['', [Validators.required, Validators.minLength(8)]],
  		ConfirmPassword : ['', Validators.required]
  	}, {validator : this.comparePasswords})
  });
  comparePasswords(fb:FormGroup){
  	let confirmPsswrdCtrl = fb.get('ConfirmPassword');
  	if(confirmPsswrdCtrl.errors == null || 'passwordMismatch' in confirmPsswrdCtrl.errors){
  		if(fb.get('Password').value!=confirmPsswrdCtrl.value){
  			confirmPsswrdCtrl.setErrors({passwordMismatch:true});
  		}
  		else
  			confirmPsswrdCtrl.setErrors(null);	
  		}
  	}

  	register(){
  		var body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      Password: this.formModel.value.Passwords.Password,
      FirstName:this.formModel.value.FirstName,
      LastName: this.formModel.value.LastName
    };
    return this.http.post(this.BaseURI + '/account/register', body);
  	}

    login(formData) {
      //console.log(formData);
      //console.log(this.http.post(this.BaseURI + '/account/login', formData));
      return this.http.post(this.BaseURI + '/account/login', formData);
  }

  getUserProfile() {
    return this.http.get(this.BaseURI + '/UserProfile');
  }
}
