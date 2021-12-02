import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styles: []
})

export class LoginComponent implements OnInit {
  formModel = {
    UserName: '',
    Password: ''
  }
  constructor(private service: UserService, private router: Router, private toastr: ToastrService) {
   }

  ngOnInit() {
    if (localStorage.getItem('token') === null)
      this.router.navigateByUrl('/');
  }

  onSubmit(form: NgForm) {
    this.service.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        if(this.checkIsAdmin()){
          this.router.navigateByUrl('admin');
        }else{
          this.router.navigateByUrl('user-profile');
        }
      },
      err => {
        if (err.status == 400)
          this.toastr.error('Incorrect username or password.', 'Authentication failed.');
        else
          console.log(err);
      }
    );
  }

  checkIsAdmin():boolean{
    console.log(localStorage.getItem('token'));
    var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
      var userRole = payLoad.role;
      if (userRole == 'Administrator') {
        return true;
      }
    return false;
  }
}
