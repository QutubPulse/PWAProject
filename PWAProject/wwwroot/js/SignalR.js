const connection = new signalR.HubConnectionBuilder().withUrl("/broadcastHub").build();  


connection.on("ReceiveMessage", (obj) => {
    console.log('New Message Recivied ' + obj.name + ' ' + obj.price + ' ' + obj.quantity + ' ' + obj.description);
});

connection.start().catch(err => console.error(err.toString())); 