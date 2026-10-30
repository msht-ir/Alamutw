window.leafletMap_by_msht_mapup = {
    mapInstance: null,

    init: function (elementId, lat, lng, zoom) {
        var container = document.getElementById(elementId);
        if (!container) {
            console.error("Map container not found: " + elementId);
            return;
        }

        // If map already exists, destroy it to prevent "Map container is already initialized" error
        if (this.mapInstance) {
            this.mapInstance.remove();
            this.mapInstance = null;
        }

        try {
            // Initialize map
            this.mapInstance = L.map(elementId).setView([lat, lng], zoom);
            // Add MapUp tiles
            L.tileLayer('https://tiles.mapup.ir/styles/basic-preview/{z}/{x}/{y}.png', {
                maxZoom: 20,
                minZoom: 5
            }).addTo(this.mapInstance);
            // Add marker
            L.marker([lat, lng]).addTo(this.mapInstance)
                .bindPopup('My Point')
                .openPopup();
            console.log("Map successfully initialized.");
        } catch (error) {
            console.error("Leaflet error: ", error);
        }
    }
};