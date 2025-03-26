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
import { FormsModule } from "@angular/forms";
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
import { CashFlowDto } from "../../../models/cashflow";

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
	],
	templateUrl: "./add-cash-flow-panel.component.html",
	styleUrl: "./add-cash-flow-panel.component.css",
	outputs: [],
})
export class AddCashFlowPanelComponent {
	title = "add-cashflow";
	submitted = false;
	cashflowService = inject(CashFlowService);
	newCashflow = new CashFlowDto(1, "", "", 0, 0, 0);
	cashFlows: CashFlowDto[] = [];

	updateTable(): void {
		this.cashflowService.GetAllFlows().subscribe((cf) => {
			this.cashFlows = cf;
		});
	}

	handleSubmit() {
		this.cashflowService.CreateFlow(this.newCashflow).subscribe({
			complete: () => {
				this.submitted = true;
				this.updateTable();
			},
		});
	}
}
