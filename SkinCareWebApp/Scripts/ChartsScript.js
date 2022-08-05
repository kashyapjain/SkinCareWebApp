 // helper fuctions

function WeeksListGenerator() {
    let weekdays = [];
    let count = 1;
    const currentDate = new Date().getTime()
    while (count < 14) {

        weekdays.push(new Date(currentDate - count * 86400000).toLocaleDateString('en-US', { day: 'numeric', month: 'short' }));
        count += 1;
    }

    //console.log(weekdays);
    return weekdays;
}

function WeekListGeneratorForBar() {
    let weekdays = [];
    let data = ["2016-12-01",
        "2016-12-02",
        "2016-12-03",
        "2016-12-04",
        "2016-12-05",
        "2016-12-06",
        "2016-12-07",
        "2016-12-08",
        "2016-12-09",
        "2016-12-10",
        "2016-12-11",
        "2016-12-12",
        "2016-12-13",
        "2016-12-14",
        "2016-12-15",
        "2016-12-16",
        "2016-12-17",
        "2016-12-18",
        "2016-12-19",
        "2016-12-20",
        "2016-12-21",
        "2016-12-22",
        "2016-12-23",
        "2016-12-24",
        "2016-12-25",
        "2016-12-26",
        "2016-12-27",
        "2016-12-28",
        "2016-12-29",
        "2016-12-30",
        "2016-12-31"]
    weekdays =  data.map((item) => {
        let date = new Date(item).toLocaleDateString('en-US', { day: 'numeric', month: 'short' });
        return date;
    })

    //console.log(weekdays);
    return weekdays;
}
 // ======================================



// graphs
//line
var ctxL = document.getElementById("lineChart").getContext('2d');
const labelsData = WeeksListGenerator();
var charts = new Chart(ctxL, {
    type: 'line',
    data: {
        labels: labelsData.reverse(),
        datasets: [{
            label: "UV index data",
            data: [6, 9, 8, 5, 6, 10, 9, 5, 3, 2, 9, 5, 6, 3, 4, 1, 10, 7, 10, 5, 6, 3, 5, 4, 3, 7, 6, 5, 10, 4],
            backgroundColor: [
                'rgba(105, 0, 132, .2)',
            ],
            borderColor: [
                'rgba(200, 99, 132, .7)',
            ],
            borderWidth: 2
        }
        ]
    },
    options: {
        responsive: true
    }
});

//bar 
var ctxL_bar = document.getElementById("bargraph").getContext('2d');

charts = new Chart(ctxL_bar, {
    type: 'bar',
    data: {
        labels: WeekListGeneratorForBar(),
        datasets: [{
            label: "Temperatur trends ( Previous 30 days )",
            data: [8.1,
                8,
                9.5,
                8.2,
                8.2,
                11.2,
                13.2,
                13.4,
                14.9,
                13.6,
                10.3,
                11.4,
                13.3,
                13.9,
                11.5,
                10.5,
                7.9,
                7.5,
                7,
                9.8,
                12.1,
                10.7,
                11.5,
                11.5,
                13.5,
                8.6,
                6.8,
                8.1,
                9.5,
                4.8,
                8.9],
            backgroundColor: [
                'rgba(105, 0, 132, .2)',
            ],
            borderColor: [
                'rgba(200, 99, 132, .7)',
            ],
            borderWidth: 2
        }
        ]
    },
    options: {
        responsive: true
    }
});






