window.addEventListener("load", init)
window.addEventListener("load", addNewPastry)

const types= ["Bread","Cake", "Cookie"];
const urlArray = window.location.href.split('/');
const bakeryId = urlArray[urlArray.length - 1];

function init(){
    fetch(`/api/bakeries/${bakeryId}`, {
        method: "GET", /* is de default */
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => showPastries(data))
        .catch(reason => alert("Call failed: " + reason));
}

function showPastries(pastries){
    const tableBody = document.getElementById("pastriesTableBody");
    tableBody.innerHTML = "";
    pastries.forEach(pastry => addPastry(pastry));
}

function addPastry(pastry) {
    
    const tableBody = document.getElementById("pastriesTableBody");
    tableBody.innerHTML += `<tr>
<td>${pastry.name}</td>
<td>€${pastry.price}</td>
<td>${pastry.quantity}</td>
<td>${types[pastry.type]}</td>
</tr>`
}


function addNewPastry() {
    const form = document.getElementById("addPastrie");
    form.innerHTML += `
    <div class="form-group row">
        <label class="col-sm-3 font-weight-bold">Pastrie</label> 
        <div class="col-sm-9 p-0">
            <select id="pastry" class="form-control"></select>
        </div>
    </div>
    <!-- 
    <div class="form-group row">
        <label class="col-sm-3 font-weight-bold">Total Price</label><input id="totalPrice" class="form-control col-sm-9"/>
    </div>
    -->
     <div class="form-group row">
        <label class="col-sm-3 font-weight-bold">Total Stock</label><input id="totalStock" class="form-control col-sm-9"/>
    </div>
    <div>
        <button type="button" onclick="completeAdd()" class="btn btn-primary" id="buttonAdd">Add pastry to bakery</button>
    </div>`

    fetch(`/Api/Pastries`, {
        method: "GET",
        headers: {"Accept": "application/json"}
    }).then(response => response.json())
        .then (p => loop(p))
        .catch(() => alert('Updating failed'));
}

function loop(p){
    p.forEach(pa=>addOption(pa))
}
function addOption(i) {
    const select = document.getElementById("pastry")
   
        select.innerHTML +=`<option value="${i.id}">${i.name}</option>`;
    
}

async function completeAdd(){
    
    const pastry = document.getElementById("pastry").value;
    //const price = parseFloat(document.getElementById("totalPrice").value);
    const totalStock = parseFloat(document.getElementById("totalStock").value);
    
    fetch(`/Api/Bakeries`, {
        method: "POST",
        body: JSON.stringify({
            "BakeryId": bakeryId,"PastryId":pastry, "TotalStock":totalStock//, "TotalPrice": price
            
            }
        ),
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    }).catch(() => alert('Updating failed'));
    await sleep(500)
    init();
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

