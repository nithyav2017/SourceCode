var express = require('express');
var cors = require('cors');
var multer = require('multer');
require('dotenv').config()


var app = express();

app.use(cors());
app.use('/public', express.static(process.cwd() + '/public'));

const upload = multer({storage: multer.memoryStorage() });
app.get('/', function (req, res) {
  res.sendFile(process.cwd() + '/views/index.html');
});

app.post('/api/fileanalyse',upload.single('upfile'),(req,res) => {
  try{
  const file = req.file;

  if(!file)
    return res.status(400).send('No File Uploded');

  const fileName = file.originalname;
  const fileType= file.mimetype;
  const fileSize = file.size;

  res.json({
     name: fileName,
     type: fileType,
     size: fileSize
  }); 
  }catch(err)
  {
    console.error(err);
    res.json({message:err.message});
  }
});


 
const port = process.env.PORT || 3000;
app.listen(port, function () {
  console.log('Your app is listening on port ' + port)
});
