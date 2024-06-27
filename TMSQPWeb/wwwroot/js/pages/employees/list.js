

$(document).ready(function () {



    const jsCI_No = 0;

    const jsCI_NumberOfEmployee = 1;    
    const jsCI_ImportedDate = 2;
    const jsCI_Remark = 3;

    const jsCI_NumberOfNotification = 4;

    const jsCI_NotifiedDate = 5;
    const jsCI_DepartmentName = 6;


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
        //columnDefs: [
        //    { target: jsCI_ImportedDate, className: 'dt-body-center' },
        //    { target: jsCI_NotifiedDate, className: 'dt-body-center' },
        //],
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