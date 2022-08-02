function ChangeColor(elementid, selectclass) {
    var tr = document.getElementById(elementid);
    if (selectclass == "0")
        tr.style.background = "rgba(200,245, 3, 0.700)";
    else if (selectclass == "1")
        tr.style.background = "rgba(24, 130, 172, 0.600)";
    else if (selectclass == "2")
        tr.style.background = "rgba(131, 64, 128, 0.500)";
}
function login(email, password, role, team) {
    sessionStorage.setItem("email", email);
    sessionStorage.setItem("password", password);
    sessionStorage.setItem("role", role);
    sessionStorage.setItem("team", team);
    console.log(sessionStorage.getItem("email"));
    console.log(sessionStorage.getItem("password") + " " + sessionStorage.getItem("role") + " " + sessionStorage.getItem("team"));
}

function useremail() {
    let getemail = sessionStorage.getItem("email");
    return getemail;
}

function userpassword() {
    let getpassword = sessionStorage.getItem("password");
    return getpassword;
}

function userrole() {
    let getrole = sessionStorage.getItem("role");
    return getrole;
}

function gologin() {
    window.location.href = '/login';
}



function openupdateteam() {
    document.getElementById("teamid").style.visibility = "visible";
    document.getElementById("teamname").style.visibility = "visible";
    document.getElementById("teamname1").style.visibility = "hidden";
    document.getElementById("updatebutton").style.visibility = "visible";
    document.getElementById("addbutton").style.visibility = "hidden";
    document.getElementById("deletebutton").style.visibility = "hidden";
}

function openaddteam() {
    document.getElementById("teamid").style.visibility = "hidden";
    document.getElementById("teamname").style.visibility = "hidden";
    document.getElementById("teamname1").style.visibility = "visible";
    document.getElementById("updatebutton").style.visibility = "hidden";
    document.getElementById("addbutton").style.visibility = "visible";
    document.getElementById("deletebutton").style.visibility = "hidden";
}

function opendeleteteam() {
    document.getElementById("teamid").style.visibility = "visible";
    document.getElementById("teamname").style.visibility = "hidden";
    document.getElementById("teamname1").style.visibility = "hidden";
    document.getElementById("updatebutton").style.visibility = "hidden";
    document.getElementById("addbutton").style.visibility = "hidden";
    document.getElementById("deletebutton").style.visibility = "visible";
}

function goadmin() {
    setTimeout(() => {
    window.location.href = '/userconfirm';
    }, 1500)
}

function goleader() {
    setTimeout(() => {
    window.location.href = '/assigntask';
    }, 1500)
}

function goofficer() {
    setTimeout(() => {
    window.location.href = '/officer';
    }, 1500)
}


window.mulselect = function (sel) {
    var results = [];
    var i;
    for (i = 0; i < sel.options.length; i++) {
        if (sel.options[i].selected) {
            results[results.length] = sel.options[i].value;
        }
    }
    return results;
};


function OpenAddTasks() {
    document.getElementById("addtask").style.display = "block";
    document.getElementById("assigntask").style.display = "none";
}

function OpenAssignTask() {
    document.getElementById("assigntask").style.display = "block";
    document.getElementById("addtask").style.display = "none";
}


