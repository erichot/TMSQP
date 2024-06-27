

const ICO_SwalIconQuestion = "question";

function fnSwalShow(_title, _message) {
    Swal.fire({
        title: _title,
        text: _message
    });
};


function fnSwalShowHtml(_title, _message) {
    Swal.fire({
        title: _title,
        html: _message
    });
};



function fnSwalBtnConfirm_OnClick(_triggerBtnID, _title, _message, _icon) {
    Swal.fire({
        title: _title,
        text: _message,
        icon: _icon,
        //type: _icon,
        showCancelButton: true
    }).then(function (result) {
        if (result.value) {
            $('#' + _triggerBtnID).removeAttr('disabled');
            //jsPageLoading_FadeOut(9000);
            jsPageLoading_Show();
            $('#' + _triggerBtnID).trigger('click');
        };
    });
};



function fnSwalShowFormMessageThenRedirect(_title, _message, _redirectURL) {
    Swal.fire({
        title: _title,
        text: _message,
        type: "success"
    }).then(function () {
        window.location = _redirectURL;
    });

};










function fnSwalBtnConfirmSubmit_OnClick(_triggerBtnID, _title, _confirmMessage) {    
    fnSwalBtnConfirm_OnClick(_triggerBtnID, _title, _confirmMessage, ICO_SwalIconQuestion);
};
function fnSwalBtnConfirmInsert_OnClick(_triggerBtnID) {
    const TIT_ConfirmBeforeInsert = "Add";
    const MSG_ConfirmBeforeInsert = "Confirm to insert？";
    fnSwalBtnConfirm_OnClick(_triggerBtnID, TIT_ConfirmBeforeInsert, MSG_ConfirmBeforeInsert, ICO_SwalIconQuestion);
};
function fnSwalBtnConfirmToSign_OnClick(_triggerBtnID) {
    const TIT_ConfirmBeforeSign = "簽收";
    const MSG_ConfirmBeforeSign = "確認簽收並送出？";
    fnSwalBtnConfirm_OnClick(_triggerBtnID, TIT_ConfirmBeforeSign, MSG_ConfirmBeforeSign, ICO_SwalIconQuestion);
};
function fnSwalBtnConfirmUpdate_OnClick(_triggerBtnID) {
    const TIT_ConfirmBeforeUpdate = "Update";
    const MSG_ConfirmBeforeUpdate = "Confirm to update？";
    fnSwalBtnConfirm_OnClick(_triggerBtnID, TIT_ConfirmBeforeUpdate, MSG_ConfirmBeforeUpdate, ICO_SwalIconQuestion);
};
function fnSwalBtnConfirmDelete_OnClick(_triggerBtnID, _msg) {
    const TIT_ConfirmBeforeDelete = "Delete";
    if (_msg == undefined) _msg = '';
    const MSG_ConfirmBeforeDelete = "Confirm to delete " + _msg + " ？";
    fnSwalBtnConfirm_OnClick(_triggerBtnID, TIT_ConfirmBeforeDelete, MSG_ConfirmBeforeDelete, ICO_SwalIconQuestion);    
};
function fnSwalBtnSubmitForRemove_OnClick(_triggerBtnID) {
    const TIT_SubmitForRemove = "提交詢問";
    const MSG_SubmitForRemove = "確認將此筆「資料刪除」提交送審？";    
    fnSwalBtnConfirm_OnClick(_triggerBtnID, TIT_SubmitForRemove, MSG_SubmitForRemove, ICO_SwalIconQuestion);
};
function fnSwalBtnSubmitForEdit_OnClick(_triggerBtnID) {
    const TIT_SubmitForEdit = "提交詢問";
    const MSG_SubmitForEdit = "確認將此筆「資料修改」提交送審？";
    fnSwalBtnConfirm_OnClick(_triggerBtnID, TIT_SubmitForEdit, MSG_SubmitForEdit, ICO_SwalIconQuestion);    
};
function fnSwalBtnSubmitForUpload_OnClick(_triggerBtnID) {
    const TIT_SubmitForEdit = "Uploading";
    const MSG_SubmitForEdit = "Confirm to upload the payslip file? This may take 10 ~ 20 minutes.";
    fnSwalBtnConfirm_OnClick(_triggerBtnID, TIT_SubmitForEdit, MSG_SubmitForEdit, ICO_SwalIconQuestion);
};
function fnSwalBtnApproveTheSubmission_OnClick(_triggerBtnID) {
    const TIT_ApproveTheSubmission = "詢問";
    const MSG_ApproveTheSubmission = "確認核准？";
    fnSwalBtnConfirm_OnClick(_triggerBtnID, TIT_ApproveTheSubmission, MSG_ApproveTheSubmission, ICO_SwalIconQuestion);
};
function fnSwalBtnRejectTheSubmission_OnClick(_triggerBtnID) {
    const TIT_RejectTheSubmission = "詢問";
    const MSG_RejectTheSubmission = "確認駁回？";
    fnSwalBtnConfirm_OnClick(_triggerBtnID, TIT_RejectTheSubmission, MSG_RejectTheSubmission, ICO_SwalIconQuestion);
};
function fnSwalBtnSendForActivation_OnClick(_triggerBtnID) {
    const TIT = "確認發送";
    const MSG = "確認發送啟用通知？";
    fnSwalBtnConfirm_OnClick(_triggerBtnID, TIT, MSG, ICO_SwalIconQuestion);
};

function fnSwalBtnUnlockAccount_OnClick(_triggerBtnID, _loginID, _userName) {
    const TIT_ConfirmBeforeInsert = "Unlock";
    const MSG_ConfirmBeforeInsert = "Confirm to unlock this account [" + _loginID + " " + _userName + "] ？";
    fnSwalBtnConfirm_OnClick(_triggerBtnID, TIT_ConfirmBeforeInsert, MSG_ConfirmBeforeInsert, ICO_SwalIconQuestion);
};

