function changeCounters(id, amt, min, max) {
	var old = parseInt(document.getElementById("cnt" + id + "val").value);
	if (old + amt >= min && old + amt <= max) {
		document.getElementById("cnt" + id + "val").value = old + amt;
		document.getElementById("cnt" + id + "disp").innerText = old + amt;
	}
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
			var valA = rows[i].getElementsByTagName("TD")[col].innerText;
			if (valA == "-")
				valA = "-1";

			var indexB = i + 1;
			while (rows[indexB].className.includes("matchdata")) {
				indexB++;
				if (indexB >= rows.length) {
					break loop;
				}
			}

			var valB = rows[indexB].getElementsByTagName("TD")[col].innerText;
			if (valB == "-")
				valB = "-1";

			if (parseInt(valA) < parseInt(valB)) {
				rows[i].parentNode.insertBefore(rows[indexB], rows[i]);
				hasSwitched = true;
			}
			i = indexB - 1;
		}
	}
}

function increment() {
	if (Cookies.get("scoutCount") == undefined) {
		Cookies.set("scoutCount", "1");
	}
	else {
		var val = parseInt(Cookies.get("scoutCount")) + 1;
		Cookies.set("scoutCount", val.toString());
	}
}

function getStats() {
	if (Cookies.get("scoutCount") == undefined) {
		Cookies.set("scoutCount", "0");
	}
	document.getElementById("stat1").innerHTML = "Scouted <b>" + Cookies.get("scoutCount") + "</b> times.";
}