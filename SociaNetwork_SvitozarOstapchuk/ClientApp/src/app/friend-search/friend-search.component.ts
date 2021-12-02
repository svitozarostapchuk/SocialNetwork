import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { FriendsService } from 'src/app/friends.service';
import { User } from 'src/app/user';
import { UserService } from 'src/app/user.service';

@Component({
  selector: 'app-friend-search',
  templateUrl: './friend-search.component.html',
  styleUrls: ['./friend-search.component.css']
})
export class FriendSearchComponent implements OnInit {
  users$:Observable<User[]>
  users: User[];
  searchResults$:Observable<User[]>
  searchResults: User[];
  constructor(private router: Router,private userService: UserService, private friendsService: FriendsService, private fb: FormBuilder) { }

  searchFormModel = this.fb.group({
    Name: [''],
    AgeMin: [''],
    AgeMax: [''],
    Email:[''],
    City:[''],
    School:[''],
    University:[''],
  });

  ngOnInit() {
    this.users$ = this.userService.getUsersObservable();
    this.users$.subscribe(x=> {this.users = x;} );
    this.searchFormModel.reset();
  }

  search(searchModel:any) {
    this.searchResults$ =  this.friendsService.searchFriendsObservable(searchModel);
    this.searchResults$.subscribe(
      (res: any) => {
          this.searchResults = res;
          this.searchFormModel.reset();
      },
      err => {
        console.log(err);
      }
    );
  }
  
  addFriend(model){
    this.friendsService.addFriendObservable(model).subscribe();
  }
}
