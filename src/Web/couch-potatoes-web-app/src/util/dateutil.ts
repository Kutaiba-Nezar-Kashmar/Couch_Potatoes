export const safeConvertDateToString = (date: Date | null) => {
    if (!date) {
        return '';
    }

    if (!(date instanceof Date)) {
        const asDate = new Date(date);
        return asDate.toLocaleDateString();
    }

    return date.toLocaleDateString();
};
