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

function change(id, amt) {
	if (parseInt(document.getElementById(id).value) + amt >= 0)
		document.getElementById(id).value = (parseInt(document.getElementById(id).value) + amt).toString();
	document.getElementById(id + "disp").innerText = document.getElementById(id).value;
}

function changeStartHab(id, amt) {
	if (parseInt(document.getElementById(id).value) + amt >= 1 && parseInt(document.getElementById(id).value) + amt <= 2)
		document.getElementById(id).value = (parseInt(document.getElementById(id).value) + amt).toString();
	document.getElementById(id + "disp").innerText = document.getElementById(id).value;
}

function changeEndHab(id, amt) {
	if (parseInt(document.getElementById(id).value) + amt >= 0 && parseInt(document.getElementById(id).value) + amt <= 3)
		document.getElementById(id).value = (parseInt(document.getElementById(id).value) + amt).toString();
	document.getElementById(id + "disp").innerText = document.getElementById(id).value;
}