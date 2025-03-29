import { Component, inject, OnDestroy, type OnInit } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatCardHeader, MatCardModule } from "@angular/material/card";
import { MatIcon } from "@angular/material/icon";
import { MatProgressSpinner } from "@angular/material/progress-spinner";
import type { CashFlowDto } from "../../models/cashflow";
import { CashFlowService } from "../../services/cash-flow.service";
import { Subject, takeUntil } from "rxjs";

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
	private cashFlowService = inject(CashFlowService);
	protected cashFlows: CashFlowDto[] = [];
	private destroySubject$ = new Subject<void>();

	ngOnInit(): void {
		this.updateTable();
		this.cashFlowService.isSubmitted$
			.pipe(takeUntil(this.destroySubject$))
			.subscribe((result: boolean) => {
				if (result) {
					this.updateTable();
				}
			});
	}

	ngOnDestroy(): void {
		this.destroySubject$.next();
		this.destroySubject$.complete();
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
