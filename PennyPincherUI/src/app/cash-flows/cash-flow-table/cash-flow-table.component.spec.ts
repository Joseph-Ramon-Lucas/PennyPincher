import { type ComponentFixture, TestBed } from "@angular/core/testing";

import { CashFlowTableComponent } from "./cash-flow-table.component";

describe("CashFlowTableComponent", () => {
	let component: CashFlowTableComponent;
	let fixture: ComponentFixture<CashFlowTableComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [CashFlowTableComponent],
		}).compileComponents();

		fixture = TestBed.createComponent(CashFlowTableComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it("should create", () => {
		expect(component).toBeTruthy();
	});
});
