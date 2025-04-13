import { Component, inject, input, type InputSignal } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { UtilityService } from "../../services/utility.service";
import { AnalysisStatusDto } from "../../models/analysis";

@Component({
	selector: "app-status-text",
	imports: [MatCardModule, MatButtonModule],
	templateUrl: "./status-text.component.html",
	styleUrl: "./status-text.component.css",
})
export class StatusTextComponent {
	private utilityService = inject(UtilityService);
	protected categoryEnumToString = this.utilityService.categoryEnumToString;

	private DEFAULT_ANALYSIS_STATUS = new AnalysisStatusDto(0, 0, 0, 0, "", 0, 0);
	public statuses: InputSignal<AnalysisStatusDto> = input(
		this.DEFAULT_ANALYSIS_STATUS,
	);
}
