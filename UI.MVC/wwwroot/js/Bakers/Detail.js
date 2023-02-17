window.addEventListener("load", init);
const urlParams = new URLSearchParams(window.location.search);

const bakerId = parseInt(urlParams.get("id"));

function init() {
    fetch(`/api/bakers/${bakerId}`, {
        method: "GET",
        headers: {
            "Accept": "application/json"
        }
    })
        .then(response => response.json())
        .then(baker => putData(baker))
        .catch(reason => alert("Call failed: " + reason));
}

function putData(baker) {
    document.getElementById("name").value = baker.name;
    document.getElementById("birthdate").value = baker.birthDate.substring(0,10);
    document.getElementById("income").value = baker.income;
}

function changeBaker() {
    let newBaker = {
        "id": bakerId, 
        "bakery": null, 
        "name": document.getElementById("name").value, 
        "birthDate": document.getElementById("birthdate").value, 
        "income": document.getElementById("income").value
    }

    fetch(`/api/bakers/${bakerId}`, {
        method: "PUT",
        body: JSON.stringify(newBaker),
        headers: {
            "Content-Type": "application/json"
        }
    }).then(response => {
        let nameFail = document.getElementById("nameFail")
        let incomeFail = document.getElementById("incomeFail")
        let bakerSucces = document.getElementById("bakerSuccess") 
        
        let valName = document.getElementById("name").value.length;
        let valIncome = document.getElementById("income").value;
        
        if (response.status === 400) {
            
            if (valName<=3) {
                bakerSucces.innerHTML = ""
                nameFail.innerHTML = "Name should be at least 3 characters long"
            }
            
            if (valIncome>=10000 || valIncome<=1500){
                bakerSucces.innerHTML = ""
                incomeFail.innerHTML = "Income must be between 1 500 & 10 000"
            }
        } else if (response.status === 204) {
            nameFail.innerHTML = ""
            incomeFail.innerHTML = ""
            bakerSucces.innerHTML = "Baker changes saved!"
        }
    })
        .catch(error => {
            alert(error.message)
        });
}