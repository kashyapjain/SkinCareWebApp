

//Setting up some elements
let uvCardTimeEle = document.getElementById("uvCardTimeEle");
uvCardTimeEle.innerHTML = `<b>${new Date().toLocaleTimeString()}</b>`


let weatherCardDateEle = document.getElementById("weatherCardDateEle");

let url = window.location;

function refreshTime() {
    weatherCardDateEle.innerHTML = `<b>${new Date().toLocaleDateString("en-US", { year: 'numeric', month: 'short', day: 'numeric' })}</b>`
}
setInterval(refreshTime, 1000);

//

let scrollObj = { value: 1 };

gsap.registerPlugin(ScrollTrigger);

const uvCard = document.getElementById("uv-card");
const weatherCard = document.getElementById("weather-card");
const precautions = document.getElementById("precautions2");


let t1 = gsap.timeline({ default: { duration: 1 } });

t1.to(uvCard,
    {
        scrollTrigger: {
            trigger: uvCard,
            scrub: 1,
            start:"end end"
        },
        x: -100,
        opacity: 0 ,
    })
    .to(weatherCard, {
        scrollTrigger: {
            trigger: weatherCard,
            start: "end end ",
            scrub: 1,
        },
        x: 100,
        opacity: 0,
    }, "-=1");


$("#citySearchBar").on("select2:select", (e) => {
    var data = e.params.data;
    const [Lat, Lon] = data.id.split(',');
    const cityName = data.text;
    //console.log(Lat, Lon, cityName);

    document.cookie = 'lat=' + Lat + ";";
    document.cookie = 'lon=' + Lon + ";";
    document.cookie = 'city=' + cityName + ";";

    console.log(document.cookie);

    document.location.reload();

});

$("#citySearchBar").select2({
    ajax: {
        url: "https://wft-geo-db.p.rapidapi.com/v1/geo/cities",
        headers: {
            "X-RapidAPI-Key": "4936931759mshe6fcf6f98ce1fb8p19187djsn385119d168d6",
            "X-RapidAPI-Host": "wft-geo-db.p.rapidapi.com"
        },
        dataType: 'json',
        delay: 1000,
        data: function (params) {
            return {
                namePrefix: params.term
            };
        },
        processResults: function (data, params) {
            //console.log(data.data);
            return {
                results: dataToBeExported(data.data),
            };
        },
        cache: true
    },
    placeholder: 'Search.....',

});



function dataToBeExported(array) {
    var data = array.map((item) => {

        return {
            id: `${item.latitude},${item.longitude}`,
            text: `${item.city}, ${item.region}, ${item.countryCode}`
        }
    });

    return data;
}


Enabler()

function Enabler() {

    let section = url.hash;
    console.log(section)
    if (section == null) {

    }
    else if (section === "#UvAndWeatherCardSection") {
        
        document.getElementById("UvAndWeatherCardSection").style = 'transition:all 0.6s ease-in-out;border: 1px solid red;margin: 20px';

        setTimeout(() => {
            document.getElementById("UvAndWeatherCardSection").style = ' border: none;margin: 0px';
        },1000)
    }
    else if (section == "#PrecautionsSection") {
        document.getElementById("PrecautionsSection").style = 'transition:all 0.6s ease-in-out; border: 1px solid red;margin: 20px';
        
        setTimeout(() => {
            document.getElementById("PrecautionsSection").style = 'border: none;margin: 0px';
        }, 1000)
    }
    else if (section == "#ChartsSection") {
        document.getElementById("ChartsSection").style = 'transition:all 0.6s ease-in-out; border: 1px solid red;margin: 20px';

        setTimeout(() => {
            document.getElementById("ChartsSection").style = 'border: none;margin: 0px';
        }, 1000)
    }

};