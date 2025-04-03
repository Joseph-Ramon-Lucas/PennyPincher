import { Component, inject, type OnInit } from "@angular/core";
import { AnalysisService } from "../../services/analysis.service";
import {
	AnalysisComparisonDto,
	AnalysisStatusDto,
	type CFDataStores,
} from "../../models/analysis";
import { CashFlowDto } from "../../models/cashflow";

@Component({
	selector: "app-analysis",
	imports: [],
	templateUrl: "./analysis.component.html",
	styleUrl: "./analysis.component.css",
})
export class AnalysisComponent implements OnInit {
	private analysisService = inject(AnalysisService);
	private dataStore: CFDataStores = 1;
	protected statuses: AnalysisStatusDto = new AnalysisStatusDto(
		0,
		0,
		0,
		0,
		"",
		0,
		0,
	);
	protected comparisons: AnalysisComparisonDto = new AnalysisComparisonDto(
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

	ngOnInit(): void {
		this.getFinancialData();
	}

	getFinancialData(): void {
		this.analysisService.UpdateCashFlowItemLogStore().subscribe({
			complete: () => {
				this.analysisService
					.Status(this.dataStore)
					.subscribe((result: AnalysisStatusDto) => {
						this.statuses = result;
					});
				this.analysisService
					.CompareCashFlows(this.dataStore)
					.subscribe((result: AnalysisComparisonDto) => {
						this.comparisons = result;
					});
			},
		});
	}

	printStatus(): void {
		console.log(this.statuses);
		console.log(this.comparisons);
	}
}
