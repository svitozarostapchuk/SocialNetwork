import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from 'src/app/user';
import { UserService } from 'src/app/user.service';

@Component({
  selector: 'app-friend-profile',
  templateUrl: './friend-profile.component.html',
  styleUrls: ['./friend-profile.component.css']
})
export class FriendProfileComponent implements OnInit {
  user$:Observable<User>
  user: User;
  
  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {
    let id = parseInt(sessionStorage.getItem('id'));
    this.user$ = this.service.getUserByIdObservable(id);
    this.user$.subscribe(x=> {this.user = x;} );
  }
}