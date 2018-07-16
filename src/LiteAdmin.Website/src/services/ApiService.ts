import Axios from 'axios';
import { AxiosInstance } from 'axios';
import { AxiosResponse } from 'axios';
import { AxiosError } from 'axios';
import { Store } from 'vuex';
import * as ActionTypes from '@/store/ActionTypes';
import store from '@/store';

interface ILiteAdminOptions
{
    /* tslint:disable */
    headers: Function;
    /* tslint:enable */
    loginUrl: string;
}

interface ILiteAdminWindow extends Window
{
    liteAdminOptions: ILiteAdminOptions;
}

declare let window: ILiteAdminWindow;

const dateFormat = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z?$/;

function reviver(key: string, value: any)
{
    if (typeof (value) === 'string' && dateFormat.test(value))
    {
        return new Date(value);
    }

    return value;
}

export default class ApiService
{
    public static httpClient(): AxiosInstance
    {
        const requestHeaders: any = new Object();
        if (window.liteAdminOptions && window.liteAdminOptions.headers)
        {
            const headers: any = window.liteAdminOptions.headers();
            if (headers)
            {
                for (const key in headers)
                {
                    if (headers.hasOwnProperty(key))
                    {
                        requestHeaders[key] = headers[key];
                    }
                }
            }
        }

        const instance: AxiosInstance = Axios.create({
            baseURL: store.getters.apiUrl,
            timeout: 20000,
            headers: requestHeaders,
            transformResponse: (data: any): any =>
            {
                if (data && typeof(data) === 'string')
                {
                    return JSON.parse(data, reviver);
                }

                return data;
            },
        });

        instance.interceptors.response.use(
            (response: AxiosResponse): AxiosResponse =>
            {
                return response;
            },
            (error: AxiosError): Promise<AxiosError> =>
            {
                if (error.response &&
                    error.response.status === 401 &&
                    window.liteAdminOptions &&
                    window.liteAdminOptions.loginUrl &&
                    window.liteAdminOptions.loginUrl !== window.location.pathname)
                {
                    window.location.href = window.liteAdminOptions.loginUrl;
                }
                else
                {
                    store.dispatch(ActionTypes.showSnackbar, error.message);
                }
                return Promise.reject(error);
            });

        return instance;
    }
}
