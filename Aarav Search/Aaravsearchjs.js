function Gettags(id1,id2,id3,id4,id5,id6,id7,id8,id9,id10) {
    var query = document.getElementById("Search").value;
    var allatags = document.getElementsByTagName("a");
    var found = false;

    for (var i = 0; i < allatags.length; i++) {

        var anchorText = allatags[i].innerHTML;
        var index = anchorText.indexOf(query);

        if (index > 0) {
            found = true;
            allatags[i].style.display = "block";
        }
        else
        {
            allatags[i].style.display = "none";
        }
    }

    if (found == false) {
        document.getElementsbyid(id1).style.display = "block"
        document.getElementsbyid(id2).style.display = "block"
        document.getElementsbyid(id3).style.display = "block"
        document.getElementsbyid(id4).style.display = "block"
        document.getElementsbyid(id5).style.display = "block"
        document.getElementsbyid(id6).style.display = "block"
        document.getElementsbyid(id7).style.display = "block"
        document.getElementsbyid(id8).style.display = "block"
        document.getElementsbyid(id9).style.display = "block"
        document.getElementsbyid(id10).style.display = "block"
    }
}