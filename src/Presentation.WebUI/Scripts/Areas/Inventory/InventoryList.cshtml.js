var sortExpression = "Type";
var pageIndex = 0;

function stripe() {
    $("tr:even").addClass("even");
    $("tr:odd").addClass("odd");
}

function getData() {
    var filterKeywords = $("#filterKeywords").val();
    var url = "/inventory/home/getinventorylist?" +
        "pageIndex=" + pageIndex +
        "&sortExpression=" + sortExpression +
        (filterKeywords.length == 0 ? "" : "&filterKeywords=" + filterKeywords);

    $.getJSON( url, function (data) {
            togglePageButtons(data);

            var viewModel = ko.mapping.fromJS(data);
            ko.applyBindings(viewModel);

            stripe();
            $("#loadingDiv").hide();
        });
}

$(function () {
    $.ajaxSettings.dataFilter = function (data, type) {
        if (type === 'json') {
            // convert things that look like Dates into a UTC Date string
            // and completely replace them. 
            data = data.replace(/(.*?")(\\\/Date\([0-9\-]+\)\\\/)(")/g,
                function (fullMatch, $1, $2, $3) {
                    try {
                         return $1 + new Date(parseInt($2.substr(7))).toDateString()  + $3;
                    }
                    catch (e) { }
                    // something miserable happened, just return the original string            
                    return $1 + $2 + $3;
                });
        }
        return data;
    };

    getData();
});

function move(pages) {
    pageIndex = pageIndex + pages;
    $("#loadingDiv").show();
    getData();
}

function togglePageButtons(data) {
    if (data) {
        if (data.CurrentPage == 0 || data.NumberOfResults < data.ItemsPerPage) {
            $("#back").hide();
        }
        else {
            $("#back").show();
        }
       
        if (data.CurrentPage == Math.round(data.NumberOfResults / data.ItemsPerPage) || data.NumberOfResults < data.ItemsPerPage) {
            $("#next").hide();
        }
        else {
            $("#next").show();
        }
    }
}

function go(){
    pageIndex = 0;
    $("#loadingDiv").show();
    getData();
}

function sort(column) {
    sortExpression = column;
    pageIndex = 0;
    $("#loadingDiv").show();
    getData();
}