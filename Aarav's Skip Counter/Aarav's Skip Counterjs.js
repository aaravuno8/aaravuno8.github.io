var currnumber = 0;
var skipnumber = 0;

function Onback() {
    document.getElementById("p").style.display = "block";
    document.getElementById("start").style.display = "none";
}
function Onload() {
    document.getElementById("start").style.display = "block";
    document.getElementById("p").style.display = "none";
    document.getElementById("p1").innerHTML = "0"
}
function onnextclick() {
    currnumber = currnumber + skipnumber;
    document.getElementById("p1").innerHTML = currnumber;
}
function onbackclick() {
    currnumber = currnumber - skipnumber;
    document.getElementById("p1").innerHTML = currnumber;
}
function Submit() {
    skipnumber = document.getElementById("input1").value;

    skipnumber = parseInt(skipnumber);
    document.getElementById("start").style.display = "none";
    document.getElementById("p").style.display = "block";
}