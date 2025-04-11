import {
	ChangeDetectionStrategy,
	Component,
	inject,
	type OnInit,
	signal,
	type WritableSignal,
} from "@angular/core";
import {
	AnalysisStatusDto,
	AnalysisComparisonDto,
	CFDataStores,
} from "../models/analysis";
import { CashFlowDto } from "../models/cashflow";
import { AnalysisService } from "../services/analysis.service";
import { MatCardModule } from "@angular/material/card";
import { MatButtonModule } from "@angular/material/button";
import { UtilityService } from "../services/utility.service";
import { BaseChartDirective } from "ng2-charts";
import type { ChartConfiguration } from "chart.js";

@Component({
	selector: "app-dashboard",
	imports: [MatCardModule, MatButtonModule, BaseChartDirective],
	templateUrl: "./dashboard.component.html",
	styleUrl: "./dashboard.component.css",
	changeDetection: ChangeDetectionStrategy.Default,
})
export class DashboardComponent implements OnInit {
	private analysisService = inject(AnalysisService);
	private utilityService = inject(UtilityService);
	private dataStore: WritableSignal<CFDataStores> = signal(
		CFDataStores.Projected,
	);
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
		0,
	);
	protected categoryEnumToString = this.utilityService.categoryEnumToString;
	protected statuses: WritableSignal<AnalysisStatusDto> = signal(
		this.DEFAULT_ANALYSIS_STATUS,
	);
	protected comparisons: WritableSignal<AnalysisComparisonDto> = signal(
		this.DEFAULT_ANALYSIS_COMPARISON,
	);

	public barChartLegend = true;
	public barChartPlugins = [];

	public barChartData: ChartConfiguration<"bar">["data"] = {
		labels: ["2006", "2007", "2008", "2009", "2010", "2011", "2012"],
		datasets: [
			{ data: [65, 59, 80, 81, 56, 55, 40], label: "Series A" },
			{ data: [28, 48, 40, 19, 86, 27, 90], label: "Series B" },
		],
	};

	public barChartOptions: ChartConfiguration<"bar">["options"] = {
		responsive: false,
	};

	ngOnInit(): void {
		this.getFinancialData();
	}

	getFinancialData(): void {
		this.analysisService.UpdateCashFlowItemLogStore().subscribe({
			complete: () => {
				this.analysisService
					.Status(this.dataStore())
					.subscribe((result: AnalysisStatusDto) => {
						this.statuses.set(result);
					});
				this.analysisService
					.CompareCashFlows(this.dataStore())
					.subscribe((result: AnalysisComparisonDto) => {
						console.log(
							"comparison data:",
							result,
							"datastore:",
							this.dataStore(),
						);
						this.comparisons.set(result);
					});
			},
		});
	}

	printStatus(cfType: number): void {
		if (cfType === 0) {
			this.dataStore.set(CFDataStores.Current);
		} else {
			this.dataStore.set(CFDataStores.Projected);
		}
		this.getFinancialData();
		console.log(this.statuses());
		console.log("test hte gfross data", this.statuses()?.grossIncome ?? 0);

		console.log(this.comparisons());
	}
}
