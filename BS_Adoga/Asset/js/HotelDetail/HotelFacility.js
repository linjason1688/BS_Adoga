
import Facilities from './HotelFacilityComponent.js';

var icons = {
    wifi: "fas fa-wifi",
    utensils: "fas fa-utensils",
    smoking: "fas fa-smoking",
    noSmoking: "fas fa-smoking-ban",
    check_circle: "far fa-check-circle",
    language: "fas fa-language",
    gym: "fas fa-dumbbell",
    swimmingPool: "fas fa-swimming-pool",
    car: "fas fa-car",
}

var facilityData = '';
var facilitiesVue = '';


axios.get('../api/HotelDetail/GetHotelFacilities', {
    params: {
        hotelName: "台中商旅",
    }
}).then(function (response) {
    console.log(response.data);
    facilityData = response.data;
    facilitiesVue = new Vue({
        el: '#facility-collapse',
        data: {
            categories: [
                {
                    categoryName: "可使用語言",
                    categoryIcon: "fas fa-globe",
                    facilities: [
                        {
                            facility: "Chinese",
                            facility_Ch: "中文",
                            icon: icons.language,
                            haveFacility: true
                        },
                        {
                            facility: "English",
                            facility_Ch: "英語",
                            icon: icons.language,
                            haveFacility: true
                        },
                    ]
                },
                {
                    categoryName: "兒童專屬",
                    categoryIcon: "fas fa-child",
                    facilities: [
                        {
                            facility: "FamilyChildFriendly",
                            facility_Ch: "家庭房",
                            icon: "fas fa-users",
                            haveFacility: facilityData["FamilyChildFriendly"]
                        },
                    ]
                },
                {
                    categoryName: "友善設施",
                    categoryIcon: "fas fa-wheelchair",
                    facilities: [
                        {
                            facility: "FacilitiesFordisabledGuests",
                            facility_Ch: "無障礙友善設施",
                            icon: icons.check_circle,
                            haveFacility: facilityData["FacilitiesFordisabledGuests"]
                        },
                        {
                            facility: "Elavator",
                            facility_Ch: "電梯",
                            icon: icons.check_circle,
                            haveFacility: true
                        },
                    ]
                },
                {
                    categoryName: "網路服務",
                    categoryIcon: icons.wifi,
                    facilities: [
                        {
                            facility: "Internet",
                            facility_Ch: "公共區域Wi-Fi",
                            icon: icons.wifi,
                            haveFacility: facilityData["Internet"]
                        },
                        {
                            facility: "RoomFreeWi-Fi",
                            facility_Ch: "房內免費Wi-Fi",
                            icon: icons.wifi,
                            haveFacility: true
                        },
                    ]
                },
                {
                    categoryName: "休閒娛樂設施",
                    categoryIcon: icons.swimmingPool,
                    facilities: [
                        {
                            facility: "SwimmingPool",
                            facility_Ch: "室內泳池",
                            icon: icons.swimmingPool,
                            haveFacility: facilityData["SwimmingPool"]
                        },
                        {
                            facility: "Gym",
                            facility_Ch: "健身房",
                            icon: icons.gym,
                            haveFacility: facilityData["Gym"]
                        },
                        {
                            facility: "SpaSauna",
                            facility_Ch: "Spa/桑拿",
                            icon: "fas fa-spa",
                            haveFacility: facilityData["SpaSauna"]
                        },
                        {
                            facility: "GolfCourse",
                            facility_Ch: "高爾夫球場",
                            icon: "fas fa-golf-ball",
                            haveFacility: facilityData["GolfCourse"]
                        },
                    ]
                },
                {
                    categoryName: "餐飲服務",
                    categoryIcon: icons.utensils,
                    facilities: [
                        {
                            facility: "Restaurants",
                            facility_Ch: "餐廳",
                            icon: icons.utensils,
                            haveFacility: facilityData["Restaurants"]
                        },
                        {
                            facility: "NightClub",
                            facility_Ch: "酒吧",
                            icon: "fas fa-glass-martini-alt",
                            haveFacility: facilityData["NightClub"]
                        },
                        {
                            facility: "FreeBreakfast",
                            facility_Ch: "早餐(免費)",
                            icon: icons.utensils,
                            haveFacility: true
                        },
                    ]
                },
                {
                    categoryName: "服務與便利設施",
                    categoryIcon: "fas fa-concierge-bell",
                    facilities: [
                        {
                            facility: "BusinessFacilities",
                            facility_Ch: "商務會議室",
                            icon: "fas fa-briefcase",
                            haveFacility: facilityData["BusinessFacilities"]
                        },
                        {
                            facility: "FrontDesk",
                            facility_Ch: "櫃台服務",
                            icon: "fas fa-user-tie",
                            haveFacility: facilityData["FrontDesk"]
                        },
                        {
                            facility: "SmokingArea",
                            facility_Ch: "吸菸區",
                            icon: icons.smoking,
                            haveFacility: facilityData["SmokingArea"]
                        },
                        {
                            facility: "NoSmoking",
                            facility_Ch: "禁菸區",
                            icon: icons.noSmoking,
                            haveFacility: facilityData["NoSmoking"]
                        },
                        {
                            facility: "PetsAllowed",
                            facility_Ch: "可帶寵物",
                            icon: "fas fa-paw",
                            haveFacility: facilityData["PetsAllowed"]
                        },
                        {
                            facility: "Room",
                            facility_Ch: "門房",
                            icon: "fas fa-door-open",
                            haveFacility: true
                        },
                    ]
                },
                {
                    categoryName: "交通服務/設施",
                    categoryIcon: icons.car,
                    facilities: [
                        {
                            facility: "AirportTransfer",
                            facility_Ch: "機場接送",
                            icon: "fas fa-plane",
                            haveFacility: facilityData["AirportTransfer"]
                        },
                        {
                            facility: "CarPark",
                            facility_Ch: "停車場(免費)",
                            icon: "fas fa-parking",
                            haveFacility: facilityData["CarPark"]
                        },
                    ]
                },
            ]
        },
        components: {
            'facility-icon': Facilities
        }
    })
}).catch((error) => console.log(error))


