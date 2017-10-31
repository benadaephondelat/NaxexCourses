/**
 * Creates the edit and the delete button in the EditCourses.cshtml view and handles their click events
 * @dependencies: jQuery, mvc-grid
 * @param {function} jQuery
 * @param {function} mvc-grid.js
 */
var manageCoursesGridModule = (function (jQuery) {
    if (typeof jQuery === 'undefined') {
        throw new Error('jQuery is not found.');
    }

    /* function declarations */
    var createButtons,
        gridRowClickHandler,
        gridIsEmpty,
        createPostRequestAjaxCall,
        initiliazieModule;

    /* variables */
    var editCourseButtonId = 'edit-course-button',
        editCourseAjaxUrl = '/ManageCourses/EditCourse',
        deleteCourseButtonId = 'delete-course-button',
        deleteCourseAjaxUrl = '/ManageCourses/DeleteCourse';

    /* DOM objects */
    var antiForgeryToken = $('#anti-forgery-token').attr('value');

    createButtons = function () {
        $(this).append('<button id="edit-course-button" class="btn btn-success">Edit</button>');
        $(this).append('<button id="delete-course-button" class="btn btn-danger">Delete</button>');
    };

    gridRowClickHandler = function (currentGridRow) {
        $(currentGridRow).on('click', function (event) {
            var clickedElement = event.target.id;

            if (clickedElement === editCourseButtonId) {
                var gameId = $(currentGridRow).find('td.course-id').html();

                var data = JSON.stringify({
                    gameId: gameId
                });

                createPostRequestAjaxCall(antiForgeryToken, data, editCourseAjaxUrl);
            } else if (clickedElement === deleteCourseButtonId) {
                var gameId = $(currentGridRow).find('td.course-id').html();

                var data = JSON.stringify({
                    gameId: gameId
                });

                createPostRequestAjaxCall(antiForgeryToken, data, deleteCourseAjaxUrl).done(function (result) {
                    window.location.reload();
                });
            }
        });
    };

    gridIsEmpty = function(gridRow) {
        if (gridRow.innerText.indexOf('No data found') !== -1) {
            return true;
        }

        return false;
    }

    createPostRequestAjaxCall = function(token, data, url) {
        var ajaxCall = $.ajax({
            url: url,
            type: 'POST',
            headers: { 'RequestVerificationToken': token },
            data: data,
            contentType: 'application/json',
            dataType: 'html',
            async: true
        });

        return ajaxCall;
    }

    initiliazieModule = function () {
        $('.mvc-grid').mvcgrid();

        $('.mvc-grid tbody tr').each(function (index, currentGridRow) {
            if (gridIsEmpty(currentGridRow)) {
                return false;
            }

            createButtons.call(currentGridRow);

            gridRowClickHandler(currentGridRow);
        });
    };

    return {
        init: initiliazieModule
    }

})(jQuery || {});

manageCoursesGridModule.init();