import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ApiData } from '../../services/api-data';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard implements OnInit {

  constructor(private api: ApiData,
    private cdr: ChangeDetectorRef
  ) { }
  user: any = null;
  ngOnInit(): void {
    this.api.userData().subscribe({
      next: (res: any) => {
        this.user = res;
        this.cdr.detectChanges()
      }
    })
  }
}
