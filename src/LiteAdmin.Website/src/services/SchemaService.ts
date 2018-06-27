import ApiService from '@/services/ApiService';
import { ITable } from '@/store/SchemaModule';

export class SchemaService extends ApiService
{
    public static getSchema(): Promise<ITable[]>
    {
        return new Promise((resolve, reject) =>
        {
            const url = 'schema';
            this.httpClient().get(url)
                .then((response) => resolve(response.data))
                .catch(reject);
        });
    }
}
