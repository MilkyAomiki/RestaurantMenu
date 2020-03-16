// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
