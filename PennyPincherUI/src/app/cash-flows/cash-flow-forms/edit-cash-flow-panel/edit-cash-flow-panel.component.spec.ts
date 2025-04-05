import { type ComponentFixture, TestBed } from "@angular/core/testing";

import { EditCashFlowPanelComponent } from "./edit-cash-flow-panel.component";
import {
	HttpTestingController,
	provideHttpClientTesting,
} from "@angular/common/http/testing";
import { provideHttpClient } from "@angular/common/http";

describe("EditCashFlowPanelComponent", () => {
	let component: EditCashFlowPanelComponent;
	let fixture: ComponentFixture<EditCashFlowPanelComponent>;
	let httpTesting: HttpTestingController;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [EditCashFlowPanelComponent],
			providers: [provideHttpClient(), provideHttpClientTesting()],
		}).compileComponents();

		httpTesting = TestBed.inject(HttpTestingController);
		fixture = TestBed.createComponent(EditCashFlowPanelComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it("should create", () => {
		expect(component).toBeTruthy();
	});
});
