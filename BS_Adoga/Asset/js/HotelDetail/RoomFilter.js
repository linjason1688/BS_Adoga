import RoomType from './RoomComponent.js'

$().ready(function () {

    function SetRoomAlbumData(resData, roomData) {
        //console.log('開始設定albumdata，看下roomData')
        roomAlbum.$set(roomAlbum, 'RoomInfo',
            {
                RoomID: roomData.RoomID,
                RoomName: roomData.RoomName,
                FreeWiFi: roomData.WiFi,
                NoSmoking: roomData.NoSmoking,
                Bathroom: roomData.BathroomName,
                Bed: roomData.RoomBed,
                RoomDiscount: roomData.RoomDiscount,
                RoomNowPrice: roomData.RoomNowPrice,
                RoomLeft: roomData.RoomLeft
            });
        //console.log("看看setroominfo後的全部data")
        //console.log(roomAlbum.RoomInfo)
        for (i = 0; i < resData.length; i++) {
            roomAlbum.$set(roomAlbum.ImgGroup, i,
                {
                    ImgID: resData[i].ImageID,
                    ImgURL: resData[i].ImageURL,
                    Index: i,
                    IsActive: false
                });
        }
        roomAlbum.ImgGroup[0].IsActive = true;
        //console.log("看看setimgGroup後的全部data")
        //console.log(roomAlbum)
    }

    function SetRoomTypeData(response) {
        //這邊response進來好像就轉成js了，可以直接用，所以不需要用JSON.parse轉格式喔
        roomGroup.group = [];
        var data = response;
        (data).forEach((item, index) => {
            // a = item.HotelID
            // roomGroup.group = Object.assign({ }, roomGroup.group,)
            // roomGroup.group.push(

            //給RoomBed[]塞物件 方法1
            var roomBed = [];
            var bedTypeStr = '';
            (item.RoomBed).forEach((bed, index) => {
                roomBed.push({
                    Name: bed.Name,
                    Amount: bed.Amount
                })
                if (index != item.RoomBed.length - 1)
                    bedTypeStr += bed.Name + ","
                else
                    bedTypeStr += bed.Name
            })
            roomGroup.$set(roomGroup.group, index,
                {
                    HotelID: item.HotelID,
                    RoomID: item.RoomID,
                    RoomName: item.RoomName,
                    RoomImgURL: item.RoomImgURL,
                    WiFi: item.WiFi,
                    Breakfast: item.Breakfast,
                    NoSmoking: item.NoSmoking,
                    BathroomName: item.BathroomName,
                    RoomBed: roomBed,
                    Adult: item.Adult,
                    Child: item.Child,
                    RoomOrder: item.RoomOrder,
                    RoomPrice: Math.ceil(item.RoomPrice).toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","),
                    RoomDiscount: Math.round((1 - item.RoomDiscount) * 10),
                    RoomNowPrice: Math.ceil(item.RoomNowPrice).toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","),
                    RoomLeft: item.RoomLeft,
                    Booking: function () {
                        axios.get('../HotelDetail/SetCheckOutData', {
                            params: {
                                hotelId: item.HotelID,
                                roomId: item.RoomID,
                                roomName: item.RoomName,
                                noSmoking: item.NoSmoking,
                                breakfast: item.Breakfast,
                                bedType: bedTypeStr,
                                roomOrder: item.RoomOrder,
                                roomPrice: item.RoomPrice,
                                roomDiscount: item.RoomDiscount,
                                roomNowPrice: item.RoomNowPrice
                            }
                        }).then(function (response) {
                            console.log(response);
                            window.location.href = '../CheckOut/Index';
                        }).catch((error) => console.log(error));
                    }
                }
            );

            //給RoomBed[]塞物件 方法2
            // for (var i = 0; i < item.RoomBed.length; {
            //     roomGroup.group[index].RoomBed.push({
            //         Name: item.RoomBed[i].Name,
            //         Amount: item.RoomBed[i].Amount
            //     })
            // }
        })
    }

    //一開始載入頁面時要帶入Room的全部資料
    axios.get('../api/HotelDetail/GetAllRoom', {
        params: {
            hotelName: filternav.Value,
            startDate: filternav.startDate,
            endDate: filternav.endDate,
            orderRoom: filternav.room,
            adult: filternav.adult,
            child: filternav.kids,
        }
    }).then(function (response) {
        SetRoomTypeData(response.data);
    }).catch((error) => console.log(error))

    var filterRoom = new Vue({
        el: '#filter-room',
        data: {
            FreeBreakfast: false,
            NoSmoking: false,
            FamilyRoom: false
        },
        watch: {
            FreeBreakfast() {
                //console.log(`brkf:${this.FreeBreakfast}`)
                this.filter();
            },
            NoSmoking() {
                //console.log(`nosmoking:${this.NoSmoking}`)
                this.filter();
            },
            FamilyRoom() {
                //console.log(`family:${this.FamilyRoom}`)
                this.filter();
            }
        },
        methods: {
            filter() {
                if (this.FreeBreakfast || this.NoSmoking || this.FamilyRoom) {
                    console.log((this.FreeBreakfast || this.NoSmoking || this.FamilyRoom))
                    roomGroup.isFiltered = true;
                }
                if (!this.FreeBreakfast && !this.NoSmoking && !this.FamilyRoom) {
                    console.log((!this.FreeBreakfast && !this.NoSmoking && !this.FamilyRoom))
                    roomGroup.isFiltered = false;
                }
                axios.get('../api/HotelDetail/GetSpecificRoom', {
                    params: {
                        hotelName: filternav.Value,
                        startDate: filternav.startDate,
                        endDate: filternav.endDate,
                        orderRoom: filternav.room,
                        adult: filternav.adult,
                        child: filternav.kids,
                        freeBreakfast: this.FreeBreakfast,
                        noSmoking: this.NoSmoking,
                        family: this.FamilyRoom,
                    }
                }).then(function (response) {
                    SetRoomTypeData(response.data)
                }).catch((error) => console.log(error))
            },
            clearFilter() {
                this.FreeBreakfast = false;
                this.NoSmoking = false;
                this.FamilyRoom = false;
                //$.post('../api/HotelDetail/GetAllRoom', SetRoomTypeData)
                axios.get('../api/HotelDetail/GetAllRoom', {
                    params: {
                        hotelName: filternav.Value,
                        startDate: filternav.startDate,
                        endDate: filternav.endDate,
                        orderRoom: filternav.room,
                        adult: filternav.adult,
                        child: filternav.kids,
                    }
                }).then(function (response) {
                    SetRoomTypeData(response.data);
                }).catch((error) => console.log(error))
            }
        }
    })

    var roomGroup = new Vue({
        el: '#room-group',
        data: {
            group: [],
            isFiltered: false
        },
        methods: {
            OpenAlbum(data) {
                //console.log('成功觸發父層event')
                //console.log(data);
                axios.get('../api/HotelDetail/GetRoomImages', {
                    params: {
                        hotelID: data.HotelID,
                        roomID: data.RoomID
                    }
                }).then(function (response) {
                    //console.log(response.data);
                    SetRoomAlbumData(response.data, data);
                    //console.log("axios內層")
                    //console.log(roomAlbum.RoomInfo)
                    let roomAlbumDom = document.getElementById('room-album');
                    roomAlbumDom.style.display = 'block';
                }).catch((error) => console.log(error))
                //console.log("axios外層")
                //console.log(roomAlbum.RoomInfo) 
            }
        },
        components: {
            'room-type': RoomType
        }
    })

    var roomAlbum = new Vue({
        el: '#room-album',
        data: {
            RoomInfo: {},
            ImgGroup: []
        },
        methods: {
            ImgChange(index) {
                this.ImgGroup[index].IsActive = true;
                for (i = 0; i < this.ImgGroup.length; i++) {
                    if (i != index) {
                        this.ImgGroup[i].IsActive = false;
                    }
                }
            },
            closeAlbum() {
                let roomAlbumDom = document.getElementById('room-album');

                let closeRoomAlbum = document.getElementById('room-album-close-icon');
                closeRoomAlbum.addEventListener('click', function () {
                    //console.log('close');
                    roomAlbumDom.style.display = 'none';
                })
            }
        }
    })
})