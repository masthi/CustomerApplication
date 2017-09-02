function CustomerViewModel($scope, $http) {
    $scope.Customer = {
        "CustomerCode": "",
        "CustomerName": "",
        "CustomerAmount": "",
        "CustomerAmountColor": ""
    };
    $scope.Customers = {};
    $scope.$watch("Customers", function () {
        for (var i = 0; i < $scope.Customers.length; i++) {
            var cust = $scope.Customers[i];
            cust.CustomerAmountColor = $scope.getColor(cust.CustomerAmount);
        }
    }
   );
    $scope.getColor = function (Amount) {
        if (Amount == 0) {
            return "";
        }
        else if (Amount > 100) {
            return "Blue";
        }
        else {
            return "Red";
        }

    }
    $scope.$watch("Customer.CustomerAmount", function () {
        $scope.Customer.CustomerAmountColor = $scope.getColor($scope.Customer.CustomerAmount);
    });
    $scope.Add = function () {
        $http({ method: "POST", data: $scope.Customer, url: "/api/CustomerWebAPI/" })
            .then(function (response) {
                //Load the collection of customer
                $scope.Customers = response.data;
                $scope.Customer =
                {
                    "CustomerCode": "",
                    "CustomerName": "",
                    "CustomerAmount": "",
                    "CustomerAmountColor": ""
                };
            });
    };
    $scope.Load = function () {
        $http({ method: "GET", url: "/api/CustomerWebAPI/" })
        .then(function (response) {
            $scope.Customers = response.data;
        });
    }
    $scope.LoadByName = function () {
        var custsearch = $scope.Customer;
        $http({ method: "GET", url: "/api/CustomerWebAPI/?CustomerName=" + custsearch.CustomerName  })
        .then(function (response) {
            $scope.Customers = response.data;
        });
    }

    $scope.LoadByCustomerCode = function (CustomerCode) {
        
        $http({ method: "GET", url: "/api/CustomerWebAPI/?CustomerCode=" + CustomerCode })
        .then(function (response) {
            $scope.Customer = response.data[0];
            console.log(response);
            
        });
    }
    
    $scope.Load();
}