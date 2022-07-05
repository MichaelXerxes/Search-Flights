
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { FlightService } from './../api/services/flight.service';
import { BookDto, FlightRm } from '../api/models';
import { AuthserviceService } from '../authservice/authservice.service';
import { FormBuilder, Validators } from '@angular/forms';
//import { parse } from 'path';
@Component({
  selector: 'app-book-flights',
  templateUrl: './book-flights.component.html',
  styleUrls: ['./book-flights.component.css']
})
export class BookFlightsComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private router: Router,
    private flightService: FlightService,
    private authService: AuthserviceService,
    private fb: FormBuilder) { }
  flightId: string = 'not loaded'
  flight: FlightRm = {}

  form = this.fb.group({
    number: [1, Validators.compose([Validators.required, Validators.min(1), Validators.max(254)])]
  })
  ngOnInit(): void {
    if (!this.authService.currentUser)
      this.router.navigate(['/register-passenger'])

    this.route.paramMap.subscribe(p => this.findFlight(p.get("flightId")))
  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not passed';

    this.flightService.findFlight({ id: this.flightId }).subscribe(
      f => this.flight = f,
      this.handleError)
  }
  private handleError=(err: any) =>{
    if (err.status == 404) {
      alert("Flight not found!")
      this.router.navigate(['/search-flights'])
    }
    if (err.status == 409) {
      console.log("err: " + err);
      alert(JSON.parse(err.error).message)
    }
    console.log("Resposne XX Error Status", err.status)
    console.log("Resposne XX Error Status Text", err.statusText)
    console.log(err);
  }
  book() {
    // che ck validation
    if (this.form.invalid)
      return;

    const booking: BookDto = {
      flightId: this.flight.id,
      passengerEmail: this.authService.currentUser?.email,
      numberOfSeats: this.form.get('number')?.value
    }

    this.flightService.bookFlight({ body: booking })
      .subscribe(_ => this.router.navigate(['/my-bookings']),
        this.handleError)
   // this.form.get('number')?.value
   // console.log('Booking ${this.form.get('number')?.value} passengers for the flight : ${this} ')
   // console.log($'{this}')

   
  }
  get number() {
    return this.form.controls.number
  }
}
