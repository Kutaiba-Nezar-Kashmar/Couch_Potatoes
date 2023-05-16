import { Firestore } from '@google-cloud/firestore';

const serviceAccount =
    process.env['GCP_SERVICE_ACCOUNT_KEY'] || '../servie-account-key.json';

const db = new Firestore({
    projectId: 'couch-potatoes-sep6',
    keyFilename: serviceAccount,
});

const getFirestore = () => db;
export default getFirestore;
