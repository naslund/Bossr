import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import 'rxjs/add/operator/switchMap';

import { WorldService } from './world.service';
import { CurrentUserManager } from '../current-user/current-user-manager';
import { CurrentUserPersister } from '../current-user/current-user-persister';

import { World } from './world';

@Component({
  selector: 'world',
  templateUrl: './world-detail.component.html',
  styleUrls: ['./world-detail.component.css'],
  providers: [WorldService]
})

export class WorldDetailComponent implements OnInit, OnDestroy {
  title = 'World Details';
  world: World;
  subscription;

  constructor(private currentUserManager: CurrentUserManager, private currentUserPersister: CurrentUserPersister, private worldService: WorldService, private route: ActivatedRoute, private location: Location) { }

  ngOnInit() {
    let currentUser = this.currentUserPersister.getCurrentUser();
    this.getWorld(currentUser);

    this.subscription = this.currentUserManager
      .getCurrentUser()
      .subscribe(currentUser => this.getWorld(currentUser));
  }

  private getWorld(currentUser) {
    if (currentUser) {
      this.route.params
        .switchMap((params: Params) => this.worldService.getWorld(+params['id']))
        .subscribe(world => this.world = world);
    } else {
      this.world = undefined;
    }
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
