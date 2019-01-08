//Loading Hint or Searching
var loadingHint = function () {
    if ($('#mySearchData').is(':empty')) {
        $("#loading").text("Please wait until the content is loaded. It might take time for the results to emerged, but we promise you intresting results...");
    }
}

//Comments Collapsing
var collapseComments = function(){
    $('.accordion-group').on('click.collapse-next.data-api', '[data-toggle=collapse-next]', function () {

    var $target = $(this).parent().next()
    $target.data('collapse') ? $target.collapse('toggle') : $target.collapse('toggle')
    });
}