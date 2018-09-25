import { Component, OnInit } from '@angular/core';
import { HeroService } from '../services/HeroService';
import { Hero } from '../models/Hero';

@Component({
  selector: 'app-heroes',
  templateUrl: './HeroListComponent.html',
  styleUrls: ['./HeroListComponent.css']
})
export class HeroListComponent implements OnInit {

  heroes: Hero[];

  selectedHero: Hero;

  constructor(private heroService: HeroService) { }

  ngOnInit() {
    this.heroService.getHeroes()
      .subscribe(this.onloadHeroes);
  }

  private onloadHeroes = (data: Hero[]): void => {
    this.heroes = data;
  };

  onSelect(hero: Hero): void {
    this.selectedHero = hero;
  }

}
