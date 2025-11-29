import { type ComponentFixture, TestBed } from "@angular/core/testing";

import { CashFlowsComponent } from "./cash-flows.component";
import {
	HttpTestingController,
	provideHttpClientTesting,
} from "@angular/common/http/testing";
import { provideHttpClient } from "@angular/common/http";

describe("CashFlowsComponent", () => {
	let component: CashFlowsComponent;
	let fixture: ComponentFixture<CashFlowsComponent>;
	let httpTesting: HttpTestingController;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [CashFlowsComponent],
			providers: [provideHttpClient(), provideHttpClientTesting()],
		}).compileComponents();

		httpTesting = TestBed.inject(HttpTestingController);
		fixture = TestBed.createComponent(CashFlowsComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it("should create", () => {
		expect(component).toBeTruthy();
	});
});
