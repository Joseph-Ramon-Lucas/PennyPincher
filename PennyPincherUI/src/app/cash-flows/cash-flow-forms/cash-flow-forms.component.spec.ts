import { type ComponentFixture, TestBed } from "@angular/core/testing";

import { CashFlowFormsComponent } from "./cash-flow-forms.component";
import {
	HttpTestingController,
	provideHttpClientTesting,
} from "@angular/common/http/testing";
import { provideHttpClient } from "@angular/common/http";

describe("CashFlowFormsComponent", () => {
	let component: CashFlowFormsComponent;
	let fixture: ComponentFixture<CashFlowFormsComponent>;
	let httpTesting: HttpTestingController;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [CashFlowFormsComponent],
			providers: [provideHttpClient(), provideHttpClientTesting()],
		}).compileComponents();

		httpTesting = TestBed.inject(HttpTestingController);
		fixture = TestBed.createComponent(CashFlowFormsComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it("should create", () => {
		expect(component).toBeTruthy();
	});
});
