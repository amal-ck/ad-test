import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ApiData } from '../../services/api-data';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard implements OnInit {

  constructor(private api: ApiData,
    private cdr: ChangeDetectorRef
  ) { }
  // games: any = null;
  pageNumber = 1;
pageSize = 9;
totalPages = 1;
games: any[] = [];
  ngOnInit(): void {
    this.loadGames();
  }

  loadGames() {
  this.api.getGames(this.pageNumber, this.pageSize).subscribe((res: any) => {
    this.games = res.data;
    this.totalPages = res.totalPages;
    this.cdr.detectChanges();
  });
}

nextPage() {
  if (this.pageNumber < this.totalPages) {
    this.pageNumber++;
    this.loadGames();
  }
}

prevPage() {
  if (this.pageNumber > 1) {
    this.pageNumber--;
    this.loadGames();
  }
}
}
