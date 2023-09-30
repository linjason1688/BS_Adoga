import $bus from './SearchDataComponent.js';

var allHotel = '';
var list;
var btnOrderPrice = document.getElementById("orderPrice");
var btnOrderStar = document.getElementById("orderStar");

btnOrderPrice.addEventListener("click", function () {
    orderItem = "price";
})

btnOrderStar.addEventListener("click", function () {
    orderItem = "Star";
})

//axios去get資料先
axios.get('../api/Search/GetHotelByCity', {
    //一開始用params裡面的資料去跑Api抓資料；成功抓完資料就會跑response
    params: {
        CityName: filternav.Value,
        startDate: filternav.startDate,
        endDate: filternav.endDate,
        adult: filternav.adult,
        kid: filternav.kids,
        room: filternav.room,
    }
}).then(function (response) {
    HotelList(response.data);
}).catch((error) => console.log(error))

function HotelList(response) {
    var DataList = response;

    Vue.component('hotel-card', {
        data() {
            return {
                //預設目前的頁面為第幾頁
                pageNumber: 0,
            }
        },
        props: {
            listData: {
                //收集這頁所需要的所有資料
                type: Array,
                required: true
            },
            size: {
                //這頁所需要的資料數量
                type: Number,
                required: false,
                default: 3
            },
        },
        filters: {
            //設定價錢的格式
            PriceFormat: function (price) {
                if (!price) return '0'
                // 获取整数部分
                const intPart = Math.trunc(price)
                // 整数部分处理，增加,
                const intPartFormat = intPart.toString().replace(/(\d)(?=(?:\d{3})+$)/g, '$1,')

                return intPartFormat
            }
        },
        methods: {
            nextPage() {
                this.pageNumber++;
            },
            prevPage() {
                this.pageNumber--;
            },
            emitEvent: function (name) {
                var url = '/HotelDetail/' + name + '-('
                    + DateFormat(filternav.startDate) + ')-('
                    + DateFormat(filternav.endDate) + ')-'
                    + filternav.room + '-'
                    + filternav.adult + '-' 
                    + filternav.kids;
                //debugger;
                window.location.href = url;
            }
        },
        computed: {
            //計算出最後一頁在第幾頁
            pageCount() {
                let l = this.listData.length,
                    s = this.size;
                return Math.ceil(l / s);
            },
            //單頁所需要的所有資料
            paginatedData() {
                const startData = this.pageNumber * this.size,
                    endData = startData + this.size;
                //debugger;
                return this.listData.slice(startData, endData);
            },
        },
        template: `<div>
                <div v-for="hotel in paginatedData">
                    <div class="card">
                        <div class="img">
                            <img :src="hotel.imgUrl" alt="">
                            <div class="small-img">
                                <img  v-for="i in 3" src="https://res.cloudinary.com/dodoko/image/upload/v1628084775/Adoga/Hotel/CosmosHotelTaipei/CosmosHotelTaipei_img04.webp" alt="">
                            </div>
                        </div>
                        <div class="detail">
                            <a href="#">{{ hotel.HotelName }}</a>
                            <div class="small-rank">
                                <div class="star">
                                    <i class="fas fa-star" v-for="item in hotel.Star"></i>
                                </div>
                                <div class="address">
                                    <i class="fas fa-map-marker-alt"></i>
                                    <p>{{hotel.HotelAddress}}</p>
                                </div>
                            </div>
                            <div class="include-list">
                                <p class="Breakfast" v-if="hotel.I_RoomVM.Breakfast">早餐</p>
                                <p class="NoSomking" v-if="hotel.I_RoomVM.NoSmoking">禁煙</p>
                                <p class="wifi" v-if="hotel.I_RoomVM.WiFi">WiFi</p>
                            </div>
                        </div>
                        <div class="rank">
                            <div class="offer" v-if="hotel.I_RoomDetailVM.RoomDiscount!=0">
                                <p>只剩<span>{{(hotel.I_RoomDetailVM.RoomCount - hotel.I_RoomDetailVM.RoomOrder)}}</span>間房可享<span>{{hotel.I_RoomDetailVM.RoomDiscount*100}}</span>％優惠</p>
                            </div>
                            <div class="price">
                                <p>每晚含稅價</p>
                                <del v-if="hotel.I_RoomDetailVM.RoomDiscount!=0">{{hotel.I_RoomVM.RoomPrice | PriceFormat}}</del>
                                <ins>NT$<span>{{Math.ceil(hotel.I_RoomVM.RoomPrice*(1-hotel.I_RoomDetailVM.RoomDiscount)) | PriceFormat}}</span></ins>
                            </div>
                            <a @click="emitEvent(hotel.HotelName)" class="moreInfo btn">查看空房情況</a>
                        </div>
                    </div>
                </div>
                <div class="d-flex justify-content-center">
                    <button class="btn"
                        :disabled="pageNumber === 0" 
                        @click="prevPage">
                        上一頁
                    </button>
                    <button class="btn"
                        :disabled="pageNumber >= pageCount -1" 
                        @click="nextPage">
                        下一頁
                    </button>
                </div>
            </div>`
    })

    

    new Vue({
        el: "#infomation",
        data: {
            card: DataList,
            orderKey: "price",     
            star: [],
            hotel: [],
            room: []
        },
        created: function () {
            //debugger;
            $bus.$on('onStar', this.starArrange);
            $bus.$on('hotelFacility', this.hotelArrange);
            $bus.$on('roomFacility', this.roomArrange);
        },
        methods: {
            orderPrice() {
                this.orderKey = "price";
                this.card = sortByKey(this.card, this.orderKey);
            },
            orderStar() {
                this.orderKey = "Star";
                this.card = sortByKey(this.card, this.orderKey);
            },
            starArrange(arrayStar) {
                //debugger;
                this.star = arrayStar;
                this.card = Arrangement(DataList, this.star, this.hotel, this.room);
            },
            hotelArrange(arrayData) {
                //debugger;
                this.hotel = arrayData;
                this.card = Arrangement(DataList, this.star, this.hotel, this.room);
            },
            roomArrange(arrayData) {
                //debugger;
                this.room = arrayData;
                this.card = Arrangement(DataList, this.star, this.hotel, this.room);
            }
        }
    })
}

