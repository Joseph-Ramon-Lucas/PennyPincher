import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../environments/environment.development";
import type { CashFlowDto } from "../models/cashflow";
import type { Observable } from "rxjs";
import type {
	AnalysisComparisonDto,
	AnalysisStatusDto,
	CFDataStores,
} from "../models/analysis";

@Injectable({
	providedIn: "root",
})
export class AnalysisService {
	private apiUrl = `${environment.apiURL}/api/analysis`;
	private http = inject(HttpClient);

	public UpdateCashFlowItemLogStore(): Observable<CashFlowDto[]> {
		return this.http.post<CashFlowDto[]>(this.apiUrl, {});
	}

	public GetCashFlowItemLogStore(): Observable<CashFlowDto[]> {
		return this.http.get<CashFlowDto[]>(this.apiUrl);
	}

	public Status(DataStore: CFDataStores): Observable<AnalysisStatusDto> {
		return this.http.get<AnalysisStatusDto>(
			`${this.apiUrl}/getstatus/${DataStore}`,
		);
	}

	public CompareCashFlows(
		DataStore: CFDataStores,
	): Observable<AnalysisComparisonDto> {
		return this.http.get<AnalysisComparisonDto>(
			`${this.apiUrl}/getstatus/compare/${DataStore}`,
		);
	}
}
