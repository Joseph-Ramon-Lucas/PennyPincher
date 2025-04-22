import {
	Component,
	inject,
	input,
	type InputSignal,
	type OnChanges,
} from "@angular/core";
import type { ChartConfiguration } from "chart.js";
import { BaseChartDirective } from "ng2-charts";
import { AnalysisComparisonDto } from "../../models/analysis";
import { CashFlowDto } from "../../models/cashflow";
import { UtilityService } from "../../services/utility.service";
import { CATEGORY_TYPES } from "../../models/expense";

@Component({
	selector: "app-comparison-chart",
	imports: [BaseChartDirective],
	templateUrl: "./comparison-chart.component.html",
	styleUrl: "./comparison-chart.component.css",
	standalone: true,
})
export class ComparisonChartComponent implements OnChanges {
	private utilityService = inject(UtilityService);
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
	public comparisons: InputSignal<AnalysisComparisonDto> = input(
		this.DEFAULT_ANALYSIS_COMPARISON,
	);
	public expenseCategoryTypes = input([CATEGORY_TYPES.None]);

	public barChartLegend = true;
	public barChartPlugins = [];
	public barChartOptions: ChartConfiguration<"bar">["options"] = {
		responsive: false,
	};

	public labels = new Array(this.expenseCategoryTypes().length).fill(0);
	public currData = new Array(this.expenseCategoryTypes().length).fill(0);
	public projectedData = new Array(this.expenseCategoryTypes().length).fill(0);

	public barChartData: ChartConfiguration<"bar">["data"] = {
		labels: [],
		datasets: [
			{ data: [], label: "Series A" },
			{ data: [], label: "Series B" },
		],
	};

	ngOnChanges(): void {
		this.comparisons().currentCategorySum.map((catSum) => {
			this.currData[catSum.category - 1] = catSum.amount;
		});

		this.comparisons().projectedCategorySum.map((catSum) => {
			this.projectedData[catSum.category - 1] = catSum.amount;
		});

		this.expenseCategoryTypes().map((category) => {
			if (this.currData[category - 1] || this.projectedData[category - 1]) {
				this.labels[category - 1] =
					this.utilityService.categoryEnumToString(category);
			}
		});
		// for (let i = 0; i < this.currData.length; i++) {
		// 	if (this.currData[i] === 0 && this.projectedData[i] === 0) {
		// 		this.currData.splice(i, 1);
		// 		this.projectedData.splice(i, 1);
		// 		this.labels.splice(i, 1);
		// 	}
		// }

		console.log(
			"LABEL",
			this.labels,
			"curr",
			this.currData,
			"proj",
			this.projectedData,
		);
		this.barChartData = {
			labels: this.labels,
			datasets: [
				{
					data: this.currData,
					label: "Current Expenses",
				},
				{
					data: this.projectedData,
					label: "Projected Expenses",
				},
			],
		};
	}
}
