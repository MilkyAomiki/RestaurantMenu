﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


document.getElementById('search').addEventListener('click', ShowElement, false);
function ShowElement()
{
    const curDisplayState = document.getElementById('SearchFields').style.display;
    if (curDisplayState == 'none' || curDisplayState == '')
    {
        document.getElementById('SearchFields').style.display = 'table-row';
    } else {
        document.getElementById('SearchFields').style.display = 'none';
    }
}

/*Bind filtration to every field*/
/*Filtration: On (remove) input char to field, look at every field, find filled, and call "Filter()" */
$('#SearchFields').children('th').children('input').each(function () {
    $(this).keyup(function (e) {

        var fieldsList = new Array();
        
        for (var i in $('#SearchFields').children('th').children('input')) {

            
            if (i == "length") {
                Filter(fieldsList);
                return false;
            }
            var fieldname = $('#SearchFields').children('th').children('input').eq(i).attr('name');
            console.log(fieldname);
            var fieldvalue = $('#SearchFields').children('th').children('input').eq(i).val();
            console.log(fieldvalue);

            if (fieldname != null && fieldvalue != "") {
                var field = { name: fieldname, value: fieldvalue }
                fieldsList.push(field);
            }
            
        }

    });
});
function Filter(fieldsList) {

    /* If list of fields is empty, show all dishes */
    if (fieldsList.length == 0) {
        $('tbody').children('tr').each(function () { $(this).show(); $('#SearchFields').css('display', 'table-row');})
    } else {

        /* For each element in list of fields hide elements */
        for (var i = 0; i < fieldsList.length; i++) {


            $.post("/Filter/Filtration?name=" + fieldsList[i].name + "&filter=" + fieldsList[i].value, null, function (result) {
                console.log(result);
                $('#Filtred').text(result.length);
                $('tbody').children('tr').each(function () {
                    var matched = false;

                    for (var i = 0; i < result.length; i++) {
                        if ($(this).attr('id') == result[i]) {
                            matched = true;
                            console.log("<b> "+$(this).attr('name') + " is matched " + "</b>");
                            break;
                    
                        }
                    }
                    if (matched == false) {
                        $(this).hide();
                        $('#SearchFields').css('display', 'table-row');

                    }
                })
            })
        }
    }
}
