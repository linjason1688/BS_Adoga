export default {
    props: {
        order: ['order']
    },
    template:
        `<div class="booking-item" >
            <div class="side-info">
                <div class="time-place-item">
                    <span class="time me-2"><i class="far fa-circle me-2"></i>{{order.FewDaysAgo}}天前</span>
                    <span class="place"> | <i class="fas fa-map-marker-alt mx-2"></i> {{order.City}}</span>
                </div>
                <time>{{order.OrderDateStr}}預訂</time>
            </div>
            <div class="booking-info-wrap py-5 ps-4">
                <div class="booking-info">
                    <div class="main d-flex">
<div class="img-wrap">
                        <img :src="order.HotelImageURL" alt="">
</div>
                        <div class="hotel-status mt-3 mx-4 ">
                            <h4>{{order.HotelName}} {{order.HotelEngName}}</h4>
                            <p class="order-title mb-1">訂單編號：{{order.OrderID}}</p>
                            <p v-if="order.PayStatus && order.CheckCheckOut < 1" class="order-status-suc"><i
                                    class="fas fa-check-circle me-2"></i>已退房</p>
                            <p v-if="order.PayStatus && order.CheckCheckOut >= 1" class="order-status-suc"><i class="fas fa-check-circle me-2"></i>已付款</p>
                            <p v-if="order.PayStatus == false" class="order-status-fail"><i
                                    class="fas fa-times-circle me-2"></i>未付款</p>
                            <p class="room-type fw-bold mt-5 mb-0">{{order.BedStr}}</p>
                            <p v-if="order.Breakfast" class="free-status"><span
                                    class="free-tag me-2">免費</span><span>早餐</span></p>
                        </div>
                        <div class="time-price-status mt-3">
                            <div class="time-info">
                                <div class="start-date me-4">
                                    <span>入住日期</span>
                                    <div class="start-date-detail">
                                        <span class="day">{{order.CheckInDay}}</span>
                                        <div class="month-week">
                                            <p>{{order.CheckInMonth}}</p>
                                            <p>{{order.CheckInWeek}}</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="end-date me-4">
                                    <span>退房日期</span>
                                    <div class="end-date-detail">
                                        <span class="day">{{order.CheckOutDay}}</span>
                                        <div class="month-week">
                                            <p>{{order.CheckOutMonth}}</p>
                                            <p>{{order.CheckOutWeek}}</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <p class="price-info mt-5">
                                <span>TWD</span>
                                {{order.RoomPriceTotal}}
                            </p>
                        </div>

                    </div>
                    <div class="footer">
                        <div class="link-group">
                            <a v-if="order.PayStatus && order.CheckCheckOut < 1" data-bs-toggle="modal" data-bs-target="#large" @click="order.Evaluation">留下住宿評鑑</a>
                            <a href="/">訂別間</a>
                            <a v-if="order.PayStatus==false && order.In24Hours" @click="order.ContinuePay">立即付款</a>
                        </div>
                        <a @click="order.GoToDetail"  type="button" class="detail-btn">查看更多細節</a>
                    </div>
                </div>
            </div>
        </div> `
}