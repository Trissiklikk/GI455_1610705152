var mongo = require("mongodb".MongoClient);
var url = "mongodb://localhost:27017/";

mongo.connect(url,{useUnifiedTopology: true},(err , result)=>{

    if(err) throw err;
    var selectDB = result.db("gi455");


});

function createCollection = (db)=>{
    selectDB.createCollection("inventory" , (err,res)=>{
        if(err) throw err;
    });

    selectDB.createCollection("shopData", (err,res)=>{

        if(err) throw err;
    });

}