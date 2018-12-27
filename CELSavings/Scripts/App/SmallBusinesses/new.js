$(document).ready(function () {
    $('#sellDate').datepicker({
        uiLibrary: 'bootstrap4',
        format: 'dd mmm yyyy'
    });
    $('#installmentStart').datepicker({
        uiLibrary: 'bootstrap4',
        format: 'dd mmm yyyy'
    });
    var vm = {
        product: '',
        productDescription: '',
        customerOrGuarantorId: 0,
        name: '',
        amount: 0
    };
    $("#txtBuyingPrice").val(0);
    $("#txtProfitPercentage").val(8);
    $("#txtSellingPrice").val(0);

    $("#txtBuyingPrice").on('change', calculateProfit);
    $("#txtProfitPercentage").on('change', calculateProfit);

    var savingAccounts = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        //prefetch: '../data/films/post_1960.json',
        remote: {
            url: '/api/savingaccounts?query=%QUERY',
            wildcard: '%QUERY',
            cache: false
        }
    });

    $('#txtAccount').typeahead({
        minLength: 1,
        hint: true,
        highlight: true
    }, {
            name: 'savingaccounts',
            display: 'name',
            source: savingAccounts
            //limit: 'Infinity'
        }).on("typeahead:select", function (e, savingAccount) {
            console.log(savingAccount);
            vm.customerOrGuarantorId = savingAccount.id;
            $("#txtCustomerName").val(savingAccount.name);
        });



    $.validator.addMethod("validProduct", function () {
        var product = $("#txtProduct").val();
        return product.length > 0;
    }, "Please select a product name");

    $.validator.addMethod("validProductDescription", function () {
        var product = $("#txtProductDescription").val();
        return product.length > 0;
    }, "Product description is required");

    $.validator.addMethod("validBuyingPrice", function () {
        return parseFloat($("#txtBuyingPrice").val()) > 0;
    }, "Buying price must be greater than zero");

    $.validator.addMethod("validProfitPercentage", function () {
        return parseFloat($("#txtProfitPercentage").val()) > 0;
    }, "Profit percentage must be greater than zero");

    $.validator.addMethod("validSellingPrice", function () {
        return parseFloat($("#txtSellingPrice").val()) > 0;
    }, "Selling price must be greater than zero");


    $.validator.addMethod("validAccount", function () {
        return vm.customerOrGuarantorId && vm.customerOrGuarantorId !== 0;
    }, "Please select a valid customer/guarantor");

    $.validator.addMethod("validCustomerName", function () {
        var product = $("#txtCustomerName").val();
        return product.length > 0;
    }, "Customer name is required");

    var validator = $("#newSmallBusiness").validate({
        submitHandler: function () {
            //e.preventDefault();
            //vm.amount = parseFloat($("#txtAmount").val());
            $.ajax({
                url: '/api/smallbusinesses',
                method: 'post',
                data: vm

            }).done(function () {
                toastr.clear();
                var notify = toastr.success("Payment successfully recorded.");
                var $notifyContainer = jQuery(notify).closest('.toast-top-right');
                $notifyContainer.css("margin-top", 65);
                //$("#txtAccount").typeahead("val", "");
                //$("#txtPaymentMonth").val("");
                // $("#txtAmount").val("2000");
                vm = {
                    name: '',
                    amount: 0
                };
                validator.resetForm();

            })
                .fail(function (error) {
                    toastr.clear();

                    let str = "<ul>";

                    for (var property in error.responseJSON.modelState) {
                        if (error.responseJSON.modelState.hasOwnProperty(property)) {
                            console.log(property);

                            str += "<li>" + error.responseJSON.modelState[property] + "</li>";
                        }
                    }
                    str += "</ul>";
                    var notify = toastr.error("Error occurred: <br>" + str); //error.responseJSON.message);
                    var $notifyContainer = jQuery(notify).closest('.toast-top-right');
                    $notifyContainer.css("margin-top", 65);
                });

            return false;
        }
    });


    // Document Ready End
});

var calculateProfit = function () {
    let buyingPrice = Number($("#txtBuyingPrice").val());
    console.log(buyingPrice);
    let profitPercent = $("#txtProfitPercentage").val() / 100;
    console.log(profitPercent);
    let sellingPrice = buyingPrice + (buyingPrice * profitPercent);
    $("#txtSellingPrice").val(sellingPrice);
}; 
