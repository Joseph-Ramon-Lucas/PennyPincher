import { Component } from "@angular/core";
import { CashFlowTableComponent } from "./cash-flow-table/cash-flow-table.component";

@Component({
	selector: "app-cash-flows",
	imports: [CashFlowTableComponent],
	templateUrl: "./cash-flows.component.html",
	styleUrl: "./cash-flows.component.css",
})
export class CashFlowsComponent {
	title = "Budget Cash Flows";
}
