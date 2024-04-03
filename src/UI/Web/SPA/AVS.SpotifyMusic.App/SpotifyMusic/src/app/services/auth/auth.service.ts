import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject, map, take } from 'rxjs';
import { UserResponseLogin } from '../../models/identity/userResponseLogin';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSource = new ReplaySubject<UserResponseLogin>(1);
  public currentUser$ = this.currentUserSource.asObservable();
  private url = "https://localhost:7170/api/auth"

  constructor(private httpClient: HttpClient) { }

  public login(model: any): Observable<void> {

    return this.httpClient.post<UserResponseLogin>(`${this.url}/signin`, model).pipe(
      take(1),
      map((response: UserResponseLogin) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user)
        }
      })
    );

  }

  public register(model: any): Observable<void> {

    return this.httpClient.post<UserResponseLogin>(`${this.url}/signup`, model).pipe(
      take(1),
      map((response: UserResponseLogin) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user)
        }
      })
    );

  }

  public logout(): void {

    localStorage.removeItem('user');
    const user: any = null;
    this.currentUserSource.next(user);
    this.currentUserSource.complete();

  }

  public setCurrentUser(user: UserResponseLogin): void {

    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);

  }

}
