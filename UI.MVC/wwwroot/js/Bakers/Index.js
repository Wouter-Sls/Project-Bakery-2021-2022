window.addEventListener("load", init)

const loadBakersButton=
    document.getElementById("loadBakers");
loadBakersButton.addEventListener(
    "click", init);

function init(){
    fetch("/api/bakers", {
        method: "GET", 
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => showBakers(data))
        .catch(reason => alert("Call failed: " + reason));
}

function showBakers(bakers){
    const tableBody = document.getElementById("bakerTableBody");
    tableBody.innerHTML = "";
    bakers.forEach(baker => addBaker(baker));
}

function addBaker(baker) {
    const tableBody = document.getElementById("bakerTableBody");
    const birth = baker.birthDate;
    tableBody.innerHTML += `<tr>
<td>${baker.name}</td>
<td>${birth.substr(0,10)}</td>
<td>${baker.income}</td>
<td>  <a href="/Baker/Detail?id=${baker.id}">Details</a></td>
</tr>`
}

