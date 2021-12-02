import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/user';
import { UserService } from 'src/app/user.service';
import { ChatService } from '../chat.service';
import { Message } from '../message';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})

export class ChatComponent implements OnInit {
  title = 'app';
  user$: Observable<User>
  user:  User;
  
  constructor(private chatService: ChatService, private userService: UserService) {}
  msgDto: Message = new Message();
  
  msgInboxArray: Message[] = [];
  
  ngOnInit(): void {
    this.chatService.retrieveMappedObject().subscribe( (receivedObj: Message) => { this.addToInbox(receivedObj);});  // calls the service method to get the new messages sent
    this.user$ = this.userService.getCurrentUserByIdObservable();
    this.user$.subscribe(x=> {this.user = x;} );                                      
  }

  send(): void {  
    this.msgDto.user1 = this.user.userName;
    this.msgDto.user2 =   sessionStorage.getItem('friendUsername');
    this.chatService.broadcastMessage(this.msgDto); 
    this.msgDto.messageText = '';               
  }

  delete() : void {
    this.chatService.deleteMessage().subscribe();
  }

  addToInbox(obj: Message) {
    let newObj = new Message();
    newObj.user1 = obj.user1;
    newObj.user2 = obj.user2;
    newObj.messageText = obj.messageText;
    this.msgInboxArray.push(newObj);
  }
}

