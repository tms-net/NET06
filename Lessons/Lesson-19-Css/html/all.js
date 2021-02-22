$(".btn").click(function() {
    var myBtn = this;
    var main = $(".main");
    if (myBtn.id == "1") {
        main.setStyle("style1");
    }
})