//make the select box single select for companies type
//document.querySelectorAll("#CompaniesType")[0].removeAttribute("multiple");



    function deleteRow(button) {
        var row = button.closest('.row');
    row.remove();
    }

var rowIndex = 0; // Initialize the index

function addDocumentRow() {
    rowIndex++; // Increment the index

    // Clone the last row and clear its input values
    var newRow = $('.container.row:last').clone();
    newRow.find('input, select').val('');

    // Update the names and IDs of input fields in the new row
    newRow.find('[name]').each(function () {
        var oldName = $(this).attr('name');
        var newName = oldName.replace('[0].', '[' + rowIndex + '].');
        $(this).attr('name', newName);
        $(this).attr('id', newName);
    });

    // Append the new row to the container
    $('#line').append(newRow);
}
