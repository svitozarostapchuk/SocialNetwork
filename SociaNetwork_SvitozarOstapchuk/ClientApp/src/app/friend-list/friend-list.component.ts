import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { FriendsService } from 'src/app/friends.service';
import { User } from 'src/app/user';

@Component({
  selector: 'friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendsListComponent implements OnInit {
  friends$:Observable<User[]>
  friends: User[];
  user$:Observable<User>
  user: User;
  constructor(private router: Router, private friendService: FriendsService) { }

  ngOnInit() {
    this.friends$ = this.friendService.getFriendsByUserIdObservable();
    this.friends$.subscribe(x=> {this.friends = x;} );
  }

  public setId(value){
    sessionStorage.setItem('id', value);
  }

  setDataForChat(friendUsername){
    sessionStorage.setItem('friendUsername', friendUsername);
  }

  public chat(){
    console.log(" going to chat page");
  }
}

