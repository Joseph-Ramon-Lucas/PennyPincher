import { Component, inject, type OnInit } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatCardHeader, MatCardModule } from "@angular/material/card";
import { MatIcon } from "@angular/material/icon";
import { MatProgressSpinner } from "@angular/material/progress-spinner";
import type { CashFlowDto } from "../../models/cashflowdto";
import { CashFlowService } from "../../services/cash-flow.service";

@Component({
	selector: "app-cash-flow-table",
	imports: [
		MatCardModule,
		MatCardHeader,
		MatButtonModule,
		MatProgressSpinner,
		MatIcon,
	],
	templateUrl: "./cash-flow-table.component.html",
	styleUrl: "./cash-flow-table.component.css",
})
export class CashFlowTableComponent implements OnInit {
	cashFlows: CashFlowDto[] = [];
	cashFlowService = inject(CashFlowService);

	ngOnInit(): void {
		this.updateTable();
	}

	updateTable(): void {
		this.cashFlows = [];
		this.cashFlowService.GetAllFlows().subscribe((cf) => {
			this.cashFlows = cf;
		});
	}
}
