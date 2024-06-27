

const _dtDeferLoading = 0;
const _dtIsProcessing = true;


const _dtPageLengthShort = 5;
const _dtPageLengthMiddle = 10;
const _dtPageLengthLarger = 25;
const _dtPageLength = 10;
const _dtPageType = 'full_numbers';
const _dtLengthMenu = [[5, 10, 25, 50, -1], [5, 10, 25, 50, "ALL"]];

var _dtDom = 'rt' + '<"row"<"col-sm-3 fg-toolbar  ui-widget-header ui-helper-clearfix tfoot"i>' + '<"col-sm-2"l><"col-sm-7 pagination"p>>';

var _dtDomFilter = 'rft' + '<"row"<"col-sm-3 fg-toolbar  ui-widget-header ui-helper-clearfix tfoot"i>' + '<"col-sm-2"l><"col-sm-7 pagination"p>>';
var _dtDomEditor = 'rt' + '<"row"<"col-sm-3 fg-toolbar  ui-widget-header ui-helper-clearfix tfoot"i>' + '<"col-sm-2"l><"col-sm-7 pagination"p>>';
var _dtDomList = 'rt' + '<"row"<"col-sm-2 fg-toolbar   tfoot"i>' + '<"col-sm-2"l>"' + '<"col-sm-6 pagination"p>' + '<"col-sm-2"B>>';
var _dtDomReport = 'rt' + '<"row"<"col-sm-2 fg-toolbar   tfoot"i>' + '<"col-sm-2"l>"' + '<"col-sm-6 pagination"p>' + '<"col-sm-2 text-right"B>>';

var _dtDomWithBtnAdd = '"Burt' + '<"row"<"col-sm-3 fg-toolbar  ui-widget-header ui-helper-clearfix tfoot"i>' + '<"col-sm-2"l><"col-sm-7 pagination"p>>';
var _dtDomFilterWithExport = 'rft' + '<"row"<"col-sm-2 fg-toolbar  ui-widget-header ui-helper-clearfix tfoot"i>' + '<"col-sm-2"l><"col-sm-6 pagination"p>' + '<"col-sm-2 text-right"B>>';

const _dtButonsExcel = ['excel'];
const _dtLoadingRecordsText = "loading please wait";
const _dtLngSelectRows = "";





//const _dtLngZero = "no data or waiting for loading";
const _dtLngZero = "no data or not loading yet";
const _dtLngInfo = "Total: _MAX_";
const _dtLngInfoFiltered = "Filtered: _TOTAL_.";
const _dtLngEmpty = "Empty";

//const _dtLngLengthMenu = "Page count： _MENU_";
const _dtLngLengthMenu = 'Show <select class="form-control">' +
    '<option value="10">10</option>' +
    '<option value="20">20</option>' +
    '<option value="30">30</option>' +
    '<option value="50">50</option>' +
    '<option value="50">100</option>' +
    '<option value="50">500</option>' +
    '<option value="50">1000</option>' +    
    '</select> rows'
// '<option value="-1">所有</option>' +

const _dtLngPageFirst = "First Page";
const _dtLngPagePrevious = "<<";
const _dtLngPageNext = ">>";
const _dtLngPageLast = "Last Page";

//const _dtLngProcessing = '<div style="position:fixed;top:50%;left:50%;"><span style="background-color: white;">載入中請稍等</span><i class="fa fa-spinner fa-spin fa-3x fa-fw"></i></div>';
//const _dtLngProcessing = '<div class="fa-3x"><i class="fas fa-spinner fa-spin"></i></div>';
const _dtLngProcessing = '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading..n.</span> ';
const _dtLngSearch = "Filter/Search";
const _dtLngSearchPlaceholder = "";



const _dtBtnCsvText = '<i class="fa fa-file-csv"></i>';
const _dtBtnCsvCss = 'dtExtBtn';
const _dtBtnCsvTitle = 'Export to CSV';

const _dtBtnXlsxText = '<i class="fa fa-file-excel-o"></i>';
const _dtBtnXlsxCss = 'dtExtBtn';
const _dtBtnXlsxTitle = 'Export to Excel';



const _edBtnCreateClassName = "btn ml-5 btn-primary btn-outline-light";
const _edBtnEditClassName = "btn btn-primary btn-outline-light";
const _edBtnRemoveClassName = "btn px-2 btn-primary btn-outline-light";
const _edBtnCancelClassName = "btn mr-5  btn-primary btn-outline-light";

const _edLngCreateTitle = "Add Record";
const _edLngEditTitle = "Edit";
const _edLngRemoveTitle = "Remove";
const _edLngRemoveButtonText = "Remove（Enter）";
const _edLngCreateButtonText = "Insert (Enter）";
const _edLngEditButtonText = "Update（Enter）";
const _edLngCancelButtonText = "Cancel（ESC）";

const _edI18nErrorSystemMessage = "Please check error";








function JS_ED_RemoveClick_GetMessage(trObject, idIndex, nameIndex) {

    var vId = $(trObject).find('td:eq(' + idIndex + ')').text();
    var vName = $(trObject).find('td:eq(' + nameIndex + ')').text();

    return 'Confirm to delete？（' + vId + ' ' + vName + '）';
};








function JS_fnGeneralAjaxErrorDisplayMessageHtml(jqXHR, textStatus, errorThrown) {


    var msg = "API exception（" + jqXHR.status + "）：";

    switch (jqXHR.status) {
        // ApiStatusCode.NotAcceptable
        case 406:
            msg = "［validation warrning］<br/>"  + jqXHR.responseText.replace(/\n/g, "<br />");
            break;


        // 資料重複
        case 409:
            msg = jqXHR.responseText.replace(/\n/g, "<br />");
            break;

        default:
            msg += "" + jqXHR.responseText.replace(/\n/g, "<br />")
                + "<br/>[readyState]=" + jqXHR.readyState
                + "<br/>[statusText]=" + jqXHR.statusText
                + "<br/>[textStatus]=" + textStatus;
    }


    return msg;
};