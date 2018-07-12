import ApiService from '@/services/ApiService';

export interface ILookup
{
    id: any;
    name: string;
}

export class LookupService extends ApiService
{
    public static getLookup(tableName: string): Promise<ILookup[]>
    {
        return new Promise((resolve, reject) =>
        {
            const url = 'lookup/' + tableName;
            this.httpClient().get(url)
                .then((response) => resolve(response.data))
                .catch(reject);
        });
    }
}
