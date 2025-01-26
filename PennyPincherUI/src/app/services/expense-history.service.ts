import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ExpenseHistoryService {

  private http = inject(HttpClient);
  private apiUrl = environment.apiURL + '/api/history';

  constructor() {}

  public addItem(itemToAdd : any): Observable<any> {
    return this.http.post(this.apiUrl, itemToAdd);
    // this.http.post(this.apiUrl, itemToAdd).subscribe(newItem => {
    //   console.log('Added new item: ', newItem);
    // });
  }

  public getAllItems(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  public getItemById(): any {

  }

  public getItemByCategory(): any {

  }

  public updateItem(): any {

  }

  public deleteItem(): void {

  }
}
