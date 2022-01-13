function SaveProduct() {
    connection.invoke("SendMessage",
        $('#name').val(),
        $('#price').val(),
        $('#quantity').val(),
        $('#Description').val(),
    ).catch(err => console.error(err.toString()));
}