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
