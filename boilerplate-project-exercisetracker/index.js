const express = require('express')
const app = express()
const cors = require('cors');
require('dotenv').config();
const { default: mongoose } = require('mongoose');
const { Time, DateTime } = require('mssql');
mongoose.connect(process.env.MONGO_URI).then(() => console.log('MongoDB Connected'))
                                       .catch(err => console.error('MongoDB Connection error', err)) ;


const Schema= mongoose.Schema;

app.use(cors())
app.use(express.static('public'))
app.use(express.urlencoded({extended : true}));
app.use(express.json());


const excerciseSchema=  new Schema({
   description: {type:String},
    duration: {type:Number},
    date: {type:Date},
    _id:false});
 


  const userSchema= new Schema({
  username:{type:String, required: true},
  count:{type:Number},
  _id: {type:String},
  excercises:   excerciseSchema,
  log: [excerciseSchema]
  });

const User = mongoose.model('User', userSchema);

module.exports= User; 

app.get('/', (req, res) => {
  res.sendFile(__dirname + '/views/index.html')
});

 //Users Get/Post
app.post('/api/users', async(req,res) =>{
  const {username} = req.body;
  const id= crypto.randomUUID();
  
  if(!username)
  {
    return res.status(400).json({
        success:false,
        message: "Username field is required"
    });
  }
  const newUser= new User({username: username, _id: id});
  try{
    const savedUser= await newUser.save();
    res.json({username:username , _id:id})
  }catch(err)
  {
    res.json({ message: "Failed to Add User"});
  }
  
}) 
  app.get('/api/users', async(req,res) => {
    try{
     const users = await User.find();
     res.json(users);
    }
    catch(err){
      res.status(500).json({error:"Failed to fetch Users"})
    }
  })
  
//User's Excercide Get/Post
app.post('/api/users/:_id/exercises',async (req,res) =>{
  const userId= req.params._id;
  const {description, duration,date} = req.body;

  try{
      
      const user = await User.findOneAndUpdate({_id: userId}, {$set:{username: req.body.username},
                          excercises:
                          {  description, 
                             duration ,
                             date:date ? new Date(date) : new Date()},                             
                          $push: {
                                  log: {
                                    description,
                                    duration,
                                    date: date ? new Date(date) : new Date()
                                  }
                                }
                              },
                       {new :true, runValidators: true}); 
              
      if(!user) return res.json({ error: 'User not found'});  
      console.log(user);
 
       res.json({ username:user.username,   description:user.excercises?.description, 
                 duration:user.excercises?.duration, date:user.excercises?.date.toDateString() , _id: userId  }); 
      
  }catch(err)
  {
    res.json({message: "Failed to Create excercise", err});
  }

});

app.get('/api/users/:_id/logs', async(req,res) => {
  try{
     
     const {_id } = req.params;
     const {from,to,limit} = req.query;

     const user = await User.findById(_id); 
     let log = user.log || []; 
     
     if(from)
     {
       console.log(from);
       fromDate= new Date(from).getTime();     
       log = log.filter(x=> new Date(x.date).getTime() >= fromDate);    
     }

     if(to)
     {
       toDate= new Date(to).getTime();     
       log = log.filter(x=> new Date(x.date).getTime() <= toDate);     
     }

       if(limit)
     { 
       log = log.slice(0,parseInt(limit));    
     }

     let  formattedLog;
    if(log.length > 0)
    {
       
       formattedLog = log.map(log => ({
        description: log.description,
        duration: log.duration,
        date: log.date.toDateString()
     }));
    }
   

    res.json({
          username: user.username.toString() ,
          count:log.length || 0   ,
          log:formattedLog
    })
  }catch(err){
      console.error(err);
      res.json({error:"Failed to fetch Users",message:err.message, stack:err.stack})
    }
  })
 
 
const listener = app.listen(process.env.PORT || 3000, () => {
  console.log('Your app is listening on port ' + listener.address().port)
})
