import { Component, OnInit } from "@angular/core";
import { ApartmentService, IApartment } from "./apartment.service";


@Component({
    selector: 'apartments',
    templateUrl: './apartment.component.html'
})

export class ApartmentComponent implements OnInit {
    
    public errorMessage: string = "";

    public Apartments: IApartment[] = []

    public id: string = ''

    public rooms: string = ''

    public cost: string = ''

    public regionId: string = '';

    public description: string = '';
   
    public id1: string = ''

    public rooms1: string = ''

    public cost1: string = ''

    public regionId1: string = '';

    public description1: string = '';

    public constructor(private apartmentService: ApartmentService){
    }

    public create(): void {
        const item: IApartment = {
            Rooms: this.rooms,
            Cost: this.cost,
            RegionId: this.regionId,
            Description: this.description
        };

        this.apartmentService.postApartment(item).subscribe(result => {
            this.refresh();
            console.log(result);
            let data = result.json() as IApartment[];
            this.Apartments = data;
        },error => {
            console.error('my err:', error);  
            this.errorMessage = error._body;         
        })
    }

    public delete(): void {

        this.apartmentService.deleteApartment(this.id).subscribe(result => {
            this.refresh();
            console.log(result);
            let data = result.json() as IApartment[];
            this.Apartments = data;
        })
    }

    public update(): void {
        const item: IApartment = {
            Cost: this.cost1,
            RegionId: this.regionId1,
            Rooms: this.rooms1,
            Description: this.description1
        };

        this.apartmentService.updateApartment(item, this.id1).subscribe(result => {
            this.refresh();
            console.log(result);
            let data = result.json() as IApartment[];
            this.Apartments = data;    
        },error => {
            console.error('my err:', error);
            this.errorMessage = error._body;           
        })
    }

    public ngOnInit() {
        this.getApartments();
    }

    private getApartments(): void {
        this.apartmentService.getApartments().subscribe(result => {
            let data = result.json() as IApartment[];
            this.Apartments = data;
        });
    }


    private refresh(): void {
        this.id = '';
        this.cost = '';
        this.rooms = '';
        this.regionId = '';
        this.description = '';
        this.id1 = '';
        this.cost1 = '';
        this.rooms1 = '';
        this.regionId1 = '';
        this.description1 = '';
        this.errorMessage = '';
    }
}