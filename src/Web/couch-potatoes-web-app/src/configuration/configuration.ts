import { firestore, storage } from '../firebase';
import { getDownloadURL, ref } from 'firebase/storage';

export const CONFIG_PATH = process.env['PUBLIC_URL'];
export const CONFIG_FILE = 'couch-config.json';

export interface CouchConfig {
    baseUrl: string;
    firebase: {
        projectId: string;
    };
}

export async function getConfig(): Promise<CouchConfig> {
    // const configResponse = await fetch(`${CONFIG_PATH}/${CONFIG_FILE}`);
    const bucket = storage;
    const configFileRef = ref(bucket, CONFIG_FILE);

    try {
        if (process.env.NODE_ENV === 'development') {
            const config = await fetch(`${CONFIG_PATH}/${CONFIG_FILE}`);
            return await config.json();
        }

        const downloadUrl = await getDownloadURL(configFileRef);
        const configResponse = await fetch(downloadUrl);
        const config = await configResponse.json();
        return config as CouchConfig;
    } catch (err) {
        const res = await fetch(`${CONFIG_PATH}/${CONFIG_FILE}`);
        return await res.json();
    }
}
