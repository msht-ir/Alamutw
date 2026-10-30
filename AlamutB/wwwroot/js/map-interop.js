window.leafletMap = {
    mapInstance: null,
    markerGroup: null, // We will keep markers in a group so we can clear them later
    polygonLayer: null,

    // 1. Initialize the Map and Tiles ONLY
    init: function (elementId, lat, lng, zoom) {
        var container = document.getElementById(elementId);
        if (!container) return;

        if (this.mapInstance) {
            this.mapInstance.remove();
        }

        this.mapInstance = L.map(elementId).setView([lat, lng], zoom);

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; OpenStreetMap'
        }).addTo(this.mapInstance);

        this.markerGroup = L.featureGroup().addTo(this.mapInstance);
    },

    // 2. Add Markers (can be called multiple times)
    addMarkers: function (points) {
        if (!this.mapInstance || !points) return;

        var self = this;
        points.forEach(function (point) {
            L.marker([point.lat, point.lng])
                .bindPopup(point.name)
                .addTo(self.markerGroup); // Add to our group
        });
    },

    // 3. Draw Polygon around the points
    drawPolygon: function () {
        if (!this.mapInstance) return;

        // Get all markers currently on the map
        var allMarkers = this.markerGroup.getLayers();
        if (allMarkers.length < 3) return; // Need at least 3 points

        // Extract lat/lng from the markers
        var points = allMarkers.map(function (marker) {
            return { lat: marker.getLatLng().lat, lng: marker.getLatLng().lng };
        });

        var hullPoints = this.getConvexHull(points);
        var polygonCoords = hullPoints.map(function (p) { return [p.lat, p.lng]; });

        // Remove old polygon if it exists
        if (this.polygonLayer) {
            this.mapInstance.removeLayer(this.polygonLayer);
        }

        this.polygonLayer = L.polygon(polygonCoords, {
            color: 'blue',
            fillColor: '#3388ff',
            fillOpacity: 0.2,
            weight: 2
        }).addTo(this.mapInstance).bindPopup("Area of Interest");
    },

    // 4. Fit map to show all markers
    zoomToFit: function () {
        if (!this.mapInstance || !this.markerGroup) return;

        var layers = this.markerGroup.getLayers();

        if (layers.length === 1) {
            // If there is exactly 1 point, just center on it at zoom level 10
            var singlePoint = layers[0].getLatLng();
            this.mapInstance.setView(singlePoint, 10);
        }
        else if (layers.length > 1) {
            // If there are 2 or more points, fit bounds normally
            this.mapInstance.fitBounds(this.markerGroup.getBounds().pad(0.2));
        }
    },

    // 5. Add Click Event to Map
    addClickEvent: function () {
        if (!this.mapInstance) return;

        this.mapInstance.on('click', function (e) {
            var lat = e.latlng.lat;
            var lng = e.latlng.lng;

            // Change 'AlamutB' to your actual project Assembly Name if different
            DotNet.invokeMethodAsync('AlamutB', 'HandleMapClick', lat, lng);

            L.popup()
                .setLatLng(e.latlng)
                .setContent("Clicked: " + lat.toFixed(4) + ", " + lng.toFixed(4))
                .openOn(this.mapInstance);
        });
    },

    // Helper: Convex Hull Algorithm
    getConvexHull: function (points) {
        var sorted = points.slice().sort(function (a, b) {
            return a.lng === b.lng ? a.lat - b.lat : a.lng - b.lng;
        });
        var cross = function (o, a, b) {
            return (a.lng - o.lng) * (b.lat - o.lat) - (a.lat - o.lat) * (b.lng - o.lng);
        };
        var lower = [];
        for (var i = 0; i < sorted.length; i++) {
            while (lower.length >= 2 && cross(lower[lower.length - 2], lower[lower.length - 1], sorted[i]) <= 0) lower.pop();
            lower.push(sorted[i]);
        }
        var upper = [];
        for (var i = sorted.length - 1; i >= 0; i--) {
            while (upper.length >= 2 && cross(upper[upper.length - 2], upper[upper.length - 1], sorted[i]) <= 0) upper.pop();
            upper.push(sorted[i]);
        }
        upper.pop(); lower.pop();
        return lower.concat(upper);
    }
};