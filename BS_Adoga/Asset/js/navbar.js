const progress = document.getElementById("");
// const next = document.querySelectorAll(".next");
const circles = document.querySelectorAll(".step");

let currentActive = 1;

$(document).ready(function () {
  $(".next").click(function () {
    currentActive++;

    if (currentActive > circles.length) {
      currentActive = circles.length;
    }
    upData();
  });
});

function upData() {
  circles.forEach((circle, idx) => {
    if (idx < currentActive) {
      circle.classList.add("step-active");
    } else {
      circle.classList.remove("step-active");
    }
  });

  const actives = document.querySelectorAll(".step-active");

  console.log(actives.length, circles.length);
}
