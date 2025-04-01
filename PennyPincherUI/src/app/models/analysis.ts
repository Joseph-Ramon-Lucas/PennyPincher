import type { CashFlowDto } from "./cashflow";
import type { CATEGORY_TYPES } from "./expense";

export enum CFDataStores {
	Current = 0,
	Projected = 1,
}

export class AnalysisStatusDto {
	constructor(
		public GrossIncome: number,
		public NetIncome: number,
		public Liabilities: number,
		public NetIncomeRatio: number,
		public MostCostlyName: string,
		public MostCostlyAmount: number,
		public PercentOfEarningsGoingToMostCostlyAmount: number,
	) {}
}

export class AnalysisComparisonDto {
	constructor(
		public CurrentTopExpenses: CashFlowDto[],
		public ProjectedTopExpenses: CashFlowDto[],
		public CurrentCategorySum: CashFlowDto[],
		public ProjectedCategorySum: CashFlowDto[],
		public CurrentMostCostlyCategoryAmount: number,
		public ProjectedMostCostlyCategoryAmount: number,
		public CurrentMostCostlyCategory: CATEGORY_TYPES,
		public ProjectedMostCostlyCategory: CATEGORY_TYPES,
		public CostlyCategoryRatio: number,
	) {}
}
