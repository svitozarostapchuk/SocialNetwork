import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../user';
import { UserService } from '../user.service';

@Component({
  selector: 'user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})

export class UserProfileComponent implements OnInit {
  user$:Observable<User>
  user: User;
  
  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    this.user$ = this.service.getCurrentUserByIdObservable();
    this.user$.subscribe(x=> {this.user = x;} );
  }

}
