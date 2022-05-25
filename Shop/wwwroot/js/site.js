// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
﻿function DisplayModalStacked(url, modalNr, data) {
    $.ajax({
        type: "GET",
        url: url,
        data: data,
        success: function (result) {
            $("#myGlobalModalStacked" + modalNr).find(".modal-content").html(result);
            $("#myGlobalModalStacked" + modalNr).modal('show');
        },
        error: function (request, status, error) {
            alert("Failed to load! Please contact the Administrator!");
        },

        traditional: true
    });
}
