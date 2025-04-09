import { TestBed } from "@angular/core/testing";

import { CashFlowService } from "./cash-flow.service";
import {
	HttpTestingController,
	provideHttpClientTesting,
} from "@angular/common/http/testing";
import { provideHttpClient } from "@angular/common/http";

describe("CashFlowService", () => {
	let service: CashFlowService;
	let httpTesting: HttpTestingController;

	beforeEach(() => {
		TestBed.configureTestingModule({
			providers: [provideHttpClient(), provideHttpClientTesting()],
		});
		service = TestBed.inject(CashFlowService);
		httpTesting = TestBed.inject(HttpTestingController);
	});

	it("should be created", () => {
		expect(service).toBeTruthy();
	});

	it("should have an http service", () => {
		expect(httpTesting).toBeTruthy();
	});

	it("should have a data form", () => {
		expect(service.cashFlowForm).toBeTruthy();
	});
});
