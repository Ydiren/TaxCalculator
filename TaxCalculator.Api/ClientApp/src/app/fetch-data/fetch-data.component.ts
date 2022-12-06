import {Component, Inject, Input} from '@angular/core';
import {TaxCalculatorService} from "../Services/tax-calculator.service";

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public calculationResult?: CalculateTaxResult | null;
  @Input() salary!: number | string;

  constructor(private taxCalculatorService: TaxCalculatorService) { }

  calculateTax() {
    this.calculationResult = null;
    this.taxCalculatorService.calculateTax(Number(this.salary))
      .subscribe(
        result => this.calculationResult = result,
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
