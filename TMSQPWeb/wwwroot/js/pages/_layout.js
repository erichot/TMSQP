$(document).ready(function () {
    $('#hypPayslip').hover(
        function () { // Mouse enter
            var imgPayslip = $(this).find('img');
            imgPayslip.attr('src', '/icon/payslip24_green.png');
        },
        function () { // Mouse leave
            var imgPayslip = $(this).find('img');
            imgPayslip.attr('src', '/icon/payslip24_gray.png');
        }
    );



    $('#hypEmployee').hover(
        function () { // Mouse enter
            var imgEmployee = $(this).find('img');
            imgEmployee.attr('src', '/icon/employee24_color.png');
        },
        function () { // Mouse leave
            var imgEmployee = $(this).find('img');
            imgEmployee.attr('src', '/icon/employee24_blank.png');
        }
    );
});