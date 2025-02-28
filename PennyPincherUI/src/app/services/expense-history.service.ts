import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../environments/environment.development";
import type { Observable } from "rxjs";
import {
	CATEGORY_TYPES,
	Expense,
	ExpenseForUpdateDto,
} from "../models/expense";

@Injectable({ providedIn: "root" })
export class ExpenseHistoryService {
	private http = inject(HttpClient);
	private apiUrl = `${environment.apiURL}/api/history`;
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

	public getItemById(itemId: number): Observable<Expense[]> {
		return this.http.get<Expense[]>(`${this.apiUrl}/${itemId}`);
	}

	public getItemByCategory(
		categoryType: CATEGORY_TYPES,
	): Observable<Expense[]> {
		return this.http.get<Expense[]>(
			`${this.apiUrl}/${categoryType}/getcategoryitems`,
		);
	}

	public updateItem(
		itemId: number,
		itemWithUpdate: ExpenseForUpdateDto,
	): Observable<ExpenseForUpdateDto> {
		return this.http.put<ExpenseForUpdateDto>(
			`${this.apiUrl}/${itemId}`,
			itemWithUpdate,
		);
	}

	public deleteItem(itemToDelete: Expense): Observable<Expense> {
		return this.http.delete<Expense>(this.apiUrl);
	}
}
