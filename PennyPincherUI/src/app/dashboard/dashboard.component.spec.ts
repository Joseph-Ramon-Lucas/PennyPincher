import { type ComponentFixture, TestBed } from "@angular/core/testing";

import { DashboardComponent } from "./dashboard.component";
import {
	HttpTestingController,
	provideHttpClientTesting,
} from "@angular/common/http/testing";
import { provideHttpClient } from "@angular/common/http";

describe("DashboardComponent", () => {
	let component: DashboardComponent;
	let fixture: ComponentFixture<DashboardComponent>;
	let httpTesting: HttpTestingController;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [DashboardComponent],
			providers: [provideHttpClient(), provideHttpClientTesting()],
		}).compileComponents();

		httpTesting = TestBed.inject(HttpTestingController);
		fixture = TestBed.createComponent(DashboardComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it("should create", () => {
		expect(component).toBeTruthy();
	});
});
