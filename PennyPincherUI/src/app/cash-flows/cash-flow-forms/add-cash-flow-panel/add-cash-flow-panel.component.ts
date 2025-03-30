import {
	Component,
	EventEmitter,
	inject,
	input,
	Input,
	OnChanges,
	Output,
	SimpleChanges,
} from "@angular/core";
import {
	FormControl,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
} from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatDividerModule } from "@angular/material/divider";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";
import { MatTabsModule } from "@angular/material/tabs";
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatRadioModule } from "@angular/material/radio";
import { CashFlowService } from "../../../services/cash-flow.service";
import { CashFlowDto, FLOW_TYPES } from "../../../models/cashflow";
import { CATEGORY_TYPES } from "../../../models/expense";
import { Subject } from "rxjs";

@Component({
	selector: "app-add-cash-flow-panel",
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
		MatRadioModule,
		ReactiveFormsModule,
	],
	templateUrl: "./add-cash-flow-panel.component.html",
	styleUrl: "./add-cash-flow-panel.component.css",
})
export class AddCashFlowPanelComponent {
	title = "add-cashflow";
	private cashFlowService = inject(CashFlowService);
	protected cashFlowForm = this.cashFlowService.cashFlowForm;

	handleSubmit() {
		const newCF = new CashFlowDto(
			this.cashFlowForm.value.id ?? 0,
			this.cashFlowForm.value.name ?? "",
			this.cashFlowForm.value.description ?? "",
			this.cashFlowForm.value.amount ?? 0,
			this.cashFlowForm.value.flow ?? FLOW_TYPES.income,
			this.cashFlowForm.value.category ?? CATEGORY_TYPES.None,
		);

		this.cashFlowService.CreateFlow(newCF).subscribe({
			complete: () => {
				// push update upon successful submission to refresh table
				this.cashFlowService.submissionComplete$.next();
			},
		});
	}
}
