var mongo = require("mongodb").MongoClient;
var url ="mongodb://localhost:27017"

mongo.connect(url,{useUnifiedTopology:true},(err,result) =>{

    if(err) throw err;

    var selectDB = result.db("gi455");

    //CreateCollection(selectDB)

    Register(selectDB,"1234","1234","YOLO");
})

function Register(db, _playerID, _password, _playerName)
{
    var newData = {
        playerID:_playerID,
        password:_password,
        playerName:_playerName
    }

    db.collection("playeData").insertOne(newData, (err,result)=>{
        if(err){
            console.log(err);
        }
        else{
            console.log(result)
        }
    })
}

function CreateCollection(db)
{
    db.CreateCollection("playerData",(err,res)=>{
        if(err) throw err;
    })

    db.createCollection("inventory",(err,res)=>{
        if(err) throw err;
    })

    db.createCollection("shopData",(err,res)=>{
        if(err) throw err;
    })
}

