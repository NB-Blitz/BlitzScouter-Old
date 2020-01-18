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

function sort(col) {
	var table = document.getElementById("sortable");
	var rows = table.rows;
	var hasSwitched = true;
	while (hasSwitched) {
		hasSwitched = false;
		loop:
		for (var i = 1; i < rows.length - 1; i++)
		{
			var valA = rows[i].getElementsByTagName("TD")[col];
			if (valA == undefined)
				valA = "-1";
			else
				valA = valA.innerText

			var indexB = i + 1;
			while (rows[indexB].className.includes("matchdata")) {
				indexB++;
				if (indexB >= rows.length) {
					break loop;
				}
			}

			var valB = rows[indexB].getElementsByTagName("TD")[col];
			if (valB == undefined)
				valB = "-1";
			else
				valB = valB.innerText;

			if (parseInt(valA) < parseInt(valB)) {
				rows[i].parentNode.insertBefore(rows[indexB], rows[i]);
				hasSwitched = true;
			}
			i = indexB - 1;
		}
	}
}

function toggleColor(id) {
	var team = document.getElementById(id);
	if (team.style.backgroundColor == "rgb(237, 28, 36)")
		team.style.backgroundColor = "#005CB8";
	else
		team.style.backgroundColor = "#ED1C24";
}