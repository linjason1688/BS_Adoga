//const { search } = require("modernizr");

var person_info = document.getElementById("person-info");
var choosing_box = document.getElementById("choosing-box");
var count_person = document.getElementById("count-person");
var travel = document.querySelectorAll('.travel');
var kid_num = document.getElementById("kids-num");

var chooseInfo = document.getElementsByClassName('choose-info');
var showPerson = document.getElementById('final-person');
var showRoom = document.getElementById('final-room');

/*debugger;*/
person_info.addEventListener('click', function () {
    //debugger;
    var person_box = document.getElementById("person-box");
    person_box.style.display = "flex";

    /*if (choosing_box.style.visibility == "visible") {*/
    if (choosing_box.style.display == "flex") {
        choosing_box.style.display = "none";
        close_filter();
    }
    else {
        choosing_box.style.visibility = "visible";
        choosing_box.style.display = "flex";
    }
})
travel.forEach(item => item.addEventListener('click', function () {
    //加class前先把有onUse的子物件先清掉
    [].forEach.call(travel, function (e) {
        e.classList.remove("onUse");
    })
    on_Use(item);
    if (item.classList.contains("single") || item.classList.contains("couple")) {
        close_filter();
    }
    else {
        open_filter();
        choosing_box.style.display = "flex";
        if (item.classList.contains("bussiness")) {
            kid_num.style.visibility = "hidden";
        }
    }
}))

//及時更新上方Filter的數量
//debugger;
var r = document.getElementById('room-num').getElementsByTagName('span');
var a = document.getElementById('adult-num').getElementsByTagName('span');
var k = document.getElementById('kids-num').getElementsByTagName('span');

showRoom.value = filternav.room + "間房間";
showPerson.value = filternav.adult + "位大人";
if (parseInt(filternav.kids) > 0) {
    showPerson.value += "," + filternav.kids + "位兒童";
}

var s = filternav.startDate;
var e = filternav.endDate;
var start = moment(s).format('YYYY年MM月DD日');
var end = moment(e).format('YYYY年MM月DD日');
$('#range_start').val(start);
$('#range_end').val(end);

moment.locale('zh-tw');
var start = moment(s).format('dddd');
$('#week_start').val(start);

var end = moment(e).format('dddd');
$('#week_end').val(end);

var start = moment(s, 'YYYY MM DD');
var end = moment(e, 'YYYY MM DD');


$(document).click(function (e) {
    e.stopPropagation();
    var container = $(".search-filter-nav");

    //及時更新上方Filter的數量
    var active = document.getElementsByClassName('travel onUse');

    if (active.length != 0) {
        if (active[0].classList.contains("single")) {
            showPerson.value = "1位大人";
            showRoom.value = "1間客房";
        }
        if (active[0].classList.contains("couple")) {
            showPerson.value = "2位大人";
            showRoom.value = "1間客房";
        }

        else {
            showRoom.value = r[0].innerText + "間房間";
            showPerson.value = a[0].innerText + "位大人";
            if (parseInt(k[0].innerText) > 0) {
                showPerson.value += "," + k[0].innerText + "位兒童";
            }
        }
    }


    //check if the clicked area is dropDown or not
    if (container.has(e.target).length === 0) {
        close_filter();
    }

    //var Smoke = document.getElementById("Smoke");
    //var nSmoke = document.getElementById("nSmoke");
    //if (Smoke.checked) {
    //    nSmoke.style.display = "none";
    //    Smoke.style.display = "inline-block";
    //}
    //else if (nSmoke.checked) {
    //    Smoke.style.display = "none";
    //    nSmoke.style.display = "inline-block";
    //}
    //else {
    //    nSmoke.style.display = "inline-block";
    //    Smoke.style.display = "inline-block";

    //}
})


function on_Use(el) {
    el.classList.add("onUse");
}
function open_filter() {
    count_person.style.visibility = "visible";
    count_person.style.display = "flex";
    kid_num.style.visibility = "visible";
}
function close_filter() {
    choosing_box.style.display = "none";
    count_person.style.display = "none";
    kid_num.style.visibility = "hidden";

}

//var filter = document.getElementById("filter-equipment");
//// var closeFilter = document.getElementsByClassName("closeFilter");
function closeFilter() {
    debugger;
    if (filter.style.visibility == "visible") {
        filter.style.visibility = "hidden";
    }
}


//日曆
moment().format();//中文化

$('#demo').daterangepicker({
    "autoApply": true,
    "locale": {
        "format": "MM/DD/YYYY",
        "separator": " - ",
        "applyLabel": "確認",
        "cancelLabel": "取消",
        "fromLabel": "從",
        "toLabel": "到",
        "customRangeLabel": "Custom Range",
        "weekLabel": "W",
        "daysOfWeek": [
            "日", "一", "二", "三", "四", "五", "六"
        ],
        "monthNames": [
            "一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"
        ],
        "firstDay": 1
    },
    "startDate": moment(s),
    "endDate": moment(e)
}).on('change', function () {
    var date_range = $('#demo').val();
    var dates = date_range.split(" - ");

    var start1 = moment(dates[0]).format('YYYY年MM月DD日');
    var end1 = moment(dates[1]).format('YYYY年MM月DD日');

    $('#range_start').val(start1);
    $('#range_end').val(end1);
    moment.locale('zh-tw');
    var start1 = moment(dates[0]).format('dddd');

    $('#week_start').val(start1);

    var end1 = moment(dates[1]).format('dddd');

    $('#week_end').val(end1);
    $('#person-info').trigger("click");
});




//按鈕傳參數
//var btn_search = document.getElementById("btn-searchAll");
//btn_search.addEventListener('click', function () {
//    alert("Lets search");
//})

document.getElementById("btn-searchAll").addEventListener('click', function () {
    //debugger;
    hand(document.getElementsByName("search")[0].value);
});
getSavedValue(document.getElementsByName("search")[0].value);

function hand(e) {
    var name = "key";
    var val = e;
    console.log(val);
    localStorage.setItem(name, val);
    //debugger;
}
function getSavedValue(v) {
    if (!localStorage.getItem(v)) {
        //debugger;
        return "";
    }
    //debugger;
    return localStorage.getItem(v);
}