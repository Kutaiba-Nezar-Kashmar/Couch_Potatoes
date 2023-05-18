export const CONFIG_PATH = process.env['PUBLIC_URL'];
export const CONFIG_FILE = 'couch-config.json';

export interface CouchConfig {
    baseUrl: string;
    firebase: {
        projectId: string;
    };
}

export async function getConfig(): Promise<CouchConfig> {
    const configResponse = await fetch(`${CONFIG_PATH}/${CONFIG_FILE}`);
    return (await configResponse.json()) as CouchConfig;
}
