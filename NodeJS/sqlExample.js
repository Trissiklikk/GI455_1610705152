const splice = require("sqlite3").verbose();

let DB = new splice.Database("./DB/chatDB.db", splice.OPEN_CREATE | splice.OPEN_READWRITE,(err)=>{

    if(err)throw err;
    console.log("Connected to database");

    var id = "test9999";
    var password = "999999";
    var name = "test9";



    var sqlSelect = "SELECT * FROM UserData WHERE UserID='"+id+"' AND UserID='"+password+"'";
    var sqlInsert = "INSERT INTO UserData (UserID,Password,Name,Money) VALUES ('"+id+"','"+password+"','"+name+"','100')";
    var sqlUpdate = "UPDATE UserDate SET Money='5000' WHERE UserID='"+id+"";

    DB.all("SELECT Money FROM UserData WHERE UserID='"+id+"'", (err,rows)=>{

        if(err){
            console.log(err);
        }
        else{

            if(rows.length > 0){
                var currentMoney = rows[0].Money;
                currentMoney += 100;

                DB.all("UPDATE UserData SET Money='"+currentMoney+"' WHERE UserID='"+id+"'",(err,rows)=>{


                    if(err){
                        console.log(err);
                    }
                    else{

                        var result = {
                            status: true,
                            money:currentMoney
                        }
                        console.log(JSON.stringify(result));
                    }

                })
            }
            else{

                console.log("UserID not found");
            }

        }

        

        console.log(rows);

    });

});
