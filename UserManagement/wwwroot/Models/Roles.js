function Delete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/Accounts/DeleteRole?Id=${id}';
            Swal.fire(
                'Deleted!',
                'Your Role has been deleted.',
                'success'
            )
        }
    })
}
function = Edit(id, name) => {
    document.getElementById("title").innerHTML = "Edit User Role";
    document.getElementById("Save").innerHTML = "Edit";
    document.getElementById("roleId").value = id;
    document.getElementById("roleName").value = name;
}
function =reset()=> {
    document.getElementById("title").innerHTML = "Edit User Role";
    document.getElementById("Save").innerHTML = "Edit";
    document.getElementById("roleId").value = "";
    document.getElementById("roleName").value = "";
}