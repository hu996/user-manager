$(document).ready(function () {
    $('#tableRole').DataTable({
        "autoWidth": false,
        "responsive": true
    });
});

function Delete(id) {
    Swal.fire({
        title: lbTitleMsgDelete,
        text: lbTextMsgDelete,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: lbconfirmButtonText,
        cancelButtonText: lbcancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = `/Category/Delete?Id=${id}`;
            Swal.fire(
                lbTitleDeletedOk,
                lbMsgDeletedOkRegister,
                lbSuccess
            )
        }
    })
}

Edit = (id, name,Description) => {
    document.getElementById("title").innerHTML = Edit;
    document.getElementById("Save").value = Edit;
    document.getElementById("CategoryId").value = id;
    document.getElementById("CategoryName").value = name;
    document.getElementById("Description").value = Description;
   
}

Rest = () => {
    document.getElementById("title").innerHTML = lbAddNewRole;
    document.getElementById("btnSave").value = lbbtnSave;
    document.getElementById("userId").value = "";
    document.getElementById("userName").value = "";
    document.getElementById("userEmail").value = "";
   

}

function ChangePassword(id) {

    document.getElementById('userPassId').value = id;
}
function openCity(evt, cityName) {
  // Declare all variables
  var i, tabcontent, tablinks;

  // Get all elements with class="tabcontent" and hide them
  tabcontent = document.getElementsByClassName("tabcontent");
  for (i = 0; i < tabcontent.length; i++) {
    tabcontent[i].style.display = "none";
  }

  // Get all elements with class="tablinks" and remove the class "active"
  tablinks = document.getElementsByClassName("tablinks");
  for (i = 0; i < tablinks.length; i++) {
    tablinks[i].className = tablinks[i].className.replace(" active", "");
  }

  // Show the current tab, and add an "active" class to the button that opened the tab
  document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
    // Default Tab
    document.getElementById("defaultOpen").click();
}