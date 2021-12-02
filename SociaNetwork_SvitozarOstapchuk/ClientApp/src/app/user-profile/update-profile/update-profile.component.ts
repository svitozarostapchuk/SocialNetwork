import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from 'src/app/user';
import { UserService } from 'src/app/user.service';

@Component({
  selector: 'update-profile',
  templateUrl: './update-profile.component.html',
  //styleUrls: ['edit-profile.component.css']
})

export class UpdateProfileComponent implements OnInit {

  constructor(public service: UserService, private toastr: ToastrService, private router:Router, private fb: FormBuilder) { }

  user$:Observable<User>
  user:User
  profileFormModel = this.fb.group({
    firstName: [''],
    lastName: [''],
    age: [''],
    email:[''],
    password: [''],
    phoneNumber: [''],
    city:[''],
    school:[''],
    university:[''],
    aboutUser:[''],
  });

  ngOnInit() {
    this.user$ = this.service.getCurrentUserByIdObservable();
    this.user$.subscribe(x=> {this.user = x;} );
  }

  onSubmit(userModel:User) {
    userModel.userName = this.user.userName;
    this.service.updateUser(userModel).subscribe(
      (res: any) => {
          this.profileFormModel.reset();
          this.toastr.success('Information has been updated!');
          this.router.navigateByUrl('');
      },
      err => {
        console.log(err);
      }
    );
  }
  
}
