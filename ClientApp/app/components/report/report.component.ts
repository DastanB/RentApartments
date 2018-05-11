import { Component, OnInit } from "@angular/core";
import { ApartmentService, IApartment } from "../apartment/apartment.service";
import { OrderService, IOrder, IReport } from "../order/order.service";

@Component({
    selector: 'reports',
    templateUrl: './report.component.html'
})

export class ReportComponent implements OnInit {
    
    public errorMessage: string = "";

    public Orders: IOrder[] = []

    public Reports: IReport[] = []
    
    public Total: string = ''
    
    public constructor(private orderService: OrderService){
    }


    public ngOnInit() {
        this.getOrders();
        this.getReports();
        this.getTotal();
    }

    private getOrders(): void {
        this.orderService.getOrders().subscribe(result => {
            let data = result.json() as IOrder[];
            this.Orders = data;
        })
    }

    private getReports(): void {
        this.orderService.getReports().subscribe(result => {
            let data = result.json() as IReport[];
            this.Reports = data;
        })
    }

    private getTotal(): void {
        this.orderService.getTotal().subscribe(result => {
            let data = result.json() as string;
            this.Total = data;
        })
    }
}