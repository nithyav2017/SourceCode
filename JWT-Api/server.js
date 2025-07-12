const sql = require('mssql');
const express = require('express');
const bodyParser = require('body-parser');
const jwt = require('jsonwebtoken');
const cors = require('cors');

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
     .catch(err => {
        
        console.error(" Connection error:", err);
    });

const app = express();
const PORT = 3000;

const SECRET_KEY =  process.env.JWT_SECRET || 'fallback_dev-key';

 
app.use(cors());
app.use(bodyParser.json());

// Public route
app.get('/', (req, res) => {
  res.send('Hello World!');
});

// Login: issues JWT
app.post('/api/login', async(req, res) => {
  const { username, password } = req.body;
  try{
    const result = await sql.query `SELECT * FROM Users WHERE Username= ${username}`;

    if(result.recordset.length === 0){
      return res.status(401).json({message: 'User Not Found'});
    }
    console.log("Query result:", result.recordset);
    const user= result.recordset[0];

    if(user.PasswordHash != password)
    {
      return res.status(401).json({message: 'Invalid Credential'});
    }
    const token = jwt.sign({ username: user.UserName ,role: user.Role}, SECRET_KEY, { expiresIn: '1h' });
    return res.json({ token });
  
}
catch(err){
  console.error("Login Error:",err);
  res.status(401).json({ message: 'Invalid credentials' });
}
});


// Middleware: verify JWT
function verifyToken(req, res, next) {
  const header = req.headers['authorization'];
  if (!header) return res.sendStatus(403);
  const token = header.split(' ')[1];
  jwt.verify(token, SECRET_KEY, (err, decoded) => {
    if (err) return res.sendStatus(403);
    req.user = decoded;
    next();
  });
}

function authorizeRoles(...allowedRoles){
  return(req,res,next)=>{
    const user= req.user
    if(!user){
      return res.status(401).json({message: "Unauthorized : No data found"});
    }
    if(!allowedRoles.includes(user.role))
    {
       return res.status(403).json({
          message: `Forbidden: Role '${user.role}' does not have access`
       });
    } 
    next();
  };
}

//========Routes=========
// Protected route
app.get('/api/profile', verifyToken, (req, res) => {
  res.json({ message: 'Welcome!', user: req.user });
});
app.get('/api/participants', verifyToken, authorizeRoles('admin'), async (req, res) => {
  // Only Admin  can list all participants
  const result = await sql.query `SELECT * FROM Participants`;
  res.json(result.recordset);
});

app.get('/api/participant/me', verifyToken, authorizeRoles('Participant'), async (req, res) => {
  // Only this participant can view their own record
  const userId = req.user.id;
  const result = await sql.query `SELECT * FROM Participants WHERE UserID = ${userId}`;
  res.json(result.recordset[0]);
});

//=====Start Server=======
app.listen(PORT, () => console.log(`Server listening on http://localhost:${PORT}`));
