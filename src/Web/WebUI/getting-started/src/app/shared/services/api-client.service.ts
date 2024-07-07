import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import * as AspNetData from 'devextreme-aspnet-data'
import { DxParams } from '../models/dx-params';


const API_URL = (environment.apiUrl.endsWith('/') ? environment.apiUrl : environment.apiUrl + '/') + 'api/';

export interface IHttpOptions {
    headers?: HttpHeaders;
    observe?: 'body';
    params?:
    | HttpParams
    | {
        [param: string]: string | string[];
    };
    reportProgress?: boolean;
    responseType?: 'json';
    withCredentials?: boolean;
}
@Injectable({
    providedIn: 'root'
})
export class ApiClientService {
    constructor(private http: HttpClient, private authService: AuthService) { }

    get<T>(url: string, options?: IHttpOptions): Observable<T> {
        return this.http
            .get<T>(API_URL + url, this.parseOptions(options));
    }

    post<T>(
        url: string,
        body?: any | null,
        options?: IHttpOptions
    ): Observable<T> {
        return this.http
            .post<T>(API_URL + url, body, this.parseOptions(options));
    }

    put<T>(url: string, body?: any | null, options?: IHttpOptions): Observable<T> {
        return this.http
            .put<T>(API_URL + url, body, this.parseOptions(options));
    }

    delete<T>(url: string, options?: IHttpOptions): Observable<T> {
        return this.http
            .delete<T>(API_URL + url, this.parseOptions(options));
    }

    // Dx Calls All CRUD operations
    dxCallApi(options: DxParams, Id?: string): any {
        const accessToken = this.getAccessToken();

        // if (Id)
        //     options.url = options.url + "/" + Id;
        function buildUrl(baseUrl: string, baseRoute: string, customRoute?: string | undefined): string {
            return baseUrl + (customRoute === undefined ? baseRoute : customRoute);
        }
        return AspNetData.createStore({
            key: options.key,
            loadMethod: options.method,
            loadUrl: buildUrl(API_URL, options.url),
            insertUrl: buildUrl(API_URL, options.url, options.insertUrl),
            updateUrl: buildUrl(API_URL, options.url, options.updateUrl),
            deleteUrl: buildUrl(API_URL, options.url, options.deleteUrl),

            cacheRawData: options.cacheRawData,
            // onAjaxError: ajaxError,
            onBeforeSend: (operation, ajaxOptions) => {
              ajaxOptions.headers = {
                  authorization: 'Bearer ' + accessToken
              };
              ajaxOptions.xhrFields = { withCredentials: options.hasCredentials };
          },
          errorHandler: function (error) { },
          onAjaxError(e) {

              return e.xhr.status;
          },
      });
    }

    private parseOptions(options?: IHttpOptions): IHttpOptions {
        let parsedOptions: IHttpOptions = options ? { ...options } : {};

        // Check if access token is null
        if (this.getAccessToken() === null) {
            return parsedOptions;
        } else {
            //Add the access token to the authorization header
            const authHeader = {
                authorization: 'Bearer ' + this.getAccessToken()
            };

            if (parsedOptions.headers) {
                parsedOptions.headers = parsedOptions.headers.append('Authorization', authHeader.authorization);
            } else {
                parsedOptions.headers = new HttpHeaders(authHeader);
            }

            return parsedOptions;
        }
    }

    getAccessToken(): string {
        return 'Bearer ' + this.authService.getAccessToken();
    }

}
