import moment from 'moment';

export const formatDate = (value: Date | null): string =>
{
    if (value)
    {
        return moment(value).format('YYYY-MM-DD hh:mm');
    }

    return '';
};
