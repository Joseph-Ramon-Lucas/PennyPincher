import { Component, inject, input, type InputSignal } from "@angular/core";
import { UtilityService } from "../../services/utility.service";
import { AnalysisComparisonDto } from "../../models/analysis";
import { CashFlowDto } from "../../models/cashflow";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";

@Component({
	selector: "app-comparison-text",
	imports: [MatCardModule, MatButtonModule],
	templateUrl: "./comparison-text.component.html",
	styleUrl: "./comparison-text.component.css",
})
export class ComparisonTextComponent {
	private utilityService = inject(UtilityService);
	protected categoryEnumToString = this.utilityService.categoryEnumToString;

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
}
