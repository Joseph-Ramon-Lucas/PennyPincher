export class ExpenseDto {
	constructor(
		public id: number,
		public name: string,
		public category: CATEGORY_TYPES,
		public price: number,
	) {}
}
export class ExpenseForUpdateDto {
	constructor(
		public name: string,
		public price: number,
		public category: CATEGORY_TYPES,
	) {}
}

export enum CATEGORY_TYPES {
	None = 0,
	Living = 1,
	Utilities = 2,
	Entertainment = 3,
	Shopping = 4,
	Takeout = 5,
}
