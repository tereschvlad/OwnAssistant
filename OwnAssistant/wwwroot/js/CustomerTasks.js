let markers = [];

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
    }
};