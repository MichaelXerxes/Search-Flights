import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthserviceService {

  constructor() { }

  currentUser?: User;
  loginUser(user: User) {
    console.log("Log in the user with email" + user.email)
    this.currentUser = user
  }
}

interface User {
  email: string
}
