var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {
    Clear();
    function Clear() {
        $scope.Employee = {};
        $scope.Employee.EmpId = 0;
        $scope.EmployeeList = [];
        $scope.DepartmentList = [];
        $scope.CityList = [];
        GetAllDepartment();
        GetAllCity();
    }

  
    $scope.InsertData = function () {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Submit") {
           
            $http({
                method: "post",
                url: "http://localhost:7126/Employee/Insert_Employee",
                datatype: "json",
                data: JSON.stringify($scope.Employee)
            }).then(function (response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.Employee.EmpName = "";
                $scope.Employee.Gender = "";
                $scope.Employee.DeptName = "";
                $scope.Employee.CityName = "";
            })
        } else {
           
            $scope.Employee.EmpId = document.getElementById("EmpID_").value;
            $http({
                method: "post",
                url: "http://localhost:7126/Employee/Update_Employee",
                datatype: "json",
                data: JSON.stringify($scope.Employee)
            }).then(function (response) {
                alert(response.data);
                $scope.GetAllData();
                $scope.Employee.EmpName = "";
                $scope.Employee.Gender = "";
                $scope.Employee.DeptName = "";
                $scope.Employee.CityName = "";
                document.getElementById("btnSave").setAttribute("value", "Submit");
                document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
                document.getElementById("spn").innerHTML = "Add New Employee";
            })
        }
    }
    function GetAllCity(){
        $http({
            method: "get",
            url: "http://localhost:7126/Employee/Get_AllCity"
        }).then(function (response) {
            $scope.CityList = response.data;
            console.log($scope.CityList);
        }, function () {
            alert("Error Occur");
        })
    }
    function GetAllDepartment() {
        $http({
            method: "get",
            url: "http://localhost:7126/Employee/Get_AllDepartment"
        }).then(function (response) {
            $scope.DepartmentList = response.data;
            console.log($scope.DepartmentList);
        }, function () {
            alert("Error Occur");
        })
    }
    $scope.GetAllData = function () {
        $http({
            method: "get",
            url: "http://localhost:7126/Employee/Get_AllEmployee"
        }).then(function (response) {
            $scope.EmployeeList = response.data;
        }, function () {
            alert("Error Occur");
        })
    };
    $scope.DeleteEmp = function (Emp) {
        $http({
            method: "post",
            url: "http://localhost:7126/Employee/Delete_Employee",
            datatype: "json",
            data: JSON.stringify(Emp)
        }).then(function (response) {
            alert(response.data);
            $scope.GetAllData(); 
        })
    };
    $scope.UpdateEmp = function (Emp) {
        document.getElementById("EmpID_").value = Emp.EmpId;
        $scope.Employee = Emp;
        document.getElementById("btnSave").setAttribute("value", "Update");
        document.getElementById("btnSave").style.backgroundColor = "Yellow";
        document.getElementById("spn").innerHTML = "Update Employee Information";
    }
})  