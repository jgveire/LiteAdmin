class StringHelper
{
    public split(value: string): string
    {
        const expression: RegExp = /^[a-z]+|[A-Z][^A-Z]+/g;
        const items: RegExpMatchArray | null = value.match(expression);
        if (items == null)
        {
            return '';
        }

        let result: string = '';
        for (let i = 0; i < items.length; i++)
        {
            if (i === 0)
            {
                result += items[i][0].toUpperCase() + items[i].substr(1) + ' ';
            }
            else
            {
                result += items[i].toLowerCase() + ' ';
            }
        }

        return result.trim();
    }
}

export let stringHelper = new StringHelper();