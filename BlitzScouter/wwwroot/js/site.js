// Javascript - Used to manage incrementation and decrementation of the counters used whilst scouting.
function blue() {
	document.getElementById("colorRed").style.height = "100px";
	document.getElementById("colorBlue").style.height = "110px";
	document.getElementById("color").value = "Blue";
}

function red() {
	document.getElementById("colorRed").style.height = "110px";
	document.getElementById("colorBlue").style.height = "100px";
	document.getElementById("color").value = "Red";
}

function change(id, amt, min, max) {
	if (parseInt(document.getElementById(id).value) + amt >= min && parseInt(document.getElementById(id).value) + amt <= max)
		document.getElementById(id).value = (parseInt(document.getElementById(id).value) + amt).toString();
	document.getElementById(id + "disp").innerText = document.getElementById(id).value;
}