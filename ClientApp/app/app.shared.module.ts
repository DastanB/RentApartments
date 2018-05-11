import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { ApartmentComponent } from './components/apartment/apartment.component';
import { ApartmentService } from './components/apartment/apartment.service';
import { ClientComponent } from './components/client/client.component';
import { ClientService } from './components/client/client.service';
import { OrderComponent } from './components/order/order.component';
import { OrderService } from './components/order/order.service';
import { ReportComponent } from './components/report/report.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        ApartmentComponent,
        ClientComponent,
        OrderComponent,
        ReportComponent
    ],
    providers: [
        ApartmentService,
        ClientService,
        OrderService
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'apartments', component: ApartmentComponent },
            { path: 'clients', component: ClientComponent },
            { path: 'orders', component: OrderComponent },
            { path: 'reports', component: ReportComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
