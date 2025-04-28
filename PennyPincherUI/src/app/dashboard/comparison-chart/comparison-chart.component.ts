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

	// hASH map? {label: [curr, proj]}
	public chartData: {
		[key: string]: number[];
	} = {};
	public barChartData: ChartConfiguration<"bar">["data"] = {
		labels: [],
		datasets: [
			{ data: [], label: "Series A" },
			{ data: [], label: "Series B" },
		],
	};

	public updateChartData(): void {
		//prefilling all categories with [0 current, 0 projected]
		this.expenseCategoryTypes().map((category: CATEGORY_TYPES) => {
			this.chartData[this.utilityService.categoryEnumToString(category)] = [
				0, 0,
			];
		});

		//fill each current / projected with actual data
		this.comparisons().currentCategorySum.map((catSum) => {
			this.chartData[
				this.utilityService.categoryEnumToString(catSum.category)
			][0] = catSum.amount;
		});
		this.comparisons().projectedCategorySum.map((catSum) => {
			this.chartData[
				this.utilityService.categoryEnumToString(catSum.category)
			][1] = catSum.amount;
		});

		//just need to delete the categories that are unused with 0,0
		Object.keys(this.chartData).map((category) => {
			if (
				this.chartData[category][0] === 0 &&
				this.chartData[category][1] === 0
			) {
				delete this.chartData[category];
			}
		});
	}

	ngOnChanges(): void {
		this.updateChartData();
		this.barChartData = {
			labels: Object.keys(this.chartData),
			datasets: [
				{
					data: Object.values(this.chartData).map((n) => n[0]),
					label: "Current Expenses",
				},
				{
					data: Object.values(this.chartData).map((n) => n[1]),
					label: "Projected Expenses",
				},
			],
		};
	}
}
