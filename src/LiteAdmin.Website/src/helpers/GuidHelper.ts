class GuidHelper
{
    public generate(): string
    {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (c) =>
        {
            /* tslint:disable */
            const r = Math.random() * 16 | 0;
            const v = c === 'x' ? r : (r & 0x3 | 0x8);
            /* tslint:enable */
            return v.toString(16);
        });
    }
}

export let guidHelper = new GuidHelper();