import { Component, inject } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatDividerModule } from "@angular/material/divider";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";
import { MatTabsModule } from "@angular/material/tabs";
import { MatTooltipModule } from "@angular/material/tooltip";
import { CashFlowService } from "../../../services/cash-flow.service";
import { MatRadioButton } from "@angular/material/radio";
import {
	type CashFlowDto,
	CashFlowUpdateDto,
	FLOW_TYPES,
} from "../../../models/cashflow";
import { CATEGORY_TYPES } from "../../../models/expense";

@Component({
	selector: "app-edit-cash-flow-panel",
	imports: [
		MatFormFieldModule,
		MatInputModule,
		MatSelectModule,
		MatButtonModule,
		MatDividerModule,
		MatIconModule,
		MatTooltipModule,
		MatCardModule,
		FormsModule,
		MatTabsModule,
		ReactiveFormsModule,
		MatRadioButton,
	],
	templateUrl: "./edit-cash-flow-panel.component.html",
	styleUrl: "./edit-cash-flow-panel.component.css",
})
export class EditCashFlowPanelComponent {
	title = "edit-cashflow";
	private cashFlowService = inject(CashFlowService);
	protected cashFlows: CashFlowDto[] = [];
	protected cashFlowForm = this.cashFlowService.cashFlowForm;

	handleSubmit(): void {
		const newCashFlowId = this.cashFlowForm.value.id ?? 0;
		const newCashFlow: CashFlowUpdateDto = new CashFlowUpdateDto(
			this.cashFlowForm.value.name ?? "",
			this.cashFlowForm.value.description ?? "",
			this.cashFlowForm.value.amount ?? 0,
			this.cashFlowForm.value.flow ?? FLOW_TYPES.income,
			this.cashFlowForm.value.category ?? CATEGORY_TYPES.None,
		);
		console.log("about to edit this cashflow", newCashFlow);

		this.cashFlowService.UpdateFlow(newCashFlowId, newCashFlow).subscribe({
			complete: () => {
				this.cashFlowService.submissionComplete$.next();
			},
		});
	}
}
