import { TestBed } from "@angular/core/testing";
import {
	HttpTestingController,
	provideHttpClientTesting,
} from "@angular/common/http/testing";

import { ExpenseHistoryService } from "./expense-history.service";
import { Observable, Subject } from "rxjs";
import { provideHttpClient } from "@angular/common/http";
import { NgModule } from "@angular/core";

describe("ExpenseHistoryService", () => {
	let service: ExpenseHistoryService;
	let httpTesting: HttpTestingController;

	beforeEach(() => {
		TestBed.configureTestingModule({
			providers: [provideHttpClient(), provideHttpClientTesting()],
		});
		service = TestBed.inject(ExpenseHistoryService);
		httpTesting = TestBed.inject(HttpTestingController);
	});

	it("should be created", () => {
		expect(service).toBeTruthy();
	});
	it("should have httpClient", () => {
		expect(httpTesting).toBeTruthy();
	});

	it("should have an expense form", () => {
		expect(service.expenseForm).toBeTruthy();
	});

	describe("submission Complete Subject", () => {
		it("should have a submission complete subject", () => {
			expect(service.submissionComplete$).toBeTruthy();
		});

		it("submission complete should return a void subject", () => {
			expect(service.submissionComplete$).toEqual(new Subject<void>());
		});
	});
});
