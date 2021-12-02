import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { SearchModel } from './searchfilter';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class FriendsService {
constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getFriendsByUserIdObservable (){
    return this.http.get<User[]>(this.baseUrl+'api/friends/user');
  }

  searchFriendsObservable (model: SearchModel){
    return this.http.post<User[]>(this.baseUrl+'api/users/filtered', model);
  }

  addFriendObservable(model:User){
    return this.http.post<User>(this.baseUrl+'api/friends/addfriend', model);
  }
}
