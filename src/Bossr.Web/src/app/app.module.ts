import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app/app.component';
import { WorldsAllComponent } from './worlds/worlds-all.component';
import { WorldDetailComponent } from './worlds/world-detail.component';
import { CreaturesComponent } from './creatures/creatures.component';
import { LoginComponent } from './login/login.component';
import { NavigationComponent } from './navigation/navigation.component';

import { ErrorHandler, DataExtractor } from './shared/response.helper';
import { CurrentUserManager } from './current-user/current-user-manager';
import { RequestHelper } from './shared/request.helper';
import { CurrentUserPersister } from './current-user/current-user-persister';

const appRoutes: Routes = [
  { path: 'creatures', component: CreaturesComponent },
  { path: 'worlds', component: WorldsAllComponent },
  { path: 'world/:id', component: WorldDetailComponent },
  { path: '', redirectTo: '/creatures', pathMatch: 'full' }
];

@NgModule({
  declarations: [
    AppComponent,
    WorldsAllComponent,
    WorldDetailComponent,
    CreaturesComponent,
    LoginComponent,
    NavigationComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    ErrorHandler,
    DataExtractor,
    CurrentUserManager,
    CurrentUserPersister,
    RequestHelper
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
