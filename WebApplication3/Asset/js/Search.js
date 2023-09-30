var filter = new Vue({
    el: '#filter',
    data: {
        equipmentsNum: {
            swim: 15,
            airport: 44,
            familyFriendly: 468,
            restaurant: 120,
            club: 3,
            golf: 1,
            internet: 606,
            gym: 38,
            nSmoke: 288,
            Smoke: 144,
            FFG: 100,
            carPark: 317,
            frontDesk: 139,
            Spa: 17,
            petAllow: 102,
            business: 61
        },
        equipmentsName: {
            swim: '游泳池',
            airport: '機場接送',
            familyFriendly: '親子友善住宿',
            restaurant: '餐廳',
            club: '夜店',
            golf: '附設高爾夫球場',
            internet: '網路',
            gym: '健身房',
            nSmoke: '禁菸',
            Smoke: '吸菸區',
            FFG: '無障礙友善設施',
            carPark: '停車場',
            frontDesk: '24小時櫃台服務',
            Spa: 'Spa桑拿',
            petAllow: '可帶寵物',
            business: '商務設備'
        }
    },
    method: {

    }
})

// JS
var openFilter = document.getElementById("openFilter");
var filter = document.getElementById("filter-equipment");
// var closeFilter = document.getElementsByClassName("closeFilter");

openFilter.addEventListener('click', function () {
    if (filter.style.visibility == "visible") {
        filter.style.visibility = "hidden";
    }
    else { filter.style.visibility = "visible"; }
})

function closeFilter() {
    if (filter.style.visibility == "visible") {
        filter.style.visibility = "hidden";
    }
}

function dropright() {
    var droprightBox = document.getElementsByClassName("dropright-box");
    // debugger;
    // droprightBox.style.visibility = "visible";

    //下拉框查询组件点击查询栏时不关闭下拉框
    //需要在tag裡加入data-stopPropagation="true"才能執行成功
    event.stopPropagation();

}

var dropdownItem = document.querySelectorAll('.dropdown-item');
dropdownItem.forEach(item => item.getElementsByClassName("fas fa-minus").addEventListener('click', function () {
    debugger;
    alert("Minus");
}))
dropdownItem.forEach(item => item.addEventListener('click', function (e) {
    // e.stopPropagation();

}))