import { Component, OnInit, OnDestroy } from '@angular/core';

import { WorldService } from './world.service';
import { CurrentUserManager } from '../current-user/current-user-manager';
import { CurrentUserPersister } from '../current-user/current-user-persister';

import { World } from './world';

@Component({
  selector: 'worlds',
  templateUrl: './worlds-all.component.html',
  styleUrls: ['./worlds-all.component.css'],
  providers: [WorldService]
})

export class WorldsAllComponent implements OnInit, OnDestroy {
  title = 'Worlds';
  worlds: World[];
  subscription;

  constructor(private currentUserManager: CurrentUserManager, private currentUserPersister: CurrentUserPersister, private worldService: WorldService) { }

  ngOnInit() {
    let currentUser = this.currentUserPersister.getCurrentUser();
    this.getWorlds(currentUser);

    this.subscription = this.currentUserManager
      .getCurrentUser()
      .subscribe(currentUser => this.getWorlds(currentUser));
  }

  private getWorlds(currentUser) {
    if (currentUser) {
      this.worldService
        .getAllWorlds()
        .subscribe(worlds => this.worlds = worlds);
    } else {
      this.worlds = undefined;
    }
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
