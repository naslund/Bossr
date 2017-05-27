import { Component, OnInit } from '@angular/core';

import { WorldService } from './world.service';
import { CurrentUserManager } from '../current-user/current-user-manager';

import { World } from './world';

@Component({
  selector: 'worlds',
  templateUrl: './worlds-all.component.html',
  styleUrls: ['./worlds-all.component.css'],
  providers: [WorldService]
})

export class WorldsAllComponent implements OnInit {
  title = 'Worlds';

  worlds: World[];

  constructor(private currentUserManager: CurrentUserManager, private worldService: WorldService) { }

  ngOnInit() {
    if (this.currentUserManager.getCurrentUser() !== null) {
      this.getWorlds();
    }
  }

  private getWorlds() {
    this.worldService
      .getAllWorlds()
      .subscribe(worlds => this.worlds = worlds);
  }
}
