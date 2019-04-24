function Onload() {
    Onchange1()
    Onchange2()
    Onchange3()
    document.getElementById('link').style.display = "none"
}
function Onchange1() {
    var number = document.getElementById('Input1').value;
    document.getElementById('P1').innerHTML = number;
}
function Onchange2() {
    var number = document.getElementById('Input2').value;
    document.getElementById('P2').innerHTML = number;
}
function Onchange3() {
    var number = document.getElementById('Input3').value;
    document.getElementById('P3').innerHTML = number;
}
function Check() {
    var check1 = document.getElementById('Input1').value;
    var check2 = document.getElementById('Input2').value;
    var check3 = document.getElementById('Input3').value;
    if (check1 == 0) {
        if (check2 == 50) {
            if (check3 == 100) {
                alert("Correct Passcode");
                    document.getElementById("link").style.display = "block";
            }
            else {
                alert("Wrong Passcode");
            }
        }
        else {
            alert("Wrong Passcode");
        }
    }
    else {
        alert("Wrong Passcode");
    }
}