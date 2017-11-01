/**
 * Handles the event in the available courses partial view
 * @dependencies: jQuery
 * @param {function} jQuery
 */
var availableCoursesModule = (function (jQuery) {
    if (typeof jQuery === 'undefined') {
        throw new Error('jQuery is not found.');
    }

    /* function declarations */
    var registerButtonClickHandler,
        initiliazieModule;

    /* variables */
    var registerCourseUrl = '/ListCourses/RegisterToCourse';

    registerButtonClickHandler = function () {
        $('.register-course-button').off().on('click', function (event) {
            var token = $('#token').attr('value');

            var data = $(event.target).attr('data-course-id');

            var data = JSON.stringify({
                courseId: data
            });

            $.ajax({
                url: registerCourseUrl,
                type: 'POST',
                headers: { 'RequestVerificationToken': token },
                data: data,
                contentType: 'application/json',
                dataType: 'html',
                async: true
            }).done(function (result) {
                window.location = window.location.origin + '/ListCourses/Index'
            }).fail(function (response) {
                if (response.status === 400) {
                    $('#modal-text').text(response.statusText);
                    $('#open-modal').click();
                }
            });
        });
    }

    initiliazieModule = function () {
        registerButtonClickHandler();
    };

    return {
        init: initiliazieModule
    }

})(jQuery || {});

availableCoursesModule.init();