import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../environments/environment.development";
import {
	type CashFlowDto,
	type CashFlowUpdateDto,
	FLOW_TYPES,
} from "../models/cashflow";
import { Subject, type Observable } from "rxjs";
import { FormControl, FormGroup } from "@angular/forms";
import { CATEGORY_TYPES } from "../models/expense";

@Injectable({
	providedIn: "root",
})
export class CashFlowService {
	private http = inject(HttpClient);
	private apiUrl = `${environment.apiURL}/api/cashflow`;

	public submissionComplete$ = new Subject<void>();
	public destroySubject$ = new Subject<void>();

	public cashFlowForm = new FormGroup({
		id: new FormControl(0),
		name: new FormControl(""),
		description: new FormControl(""),
		amount: new FormControl(0),
		flow: new FormControl(FLOW_TYPES.income),
		category: new FormControl(CATEGORY_TYPES.None),
	});

	public CreateFlow(cashFlow: CashFlowDto): Observable<CashFlowDto> {
		return this.http.post<CashFlowDto>(this.apiUrl, cashFlow);
	}

	public GetAllFlows(targetFlowType?: FLOW_TYPES): Observable<CashFlowDto[]> {
		if (targetFlowType) {
			return this.http.get<CashFlowDto[]>(
				`${this.apiUrl}?targetFlowType=${targetFlowType}`,
			);
		}
		return this.http.get<CashFlowDto[]>(this.apiUrl);
	}

	public GetFlow(targetCashFlowID: number): Observable<CashFlowDto> {
		return this.http.get<CashFlowDto>(`${this.apiUrl}/${targetCashFlowID}`);
	}

	public UpdateFlow(
		targetCashFlowID: number,
		newCashFlow: CashFlowUpdateDto,
	): Observable<CashFlowDto> {
		return this.http.put<CashFlowDto>(
			`${this.apiUrl}/${targetCashFlowID}`,
			newCashFlow,
		);
	}

	public DeleteFlow(targetCashFlowID: number): Observable<CashFlowDto> {
		return this.http.delete<CashFlowDto>(`${this.apiUrl}/${targetCashFlowID}`);
	}
}
