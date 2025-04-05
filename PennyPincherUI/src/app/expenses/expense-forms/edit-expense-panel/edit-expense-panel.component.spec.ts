import { type ComponentFixture, TestBed } from "@angular/core/testing";

import { EditExpensePanelComponent } from "./edit-expense-panel.component";
import {
	HttpTestingController,
	provideHttpClientTesting,
} from "@angular/common/http/testing";
import { provideHttpClient } from "@angular/common/http";

describe("EditExpensePanelComponent", () => {
	let component: EditExpensePanelComponent;
	let fixture: ComponentFixture<EditExpensePanelComponent>;
	let httpTesting: HttpTestingController;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [EditExpensePanelComponent],
			providers: [provideHttpClient(), provideHttpClientTesting()],
		}).compileComponents();

		httpTesting = TestBed.inject(HttpTestingController);
		fixture = TestBed.createComponent(EditExpensePanelComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it("should create", () => {
		expect(component).toBeTruthy();
	});
});
