angular.module('inventoryFilters', []).filter('msdate', function () {
    return function (input) {
        var date = new Date(parseInt(input.substr(6)));
        return date.toDateString()
    }
});