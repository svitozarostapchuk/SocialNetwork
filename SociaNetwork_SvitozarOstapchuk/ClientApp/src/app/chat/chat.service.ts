import { Inject, Injectable, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';          // import signalR
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { Message } from './message';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private  connection: any = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44395/chatsocket")   // mapping to the chathub as in startup.cs
    .configureLogging(signalR.LogLevel.Information)
    .build();
  private receivedMessageObject: Message = new Message();
  private sharedObj = new Subject<Message>();

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { 
    this.connection.onclose(async () => {
      await this.start();
    });
   this.connection.on("ReceiveOne", (user1, user2, message) => { this.mapReceivedMessage(user1,user2, message); });
   this.start();                 
  }


  // Start the connection
  public async start() {
    try {
      await this.connection.start();
      console.log("connected");
    } catch (err) {
      console.log(err);
      setTimeout(() => this.start(), 5000);
    } 
  }

  private mapReceivedMessage(user1: string, user2: string, message: string): void {
    this.receivedMessageObject.user1 = user1;
    this.receivedMessageObject.user2 = user2;
    this.receivedMessageObject.messageText = message;
    this.sharedObj.next(this.receivedMessageObject);
  }

  /* ****************************** Public Mehods ********************************** */

  // Calls the controller method
  public broadcastMessage(msgDto: any) {
    this.http.post(this.baseUrl+'api/chat/send', msgDto).subscribe(data => console.log(data));
    //this.connection.invoke("SendMessage1", msgDto.user, msgDto.messageText).catch(err => console.error(err));    // This can invoke the server method named as "SendMethod1" directly.
  }

  public retrieveMappedObject(): Observable<Message> {
    return this.sharedObj.asObservable();
  }

  deleteMessage(){
    return this.http.delete<Message>(this.baseUrl+'api/chats/');
  }

}
