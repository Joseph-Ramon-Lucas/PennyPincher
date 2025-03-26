import { Component, inject, type OnInit } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatCardHeader, MatCardModule } from "@angular/material/card";
import { MatIcon } from "@angular/material/icon";
import { MatProgressSpinner } from "@angular/material/progress-spinner";
import type { CashFlowDto } from "../../models/cashflow";
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
	cashFlowService = inject(CashFlowService);
	cashFlows: CashFlowDto[] = [];

	ngOnInit(): void {
		this.updateTable();
	}

	updateTable(): void {
		this.cashFlowService.GetAllFlows().subscribe((cf) => {
			console.log("table component before cfs", this.cashFlows);
			this.cashFlows = cf;
			console.log("table component after cfs", this.cashFlows);
		});
	}
	deleteRow(idToDelete: number): void {
		this.cashFlowService.DeleteFlow(idToDelete).subscribe({
			complete: () => {
				this.updateTable();
			},
		});
	}
}
