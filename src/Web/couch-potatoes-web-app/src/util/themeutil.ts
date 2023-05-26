import { Theme } from '../models/theme';

export const getTextColor = (theme: Theme) => {
    return theme == Theme.DARK ? 'white' : 'black';
};

export const getDarkGrayBackground = () => {
    return `${process.env['PUBLIC_URL']}/dark-gray-bg.png`;
};
