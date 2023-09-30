export default {
    props: {
        items: ['items']
    },
    template:
        `<div class="card shadow mb-4">
    <div class="card-body">
    <div class="table-responsive">
        <table class="table table-bordered table-striped table-hover" id="dataTable" width="100%" cellspacing="0">
            <thead>
                <tr>
                    <td>訂單編號</td>
                    <td>顧客編號</td>
                    <td>房型編號</td>
                    <td>房型名稱</td>
                    <td>訂單日期</td>
                    <td>入住日期</td>
                    <td>退房日期</td>
                    <td>訂房數</td>
                    <td>房間總價</td>
                    <td>名</td>
                    <td>姓</td>
                    <td>Email</td>
                    <td>手機號碼</td>
                    <td>國家</td>
                    <td>付款狀態</td>
                </tr>
            </thead>
            <tfoot>
                <tr>
                    <td>訂單編號</td>
                    <td>顧客編號</td>
                    <td>房型編號</td>
                    <td>房型名稱</td>
                    <td>訂單日期</td>
                    <td>入住日期</td>
                    <td>退房日期</td>
                    <td>訂房數</td>
                    <td>房間總價</td>
                    <td>名</td>
                    <td>姓</td>
                    <td>Email</td>
                    <td>手機號碼</td>
                    <td>國家</td>
                    <td>付款狀態</td>
                </tr>
            </tfoot>
            <tbody>
                <tr v-for="item in items" v-cloak>
                    <td>{{ item.OrderID }}</td>
                    <td>{{ item.CustomerID }}</td>
                    <td>{{ item.RoomID }}</td>
                    <td>{{ item.RoomName }}</td>
                    <td>{{ item.OrderDate }}</td>
                    <td>{{ item.CheckInDate }}</td>
                    <td>{{ item.CheckOutDate }}</td>
                    <td>{{ item.RoomCount }}</td>
                    <td>{{ item.RoomPriceTotal }}</td>
                    <td>{{ item.FirstName }}</td>
                    <td>{{ item.LastName }}</td>
                    <td>{{ item.Email }}</td>
                    <td>{{ item.PhoneNumber }}</td>
                    <td>{{ item.Country }}</td>
                    <td v-if="item.PaymentStatus == true" class="text-success">已付款</td>
                    <td v-else class="text-danger">未付款</td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
</div>`
}




