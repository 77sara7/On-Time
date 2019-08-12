import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from 'selenium-webdriver/http';

@Injectable()
export class FileService {


    constructor() {

    }
    // downloadFile(id): Observable<Blob> {
    //     let options = new RequestOptions({responseType: ResponseContentType.Blob });
    //     return this.http.get(this._baseUrl + '/' + id, options)
    //         .map(res => res.blob())
    //         .catch(this.handleError)
    // }
}