function DateFormat(date) {
    var date = new Date(date);
    return date.getFullYear() + '-' + (date.getMonth() + 1).toString().padStart(2, '0') + '-' + date.getDate().toString().padStart(2, '0');
}

function sortByKey(array, key) {
    return array.sort(function (a, b) {
        var x;
        var y;

        //Price順序
        if (key == "price") {
            x = a.I_RoomVM.RoomPrice * (1 - a.I_RoomDetailVM.RoomDiscount);
            y = b.I_RoomVM.RoomPrice * (1 - b.I_RoomDetailVM.RoomDiscount);
            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
        }
        //Star逆序
        else {
            x = a[key];
            y = b[key];
            return ((x > y) ? -1 : ((x < y) ? 1 : 0));
        }
    })
}

function Arrangement(listdata, star, fac, room) {
    //debugger;
    let list = [];
    for (var i = 0; i < listdata.length; i++) {
        if (star.length == 0 && fac.length == 0 && room.length==0) {
            debugger;
            list.push(listdata[i]);
        }
        else {
            var alltrue;
            if (star.length == 0) { alltrue = true; }
            else { alltrue = star.includes(listdata[i].Star); }

            if (fac.length != 0) {
                for (var l = 0; l < fac.length; l++) {
                    if (fac[l].haveFacility == true) {
                        var name = fac[l].facility;
                        var now = listdata[i]["I_FacilityVM"][name];
                        //debugger;
                        if (now == false) {
                            alltrue = false;
                        }
                    }
                }
            }
            if (room.length != 0) {
                for (var l = 0; l < fac.length; l++) {
                    if (room[l].haveFacility == true) {
                        var name = room[l].facility;
                        var now = listdata[i]["I_FacilityVM"][name];
                        if (now == false) {
                            alltrue = false;
                        }
                    }
                }
            }
            
            if (alltrue == true) {
                debugger;
                list.push(listdata[i]);
            }
        }
    }
    debugger;
    return list;
}
