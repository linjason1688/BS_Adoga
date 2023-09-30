export default {
    props: {
        room:['room']
    },
    template: ` <div v-bind:key="room.RoomID" v-bind:id="room.RoomID" class="room-type mb-4">
                    <div class="room-type-title">
                        <h5>{{room.RoomName}}</h5>
                    </div>
                    <div class="container wrap-1100">
                        <div class="row">
                            <div class="col-3 d-flex flex-column">
                                <p>房型摘要</p>
                                <img v-bind:src="room.RoomImgURL">
                                <a v-on:click="clickAlbum" class="link fz-14 mt-1">查看房間照片&設施</a>
                                <div class="service-group mt-4">
                                    <div v-if="room.WiFi" class="d-flex align-items-center mb-2 color-green">
                                        <i class="fas fa-wifi me-3"></i>
                                        <span class="fz-14">免費Wi-Fi</span>
                                    </div>
                                    <div v-for="bed in room.RoomBed" class="d-flex align-items-center mb-2">
                                        <i class="fas fa-bed me-3"></i>
                                        <span class="fz-14">{{bed.Amount}}張{{bed.Name}}</span>
                                    </div>
                                    <div v-if="room.NoSmoking" class="d-flex align-items-center mb-2">
                                        <i class="fas fa-smoking-ban me-3"></i>
                                        <span class="fz-14">禁菸房</span>
                                    </div>
                                    <div class="d-flex align-items-center mb-2">
                                        <i class="fas fa-bath me-3"></i>
                                        <span class="fz-14">{{room.BathroomName}}</span>
                                    </div>
                                    <!-- <div id="hover-equim-serv" @@mouseover="hover=true" @@mouseleave="hover=false">
                                            <div class="d-flex align-items-center mb-2 color-blue">
                                                <i class="far fa-plus-square me-3"></i>
                                                <span class="fz-14">看看所有設施與服務</span>
                                            </div>
                                            <div class="equim-serv" v-show="hover">
                                                ddd
                                            </div>
                                        </div> -->
                                </div>
                            </div>

                            <div class="col-9">
                                <div class="row field">
                                    <div class="col-4">
                                        <p>優惠內容&取消規定</p>
                                    </div>
                                    <div class="col-2">
                                        <p>人數限制</p>
                                    </div>
                                    <div class="col-2">
                                        <p>每晚價格/每房</p>
                                    </div>
                                    <div class="col-1">
                                        <p>房數</p>
                                    </div>
                                    <div class="col-3">
                                    </div>
                                </div>
                                <div class="row room-case one-case pe-3 mb-3">
                                    <div class="col-4 pt-2">
                                        <p class="fz-12 fw-bold mb-2">優惠專案</p>
                                        <p v-if="room.Breakfast" class="fz-14 mb-2">
                                            <i class="fas fa-check color-green fz-14 me-2"></i>
                                            免費早餐 - 1份
                                            <span class="fw-bold color-green"> 包含</span>
                                        </p>
                                        <p v-if="room.WiFi" class="fz-14 mb-2">
                                            <i class="fas fa-check color-green fz-14 me-2"></i>
                                            免費Wi-Fi
                                        </p>
                                    </div>
                                    <div class="col-2 pt-2 color-gray text-center">
                                        <div>
                                            <span class="fz-14">{{room.Adult}} x </span>
                                            <i class="fas fa-male fz-20"></i>
                                        </div>
                                        <div>
                                            <span class="fz-14">{{room.Child}} x </span>
                                            <i class="fas fa-child fz-16"></i>
                                        </div>
                                        <p class="fz-12">
                                            1 位未滿3歲兒童可<span class="color-green fw-bold">免費</span>同住
                                        </p>
                                    </div>
                                    <div class="col-2 pt-2 text-end ps-0">
                                        <p v-if="room.RoomDiscount!=0" class="text-start mb-3">
                                            <span class="room-case-discount">今日{{room.RoomDiscount}}折！</span>
                                        </p>
                                        <del v-if="room.RoomDiscount!=0"
                                             class="room-case-price fz-16 color-gray fw-bold">
                                            <span class="fz-16">NT$</span>
                                            {{room.RoomPrice}}
                                        </del>
                                        <p class="room-case-price">
                                            <span class="fz-16">NT$</span>
                                            {{room.RoomNowPrice}}
                                        </p>
                                        <p class="color-gray fz-12">每晚低至</p>
                                    </div>
                                    <div class="col-1 pt-2 text-center">
                                        <select disabled>
                                            <option :value="room.RoomOrder" selected>{{room.RoomOrder}}</option>
                                        </select>
                                    </div>
                                    <div class="col-3 pt-2 text-end">
                                        <button v-on:click="room.Booking" class="keep-btn btn fz-16 py-2 mb-2">訂房</button>
                                        <!--<button class="cart-btn btn fz-14 py-2">加入購物車</button>-->
                                        <p class="remain fw-bold fz-14 color-red mt-5 mb-4">本站剩{{room.RoomLeft}}間！</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`,
    methods: {
        clickAlbum() {
            console.log('觸發組件的方法了')
            console.log(this.room)
            this.$emit('album', this.room);
        }
    }
}