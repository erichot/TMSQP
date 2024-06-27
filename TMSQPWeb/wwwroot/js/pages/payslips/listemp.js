

$(document).ready(function () {



    const jsCI_RowNo = 0;

    const jsCI_EmployeeId = 1;
    const jsCI_EmployeeName = 2;
    const jsCI_ImportedDate = 3;

    const jsCI_LabourTypeName = 4;

    const jsCI_DivisionName = 5;
    const jsCI_NettPay = 6;


    const jsCI_EmploymentTypeName = 7;
    const jsCI_DesignationName = 8;

    const jsCI_IsSubmitted = 9;
    const jsCI_CreateDate = 10;

    const jsCI_REMOVE = 11;
    const jsCI_EDIT = 12;











    var DtList = $('#tbList').DataTable({
        paging: true,
        pageLength: _dtPageLengthLarger,
        pagingType: _dtPageType,
       
        processing: _dtIsProcessing,
        order: [],
        dom: _dtDomFilterWithExport,
        columnDefs: [
            { target: jsCI_RowNo, width: 100 },
            { target: jsCI_EmployeeId, width: 140 },
            { target: jsCI_NettPay, width: 120 }
        ],
        buttons: [
            {
                extend: 'excel',
                text: _dtBtnXlsxText,
                className: _dtBtnXlsxCss,
                charset: 'utf-8',
                titleAttr: _dtBtnXlsxTitle,
                bom: true
            }
        ],
        language: {
            paginate: {
                first: _dtLngPageFirst,
                next: _dtLngPageNext,
                last: _dtLngPageLast,
                previous: _dtLngPagePrevious
            },
            select: {
                rows: _dtLngSelectRows
            },
            lengthMenu: _dtLngLengthMenu,
            processing: _dtLngProcessing,
            zeroRecords: _dtLngZero,
            info: _dtLngInfo,
            infoFiltered: _dtLngInfoFiltered,
            loadingRecords: _dtLoadingRecordsText,
            infoEmpty: _dtLngEmpty
        }
    });


});