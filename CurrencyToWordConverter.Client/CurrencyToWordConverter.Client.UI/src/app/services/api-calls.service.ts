import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiCallsService {

  private apiUrl = 'https://localhost:7167';

  constructor(private http: HttpClient) { }

  getCurrenctConvertedToWords(number: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/api/Convert?currency=${number}`);
  }
}
