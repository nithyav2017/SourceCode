angular.module('myApp',['ngRoute'])
    .config(function($routeProvider, $httpProvider){
        $routeProvider
            .when('/',{
                 template: `<form ng-submit="login()">
            <input type="text" ng-model="username" placeholder="Username" required autofocus>
            <input type="password" ng-model="password" placeholder="Password" required>
            <button type="submit">Login</button>
          </form>
            <p style="color:red" ng-if="error">{{error}}</p>`,
        controller: 'LoginController'})
            .when('/dashboard', {
                template:  '<h1>About</h1><p>You are logged in</p>'
            })
            .otherwise('/');
        $httpProvider.interceptors.push(function(AuthService){
            return {
                request: function (config){
                    var token = AuthService.getToken();
                    if(token){
                        config.headers.Authorization= 'Bearer '+ token;
                    }
                    return config;
                }

            };
        });
            
    })
    .factory('AuthService',function(){
        let token = null;

        return {
            setToken: function(t) {token = t; localStorage.setItem('jwt', t)},
            getToken: function(){return token || localStorage.getItem('jwt')},
            isAuthenticated: function(){return !!this.getToken();},
               
            clearToken: function (){
                token= null;
                localStorage.removeItem('jwt');
            }
        };
    })
    .controller('LoginController', function($scope,$http,$location,AuthService){
        $scope.login = function(){
            $http.post('http://localhost:3000/api/login',{
                username: $scope.username,
                password: $scope.password
            }).then(function (response){
                AuthService.setToken(response.data.token)
                $location.path('/dashboard');
            }).catch(function(err)  {
                $scope.error='Invalid Credential';
            });
        };
    });