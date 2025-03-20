import { TestBed } from '@angular/core/testing';

import { CashFlowService } from './cash-flow.service';

describe('CashFlowService', () => {
  let service: CashFlowService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CashFlowService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
