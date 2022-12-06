import {Component, Inject, Input, Output} from '@angular/core';
import {TaxCalculatorService} from "../Services/tax-calculator.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public calculationResult?: CalculateTaxResult | null;
  @Input() salary!: number | string;
  @Output() fetchingData : boolean = false;

  constructor(private taxCalculatorService: TaxCalculatorService) { }

  calculateTax() {
    this.calculationResult = null;
    this.fetchingData = true;
    this.taxCalculatorService.calculateTax(Number(this.salary))
      .subscribe(
        result => {
          this.calculationResult = result
          this.fetchingData = false;
        },
        error => console.error(error));
  }
}

export interface CalculateTaxResult {
  grossAnnualSalary: number;
  grossMonthlySalary: number;
  netAnnualSalary: number;
  netMonthlySalary: number;
  annualTax: number;
  monthlyTax: number;
}
