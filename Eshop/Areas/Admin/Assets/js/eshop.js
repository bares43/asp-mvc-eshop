function JsonAjaxCallback(json) {
    $.each(json,function(index, value) {
        switch (value.Action) {
            case "Noty_Success":
                noty({ text: value.Params.message, theme: "relax", type: "success" });
                break;
            case "Noty_Warning":
                noty({ text: value.Params.message, theme: "relax", type: "warning" });
                break;
            case "Js":
                eval(value.Params.js);
                break;
            case "Refresh":
                location.reload();
                break;
        }
    });
}

function recountCart() {
    var totalPrice = 0;
    $(".product-row").each(function() {
        var productPrice = $(this).data("price") * parseInt($(this).find(".amount").val());

        totalPrice += productPrice;

        $(this).find(".product-price").text(productPrice);
    });   

    $("#totalPrice").text(totalPrice);
}

function removeProductFromCart(id) {
    $("tr[data-id=" + id + "]").remove();
}

function cartSetAmount(id, amount) {
    $(".product-row[data-id=" + id + "] .amount").val(amount);
}