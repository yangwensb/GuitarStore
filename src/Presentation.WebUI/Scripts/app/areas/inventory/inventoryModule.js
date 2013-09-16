angular
    .module('inventory', [
        'inventory.ctrl.list', 'inventoryFilters'
    ])
    .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

        $routeProvider.when('/', {
            templateUrl: '/Inventory/Home/GetinventoryList',
            controller: 'inventoryListCtrl',
        });
        $routeProvider.otherwise({
            redirectTo: '/'
        });

        // Specify HTML5 mode (using the History APIs) or HashBang syntax.
        $locationProvider.html5Mode(false).hashPrefix('!');

    }]);
