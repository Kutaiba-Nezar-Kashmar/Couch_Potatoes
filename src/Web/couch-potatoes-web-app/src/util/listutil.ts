export function groupElements<T>(xs: T[], elementsPerGroup: number): T[][] {
    let counter = 0;
    let groups: T[][] = [];

    let buffer: T[] = [];
    xs.forEach((elm, index) => {
        if (counter === 3) {
            counter = 0;
            groups.push(buffer);
            buffer = [];
        }

        buffer.push(elm);
        counter++;

        if (index === xs.length - 1) {
            groups.push(buffer);
        }
    });

    return groups;
}
