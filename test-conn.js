const sql = require('mssql');

const dbConfig = {
    user: 'sa',
    password: 'Learning@12',
    server: 'localhost',
    port: 1433,
    database: 'PharmaClinicalSuiteDB',
    options: {
        encrypt: false,
        trustServerCertificate: true
    }
};

sql.connect(dbConfig)
    .then(pool => {
        console.log("Connected!");
        return pool.request().query("SELECT @@VERSION AS version");
    })
    .then(result => {
        console.log("Result:", result.recordset);
    })
    .catch(err => {
        console.error(" Connection error:", err);
    });
