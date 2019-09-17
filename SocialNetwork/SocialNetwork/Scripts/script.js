$(document).ready(function() {
    console.log("Document ready!");
});

setTimeout(function() {
    window.location.replace('@Url.Action("Login", "Account")');
}, 3000);