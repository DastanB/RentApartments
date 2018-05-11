import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

export interface IApartment {
    Id?: string,
    Rooms?: string,
    Cost?: string,
    RegionId?: string,
    Description?: string
}

@Injectable()
export class ApartmentService{

    private _baseUrl: string = '/api/Apartment';

    public constructor(private http: Http){
    }

    public getApartments() {
        return this.http.get(this._baseUrl);
    }

    public getApartment(Id: string) {
        return this.http.get(this._baseUrl + '/Only?id=' + Id);
    }

    public getAprtByReg(region: string){
        return this.http.get(this._baseUrl + "/Almaty?region="+ region);
    }

    public getAprtByOrdered(region: string){
        return this.http.get(this._baseUrl + "/OrderedBy");
    }

    public getAprtByCost(from: string, to: string){
        return this.http.get(this._baseUrl + "/Cost?from="+ from + "&to=" + to);
    }

    public postApartment(person: IApartment) {
        return this.http.post(this._baseUrl, person);
    }

    public updateApartment(person: IApartment, id: string) {
        return this.http.put(this._baseUrl + '/' + id, person);
    }

    public deleteApartment(id: string){
        return this.http.delete(this._baseUrl + '/' + id);
    }
}