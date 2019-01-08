
$(document).ready(function () {


    $('.accordion-group').on('click.collapse-next.data-api', '[data-toggle=collapse-next]', function () {
        var $target = $(this).parent().next()
        $target.data('collapse') ? $target.collapse('toggle') : $target.collapse('toggle')
    });
}