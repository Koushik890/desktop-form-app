import express from 'express';
import fs from 'fs-extra';
import path from 'path';
import { Submission } from './models/submission';

const router = express.Router();
const dbPath = path.join(__dirname, 'db.json');

const readDb = async (): Promise<any> => {
    return fs.readJson(dbPath);
};

const writeDb = async (data: any): Promise<void> => {
    return fs.writeJson(dbPath, data);
};

router.get('/ping', (req, res) => {
    res.send(true);
});

router.get('/', (req, res) => {
    res.send('Welcome to the Slidely API');
});

router.post('/submit', async (req, res) => {
    const newSubmission: Submission = req.body;
    const db = await readDb();
    db.submissions.push(newSubmission);
    await writeDb(db);
    res.sendStatus(200);
});

router.get('/read', async (req, res) => {
    const index = parseInt(req.query.index as string, 10);
    const db = await readDb();
    if (index >= 0 && index < db.submissions.length) {
        res.json(db.submissions[index]);
    } else {
        res.status(404).send('Submission not found');
    }
});

router.delete('/delete', async (req, res) => {
    const index = parseInt(req.query.index as string, 10);
    const db = await readDb();
    if (index >= 0 && index < db.submissions.length) {
        db.submissions.splice(index, 1);
        await writeDb(db);
        res.sendStatus(200);
    } else {
        res.status(404).send('Submission not found');
    }
});

router.put('/edit', async (req, res) => {
    const index = parseInt(req.query.index as string, 10);
    const updatedSubmission: Submission = req.body;
    const db = await readDb();
    if (index >= 0 && index < db.submissions.length) {
        db.submissions[index] = updatedSubmission;
        await writeDb(db);
        res.sendStatus(200);
    } else {
        res.status(404).send('Submission not found');
    }
});

export default router;
