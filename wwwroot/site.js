
function myFunction() {
    alert("You do not have permission ");
}
function compareDates(startDate, endDate) {
    debugger
    startDate = document.getElementById("fromdate");
    endDate = document.getElementById("ToDate");

    if (endDate< startDate) {
        alert("Error !");
    }
}