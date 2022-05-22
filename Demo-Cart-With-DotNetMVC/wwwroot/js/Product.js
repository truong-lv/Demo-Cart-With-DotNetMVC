function saveComment() {
    let value = document.getElementById("comment").value;
    sessionStorage.setItem("comment",value)
}

function loadComment() {
    let value = sessionStorage.getItem("comment");
    console.log(value+"5")
    document.getElementById("comment").value = value;
}