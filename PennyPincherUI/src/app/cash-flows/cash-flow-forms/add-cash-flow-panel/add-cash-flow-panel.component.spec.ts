import { type ComponentFixture, TestBed } from "@angular/core/testing";

import { AddCashFlowPanelComponent } from "./add-cash-flow-panel.component";
import {
	HttpTestingController,
	provideHttpClientTesting,
} from "@angular/common/http/testing";
import { provideHttpClient } from "@angular/common/http";

describe("AddCashFlowPanelComponent", () => {
	let component: AddCashFlowPanelComponent;
	let fixture: ComponentFixture<AddCashFlowPanelComponent>;
	let httpTesting: HttpTestingController;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [AddCashFlowPanelComponent],
			providers: [provideHttpClient(), provideHttpClientTesting()],
		}).compileComponents();

		httpTesting = TestBed.inject(HttpTestingController);
		fixture = TestBed.createComponent(AddCashFlowPanelComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it("should create", () => {
		expect(component).toBeTruthy();
	});
});
