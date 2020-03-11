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

	var sorted = 1;
	while (sorted < rows.length) {
		var bigIndex = sorted;
		var num = Number.MIN_SAFE_INTEGER;
		for (var i = sorted; i < rows.length; i++)
		{
			var valS = rows[i].getElementsByTagName("TD")[col].innerText;
			if (valS == "-") {
				valS = "-1";
			}
			var val = parseInt(valS);

			if (val > num) {
				bigIndex = i;
				num = val;
			}
		}

		var temp = rows[bigIndex].innerHTML;
		rows[bigIndex].innerHTML = rows[sorted].innerHTML;
		rows[sorted].innerHTML = temp;
		sorted++;
	}
}

function increment(v) {
	if (Cookies.get(v) == undefined) {
		Cookies.set(v, "1");
	}
	else {
		var val = parseInt(Cookies.get(v)) + 1;
		Cookies.set(v, val.toString());
	}
}

function getStats() {
	if (Cookies.get("scoutCount") == undefined) {
		Cookies.set("scoutCount", "0");
	}
	document.getElementById("stat1").innerHTML = "Scouted <b>" + Cookies.get("scoutCount") + "</b> times.";
}

function star() {
	var star = document.getElementById("starButton");
	var starVal = document.getElementById("star");
	if (starVal.value == "True") {
		star.innerHTML = "<svg class=\"bi bi-star\" width=\"1em\" height=\"1em\" viewBox=\"0 0 20 20\" fill=\"currentColor\" xmlns=\"http://www.w3.org/2000/svg\"><path fill-rule=\"evenodd\" d=\"M4.866 16.85c-.078.444.36.791.746.593l4.39-2.256 4.389 2.256c.386.198.824-.149.746-.592l-.83-4.73 3.523-3.356c.329-.314.158-.888-.283-.95l-4.898-.696-2.184-4.327a.513.513 0 00-.927 0L7.354 7.12l-4.898.696c-.441.062-.612.636-.282.95l3.522 3.356-.83 4.73zm4.905-2.767l-3.686 1.894.694-3.957a.565.565 0 00-.163-.505L3.71 8.745l4.052-.576a.525.525 0 00.393-.288l1.847-3.658 1.846 3.658c.08.157.226.264.393.288l4.053.575-2.907 2.77a.564.564 0 00-.163.506l.694 3.957-3.686-1.894a.503.503 0 00-.461 0z\" clip-rule=\"evenodd\"></path></svg>";
		starVal.value = "False";
	}
	else {
		star.innerHTML = "<svg class=\"bi bi-star\" width=\"1em\" height=\"1em\" viewBox=\"0 0 20 20\" fill=\"currentColor\" xmlns=\"http://www.w3.org/2000/svg\"><path d=\"M5.612 17.443c-.386.198-.824-.149-.746-.592l.83-4.73-3.522-3.356c-.33-.314-.16-.888.282-.95l4.898-.696 2.184-4.327c.197-.39.73-.39.927 0l2.184 4.327 4.898.696c.441.062.612.636.283.95l-3.523 3.356.83 4.73c.078.443-.36.79-.746.592L10 15.187l-4.389 2.256z\"></path></svg>";
		starVal.value = "True";
	}
}