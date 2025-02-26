import { Component, inject } from "@angular/core";
import { ExpenseTable } from "./expense-table.component";
import { ExpenseForm } from "./expense-form.component";
import { NavBarComponent } from "../nav-bar/nav-bar.component";

@Component({
	selector: "app-expense",
	imports: [ExpenseTable, ExpenseForm, NavBarComponent],
	templateUrl: "./expense.component.html",
	styleUrl: "./expense.component.css",
})
export class ExpenseComponent {}
