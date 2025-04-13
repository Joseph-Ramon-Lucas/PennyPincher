import {
	Component,
	inject,
	input,
	type InputSignal,
	type OnChanges,
	SimpleChanges,
} from "@angular/core";
import type { ChartConfiguration } from "chart.js";
import { BaseChartDirective } from "ng2-charts";
import { AnalysisComparisonDto } from "../../models/analysis";
import { CashFlowDto } from "../../models/cashflow";
import { UtilityService } from "../../services/utility.service";

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

	public barChartLegend = true;
	public barChartPlugins = [];
	public barChartOptions: ChartConfiguration<"bar">["options"] = {
		responsive: true,
	};

	public barChartData: ChartConfiguration<"bar">["data"] = {
		labels: [],
		datasets: [
			{ data: [], label: "Series A" },
			{ data: [], label: "Series B" },
		],
	};

	ngOnChanges(): void {
		const labels = [
			// find the union of both category lists with set to remove duplicates
			...new Set([
				...this.comparisons().currentCategorySum.map((cf) =>
					this.utilityService.categoryEnumToString(cf.category),
				),
				...this.comparisons().projectedCategorySum.map((cf) =>
					this.utilityService.categoryEnumToString(cf.category),
				),
			]),
		];
		const currData = [
			...this.comparisons().currentCategorySum.map((cf) => cf.amount),
		];
		const projData = [
			...this.comparisons().projectedCategorySum.map((cf) => cf.amount),
		];
		console.group("LABEL", labels, "curr", currData, "proj", projData);
		console.groupEnd;
		this.barChartData = {
			labels: labels,
			datasets: [
				{
					data: [
						...this.comparisons().currentCategorySum.map((cf) => cf.amount),
					],
					label: "Current Expenses",
				},
				{
					data: [
						...this.comparisons().projectedCategorySum.map((cf) => cf.amount),
					],
					label: "Projected Expenses",
				},
			],
		};
	}
}
