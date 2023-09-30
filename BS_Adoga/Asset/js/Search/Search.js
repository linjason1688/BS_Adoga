import $bus from './SearchDataComponent.js';

Vue.component('filterstar', {
    data() {
        return {
            select:[]
        }
    },
    props: {
        starList: {
            type: Array,
            required: true
        }
    },
    methods: {
        AddStar: function (num) {
            if (checkData(this.select, num)) {
                this.select.push(num);
            }
        },
    },
    watch: {
        select: function(){
            //console.log(this.select);
            //debugger;
            $bus.$emit('onStar', this.select);
        }
    },
    template: `<ul id="dropdown-star">
                    <li>星级</li>
                    <li class="dropdown-item" v-for="item in starList">
                        <input class="form-check-input" type="checkbox" :id="item.name" @click="AddStar(item.num)">
                        <label class="form-check-label" :for="item.name">
                            <p v-if="item.num==0">尚未標示</p>
                            <i v-else v-for="s in item.num" class="fas fa-star"></i>
                        </label>
                    </li>
                </ul>`
})

new Vue({
    el: '#filter',
    data: {
        star: [
            {
                num: 5,
                name: "five"
            },
            {
                num: 4,
                name: "four"
            },
            {
                num: 3,
                name: "three"
            },
            {
                num: 2,
                name: "two"
            },
            {
                num: 1,
                name: "one"
            },
            {
                num: 0,
                name: "zero"
            }]
    }
})

var equipment = new Vue({
    el: '#filter-equipment',
    data: {
        required:[],
        hotelFacilities: [
            {
                facility: "SwimmingPool",
                facilityName: "游泳池",
                haveFacility: false
            },
            {
                facility: "AirportTransfer",
                 facilityName: "機場接送",
                 haveFacility: false
             },
            {
                facility: "FamilyChildFriendly",
                 facilityName: "親子友善住宿",
                 haveFacility: false
             },
            {
                facility: "Restaurants",
                 facilityName: "餐廳",
                 haveFacility: false
             },
            {
                facility: "Nightclub",
                 facilityName: "夜店",
                 haveFacility: false
             },
            {
                facility: "GolfCourse",
                 facilityName: "附設高爾夫球場",
                 haveFacility: false
             },
            {
                 facility: "Gym",
                 facilityName: "健身房",
                 haveFacility: false
             },
            {
                facility: "NoSmoking",
                 facilityName: "禁菸區",
                 haveFacility: false
             },
            {
                facility: "SmokingArea",
                 facilityName: "吸菸區",
                 haveFacility: false
             },
            {
                facility: "FacilitiesFordisabledGuests",
                 facilityName: "無障礙友善設施",
                 haveFacility: false
             },
            {
                facility: "CarPark",
                 facilityName: "停車場",
                 haveFacility: false
             },
            {
                facility: "FrontDesk",
                 facilityName: "24小時櫃台服務",
                 haveFacility: false
             },
            {
                facility: "SpaSauna",
                 facilityName: "Spa桑拿",
                 haveFacility: false
             },
            {
                facility: "BusinessFacilities",
                 facilityName: "商務設備",
                 haveFacility: false
             }
        ],
        roomFacilities: [
            {
                facility: "Internet",
                facilityName: "網路",
                haveFacility: false
            },
            {
                facility: "PetsAllowed",
                facilityName: "可帶寵物",
                haveFacility: false
            }
        ]
    },
    watch: {
        hotelFacilities: {
            handler: function (e) {
                //debugger;
                $bus.$emit('hotelFacility', e);
            },
            deep:true
        },
        roomFacilities: {
            handler: function (e) {
                //console.log(e);
                //debugger;
                $bus.$emit('roomFacility', e);
            },
            deep: true
        }
    },
})


function checkData(arraylist, num) {
    if (arraylist.length == 0) {
        //初始資料量為0，直接用for會出bug
        return true;
    }
    else {
        //如果判斷發現有重複的就直接移除
        for (var i = 0; i < arraylist.length; i++) {
            if (arraylist[i] == num) {
                arraylist.splice(i, 1);
                return false;
            }
        }
        return true;
    }
    
}

// JS
var btnStar = document.getElementById("filter-star");
var ulStar = document.getElementById("dropdown-star");

btnStar.addEventListener('click', function () {
    if (ulStar.style.display == "none") {
        ulStar.style.display = "block";
    }
    else {
        ulStar.style.display = "none";
    }
})

var openFilter = document.getElementById("openFilter");
var filter = document.getElementById("filter-equipment");

document.getElementById("closeFilter").addEventListener('click', Closefilter);
document.getElementById("blank").addEventListener('click', Closefilter);

document.getElementById("clear-filter").addEventListener('click', function () {
    for (i = 0; i < equipment.hotelFacilities.length; i++) {
        equipment.hotelFacilities[i].haveFacility = false;
    }
})


openFilter.addEventListener('click', openClose);

function openClose() {
    debugger;
    if (filter.style.visibility == "visible") {
        Closefilter();
    }
    else {
        filter.style.visibility = "visible";
    }
}

function Closefilter() {
    filter.style.visibility = "hidden";
}


function dropright() {
    var droprightBox = document.getElementsByClassName("dropdown-menu");
    // debugger;
    // droprightBox.style.visibility = "visible";

    //droprightBox.addEventListener('click', function () {

    //})
        //下拉框查询组件点击查询栏时不关闭下拉框
        //需要在tag裡加入data-stopPropagation="true"才能執行成功
        event.stopPropagation();

}


//觸發彈窗底部頁面禁止滑動
//function fixed() {
//    var scrollTop = document.body.scrollTop;//設定背景元素的位置
//    $('#filter').attr('data-top', scrollTop);
//    var contentStyle = document.getElementById("filter").style;//content是可以滾動的背景元素id名稱
//    contentStyle.position = 'fixed'; //contentStyle是第二步的變數，設定背景元素的position屬性為‘fixed’
//    contentStyle.top = "-" + scrollTop + "px";
//}

////關閉彈窗底部頁面恢復滑動
//function fixed_cancel() {
//    var contentStyle = document.getElementById("filter").style;
//    var scrollTop = $('#filter').attr('data-top');//設定背景元素的位置
//    contentStyle.top = '0px';//恢復背景元素的初始位置
//    contentStyle.position = "static";//恢復背景元素的position屬性（初始值為absolute，就恢復為absolute，以此類推）
//    $(document).scrollTop(scrollTop);//scrollTop,設定滾動條的位置
//}