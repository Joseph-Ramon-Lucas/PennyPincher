import { TestBed } from '@angular/core/testing';

import { ExpenseHistoryService } from './expense-history.service';

describe('ExpenseHistoryService', () => {
  let service: ExpenseHistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ExpenseHistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
