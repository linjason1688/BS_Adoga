$(document).ready(function () {
  var current_fs, next_fs; //fieldsets
  var opacity;

  $(".next").click(function () {
    current_fs = $(this).parent().parent();
    next_fs = $(this).parent().parent().next();

    //show the next fieldset
    next_fs.show();
    //hide the current fieldset with style
    current_fs.animate(
      {
        opacity: 0,
      },
      {
        step: function (now) {
          // for making fielset appear animation
          opacity = 1 - now;

          current_fs.css({
            display: "none",
            position: "relative",
          });
          next_fs.css({
            opacity: opacity,
          });
        },
        duration: 600,
      }
    );
  });
});
