import { firestore } from 'firebase-admin';
import Firestore = firestore.Firestore;

const { initializeApp,  cert } = require('firebase-admin/app');
const { getFirestore } = require('firebase-admin/firestore');

const serviceAccount = require(process.env['GCP_SERVICE_ACCOUNT_KEY'] || './path/to/serviceAccountKey.json');

initializeApp({
    credential: cert(serviceAccount)
});
export const getDb = (): Firestore => getFirestore();
