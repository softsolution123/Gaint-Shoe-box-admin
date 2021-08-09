$(document).ready(function () {

    //Disable fileds of CUSTOMER INFO and Contact person on update
    debugger;

    //Disable fileds of Customer and Contact on project Update
    var proId = $('#hdProjectId').val();
    if (proId != 0) {

        //customer fileds
        $("#cusDiv input").attr("disabled", true);

        //Contact fileds
        $("#conDiv input").attr("disabled", true);

        //Fill List of Services on update project
        ServicesListOnUpdate(proId);
    }

    //ddlCustomer for disable or Enable Text fileds of customer detail
    $('#ddlCus').change(function () {
        var customerVal = $('#ddlCus').val();
        if (customerVal.length != 0) {

            //Web Servise Call
            CustomerDetailService(customerVal);

            $('#cusDiv input').val('');
            $("#cusDiv input").attr("disabled", true);
        }
        else {
            $("#cusDiv input").val('');
            $("#cusDiv input").removeAttr("disabled");
        }
    });
    //End

    //ddlContact for disable or Enable Text fileds of contact person detail
    $('#ddlcon').change(function () {
        debugger;
        var conVal = $('#ddlcon').val();
        if (conVal.length != 0) {

            //Web Servise Call
            CotactDetailService(conVal);

            $('#conDiv input').val('');
            $("#conDiv input").attr("disabled", true);
        }
        else {
            $("#conDiv input").val('');
            $("#conDiv input").removeAttr("disabled");
        }
    });
    // End

    //ddlService for servies detail
    $('#ddlService').change(function () {
        debugger;
        var serVal = $('#ddlService').val();
        if (serVal.length != 0) {

            callAjaxMethodForServices(serVal);

            //Value of ddlQuantity changed
            $('#ddlQuantity').val('1').trigger('change');
        }
        else {
            $('#ddlQuantity').val('0').trigger('change');
            $('#txtRate').val('0');
        }
    });
    //End

    //ddlRate for rate
    //Global vaviable for basic rate of service
    //Filled on ddlService selected change with web service below
    var baseRate = "";
    $('#ddlQuantity').change(function () {
        debugger;
        var qntVal = $('#ddlQuantity').val();
        if (qntVal.length != 0) {
            var rat = parseInt(baseRate);
            var qnt = parseInt(qntVal);
            var total = rat * qnt;
            $('#txtRate').val(total);
        }
    });
    //End

    //Add Servise values in list 
    var i = 0;
    $('#btnAdd').click(function () {
        debugger;

        i++;

        var serTxt = $('#ddlService option:selected').text();
        var serVal = $('#ddlService option:selected').val();
        var qntVal = $('#ddlQuantity').val();
        var rateVal = $('#txtRate').val();
        if (serTxt == 'Select Service') {
            alert("Service must be selected.")
            $('#ddlService').focus();
            return false;
        }
        if (qntVal == 0) {
            alert("Quantity must be selected.")
            $('#ddlQuantity').focus();
            return false;
        }

        //Web service call for append list of services 
        ServicesList(serTxt, qntVal, rateVal, i, serVal);

        //Empty Fileds after Add click
        $('#ddlService').val('').trigger('change');
        $('#ddlQuantity').val('0').trigger('change');
        $('#txtRate').val('0');

    });


    //Delete Service from list
    $('#deleteService').live('click', function () {
        var r = confirm("Are you sure!");
        if (r == true) {
            if (i != 0) {
                i--
            }
            var servicId = $(this).parents('tr').children("td:nth-child(1)").html();
            ServiceRemoveFromList(servicId);
            $(this).parents('tr').remove();
            return false;
        }
        return false;
    });

    //Edit Service from list
    $('#editService').live('click', function () {

        //Check already in edit mode
        var serTxt = $('#ddlService option:selected').text();
        var serVal = $('#ddlService option:selected').val();
        var qntVal = $('#ddlQuantity').val();
        var rateVal = $('#txtRate').val();

        if (serVal.length != 0 && qntVal.length != 0 && rateVal.length != 0) {

            i++;
            //Web service call for append list of services 
            ServicesList(serTxt, qntVal, rateVal, i, serVal);
        }

        if (i != 0) {
            i--
        }

        var servicId = $(this).parents('tr').children("td:nth-child(1)").html();
        ServiceRemoveFromList(servicId);

        var servicName = $(this).parents('tr').children("td:nth-child(2)").html();

        $("select#ddlService option").each(function () { this.selected = (this.text == servicName); });
        var serVal = $('#ddlService').val();
        $('#ddlService').val(serVal).trigger('change');

        var quantity = $(this).parents('tr').children("td:nth-child(3)").html();
        $('#ddlQuantity').val(quantity).trigger('change');

        var rate = $(this).parents('tr').children("td:nth-child(4)").html();
        rate = rate.replace('NOK', '');
        $('#txtRate').val(rate);

        var servicName = $(this).parents('tr').remove();


        return false;
    });

    //Web servise called for rate of service
    function ServiceRemoveFromList(val) {
        debugger;
        $.ajax({

            type: "POST",

            url: '/WebServices/ServicePage.asmx/RemoveService',

            data: '{ servicId: "' + val + '" }',

            contentType: "application/json; charset=utf-8",

            async: false,

            dataType: "json",

            success: function (response) {
            },

            failure: function (response) {
                alert("Service Failed");
            }

        });
    }
    //End


    //Web servise called for service List
    function ServicesList(serName, quantity, price, id, serId) {

        $.ajax({

            type: "POST",

            url: '/WebServices/ServicePage.asmx/ServicesList',

            data: '{ serName: "' + serName + '", quantity: "' + quantity + '", price: "' + price + '",id: "' + id + '",serId: "' + serId + '"}',

            contentType: "application/json; charset=utf-8",

            async: false,

            dataType: "json",

            success: function (response) {
                var list = response.d.toString();
                $('#tService').append(list);
            },

            failure: function (response) {
                alert("Service Failed");
            }

        });
    }
    //End

    //Web servise called for service List
    function ServicesListOnUpdate(val) {

        $.ajax({

            type: "POST",

            url: '/WebServices/ServicePage.asmx/ServicesListOnUpdate',

            data: '{ proId: "' + val + '"}',

            contentType: "application/json; charset=utf-8",

            async: false,

            dataType: "json",

            success: function (response) {
                var list = response.d.toString();
                $('#tService').append(list);
            },

            failure: function (response) {
                alert("Service Failed");
            }

        });
    }
    //End

    //Web servise called for rate of service
    function callAjaxMethodForServices(val) {

        $.ajax({

            type: "POST",

            url: '/WebServices/ServicePage.asmx/ServiceRate',

            data: '{ sId: "' + val + '" }',

            contentType: "application/json; charset=utf-8",

            async: false,

            dataType: "json",

            success: function (response) {
                var rat = response.d.toString();
                $('#txtRate').val('');
                $('#txtRate').val(rat);
                $('#txtRate').attr("disabled", true);
                baseRate = $('#txtRate').val();
            },

            failure: function (response) {
                alert("Service Failed");
            }

        });
    }
    //End

    //WEb service for customer detail
    function CustomerDetailService(val) {
        debugger;
        $.ajax({

            type: "POST",

            url: '/WebServices/ServicePage.asmx/CustomerDetail',

            data: '{ cusId: "' + val + '" }',

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            success: function (response) {
                var cusRes = response.d.toString();
                var cusDetail = cusRes.split('###');
                $('#txtCusName').val(cusDetail[0]);
                $('#txtCusContact').val(cusDetail[1]);
                $('#txtCusEmail').val(cusDetail[2]);
                $('#txtCusAdd').val(cusDetail[3]);
            },

            failure: function (response) {
                alert("Service Failed");
            }

        });
    }
    //End

    //WEb service for Contact Person detail
    function CotactDetailService(val) {
        debugger;
        $.ajax({

            type: "POST",

            url: '/WebServices/ServicePage.asmx/ContactDetail',

            data: '{ conId: "' + val + '" }',

            contentType: "application/json; charset=utf-8",

            dataType: "json",

            success: function (response) {
                var conRes = response.d.toString();
                var conDetail = conRes.split('###');
                $('#txtConName').val(conDetail[0]);
                $('#txtConContact').val(conDetail[1]);
                $('#txtConEmail').val(conDetail[2]);
                $('#txtConAdd').val(conDetail[3]);
            },

            failure: function (response) {
                alert("Service Failed");
            }

        });
    }
    //End

    //Check On Create project list of services
    $('#btnCreate').click(function () {

        debugger;

        var trlength = $('#tService tr').length;
        if (trlength == 0) {
            alert("Minimum one service must be added");
            return false;
        }
    })

})