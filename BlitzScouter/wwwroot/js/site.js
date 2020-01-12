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

function changeCounters(id, amt, min, max) {
	var old = parseInt(document.getElementById("cnt" + id + "val").value);
	if (old + amt >= min && old + amt <= max) {
		document.getElementById("cnt" + id + "val").value = old + amt;
		document.getElementById("cnt" + id + "disp").innerText = old + amt;
	}
}

function changeCheckboxes(id) {
	checkboxes[id] = !checkboxes[id];
}

/*
function toStr() {
	// Counters to String
	var counter = "";
	for (var i = 0; i < counters.length; i++)
		if (i == counters.length - 1)
			counter += counters[i];
		else
			counter += counters[i] + ",";
	document.getElementById("counter").value = counter;

	// Checkboxes to String
	var checkbox = "";
	for (var i = 0; i < checkboxes.length; i++)
		if (i == checkboxes.length - 1)
			checkbox += checkboxes[i];
		else
			checkbox += checkboxes[i] + ",";
	document.getElementById("checkbox").value = checkbox;

	score.increment();
}
*/

function increment() {
	var score = localStorage.getItem("score");
	if (score == null)
		score = 0;
	localStorage.setItem("score", parseInt(score) + 1);
}

function toggleDisp(team) {
	var objs = document.getElementsByClassName(team + "matchdata");
	for (var i = 0; i < objs.length; i++) {
		if (objs[i].style.display == "none")
			objs[i].style.display = "revert";
		else
			objs[i].style.display = "none";
	}
}

function loadScore() {
	var score = localStorage.getItem("score");
	if (score == null)
		score = "0";
	document.getElementById("scoreNum").innerHTML = score;
}