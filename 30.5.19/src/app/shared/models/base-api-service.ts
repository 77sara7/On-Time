import { environment } from "../../../environments/environment";
import { HttpHeaders } from "@angular/common/http";

const BASE_HTTP_METHODS = ['get', 'post', 'put', 'delete']; 

export class BaseApiService{
 
    constructor(
		...serviceRouteParts: string[])
	{
        console.log(serviceRouteParts);
        
		this._serviceRouteParts = serviceRouteParts;
	}
	
    //#endregion

    //#region variables

    private _serviceRouteParts: string[];

    apiServer = environment.apiServer;
	//portNumber: number = this.apiServer.port;
	useHttps: boolean = this.apiServer.useHttps;
    serverUrl: string = this.apiServer.serverUrl;
    fullAddress: string = this.apiServer.fullAddress();
	
	get basePath() :string{
        return `${this.fullAddress}api`;
    }

    get requestOptions(): any {

		let headers = new HttpHeaders();
		headers = headers.set('Content-Type', 'application/json')
			.set('Accept', 'application/json');

		let options = {
			headers: headers
		};

		return options;
	}

    actionUrl(actionName: string, params?: any[]) :string {

        if(BASE_HTTP_METHODS.indexOf(actionName.toLowerCase()) > -1 )//action url 
            actionName = '';
        
        let middle = this._serviceRouteParts.join('/');
        let more = params && params.length > 0 ? '/' + params.join('/') : '';

        return `${this.basePath}/${middle}/${actionName}${more}`;
    }
}