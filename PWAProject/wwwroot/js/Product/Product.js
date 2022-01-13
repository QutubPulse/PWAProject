﻿function SaveProduct() {
    var liProductId = 0;
    if ($('#stProductName').val() == '') {
        toastr.warning('Please enter product name.');
    }
    else if ($('#dcPrice').val() == '') {
        toastr.warning('Please enter price.');
    }
    else if ($('#inQuantity').val()=='') {
        toastr.warning('Please enter quantity.');
    }
    else if ($('#stDescription').val() == '') {
        toastr.warning('Please enter description.');
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
                    response.success.toString(),
                    liProductId.toString(),
                    $('#stProductName').val(),
                    $('#dcPrice').val(),
                    $('#inQuantity').val(),
                    $('#stDescription').val(),
                ).catch(err => console.error(err.toString()));
                if (response.success == 101 || response.success == 102) {
                    window.location.href = response.url;
                }
                else {
                    toastr.success('Error try again.');
                }
            }
        });
    }

   
}