import { Component, OnInit } from "@angular/core";
import { ClientService, IClient } from "./client.service";

@Component({
    selector: 'clients',
    templateUrl: './client.component.html'
})

export class ClientComponent implements OnInit {
    
    public errorMessage: string = "";

    public Clients: IClient[] = []

    public id: string = ''

    public id1: string = ''

    public name: string = ''

    public name1: string = ''

    public surName: string = ''

    public surName1: string = ''

    public telNumber: string = '';

    public telNumber1: string = '';

    public constructor(private clientService: ClientService){
    }

    public create(): void {
        const item: IClient = {
            Name: this.name,
            SurName: this.surName,
            TelNumber: this.telNumber
        };

        this.clientService.postClient(item).subscribe(result => {
            this.refresh();
            console.log(result);
            let data = result.json() as IClient[];
            this.Clients = data;
        },error => {
            console.error('my err:', error);  
            this.errorMessage = error._body;         
        })
    }

    public delete(): void {

        this.clientService.deleteClient(this.id).subscribe(result => {
            this.refresh();
            console.log(result);
            let data = result.json() as IClient[];
            this.Clients = data;
        })
    }

    public update(): void {
        const item: IClient = {
            Name: this.name1,
            SurName: this.surName1,
            TelNumber: this.telNumber1
        };

        this.clientService.updateClient(item, this.id1).subscribe(result => {
            this.refresh();
            console.log(result);
            let data = result.json() as IClient[];
            this.Clients = data;    
        },error => {
            console.error('my err:', error);
            this.errorMessage = error._body;           
        })
    }

    public ngOnInit() {
        this.getApartments();
    }

    private getApartments(): void {
        this.clientService.getClients().subscribe(result => {
            let data = result.json() as IClient[];
            this.Clients = data;
        });
    }

    
    private refresh(): void {
        this.id = '';
        this.name = '';
        this.surName = '';
        this.telNumber = '';
        this.errorMessage = '';
    }
}