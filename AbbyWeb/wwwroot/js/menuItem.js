$(document).ready(function () {
    $('#DT_load').Datatable();
});
alert("hi")
function Delete(id) {
    alert("here")
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/api/MenuItem/${id}`, // Use the API endpoint
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                    }
                }
            })
        }
    });
}
