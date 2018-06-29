import ApiService from '@/services/ApiService';

export class TableDataService extends ApiService
{
    public static getItems(tableName: string): Promise<any[]>
    {
        return new Promise((resolve, reject) =>
        {
            const url = tableName;
            this.httpClient().get(url)
                .then((response) => resolve(response.data))
                .catch(reject);
        });
    }
}
