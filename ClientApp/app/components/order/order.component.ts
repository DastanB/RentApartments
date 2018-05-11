import { Component, OnInit } from "@angular/core";
import { ApartmentService, IApartment } from "../apartment/apartment.service";
import { OrderService, IOrder} from "./order.service";
import { ClientService, IClient } from "../client/client.service";

@Component({
    selector: 'orders',
    templateUrl: './order.component.html'
})

export class OrderComponent implements OnInit {
    
    public errorMessage: string = "";

    public Orders: IOrder[] = []

    public statuc: string = ''

    public Apartments: IApartment[] = []

    public Clients: IClient[] = []

    public id: string = ''

    public id1: string = ''

    public idA: string = ''

    public clientId: string = ''

    public apartmentId: string = ''

    public apartmentId1: string = ''

    public region: string = ''

    public uApartmentId: string = ''

    public from: string = '1'

    public to: string = '999999999'

    public name: string = ''

    public surname: string = ''

    public constructor(private clientService: ClientService,private orderService: OrderService, private apartmentService: ApartmentService){
    }

    public create(): void {
        const item: IOrder = {
            ClientId: this.clientId,
            ApartmentId: this.apartmentId
        };

        this.orderService.postOrder(this.apartmentId, this.clientId, item).subscribe(result => {
            this.refresh();
            console.log(result);
            let data = result.json() as IOrder[];
            this.Orders = data;
        },error => {
            console.error('my err:', error);  
            this.errorMessage = error._body;         
        })
    }

    public delete(): void {

        this.orderService.deleteOrder(this.id).subscribe(result => {
            this.refresh();
            console.log(result);
            let data = result.json() as IOrder[];
            this.Orders = data;
        })
    }

    public update(): void {
        const item: IOrder = {
            ApartmentId: this.uApartmentId
        };

        this.orderService.updateOrder(this.id1, this.uApartmentId, item ).subscribe(result => {
            this.refresh();
            console.log(result);
            let data = result.json() as IOrder[];
            this.Orders = data;  
        },error => {
            console.error('my err:', error);
            this.errorMessage = error._body;           
        })
    }

    public ngOnInit() {
        this.getOrders();
    }

    public getApartments(): void {
        this.apartmentService.getApartments().subscribe(result => {
            this.refresh();
            let data = result.json() as IApartment[];
            this.Apartments = data;
        });
    }

    public getApartment(): void {
        this.apartmentService.getApartment(this.idA).subscribe(result => {
            this.refresh();
            let data = result.json() as IApartment[];
            this.Apartments = data;
        });
    }

    public getAprtByReg(): void {
        this.apartmentService.getAprtByReg(this.region).subscribe(result => {
            this.refresh();
            let data = result.json() as IApartment[];
            this.Apartments = data;
        })
    }

    public getAprtByCost(): void {
        this.apartmentService.getAprtByCost(this.from, this.to).subscribe(result => {
            this.refresh();
            let data = result.json() as IApartment[];
            this.Apartments = data;
        },error => {
            console.error('my err:', error);
            this.errorMessage = error._body;           
        })
    }

    public getClientByNS(): void {
        this.clientService.getClientByName(this.name, this.surname).subscribe(result => {
            this.refresh();
            let data = result.json() as IApartment[];
            this.Clients = data;
        })
    }

    private getOrders(): void {
        this.orderService.getOrders().subscribe(result => {
            let data = result.json() as IOrder[];
            this.Orders = data;
        })
    }


    private refresh(): void {
        this.id = '';
        this.apartmentId = '';
        this.clientId = '';
        this.idA = '';
        this.region = '';
        this.from = '1';
        this.to = '999999999';
        this.errorMessage = '';
    }
}