//3個foreach太多了啦 這個方法不好 要換。
function ChangeFacilityData(data) {
    facilitiesArea.categories.forEach(category => {
        category.facilities.forEach(item => {
            Object.keys(data).forEach(key => {
                // console.log(key)
                // console.log(item.facility)
                // console.log(data[key].toString());
                if (item.facility == key.toString()) {
                    item.haveFacility = data[key];
                    // console.log(item.facility + item.haveFacility);
                }
            })
        })
    });
}

// facilitiesVue = new Vue({
//     el: '#facility-collapse',
//     data: {
//         categories: [
//             {
//                 categoryName: "可使用語言",
//                 categoryIcon: "fas fa-globe",
//                 facilities: [
//                     {
//                         facility: "Chinese",
//                         facility_Ch: "中文",
//                         icon: icons.language,
//                         haveFacility: true
//                     },
//                     {
//                         facility: "English",
//                         facility_Ch: "英語",
//                         icon: icons.language,
//                         haveFacility: true
//                     },
//                     {
//                         facility: "Japanese",
//                         facility_Ch: "日語",
//                         icon: icons.language,
//                         haveFacility: false
//                     },
//                 ]
//             },
//             {
//                 categoryName: "兒童專屬",
//                 categoryIcon: "fas fa-child",
//                 facilities: [
//                     {
//                         facility: "ChildrenPlayroom",
//                         facility_Ch: "兒童娛樂室",
//                         icon: "fas fa-child",
//                         haveFacility: false
//                     },
//                     {
//                         facility: "ChildcareService",
//                         facility_Ch: "保姆托兒服務",
//                         icon: "fas fa-baby-carriage",
//                         haveFacility: false
//                     },
//                     {
//                         facility: "FamilyRoom",
//                         facility_Ch: "家庭房",
//                         icon: "fas fa-users",
//                         haveFacility: true
//                     },
//                 ]
//             },
//             {
//                 categoryName: "友善設施",
//                 categoryIcon: "fas fa-wheelchair",
//                 facilities: [
//                     {
//                         facility: "FacilitiesFordisabledGuests",
//                         facility_Ch: "無障礙友善設施",
//                         icon: icons.check_circle,
//                         haveFacility: facilityData["FacilitiesFordisabledGuests"]
//                     },
//                     {
//                         facility: "Elavator",
//                         facility_Ch: "電梯",
//                         icon: icons.check_circle,
//                         haveFacility: true
//                     },
//                 ]
//             },
//             {
//                 categoryName: "網路服務",
//                 categoryIcon: icons.wifi,
//                 facilities: [
//                     {
//                         facility: "Internet",
//                         facility_Ch: "公共區域Wi-Fi",
//                         icon: icons.wifi,
//                         haveFacility: facilityData["Internet"]
//                     },
//                     {
//                         facility: "RoomFreeWi-Fi",
//                         facility_Ch: "房內免費Wi-Fi",
//                         icon: icons.wifi,
//                         haveFacility: true
//                     },
//                 ]
//             },
//             {
//                 categoryName: "休閒娛樂設施",
//                 categoryIcon: icons.swimmingPool,
//                 facilities: [
//                     {
//                         facility: "OutdoorSwimmingPool",
//                         facility_Ch: "泳池(室外)",
//                         icon: icons.swimmingPool,
//                         haveFacility: false
//                     },
//                     {
//                         facility: "SwimmingPool",
//                         facility_Ch: "室內泳池",
//                         icon: icons.swimmingPool,
//                         haveFacility: facilityData["SwimmingPool"]
//                     },
//                     {
//                         facility: "Gym",
//                         facility_Ch: "健身房",
//                         icon: icons.gym,
//                         haveFacility: facilityData["Gym"]
//                     },
//                     {
//                         facility: "Garden",
//                         facility_Ch: "花園",
//                         icon: "fab fa-pagelines",
//                         haveFacility: true
//                     },
//                     {
//                         facility: "TravelPlan",
//                         facility_Ch: "旅遊行程",
//                         icon: "fas fa-route",
//                         haveFacility: true
//                     },
//                 ]
//             },
//             {
//                 categoryName: "衛生安全",
//                 categoryIcon: "fas fa-pump-medical",
//                 facilities: [
//                     {
//                         facility: "MeasureBodyTemp",
//                         facility_Ch: "住客及員工體溫測量",
//                         icon: icons.check_circle,
//                         haveFacility: true
//                     },
//                     {
//                         facility: "FirstAidKit",
//                         facility_Ch: "急救箱",
//                         icon: icons.check_circle,
//                         haveFacility: true
//                     },
//                     {
//                         facility: "DrySoap",
//                         facility_Ch: "乾洗手",
//                         icon: icons.check_circle,
//                         haveFacility: true
//                     },
//                     {
//                         facility: "DailySterilize",
//                         facility_Ch: "每日消毒",
//                         icon: icons.check_circle,
//                         haveFacility: true
//                     },
//                     {
//                         facility: "SafetyTrainEmployee",
//                         facility_Ch: "員工皆受安全訓練",
//                         icon: icons.check_circle,
//                         haveFacility: true
//                     },
//                     {
//                         facility: "Thermometer",
//                         facility_Ch: "體溫計",
//                         icon: icons.check_circle,
//                         haveFacility: true
//                     },
//                     {
//                         facility: "AllRoomSterilizeDaily",
//                         facility_Ch: "所有客房每日消毒",
//                         icon: icons.check_circle,
//                         haveFacility: true
//                     },
//                     {
//                         facility: "SterilizeEquiment",
//                         facility_Ch: "消毒設備",
//                         icon: icons.check_circle,
//                         haveFacility: true
//                     },
//                 ]
//             },
//             {
//                 categoryName: "餐飲服務",
//                 categoryIcon: icons.utensils,
//                 facilities: [
//                     {
//                         facility: "24_SentFood",
//                         facility_Ch: "24小時送餐服務",
//                         icon: "fas fa-concierge-bell",
//                         haveFacility: true
//                     },
//                     {
//                         facility: "Cafe",
//                         facility_Ch: "咖啡店",
//                         icon: "fas fa-mug-hot",
//                         haveFacility: true
//                     },
//                     {
//                         facility: "Restaurants",
//                         facility_Ch: "餐廳",
//                         icon: icons.utensils,
//                         haveFacility: facilityData["Restaurants"]
//                     },
//                     {
//                         facility: "DeliverDailyUseProduct",
//                         facility_Ch: "生活用品配送",
//                         icon: "fas fa-shipping-fast",
//                         haveFacility: true
//                     },
//                     {
//                         facility: "NightClub",
//                         facility_Ch: "酒吧",
//                         icon: "fas fa-glass-martini-alt",
//                         haveFacility: facilityData["NightClub"]
//                     },
//                     {
//                         facility: "FreeBreakfast",
//                         facility_Ch: "早餐(免費)",
//                         icon: icons.utensils,
//                         haveFacility: true
//                     },
//                     {
//                         facility: "MuslimRestaurant",
//                         facility_Ch: "清真餐廳",
//                         icon: "fas fa-mosque",
//                         haveFacility: true
//                     },
//                 ]
//             },
//             {
//                 categoryName: "服務與便利設施",
//                 categoryIcon: "fas fa-concierge-bell",
//                 facilities: [
//                     {
//                         facility: "24_Security",
//                         facility_Ch: "24小時保全",
//                         icon: "fas fa-shield-alt",
//                         haveFacility: true
//                     },
//                     {
//                         facility: "ForeignCurrencyExchange",
//                         facility_Ch: "外幣兌換",
//                         icon: "fas fa-sync",
//                         haveFacility: true
//                     },
//                     {
//                         facility: "SafeBox",
//                         facility_Ch: "保險箱",
//                         icon: "fas fa-lock",
//                         haveFacility: true
//                     },
//                     {
//                         facility: "FrontDesk",
//                         facility_Ch: "櫃台服務",
//                         icon: "fas fa-user-tie",
//                         haveFacility: facilityData["FrontDesk"]
//                     },
//                     {
//                         facility: "SmokingArea",
//                         facility_Ch: "吸菸區",
//                         icon: icons.smoking,
//                         haveFacility: facilityData["SmokingArea"]
//                     },
//                     {
//                         facility: "DeliverLaudry",
//                         facility_Ch: "送洗服務",
//                         icon: "fas fa-tshirt",
//                         haveFacility: true
//                     },
//                     {
//                         facility: "KeepLuggage",
//                         facility_Ch: "可寄放行李",
//                         icon: "fas fa-luggage-cart",
//                         haveFacility: true
//                     },
//                     {
//                         facility: "DailyRoomCleaningService",
//                         facility_Ch: "每日客房清潔服務",
//                         icon: "fas fa-broom",
//                         haveFacility: true
//                     },
//                     {
//                         facility: "ConciergeService",
//                         facility_Ch: "禮賓服務",
//                         icon: "fas fa-user-tie",
//                         haveFacility: true
//                     },
//                     {
//                         facility: "PetsAllowed",
//                         facility_Ch: "可帶寵物",
//                         icon: "fas fa-paw",
//                         haveFacility: facilityData["PetsAllowed"]
//                     },
//                     {
//                         facility: "Room",
//                         facility_Ch: "門房",
//                         icon: "fas fa-door-open",
//                         haveFacility: true
//                     },
//                 ]
//             },
//             {
//                 categoryName: "交通服務/設施",
//                 categoryIcon: icons.car,
//                 facilities: [
//                     {
//                         facility: "CallTaxiService",
//                         facility_Ch: "代客叫車服務",
//                         icon: "fas fa-taxi",
//                         haveFacility: true
//                     },
//                     {
//                         facility: "CarRentalService",
//                         facility_Ch: "租車服務",
//                         icon: icons.car,
//                         haveFacility: true
//                     },
//                     {
//                         facility: "AirportTransfer",
//                         facility_Ch: "機場接送",
//                         icon: "fas fa-plane",
//                         haveFacility: facilityData["AirportTransfer"]
//                     },
//                     {
//                         facility: "ParkingService",
//                         facility_Ch: "代客停車",
//                         icon: icons.car,
//                         haveFacility: true
//                     },
//                     {
//                         facility: "CarPark",
//                         facility_Ch: "停車場(免費)",
//                         icon: "fas fa-parking",
//                         haveFacility: facilityData["CarPark"]
//                     },
//                 ]
//             },
//         ]
//     },
//     components: {
//         'facility-icon': Facilities
//     }
// })



