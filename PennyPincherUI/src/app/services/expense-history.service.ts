import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../environments/environment.development";
import type { Observable } from "rxjs";
import type {
	CATEGORY_TYPES,
	ExpenseDto,
	ExpenseForUpdateDto,
} from "../models/expense";

@Injectable({ providedIn: "root" })
export class ExpenseHistoryService {
	private http = inject(HttpClient);
	private apiUrl = `${environment.apiURL}/api/history`;
	constructor() {}

	public addItem(itemToAdd: ExpenseDto): Observable<ExpenseDto> {
		return this.http.post<ExpenseDto>(this.apiUrl, itemToAdd);
	}

	public getAllItems(): Observable<ExpenseDto[]> {
		return this.http.get<ExpenseDto[]>(this.apiUrl);
	}

	public getItemById(itemId: number): Observable<ExpenseDto[]> {
		return this.http.get<ExpenseDto[]>(`${this.apiUrl}/${itemId}`);
	}

	public getItemByCategory(
		categoryType: CATEGORY_TYPES,
	): Observable<ExpenseDto[]> {
		return this.http.get<ExpenseDto[]>(
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

	public deleteItem(itemToDelete: ExpenseDto): Observable<ExpenseDto[]> {
		const options = {
			headers: new HttpHeaders({
				"Content-Type": "application/json-patch+json",
			}),

			body: itemToDelete,
		};
		console.log("OPTIONS!!!", options);

		return this.http.delete<ExpenseDto[]>(this.apiUrl, options);
	}
}
