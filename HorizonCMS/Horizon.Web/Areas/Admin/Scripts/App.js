// script.js

// create the module and name it scotchApp
// also include ngRoute for all our routing needs
var HorizonApp = angular.module('horizonApp', ['ngRoute']);

// configure our routes
HorizonApp.config(function ($routeProvider) {
    $routeProvider

        // route for the home page
        .when('/users', {
            templateUrl: '/Admin/Users/Index',
            controller: 'mainController'
        })

        // route for the about page
        .when('/roles', {
            templateUrl: '/Admin/Roles/Index',
            controller: 'aboutController'
        })

          .when('/posttypes', {
              templateUrl: '/Admin/PostCategories/Index',
              controller: 'aboutController'
          })

          .when('/posts', {
              templateUrl: '/Admin/posts/Index',
              controller: 'aboutController'
          })

        // route for the contact page
        .when('/contact', {
            templateUrl: '/Admin/Roles/Index',
            controller: 'contactController'
        });
});

// create the controller and inject Angular's $scope
HorizonApp.controller('mainController', function ($scope) {
    // create a message to display in our view
    $scope.message = 'Everyone come and see how good I look!';
});

HorizonApp.controller('aboutController', function ($scope) {
    $scope.message = 'Look! I am an about page.';
});

HorizonApp.controller('contactController', function ($scope) {
    $scope.message = 'Contact us! JK. This is just a demo.';
});