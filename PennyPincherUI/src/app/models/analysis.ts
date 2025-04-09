import type { CashFlowDto } from "./cashflow";
import type { CATEGORY_TYPES } from "./expense";

export enum CFDataStores {
	Current = 0,
	Projected = 1,
}

export class AnalysisStatusDto {
	constructor(
		public grossIncome: number,
		public netIncome: number,
		public liabilities: number,
		public netIncomeRatio: number,
		public mostCostlyName: string,
		public mostCostlyAmount: number,
		public percentOfEarningsGoingToMostCostlyAmount: number,
	) {}
}

export class AnalysisComparisonDto {
	constructor(
		public currentTopExpenses: CashFlowDto[],
		public projectedTopExpenses: CashFlowDto[],
		public currentCategorySum: CashFlowDto[],
		public projectedCategorySum: CashFlowDto[],
		public currentMostCostlyCategoryAmount: number,
		public projectedMostCostlyCategoryAmount: number,
		public currentMostCostlyCategory: CATEGORY_TYPES,
		public projectedMostCostlyCategory: CATEGORY_TYPES,
		public costlyCategoryRatio: number,
	) {}
}
