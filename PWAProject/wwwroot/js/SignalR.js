const connection = new signalR.HubConnectionBuilder().withUrl("/broadcastHub").build();  


connection.on("ReceiveMessage", (obj) => {
   
    if (obj.success == "101") {
        $("#tblProduct > tbody").prepend("<tr id='tr_" + obj.productId + "'><td>" + obj.name + "</td><td>" + obj.price + "</td><td>" + obj.quantity + "</td><td>" + obj.description + "</td><td> <a class='btn-danger p-1' href='/Edit/" + obj.productId + "'>View</a></td></tr>");
    }
    else if (obj.success == "102") {        
        $('#tr_' + obj.productId).find("td:eq(0)").text(obj.name);
        $('#tr_' + obj.productId).find("td:eq(1)").text(obj.price);
        $('#tr_' + obj.productId).find("td:eq(2)").text(obj.quantity);
        $('#tr_' + obj.productId).find("td:eq(3)").text(obj.description);
        $('#tr_' + obj.productId).removeAttr('class','blink')
        $('#tr_' + obj.productId).addClass('blink')
    }
});

connection.start().catch(err => console.error(err.toString())); 