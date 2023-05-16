export enum ResultType {
    OK,
    ERROR,
}

export enum OptionType {
    SOME,
    NOTHING,
}

export type Result<TSucess, TError> =
    | { type: ResultType.OK; data: TSucess }
    | { type: ResultType.ERROR; error: TError };
export type Option<T> =
    | { type: OptionType.SOME; data: T }
    | { type: OptionType.NOTHING };

export function voidData(): void {}
