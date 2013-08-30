function grades($scope) {
    $scope.studentcount;
    $scope.students = [{ Grade: 0 }];
    $scope.avggrade = 0;
    $scope.mingrade = 0;
    $scope.maxgrade = 0;

    $scope.getStats = function (students) {
        var gradetotal = 0;

        if(students.length > 0) {
                $scope.mingrade = students[0].Grade;
                $scope.maxgrade = students[0].Grade;
                $scope.avggrade = students[0].Grade;
        }
        angular.forEach(students, function (object) {
            if (object.Grade < $scope.mingrade) {
                $scope.mingrade = object.Grade;
            }

            if (object.Grade > $scope.maxgrade) {
                $scope.maxgrade = object.Grade;
            }

            if (object.Grade > 0) {
                gradetotal = gradetotal + object.Grade;
            }
        });

        $scope.studentcount = students.length;
        $scope.avggrade = gradetotal / $scope.studentcount

        $('.checkgrade').filter(function () {
            return $(this).find('span').text() < 65;
        }).addClass('failing-grade');
    };


    $scope.statInit = function () {
        angular.forEach($scope.students, function (student) {
            student.mingrade = 0;
            student.avggrade = 0;
            student.maxgrade = 0;
        });

        $scope.getStats($scope.students);
    }();

   


}