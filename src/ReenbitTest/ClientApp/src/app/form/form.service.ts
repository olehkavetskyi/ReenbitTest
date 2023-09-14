import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FormService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }
  upload(data: FormData) {
    return this.http.post(this.apiUrl + 'file', data);
  }
}