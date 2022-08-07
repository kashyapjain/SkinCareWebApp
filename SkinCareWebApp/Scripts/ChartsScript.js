
    // helper fuctions
    function WeeksListGenerator() {
        let weekdays = [];
        let count = 1;
        let currentDate = new Date().getTime();
        //currentDate = currentDate + (3 * 86400000);
    while (count <= 7) {

        weekdays.push(new Date(currentDate - count * 86400000).toLocaleDateString('en-US', { day: 'numeric', month: 'short' }));
    count += 1;
        }

    console.log(weekdays);
    return weekdays;
    }



// Graphs plotting

fetch("/home/DataForChartTrends")
    .then((resp) => {
        return resp.json();
    })
    .then((data) => {
        console.log(data);

        //Line Chart
        var ctxL = document.getElementById("lineChart").getContext('2d');
        const labelsData = WeeksListGenerator().reverse();

        var charts = new Chart(ctxL, {
            type: 'line',
            data: {
                labels: labelsData,
                datasets: [{
                    label: "UV index data",
                    data: data.map((item) => item.UvIndex),
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


        //Bar Chart
        var ctxL_bar = document.getElementById("bargraph").getContext('2d');
        charts = new Chart(ctxL_bar, {
            type: 'bar',
            data: {
                labels: labelsData,
                datasets: [{
                    label: "Temperatur trends ( Past 7 days )",
                    data: data.map((item) => item.Temp),
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

    })
    .catch((err) => {
        console.log(err);
    })
