angular
    .module('inventory.ctrl.list', [])
    .controller('inventoryListCtrl', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        var filterKeywords = '';
        var sortExpression = 'Type';

        $scope.inventoryListModel = {
            InventoryList: [],
            NumberOfResults: 0,
            ItemsPerPage: 0,
            CurrentPage: 0,
            SortExpression: sortExpression,
            TotalPages: 0
        };

        $scope.loadingDivHidden = true;
        $scope.nextButtonHidden = true;
        $scope.backButtonHidden = true;
        $scope.currentPage = 0;
        $scope.filterKeywords = '';

        getData();
       

        $scope.sort = function (column) {
            sortExpression = column;
            $scope.currentPage = 0;
            $scope.loadingDivHidden = false;
            getData();
        }

        $scope.move = function (pages) {
            $scope.currentPage = $scope.currentPage + pages;
            $scope.loadingDivHidden = false;
            getData();
        }

         $scope.go = function() {
            $scope.currentPage = 0;
            $scope.loadingDivHidden = false;
            getData();
        }

         function togglePageButtons(data) {
            if (data) {
                if ($scope.currentPage == 0 || data.NumberOfResults < data.ItemsPerPage) {
                    $scope.backButtonHidden = true;
                }
                else {
                    $scope.backButtonHidden = false;
                }

                if (data.CurrentPage == Math.floor(data.NumberOfResults / data.ItemsPerPage) || data.NumberOfResults < data.ItemsPerPage) {
                    $scope.nextButtonHidden = true;
                }
                else {
                    $scope.nextButtonHidden = false;
                }
            }
        }

        function getData() {
                    $http({
                        method: 'GET',
                        url: '/inventory/home/getinventorylist?pageIndex=' + $scope.currentPage +
                            '&sortExpression=' + sortExpression +
                            ($scope.filterKeywords.length == 0 ? "" : "&filterKeywords=" + $scope.filterKeywords)
                    }).success(function (data, status, headers, config) {
                        $scope.inventoryListModel = data;
                        togglePageButtons(data);
                        $scope.loadingDivHidden = true;
                    });
        }


    }]);