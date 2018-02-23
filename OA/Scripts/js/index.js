

function sentMail() {
/*    $.pjax({
        url:"./writeMail/writeMail.html",
        container:"#control"
    })*/
    $.ajax({
        url:"/pages/sendMail/sendMail.html",
        success:function (data) {
            $("#control").html(data)
        }
    })
}

function draft() {
    $.ajax({
        url:"/mail/DraftBox",
        success:function (data) {
            $("#control").html(data)
        }
    })
}

function writeMail() {
    $.ajax({
        url:"/mail/WriteMail",
        success:function (data) {
            $("#control").html(data)
            console.log(1);
        }
    })
}

function inbox() {
    $.ajax({
        url:"/mail/inbox",
        success:function (data) {
            $("#control").html(data)
        }
    })
}

function userControl() {
    $.ajax({
        url:"/user/index",
        success:function (data) {
            $("#control").html(data)
        }
    })
}

function roleControl() {
    $.ajax({
        url:"/role/index",
        success:function (data) {
            $("#control").html(data)
        }
    })
}

function actionControl() {
    $.ajax({
        url:"/action/index",
        success:function (data) {
            $("#control").html(data)
        }
    })
}

