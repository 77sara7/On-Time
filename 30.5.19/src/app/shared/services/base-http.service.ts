import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpRequest, HttpResponse } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';


@Injectable()
export class BaseHttpService {


	constructor(private http: HttpClient/*, private loginService:LoginService*/) {
	}
	
	get<T>(url: string, param?: any): Observable<T> {

		return this.http.get<T>(this.composeUrl(url, param), this.requestOptions)
			.map((res: HttpResponse<T>) => this.extractData(res));
		
	}

	post<T>(url: string, body: any): Observable<T> {

		return this.http.post<T>(url, body, this.requestOptions)
			.map((res: HttpResponse<T>) => this.extractData(res))
			.catch((err: HttpErrorResponse) => {
				debugger
				alert(err);
				// simple logging, but you can do a lot more, see below
				console.error('An error occurred:', err.error);
				return this.handleError<T>(err);
			  });
			//.catch((err) => this.handleError<T>(err));
	}
	

	put<T>(url: string, body: any): Observable<T> {

		return this.http.put<T>(url, body, this.requestOptions)
			.map((res: HttpResponse<T>) => this.extractData<T>(res))
			.catch((err) => this.handleError<T>(err));
	}

	delete<T>(url: string, param?: any): Observable<T> {
		return this.http.delete<T>(this.composeUrl(url, param), this.requestOptions)
			.map((res: HttpResponse<T>) => this.extractData(res))
			.catch((err) => this.handleError<T>(err));
	}

	private composeUrl(url: string, param?: any): string {
		if (url.lastIndexOf('/') == url.length - 1)
			url = url.substr(0, url.length - 2);

		if (param) {
			if (typeof (param) === 'object') {// URLSearchParams
				url += '?' + param;
			}
			else {
				url += '/' + param;
			}
		}

		return url;
	}

	private handleError<T>(error: any): Observable<T> {

		console.log(error);
		return;
	}

	private extractData<T>(response: HttpResponse<T>){
		return response.body;
	}

	private get requestOptions(): any {

		let headers = new HttpHeaders();
		headers = headers.set('Content-Type', 'application/json')
			.set('Accept', 'application/json');

		let options = {
			observe: 'response',
			headers: headers
		};

		return options;
	}



}