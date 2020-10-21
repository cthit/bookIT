const express = require("express");
const http = require("http");
//remove for: app.use(express.urlencoded({extended: true));
const bodyParser = require("body-parser");
const cors = require("cors");

const app = express();
const server = http.createServer(app);

app.use(bodyParser.json());
app.use(
    bodyParser.urlencoded({
        extended: true,
    })
);
app.use(cors());

const pg = require("pg");
var pool = null;

const initExpress = (port = 4000) => {
    server.listen(port);
};

//Add migration
//https://github.com/db-migrate/pg
const initDB = () => {
    const { Pool } = pg;
    pool = new Pool({
        user: process.env.DB_USER,
        database: process.env.DB_NAME,
        password: process.env.DB_PASS,
        host: process.env.DB_HOST,
        port: Number(process.env.DB_PORT),
    });
};

const getPool = () => pool;

const query = (sql, values, convertResult) =>
    new Promise((resolve, reject) => {
        pool.query(sql, values, (errors, results) => {
            if (errors) {
                reject(errors);
            } else {
                resolve(convertResult(results));
            }
        });
    });

const to = promise => {
    return promise
        .then(data => {
            return [null, data];
        })
        .catch(err => [err]);
};

const closeExpress = () => {
    server.close();
};

const v4UUIDPattern = new RegExp(
    /^[0-9A-F]{8}-[0-9A-F]{4}-4[0-9A-F]{3}-[89AB][0-9A-F]{3}-[0-9A-F]{12}$/i
);

const isUUID = uuid => uuid.match(v4UUIDPattern);

const renameProp = (oldProp, newProp, { [oldProp]: old, ...others }) => ({
    [newProp]: old,
    ...others,
});

const getApp = () => app;

const yupToFormErrors = yupError => {
    const errors = {};
    if (yupError.inner) {
        for (var i = 0; i < yupError.inner.length; i++) {
            const fieldError = yupError.inner[i];
            errors[fieldError.path] = fieldError.message;
        }
    }
    return errors;
};

const validateSchema = async (schema, data) => {
    const [schemaErr] = await to(
        schema.validate(data, {
            abortEarly: false,
        })
    );
    return schemaErr == null ? null : yupToFormErrors(schemaErr);
};

module.exports = {
    query,
    to,
    initExpress,
    initDB,
    getPool,
    closeExpress,
    isUUID,
    renameProp,
    getApp,
    validateSchema,
};
