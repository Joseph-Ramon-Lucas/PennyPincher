import {
	ChangeDetectionStrategy,
	Component,
	effect,
	inject,
	type OnInit,
	signal,
	type WritableSignal,
} from "@angular/core";
import {
	AnalysisStatusDto,
	AnalysisComparisonDto,
	CFDataStores,
} from "../models/analysis";
import { CashFlowDto } from "../models/cashflow";
import { AnalysisService } from "../services/analysis.service";
import { MatCardModule } from "@angular/material/card";
import { MatButtonModule } from "@angular/material/button";
import { UtilityService } from "../services/utility.service";
import { StatusChartComponent } from "./status-chart/status-chart.component";
import { ComparisonChartComponent } from "./comparison-chart/comparison-chart.component";
import { StatusTextComponent } from "./status-text/status-text.component";
import { ComparisonTextComponent } from "./comparison-text/comparison-text.component";
import { MatRadioModule } from "@angular/material/radio";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CATEGORY_TYPES } from "../models/expense";

@Component({
	selector: "app-dashboard",
	imports: [
		MatCardModule,
		MatButtonModule,
		MatRadioModule,
		StatusChartComponent,
		StatusTextComponent,
		ComparisonChartComponent,
		ComparisonTextComponent,
		ReactiveFormsModule,
		FormsModule,
	],
	templateUrl: "./dashboard.component.html",
	styleUrl: "./dashboard.component.css",
	changeDetection: ChangeDetectionStrategy.Default,
})
export class DashboardComponent implements OnInit {
	constructor() {
		// To track radio button selection and recalculate analysis data
		effect(() => {
			this.getFinancialData(this.dataStore());
		});
	}

	private analysisService = inject(AnalysisService);
	public dataStore: WritableSignal<CFDataStores> = signal(
		CFDataStores.Projected,
	);
	private utilityService = inject(UtilityService);
	protected categoryEnumToString = this.utilityService.categoryEnumToString;

	private DEFAULT_ANALYSIS_STATUS = new AnalysisStatusDto(0, 0, 0, 0, "", 0, 0);
	private DEFAULT_ANALYSIS_COMPARISON = new AnalysisComparisonDto(
		[new CashFlowDto(0, "", "", 0, 0, 0)],
		[new CashFlowDto(0, "", "", 0, 0, 0)],
		[new CashFlowDto(0, "", "", 0, 0, 0)],
		[new CashFlowDto(0, "", "", 0, 0, 0)],
		0,
		0,
		0,
		0,
		0,
		0,
	);
	protected statuses: WritableSignal<AnalysisStatusDto> = signal(
		this.DEFAULT_ANALYSIS_STATUS,
	);
	protected comparisons: WritableSignal<AnalysisComparisonDto> = signal(
		this.DEFAULT_ANALYSIS_COMPARISON,
	);
	protected expenseCategoryTypes: CATEGORY_TYPES[] = [CATEGORY_TYPES.None];

	ngOnInit(): void {
		this.getFinancialData(this.dataStore());
	}

	checkStatus() {
		console.log("THIS IS THE DATASTORE", this.dataStore());
	}
	getFinancialData(dataStore: CFDataStores): void {
		this.analysisService.UpdateCashFlowItemLogStore().subscribe({
			complete: () => {
				this.analysisService
					.Status(dataStore)
					.subscribe((result: AnalysisStatusDto) => {
						this.statuses.set(result);
					});
				this.analysisService
					.CompareCashFlows(dataStore)
					.subscribe((result: AnalysisComparisonDto) => {
						// console.log("comparison data:", result, "datastore:", dataStore);
						this.comparisons.set(result);
					});
				this.analysisService
					.GetExpenseCategoryTypes()
					.subscribe((result: CATEGORY_TYPES[]) => {
						this.expenseCategoryTypes = result;
					});
			},
		});
	}
}
