import {
	Component,
	inject,
	signal,
	type WritableSignal,
	type OnInit,
} from "@angular/core";
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
	tempAnsly = new AnalysisStatusDto(0, 0, 0, 0, "", 0, 0);

	public statuses: WritableSignal<AnalysisStatusDto> = signal(this.tempAnsly);
	public comparisons: AnalysisComparisonDto = new AnalysisComparisonDto(
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
						console.log("API RESULT", result.grossIncome);

						this.statuses.set(result);
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
		console.log(this.statuses());
		console.log("test hte gfross data", this.statuses().grossIncome);

		console.log(this.comparisons);
	}
}
