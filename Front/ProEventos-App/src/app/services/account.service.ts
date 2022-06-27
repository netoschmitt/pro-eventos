import { map, take } from 'rxjs/operators';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '@app/models/identity/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiURL + 'api/account/';
  constructor(private http: HttpClient) { }

  public login(model: any): Observable<void> {
    return this.http.post<User>(this.baseUrl + 'login', model).pipe(
      take(1),
      map((response: User) => {
        const user = response;
        if (user) {

        }
      })
    );
  }

}
