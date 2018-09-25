import { Component, OnInit } from '@angular/core';
import { Hero } from '../../heroes/models/Hero';
import { HeroService } from '../../heroes/services/HeroService';

@Component({
  selector: 'app-dashboard',
  templateUrl: './DashboardComponent.html',
  styleUrls: ['./DashboardComponent.css']
})
export class DashboardComponent implements OnInit {
  heroes: Hero[] = [];

  constructor(private heroService: HeroService) { }

  ngOnInit() {
    this.getHeroes();
  }

  getHeroes(): void {
    this.heroService.getHeroes()
      .subscribe(heroes => this.heroes = heroes.slice(1, 5));
  }
}
