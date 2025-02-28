import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../environments/environment.development";
import type { Observable } from "rxjs";
import type { Expense } from "../models/expense";

@Injectable({ providedIn: "root" })
export class ExpenseHistoryService {
	private http = inject(HttpClient);
	private apiUrl = environment.apiURL + "/api/history";

	constructor() {}

	public addItem(itemToAdd: Expense): Observable<Expense> {
		return this.http.post<Expense>(this.apiUrl, itemToAdd);
		// this.http.post(this.apiUrl, itemToAdd).subscribe(newItem => {
		//   console.log('Added new item: ', newItem);
		// });
	}

	public getAllItems(): Observable<Expense[]> {
		return this.http.get<Expense[]>(this.apiUrl);
	}

	public getItemById(): any {}

	public getItemByCategory(): any {}

	public updateItem(): any {}

	public deleteItem(): void {}
}
