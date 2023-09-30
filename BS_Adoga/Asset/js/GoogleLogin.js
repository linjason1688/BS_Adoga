//jQuery處理button click event 當畫面DOM都載入時....
$(function () {
    $("#btnSignIn").on("click", function () {
        GoogleLogin();//Google 登入
    });
    $("#btnDisconnect").on("click", function () {
        Google_disconnect();//和Google App斷連
    });
});

function GoogleSigninInit() {
    gapi.load('auth2', function () {
        gapi.auth2.init({
            client_id: "373077817054-5hahnkio91en8pnqqqpaginugjt0f85v.apps.googleusercontent.com"//必填，記得開發時期要開啟 Chrome開發人員工具 查看有沒有403錯誤(Javascript來源被禁止)
        });
    });//end gapi.load
}//end GoogleSigninInit function


function GoogleLogin() {
    let auth2 = gapi.auth2.getAuthInstance();//取得GoogleAuth物件
    auth2.signIn().then(function (GoogleUser) {
        console.log("Google登入成功");
        //取得user id，不過要發送至Server端的話，請使用↓id_token
        let user_id = GoogleUser.getId();
        //true會回傳access token ，false則不會，自行決定。如果只需要Google登入功能應該不會使用到access token
        let AuthResponse = GoogleUser.getAuthResponse(true);
        //取得id_token
        let id_token = AuthResponse.id_token;
        $.ajax({
            url: id_token_to_userIDUrl,
            method: "post",
            data: { id_token: id_token },
            success: function (msg) {
                console.log(msg);
                document.location.href ="https://adoga.azurewebsites.net/";
            }
        });//end $.ajax
    },
        function (error) {
            console.log("Google登入失敗");
            console.log(error);
        });
}//end function GoogleLogin



function Google_disconnect() {
    let auth2 = gapi.auth2.getAuthInstance(); //取得GoogleAuth物件

    auth2.disconnect().then(function () {
        console.log('User disconnect.');
    });
}