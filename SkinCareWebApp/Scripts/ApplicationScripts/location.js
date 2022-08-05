window.onload = function () {
    setLocation();
};

function setLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            document.cookie = 'lat=' + position.coords.latitude+";";
            document.cookie = 'lon=' + position.coords.longitude + ";";
        });
    } else {
        
    }
}