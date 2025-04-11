import {
	Component,
	input,
	type InputSignal,
	type OnChanges,
} from "@angular/core";
import type { ChartOptions } from "chart.js";
import { BaseChartDirective } from "ng2-charts";
import { AnalysisStatusDto } from "../../models/analysis";

@Component({
	selector: "app-status-chart",
	imports: [BaseChartDirective],
	templateUrl: "./status-chart.component.html",
	styleUrl: "./status-chart.component.css",
})
export class StatusChartComponent implements OnChanges {
	private DEFAULT_ANALYSIS_STATUS = new AnalysisStatusDto(0, 0, 0, 0, "", 0, 0);
	statuses: InputSignal<AnalysisStatusDto> = input(
		this.DEFAULT_ANALYSIS_STATUS,
	);
	public pieChartLegend = true;
	public pieChartPlugins = [];
	public pieChartOptions: ChartOptions<"pie"> = {
		responsive: false,
	};
	public pieChartLabels = ["Net Income", "Liabilities"];
	public pieChartDatasets = [
		{
			data: [0, 0],
		},
	];

	ngOnChanges(): void {
		console.log("CHART GANG", this.statuses(), this.statuses().liabilities);
		this.pieChartDatasets = [
			{
				data: [this.statuses().netIncome, this.statuses().liabilities],
			},
		];
	}
}
