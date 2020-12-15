$(function () {
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            $('#modal-placeholder').html(data);
            $('#modal-placeholder > .modal').modal('show');
        });
    });
});

