import type { CATEGORY_TYPES } from "./expense";

export class CashFlowDto {
	constructor(
		public id: number,
		public name: string,
		public description: string,
		public amount: number,
		public flow: FLOW_TYPES,
		public category: CATEGORY_TYPES,
	) {}
}

export class CashFlowUpdateDto {
	constructor(
		public name: string,
		public description: string,
		public amount: number,
		public flow: FLOW_TYPES,
		public category: CATEGORY_TYPES,
	) {}
}

export enum FLOW_TYPES {
	income = 0,
	expense = 1,
}
