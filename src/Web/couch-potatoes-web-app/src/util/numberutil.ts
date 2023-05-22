export function sliceNumber(number: number, decimals: number) {
    const slicedNumber = number.toString().slice(0, decimals + 2);
    return parseFloat(slicedNumber).toFixed(decimals);
}