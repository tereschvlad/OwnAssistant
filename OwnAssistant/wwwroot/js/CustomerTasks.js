let markers = [];
let map;

let tasksManager = {
    initCrtTask: function() {
        map = L.map('map', { fullscreenControl: true, }).setView([50.4579725, 30.5026167], 10);

        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }).addTo(map);

        map.off('click').on('click', (e) => {
            markers.push(e.latlng);
            let marker = L.marker(e.latlng).addTo(map);
            marker.on('click', (e) => {
                map.removeLayer(e.target);
            });
        });

        $('#customerTasksForm').on('submit', function (e) {
            e.preventDefault();
            if (!$(this).valid()) {
                return;
            }

            let formData = new FormData(this);
            let data = {
                Checkpoints: [],
                Arr:[]
            };

            
            formData.forEach((val, key) => {
                data[key] = val;
            });

            map.eachLayer((layer) => {
                if (layer instanceof L.Marker) {
                    data.Checkpoints.push({
                        Lat: layer.getLatLng().lat,
                        Long: layer.getLatLng().lng
                    });
                }
            });

            fetch('/CustomerTask/CreateTask', {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data)
            }).then(responce => {
                if (responce.status === 200) {
                    location.reload();
                }
            });            
        });

        $('#RepeationType').on('change', function (e) {
            var repeatType = $(e.target).val();

            if (repeatType != CustomerRepeationType.None) {
                $('#repeatPeriodBlock').removeClass('d-none');
            }
            else {
                $('#repeatPeriodBlock').addClass('d-none');
            }
        });
    },
    initViewTask: function (json) {
        map = L.map('map', { fullscreenControl: true, }).setView([50.4579725, 30.5026167], 10);
        debugger;
        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }).addTo(map);

        let checkpoints = JSON.parse(json);

        checkpoints.forEach(val => {
            var latlng = L.latLng(val.lat, val.long);
            L.marker(latlng).addTo(map)
        });
    }
};

let CustomerRepeationType = {
    None: 0,
    Weekdays: 1,
    Weekends: 2,
    WithoutWeekends: 3,
    EveryDays: 4,
    EveryWeeks: 5,
    EveryMounths: 6
}