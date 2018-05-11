import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

export interface IClient {
    Id?: string,
    Name?: string,
    SurName?: string,
    TelNumber?: string,
}

@Injectable()
export class ClientService{

    private _baseUrl: string = '/api/Client';

    public constructor(private http: Http){
    }

    public getClients() {
        return this.http.get(this._baseUrl);
    }

    public getClient(Id: string) {
        return this.http.get(this._baseUrl + '/' + Id);
    }

    public getClientByName(name: string, surname: string){
        return this.http.get(this._baseUrl + "/By?name=" + name + "&surname=" + surname);
    }

    public postClient(person: IClient) {
        return this.http.post(this._baseUrl, person);
    }

    public updateClient(person: IClient, id: string) {
        return this.http.put(this._baseUrl + '/' + id, person);
    }

    public deleteClient(id: string){
        return this.http.delete(this._baseUrl + '/' + id);
    }
}