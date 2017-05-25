import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { WorldsComponent } from './worlds.component';
import { CreaturesComponent } from './creatures.component';

const appRoutes: Routes = [
  { path: 'creatures', component: CreaturesComponent },
  { path: 'worlds', component: WorldsComponent },
  { path: '', redirectTo: '/creatures', pathMatch:'full' }
];

@NgModule({
  declarations: [
    AppComponent,
    WorldsComponent,
    CreaturesComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }
