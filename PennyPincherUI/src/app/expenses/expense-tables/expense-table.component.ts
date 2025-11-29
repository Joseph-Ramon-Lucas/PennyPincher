import { Component, inject, type OnDestroy, type OnInit } from "@angular/core";
import { ExpenseDto } from "../../models/expense";
import { ExpenseHistoryService } from "../../services/expense-history.service";
import { MatIconModule } from "@angular/material/icon";
import { MatDividerModule } from "@angular/material/divider";
import { MatButtonModule } from "@angular/material/button";
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatCard, MatCardContent, MatCardHeader } from "@angular/material/card";
import { takeUntil } from "rxjs";
import { UtilityService } from "../../services/utility.service";

@Component({
	selector: "expense-table",
	templateUrl: "./expense-table.component.html",
	styleUrl: "./expense-table.component.css",
	imports: [
		MatButtonModule,
		MatDividerModule,
		MatIconModule,
		MatTooltipModule,
		MatProgressSpinnerModule,
		MatCard,
		MatCardHeader,
		MatCardContent,
	],
})
export class ExpenseTable implements OnInit, OnDestroy {
	title = "ExpenseTable";
	protected expenses: ExpenseDto[] = [];
	private expenseHistoryService = inject(ExpenseHistoryService);
	protected utilityService = inject(UtilityService);
	protected categoryEnumToString = this.utilityService.categoryEnumToString;

	ngOnInit(): void {
		this.updateTable();
		// update the table when a user submits data via the form
		this.expenseHistoryService.submissionComplete$
			.pipe(takeUntil(this.expenseHistoryService.destroySubject$))
			.subscribe(() => {
				this.updateTable();
			});
	}

	ngOnDestroy(): void {
		this.expenseHistoryService.destroySubject$.next();
		this.expenseHistoryService.destroySubject$.complete();
	}

	updateTable(): void {
		this.expenseHistoryService.getAllItems().subscribe((expenses) => {
			this.expenses = expenses;
			console.log("HERE IS TABLE INFO:", this.expenses);
		});
	}

	deleteRow(idToDelete: number): void {
		//temp expense dto item
		//plans to convert to just an id
		const expenseToDelete = new ExpenseDto(idToDelete, "", 0, 1);

		this.expenseHistoryService.deleteItem(expenseToDelete).subscribe({
			complete: () => {
				this.updateTable();
			},
		});
	}
}
