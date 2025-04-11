import { Injectable } from "@angular/core";
import { CATEGORY_TYPES } from "../models/expense";

@Injectable({
	providedIn: "root",
})
export class UtilityService {
	public categoryEnumToString(category: CATEGORY_TYPES): string {
		switch (category) {
			case CATEGORY_TYPES.None:
				return "None";
			case CATEGORY_TYPES.Living:
				return "Living";
			case CATEGORY_TYPES.Utilities:
				return "Utilities";
			case CATEGORY_TYPES.Entertainment:
				return "Entertainment";
			case CATEGORY_TYPES.Shopping:
				return "Shopping";
			case CATEGORY_TYPES.Takeout:
				return "Takeout";
			default:
				return "";
		}
	}
}
