// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.



document.getElementById('search').addEventListener('click', ShowElement, false);
function ShowElement() {
    const curDisplayState = document.getElementById('SearchFields').style.display;
    if (curDisplayState == 'none' || curDisplayState == '') {
        document.getElementById('SearchFields').style.display = 'table-row';
    } else {
        document.getElementById('SearchFields').style.display = 'none';
    }
}




//function Filter(fieldsList, visibleitemsId) {

//    /* If list of fields is empty, show all dishes */
//    if (fieldsList.length == 0) {
//        var ItemsCount = 0;

//        $('#DishesBody').children('tr').each(function () {
//            var matched = false;

//            for (var i = 0; i < visibleitemsId.length; i++) {
//                if ($(this).attr('id') == visibleitemsId[i]) {
//                    matched = true;
//                    ItemsCount++;
//                    break;

//                }
//            }
//            if (matched == true) {
//                if ($(this).is(":hidden")) {
//                    $(this).show();

//                    $('#SearchFields').css('display', 'table-row');
//                }
//            }
//            if (matched == false) {
//                $(this).hide();
//                $('#SearchFields').css('display', 'table-row');

//            }
//        })
//        $('#Filtred').text(ItemsCount);

//    } else {

//        /* Get the id of elements and show/hide him */

//        $.post("/Filter/Filtration", JSON.stringify(fieldsList), function (result) {
//            console.log(result);
//            $('#Filtred').text(result.length);
//            var ItemsCount = 0;
//            $('tbody').children('tr').each(function () {
//                var matched = false;


//                for (var i = 0; i < result.length; i++) {
//                    if ($(this).attr('id') == result[i]) {
//                        matched = true;
//                        ItemsCount++;
//                        break;

//                    }
//                }
//                if (matched == true) {
//                    if ($(this).is(":hidden")) {
//                        $(this).show();

//                        $('#SearchFields').css('display', 'table-row');
//                    }
//                }
//                if (matched == false) {
//                    $(this).hide();
//                    $('#SearchFields').css('display', 'table-row');

//                }
//            })
//            $('#Filtred').text(ItemsCount);
//        }, "json")

//    }
//}

//function LoadVisibleItems(visibleitemsId) {
//    var t = 0;
//    $('#DishesBody').children('tr').each(function () {
//        for (var i in visibleitemsId) {
         
//            if ($(this).attr('id') == visibleitemsId[i]) {
//                /*Why this isn't working?*/
//                $(this).show();
//                /**/
//              //  $(this).css('visibility', 'visible');
//                t++;
//            }
          
//        }
//    })
//    $('#Filtred').text(t);

//}
