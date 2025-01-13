import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoghistoryService } from './loghistory.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'PennyPincherUI';
  logs: any[] = [];
  logHistoryService = inject(LoghistoryService);

  constructor() {
    this.logHistoryService.get().subscribe(logs => {
      this.logs = logs;
    })
  }
}
