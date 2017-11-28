import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    //selector: 'mpispeciesapi',
    templateUrl: './mpispecies.component.html'
})
export class MPISpeciesComponent {
    public mpidata: MarineSpecies[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/MPISpeciesAPI/GetMarineSpecies').subscribe(result => {
            this.mpidata = result.json() as MarineSpecies[];
        }, error => console.error(error));
    }
}

interface MarineSpecies {
    idPk: number;
    classFk: number;
    speciesFk: number;
    scientific: string;
    common: string;
    /*
    text: string;
    flag: boolean;
    familyFk: number;
    classFkNavigation: number;
    familyFkNavigation: number;
    recordPet: number[];
    shipmentItem: number[];
    tankLog: number[];
    */
}