import { Component } from '@angular/core';
import { FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiCallsService } from './services/api-calls.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  currency: string = '';
  result: string = '';

  constructor(private apiService: ApiCallsService, private _snackBar: MatSnackBar) { }

  ngOnInit() {

  }

  currencyFormControl = new FormControl('', [Validators.required, Validators.maxLength(12), Validators.pattern("([0-9])*,{0,1}([0-9]{1,2})")]);
  matcher = new MyErrorStateMatcher();

  public sendCurrencyToConvert() {
    var dollarValue = this.currency.toString().split(',')[0];

    if (dollarValue.length > 9) {
      this.openSnackBar('Dollar string length limit exceeds.');
      return;
    }

    if (!this.currencyFormControl.errors) {
      this.apiService.getCurrenctConvertedToWords(this.currency.replace(',', '.')).subscribe((result: any) => {
        this.result = result.data;
      }, (error) => {
        this.openSnackBar(error.error.error.message);
      });
    }
  }

  private openSnackBar(msg: string) {
    this._snackBar.open('Error Occurred!', msg, {
      duration: 3000,
      horizontalPosition: 'end',
      verticalPosition: 'top',
    });
  }
}

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

