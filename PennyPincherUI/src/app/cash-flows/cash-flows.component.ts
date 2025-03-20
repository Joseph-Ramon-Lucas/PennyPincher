import { Component } from "@angular/core";
import { CashFlowTableComponent } from "./cash-flow-table/cash-flow-table.component";
import { CashFlowFormsComponent } from "./cash-flow-forms/cash-flow-forms.component";

@Component({
	selector: "app-cash-flows",
	imports: [CashFlowTableComponent, CashFlowFormsComponent],
	templateUrl: "./cash-flows.component.html",
	styleUrl: "./cash-flows.component.css",
})
export class CashFlowsComponent {
	title = "Budget Cash Flows";
}
