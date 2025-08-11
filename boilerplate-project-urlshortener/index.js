require('dotenv').config();
const express = require('express');
const cors = require('cors');
const dns= require('dns');
const { default: mongoose } = require('mongoose');
mongoose.connect(process.env.MONGO_URI).then(() => console.log('MongoDB Connected'))
                                       .catch(err => console.error('MongoDB Connection error', err)) ;

const Schema= mongoose.Schema;
const app = express();

const urlSchema= new Schema({
  original_url: {type:String, required:true},
  short_url:{type:Number, required:true, unique:true} 
});

const urlModel= mongoose.model('Url',urlSchema);

module.exports=urlModel;

// Basic Configuration
const port = process.env.PORT || 3000;

app.use(cors());

app.use('/public', express.static(`${process.cwd()}/public`));

app.use(express.urlencoded({ extended: false }));

 
app.get('/', function(req, res) {
  res.sendFile(process.cwd() + '/views/index.html');
});

// Your first API endpoint
app.get('/api/hello', function(req, res) {
  res.json({ greeting: 'hello API' });
});

app.post('/api/shorturl', async(req,res) => {
  const {url} = req.body; 
  try
  {
    const webUri = new URL(url);
    console.log(webUri );

    
    let host
    try{
        host = new URL(url).hostname;
        console.log(host)
    }catch(err)
    {
      return res.json(({error: 'Invalid Url'}));
    }

    dns.lookup(host, async(err) => {
       if(err)
       {
        return res.json({error: 'invalid url'});
       }
    

       const existing = await urlModel.findOne({original_url : url})

        if(existing){
          console.log(existing);
          return res.json(({original_url : existing.original_url, short_url: existing.short_url}));
        }

        const code= Math.floor(1000 + Math.random() * 9000);
        
        const newUrl = new urlModel( {
                    original_url: url,
                    short_url:code
          });
          await newUrl.save(); 
         return res.json({original_url:newUrl.original_url, short_url:newUrl.short_url});  
    });
  }catch(err)
  {
    console.error(err);
    res.json 
    ({
      message : err.message
    });
  }
})

app.get('/api/shorturl/:short_url',async (req,res) =>{
   const {short_url}= req.params;
   console.log(short_url);

   const existing = await urlModel.findOne({short_url : short_url});

   if(existing)
   {
      const redirectUrl = existing.original_url;
      res.redirect(redirectUrl);
     
   }
   else{
     res.json({message: 'No Original Url is exist for '+ short_url});
   }
})

 
app.listen(port, function() {
  console.log(`Listening on port ${port}`);
});
