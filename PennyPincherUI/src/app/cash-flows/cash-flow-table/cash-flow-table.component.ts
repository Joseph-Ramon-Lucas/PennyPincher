import { Component, inject, type OnDestroy, type OnInit } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatCardHeader, MatCardModule } from "@angular/material/card";
import { MatIcon } from "@angular/material/icon";
import { MatProgressSpinner } from "@angular/material/progress-spinner";
import type { CashFlowDto } from "../../models/cashflow";
import { CashFlowService } from "../../services/cash-flow.service";
import { takeUntil } from "rxjs";
import { UtilityService } from "../../services/utility.service";

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
export class CashFlowTableComponent implements OnInit, OnDestroy {
	protected cashFlows: CashFlowDto[] = [];
	private cashFlowService = inject(CashFlowService);
	protected utilityService = inject(UtilityService);
	protected categoryEnumToString = this.utilityService.categoryEnumToString;

	ngOnInit(): void {
		this.updateTable();
		this.cashFlowService.submissionComplete$
			.pipe(takeUntil(this.cashFlowService.destroySubject$))
			.subscribe(() => {
				this.updateTable();
			});
	}

	ngOnDestroy(): void {
		this.cashFlowService.destroySubject$.next();
		this.cashFlowService.destroySubject$.complete();
	}

	updateTable(): void {
		this.cashFlowService.GetAllFlows().subscribe((cf) => {
			this.cashFlows = cf;
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
