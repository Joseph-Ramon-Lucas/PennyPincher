import { TestBed } from "@angular/core/testing";

import { AnalysisService } from "./analysis.service";
import {
	HttpTestingController,
	provideHttpClientTesting,
} from "@angular/common/http/testing";
import { provideHttpClient } from "@angular/common/http";

describe("AnalysisService", () => {
	let service: AnalysisService;
	let httpTesting: HttpTestingController;
	beforeEach(() => {
		TestBed.configureTestingModule({
			providers: [provideHttpClient(), provideHttpClientTesting()],
		});
		service = TestBed.inject(AnalysisService);
		httpTesting = TestBed.inject(HttpTestingController);
	});

	it("should be created", () => {
		expect(service).toBeTruthy();
	});
});
