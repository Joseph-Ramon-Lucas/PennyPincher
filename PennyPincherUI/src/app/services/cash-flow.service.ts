import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../environments/environment.development";
import type {
	CashFlowDto,
	CashFlowUpdateDto,
	FLOW_TYPES,
} from "../models/cashflow";
import type { Observable } from "rxjs";

@Injectable({
	providedIn: "root",
})
export class CashFlowService {
	private http = inject(HttpClient);
	private apiUrl = `${environment.apiURL}/api/cashflow`;
	constructor() {}

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
