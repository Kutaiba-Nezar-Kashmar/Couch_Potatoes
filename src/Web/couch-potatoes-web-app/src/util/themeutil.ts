import { Theme } from '../models/theme';

export const getTextColor = (theme: Theme) => {
    return theme == Theme.DARK ? 'white' : 'black';
};
