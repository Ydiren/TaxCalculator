import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CalculateTaxResult} from "../fetch-data/fetch-data.component";

@Injectable({
  providedIn: 'root'
})
export class TaxCalculatorService {

  private readonly API_ROUTES = {
    calculateTax: (salary:number) => `TaxCalculator/calculatetax?salary=${salary}`
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  calculateTax(salary: number) {
    let url = `${this.baseUrl}${this.API_ROUTES.calculateTax(salary)}`;
    return this.http.get<CalculateTaxResult>(url);
  }
}
