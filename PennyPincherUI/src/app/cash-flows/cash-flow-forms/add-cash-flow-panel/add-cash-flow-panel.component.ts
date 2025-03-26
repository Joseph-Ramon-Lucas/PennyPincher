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
	submitted = false;
	cashflowService = inject(CashFlowService);

	cashFlows: CashFlowDto[] = [];

	newCashFlowForm = new FormGroup({
		id: new FormControl(0),
		name: new FormControl(""),
		description: new FormControl(""),
		amount: new FormControl(0),
		flow: new FormControl(FLOW_TYPES.income),
		category: new FormControl(CATEGORY_TYPES.None),
	});

	updateTable(): void {
		this.cashflowService.GetAllFlows().subscribe((cf) => {
			this.cashFlows = cf;
		});
	}

	handleSubmit() {
		console.log(this.newCashFlowForm.value.id);

		const newCF = new CashFlowDto(
			this.newCashFlowForm.value.id ?? 0,
			this.newCashFlowForm.value.name ?? "",
			this.newCashFlowForm.value.description ?? "",
			this.newCashFlowForm.value.amount ?? 0,
			this.newCashFlowForm.value.flow ?? FLOW_TYPES.income,
			this.newCashFlowForm.value.category ?? CATEGORY_TYPES.None,
		);

		this.cashflowService.CreateFlow(newCF).subscribe({
			complete: () => {
				this.submitted = true;
				this.updateTable();
			},
		});
	}
}
