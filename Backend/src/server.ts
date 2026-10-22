import express from 'express';
import bodyParser from 'body-parser';
import routes from './routes';

const app = express();
const port = 3000;

app.use(bodyParser.json());
app.use(routes);

app.listen(port, '127.0.0.1', () => {
    console.log(`Server is running on http://localhost:${port}`);
});
