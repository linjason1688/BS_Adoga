export default {
    props:['hcard','fnav']
    ,
    filters: {
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
        emitEvent: function (name) {
            window.location.href = '/HotelDetail/' +name+ '-(' +this.fnav.start+')-(' +this.fnav.end+ ')-' +this.fnav.room+ '-' +this.fnav.adult+ '-' +this.fnav.kid;
        }
    },
    template:
        `<div class="card">
            <div class="img">
                <img src="//pix6.agoda.net/hotelImages/1158270/-1/e11b2751b3be0ee28417b1b61904cf22.jpg?s=450x450" alt="">
                <div class="small-img">
                    <img src="https://picsum.photos/id/685/50/33" alt="">
                    <img src="https://picsum.photos/id/685/50/33" alt="">
                    <img src="https://picsum.photos/id/685/50/33" alt="">
                    <img src="https://picsum.photos/id/685/50/33" alt="">
                    <div class="see-more">
                        <p>查看更多</p>
                    </div>
                </div>
            </div>
            <div class="detail">
                <a href="#">{{ hcard.HotelName }}</a>
                <div class="small-rank">
                    <div class="star">
                        <i class="fas fa-star" v-for="item in hcard.Star"></i>
                    </div>
                    <div class="address">
                        <i class="fas fa-map-marker-alt"></i>
                        <p>{{hcard.HotelAddress}}</p>
                    </div>
                </div>
                <div class="include-list">
                    <p class="Breakfast" v-if="hcard.I_RoomVM.Breakfast">早餐</p>
                    <p class="NoSomking" v-if="hcard.I_RoomVM.NoSmoking">禁煙</p>
                    <p class="wifi" v-if="hcard.I_RoomVM.WiFi">WiFi</p>
                </div>
            </div>
            <div class="rank">
                <div class="offer" v-if="hcard.I_RoomDetailVM.RoomDiscount!=0">
                    <p>只剩<span>{{(hcard.I_RoomDetailVM.RoomCount - hcard.I_RoomDetailVM.RoomOrder)}}</span>間房可享<span>{{hcard.I_RoomDetailVM.RoomDiscount * 100}}</span>％優惠</p>
                </div>
                <div class="price">
                    <p>每晚含稅價</p>
                    <del v-if="hcard.I_RoomDetailVM.RoomCount!=0">{{hcard.I_RoomVM.RoomPrice | PriceFormat}}</del>
                    <ins>NT$<span>{{parseInt(hcard.I_RoomVM.RoomPrice*(1-hcard.I_RoomDetailVM.RoomDiscount)) | PriceFormat}}</span></ins>
                </div>
                <a @click="emitEvent(hcard.HotelName)" class="moreInfo btn">查看空房情況</a>
            </div>
        </div>`
}

//+'-('+filternav.start+')-('+filternav.end+')-'+filternav.room+'-'+filternav.adult+'-'+filternav.kids