import { Injectable } from '@angular/core';
import { Http} from '@angular/http';

export interface IOrder {
    Id?: string,
    ApartmentId?: string,
    ClientId?: string,
    Cost?: string,
    Region?: string
}

export interface IReport {
    Region?: string,
    AllCosts?: string,
    Percent?: string
}

@Injectable()
export class OrderService{

    private _baseUrl: string = '/api/Order';

    public constructor(private http: Http){
    }

    public getOrders() {
        return this.http.get(this._baseUrl);
    }

    public getOrder(Id: string) {
        return this.http.get(this._baseUrl + '/' + Id);
    }

    public getReports(){
        return this.http.get(this._baseUrl + "/Report")
    }

    public getTotal(){
        return this.http.get(this._baseUrl + "/Total")
    }

    public postOrder(ids: string, clientId: string, order: IOrder) {
        return this.http.post(this._baseUrl + '?ids=' + ids + '&clientId=' + clientId, order);
    }

    public updateOrder(orderId: string, apartmentId: string, order: IOrder) {
        return this.http.put(this._baseUrl + '/?orderId=' + orderId + "&apartmentid=" + apartmentId, order)
    }

    public deleteOrder(id: string){
        return this.http.delete(this._baseUrl + '/' + id);
    }
}