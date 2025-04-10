import {
	Component,
	inject,
	type OnInit,
	signal,
	type WritableSignal,
} from "@angular/core";
import {
	type CFDataStores,
	AnalysisStatusDto,
	AnalysisComparisonDto,
} from "../models/analysis";
import { CashFlowDto } from "../models/cashflow";
import { AnalysisService } from "../services/analysis.service";
import { MatCardModule } from "@angular/material/card";
import { MatButtonModule } from "@angular/material/button";

@Component({
	selector: "app-dashboard",
	imports: [MatCardModule, MatButtonModule],
	templateUrl: "./dashboard.component.html",
	styleUrl: "./dashboard.component.css",
})
export class DashboardComponent implements OnInit {
	private analysisService = inject(AnalysisService);
	private dataStore: CFDataStores = 1;
	private DEFAULT_ANALYSIS_STATUS = new AnalysisStatusDto(0, 0, 0, 0, "", 0, 0);
	private DEFAULT_ANALYSIS_COMPARISON = new AnalysisComparisonDto(
		[new CashFlowDto(0, "", "", 0, 0, 0)],
		[new CashFlowDto(0, "", "", 0, 0, 0)],
		[new CashFlowDto(0, "", "", 0, 0, 0)],
		[new CashFlowDto(0, "", "", 0, 0, 0)],
		0,
		0,
		0,
		0,
		0,
	);
	public statuses: WritableSignal<AnalysisStatusDto> = signal(
		this.DEFAULT_ANALYSIS_STATUS,
	);
	public comparisons: WritableSignal<AnalysisComparisonDto> = signal(
		this.DEFAULT_ANALYSIS_COMPARISON,
	);

	ngOnInit(): void {
		this.getFinancialData();
	}

	getFinancialData(): void {
		this.analysisService.UpdateCashFlowItemLogStore().subscribe({
			complete: () => {
				this.analysisService
					.Status(this.dataStore)
					.subscribe((result: AnalysisStatusDto) => {
						console.log("API RESULT", result.grossIncome);

						this.statuses.set(result);
					});
				this.analysisService
					.CompareCashFlows(this.dataStore)
					.subscribe((result: AnalysisComparisonDto) => {
						this.comparisons.set(result);
					});
			},
		});
	}

	printStatus(): void {
		this.getFinancialData();
		console.log(this.statuses());
		console.log("test hte gfross data", this.statuses()?.grossIncome ?? 0);

		console.log(this.comparisons());
	}
}
