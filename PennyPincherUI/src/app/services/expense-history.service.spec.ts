import { fakeAsync, TestBed, tick } from "@angular/core/testing";
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

	describe("destroy component subject", () => {
		it("should have a destroy subject subject", () => {
			expect(service.destroySubject$).toBeTruthy();
		});
		it("should return a void subject", () => {
			expect(service.destroySubject$).toEqual(new Subject<void>());
		});
		it("should push a void value", fakeAsync(() => {
			service.destroySubject$.next();
			service.destroySubject$.subscribe((data) => {
				expect(data).toBeDefined;
			});
			tick();
		}));
	});

	describe("submission Complete Subject", () => {
		it("should have a submission complete subject", () => {
			expect(service.submissionComplete$).toBeTruthy();
		});

		it("should return a void subject", () => {
			expect(service.submissionComplete$).toEqual(new Subject<void>());
		});

		it("should push a void value", () => {
			expect(service.submissionComplete$.next()).toEqual(
				new Subject<void>().next(),
			);
		});
		it("should detect a pushed value", fakeAsync(() => {
			service.submissionComplete$.next();
			service.submissionComplete$.subscribe((data) => {
				expect(data).toBeDefined();
			});
			tick();
		}));
	});
});
