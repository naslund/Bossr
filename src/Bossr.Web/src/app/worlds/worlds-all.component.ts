import { Component, OnInit } from '@angular/core';

import { WorldService } from './world.service';
import { UserManager } from '../usermanager/usermanager';

import { World } from './world';

@Component({
  selector: 'worlds',
  templateUrl: './worlds-all.component.html',
  styleUrls: ['./worlds-all.component.css'],
  providers: [WorldService]
})

export class WorldsComponent implements OnInit {
  title = 'Worlds';

  worlds: World[];

  constructor(private tokenStorage: UserManager, private worldService: WorldService) { }

  ngOnInit() {
    if (this.tokenStorage.isThereAnyToken()) {
      this.getWorlds();
    }
  }

  private getWorlds() {
    this.worldService
      .getAllWorlds()
      .subscribe(worlds => this.worlds = worlds);
  }
}
