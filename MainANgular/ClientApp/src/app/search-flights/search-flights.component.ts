import { Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FlightService } from './../api/services/flight.service';
import { FlightRm } from '../api/models';
@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent implements OnInit {


  searchResult: FlightRm[] = []
  constructor(private flightService: FlightService) { }

  ngOnInit(): void {

  }
  search() {
    this.flightService.searchFlight({}).subscribe(r => this.searchResult = r,
this.handleError    )
  }
  private handleError(err: any) {
    if (err.status == 404) {
      alert("Flight not found!")
    }
    console.log("Resposne XX Error Status", err.status)
    console.log("Resposne XX Error Status Text", err.statusText)
    console.log(err);
  }
}
/*export interface FlightRa {
  airline: string;
  arrival: TimePlaceRa;
  departure: TimePlaceRa;
  price: string;
  remaningNumberOfSets: number;
}

export interface TimePlaceRa {
  place: string;
  time: string;
}*/
