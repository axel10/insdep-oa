function authControl() {
    $.insdep.window({id:"all-auth",href:"./all_auth.html",title:"所有权限"})
}

function sentMail() {
/*    $.pjax({
        url:"./writeMail/writeMail.html",
        container:"#control"
    })*/
    $.ajax({
        url:"./pages/sendMail/sendMail.html",
        success:function (data) {
            $("#control").html(data)
        }
    })
}

function draft() {
    $.ajax({
        url:"./pages/draft/draft.html",
        success:function (data) {
            $("#control").html(data)
        }
    })
}

function writeMail() {
    $.ajax({
        url:"./pages/writeMail/writeMail.html",
        success:function (data) {
            $("#control").html(data)
        }
    })
}

