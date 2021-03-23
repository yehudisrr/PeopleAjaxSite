$(() => {
    function fillTable() {
        $("tbody").empty();
        $.get('home/getall', function (ppl) {
            ppl.forEach(p => {
                $("tbody").append(`
<tr>
    <td>${p.firstName}</td>
    <td>${p.lastName}</td>
    <td>${p.age}</td>
    <td>
      <button class="btn btn-outline-success edit-button" data-id="${p.id}" data-first-name="${p.firstName}" data-last-name="${p.lastName}" data-age="${p.age}">Edit</button>
    </td>
    <td>
      <button class="btn btn-outline-danger delete-button" data-id="${p.id}">Delete</button>
    </td>
</tr>`);
            });
        });
    }
    fillTable();

    $("#add").on('click', function () {
        const firstName = $("#first-name").val();
        const lastName = $("#last-name").val();
        const age = $("#age").val();
        $("#first-name").val('');
        $("#last-name").val('');
        $("#age").val('');

        $.post('home/add', { firstName, lastName, age }, function (p) {
            fillTable();
        });

    });

    $('.edit').on('click', '.edit-button', function () {
        const id = $(this).data('id');
        $('#id').val(id);
        const firstName = $(this).data('first-name');
        $('#firstName').val(firstName);
        const lastName = $(this).data('last-name');
        $('#lastName').val(lastName);
        const age = $(this).data('age');
        $('#modalAge').val(age);
        $('#edit-modal').modal();
    });

    $("#save").click(function () {
        const id = $('#id').val();
        const firstName = $('#firstName').val();
        const lastName = $('#lastName').val();
        const age = $('#modalAge').val();
       
        $.post('home/edit', { id, firstName, lastName, age }, function (p) {
            fillTable();
        });
        $('#edit-modal').modal('hide');
    });

    $('.edit').on('click', '.delete-button', function () {
        const id = $(this).data('id');
        $('#id').val(id);
        $.post('home/delete', {id}, function (p) {
            fillTable();
        });
    });

})