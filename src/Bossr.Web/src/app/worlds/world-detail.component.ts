import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import 'rxjs/add/operator/switchMap';

import { WorldService } from './world.service';
import { CurrentUserManager } from '../current-user/current-user-manager';

import { World } from './world';

@Component({
  selector: 'world',
  templateUrl: './world-detail.component.html',
  styleUrls: ['./world-detail.component.css'],
  providers: [WorldService]
})

export class WorldDetailComponent implements OnInit {
  world: World;

  constructor(private currentUserManager: CurrentUserManager, private worldService: WorldService, private route: ActivatedRoute, private location: Location) { }

  ngOnInit() {
    if (this.currentUserManager.getCurrentUser() !== null) {
      this.route.params
        .switchMap((params: Params) => this.worldService.getWorld(+params['id']))
        .subscribe(world => this.world = world);
    }
  }
}
