import { type ComponentFixture, TestBed } from "@angular/core/testing";

import { CashFlowTableComponent } from "./cash-flow-table.component";
import {
	HttpTestingController,
	provideHttpClientTesting,
} from "@angular/common/http/testing";
import { provideHttpClient } from "@angular/common/http";

describe("CashFlowTableComponent", () => {
	let component: CashFlowTableComponent;
	let fixture: ComponentFixture<CashFlowTableComponent>;
	let httpTesting: HttpTestingController;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [CashFlowTableComponent],
			providers: [provideHttpClient(), provideHttpClientTesting()],
		}).compileComponents();

		httpTesting = TestBed.inject(HttpTestingController);
		fixture = TestBed.createComponent(CashFlowTableComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it("should create", () => {
		expect(component).toBeTruthy();
	});
});
