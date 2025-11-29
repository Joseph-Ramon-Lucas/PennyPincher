import { type ComponentFixture, TestBed } from "@angular/core/testing";

import { NavBarComponent } from "./nav-bar.component";
import { provideRouter } from "@angular/router";

describe("NavBarComponent", () => {
	let component: NavBarComponent;
	let fixture: ComponentFixture<NavBarComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [NavBarComponent],
			providers: [provideRouter([])],
		}).compileComponents();

		fixture = TestBed.createComponent(NavBarComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it("should create", () => {
		expect(component).toBeTruthy();
	});
});
