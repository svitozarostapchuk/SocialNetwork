import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './User';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private fb: FormBuilder) { }
  
  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', Validators.email],
    FirstName: [''],
    LastName: [''],
    Age: [''],
    PhoneNumber: [''],
    City:[''],
    School:[''],
    University:[''],
    AboutUser:[''],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })
  });

  getCurrentUserByIdObservable (){
    return this.http.get<User>(this.baseUrl+'api/users/current');
  }

  getUserByIdObservable (id: number){
    return this.http.get<User>(this.baseUrl+'api/users/'+id);
  }

  getUsersObservable (){
    return this.http.get<User[]>(this.baseUrl+'api/users');
  }

  updateUser(user: User) {
    return this.http.patch<User>(this.baseUrl+'api/account/update', user);  
  }

  deleteUser(id){
    return this.http.delete<User>(this.baseUrl+'api/users/'+id);
  }

  getUserProfile() {
    return this.http.get(this.baseUrl + '/UserProfile');
  }

  register(bodyParameter){
    var body = {
      UserName: this.formModel.value.UserName,
      FirstName: this.formModel.value.FirstName,
      LastName: this.formModel.value.LastName,
      Age: this.formModel.value.Age,
      City: this.formModel.value.City,
      School: this.formModel.value.School,
      University: this.formModel.value.University,
      AboutUser: this.formModel.value.AboutUser,
      PhoneNumber: this.formModel.value.PhoneNumber,
      Email: this.formModel.value.Email,
      Password: this.formModel.value.Passwords.Password,
      Role:"user"
    };
    return this.http.post(this.baseUrl + 'api/account/register', body);
  }

  login(model: User){
    let result = this.http.post(this.baseUrl + 'api/account/login', model);
    
    return result;
  }

  roleMatch(allowedRoles): boolean {
    var isMatch = false;
    var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    var userRole = payLoad.role;
    allowedRoles.forEach(element => {
      if (userRole == element) {
        isMatch = true;
        return false;
      }
    });
    return isMatch;
  }

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }
}
  
