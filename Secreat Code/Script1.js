function Know() {
    window.open('Know.html');
}
function Learn() {
    window.open('learn.html');
}
function l2() {
    if (document.getElementById('Code1').value == "Great job!") {
        document.getElementById("code1").style.display = "none";
        document.getElementById("code2").style.display = "block";
    }
    else {
        alert('Try agian')
    }
}