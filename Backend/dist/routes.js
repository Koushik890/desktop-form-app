"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const fs_extra_1 = __importDefault(require("fs-extra"));
const path_1 = __importDefault(require("path"));
const router = express_1.default.Router();
const dbPath = path_1.default.join(__dirname, 'db.json');
const readDb = () => __awaiter(void 0, void 0, void 0, function* () {
    return fs_extra_1.default.readJson(dbPath);
});
const writeDb = (data) => __awaiter(void 0, void 0, void 0, function* () {
    return fs_extra_1.default.writeJson(dbPath, data);
});
router.get('/ping', (req, res) => {
    res.send(true);
});
router.get('/', (req, res) => {
    res.send('Welcome to the Slidely API');
});
router.post('/submit', (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    const newSubmission = req.body;
    const db = yield readDb();
    db.submissions.push(newSubmission);
    yield writeDb(db);
    res.sendStatus(200);
}));
router.get('/read', (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    const index = parseInt(req.query.index, 10);
    const db = yield readDb();
    if (index >= 0 && index < db.submissions.length) {
        res.json(db.submissions[index]);
    }
    else {
        res.status(404).send('Submission not found');
    }
}));
router.delete('/delete', (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    const index = parseInt(req.query.index, 10);
    const db = yield readDb();
    if (index >= 0 && index < db.submissions.length) {
        db.submissions.splice(index, 1);
        yield writeDb(db);
        res.sendStatus(200);
    }
    else {
        res.status(404).send('Submission not found');
    }
}));
router.put('/edit', (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    const index = parseInt(req.query.index, 10);
    const updatedSubmission = req.body;
    const db = yield readDb();
    if (index >= 0 && index < db.submissions.length) {
        db.submissions[index] = updatedSubmission;
        yield writeDb(db);
        res.sendStatus(200);
    }
    else {
        res.status(404).send('Submission not found');
    }
}));
exports.default = router;
