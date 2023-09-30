export default {
    props: {
        cat: ['cat'],
    },
    template:
        `<div class="serv-group d-flex my-4">
        <i :class="cat.categoryIcon" class=" fz-big-icon color-dark-gray border-e-light-gray me-4"></i>
        <div class="serv-row">
            <h4 class="fz-16 fw-bold color-dark-gray">{{cat.categoryName}}</h4>
            <div class="row color-dark-gray">
                <p v-for="f in cat.facilities" v-if="f.haveFacility" class="col-4"><i :class="f.icon" class=" fz-14 me-2"></i>{{f.facility_Ch}}</p>
            </div>
        </div>
    </div>`
}