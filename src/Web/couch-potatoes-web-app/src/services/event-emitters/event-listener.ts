export interface EventListener {
    onEvent(data: any): Promise<any> | void;
}
