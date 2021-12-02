import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../user';
import { UserService } from '../user.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {
  users$:Observable<User[]>
  users: User[];

  constructor(private router: Router, private service: UserService) { }

  ngOnInit() {

    this.users$ = this.service.getUsersObservable();
    this.users$.subscribe(x=> {this.users = x;} );
  }

  deleteUser(id){
    this.service.deleteUser(id).subscribe();
    window.location.reload();
  }
}