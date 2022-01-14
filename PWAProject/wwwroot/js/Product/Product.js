function SaveProduct() {
    var liProductId = 0;
    if ($('#stProductName').val() == '') {
        alert('Please enter product name.');
    }
    else if ($('#dcPrice').val() == '') {
        alert('Please enter price.');
    }
    else if ($('#inQuantity').val() == '') {
        alert('Please enter quantity.');
    }
    else if ($('#stDescription').val() == '') {
        alert('Please enter description.');
    }
    else {
        var formData = new FormData();
        formData.append("inProductId", $('#inProductId').val());
        formData.append("stProductName", $('#stProductName').val());
        formData.append("dcPrice", $('#dcPrice').val());
        formData.append("inQuantity", $('#inQuantity').val());
        formData.append("stDescription", $('#stDescription').val());
        $.ajax({
            type: "POST",
            url: "/Home/SaveProduct",
            processData: false,
            contentType: false,
            data: formData,
            success: function (response) {
                liProductId = response.productid;
                connection.invoke("SendMessage",
                  ""
                ).catch(err => console.error(err.toString()));
                if (response.success == 101 || response.success == 102) {
                    window.location.href = response.url;
                }
                else {
                    alert('Error try again.');
                }
            }
        });
    }
}
